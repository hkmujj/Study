using CommonUtil.Model;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using Excel.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Urban.ATC.Siemens.Subsystem.Inteeface;
using Urban.ATC.Siemens.Subsystem.Model.Comfig;

namespace Urban.ATC.Siemens.Subsystem.Server
{
    public class InterfaceAdapterService : IInterfaceAdapterService
    {
        public IReadOnlyDictionary<string, int> InBoolDictionary { get; private set; }
        public IReadOnlyDictionary<string, int> InFloatDictionary { get; private set; }
        public IReadOnlyDictionary<string, int> OutBoolDictionary { get; private set; }
        public IReadOnlyDictionary<string, int> OutFloatDictionary { get; private set; }

        public void Initalize(string configFile)
        {
            if (!File.Exists(configFile))
            {
                AppLog.Info(string.Format("File {0} is not exist! Can not initalize InterfaceAdapterService", configFile));
            }

            var config = DataSerialization.DeserializeFromXmlFile<CommunicationInterfaceConfig>(configFile);
            if (config == null)
            {
                AppLog.Info(string.Format("Can not parser file {0} when initalize InterfaceAdapterService. ", configFile));
                config = new CommunicationInterfaceConfig();
            }

            InBoolDictionary = LoadInterfaceFile(config.InBoolConfig);
            OutBoolDictionary = LoadInterfaceFile(config.OutBoolConfig);
            InFloatDictionary = LoadInterfaceFile(config.InFloatConfig);
            OutFloatDictionary = LoadInterfaceFile(config.OutFloatConfig);
        }

        protected virtual IReadOnlyDictionary<string, int> LoadInterfaceFile(ExcelReaderConfig excelReaderConfig)
        {
            if (excelReaderConfig == null)
            {
                AppLog.Debug("Call InterfaceAdapterService.LoadInterfaceFile,but excelReaderConfig == null , reture empty dictionary");
                return new ReadOnlyDictionary<string, int>(new Dictionary<string, int>());
            }

            DataSet dt = null;
            var dic = new Dictionary<string, int>();
            try
            {
                dt = excelReaderConfig.Adapter();
            }
            catch (Exception e)
            {
                AppLog.Error(string.Format("Can not parser file {0} when LoadInterfaceFile. {1}", excelReaderConfig.File, e));
            }
            if (dt != null && dt.Tables.Count != 0)
            {
                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    dic.Add(row[0].ToString(), Convert.IsDBNull(row[1]) ? int.MaxValue : Convert.ToInt32(row[1]));
                }
            }

            AppLog.Info(string.Format("Load interfacel complated, loaded {0} count result.", dic.Count));

            return dic.AsReadOnlyDictionary();
        }
    }
}
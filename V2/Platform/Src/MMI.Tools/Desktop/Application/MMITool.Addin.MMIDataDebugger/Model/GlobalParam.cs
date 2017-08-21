using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using Excel.Interface;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.MMIDataDebugger.Config.Model;

namespace MMITool.Addin.MMIDataDebugger.Model
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        private GlobalParam()
        {
            
        }

        public Lazy<MMIDataDebuggerConfig> DataDebuggerConfig { get; private set; }

        public Lazy<Dictionary<AppIndexType, ReadOnlyCollection<IndexDescriptionConfig>>> IndexDescriptionConfigDic { get; private set; }

        public void Initalize()
        {
            DataDebuggerConfig = new Lazy<MMIDataDebuggerConfig>(() =>
            {
                var apps = ServiceLocator.Current.GetInstance<IApplicationService>();

                return
                    DataSerialization.DeserializeFromXmlFile<MMIDataDebuggerConfig>(Path.Combine(apps.AddinConfigPath,
                        MMIDataDebuggerConfig.File));
            });

            IndexDescriptionConfigDic = new Lazy<Dictionary<AppIndexType, ReadOnlyCollection<IndexDescriptionConfig>>>(
                () =>
                {
                    var dic = new Dictionary<AppIndexType, ReadOnlyCollection<IndexDescriptionConfig>>();
                    var apps = ServiceLocator.Current.GetInstance<IApplicationService>();
                    foreach (var e in DataDebuggerConfig.Value.ValueDescriptionConfigItems)
                    {
                        e.File = Path.Combine(apps.AddinConfigPath, e.File);

                        var dt = e.Adapter();

                        var lst = (from DataRow row in dt.Tables[0].Rows
                            select new IndexDescriptionConfig()
                            {
                                Name = row["Name"].ToString(), Index = int.Parse(row["Index"].ToString())
                            }).OrderBy(o => o.Index).ToList();

                        dic.Add(e.IndexType,
                            new ReadOnlyCollection<IndexDescriptionConfig>(lst));
                    }

                return dic;
            });
        }
    }
}
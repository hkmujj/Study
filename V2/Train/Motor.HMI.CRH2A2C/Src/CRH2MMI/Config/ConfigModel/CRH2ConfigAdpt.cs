using System.IO;
using CommonUtil.Util;
using CRH2MMI.Common.Global;

namespace CRH2MMI.Config.ConfigModel
{
    internal class CRH2ConfigAdpt
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        public string File { set; get; }

        public string ConfigPath { set; get; }

        public void Init()
        {
            LogMgr.Debug(string.Format("Parser CRH2Config file : {0}.", File));
            CRH2Config = DataSerialization.DeserializeFromXmlFile<CRH2Config>(File);

            CurrentCRH2Config = DataSerialization.DeserializeFromXmlFile<CRH2DetailConfig>(Path.Combine(ConfigPath,
                CRH2Config.DetailConfigFile.GetCurrentDetailConfigFile(CRH2Config.Type))).Data;
            if (CurrentCRH2Config.EspecialConfig == null)
            {
                CurrentCRH2Config.EspecialConfig = new EspecialConfig() {SpeedScale = 1f};
            }

            CurrentPortReaderConfig = CRH2Config.CRH2FileConfig.CommunicationPortReaderConfigs.Find(f => f.Type == CRH2Config.Type);
            CurrentFaultReaderConfig = CRH2Config.CRH2FileConfig.FaultReaderConfigs.Find(f => f.Type == CRH2Config.Type);
        }

        /// <summary>
        /// 配置文件的解析结果
        /// </summary>
        public CRH2Config CRH2Config { private set; get; }

        /// <summary>
        /// 当前的配置
        /// </summary>
        public CRH2ConfigData CurrentCRH2Config { private set; get; }


        public CRH2ExcelReaderConfig CurrentPortReaderConfig { private set; get; }

        public CRH2ExcelReaderConfig CurrentFaultReaderConfig { private set; get; }
    }
}

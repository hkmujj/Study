using System.Collections.Generic;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Other.ContactLine.JW4G.Model
{
    public class GlobalParam
    {
        public static GlobalParam Instance { get; private set; }

        static GlobalParam()
        {
            Instance = new GlobalParam();
        }
        /// <summary>
        /// 逻辑接口索引
        /// </summary>
        public IProjectIndexDescriptionConfig IndexConfig { get; private set; }
        public IEnumerable<FalutInUnit> FalutInUnits { get; set; }
        /// <summary>
        /// 子系统参数
        /// </summary>
        public SubsystemInitParam InitParam { get; private set; }

        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            IndexConfig =
                initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(initParam.AppConfig));
            Initalize(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory, initParam.AppConfig.AppPaths.ConfigDirectory);
        }

        public void Initalize(string rootConfig, string appConfig)
        {
            FalutInUnits = ExcelParser.Parser<FalutInUnit>(rootConfig);
        }
    }
}
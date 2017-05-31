using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Engine.TCMS.Turkmenistan.Model.ConfigModel;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.TCMS.Turkmenistan.Model
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public SubsystemInitParam InitParam { private set; get; }

        public string DesignName { get { return typeof(TurkmenistanSubystem).Namespace; } }

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }

        public ReadOnlyCollection<StateInterfaceItem> StateInterfaceCollection { get; private set; }
        public List<FaultCutUnit> FaultCutUnit { get; private set; }
        public List<AxleBitUnit> AxleBitUnit { get; private set; }
        /// <summary>
        /// 语言是否是土库曼斯坦
        /// </summary>
        public bool IsTurkmenistan { get; set; }
        /// <summary>
        /// 是否显示重联信息 
        /// </summary>
        public bool IsReconnection { get; set; }

        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            IndexDescription =
                initParam.DataPackage.Config.IndexDescriptionConfig
                    .GetProjectIndexDescriptionConfig(new CommunicationDataKey(initParam.AppConfig));
            Initalize(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory,
                initParam.AppConfig.AppPaths.ConfigDirectory);
        }

        public void Initalize(string rootConfigPath, string appConfigPath)
        {
            StateInterfaceCollection = ExcelParser.Parser<StateInterfaceItem>(appConfigPath).ToList().AsReadOnly();
            FaultCutUnit = ExcelParser.Parser<FaultCutUnit>(appConfigPath).ToList();
            AxleBitUnit = ExcelParser.Parser<AxleBitUnit>(appConfigPath).ToList();

        }
    }
}
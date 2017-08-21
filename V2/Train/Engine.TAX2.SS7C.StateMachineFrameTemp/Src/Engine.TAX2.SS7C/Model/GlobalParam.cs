using System.Collections.ObjectModel;
using System.Linq;
using Engine.TAX2.SS7C.Model.ConfigModel;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.TAX2.SS7C.Model
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public SubsystemInitParam InitParam { private set; get; }

        public string DesignName { get { return typeof(SS7CTCMSSubystem).Namespace; } }

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }

        public ReadOnlyCollection<StateInterfaceItem> StateInterfaceCollection { get; private set; }


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
        }
    }
}
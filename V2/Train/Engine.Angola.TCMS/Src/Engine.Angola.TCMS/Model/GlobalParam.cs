using Engine.Angola.TCMS.Model.ConfigModel;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace Engine.Angola.TCMS.Model
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public SubsystemInitParam InitParam { private set; get; }

        public IProjectIndexDescriptionConfig IndexDescription { get; private set; }

        public Lazy<ReadOnlyCollection<StateInterfaceItem>> StateInterfaceCollection { get; private set; }

        private GlobalParam()
        {
            
        }

        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            IndexDescription =
                initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(initParam.AppConfig));

            Initalize(initParam.AppConfig.AppPaths.ConfigDirectory);
        }

        public void Initalize(string appConfigPath)
        {
            StateInterfaceCollection =
                new Lazy<ReadOnlyCollection<StateInterfaceItem>>(
                    () => ExcelParser.Parser<StateInterfaceItem>(appConfigPath).ToList().AsReadOnly());
        }
    }
}
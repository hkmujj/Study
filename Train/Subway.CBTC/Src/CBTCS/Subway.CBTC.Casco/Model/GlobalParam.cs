using System;
using System.Collections.ObjectModel;
using System.Linq;
using CBTC.Infrasturcture.Model.Msg.Details;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.CBTC.Casco.Model.ConfigModel;

namespace Subway.CBTC.Casco.Model
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public bool IsDebug { get; private set; }

        public SubsystemInitParam InitParam { private set; get; }

        public string DesignName
        {
            get { return typeof(CBTCCascoSubystem).Namespace; }
        }

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }

        public ReadOnlyCollection<StateInterfaceItem> StateInterfaceCollection { get; private set; }

        public Lazy<ReadOnlyCollection<IInformationContent>> InformationContents { get; private set; }

        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            IndexDescription =
                initParam.DataPackage.Config.IndexDescriptionConfig
                    .GetProjectIndexDescriptionConfig(new CommunicationDataKey(initParam.AppConfig));
            Initalize(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory,
                initParam.AppConfig.AppPaths.ConfigDirectory, initParam.DataPackage.Config.SystemConfig.IsDebugModel);
        }

        public void Initalize(string rootConfigPath, string appConfigPath, bool isDebug)
        {
            IsDebug = isDebug;

            InformationContents =
                new Lazy<ReadOnlyCollection<IInformationContent>>(
                    () =>
                        ExcelParser.Parser<InformationContent>(appConfigPath)
                            .Cast<IInformationContent>()
                            .ToList()
                            .AsReadOnly());

            //StateInterfaceCollection = ExcelParser.Parser<StateInterfaceItem>(appConfigPath).ToList().AsReadOnly();
        }
    }
}
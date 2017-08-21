using System.Collections.Generic;
using System.Linq;
using Engine.TCMS.HXD3.Model.ConfigModel;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.TCMS.HXD3.Model
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public SubsystemInitParam InitParam { private set; get; }

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }

        public List<StateInterfaceItem> StateInterfaceCollection { private set; get; }

        public List<TowEleMachineConfig> TowEleMachineConfigs { private set; get; }

        public List<SwitchStateConfig> SwitchStateConfigs { private set; get; }

        public List<WindMachineStateConfig> WindMachineStateConfigs { private set; get; }

        public List<FaultItemConfig> FaultItemConfigs { private set; get; }

        public List<OpenStateConfig> OpenStateConfigs { private set; get; }

        public List<SignalInfoUnit> SignalInfo { private set; get; }

        public List<ActionNumUnit> ActionNum { get; private set; }

        public List<TransferInfoUnit> TransferInfo { get; private set; }

        public List<SoftWareVersionUnit> SoftWareVersion { get; private set; }

        public List<DisplayLightTestUnit> DisplayLightTestCollection { private set; get; }

        public List<AssistPowerTestUnit> AssistPowerTestCollection { private set; get; }

        public List<ZeroLevelTestUnit> ZeroLevelTestCollection { private set; get; }

        public List<StartTestUnit> StartTestUnitCollection { private set; get; }

        public List<MasterDriverControlTestUnit> MasterDriverControlUnitCollection { private set; get; }

        public List<MainSwitchNotifyConfig> MainControlNotifyConfigCollection { private set; get; }

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
            StateInterfaceCollection = ExcelParser.Parser<StateInterfaceItem>(appConfigPath).ToList();
            TowEleMachineConfigs = ExcelParser.Parser<TowEleMachineConfig>(appConfigPath).ToList();
            SwitchStateConfigs = ExcelParser.Parser<SwitchStateConfig>(appConfigPath).ToList();
            WindMachineStateConfigs = ExcelParser.Parser<WindMachineStateConfig>(appConfigPath).ToList();
            FaultItemConfigs = ExcelParser.Parser<FaultItemConfig>(rootConfigPath).ToList();
            OpenStateConfigs = ExcelParser.Parser<OpenStateConfig>(appConfigPath).ToList();
            SignalInfo = ExcelParser.Parser<SignalInfoUnit>(appConfigPath).ToList();
            ActionNum = ExcelParser.Parser<ActionNumUnit>(appConfigPath).ToList();
            TransferInfo = ExcelParser.Parser<TransferInfoUnit>(appConfigPath).ToList();
            SoftWareVersion = ExcelParser.Parser<SoftWareVersionUnit>(appConfigPath).ToList();
            DisplayLightTestCollection = ExcelParser.Parser<DisplayLightTestUnit>(appConfigPath).ToList();
            AssistPowerTestCollection = ExcelParser.Parser<AssistPowerTestUnit>(appConfigPath).ToList();
            ZeroLevelTestCollection = ExcelParser.Parser<ZeroLevelTestUnit>(appConfigPath).ToList();
            StartTestUnitCollection = ExcelParser.Parser<StartTestUnit>(appConfigPath).ToList();
            MasterDriverControlUnitCollection = ExcelParser.Parser<MasterDriverControlTestUnit>(appConfigPath).ToList();
            MainControlNotifyConfigCollection = ExcelParser.Parser<MainSwitchNotifyConfig>(rootConfigPath).ToList();


            DisplayLightTestCollection.Reverse();
            AssistPowerTestCollection.Reverse();
            ZeroLevelTestCollection.Reverse();
            StartTestUnitCollection.Reverse();
            MasterDriverControlUnitCollection.Reverse();
        }
    }
}
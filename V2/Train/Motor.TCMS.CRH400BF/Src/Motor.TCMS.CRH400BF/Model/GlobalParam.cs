using System;
using System.Collections.ObjectModel;
using System.Linq;
using Motor.TCMS.CRH400BF.Model.ConfigModel;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Motor.TCMS.CRH400BF.Config;
using Motor.TCMS.CRH400BF.ViewModel;

namespace Motor.TCMS.CRH400BF.Model
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public SubsystemInitParam InitParam { private set; get; }

        public string DesignName { get { return typeof(CRH400BFSubystem).Namespace; } }

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }

        public ReadOnlyCollection<StateInterfaceItem> StateInterfaceCollection { get; private set; }

        //public Lazy<ReadOnlyCollection<StateInterfaceItem>> StateInterfaceCollection { get; private set; }

        public DomainViewModel DomainViewModel { get; private set; }

        public Lazy<ReadOnlyCollection<CarBaseConfig>> CarBaseConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarEleBrakeConfig>> CarEleBrakeConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarParkBrakeConfig>> CarParkBrakeConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarAirBrakeConfig>> CarAirBrakeConfigCollection { get; private set; }


        public Lazy<ReadOnlyCollection<CarBcu1StateConfig>> CarBcu1StateConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarAssistAirCompressorStateConfig>> CarAssistAirCompressorStateConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarBcu2StateConfig>> CarBcu2StateConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarBrakeCylinderPressConfig>> CarBrakeCylinderPressConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarEmptySpring1PressConfig>> CarEmptySpring1PressConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarEmptySpring2PressConfig>> CarEmptySpring2PressConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarKeepBrakeStateConfig>> CarKeepBrakeStateConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarParkingCylinderPressConfig>> CarParkingCylinderPressConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarPercentBrakeStateConfig>> CarPercentBrakeStateConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarWindPipePressConfig>> CarWindPipePressConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarDoorConfig>> CarDoorConfigCollection { get; private set; }



        public Lazy<ReadOnlyCollection<CarPantographConfig>> CarPantographConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarMainBreakerConfig>> CarMainBreakerConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarHighPressSwitchConfig>> CarHighPressSwitchConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarTractionConverterConfig>> CarTractionConverterConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarTractionInvertorConfig>> CarTractionInvertorConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarAssistConverterConfig>> CarAssistConverterConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarBatteryChargerConfig>> CarBatteryChargerConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarAirCompressorConfig>> CarAirCompressorConfigCollection { get; private set; }

        //public GlobalParam()
        //{
        //    DomainViewModel = ServiceLocator.Current.GetInstance<DomainViewModel>();
        //}
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
            StateInterfaceCollection = ExcelParser.Parser<StateInterfaceItem>(rootConfigPath).ToList().AsReadOnly();
            //StateInterfaceCollection =
            //    new Lazy<ReadOnlyCollection<StateInterfaceItem>>(
            //       () => ExcelParser.Parser<StateInterfaceItem>(appConfigPath).ToList().AsReadOnly());
            CarBaseConfigCollection =
               new Lazy<ReadOnlyCollection<CarBaseConfig>>(
                   () => ExcelParser.Parser<CarBaseConfig>(rootConfigPath).ToList().AsReadOnly());

            CarEleBrakeConfigCollection= new Lazy<ReadOnlyCollection<CarEleBrakeConfig>>(
                   () => ExcelParser.Parser<CarEleBrakeConfig>(rootConfigPath).ToList().AsReadOnly());

            CarParkBrakeConfigCollection = new Lazy<ReadOnlyCollection<CarParkBrakeConfig>>(
                   () => ExcelParser.Parser<CarParkBrakeConfig>(rootConfigPath).ToList().AsReadOnly());

            CarAirBrakeConfigCollection = new Lazy<ReadOnlyCollection<CarAirBrakeConfig>>(
                   () => ExcelParser.Parser<CarAirBrakeConfig>(rootConfigPath).ToList().AsReadOnly());



            CarBcu1StateConfigCollection = new Lazy<ReadOnlyCollection<CarBcu1StateConfig>>(
                  () => ExcelParser.Parser<CarBcu1StateConfig>(rootConfigPath).ToList().AsReadOnly());

            CarAssistAirCompressorStateConfigCollection = new Lazy<ReadOnlyCollection<CarAssistAirCompressorStateConfig>>(
                   () => ExcelParser.Parser<CarAssistAirCompressorStateConfig>(rootConfigPath).ToList().AsReadOnly());

            CarBcu2StateConfigCollection = new Lazy<ReadOnlyCollection<CarBcu2StateConfig>>(
                   () => ExcelParser.Parser<CarBcu2StateConfig>(rootConfigPath).ToList().AsReadOnly());

            CarBrakeCylinderPressConfigCollection = new Lazy<ReadOnlyCollection<CarBrakeCylinderPressConfig>>(
                  () => ExcelParser.Parser<CarBrakeCylinderPressConfig>(rootConfigPath).ToList().AsReadOnly());

            CarEmptySpring1PressConfigCollection = new Lazy<ReadOnlyCollection<CarEmptySpring1PressConfig>>(
                   () => ExcelParser.Parser<CarEmptySpring1PressConfig>(rootConfigPath).ToList().AsReadOnly());

            CarEmptySpring2PressConfigCollection = new Lazy<ReadOnlyCollection<CarEmptySpring2PressConfig>>(
                   () => ExcelParser.Parser<CarEmptySpring2PressConfig>(rootConfigPath).ToList().AsReadOnly());

            CarKeepBrakeStateConfigCollection = new Lazy<ReadOnlyCollection<CarKeepBrakeStateConfig>>(
                  () => ExcelParser.Parser<CarKeepBrakeStateConfig>(rootConfigPath).ToList().AsReadOnly());

            CarParkingCylinderPressConfigCollection = new Lazy<ReadOnlyCollection<CarParkingCylinderPressConfig>>(
                   () => ExcelParser.Parser<CarParkingCylinderPressConfig>(rootConfigPath).ToList().AsReadOnly());

            CarWindPipePressConfigCollection = new Lazy<ReadOnlyCollection<CarWindPipePressConfig>>(
                   () => ExcelParser.Parser<CarWindPipePressConfig>(rootConfigPath).ToList().AsReadOnly());

            CarPercentBrakeStateConfigCollection = new Lazy<ReadOnlyCollection<CarPercentBrakeStateConfig>>(
                  () => ExcelParser.Parser<CarPercentBrakeStateConfig>(rootConfigPath).ToList().AsReadOnly());

            CarDoorConfigCollection = new Lazy<ReadOnlyCollection<CarDoorConfig>>(
                  () => ExcelParser.Parser<CarDoorConfig>(rootConfigPath).ToList().AsReadOnly());
            CarPantographConfigCollection = new Lazy<ReadOnlyCollection<CarPantographConfig>>(
                () => ExcelParser.Parser<CarPantographConfig>(rootConfigPath).ToList().AsReadOnly());
            CarMainBreakerConfigCollection = new Lazy<ReadOnlyCollection<CarMainBreakerConfig>>(
                () => ExcelParser.Parser<CarMainBreakerConfig>(rootConfigPath).ToList().AsReadOnly());
            CarHighPressSwitchConfigCollection = new Lazy<ReadOnlyCollection<CarHighPressSwitchConfig>>(
                () => ExcelParser.Parser<CarHighPressSwitchConfig>(rootConfigPath).ToList().AsReadOnly());
            CarTractionConverterConfigCollection = new Lazy<ReadOnlyCollection<CarTractionConverterConfig>>(
                () => ExcelParser.Parser<CarTractionConverterConfig>(rootConfigPath).ToList().AsReadOnly());
            CarTractionInvertorConfigCollection = new Lazy<ReadOnlyCollection<CarTractionInvertorConfig>>(
                () => ExcelParser.Parser<CarTractionInvertorConfig>(rootConfigPath).ToList().AsReadOnly());
            CarAssistConverterConfigCollection = new Lazy<ReadOnlyCollection<CarAssistConverterConfig>>(
                () => ExcelParser.Parser<CarAssistConverterConfig>(rootConfigPath).ToList().AsReadOnly());
            CarBatteryChargerConfigCollection = new Lazy<ReadOnlyCollection<CarBatteryChargerConfig>>(
                () => ExcelParser.Parser<CarBatteryChargerConfig>(rootConfigPath).ToList().AsReadOnly());
            CarAirCompressorConfigCollection = new Lazy<ReadOnlyCollection<CarAirCompressorConfig>>(
                () => ExcelParser.Parser<CarAirCompressorConfig>(rootConfigPath).ToList().AsReadOnly());

        }
    }
}
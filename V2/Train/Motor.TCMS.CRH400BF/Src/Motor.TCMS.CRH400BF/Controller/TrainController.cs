using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.TCMS.CRH400BF.Config;
using Motor.TCMS.CRH400BF.Model;
using Motor.TCMS.CRH400BF.Model.Constant;
using Motor.TCMS.CRH400BF.Model.Train;
using Motor.TCMS.CRH400BF.Model.Train.CartPart;
using Motor.TCMS.CRH400BF.ViewModel;

namespace Motor.TCMS.CRH400BF.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TrainController : ControllerBase<TrainViewModel>
    {

        [ImportingConstructor]
        public TrainController(OtherController otherController)
        {

            OtherController = otherController;

        }

        public OtherController OtherController { get; private set; }

        public override void Initalize()
        {
            var tm = ViewModel.Model;

            //tm.TowPage = CreateTowPage();
            //tm.BrakePage = CreateBrakePage();

            tm.TrainStateIcon = new TrainStateIcon();
            tm.CarCollection = CreateCars();


            OtherController.ViewModel = ViewModel;
            OtherController.Initalize();
        }


        private List<Car> CreateCars()
        {
            var its = GlobalParam.Instance.CarBaseConfigCollection.Value;
            return its.Select((s, i) => new Car(s.Name, s.Index)
            {

                Door =
                    new Door(ItemLocationStyle.LeftStyle/*i > 2 ? ItemLocationStyle.RightStyle : ItemLocationStyle.LeftStyle*/)
                    {
                        DoorItems = CreateDoorStates(i),

                    },
                BrakePage = new Lazy<BrakePage>(() => new BrakePage()
                {
                    EleBrake = CreateEleBrakeState(i),
                    AirBrake = CreateAirBrakeState(i),
                    ParkBrake = CreateParkBrakeState(i),


                }),
                BrakeInfoPage = new Lazy<BrakeInfoPage>(() => new BrakeInfoPage()
                {
                    AssistAirCompressorState = CreateAssistAirCompressorState(i),
                    Bcu1State = CreateBcu1State(i),
                    Bcu2State = CreateBcu2State(i),
                    BrakeCylinderPress = CreateBrakeCylinderPress(i),
                    EmptySpring1Press = CreateEmptySpring1Press(i),
                    EmptySpring2Press = CreateEmptySpring2Press(i),
                    KeepBrakeState = CreateKeepBrakeState(i),
                    ParkingCylinderPress = CreateParkingCylinderPress(i),
                    PercentBrakeState = CreatePercentBrakeState(i),
                    WindPipePress = CreateWindPipePress(i),
                }),
                EquipmentCutOffPage = new Lazy<EquipmentCutOffPage>(() => new EquipmentCutOffPage()
                {
                    PantographState = CreatePantographState(i),
                    MainBreakerState = CreateMainBreakerState(i),
                    HighPressSwitchState = CreateHighPressSwitchState(i),
                    TractionConverterState = CreateTractionConverterState(i),
                    TractionInvertorState = CreateTractionInvertorState(i),
                    AssistConverterState = CreateAssistConverterState(i),
                    BatteryChargerState1 = CreateBatteryChargerState1(i),
                    BatteryChargerState2 = CreateBatteryChargerState2(i),
                    AirCompressorState = CreateAirCompressorState(i),
                }),
            }).ToList();

        }

        private CarItem<AirCompressorState, CarAirCompressorConfig> CreateAirCompressorState(int carIndex)
        {
            var itconfig =
           GlobalParam.Instance.CarAirCompressorConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirCompressorState, CarAirCompressorConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Unknow)
            };
        }

        private CarItem<BatteryChargerState, CarBatteryChargerConfig> CreateBatteryChargerState2(int carIndex)
        {
            var itconfig =
           GlobalParam.Instance.CarBatteryChargerConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BatteryChargerState, CarBatteryChargerConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.BatteryCharger2UnKnow)
            };
        }

        private CarItem<BatteryChargerState, CarBatteryChargerConfig> CreateBatteryChargerState1(int carIndex)
        {
            var itconfig =
           GlobalParam.Instance.CarBatteryChargerConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BatteryChargerState, CarBatteryChargerConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.BatteryCharger1UnKnow)
            };
        }

        private CarItem<AssistConverterState, CarAssistConverterConfig> CreateAssistConverterState(int carIndex)
        {
            var itconfig =
            GlobalParam.Instance.CarAssistConverterConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AssistConverterState, CarAssistConverterConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnKnow)
            };
        }

        private CarItem<TractionInvertorState, CarTractionInvertorConfig> CreateTractionInvertorState(int carIndex)
        {
            var itconfig =
           GlobalParam.Instance.CarTractionInvertorConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<TractionInvertorState, CarTractionInvertorConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnKnow)
            };
        }

        private CarItem<TractionConverterState, CarTractionConverterConfig> CreateTractionConverterState(int carIndex)
        {
            var itconfig =
           GlobalParam.Instance.CarTractionConverterConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<TractionConverterState, CarTractionConverterConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnKnow)
            };
        }

        private CarItem<HighPressSwitchState, CarHighPressSwitchConfig> CreateHighPressSwitchState(int carIndex)
        {
            var itconfig =
           GlobalParam.Instance.CarHighPressSwitchConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<HighPressSwitchState, CarHighPressSwitchConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnKnow)
            };
        }

        private CarItem<MainBreakerState, CarMainBreakerConfig> CreateMainBreakerState(int carIndex)
        {
            var itconfig =
           GlobalParam.Instance.CarMainBreakerConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<MainBreakerState, CarMainBreakerConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnKnow)
            };
        }

        private CarItem<PantographState, CarPantographConfig> CreatePantographState(int carIndex)
        {
            var itconfig =
            GlobalParam.Instance.CarPantographConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<PantographState, CarPantographConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnKnow)
            };
        }

        private ObservableCollection<DoorStateItem> CreateDoorStates(int carIndex)
        {
            var doors = GlobalParam.Instance.CarDoorConfigCollection.Value;
            var ds = new ObservableCollection<DoorStateItem>()
            {
                new DoorStateItem(1, doors[carIndex])
                {

                    Visible =carIndex!=4&& !string.IsNullOrWhiteSpace(doors[carIndex].DoorIndex1Open )
                },
                new DoorStateItem(2, doors[carIndex])
                {
                    Visible = carIndex!=4&&!string.IsNullOrWhiteSpace(doors[carIndex].DoorIndex2Open )
                },
                new DoorStateItem(3, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].DoorIndex3Open )
                },
                new DoorStateItem(4, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].DoorIndex4Open )
                },
            };

            return ds;
        }

        private CarItem<float, CarWindPipePressConfig> CreateWindPipePress(int carIndex)
        {
            var itconfig =
             GlobalParam.Instance.CarWindPipePressConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<float, CarWindPipePressConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Value)
            };
        }

        private CarItem<BrakeInfoCommonState, CarPercentBrakeStateConfig> CreatePercentBrakeState(int carIndex)
        {
            var itconfig =
             GlobalParam.Instance.CarPercentBrakeStateConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BrakeInfoCommonState, CarPercentBrakeStateConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Unknow)
            };
        }

        private CarItem<float, CarParkingCylinderPressConfig> CreateParkingCylinderPress(int carIndex)
        {
            var itconfig =
             GlobalParam.Instance.CarParkingCylinderPressConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<float, CarParkingCylinderPressConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Value)
            };
        }

        private CarItem<BrakeInfoCommonState, CarKeepBrakeStateConfig> CreateKeepBrakeState(int carIndex)
        {
            var itconfig =
             GlobalParam.Instance.CarKeepBrakeStateConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BrakeInfoCommonState, CarKeepBrakeStateConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Unknow)
            };
        }

        private CarItem<float, CarEmptySpring2PressConfig> CreateEmptySpring2Press(int carIndex)
        {
            var itconfig =
             GlobalParam.Instance.CarEmptySpring2PressConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<float, CarEmptySpring2PressConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Value)
            };
        }

        private CarItem<float, CarEmptySpring1PressConfig> CreateEmptySpring1Press(int carIndex)
        {
            var itconfig =
              GlobalParam.Instance.CarEmptySpring1PressConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<float, CarEmptySpring1PressConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Value)
            };
        }

        private CarItem<float, CarBrakeCylinderPressConfig> CreateBrakeCylinderPress(int carIndex)
        {
            var itconfig =
             GlobalParam.Instance.CarBrakeCylinderPressConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<float, CarBrakeCylinderPressConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Value)
            };
        }

        private CarItem<BrakeInfoCommonState, CarBcu2StateConfig> CreateBcu2State(int carIndex)
        {
            var itconfig =
             GlobalParam.Instance.CarBcu2StateConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BrakeInfoCommonState, CarBcu2StateConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Unknow)
            };
        }

        private CarItem<BrakeInfoCommonState, CarBcu1StateConfig> CreateBcu1State(int carIndex)
        {
            var itconfig =
             GlobalParam.Instance.CarBcu1StateConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BrakeInfoCommonState, CarBcu1StateConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Unknow)
            };
        }

        private CarItem<BrakeInfoCommonState, CarAssistAirCompressorStateConfig> CreateAssistAirCompressorState(int carIndex)
        {
            var itconfig =
             GlobalParam.Instance.CarAssistAirCompressorStateConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BrakeInfoCommonState, CarAssistAirCompressorStateConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Unknow)
            };
        }

        private CarItem<ValidateParkBrakeItem, CarParkBrakeConfig> CreateParkBrakeState(int carIndex)
        {
            var itconfig =
              GlobalParam.Instance.CarParkBrakeConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateParkBrakeItem, CarParkBrakeConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnKnow),
                State = new ValidateParkBrakeItem(),
            };
        }

        private CarItem<ValidateAirBrakeItem, CarAirBrakeConfig> CreateAirBrakeState(int carIndex)
        {
            var itconfig =
              GlobalParam.Instance.CarAirBrakeConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateAirBrakeItem, CarAirBrakeConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnKnow),
                State = new ValidateAirBrakeItem(),
            };
        }

        private CarItem<ValidateEleBrakeItem, CarEleBrakeConfig> CreateEleBrakeState(int carIndex)
        {

            var itconfig =
               GlobalParam.Instance.CarEleBrakeConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateEleBrakeItem, CarEleBrakeConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnKnow),
                State = new ValidateEleBrakeItem(),
            };

        }
    }
}

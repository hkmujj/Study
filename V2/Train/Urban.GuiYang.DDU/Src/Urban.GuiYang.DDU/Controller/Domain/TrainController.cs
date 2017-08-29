using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Controller.Domain.Train.CarPart;
using Urban.GuiYang.DDU.Model;
using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.Model.Train;
using Urban.GuiYang.DDU.Model.Train.CarPart;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Controller.Domain
{
    [Export]
    public class TrainController : ControllerBase<TrainViewModel>
    {
        [ImportingConstructor]
        public TrainController(BrakeController brakeController, OtherController otherController,
            AirConditionController airConditionController)
        {
            BrakeController = brakeController;
            OtherController = otherController;
            AirConditionController = airConditionController;
        }

        public OtherController OtherController { get; private set; }

        public BrakeController BrakeController { get; private set; }

        public AirConditionController AirConditionController { get; private set; }

        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            var tm = ViewModel.Model;

            tm.CarCollection = CreateCars();

            BrakeController.ViewModel = this.ViewModel;
            BrakeController.Initalize();

            AirConditionController.ViewModel = this.ViewModel;
            AirConditionController.Initalize();

            OtherController.ViewModel = ViewModel;
            OtherController.Initalize();
        }

        private List<Car> CreateCars()
        {
            var its = GlobalParam.Instance.CarBaseConfigCollection.Value;

            return its.Select((s, i) => new Car(s.Name, s.Index)
            {
                DriverRoom = s.IsDriverRoom ? new DriverRoom() : null,
                Door =
                    new Door(i > 2 ? ItemLocationStyle.RightStyle : ItemLocationStyle.LeftStyle)
                    {
                        DoorItems = CreateDoorStates(i),
                        LockStates = CreateDoorsLockStates(i),
                    },
                PECU = new PECU(i > 2 ? ItemLocationStyle.RightStyle : ItemLocationStyle.LeftStyle)
                {
                    PECUItems = CreatePECUs(i)
                },
                Coupling = s.IsDriverRoom ? new Coupling() : null,
                Pantograph = i == 1 || i == 4 ? new Pantograph() : null,
                MainViewPage =
                {
                    AssistInvertorState = CreateAssistInvertorState(i),
                    TractionInvertorState = CreateTractionInvertorState(i),
                    NormalBrake1 = CreateNormalBrake1(i),
                    NormalBrake2 = CreateNormalBrake2(i),
                    HightSwitchState1 = CreateHightSwitchState1(i),
                    HightSwitchState2 = CreateHightSwitchState2(i),
                    ParkingBrake = CreateParkingBrake(i),
                },
                TowPage = new Lazy<TowPage>(() => new TowPage()
                {
                    GroundConnectState = CreateGroundConnectState(i),
                    ElveState = CreateElveState(i),
                    MotoTemperatureState1 = CreateMotoTemperatureState1State(i),
                    MotoTemperatureState2 = CreateMotoTemperatureState2State(i),
                    MotoTemperatureState3 = CreateMotoTemperatureState3State(i),
                    MotoTemperatureState4 = CreateMotoTemperatureState4State(i),
                    TowBrakeKNState = CreateTowBrakeKNState(i),
                }),
                Brake =
                {
                    BrakePage1 = new Lazy<BrakePage1>(() => new BrakePage1()
                    {

                        BogieIsolationValveState1 = CreateBogieIsolationValveState1(i),
                        BogieIsolationValveState2 = CreateBogieIsolationValveState2(i),
                        PackingBrakeIsolationValveState = CreatePackingBrakeIsolationValveState(i),
                        AirCompreeState = CreateAirCompreeState(i),
                        EmergBrakeRelayState1 = CreateEmergBrakeRelayState1(i),
                        EmergBrakeRelayState2 = CreateEmergBrakeRelayState2(i),
                    }),
                    BrakePage2 = new Lazy<BrakePage2>(() => new BrakePage2()
                    {
                        MainPressure = CreateMainPressure(i),
                        BrakePressure1 = CreateBrakePressure1(i),
                        BrakePressure2 = CreateBrakePressure2(i),
                        AirSpringPreesure1 = CreateAirSpringPreesure1(i),
                        AirSpringPreesure2 = CreateAirSpringPreesure2(i),
                    }),
                    BrakePage3 = new Lazy<BrakePage3>(() => new BrakePage3()
                    {
                        Weight = CreateWeight(i),
                        Laden = CreateLaden(i),

                    }),
                },
                AirCondition =
                {
                    AirConditionPage2 = new Lazy<AirConditionPage2>(() => new AirConditionPage2()
                    {
                        Group1NewAirValve1 = CreateGroup1NewAirValve1(i),
                        Group1NewAirValve2 = CreateGroup1NewAirValve2(i),
                        Group2NewAirValve1 = CreateGroup2NewAirValve1(i),
                        Group2NewAirValve2 = CreateGroup2NewAirValve2(i),
                        Group2BackAirValve1 = CreateGroup2BackAirValve1(i),
                        Group2BackAirValve2 = CreateGroup2BackAirValve2(i),
                        Group1BackAirValve1 = CreateGroup1BackAirValve1(i),
                        Group1BackAirValve2 = CreateGroup1BackAirValve2(i),

                    }),

                    AirConditionPage1Collection = new Lazy<List<AirConditionPage1>>(() =>

                        Enum.GetValues(typeof (AirConditionItemType))
                            .Cast<AirConditionItemType>()
                            .Select(ty => new AirConditionPage1(ty)
                            {
                                AirCondition1=CreateAirConition1(i,ty),
                                AirCondition2=CreateAirConition2(i,ty),
                                ControlModel = CreateControlModel(i, ty),
                                CarTemperature=CreateCarTemperature(i,ty),
                            }).ToList()
                        ),
                },
                AssistPage = new Lazy<AssistPage>(() => new AssistPage()
                {
                    CarPowerSwitch = CreatePowerSwitch(i),
                    BatteryChargerState = CreateBatteryChargerState(i),
                    StorageBatterytemperature = CreateStorageBatterytemperature(i),
                    AssistLoadSwitch = CreateAssistLoadSwitch(i),
                    StorageBatteryA = CreateStorageBatteryA(i),
                }),
                MainPageByPass1 = new Lazy<MainPageByPass1>(() => new MainPageByPass1()
                {
                    AlertBypass = CreateAlertBypass(i),
                    MainWindLowPresByPass = CreateMainWindLowPresByPass(i),
                    ParkingRelaseBypass = CreateParkingRelaseBypass(i),
                    NormalBrakeBypass = CreateNormalBrakeBypass(i),
                    DoorBypass = CreateDoorBypass(i),
                }),
                MainPageByPass2 = new Lazy<MainPageByPass2>(() => new MainPageByPass2()
                {
                    CoulplingState = CreateCoulplingState(i),
                    PantographEnableBypass = CreatePantographEnableBypass(i),
                    ATCCutOffBypass = CreateATCCutOffBypass(i),
                    DoorEnableBypass = CreateDoorEnableBypass(i),
                    KeyBypass = CreateKeyBypass(i),
                }),

            }).ToList();
        }

        private CarItem<ControlState, CarControlModelConfig> CreateControlModel(int carIndex, AirConditionItemType type)
        {
            var itconfig =
                GlobalParam.Instance.CarControlModelConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex && f.ItemType == type);

            return new CarItem<ControlState, CarControlModelConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),

            };
        }

        private CarItem<ValidateValueStateItem, CarTemperatureConfig> CreateCarTemperature(int carIndex, AirConditionItemType type)
        {
            var itconfig =
                GlobalParam.Instance.CarTemperatureConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex && f.ItemType == type);

            return new CarItem<ValidateValueStateItem, CarTemperatureConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateValueStateItem(),
            };
        }

        private CarItem<AirConditionState, CarAirConditionConfig> CreateAirConition2(int carIndex, AirConditionItemType type)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex && f.ItemType == type);

            return new CarItem<AirConditionState, CarAirConditionConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Group2UnkownIndex),

            };
        }

        private CarItem<AirConditionState, CarAirConditionConfig> CreateAirConition1(int carIndex, AirConditionItemType type)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex && f.ItemType == type);

            return new CarItem<AirConditionState, CarAirConditionConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Group1UnkownIndex),

            };
        }

        private CarItem<SwitchState, CarKeyBypassConfig> CreateKeyBypass(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarKeyBypassConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<SwitchState, CarKeyBypassConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),

            };
        }

        private CarItem<SwitchState, CarDoorEnableBypassConfig> CreateDoorEnableBypass(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarDoorEnableBypassConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<SwitchState, CarDoorEnableBypassConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),

            };
        }

        private CarItem<SwitchState, CarATCCutOffBypassConfig> CreateATCCutOffBypass(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarATCCutOffBypassConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<SwitchState, CarATCCutOffBypassConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),

            };
        }

        private CarItem<SwitchState, CarPantographEnableBypassConfig> CreatePantographEnableBypass(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarPantographEnableBypassConfigCollection.Value.FirstOrDefault(
                    f => f.Index == carIndex);

            return new CarItem<SwitchState, CarPantographEnableBypassConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),

            };
        }

        private CarItem<CTSState, CarCoulplingStateConfig> CreateCoulplingState(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarCoulplingStateConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<CTSState, CarCoulplingStateConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),

            };
        }

        private CarItem<SwitchState, CarDoorBypassConfig> CreateDoorBypass(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarDoorBypassConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<SwitchState, CarDoorBypassConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),

            };
        }

        private CarItem<SwitchState, CarNormalBrakeBypassConfig> CreateNormalBrakeBypass(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarNormalBrakeBypassConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<SwitchState, CarNormalBrakeBypassConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),

            };
        }

        private CarItem<SwitchState, CarParkingRelaseBypassConfig> CreateParkingRelaseBypass(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarParkingRelaseBypassConfigCollection.Value.FirstOrDefault(
                    f => f.Index == carIndex);

            return new CarItem<SwitchState, CarParkingRelaseBypassConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),

            };

        }

        private CarItem<SwitchState, CarMainWindLowPresByPassConfig> CreateMainWindLowPresByPass(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarMainWindLowPresByPassConfigCollection.Value.FirstOrDefault(
                    f => f.Index == carIndex);

            return new CarItem<SwitchState, CarMainWindLowPresByPassConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),

            };
        }

        private CarItem<SwitchState, CarAlertBypassConfig> CreateAlertBypass(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAlertBypassConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<SwitchState, CarAlertBypassConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),

            };
        }

        private CarItem<BackAirValveState, CarGroup1BackAirValveConfig> CreateGroup1BackAirValve2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarGroup1BackAirValveConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BackAirValveState, CarGroup1BackAirValveConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Valve1UnkownIndex),

            };
        }

        private CarItem<BackAirValveState, CarGroup1BackAirValveConfig> CreateGroup1BackAirValve1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarGroup1BackAirValveConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BackAirValveState, CarGroup1BackAirValveConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Valve1UnkownIndex),

            };
        }

        private CarItem<BackAirValveState, CarGroup2BackAirValveConfig> CreateGroup2BackAirValve2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarGroup2BackAirValveConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BackAirValveState, CarGroup2BackAirValveConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Valve1UnkownIndex),

            };
        }

        private CarItem<BackAirValveState, CarGroup2BackAirValveConfig> CreateGroup2BackAirValve1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarGroup2BackAirValveConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BackAirValveState, CarGroup2BackAirValveConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Valve1UnkownIndex),

            };
        }

        private CarItem<NewAirValveState, CarGroup2NewAirValveConfig> CreateGroup2NewAirValve2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarGroup2NewAirValveConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<NewAirValveState, CarGroup2NewAirValveConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Valve1UnkownIndex),

            };
        }

        private CarItem<NewAirValveState, CarGroup2NewAirValveConfig> CreateGroup2NewAirValve1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarGroup2NewAirValveConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<NewAirValveState, CarGroup2NewAirValveConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Valve1UnkownIndex),

            };
        }

        private CarItem<NewAirValveState, CarGroup1NewAirValveConfig> CreateGroup1NewAirValve2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarGroup1NewAirValveConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<NewAirValveState, CarGroup1NewAirValveConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Valve1UnkownIndex),

            };
        }

        private CarItem<NewAirValveState, CarGroup1NewAirValveConfig> CreateGroup1NewAirValve1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarGroup1NewAirValveConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<NewAirValveState, CarGroup1NewAirValveConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Valve1UnkownIndex),

            };
        }

        private CarItem<ValidateFloatItem, CarStorageBatteryAConfig> CreateStorageBatteryA(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarStorageBatteryAConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarStorageBatteryAConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem(),
            };
        }

        private CarItem<SwitchState, CarAssistLoadSwitchConfig> CreateAssistLoadSwitch(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAssistLoadSwitchConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<SwitchState, CarAssistLoadSwitchConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
            };
        }

        private CarItem<ValidateFloatItem, CarStorageBatterytemperatureConfig> CreateStorageBatterytemperature(
            int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarStorageBatterytemperatureConfigCollection.Value.FirstOrDefault(
                    f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarStorageBatterytemperatureConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem(),
            };
        }

        private CarItem<ValidateFloatStateItem, CarBatteryChargerStateConfig> CreateBatteryChargerState(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarBatteryChargerStateConfigCollection.Value.FirstOrDefault(
                    f => f.Index == carIndex);

            return new CarItem<ValidateFloatStateItem, CarBatteryChargerStateConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatStateItem(),
            };
        }

        private CarItem<SwitchState, CarPowerSwitchConfig> CreatePowerSwitch(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarPowerSwitchConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<SwitchState, CarPowerSwitchConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
            };
        }

        private CarItem<ValidateFloatItem, CarLadenConfig> CreateLaden(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarLadenConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarLadenConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem(),
            };
        }

        private CarItem<ValidateFloatItem, CarWeightConfig> CreateWeight(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarWeightConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarWeightConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem(),
            };
        }

        private CarItem<ValidateFloatItem, CarAirSpringPreesureConfig> CreateAirSpringPreesure2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirSpringPreesureConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarAirSpringPreesureConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Branch2UnkownIndex),
                State = new ValidateFloatItem(),
            };
        }


        private CarItem<ValidateFloatItem, CarAirSpringPreesureConfig> CreateAirSpringPreesure1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirSpringPreesureConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarAirSpringPreesureConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Branch1UnkownIndex),
                State = new ValidateFloatItem(),
            };
        }

        private CarItem<ValidateFloatItem, CarBrakePressureConfig> CreateBrakePressure2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarBrakePressureConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarBrakePressureConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Branch2UnkownIndex),
                State = new ValidateFloatItem(),
            };
        }

        private CarItem<ValidateFloatItem, CarBrakePressureConfig> CreateBrakePressure1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarBrakePressureConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarBrakePressureConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Branch1UnkownIndex),
                State = new ValidateFloatItem(),
            };
        }

        private CarItem<ValidateFloatItem, CarMainPressureConfig> CreateMainPressure(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarMainPressureConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarMainPressureConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem(),
            };
        }

        private CarItem<ElectricRelayState, CarEmergBrakeRelayConfig> CreateEmergBrakeRelayState2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarEmergBrakeRelayConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ElectricRelayState, CarEmergBrakeRelayConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.BeUnkownIndex),

            };
        }

        private CarItem<ElectricRelayState, CarEmergBrakeRelayConfig> CreateEmergBrakeRelayState1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarEmergBrakeRelayConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ElectricRelayState, CarEmergBrakeRelayConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.PreUnkownIndex),

            };
        }

        private CarItem<AirCompreeState, CarAirCompreeConfig> CreateAirCompreeState(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirCompreeConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirCompreeState, CarAirCompreeConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),

            };
        }

        private CarItem<IsolationValveState, CarPackingBrakeIsolationValveConfig> CreatePackingBrakeIsolationValveState(
            int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarPackingBrakeIsolationValveConfigCollection.Value.FirstOrDefault(
                    f => f.Index == carIndex);

            return new CarItem<IsolationValveState, CarPackingBrakeIsolationValveConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),

            };
        }

        private CarItem<IsolationValveState, CarBogieIsolationValveConfig> CreateBogieIsolationValveState2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarBogieIsolationValveConfigCollection.Value.FirstOrDefault(
                    f => f.Index == carIndex);

            return new CarItem<IsolationValveState, CarBogieIsolationValveConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.BeUnkownIndex),

            };
        }

        private CarItem<IsolationValveState, CarBogieIsolationValveConfig> CreateBogieIsolationValveState1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarBogieIsolationValveConfigCollection.Value.FirstOrDefault(
                    f => f.Index == carIndex);

            return new CarItem<IsolationValveState, CarBogieIsolationValveConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.PreUnkownIndex),

            };
        }

        private CarItem<ValidateFloatItem, CarMotoTemperatureConfig> CreateMotoTemperatureState3State(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarMotoTemperatureConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarMotoTemperatureConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Moto3UnkownIndex),
                State = new ValidateFloatItem(),
            };
        }

        private CarItem<ValidateFloatItem, CarMotoTemperatureConfig> CreateMotoTemperatureState4State(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarMotoTemperatureConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarMotoTemperatureConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Moto4UnkownIndex),
                State = new ValidateFloatItem(),
            };
        }

        private CarItem<ValidateFloatItem, CarTowBrakeKNConfig> CreateTowBrakeKNState(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarTowBrakeKNConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarTowBrakeKNConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem(),
            };
        }

        private CarItem<ValidateFloatItem, CarMotoTemperatureConfig> CreateMotoTemperatureState1State(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarMotoTemperatureConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarMotoTemperatureConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Moto1UnkownIndex),
                State = new ValidateFloatItem(),
            };
        }

        private CarItem<ValidateFloatItem, CarMotoTemperatureConfig> CreateMotoTemperatureState2State(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarMotoTemperatureConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarMotoTemperatureConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Moto2UnkownIndex),
                State = new ValidateFloatItem(),
            };
        }


        private CarItem<ValidateFloatItem, CarElveConfig> CreateElveState(int carIndex)
        {

            var itconfig =
                GlobalParam.Instance.CarElveConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarElveConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem(),
            };
        }

        private CarItem<GroundConnectState, CarGroundConnectConfig> CreateGroundConnectState(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarGroundConnectConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<GroundConnectState, CarGroundConnectConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex)
            };
        }

        private CarItem<ParkingBrakeState, CarParkingBrakeConfig> CreateParkingBrake(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarParkingBrakeConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ParkingBrakeState, CarParkingBrakeConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.ApplyIndex)
            };
        }

        private CarItem<SwitchState, CarHightSwitchConfig> CreateHightSwitchState2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarHightSwitchConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<SwitchState, CarHightSwitchConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.BeOnIndex)
            };
        }

        private CarItem<SwitchState, CarHightSwitchConfig> CreateHightSwitchState1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarHightSwitchConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<SwitchState, CarHightSwitchConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.PreOnIndex)
            };
        }

        private CarItem<NormalBrakeState, CarNormalBrakeConfig> CreateNormalBrake1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarNormalBrakeConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<NormalBrakeState, CarNormalBrakeConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.PreApplyIndex)
            };
        }

        private CarItem<NormalBrakeState, CarNormalBrakeConfig> CreateNormalBrake2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarNormalBrakeConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<NormalBrakeState, CarNormalBrakeConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.BeApplyIndex)
            };
        }

        private CarItem<AssistInvertorState, CarAssistInvertorConfig> CreateAssistInvertorState(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAssistInvertorConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AssistInvertorState, CarAssistInvertorConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.NormalIndex)
            };
        }

        private CarItem<TractionInvertorState, CarTowInvertorConfig> CreateTractionInvertorState(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarTowInvertorConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<TractionInvertorState, CarTowInvertorConfig>(carIndex, itconfig)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.NormalIndex)
            };
        }

        private ObservableCollection<DoorLockStateItem> CreateDoorsLockStates(int carIndex)
        {
            return
                new ObservableCollection<DoorLockStateItem>(GlobalParam.Instance.CarDoorLockStateConfigCollection.Value
                    .Where(w => w.CarIndex == carIndex)
                    .OrderBy(o => o.ItemIndex)
                    .Select(s => new DoorLockStateItem(carIndex, s) { } 
                    ));

        }

        private ObservableCollection<PECUItem> CreatePECUs(int carIndex)
        {
            var doors = GlobalParam.Instance.CarPECUConfigCollection.Value;
            var ds = new ObservableCollection<PECUItem>()
            {
                new PECUItem(1, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].PECU1IndexActive)
                },
                new PECUItem(2, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].PECU2IndexActive)
                },
                new PECUItem(3, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].PECU3IndexActive)
                },
                new PECUItem(4, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].PECU4IndexActive)
                },
                new PECUItem(5, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].PECU5IndexActive)
                },
                new PECUItem(6, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].PECU6IndexActive)
                },
                new PECUItem(7, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].PECU7IndexActive)
                },
                new PECUItem(8, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].PECU8IndexActive)
                },
            };

            return ds;
        }

        private ObservableCollection<DoorStateItem> CreateDoorStates(int carIndex)
        {
            var doors = GlobalParam.Instance.CarDoorConfigCollection.Value;
            var ds = new ObservableCollection<DoorStateItem>()
            {
                new DoorStateItem(1, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door1IndexOpen)
                },
                new DoorStateItem(2, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door2IndexOpen)
                },
                new DoorStateItem(3, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door3IndexOpen)
                },
                new DoorStateItem(4, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door4IndexOpen)
                },
                new DoorStateItem(5, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door5IndexOpen)
                },
                new DoorStateItem(6, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door6IndexOpen)
                },
                new DoorStateItem(7, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door7IndexOpen)
                },
                new DoorStateItem(8, doors[carIndex])
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door8IndexOpen)
                },
            };
            return ds;
        }
    }
}
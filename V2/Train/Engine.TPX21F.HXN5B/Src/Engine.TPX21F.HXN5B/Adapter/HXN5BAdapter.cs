using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using Engine.TPX21F.HXN5B.Event;
using Engine.TPX21F.HXN5B.Extension;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Engine.TPX21F.HXN5B.Model.Domain.Monitor.Detail;
using Engine.TPX21F.HXN5B.Model.Interface;
using Engine.TPX21F.HXN5B.Resources.Image.Details;
using Engine.TPX21F.HXN5B.Resources.Keys;
using Engine.TPX21F.HXN5B.ViewModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Event;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;
using MMI.Facility.WPFInfrastructure.Event;

namespace Engine.TPX21F.HXN5B.Adapter
{
    [Export]
    public class HXN5BAdapter : NotificationObject, IDataListener
    {
        public IEventAggregator EventAggregator { private set; get; }

        public ICommunicationDataService DataService { private set; get; }

        public HXN5BViewModel ViewModel { private set; get; }

        private bool m_IsDebugModel;

        [ImportMany]
#pragma warning disable 649
        private IResetSupport[] m_Reseters;
#pragma warning restore 649

        [ImportMany]
#pragma warning disable 649
            private IUpdateDataProvider[] m_UpdateDataProviders;
#pragma warning restore 649

        [ImportingConstructor]
        public HXN5BAdapter(IEventAggregator eventAggregator, HXN5BViewModel viewModel)
        {
            EventAggregator = eventAggregator;
            ViewModel = viewModel;
        }

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            ReadServiceOnDataChangedAsync(sender, dataChangedArgs);
        }

        private void ReadServiceOnDataChangedAsync(object sender,
            CommunicationDataChangedArgs communicationDataChangedArgs)
        {
            EventAggregator.GetEvent<DataServiceDataChangedEvent>()
                .Publish(new DataServiceDataChangedEvent.Args(sender, communicationDataChangedArgs));
        }

        public void Initalize(bool isDebugModel)
        {
            m_IsDebugModel = isDebugModel;

            foreach (var it in m_UpdateDataProviders)
            {
                it.Initalize(isDebugModel);
            }

            DataService = GlobalParam.Instance.InitParam.CommunicationDataService;

            EventAggregator.GetEvent<LazyValueCreatedEvent>().Subscribe(s => DataService.ReadService.RaiseAllDataChanged());

            EventAggregator.GetEvent<SendDataRequestEvent>().Subscribe(SendData);

            EventAggregator.GetEvent<CourseStateChangedEvent>().Subscribe(ResetWhenClassOver, ThreadOption.UIThread);

            GlobalParam.Instance.InitParam.RegistDataListener(this);

            EventAggregator.GetEvent<DataServiceDataChangedEvent>()
                .Subscribe(s => UpdateDatas(s.Sender, s.DataChangedArgs), ThreadOption.UIThread);
        }

        private void ResetWhenClassOver(CourseStateChangedArgs args)
        {
            if (args.CourseService.CurrentCourseState != CourseState.Stoped)
            {
                return;
            }

            foreach (var it in m_Reseters)
            {
                it.Reset();
            }

            DataService.ReadService.RaiseAllDataChanged();
        }

        private void SendData(SendDataRequestEvent.Args args)
        {
            switch (args.Type)
            {
                case SendDataRequestEvent.DataType.InBool:
                    break;
                case SendDataRequestEvent.DataType.OutBool:
                    if (args.Index != -1)
                    {
                        DataService.WriteService.ChangeBool(args.Index, args.ValueB);
                    }
                    else if (!string.IsNullOrWhiteSpace(args.IndexString))
                    {
                        DataService.WriteService.ChangeBool(
                            GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[args.IndexString],
                            args.ValueB);
                    }
                    break;
                case SendDataRequestEvent.DataType.InFloat:
                    break;
                case SendDataRequestEvent.DataType.OutFloat:
                    if (args.Index != -1)
                    {
                        DataService.WriteService.ChangeFloat(args.Index, args.ValueF);
                    }
                    else if (!string.IsNullOrWhiteSpace(args.IndexString))
                    {
                        DataService.WriteService.ChangeFloat(
                            GlobalParam.Instance.IndexDescription.OutFloatDescriptionDictionary[args.IndexString],
                            args.ValueF);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            args.ChangedBools.UpdateIfContains(InBKeys.MMI亮屏, b => ViewModel.Model.IsVisble = b);

            foreach (var it in m_UpdateDataProviders)
            {
                it.UpdateDatas(sender, args);
            }

            UpdateOtherModel(args);

            UpdateRootModel(args);

            UpdateMonitorModel(args);

            UpdateTrainSettingModel(args);

            UpdateSoftSwitchModel(args);

            UpdateBrakeSysytemModel(args);

            UpdateFaults(args);
        }

        private void UpdateTrainSettingModel(CommunicationDataChangedArgs args)
        {
        }

        private void UpdateSoftSwitchModel(CommunicationDataChangedArgs args)
        {
            var ss = ViewModel.Domain.SoftSwitchViewModel.Model;

            foreach (var it in ss.SoftSwitchItemCollection.Value)
            {
                if (!string.IsNullOrWhiteSpace(it.ItemConfig.LogicIndexDown))
                {
                    args.ChangedBools.UpdateIfContains(it.ItemConfig.LogicIndexDown,
                        b => it.Direction = b ? SoftSwitchDirection.RightDown : SoftSwitchDirection.RightUp);
                }
                else if (!string.IsNullOrWhiteSpace(it.ItemConfig.LogicIndexUp))
                {
                    args.ChangedBools.UpdateIfContains(it.ItemConfig.LogicIndexUp,
                        b => it.Direction = b ? SoftSwitchDirection.RightUp : SoftSwitchDirection.RightDown);
                }
            }
        }

        private void UpdateBrakeSysytemModel(CommunicationDataChangedArgs args)
        {
            var model = ViewModel.Domain.BrakeSysViewModel.Model;

            args.ChangedBools.UpdateIfContains(InBKeys.制动系统_主界面_不补风,
                b => model.AirSupply = b ? UseState.Cutoff : UseState.Using);

            args.ChangedBools.UpdateIfContains(InBKeys.制动系统_主界面_600KPa,
                b => model.BrakePressure = b ? AirBrakePressure.KP600 : AirBrakePressure.KP500);
            args.ChangedBools.UpdateIfContains(InBKeys.制动系统_主界面_ATP切除,
                b => model.ATP = b ? UseState.Cutoff : UseState.Using);
            args.ChangedBools.UpdateIfContains(InBKeys.制动系统_主界面_单机切除,
                b => model.SigleTrain = b ? UseState.Cutoff : UseState.Using);

            model.EleAirBrake = GetEleAirBrake();

            args.ChangedFloats.UpdateIfContains(InfKeys.制动系统_流量, f => model.FlowRate = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.制动系统_制动缸, f => model.BrakeCylinderPressure = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.制动系统_总风缸, f => model.TotalAirPressure = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.制动系统_CMM, f => model.CMM = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.制动系统_列车管, f => model.BrakePipePressue = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.制动系统_均衡缸, f => model.BalancePressure = f);
        }


        private void UpdateFaults(CommunicationDataChangedArgs args)
        {
            if (args.RaiseDataChangedType != RaiseCommunicationDataChangedType.ByUserManul)
            {
                foreach (var infoConfig in GlobalParam.Instance.NotifyInfoConfigs.Value)
                {
                    args.ChangedBools.UpdateIfContains(infoConfig.Index, b =>
                    {
                        if (b)
                        {
                            ViewModel.Domain.FaultManagerViewModel.Controller.AddItem(infoConfig);
                        }
                        else
                        {
                            ViewModel.Domain.FaultManagerViewModel.Controller.ResetItem(infoConfig);
                        }
                    });
                }
            }
        }

        private void UpdateOtherModel(CommunicationDataChangedArgs args)
        {
            var other = ViewModel.OtherViewModel.Model;
            args.ChangedFloats.UpdateIfContains(InfKeys.主界面_整车软件版本, f => other.Softversion = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.主界面_车号, f => other.TrainNumber = f);
        }

        private void UpdateRootModel(CommunicationDataChangedArgs args)
        {
            var ms = ViewModel.Domain.Model.MainData.MainStates;
            var main = ViewModel.Domain.Model.MainData;
            args.ChangedFloats.UpdateIfContains(InfKeys.主界面_里程, f => main.Mileage = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.主界面_直流功率, f => main.CurrentWorkRate = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.主界面_速度, f => main.CurrentSpeed = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.主界面_燃油量, f => main.OilSize = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.主界面_主风缸, f => main.MainReservoirPressure = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.主界面_制动缸, f => main.BrakeCylinderPressure = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.主界面_空气流量, f => main.AirFlowRate = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.主界面_柴油机转速, f => main.OilEnginRotationRate = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.主界面_均衡风缸, f => main.BalanceAirCylinder = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.主界面_列车管, f => main.BrakePipe = f);

            main.TowBrakeNewt = GetTowBrakeNewt();

            main.AcceleratedSpeedState = GetAccSpeedState();

            ms.HandlLevel = GetHandlLevel();

            ms.WorkState = GetWorkState();

            ms.Direction = GetTrainDirection();

            args.ChangedBools.UpdateIfContains(InBKeys.主界面_空转,
                b => ms.RacingState = b ? RacingState.Racing : RacingState.Null);

            args.ChangedBools.UpdateIfContains(InBKeys.主界面_喇叭按钮, b => ms.SonaState = b ? SonaState.Sona : SonaState.Null);

            args.ChangedBools.UpdateIfContains(InBKeys.主界面_撒沙指示, b => ms.SandState = b ? SandState.Sand : SandState.Null);

            args.ChangedBools.UpdateIfContains(InBKeys.主界面_重联警铃信号,
                b => ms.ReconnexionAlertState = b ? ReconnexionAlertState.Alert : ReconnexionAlertState.Null);

            args.ChangedBools.UpdateIfContains(InBKeys.主界面_紧急制动,
                b => ms.EmergBrakeState = b ? EmergBrakeState.Occuse : EmergBrakeState.Relase);

            args.ChangedBools.UpdateIfContains(InBKeys.主界面_燃油量低警告,
                b => ms.OilLowAlertState = b ? OilLowAlertState.Low : OilLowAlertState.Null);

            args.ChangedBools.UpdateIfContains(InBKeys.主界面_蓄电池不充电,
                b => ms.StorageBatteryState = b ? StorageBatteryState.Changing : StorageBatteryState.NoChanging);

            ms.ParkingBrakeState = GetParkingBrakeState();

            ms.NormalBrake = GetNormalBrake();

            args.ChangedBools.UpdateIfContains(InBKeys.主界面_警惕,
                b => ms.VigilantState = b ? VigilantState.Vigilanted : VigilantState.Null);

            args.ChangedBools.UpdateIfContains(InBKeys.主界面_PCS断开,
                b => ms.PCSState = b ? PCSState.Relase : PCSState.Unkown);

            args.ChangedBools.UpdateIfContains(InBKeys.主界面_单独制动,
                b => ms.SingleBrakeAlert = b ? SingleBrakeAlert.Alert : SingleBrakeAlert.Null);
            args.ChangedBools.UpdateIfContains(InBKeys.主界面_他车报警,
                b => ms.OtherTrainAlert = b ? OtherTrainAlert.Alert : OtherTrainAlert.Null);
            args.ChangedBools.UpdateIfContains(InBKeys.主界面_惩罚制动,
                b => ms.PenaltyBrake = b ? PenaltyBrake.Brake : PenaltyBrake.Null);
            args.ChangedBools.UpdateIfContains(InBKeys.主界面_BC1隔离,
                b => ms.Bc1CutState = b ? BcCutState.BC1 : BcCutState.Null);
            args.ChangedBools.UpdateIfContains(InBKeys.主界面_BC2隔离,
                b => ms.Bc2CutState = b ? BcCutState.BC2 : BcCutState.Null);

            ms.OperationConsle = GetOperationConsle();
        }

        private double GetTowBrakeNewt()
        {
            var tow = DataService.ReadService.GetInFloatOf(InfKeys.主界面_牵引力);
            if (tow > 0)
            {
                return tow;
            }

            return -Math.Abs(DataService.ReadService.GetInFloatOf(InfKeys.主界面_制动力));
        }

        private void UpdateMonitorModel(CommunicationDataChangedArgs args)
        {
            var monitor = ViewModel.Domain.MonitorViewModel;

            UpdateMonitorPage(monitor.Model.SelfChargingPage, args);

            UpdateMonitorPage(monitor.Model.PhaseControlPage, args);

            UpdateMonitorPage(monitor.Model.OilEngineStartPage, args);

            UpdateMonitorPage(monitor.Model.ECUPage, args);

            UpdateMonitorPage(monitor.Model.TowPage, args);

            if (monitor.Model.DCUPageCollection.IsValueCreated)
            {
                foreach (var monitorPage in monitor.Model.DCUPageCollection.Value)
                {
                    UpdateMonitorPage(monitorPage, args);
                }
            }

            if (monitor.Model.EnginePageCollection.IsValueCreated)
            {
                foreach (var monitorPage in monitor.Model.EnginePageCollection.Value)
                {
                    UpdateMonitorPage(monitorPage, args);
                }
            }

            UpdateMonitorAssist(monitor.Model.MonitorAssistMachinePage, args);
        }

        private void UpdateMonitorAssist(Lazy<MonitorPage<AssistMachineMonitorItem>> page,
            CommunicationDataChangedArgs args)
        {
            if (page.IsValueCreated)
            {
                foreach (var it in page.Value.AllItems)
                {
                    it.COMP1 =
                        DataService.GetInFloatOf(it.AssistMachineItemConfig.COMP1)
                            .ToString(it.AssistMachineItemConfig.ValueFormat);
                    it.COMP2 =
                        DataService.GetInFloatOf(it.AssistMachineItemConfig.COMP2)
                            .ToString(it.AssistMachineItemConfig.ValueFormat);
                    it.RFC1 =
                        DataService.GetInFloatOf(it.AssistMachineItemConfig.RFC1)
                            .ToString(it.AssistMachineItemConfig.ValueFormat);
                    it.RFC2 =
                        DataService.GetInFloatOf(it.AssistMachineItemConfig.RFC2)
                            .ToString(it.AssistMachineItemConfig.ValueFormat);
                    it.TBC =
                        DataService.GetInFloatOf(it.AssistMachineItemConfig.TBC)
                            .ToString(it.AssistMachineItemConfig.ValueFormat);
                }
            }
        }

        private void UpdateMonitorPage(MonitorPage page, CommunicationDataChangedArgs args)
        {
            foreach (var it in page.AllItems.Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.Name)))
            {
                if (it.ItemConfig.IndexTypeString == "Float")
                {
                    if (it.ItemConfig.ValueTypeString == "Float")
                    {
                        it.ContentString =
                            DataService.ReadService.GetInFloatOf(it.ItemConfig.Index1)
                                .ToString(it.ItemConfig.ValueFormat);
                    }
                    else
                    {
                        AppLog.Error("Can not parser data where valutypestring != Float");
                    }
                }
                else if (it.ItemConfig.IndexTypeString == "Bool")
                {
                    if (it.ContentType == typeof(MonitorItemState))
                    {
                        it.ContentMonitorItemState = GetMonitorItemState(it.ItemConfig, args);
                    }
                    else if (it.ContentType == typeof(MonitorControlSwitch))
                    {
                        it.ContentMonitorControlSwitch = GetMonitorControlSwitch(it.ItemConfig, args);
                    }
                    else if (it.ContentType == typeof(CutableItemState))
                    {
                        it.CutableItemState = GetCutableItemState(it.ItemConfig, args);
                    }
                }
                else
                {
                    var msg = string.Format("Can not parser type ({0}), when adapter monitor. name={1}",
                        it.ItemConfig.IndexTypeString, it.ItemConfig.Name);
                    AppLog.Fatal(msg);
                    throw new InvalidDataException(msg);
                }
            }
        }

        private void UpdateMonitorPage(Lazy<MonitorPage> page, CommunicationDataChangedArgs args)
        {
            if (page.IsValueCreated)
            {
                UpdateMonitorPage(page.Value, args);
            }
        }

        private CutableItemState GetCutableItemState(MonitorItemConfig itemConfig, CommunicationDataChangedArgs args)
        {
            var s = CutableItemState.PutInto;
            if (!string.IsNullOrWhiteSpace(itemConfig.Index1) && !string.IsNullOrWhiteSpace(itemConfig.Index2))
            {
                if (DataService.GetInBoolOf(itemConfig.Index1))
                {
                    s = (CutableItemState) 0;
                }
                if (DataService.GetInBoolOf(itemConfig.Index2))
                {
                    s = (CutableItemState) 1;
                }
            }

            else
            {
                var msg = string.Format("To parser {0},  must need 2 index", typeof(CutableItemState));
                AppLog.Fatal(msg);
                throw new InvalidDataException(msg);
            }

            return s;
        }

        private MonitorControlSwitch GetMonitorControlSwitch(MonitorItemConfig itemConfig,
            CommunicationDataChangedArgs args)
        {
            var s = MonitorControlSwitch.Move;

            if (!string.IsNullOrWhiteSpace(itemConfig.Index1) && !string.IsNullOrWhiteSpace(itemConfig.Index2) &&
                !string.IsNullOrWhiteSpace(itemConfig.Index3) && !string.IsNullOrWhiteSpace(itemConfig.Index4))
            {
                if (DataService.GetInBoolOf(itemConfig.Index1))
                {
                    s = (MonitorControlSwitch) 0;
                }
                if (DataService.GetInBoolOf(itemConfig.Index2))
                {
                    s = (MonitorControlSwitch) 1;
                }
                if (DataService.GetInBoolOf(itemConfig.Index3))
                {
                    s = (MonitorControlSwitch) 2;
                }
                if (DataService.GetInBoolOf(itemConfig.Index4))
                {
                    s = (MonitorControlSwitch) 3;
                }
            }

            else
            {
                var msg = string.Format("To parser {0},  must need 4 index", typeof(MonitorControlSwitch));
                AppLog.Fatal(msg);
                throw new InvalidDataException(msg);
            }

            return s;
        }

        private MonitorItemState GetMonitorItemState(MonitorItemConfig itemConfig, CommunicationDataChangedArgs args)
        {
            var s = MonitorItemState.Close;

            if (!string.IsNullOrWhiteSpace(itemConfig.Index1) && !string.IsNullOrWhiteSpace(itemConfig.Index2))
            {
                if (DataService.GetInBoolOf(itemConfig.Index1))
                {
                    s = (MonitorItemState) 0;
                }
                if (DataService.GetInBoolOf(itemConfig.Index2))
                {
                    s = (MonitorItemState) 1;
                }
            }

            else if (!string.IsNullOrWhiteSpace(itemConfig.Index1))
            {
                if (DataService.GetInBoolOf(itemConfig.Index1))
                {
                    s = (MonitorItemState) 0;
                }
            }
            else if (!string.IsNullOrWhiteSpace(itemConfig.Index2))
            {
                s = MonitorItemState.Open;
                if (DataService.GetInBoolOf(itemConfig.Index2))
                {
                    s = (MonitorItemState) 1;
                }
            }
            else
            {
                var msg = string.Format("To parser {0},  must need 2 index", typeof(MonitorItemState));
                AppLog.Fatal(msg);
                throw new InvalidDataException(msg);
            }

            return s;
        }


        private EleAirBrakeState GetEleAirBrake()
        {
            var st = EleAirBrakeState.Unkown;
            if (DataService.GetInBoolOf(InBKeys.制动系统_主界面_空电投入))
            {
                st = EleAirBrakeState.Using;
            }
            if (DataService.GetInBoolOf(InBKeys.制动系统_主界面_空电切除))
            {
                st = EleAirBrakeState.Cutoff;
            }
            return st;
        }

        private OperationConsle GetOperationConsle()
        {
            var d = OperationConsle.Unkown;
            if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_操作台状态错误_操作台占用状态))
            {
                d = OperationConsle.Error;
            }
            else if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_1号操作台占用_操作台占用状态))
            {
                d = OperationConsle.Using1;
            }
            else if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_2号操作台占用_操作台占用状态))
            {
                d = OperationConsle.Using2;
            }

            return d;
        }

        private NormalBrake GetNormalBrake()
        {
            var d = NormalBrake.Unkown;
            if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_常用制动缓解_常用制动状态))
            {
                d = NormalBrake.Relase;
            }
            else if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_常用制动施加_常用制动状态))
            {
                d = NormalBrake.Press;
            }
            else if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_常用制动错误_常用制动状态))
            {
                d = NormalBrake.Error;
            }

            return d;
        }

        private ParkingBrakeState GetParkingBrakeState()
        {
            var d = ParkingBrakeState.Unkown;
            if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_停放制动缓解_停放制动))
            {
                d = ParkingBrakeState.Relase;
            }
            else if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_停放制动施加_停放制动))
            {
                d = ParkingBrakeState.Press;
            }
            else if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_停放制动隔离_停放制动))
            {
                d = ParkingBrakeState.Cutoff;
            }

            return d;
        }

        private TrainDirection GetTrainDirection()
        {
            var d = TrainDirection.Unkown;
            if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_前进_方向))
            {
                d = TrainDirection.Forward;
            }
            else if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_后退_方向))
            {
                d = TrainDirection.Backward;
            }

            return d;
        }

        private WorkState GetWorkState()
        {
            var ws = WorkState.Unkown;
            if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_制动_工况))
            {
                ws = WorkState.Brake;
            }
            else if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_惰转_工况))
            {
                ws = WorkState.Slow;
            }
            else if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_预置_工况))
            {
                ws = WorkState.Preset;
            }
            else if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_牵引_工况))
            {
                ws = WorkState.Tow;
            }
            else if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_工况_紧急制动))
            {
                ws = WorkState.EmergBrake;
            }
            else if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_工况_惩罚制动))
            {
                ws = WorkState.PunishmentBrake;
            }
            else if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_工况_常用制动))
            {
                ws = WorkState.NormalBrake;
            }

            return ws;
        }

        private AcceleratedSpeedState GetAccSpeedState()
        {
            var ss = AcceleratedSpeedState.Zero;
            if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_上升_速度))
            {
                ss = AcceleratedSpeedState.LargerThan0;
            }
            else if (DataService.ReadService.GetInBoolOf(InBKeys.主界面_下降_速度))
            {
                ss = AcceleratedSpeedState.SmallerThan0;
            }
            return ss;
        }

        private HandlLevel GetHandlLevel()
        {
            var hl = (HandlLevel) DataService.ReadService.GetInFloatOf(InfKeys.主界面_手柄档位);

            if (hl == HandlLevel.Unkown)
            {
                hl = (HandlLevel) DataService.ReadService.GetInFloatOf(InfKeys.主界面_制动级位);
            }

            return hl;
        }

    }
}
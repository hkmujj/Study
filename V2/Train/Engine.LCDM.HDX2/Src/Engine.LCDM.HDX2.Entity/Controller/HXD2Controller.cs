using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Resources;
using System.Windows.Input;
using CommonUtil.Controls;
using Engine.LCDM.HDX2.Entity.Constant;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Extension;
using Engine.LCDM.HDX2.Entity.Model;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Model.Domain;
using Engine.LCDM.HDX2.Entity.ViewModel;
using Engine.LCDM.HDX2.Resource;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface;
using PropertySupport = CommonUtil.Util.PropertySupport;

namespace Engine.LCDM.HDX2.Entity.Controller
{
    [Export]
    public class HXD2Controller : NotificationObject
    {
        public HXD2ViewModel ViewModel
        {
            set
            {
                m_ViewModel = value;
                m_RegionManager.RequestNavigate(RegionNames.RootRegion, RootRegionViews.ShutDownView);
                ViewModel.LCDMModel.PropertyChanged += LCDMModelOnPropertyChanged;
                ViewModel.LCDMModel.AirBrake.PropertyChanged += AirBrakeOnPropertyChanged;
            }
            get { return m_ViewModel; }
        }

        public IStateInterfaceFactory StateInterfaceFactory { private set; get; }

        public DelegateCommand MainViewLoadedCommand { private set; get; }

        public DelegateCommand RootViewLoadedCommand { private set; get; }

        private FooterModel FooterModel
        {
            get { return ViewModel.Model.FooterModel; }
        }

        private ISendInterface SendInterface { get { return ViewModel.LCDMModel.SendInterface; } }

        [Import]
        private IRegionManager m_RegionManager;

        private HXD2ViewModel m_ViewModel;

        public IEventAggregator EventAggregator { private set; get; }

        [ImportingConstructor]
        public HXD2Controller(IEventAggregator eventAggregator, IStateInterfaceFactory stateInterfaceFactory)
        {
            StateInterfaceFactory = stateInterfaceFactory;
            MainViewLoadedCommand = new DelegateCommand(OnMainViewLoaded);
            RootViewLoadedCommand = new DelegateCommand(OnRootViewLoaded);
            EventAggregator = eventAggregator;

            eventAggregator.GetEvent<BtnEvent>().Subscribe(OnBtnEvent, ThreadOption.UIThread);

            eventAggregator.GetEvent<CompositePresentationEvent<StateChangedEventArg>>()
                .Subscribe(OnStateChanged, ThreadOption.UIThread);

            eventAggregator.GetEvent<CompositePresentationEvent<RequestChangeMainRegionViewEventArg>>()
                .Subscribe(OnRequestChangeMainRegion, ThreadOption.UIThread);

            eventAggregator.GetEvent<CompositePresentationEvent<RequestChangeMainFooterRegionViewEventArge>>()
                .Subscribe(OnRequestChangeMainFooterRegionView, ThreadOption.UIThread);

            eventAggregator.GetEvent<CompositePresentationEvent<SetTrainIdEventArg>>()
                .Subscribe(OnSetTrainId, ThreadOption.UIThread);

            eventAggregator.GetEvent<CompositePresentationEvent<SetDataTimeEventArg>>()
                .Subscribe(OnSetDataTime, ThreadOption.UIThread);

            eventAggregator.GetEvent<CompositePresentationEvent<SetEventArg>>()
                .Subscribe(OnSetting, ThreadOption.UIThread);

            eventAggregator.GetEvent<CompositePresentationEvent<SetAirBrakeEventArg>>()
                .Subscribe(OnSetAirBrake, ThreadOption.UIThread);

            eventAggregator.GetEvent<CompositePresentationEvent<ChangeResourceEventArgs>>().Subscribe(OnChangeResource, ThreadOption.UIThread);

            eventAggregator.GetEvent<CompositePresentationEvent<PowerStateChangedEventArg>>()
                .Subscribe(UpdateRootViewByPower, ThreadOption.UIThread);

            eventAggregator.GetEvent<CompositePresentationEvent<EmergenceTimeUpEventArg>>().Subscribe(OnEmergenceTimeUp);
        }

        private void OnEmergenceTimeUp(EmergenceTimeUpEventArg emergenceTimeUpEventArg)
        {
            ViewModel.LCDMModel.SendInterface.SendEmergenceTimeUp(emergenceTimeUpEventArg.EmergenceTimerState);
        }

        private void OnRootViewLoaded()
        {
            UpdateRootViewByPower(new PowerStateChangedEventArg(ViewModel.LCDMModel.PowerState));
        }

        private void LCDMModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == PropertySupport.ExtractPropertyName<LCDMModel, PowerState>(f => f.PowerState))
            {
                EventAggregator.GetEvent<CompositePresentationEvent<PowerStateChangedEventArg>>().Publish(new PowerStateChangedEventArg(ViewModel.LCDMModel.PowerState));
            }
        }

        private void AirBrakeOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == PropertySupport.ExtractPropertyName<AirBrake, AirBrakePressure>(f => f.Pressure))
            {
                SendInterface.SendAirBrakePressure(ViewModel.LCDMModel.AirBrake.Pressure);
            }
        }

        private void UpdateRootViewByPower(PowerStateChangedEventArg arg)
        {
            m_RegionManager.RequestNavigate(RegionNames.RootRegion,
                arg.PowerState == PowerState.Started ? RootRegionViews.MainView : RootRegionViews.ShutDownView);
        }


        private void OnChangeResource(ChangeResourceEventArgs changeResourceEventArgs)
        {
            HXD2ResourceManager.ChangeResources(changeResourceEventArgs.ChangToResourceType);

            ViewModel.LCDMModel.Other.ResourceType = changeResourceEventArgs.ChangToResourceType;

            ViewModel.LCDMModel.RaiseResourceChanged();

            ViewModel.Model.RaiseResourceChanged();
        }

        private void OnSetAirBrake(SetAirBrakeEventArg setAirBrakeEventArg)
        {
            switch (setAirBrakeEventArg.SetAirBrakeType)
            {
                case SetAirBrakeType.Begin:
                    m_RegionManager.RequestNavigate(RegionNames.MainFooterRegion, FooterRegionViews.MainFooterView);
                    FooterModel.Title = ResourceKeys.AirBrakeSetting;
                    FooterModel.SettingBuffer = new object[]
                    {
                        ViewModel.LCDMModel.AirBrake.Pressure, ViewModel.LCDMModel.EngineControlModel,
                        ViewModel.LCDMModel.AirBrake.UseState,
                        ViewModel.LCDMModel.AirBrake.Location,
                        ViewModel.LCDMModel.AirBrake.MakeupAirState,
                    };
                    FooterModel.CreateSettingByBuffer();
                    break;
                case SetAirBrakeType.KPa:
                    if (FooterModel.NewSettings == null)
                    {
                        FooterModel.NewSettings = new ObservableCollection<string>(FooterModel.CurrentSettings.ToArray());
                    }

                    FooterModel.ChangeNewSettingByBuffer(0);
                    break;
                case SetAirBrakeType.CutInOff:
                    if (FooterModel.NewSettings == null)
                    {
                        FooterModel.NewSettings = new ObservableCollection<string>(FooterModel.CurrentSettings.ToArray());
                    }

                    FooterModel.ChangeNewSettingByBuffer(2);
                    break;
                case SetAirBrakeType.TrainType:
                    if (FooterModel.NewSettings == null)
                    {
                        FooterModel.NewSettings = new ObservableCollection<string>(FooterModel.CurrentSettings.ToArray());
                    }

                    FooterModel.ChangeNewSettingByBuffer(3);
                    break;
                case SetAirBrakeType.MakeupAir:
                    if (FooterModel.NewSettings == null)
                    {
                        FooterModel.NewSettings = new ObservableCollection<string>(FooterModel.CurrentSettings.ToArray());
                    }

                    FooterModel.ChangeNewSettingByBuffer(4);
                    break;
                case SetAirBrakeType.Ok:
                    var air = ViewModel.LCDMModel.AirBrake;

                    air.Pressure = (AirBrakePressure)FooterModel.SettingBuffer[0];
                    air.UseState = (UseState)FooterModel.SettingBuffer[2];
                    ViewModel.LCDMModel.EngineControlModel = (EngineControlModel)FooterModel.SettingBuffer[2];
                    air.Location = (AirBrakeLocation)FooterModel.SettingBuffer[3];
                    air.MakeupAirState = (MakeupAirState)FooterModel.SettingBuffer[4];
                    FooterModel.CurrentSettings = FooterModel.NewSettings;
                    FooterModel.NewSettings = null;
                    SendInterface.SendAirBrakePressure(air.Pressure);
                    SendInterface.SendAirBrakeUseState(air.UseState);
                    SendInterface.SendAirBrakeLocation(air.Location);
                    SendInterface.SendAirBrakeMakeupAirState(air.MakeupAirState);
                    break;
                case SetAirBrakeType.Cancel:
                    FooterModel.NewSettings = null;
                    FooterModel.SettingBuffer = new object[]
                    {
                        ViewModel.LCDMModel.AirBrake.Pressure, ViewModel.LCDMModel.EngineControlModel,
                        ViewModel.LCDMModel.AirBrake.UseState,
                        ViewModel.LCDMModel.AirBrake.Location,
                        ViewModel.LCDMModel.AirBrake.MakeupAirState,
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnSetting(SetEventArg setEventArg)
        {
            switch (setEventArg.SetType)
            {
                case SetType.Begin:
                    m_RegionManager.RequestNavigate(RegionNames.MainFooterRegion, FooterRegionViews.MainFooterView);
                    FooterModel.Title = ResourceKeys.Setting;
                    FooterModel.SettingBuffer = new object[] { null, ViewModel.LCDMModel.ReserveCommon, ViewModel.LCDMModel.VehicleCount };
                    FooterModel.CreateSettingByBuffer();
                    break;
                case SetType.ReserveCommon:
                    if (FooterModel.NewSettings == null)
                    {
                        FooterModel.NewSettings = new ObservableCollection<string>(FooterModel.CurrentSettings.ToArray());
                    }

                    FooterModel.ChangeNewSettingByBuffer(1);

                    break;
                case SetType.SigleMutil:
                    if (FooterModel.NewSettings == null)
                    {
                        FooterModel.NewSettings = new ObservableCollection<string>(FooterModel.CurrentSettings.ToArray());
                    }
                    FooterModel.ChangeNewSettingByBuffer(2);

                    break;
                case SetType.Ok:
                    ViewModel.LCDMModel.ReserveCommon = (ReserveCommon)FooterModel.SettingBuffer[1];
                    ViewModel.LCDMModel.VehicleCount = (VehicleCount)FooterModel.SettingBuffer[2];
                    FooterModel.CurrentSettings = FooterModel.NewSettings;
                    FooterModel.NewSettings = null;
                    SendInterface.SendReserveCommon(ViewModel.LCDMModel.ReserveCommon);
                    SendInterface.SendVehicleCount(ViewModel.LCDMModel.VehicleCount);
                    break;
                case SetType.Cancel:
                    FooterModel.NewSettings = null;
                    FooterModel.SettingBuffer = new object[] { null, ViewModel.LCDMModel.ReserveCommon, ViewModel.LCDMModel.VehicleCount };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnSetDataTime(SetDataTimeEventArg setDataTimeEventArg)
        {
            switch (setDataTimeEventArg.SetDataTimeType)
            {
                case SetDataTimeType.Begin:
                    m_RegionManager.RequestNavigate(RegionNames.MainFooterRegion, FooterRegionViews.MainFooterView);
                    FooterModel.Title = ResourceKeys.SetDateTime;
                    var dt = ViewModel.LCDMModel.Other.ShowingDateTime;
                    FooterModel.CurrentSettings =
                        new ObservableCollection<string>(new string[] { dt.ToString("HH:mm"), dt.ToString("yyyy"), dt.ToString("MM"), dt.ToString("dd"), null });
                    FooterModel.NewSettings = null;
                    break;
                case SetDataTimeType.ChangeTime:
                    break;
                case SetDataTimeType.ChangeYear:
                    break;
                case SetDataTimeType.ChangeMounth:
                    break;
                case SetDataTimeType.ChangeDay:
                    break;
                case SetDataTimeType.GetFromNet:
                    break;
                case SetDataTimeType.Ok:
                    break;
                case SetDataTimeType.Cancel:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnRequestChangeMainFooterRegionView(
            RequestChangeMainFooterRegionViewEventArge requestChangeMainFooterRegionViewEventArge)
        {
            switch (requestChangeMainFooterRegionViewEventArge.ToViewType)
            {
                case MainFooterRegionViewType.DateTimeInfo:
                    m_RegionManager.RequestNavigate(RegionNames.MainFooterRegion,
                        FooterRegionViews.DateTimeInfoFooterView);
                    break;
                case MainFooterRegionViewType.Setting:
                    m_RegionManager.RequestNavigate(RegionNames.MainFooterRegion, FooterRegionViews.MainFooterView);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnSetTrainId(SetTrainIdEventArg setTrainIdEventArg)
        {
            switch (setTrainIdEventArg.SetTrainIdType)
            {
                case SetTrainIdType.Begin:
                    m_RegionManager.RequestNavigate(RegionNames.MainFooterRegion, FooterRegionViews.MainFooterView);
                    FooterModel.Title = ResourceKeys.TrainIdSetting;
                    FooterModel.CurrentSettings = new ObservableCollection<string>(ViewModel.LCDMModel.TrainId.ToArray());
                    FooterModel.NewSettings = null;
                    break;
                case SetTrainIdType.SetData1:
                case SetTrainIdType.SetData2:
                case SetTrainIdType.SetData3:
                case SetTrainIdType.SetData4:
                    if (FooterModel.NewSettings == null)
                    {
                        FooterModel.NewSettings = new ObservableCollection<string>(FooterModel.CurrentSettings.ToArray());
                    }

                    var idx = setTrainIdEventArg.SetTrainIdType - SetTrainIdType.SetData1;

                    FooterModel.NewSettings[idx] = TrainIdBitAdd(FooterModel.NewSettings[idx][0], '0', 10);
                    break;
                case SetTrainIdType.SetData5:
                    if (FooterModel.NewSettings == null)
                    {
                        FooterModel.NewSettings = new ObservableCollection<string>(FooterModel.CurrentSettings.ToArray());
                    }
                    FooterModel.NewSettings[4] = TrainIdBitAdd(FooterModel.NewSettings[4][0], 'A', 26);
                    break;
                case SetTrainIdType.Ok:
                    FooterModel.CurrentSettings = FooterModel.NewSettings;
                    ViewModel.LCDMModel.TrainId = FooterModel.NewSettings.ToArray();
                    ViewModel.LCDMModel.SendInterface.SendTrainId(ViewModel.LCDMModel.TrainId);
                    FooterModel.NewSettings = null;
                    break;
                case SetTrainIdType.Cancel:
                    FooterModel.NewSettings = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string TrainIdBitAdd(char c, char first, int count)
        {
            return ((char)((c + 1 - first) % count + first)).ToString();
        }

        private void OnMainViewLoaded()
        {
            InitalizeMainView();
        }

        private void OnRequestChangeMainRegion(RequestChangeMainRegionViewEventArg requestChangeMainRegionViewEventArg)
        {
            string target;
            switch (requestChangeMainRegionViewEventArg.ToViewType)
            {
                case MainRegionViewType.MainView:
                    target = MainRegionViews.MainView;
                    break;
                case MainRegionViewType.SoftVersion:
                    target = MainRegionViews.SoftVersionView;
                    break;
                case MainRegionViewType.Maintenance:
                    target = MainRegionViews.MaintenanceView;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            m_RegionManager.RequestNavigate(RegionNames.MainRegion, target);
        }

        private void InitalizeMainView()
        {
            ViewModel.Model.StateInterface = StateInterfaceFactory.GetOrCreate(StateInterfaceKey.Root);

            m_RegionManager.RequestNavigate(RegionNames.MainFooterRegion, FooterRegionViews.DateTimeInfoFooterView);
        }

        private void OnStateChanged(StateChangedEventArg obj)
        {
            ViewModel.Model.StateInterface = StateInterfaceFactory.GetOrCreate(obj.TargetStateKey);
        }

        private void OnBtnEvent(BtnItem btn, MouseState state)
        {
            if (btn != null && btn.ActionResponser != null)
            {
                switch (state)
                {
                    case MouseState.MouseDown:
                        btn.ActionResponser.ResponseMouseDown();
                        break;
                    case MouseState.MouseUp:
                        btn.ActionResponser.ResponseMouseUp();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("btn");
                }
            }
        }

        private void OnBtnEvent(BtnEventArg btnEventArg)
        {
            if (ViewModel.LCDMModel.PowerState != PowerState.Shutdown && ViewModel.Model.StateInterface != null)
            {
                switch (btnEventArg.HardwareBtn)
                {
                    case HXD2HardwareBtn.LightAdd:
                        ViewModel.LCDMModel.Other.Opacity = Math.Max(ViewModel.LCDMModel.Other.Opacity - LightAdjust.AdjustStep, LightAdjust.MinLight);
                        break;
                    case HXD2HardwareBtn.LightDelete:
                        ViewModel.LCDMModel.Other.Opacity = Math.Min(ViewModel.LCDMModel.Other.Opacity + LightAdjust.AdjustStep, LightAdjust.MaxLight);
                        break;
                    case HXD2HardwareBtn.LightAuto:
                        ViewModel.LCDMModel.Other.Opacity = LightAdjust.AutoLight;
                        break;
                    case HXD2HardwareBtn.F1:
                        OnBtnEvent(ViewModel.Model.StateInterface.BtnF1, btnEventArg.MouseState);
                        break;
                    case HXD2HardwareBtn.F2:
                        OnBtnEvent(ViewModel.Model.StateInterface.BtnF2, btnEventArg.MouseState);
                        break;
                    case HXD2HardwareBtn.F3:
                        OnBtnEvent(ViewModel.Model.StateInterface.BtnF3, btnEventArg.MouseState);
                        break;
                    case HXD2HardwareBtn.F4:
                        OnBtnEvent(ViewModel.Model.StateInterface.BtnF4, btnEventArg.MouseState);
                        break;
                    case HXD2HardwareBtn.F5:
                        OnBtnEvent(ViewModel.Model.StateInterface.BtnF5, btnEventArg.MouseState);
                        break;
                    case HXD2HardwareBtn.F6:
                        OnBtnEvent(ViewModel.Model.StateInterface.BtnF6, btnEventArg.MouseState);
                        break;
                    case HXD2HardwareBtn.F7:
                        OnBtnEvent(ViewModel.Model.StateInterface.BtnF7, btnEventArg.MouseState);
                        break;
                    case HXD2HardwareBtn.F8:
                        OnBtnEvent(ViewModel.Model.StateInterface.BtnF8, btnEventArg.MouseState);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
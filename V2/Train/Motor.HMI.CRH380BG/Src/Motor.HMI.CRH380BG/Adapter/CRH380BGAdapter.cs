using System;
using System.ComponentModel.Composition;
using System.Linq;
using DevExpress.Xpf.Editors.Helpers;
using Motor.HMI.CRH380BG.Event;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;
using Motor.HMI.CRH380BG.Extension;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Event;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;
using MMI.Facility.WPFInfrastructure.Event;
using Motor.HMI.CRH380BG.Controller.BtnActionResponser;
using Motor.HMI.CRH380BG.Model.BtnStragy;
using Motor.HMI.CRH380BG.Model.Interface;
using Motor.HMI.CRH380BG.Resources.Keys;
using Motor.HMI.CRH380BG.Model.Domain.Brake;
using Motor.HMI.CRH380BG.Model.Domain.Constant;
using System.Timers;
using System.Windows.Threading;
using DevExpress.Data.Mask;

namespace Motor.HMI.CRH380BG.Adapter
{
    [Export]
    public class CRH380BGAdapter : NotificationObject, IDataListener
    {
        public IEventAggregator EventAggregator { private set; get; }

        public ICommunicationDataService DataService { private set; get; }

        public CRH380BGViewModel ViewModel { private set; get; }

        public ViewModelManager ViewModelManager { private set; get; }

        public StateViewModel StateViewModel { private set; get; }


        [ImportMany]
#pragma warning disable 649
        private IResetSupport[] m_Reseters;
#pragma warning restore 649

        [ImportMany]
#pragma warning disable 649
        private IUpdateDataProvider[] m_UpdateDataProviders;
#pragma warning restore 649

        [ImportingConstructor]
        public CRH380BGAdapter(IEventAggregator eventAggregator, CRH380BGViewModel viewModel,ViewModelManager manager)
        {
            EventAggregator = eventAggregator;
            ViewModel = viewModel;
            ViewModelManager = manager;
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
            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub自动速度控制_速度设定关闭, true));
            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub牵引手柄检测通道1, true));
            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub牵引手柄检测通道2, false));

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动屏黑屏, b =>
            {
                if (b)
                {
                    ViewModelManager.ReserveViewModel.StateViewModel.Model.IsVisble = true;
                    ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_开始);
                    CreateTimer2();
                }
                else
                {
                    ViewModelManager.ReserveViewModel.StateViewModel.Model.IsVisble = false;
                }
            });

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb非制动屏黑屏, b =>
            {
                if (b)
                {
                    ViewModelManager.MainViewModel.StateViewModel.Model.IsVisble = true;
                    ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_开始);
                    CreateTimer1();
                }
                else
                {
                    ViewModelManager.MainViewModel.StateViewModel.Model.IsVisble = false;
                }
            });

            foreach (var it in m_UpdateDataProviders)
            {
                it.UpdateDatas(sender, args);
            }

            UpdateMaintainModel(args);
            UpdateBrakeModel(args);
            UpdateBrakeEfficentModel(args);
            UpdateParkingBrakeModel(args);
            UpdateMainDataModel(args);
            UpdataSystemModel(args);
            UpdateDoorModel(args);
            UpdateSwitchModel(args);
            UpdateTitleModel(args);
            UpdataEmergencyModel(args);
            UpdateFault(args);
        }

        public void CreateTimer1()
        {
            var time = new DispatcherTimer();
            time.Interval = new TimeSpan(0, 0, 5);
            time.Tick += (sender, args) =>
            {
                time.Stop();
                ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root);
            };
            time.Start();
        }

        public void CreateTimer2()
        {
            var time = new DispatcherTimer();
            time.Interval = new TimeSpan(0, 0, 5);
            time.Tick += (sender, args) =>
            {
                time.Stop();
                ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_制动状态);
            };
            time.Start();
        }

        private void UpdateFault(CommunicationDataChangedArgs args)
        {
            var c = ViewModel.Domain.FaultViewModel.Controller;
            if (args.RaiseDataChangedType != RaiseCommunicationDataChangedType.ByUserManul)
            {
                foreach (var infoConfig in GlobalParam.Instance.NotifyInfoConfigs.Value)
                {
                    args.ChangedBools.UpdateIfContains(infoConfig.Index, b =>
                    {
                        if (b)
                        {
                            c.AddItem(infoConfig);
                        }
                        else
                        {
                            c.ResetItem(infoConfig);
                        }
                    });
                }
            }
        }

        private void UpdateMaintainModel(CommunicationDataChangedArgs args)
        {
            var br = ViewModel.Domain.Model.MaintainModel;
            //args.ChangedFloats.UpdateIfContains(inb, f => main.ContactNetVoltage = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf中间直流环节电压_01, f=> br.IntermediateDClinkProgressBar1 =f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf中间直流环节电压_03, f => br.IntermediateDClinkProgressBar3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf中间直流环节电压_06, f => br.IntermediateDClinkProgressBar6 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf中间直流环节电压_08, f => br.IntermediateDClinkProgressBar8 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf运行速度设定检测_通道1, f => br.RunSpeedSettingDetectionModel.RunSpeedSettingDetectionPipeOneProgressBar = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf运行速度设定检测_通道2, f => br.RunSpeedSettingDetectionModel.RunSpeedSettingDetectionPipeTwoProgressBar = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf使用电量_牵引单元1, f => br.UseElectricity1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf使用电量_牵引单元2, f => br.UseElectricity2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf再生电量_牵引单元1, f => br.RegenerationElectricity1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf再生电量_牵引单元2, f => br.RegenerationElectricity2 = f);

        }

        private void UpdateBrakeModel(CommunicationDataChangedArgs args)
        {
            var br = ViewModel.Domain.Model.BrakeModel;

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加1E, b => br.IsBrakeE1Apply = b ? EfficentState.Efficent : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加1E状态未知, b => br.IsBrakeE1Apply = b ? EfficentState.UnKonwn : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效1E, b => br.IsBrakeE1Efficent = b ? Efficence.Efficent : Efficence.NotEfficent);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加3E, b => br.IsBrakeE3Apply =b ? EfficentState.Efficent : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加3E状态未知, b=> br.IsBrakeE3Apply =b ? EfficentState.UnKonwn : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效3E, b => br.IsBrakeE3Efficent = b ? Efficence.Efficent : Efficence.NotEfficent);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加6E, b => br.IsBrakeE6Apply = b ? EfficentState.Efficent : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加6E状态未知, b => br.IsBrakeE6Apply = b ? EfficentState.UnKonwn : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效6E, b => br.IsBrakeE6Efficent = b ? Efficence.Efficent : Efficence.NotEfficent);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加8E, b => br.IsBrakeE8Apply = b ? EfficentState.Efficent : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加8E状态未知, b => br.IsBrakeE8Apply = b ? EfficentState.UnKonwn : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效8E, b => br.IsBrakeE8Efficent = b ? Efficence.Efficent : Efficence.NotEfficent);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加1D, b => br.IsBrakeD1Apply = b ? EfficentState.Efficent : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加1D状态未知, b => br.IsBrakeD1Apply = b ? EfficentState.UnKonwn : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效1D, b => br.IsBrakeD1Efficent = b ? Efficence.Efficent : Efficence.NotEfficent);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加2D, b => br.IsBrakeD2Apply = b ? EfficentState.Efficent : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加2D状态未知, b => br.IsBrakeD2Apply = b ? EfficentState.UnKonwn : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效2D, b => br.IsBrakeD2Efficent = b ? Efficence.Efficent : Efficence.NotEfficent);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加3D, b => br.IsBrakeD3Apply = b ? EfficentState.Efficent : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加3D状态未知, b => br.IsBrakeD3Apply = b ? EfficentState.UnKonwn : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效3D, b => br.IsBrakeD3Efficent = b ? Efficence.Efficent : Efficence.NotEfficent);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加4D, b => br.IsBrakeD4Apply = b ? EfficentState.Efficent : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加4D状态未知, b => br.IsBrakeD4Apply = b ? EfficentState.UnKonwn : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效4D, b => br.IsBrakeD4Efficent = b ? Efficence.Efficent : Efficence.NotEfficent);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加5D, b => br.IsBrakeD5Apply = b ? EfficentState.Efficent : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加5D状态未知, b => br.IsBrakeD5Apply = b ? EfficentState.UnKonwn : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效5D, b => br.IsBrakeD5Efficent = b ? Efficence.Efficent : Efficence.NotEfficent);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加6D, b => br.IsBrakeD6Apply = b ? EfficentState.Efficent : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加6D状态未知, b => br.IsBrakeD6Apply = b ? EfficentState.UnKonwn : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效6D, b => br.IsBrakeD6Efficent = b ? Efficence.Efficent : Efficence.NotEfficent);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加7D, b => br.IsBrakeD7Apply = b ? EfficentState.Efficent : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加7D状态未知, b => br.IsBrakeD7Apply = b ? EfficentState.UnKonwn : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效7D, b => br.IsBrakeD7Efficent = b ? Efficence.Efficent : Efficence.NotEfficent);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加8D, b => br.IsBrakeD8Apply = b ? EfficentState.Efficent : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动施加8D状态未知, b => br.IsBrakeD8Apply = b ? EfficentState.UnKonwn : EfficentState.Not);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效8D, b => br.IsBrakeD8Efficent = b ? Efficence.Efficent : Efficence.NotEfficent);

            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf列车管_总, f => br.TrainPipePressure = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf总风管_总, f=> br.TotalWindPipePressure = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf总风管_1, f=> br.TotalWindPipe1Pressure = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf总风管_8, f=>br.TotalWindPipe8Pressure = f);
        }

        private void UpdateBrakeEfficentModel(CommunicationDataChangedArgs args)
        {
            var br = ViewModel.Domain.Model.BrakeModel.BrakeEfficentModel;

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效1D,b => br.IsAirBrake2Usefull = b ? Efficence.Efficent : Efficence.NotEfficent);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效关闭1D, b => br.IsAirBrake1Closed = b ? EfficenceClose.Close : EfficenceClose.Open);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效2D, b => br.IsAirBrake2Usefull = b ? Efficence.Efficent : Efficence.NotEfficent);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效关闭2D, b => br.IsAirBrake2Closed = b ? EfficenceClose.Close : EfficenceClose.Open);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效3D, b => br.IsAirBrake3Usefull = b ? Efficence.Efficent : Efficence.NotEfficent);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效关闭3D, b => br.IsAirBrake3Closed = b ? EfficenceClose.Close : EfficenceClose.Open);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效4D, b => br.IsAirBrake4Usefull = b ? Efficence.Efficent : Efficence.NotEfficent);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效关闭4D, b => br.IsAirBrake4Closed = b ? EfficenceClose.Close : EfficenceClose.Open);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效5D, b => br.IsAirBrake5Usefull = b ? Efficence.Efficent : Efficence.NotEfficent);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效关闭5D, b => br.IsAirBrake5Closed = b ? EfficenceClose.Close : EfficenceClose.Open);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效6D, b => br.IsAirBrake6Usefull = b ? Efficence.Efficent : Efficence.NotEfficent);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效关闭6D, b => br.IsAirBrake6Closed = b ? EfficenceClose.Close : EfficenceClose.Open);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效7D, b => br.IsAirBrake7Usefull = b ? Efficence.Efficent : Efficence.NotEfficent);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效关闭7D, b => br.IsAirBrake7Closed = b ? EfficenceClose.Close : EfficenceClose.Open);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效8D, b => br.IsAirBrake8Usefull = b ? Efficence.Efficent : Efficence.NotEfficent);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb制动有效率有效关闭8D, b => br.IsAirBrake8Closed = b ? EfficenceClose.Close : EfficenceClose.Open);

            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf牵引制动百分比1, b=> br.AirBrakeEfficencePercent = b);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf牵引制动百分比1, b => br.AirBrakeTotalNumber = b);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf牵引制动百分比3, b => br.AirBrakeTotalNumber = b);

            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf车长, b => br.AirBrakeTrainWidth = b);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf轴数, b => br.AirBrakeAxleNumber = b);

            //时间和日期没有填写
        }

        private void UpdateParkingBrakeModel(CommunicationDataChangedArgs args)
        {
            var br = ViewModel.Domain.Model.BrakeModel.ParkingBrakeModel;

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动2_状态未知, b => br.IsParkingBrake2Unkown = b ? ParkingStateUnkown.UnKown : ParkingStateUnkown.Konwn);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动有效2, b => br.IsParkingBrake2Efficent = b ? ParkingStateEfficence.Efficent : ParkingStateEfficence.Defualut);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动失效2, b => br.IsParkingBrake2Efficent = b ? ParkingStateEfficence.UnEfficent : ParkingStateEfficence.Defualut);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动施加2, b => br.IsParkingBrake2Apply = b ? ParkingState.Efficent : ParkingState.UnEfficent);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动4_状态未知, b => br.IsParkingBrake4Unkown = b ? ParkingStateUnkown.UnKown : ParkingStateUnkown.Konwn);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动有效4, b => br.IsParkingBrake4Efficent = b ? ParkingStateEfficence.Efficent : ParkingStateEfficence.Defualut);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动失效4, b => br.IsParkingBrake4Efficent = b ? ParkingStateEfficence.UnEfficent : ParkingStateEfficence.Defualut);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动施加4, b => br.IsParkingBrake4Apply = b ? ParkingState.Efficent : ParkingState.UnEfficent);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动5_状态未知, b => br.IsParkingBrake5Unkown = b ? ParkingStateUnkown.UnKown : ParkingStateUnkown.Konwn);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动有效5, b => br.IsParkingBrake5Efficent = b ? ParkingStateEfficence.Efficent : ParkingStateEfficence.Defualut);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动失效5, b => br.IsParkingBrake5Efficent = b ? ParkingStateEfficence.UnEfficent : ParkingStateEfficence.Defualut);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动施加5, b => br.IsParkingBrake5Apply = b ? ParkingState.Efficent : ParkingState.UnEfficent);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动7_状态未知, b => br.IsParkingBrake7Unkown = b ? ParkingStateUnkown.UnKown : ParkingStateUnkown.Konwn);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动有效7, b => br.IsParkingBrake7Efficent = b ? ParkingStateEfficence.Efficent : ParkingStateEfficence.Defualut);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动失效7, b => br.IsParkingBrake7Efficent = b ? ParkingStateEfficence.UnEfficent : ParkingStateEfficence.Defualut);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb停放制动施加7, b => br.IsParkingBrake7Apply = b ? ParkingState.Efficent : ParkingState.UnEfficent);

        }
        private void UpdateMainDataModel(CommunicationDataChangedArgs args)
        {
            var main = ViewModel.Domain.Model.MainData;


            if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb蓄电池状态) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb司机室00占用) == false)
            {
                main.WelcomVisible = true;
                main.TimeVisible = false;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb蓄电池状态) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb司机室00占用))
            {
                main.WelcomVisible = false;
                main.TimeVisible = true;
                ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_确定列车配置);
                ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_确定列车配置);
                main.TrainConfigVisible = true;
            }
            else
            {
                main.WelcomVisible = false;
                main.TimeVisible = true;
            }
            
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb蓄电池状态, b =>
            {
                if (b)
                {
                    main.WelcomVisible = true;
                    main.TimeVisible = false;
                }
                else
                {
                    main.WelcomVisible = false;
                    main.TimeVisible = true;
                }
            });





            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_受电弓_1_升弓, b => main.PantographState1 = b ? PantographState.Rise : PantographState.Unkown);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_受电弓_1_降弓, b => main.PantographState1 = b ? PantographState.Down : PantographState.Unkown);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_受电弓_1_切除, b => main.PantographState1 = b ? PantographState.Removal : PantographState.Unkown);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_受电弓_2_升弓, b => main.PantographState2 = b ? PantographState.Rise : PantographState.Unkown);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_受电弓_2_降弓, b => main.PantographState2 = b ? PantographState.Down : PantographState.Unkown);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_受电弓_2_切除, b => main.PantographState2 = b ? PantographState.Removal : PantographState.Unkown);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_主断_1_闭合, b => main.MainBreakState1 = b ? MainBreakState.Closure : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_主断_1_断开, b => main.MainBreakState1 = b ? MainBreakState.Disconnect : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_主断_1_切除, b => main.MainBreakState1 = b ? MainBreakState.Removal : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_主断_2_闭合, b => main.MainBreakState2 = b ? MainBreakState.Closure : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_主断_2_断开, b => main.MainBreakState2 = b ? MainBreakState.Disconnect : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_主断_2_切除, b => main.MainBreakState2 = b ? MainBreakState.Removal : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb接触网电压充电机1_黄, b => main.VoltageChargerState1 = b ? VoltageChargerState.Yellow : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb接触网电压充电机1_蓝, b => main.VoltageChargerState1 = b ? VoltageChargerState.Blue : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb接触网电压充电机1_状态未知, b => main.VoltageChargerState1 = b ? VoltageChargerState.Unknow : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb接触网电压充电机2_黄, b => main.VoltageChargerState2 = b ? VoltageChargerState.Yellow : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb接触网电压充电机2_蓝, b => main.VoltageChargerState2 = b ? VoltageChargerState.Blue : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb接触网电压充电机2_状态未知, b => main.VoltageChargerState2 = b ? VoltageChargerState.Unknow : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb充电机1_黄, b => main.ChargerState1 = b ? ChargerState.Yellow : ChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb充电机1_蓝, b => main.ChargerState1 = b ? ChargerState.Blue : ChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb充电机1_状态未知, b => main.ChargerState1 = b ? ChargerState.Unknow : ChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb充电机2_黄, b => main.ChargerState2 = b ? ChargerState.Yellow : ChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb充电机2_蓝, b => main.ChargerState2 = b ? ChargerState.Blue : ChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb充电机2_状态未知, b => main.ChargerState2 = b ? ChargerState.Unknow : ChargerState.Black);

            args.ChangedBools.UpdateIfContains(InBoolKeys.InbEVC可以切换标志, b => main.EVCIsDisplay =b);

            if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb工况牵引)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb工况制动))
            {
                main.TractionBrakeType = TractionBrakeType.Traction;
                main.RedTriangleVisible = false;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb工况牵引)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb工况制动)== false) 
            {
                main.TractionBrakeType = TractionBrakeType.Traction;
                main.RedTriangleVisible = false;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb工况牵引) == false
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb工况制动)) 
            {
                main.TractionBrakeType = TractionBrakeType.Brake;
                main.RedTriangleVisible = false;
            }
            else
            {
                main.TractionBrakeType = TractionBrakeType.Unknow;
                main.RedTriangleVisible = true;
            }
            
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1接触网电压, f => main.ContactNetVoltage = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf网侧电流, f => main.NetworkSideCurrent = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf蓄电池电压_04, f => main.BatteryVoltage1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf蓄电池电压_05, f => main.BatteryVoltage2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfEVC显示列车速度, f => main.EVCSpeed = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf牵引制动百分比1, f => main.TrctionBrakeActualPercent1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf牵引制动百分比2, f => main.TrctionBrakeActualPercent2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf牵引制动百分比3, f => main.TrctionBrakeActualPercent3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf牵引制动百分比4, f => main.TrctionBrakeActualPercent4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf牵引制动百分比1, f => main.TrctionBrakeSetPercent1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf牵引制动百分比2, f => main.TrctionBrakeSetPercent2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf牵引制动百分比3, f => main.TrctionBrakeSetPercent3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf牵引制动百分比4, f => main.TrctionBrakeSetPercent4 = f);
        }

        private void UpdataSystemModel(CommunicationDataChangedArgs args)
        {
            var ss = ViewModel.Domain.Model.SystemModel;
            if (ViewModelManager.MainViewModel.StateViewModel.Model.CurrentStateInterface != null)
            {
                var mainid = ViewModelManager.MainViewModel.StateViewModel.Model.CurrentStateInterface.Id;

                if (mainid == StateInterfaceKey.Parser(StateKeys.Root_系统_解编_使列车就绪))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb解编时_列车已就绪_可以解编, b =>
                    {
                        if (b)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_解编_使列车就绪解编);
                        }
                    });
                }

                if (mainid == StateInterfaceKey.Parser(StateKeys.Root_系统_解编_正在解编))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb解编成功, b =>
                    {
                        if (b)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_解编_解编成功);
                            ViewModelManager.MainViewModel.StateViewModel.Model.CompiledVisible3 = false;
                        }
                    });
                }

                if (mainid == StateInterfaceKey.Parser(StateKeys.Root_系统_编组联挂)
                    || mainid == StateInterfaceKey.Parser(StateKeys.Root_系统_编组联挂1)
                    || mainid == StateInterfaceKey.Parser(StateKeys.Root_系统_编组联挂2)
                    || mainid == StateInterfaceKey.Parser(StateKeys.Root_系统_编组联挂3)
                    || mainid == StateInterfaceKey.Parser(StateKeys.Root_系统_编组联挂4))
                {
                    if (ss.GroupHangsModel.SpeedVisible == true)
                    {
                        ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_编组联挂4);
                    }
                    else
                    {
                        if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Open
                        || ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Open)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_编组联挂1);
                        }
                        else if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Close && ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Close)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_编组联挂);
                        }
                        else if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Close
                                && (ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Fault
                                || ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Hanged
                                || ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.ReadyHang
                                || ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Running))
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_编组联挂2);
                        }
                        else if ((ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Fault
                                || ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Hanged
                                || ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.ReadyHang
                                || ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Running)
                                && ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Close)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_编组联挂3);
                        }
                        else
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_编组联挂4);
                        }
                    }
                }

                if (mainid == StateInterfaceKey.Parser(StateKeys.Root_系统_关闭车钩罩)
                    || mainid == StateInterfaceKey.Parser(StateKeys.Root_系统_关闭车钩罩1)
                    || mainid == StateInterfaceKey.Parser(StateKeys.Root_系统_关闭车钩罩2)
                    || mainid == StateInterfaceKey.Parser(StateKeys.Root_系统_关闭车钩罩3)
                    || mainid == StateInterfaceKey.Parser(StateKeys.Root_系统_关闭车钩罩4)
                    || mainid == StateInterfaceKey.Parser(StateKeys.Root_系统_关闭车钩罩5)
                    || mainid == StateInterfaceKey.Parser(StateKeys.Root_系统_关闭车钩罩6))
                {
                    if (ss.GroupHangsModel.EMU2CloseCarCoverVisible == true)
                    {
                        if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Open
                            && ss.GroupHangsModel.EMU2AfterHangType == GroupHangsType.Open)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩6);
                        }
                        else if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Open && ss.GroupHangsModel.EMU2AfterHangType != GroupHangsType.Open)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩4);
                        }
                        else if (ss.GroupHangsModel.EMU1FrontHangType != GroupHangsType.Open && ss.GroupHangsModel.EMU2AfterHangType == GroupHangsType.Open)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩5);
                        }
                        else
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩);
                        }
                    }
                    else
                    {
                        if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Open
                            && ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Open)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩3);
                        }
                        else if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Open && ss.GroupHangsModel.EMU1AfterHangType != GroupHangsType.Open)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩1);
                        }
                        else if (ss.GroupHangsModel.EMU1FrontHangType != GroupHangsType.Open && ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Open)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩2);
                        }
                        else
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩);
                        }
                    }
                }
            }


            if (ViewModelManager.ReserveViewModel.StateViewModel.Model.CurrentStateInterface != null)
            {
                var reserveId = ViewModelManager.ReserveViewModel.StateViewModel.Model.CurrentStateInterface.Id;
                
                if (reserveId == StateInterfaceKey.Parser(StateKeys.Root_系统_解编_使列车就绪))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb解编时_列车已就绪_可以解编, b =>
                    {
                        if (b)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_解编_使列车就绪解编);
                        }
                    });
                }

                if (reserveId == StateInterfaceKey.Parser(StateKeys.Root_系统_解编_正在解编))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb解编成功, b =>
                    {
                        if (b)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_解编_解编成功);
                            ViewModelManager.ReserveViewModel.StateViewModel.Model.CompiledVisible3 = false;
                        }
                    });
                }

                args.ChangedBools.UpdateIfContains(InBoolKeys.Inb解编时_列车已就绪_可以解编, b =>
                {
                    if (b)
                    {
                        ViewModel.Domain.Model.SystemModel.CompiledVisible2 = false;
                    }
                    else
                    {
                        ViewModel.Domain.Model.SystemModel.CompiledVisible2 = true;
                    }
                });

                if (reserveId == StateInterfaceKey.Parser(StateKeys.Root_系统_编组联挂)
                    || reserveId == StateInterfaceKey.Parser(StateKeys.Root_系统_编组联挂1)
                    || reserveId == StateInterfaceKey.Parser(StateKeys.Root_系统_编组联挂2)
                    || reserveId == StateInterfaceKey.Parser(StateKeys.Root_系统_编组联挂3)
                    || reserveId == StateInterfaceKey.Parser(StateKeys.Root_系统_编组联挂4))
                {
                    if (ss.GroupHangsModel.SpeedVisible == true)
                    {
                        ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_编组联挂4);
                    }
                    else
                    {
                        if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Open
                        || ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Open)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_编组联挂1);
                        }
                        else if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Close && ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Close)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_编组联挂);
                        }
                        else if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Close
                                && (ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Fault
                                || ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Hanged
                                || ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.ReadyHang
                                || ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Running))
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_编组联挂2);
                        }
                        else if ((ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Fault
                                || ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Hanged
                                || ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.ReadyHang
                                || ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Running)
                                && ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Close)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_编组联挂3);
                        }
                        else
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_编组联挂4);
                        }
                    }
                }

                if (reserveId == StateInterfaceKey.Parser(StateKeys.Root_系统_关闭车钩罩)
                    || reserveId == StateInterfaceKey.Parser(StateKeys.Root_系统_关闭车钩罩1)
                    || reserveId == StateInterfaceKey.Parser(StateKeys.Root_系统_关闭车钩罩2)
                    || reserveId == StateInterfaceKey.Parser(StateKeys.Root_系统_关闭车钩罩3)
                    || reserveId == StateInterfaceKey.Parser(StateKeys.Root_系统_关闭车钩罩4)
                    || reserveId == StateInterfaceKey.Parser(StateKeys.Root_系统_关闭车钩罩5)
                    || reserveId == StateInterfaceKey.Parser(StateKeys.Root_系统_关闭车钩罩6))
                {
                    if (ss.GroupHangsModel.EMU2CloseCarCoverVisible == true)
                    {
                        if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Open
                            && ss.GroupHangsModel.EMU2AfterHangType == GroupHangsType.Open)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩6);
                        }
                        else if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Open && ss.GroupHangsModel.EMU2AfterHangType != GroupHangsType.Open)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩4);
                        }
                        else if (ss.GroupHangsModel.EMU1FrontHangType != GroupHangsType.Open && ss.GroupHangsModel.EMU2AfterHangType == GroupHangsType.Open)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩5);
                        }
                        else
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩);
                        }
                    }
                    else
                    {
                        if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Open
                            && ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Open)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩3);
                        }
                        else if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Open && ss.GroupHangsModel.EMU1AfterHangType != GroupHangsType.Open)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩1);
                        }
                        else if (ss.GroupHangsModel.EMU1FrontHangType != GroupHangsType.Open && ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Open)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩2);
                        }
                        else
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_系统_关闭车钩罩);
                        }
                    }
                }
            }
            



            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb联挂成功, b => ss.GroupHangsModel.EMU2HangVisible = b);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb自动切换到ASC速度设定为2kmh的连挂界面, b => ss.GroupHangsModel.SpeedVisible = b);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb16编接口, b => ss.GroupHangsModel.EMU2CloseCarCoverVisible = b);

            if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1前车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1前车钩状态第1位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1前车钩状态第2位))
            {
                ss.GroupHangsModel.EMU1FrontHangType = GroupHangsType.Fault;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1前车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1前车钩状态第1位) == false
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1前车钩状态第2位))
            {
                ss.GroupHangsModel.EMU1FrontHangType = GroupHangsType.ReadyHang;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1前车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1前车钩状态第1位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1前车钩状态第2位) == false)
            {
                ss.GroupHangsModel.EMU1FrontHangType = GroupHangsType.Hanged;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1前车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1前车钩状态第1位) == false
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1前车钩状态第2位) == false)
            {
                ss.GroupHangsModel.EMU1FrontHangType = GroupHangsType.Open;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1前车钩状态第0位) == false
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1前车钩状态第1位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1前车钩状态第2位)== false) 
            {
                ss.GroupHangsModel.EMU1FrontHangType = GroupHangsType.Running;
            }
            else
            {
                ss.GroupHangsModel.EMU1FrontHangType = GroupHangsType.Close;    
            }

            if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1后车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1后车钩状态第1位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1后车钩状态第2位))
            {
                ss.GroupHangsModel.EMU1AfterHangType = GroupHangsType.Fault;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1后车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1后车钩状态第1位) == false
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1后车钩状态第2位))
            {
                ss.GroupHangsModel.EMU1AfterHangType = GroupHangsType.ReadyHang;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1后车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1后车钩状态第1位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1后车钩状态第2位) == false)
            {
                ss.GroupHangsModel.EMU1AfterHangType = GroupHangsType.Hanged;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1后车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1后车钩状态第1位) == false
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1后车钩状态第2位) == false)
            {
                ss.GroupHangsModel.EMU1AfterHangType = GroupHangsType.Open;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1后车钩状态第0位) == false
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1后车钩状态第1位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组1后车钩状态第2位)== false) 
            {
                ss.GroupHangsModel.EMU1AfterHangType = GroupHangsType.Running;
            }
            else
            {
                ss.GroupHangsModel.EMU1AfterHangType = GroupHangsType.Close;    
            }

            if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2前车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2前车钩状态第1位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2前车钩状态第2位))
            {
                ss.GroupHangsModel.EMU2FrontHangType = GroupHangsType.Fault;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2前车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2前车钩状态第1位) == false
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2前车钩状态第2位))
            {
                ss.GroupHangsModel.EMU2FrontHangType = GroupHangsType.ReadyHang;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2前车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2前车钩状态第1位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2前车钩状态第2位) == false)
            {
                ss.GroupHangsModel.EMU2FrontHangType = GroupHangsType.Hanged;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2前车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2前车钩状态第1位) == false
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2前车钩状态第2位) == false)
            {
                ss.GroupHangsModel.EMU2FrontHangType = GroupHangsType.Open;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2前车钩状态第0位) == false
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2前车钩状态第1位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2前车钩状态第2位) == false) 
            {
                ss.GroupHangsModel.EMU2FrontHangType = GroupHangsType.Running;
            }
            else
            {
                ss.GroupHangsModel.EMU2FrontHangType = GroupHangsType.Close;    
            }


            if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2后车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2后车钩状态第1位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2后车钩状态第2位))
            {
                ss.GroupHangsModel.EMU2AfterHangType = GroupHangsType.Fault;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2后车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2后车钩状态第1位) == false
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2后车钩状态第2位))
            {
                ss.GroupHangsModel.EMU2AfterHangType = GroupHangsType.ReadyHang;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2后车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2后车钩状态第1位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2后车钩状态第2位) == false)
            {
                ss.GroupHangsModel.EMU2AfterHangType = GroupHangsType.Hanged;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2后车钩状态第0位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2后车钩状态第1位) == false
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2后车钩状态第2位) == false)
            {
                ss.GroupHangsModel.EMU2AfterHangType = GroupHangsType.Open;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2后车钩状态第0位) == false
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2后车钩状态第1位)
                && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb动车组2后车钩状态第2位) == false)
            {
                ss.GroupHangsModel.EMU2AfterHangType = GroupHangsType.Running;
            }
            else
            {
                ss.GroupHangsModel.EMU2AfterHangType = GroupHangsType.Close;    
            }

            if (ss.CatenaryTypeIsSelect == true)
            {
                //args.ChangedBools.UpdateIfContains(InBoolKeys.Inb接触网网高53米, b => ss.CatenaryType = b ? CatenaryType.HighNet53 : CatenaryType.AutoSelect);
                //args.ChangedBools.UpdateIfContains(InBoolKeys.Inb接触网网高64米, b => ss.CatenaryType = b ? CatenaryType.HighNet64 : CatenaryType.AutoSelect);
                if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb接触网网高53米) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb接触网网高64米) == false)
                {
                    ss.CatenaryType = CatenaryType.HighNet53;
                    ss.CurrentHighNet = 5.3;
                    EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub接触网类型手动选择53米, true));
                    EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub接触网类型手动选择64米, false));
                    EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub接触网类型自动选择, false));
                }
                else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb接触网网高53米) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb接触网网高64米))
                {
                    ss.CatenaryType = CatenaryType.HighNet64;
                    ss.CurrentHighNet = 6.4;
                    EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub接触网类型手动选择53米, false));
                    EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub接触网类型手动选择64米, true));
                    EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub接触网类型自动选择, false));
                }
                else
                {
                    ss.CatenaryType = CatenaryType.AutoSelect;
                    ss.CurrentHighNet = 5.3;
                    EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub接触网类型手动选择53米, false));
                    EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub接触网类型手动选择64米, false));
                    EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub接触网类型自动选择, true));
                }

            }
            else
            {
                ss.CatenaryType = CatenaryType.AutoSelect;
                ss.CurrentHighNet = 5.3;
                EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub接触网类型自动选择, true));
            }
            

            
            
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢11左, f => ss.WheelTemperatureModel.Car1Left1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢12左, f => ss.WheelTemperatureModel.Car1Left2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢13左, f => ss.WheelTemperatureModel.Car1Left3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢14左, f => ss.WheelTemperatureModel.Car1Left4Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢11右, f => ss.WheelTemperatureModel.Car1Right1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢12右, f => ss.WheelTemperatureModel.Car1Right2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢13右, f => ss.WheelTemperatureModel.Car1Right3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢14右, f => ss.WheelTemperatureModel.Car1Right4Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢21左, f => ss.WheelTemperatureModel.Car2Left1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢22左, f => ss.WheelTemperatureModel.Car2Left2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢23左, f => ss.WheelTemperatureModel.Car2Left3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢24左, f => ss.WheelTemperatureModel.Car2Left4Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢21右, f => ss.WheelTemperatureModel.Car2Right1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢22右, f => ss.WheelTemperatureModel.Car2Right2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢23右, f => ss.WheelTemperatureModel.Car2Right3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢24右, f => ss.WheelTemperatureModel.Car2Right4Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢31左, f => ss.WheelTemperatureModel.Car3Left1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢32左, f => ss.WheelTemperatureModel.Car3Left2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢33左, f => ss.WheelTemperatureModel.Car3Left3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢34左, f => ss.WheelTemperatureModel.Car3Left4Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢31右, f => ss.WheelTemperatureModel.Car3Right1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢32右, f => ss.WheelTemperatureModel.Car3Right2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢33右, f => ss.WheelTemperatureModel.Car3Right3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢34右, f => ss.WheelTemperatureModel.Car3Right4Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢41左, f => ss.WheelTemperatureModel.Car4Left1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢42左, f => ss.WheelTemperatureModel.Car4Left2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢43左, f => ss.WheelTemperatureModel.Car4Left3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢44左, f => ss.WheelTemperatureModel.Car4Left4Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢41右, f => ss.WheelTemperatureModel.Car4Right1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢42右, f => ss.WheelTemperatureModel.Car4Right2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢43右, f => ss.WheelTemperatureModel.Car4Right3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1前车厢44右, f => ss.WheelTemperatureModel.Car4Right4Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢11左, f => ss.WheelTemperatureModel.Car5Left1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢12左, f => ss.WheelTemperatureModel.Car5Left2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢13左, f => ss.WheelTemperatureModel.Car5Left3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢14左, f => ss.WheelTemperatureModel.Car5Left4Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢11右, f => ss.WheelTemperatureModel.Car5Right1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢12右, f => ss.WheelTemperatureModel.Car5Right2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢13右, f => ss.WheelTemperatureModel.Car5Right3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢14右, f => ss.WheelTemperatureModel.Car5Right4Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢21左, f => ss.WheelTemperatureModel.Car6Left1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢22左, f => ss.WheelTemperatureModel.Car6Left2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢23左, f => ss.WheelTemperatureModel.Car6Left3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢24左, f => ss.WheelTemperatureModel.Car6Left4Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢21右, f => ss.WheelTemperatureModel.Car6Right1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢22右, f => ss.WheelTemperatureModel.Car6Right2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢23右, f => ss.WheelTemperatureModel.Car6Right3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢24右, f => ss.WheelTemperatureModel.Car6Right4Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢31左, f => ss.WheelTemperatureModel.Car7Left1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢32左, f => ss.WheelTemperatureModel.Car7Left2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢33左, f => ss.WheelTemperatureModel.Car7Left3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢34左, f => ss.WheelTemperatureModel.Car7Left4Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢31右, f => ss.WheelTemperatureModel.Car7Right1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢32右, f => ss.WheelTemperatureModel.Car7Right2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢33右, f => ss.WheelTemperatureModel.Car7Right3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢34右, f => ss.WheelTemperatureModel.Car7Right4Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢41左, f => ss.WheelTemperatureModel.Car8Left1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢42左, f => ss.WheelTemperatureModel.Car8Left2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢43左, f => ss.WheelTemperatureModel.Car8Left3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢44左, f => ss.WheelTemperatureModel.Car8Left4Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢41右, f => ss.WheelTemperatureModel.Car8Right1Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢42右, f => ss.WheelTemperatureModel.Car8Right2Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢43右, f => ss.WheelTemperatureModel.Car8Right3Wheel = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1后车厢44右, f => ss.WheelTemperatureModel.Car8Right4Wheel = f);

            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧1_1, f => ss.GearboxMotorTemperatureModel.Car1DriveShaftEnd1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧1_2, f => ss.GearboxMotorTemperatureModel.Car1DriveShaftEnd2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧1_3, f => ss.GearboxMotorTemperatureModel.Car1DriveShaftEnd3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧1_4, f => ss.GearboxMotorTemperatureModel.Car1DriveShaftEnd4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧3_5, f => ss.GearboxMotorTemperatureModel.Car3DriveShaftEnd1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧3_6, f => ss.GearboxMotorTemperatureModel.Car3DriveShaftEnd2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧3_7, f => ss.GearboxMotorTemperatureModel.Car3DriveShaftEnd3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧3_8, f => ss.GearboxMotorTemperatureModel.Car3DriveShaftEnd4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧6_1, f => ss.GearboxMotorTemperatureModel.Car6DriveShaftEnd1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧6_2, f => ss.GearboxMotorTemperatureModel.Car6DriveShaftEnd2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧6_3, f => ss.GearboxMotorTemperatureModel.Car6DriveShaftEnd3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧6_4, f => ss.GearboxMotorTemperatureModel.Car6DriveShaftEnd4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧8_5, f => ss.GearboxMotorTemperatureModel.Car8DriveShaftEnd1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧8_6, f => ss.GearboxMotorTemperatureModel.Car8DriveShaftEnd2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧8_7, f => ss.GearboxMotorTemperatureModel.Car8DriveShaftEnd3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承驱动侧8_8, f => ss.GearboxMotorTemperatureModel.Car8DriveShaftEnd4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧1_1, f => ss.GearboxMotorTemperatureModel.Car1NoDriveShaftEnd1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧1_2, f => ss.GearboxMotorTemperatureModel.Car1NoDriveShaftEnd2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧1_3, f => ss.GearboxMotorTemperatureModel.Car1NoDriveShaftEnd3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧1_4, f => ss.GearboxMotorTemperatureModel.Car1NoDriveShaftEnd4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧3_5, f => ss.GearboxMotorTemperatureModel.Car3NoDriveShaftEnd1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧3_6, f => ss.GearboxMotorTemperatureModel.Car3NoDriveShaftEnd2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧3_7, f => ss.GearboxMotorTemperatureModel.Car3NoDriveShaftEnd3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧3_8, f => ss.GearboxMotorTemperatureModel.Car3NoDriveShaftEnd4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧6_1, f => ss.GearboxMotorTemperatureModel.Car6NoDriveShaftEnd1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧6_2, f => ss.GearboxMotorTemperatureModel.Car6NoDriveShaftEnd2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧6_3, f => ss.GearboxMotorTemperatureModel.Car6NoDriveShaftEnd3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧6_4, f => ss.GearboxMotorTemperatureModel.Car6NoDriveShaftEnd4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧8_5, f => ss.GearboxMotorTemperatureModel.Car8NoDriveShaftEnd1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧8_6, f => ss.GearboxMotorTemperatureModel.Car8NoDriveShaftEnd2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧8_7, f => ss.GearboxMotorTemperatureModel.Car8NoDriveShaftEnd3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_电机轴承非驱动侧8_8, f => ss.GearboxMotorTemperatureModel.Car8NoDriveShaftEnd4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧1_1, f => ss.GearboxMotorTemperatureModel.Car1BigGearMotor1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧1_2, f => ss.GearboxMotorTemperatureModel.Car1BigGearMotor2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧1_3, f => ss.GearboxMotorTemperatureModel.Car1BigGearMotor3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧1_4, f => ss.GearboxMotorTemperatureModel.Car1BigGearMotor4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧3_5, f => ss.GearboxMotorTemperatureModel.Car3BigGearMotor1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧3_6, f => ss.GearboxMotorTemperatureModel.Car3BigGearMotor2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧3_7, f => ss.GearboxMotorTemperatureModel.Car3BigGearMotor3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧3_8, f => ss.GearboxMotorTemperatureModel.Car3BigGearMotor4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧6_1, f => ss.GearboxMotorTemperatureModel.Car6BigGearMotor1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧6_2, f => ss.GearboxMotorTemperatureModel.Car6BigGearMotor2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧6_3, f => ss.GearboxMotorTemperatureModel.Car6BigGearMotor3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧6_4, f => ss.GearboxMotorTemperatureModel.Car6BigGearMotor4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧8_5, f => ss.GearboxMotorTemperatureModel.Car8BigGearMotor1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧8_6, f => ss.GearboxMotorTemperatureModel.Car8BigGearMotor2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧8_7, f => ss.GearboxMotorTemperatureModel.Car8BigGearMotor3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮牵引电机侧8_8, f => ss.GearboxMotorTemperatureModel.Car8BigGearMotor4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧1_1, f => ss.GearboxMotorTemperatureModel.Car1BigGearWheel1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧1_2, f => ss.GearboxMotorTemperatureModel.Car1BigGearWheel2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧1_3, f => ss.GearboxMotorTemperatureModel.Car1BigGearWheel3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧1_4, f => ss.GearboxMotorTemperatureModel.Car1BigGearWheel4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧3_5, f => ss.GearboxMotorTemperatureModel.Car3BigGearWheel1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧3_6, f => ss.GearboxMotorTemperatureModel.Car3BigGearWheel2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧3_7, f => ss.GearboxMotorTemperatureModel.Car3BigGearWheel3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧3_8, f => ss.GearboxMotorTemperatureModel.Car3BigGearWheel4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧6_1, f => ss.GearboxMotorTemperatureModel.Car6BigGearWheel1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧6_2, f => ss.GearboxMotorTemperatureModel.Car6BigGearWheel2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧6_3, f => ss.GearboxMotorTemperatureModel.Car6BigGearWheel3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧6_4, f => ss.GearboxMotorTemperatureModel.Car6BigGearWheel4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧8_5, f => ss.GearboxMotorTemperatureModel.Car8BigGearWheel1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧8_6, f => ss.GearboxMotorTemperatureModel.Car8BigGearWheel2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧8_7, f => ss.GearboxMotorTemperatureModel.Car8BigGearWheel3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_大齿轮车轮侧8_8, f => ss.GearboxMotorTemperatureModel.Car8BigGearWheel4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧1_1, f => ss.GearboxMotorTemperatureModel.Car1SmallGearMotor1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧1_2, f => ss.GearboxMotorTemperatureModel.Car1SmallGearMotor2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧1_3, f => ss.GearboxMotorTemperatureModel.Car1SmallGearMotor3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧1_4, f => ss.GearboxMotorTemperatureModel.Car1SmallGearMotor4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧3_5, f => ss.GearboxMotorTemperatureModel.Car3SmallGearMotor1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧3_6, f => ss.GearboxMotorTemperatureModel.Car3SmallGearMotor2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧3_7, f => ss.GearboxMotorTemperatureModel.Car3SmallGearMotor3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧3_8, f => ss.GearboxMotorTemperatureModel.Car3SmallGearMotor4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧6_1, f => ss.GearboxMotorTemperatureModel.Car6SmallGearMotor1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧6_2, f => ss.GearboxMotorTemperatureModel.Car6SmallGearMotor2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧6_3, f => ss.GearboxMotorTemperatureModel.Car6SmallGearMotor3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧6_4, f => ss.GearboxMotorTemperatureModel.Car6SmallGearMotor4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧8_5, f => ss.GearboxMotorTemperatureModel.Car8SmallGearMotor1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧8_6, f => ss.GearboxMotorTemperatureModel.Car8SmallGearMotor2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧8_7, f => ss.GearboxMotorTemperatureModel.Car8SmallGearMotor3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮牵引电机侧8_8, f => ss.GearboxMotorTemperatureModel.Car8SmallGearMotor4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧1_1, f => ss.GearboxMotorTemperatureModel.Car1SmallGearWheel1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧1_2, f => ss.GearboxMotorTemperatureModel.Car1SmallGearWheel2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧1_3, f => ss.GearboxMotorTemperatureModel.Car1SmallGearWheel3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧1_4, f => ss.GearboxMotorTemperatureModel.Car1SmallGearWheel4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧3_5, f => ss.GearboxMotorTemperatureModel.Car3SmallGearWheel1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧3_6, f => ss.GearboxMotorTemperatureModel.Car3SmallGearWheel2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧3_7, f => ss.GearboxMotorTemperatureModel.Car3SmallGearWheel3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧3_8, f => ss.GearboxMotorTemperatureModel.Car3SmallGearWheel4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧6_1, f => ss.GearboxMotorTemperatureModel.Car6SmallGearWheel1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧6_2, f => ss.GearboxMotorTemperatureModel.Car6SmallGearWheel2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧6_3, f => ss.GearboxMotorTemperatureModel.Car6SmallGearWheel3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧6_4, f => ss.GearboxMotorTemperatureModel.Car6SmallGearWheel4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧8_5, f => ss.GearboxMotorTemperatureModel.Car8SmallGearWheel1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧8_6, f => ss.GearboxMotorTemperatureModel.Car8SmallGearWheel2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧8_7, f => ss.GearboxMotorTemperatureModel.Car8SmallGearWheel3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1齿轮箱温度_小齿轮车轮侧8_8, f => ss.GearboxMotorTemperatureModel.Car8SmallGearWheel4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子1_1, f => ss.GearboxMotorTemperatureModel.Car1MotorStator1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子1_2, f => ss.GearboxMotorTemperatureModel.Car1MotorStator2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子1_3, f => ss.GearboxMotorTemperatureModel.Car1MotorStator3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子1_4, f => ss.GearboxMotorTemperatureModel.Car1MotorStator4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子3_5, f => ss.GearboxMotorTemperatureModel.Car3MotorStator1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子3_6, f => ss.GearboxMotorTemperatureModel.Car3MotorStator2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子3_7, f => ss.GearboxMotorTemperatureModel.Car3MotorStator3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子3_8, f => ss.GearboxMotorTemperatureModel.Car3MotorStator4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子6_1, f => ss.GearboxMotorTemperatureModel.Car6MotorStator1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子6_2, f => ss.GearboxMotorTemperatureModel.Car6MotorStator2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子6_3, f => ss.GearboxMotorTemperatureModel.Car6MotorStator3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子6_4, f => ss.GearboxMotorTemperatureModel.Car6MotorStator4 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子8_5, f => ss.GearboxMotorTemperatureModel.Car8MotorStator1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子8_6, f => ss.GearboxMotorTemperatureModel.Car8MotorStator2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子8_7, f => ss.GearboxMotorTemperatureModel.Car8MotorStator3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf动车组1牵引电机温度_牵引电机定子8_8, f => ss.GearboxMotorTemperatureModel.Car8MotorStator4 = f);

        }

        private void UpdataEmergencyModel(CommunicationDataChangedArgs args)
        {
            var em = ViewModel.Domain.Model.EmergencyModel;
            if (ViewModelManager.MainViewModel.StateViewModel.Model.CurrentStateInterface != null)
            {
                var mainid = ViewModelManager.MainViewModel.StateViewModel.Model.CurrentStateInterface.Id;
                if (mainid == StateInterfaceKey.Parser(StateKeys.Root_紧急))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb紧急释放速度限制ATP关闭是否可用, b =>
                    {
                        if (b)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_紧急释放速度);
                        }
                    });
                }
                if ((mainid == StateInterfaceKey.Parser(StateKeys.Root_紧急释放速度)))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb紧急释放速度限制ATP关闭是否可用, b =>
                    {
                        if (b == false)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_紧急);
                        }
                    });
                }
                if (mainid == StateInterfaceKey.Parser(StateKeys.Root_紧急_释放速度限制限速取消))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb速度限制100kmh, b =>
                    {
                        if (b)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_紧急_释放速度限制100);
                        }
                    });
                }
                if (mainid == StateInterfaceKey.Parser(StateKeys.Root_紧急_释放速度限制100))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb速度限制100kmh, b =>
                    {
                        if (b == false)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_紧急_释放速度限制限速取消);
                        }
                    });
                }
                if (mainid == StateInterfaceKey.Parser(StateKeys.Root_紧急_释放速度限制_取消限速))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb速度限制100kmh, b =>
                    {
                        if (b == false)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_紧急_释放速度限制取消限速);
                        }
                    });
                }
                if (mainid == StateInterfaceKey.Parser(StateKeys.Root_紧急_复位横向加速度报警没有限速))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb限速, b =>
                    {
                        if (b)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_紧急_复位横向加速度报警限速);
                        }
                    });
                }
                if (mainid == StateInterfaceKey.Parser(StateKeys.Root_紧急_复位横向加速度报警限速))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb限速, b =>
                    {
                        if (b == false)
                        {
                            ViewModelManager.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_紧急_复位横向加速度报警没有限速);
                        }
                    });
                }
            }

            if (ViewModelManager.ReserveViewModel.StateViewModel.Model.CurrentStateInterface != null)
            {
                var reserveId = ViewModelManager.ReserveViewModel.StateViewModel.Model.CurrentStateInterface.Id;

                if (reserveId == StateInterfaceKey.Parser(StateKeys.Root_紧急))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb紧急释放速度限制ATP关闭是否可用, b =>
                    {
                        if (b)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_紧急释放速度);
                        }
                    });
                }
                if ((reserveId == StateInterfaceKey.Parser(StateKeys.Root_紧急释放速度)))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb紧急释放速度限制ATP关闭是否可用, b =>
                    {
                        if (b == false)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_紧急);
                        }
                    });
                }
                if (reserveId == StateInterfaceKey.Parser(StateKeys.Root_紧急_释放速度限制限速取消))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb速度限制100kmh, b =>
                    {
                        if (b)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_紧急_释放速度限制100);
                        }
                    });
                }
                if (reserveId == StateInterfaceKey.Parser(StateKeys.Root_紧急_释放速度限制100))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb速度限制100kmh, b =>
                    {
                        if (b == false)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_紧急_释放速度限制限速取消);
                        }
                    });
                }
                if (reserveId == StateInterfaceKey.Parser(StateKeys.Root_紧急_释放速度限制_取消限速))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb速度限制100kmh, b =>
                    {
                        if (b == false)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_紧急_释放速度限制取消限速);
                        }
                    });
                }
                if (reserveId == StateInterfaceKey.Parser(StateKeys.Root_紧急_复位横向加速度报警没有限速))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb限速, b =>
                    {
                        if (b)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_紧急_复位横向加速度报警限速);
                        }
                    });
                }
                if (reserveId == StateInterfaceKey.Parser(StateKeys.Root_紧急_复位横向加速度报警限速))
                {
                    args.ChangedBools.UpdateIfContains(InBoolKeys.Inb限速, b =>
                    {
                        if (b == false)
                        {
                            ViewModelManager.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_紧急_复位横向加速度报警没有限速);
                        }
                    });
                }
            }
            
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb限速, b => em.AccelerationAlarmType = b ? AccelerationAlarmType.SpeedLimit : AccelerationAlarmType.NoSpeedLimit);
            //args.ChangedBools.UpdateIfContains(InBoolKeys.Inb复位所有横向加速度报警, b => em.AccelerationAlarmType = b ? AccelerationAlarmType.AccelerationAlarm : );
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb紧急释放速度限制ATP关闭是否可用, b => em.IsReleaseSpeed = b);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb速度限制100kmh, b => em.SpeedLimit100 = b);
            
        }

        private void UpdateDoorModel(CommunicationDataChangedArgs args)
        {
            var door = ViewModel.Domain.Model.Door;

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1车右_1门故障, b => door.Car1RightDoor2State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1车右_1门开, b => door.Car1RightDoor2State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1车右_1门锁闭, b => door.Car1RightDoor2State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb01车_右_1门状态未知, b => door.Car1RightDoor2State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1车左_1门故障, b => door.Car1LeftDoor2State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1车左_1门开, b => door.Car1LeftDoor2State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1车左_1门锁闭, b => door.Car1LeftDoor2State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb01车_左_1门状态未知, b => door.Car1LeftDoor2State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb2车右_1门故障, b => door.Car2RightDoor1State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb2车右_1门开, b => door.Car2RightDoor1State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb2车右_1门锁闭, b => door.Car2RightDoor1State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb02车_右_1门状态未知, b => door.Car2RightDoor1State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb2车右_2门故障, b => door.Car2RightDoor2State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb2车右_2门开, b => door.Car2RightDoor2State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb2车右_2门锁闭, b => door.Car2RightDoor2State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb02车_右_2门状态未知, b => door.Car2RightDoor2State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb2车左_1门故障, b => door.Car2LeftDoor1State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb2车左_1门开, b => door.Car2LeftDoor1State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb2车左_1门锁闭, b => door.Car2LeftDoor1State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb02车_左_1门状态未知, b => door.Car2LeftDoor1State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb2车左_2门故障, b => door.Car2LeftDoor2State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb2车左_2门开, b => door.Car2LeftDoor2State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb2车左_2门锁闭, b => door.Car2LeftDoor2State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb02车_左_2门状态未知, b => door.Car2LeftDoor2State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3车右_1门故障, b => door.Car3RightDoor1State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3车右_1门开, b => door.Car3RightDoor1State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3车右_1门锁闭, b => door.Car3RightDoor1State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb03车_右_1门状态未知, b => door.Car3RightDoor1State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3车右_2门故障, b => door.Car3RightDoor2State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3车右_2门开, b => door.Car3RightDoor2State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3车右_2门锁闭, b => door.Car3RightDoor2State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb03车_右_2门状态未知, b => door.Car3RightDoor2State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3车左_1门故障, b => door.Car3LeftDoor1State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3车左_1门开, b => door.Car3LeftDoor1State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3车左_1门锁闭, b => door.Car3LeftDoor1State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb03车_左_1门状态未知, b => door.Car3LeftDoor1State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3车左_2门故障, b => door.Car3LeftDoor2State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3车左_2门开, b => door.Car3LeftDoor2State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3车左_2门锁闭, b => door.Car3LeftDoor2State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb03车_左_2门状态未知, b => door.Car3LeftDoor2State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb4车右_1门故障, b => door.Car4RightDoor1State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb4车右_1门开, b => door.Car4RightDoor1State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb4车右_1门锁闭, b => door.Car4RightDoor1State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb04车_右_1门状态未知, b => door.Car4RightDoor1State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb4车左_1门故障, b => door.Car4LeftDoor1State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb4车左_1门开, b => door.Car4LeftDoor1State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb4车左_1门锁闭, b => door.Car4LeftDoor1State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb04车_左_1门状态未知, b => door.Car4LeftDoor1State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb6车右_1门故障, b => door.Car6RightDoor1State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb6车右_1门开, b => door.Car6RightDoor1State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb6车右_1门锁闭, b => door.Car6RightDoor1State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb06车_右_1门状态未知, b => door.Car6RightDoor1State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb6车右_2门故障, b => door.Car6RightDoor2State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb6车右_2门开, b => door.Car6RightDoor2State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb6车右_2门锁闭, b => door.Car6RightDoor2State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb06车_右_2门状态未知, b => door.Car6RightDoor2State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb6车左_1门故障, b => door.Car6LeftDoor1State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb6车左_1门开, b => door.Car6LeftDoor1State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb6车左_1门锁闭, b => door.Car6LeftDoor1State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb06车_左_1门状态未知, b => door.Car6LeftDoor1State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb6车左_2门故障, b => door.Car6LeftDoor2State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb6车左_2门开, b => door.Car6LeftDoor2State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb6车左_2门锁闭, b => door.Car6LeftDoor2State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb06车_左_2门状态未知, b => door.Car6LeftDoor2State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb7车右_1门故障, b => door.Car7RightDoor1State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb7车右_1门开, b => door.Car7RightDoor1State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb7车右_1门锁闭, b => door.Car7RightDoor1State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb07车_右_1门状态未知, b => door.Car7RightDoor1State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb7车右_2门故障, b => door.Car7RightDoor2State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb7车右_2门开, b => door.Car7RightDoor2State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb7车右_2门锁闭, b => door.Car7RightDoor2State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb07车_右_2门状态未知, b => door.Car7RightDoor2State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb7车左_1门故障, b => door.Car7LeftDoor1State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb7车左_1门开, b => door.Car7LeftDoor1State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb7车左_1门锁闭, b => door.Car7LeftDoor1State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb07车_左_1门状态未知, b => door.Car7LeftDoor1State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb7车左_2门故障, b => door.Car7LeftDoor2State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb7车左_2门开, b => door.Car7LeftDoor2State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb7车左_2门锁闭, b => door.Car7LeftDoor2State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb07车_左_2门状态未知, b => door.Car7LeftDoor2State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb8车右_1门故障, b => door.Car8RightDoor1State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb8车右_1门开, b => door.Car8RightDoor1State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb8车右_1门锁闭, b => door.Car8RightDoor1State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb08车_右_1门状态未知, b => door.Car8RightDoor1State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb8车左_1门故障, b => door.Car8LeftDoor1State = b ? DoorState.Fault : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb8车左_1门开, b => door.Car8LeftDoor1State = b ? DoorState.Open : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb8车左_1门锁闭, b => door.Car8LeftDoor1State = b ? DoorState.Lock : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb08车_左_1门状态未知, b => door.Car8LeftDoor1State = b ? DoorState.Unkown : DoorState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1车_右侧门已禁用, b => door.RightDoor1DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1车_左侧门已禁用, b => door.LeftDoor1DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb2车_右侧门已禁用, b => door.RightDoor2DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb2车_左侧门已禁用, b => door.LeftDoor2DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3车_右侧门已禁用, b => door.RightDoor3DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3车_左侧门已禁用, b => door.LeftDoor3DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb4车_右侧门已禁用, b => door.RightDoor4DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb4车_左侧门已禁用, b => door.LeftDoor4DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb5车_右侧门已禁用, b => door.RightDoor5DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb5车_左侧门已禁用, b => door.LeftDoor5DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb6车_右侧门已禁用, b => door.RightDoor6DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb6车_左侧门已禁用, b => door.LeftDoor6DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb7车_右侧门已禁用, b => door.RightDoor7DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb7车_左侧门已禁用, b => door.LeftDoor7DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb8车_右侧门已禁用, b => door.RightDoor8DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb8车_左侧门已禁用, b => door.LeftDoor8DisableType = b ? DoorDisableType.Disable : DoorDisableType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb车厢号1全部门状态未知, b => door.Door1OpenType = b ? DoorOpenType.Unknow : DoorOpenType.AllClose);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb车厢号2全部门状态未知, b => door.Door2OpenType = b ? DoorOpenType.Unknow : DoorOpenType.AllClose);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb车厢号3全部门状态未知, b => door.Door3OpenType = b ? DoorOpenType.Unknow : DoorOpenType.AllClose);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb车厢号4全部门状态未知, b => door.Door4OpenType = b ? DoorOpenType.Unknow : DoorOpenType.AllClose);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb车厢号6全部门状态未知, b => door.Door6OpenType = b ? DoorOpenType.Unknow : DoorOpenType.AllClose);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb车厢号7全部门状态未知, b => door.Door7OpenType = b ? DoorOpenType.Unknow : DoorOpenType.AllClose);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb车厢号8全部门状态未知, b => door.Door8OpenType = b ? DoorOpenType.Unknow : DoorOpenType.AllClose);
            
            
        }

        private void UpdateSwitchModel(CommunicationDataChangedArgs args)
        {
            var sw = ViewModel.Domain.Model.Switch;

            

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_受电弓_1_升弓 , b => sw.TractionModel.Car2PantographState = b ? PantographState.Rise : PantographState.Unkown);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_受电弓_1_降弓, b => sw.TractionModel.Car2PantographState = b ? PantographState.Down : PantographState.Unkown);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_受电弓_1_切除, b => sw.TractionModel.Car2PantographState = b ? PantographState.Removal : PantographState.Unkown);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_受电弓_2_升弓, b => sw.TractionModel.Car7PantographState = b ? PantographState.Rise : PantographState.Unkown);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_受电弓_2_降弓, b => sw.TractionModel.Car7PantographState = b ? PantographState.Down : PantographState.Unkown);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_受电弓_2_切除, b => sw.TractionModel.Car7PantographState = b ? PantographState.Removal : PantographState.Unkown);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_主断_1_闭合, b => sw.TractionModel.Car2MainBreakState = b ? MainBreakState.Closure : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_主断_1_断开, b => sw.TractionModel.Car2MainBreakState = b ? MainBreakState.Disconnect : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_主断_1_切除, b => sw.TractionModel.Car2MainBreakState = b ? MainBreakState.Removal : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_主断_2_闭合, b => sw.TractionModel.Car7MainBreakState = b ? MainBreakState.Closure : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_主断_2_断开, b => sw.TractionModel.Car7MainBreakState = b ? MainBreakState.Disconnect : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_主断_2_切除, b => sw.TractionModel.Car7MainBreakState = b ? MainBreakState.Removal : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_车顶隔离开关_1_闭合, b => sw.TractionModel.Car2RoofIsolationState = b ? RoofIsolationState.Closure : RoofIsolationState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_车顶隔离开关_1_断开, b => sw.TractionModel.Car2RoofIsolationState = b ? RoofIsolationState.Disconnect : RoofIsolationState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_车顶隔离开关_1_切除, b => sw.TractionModel.Car2RoofIsolationState = b ? RoofIsolationState.Removal : RoofIsolationState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_车顶隔离开关_2_闭合, b => sw.TractionModel.Car7RoofIsolationState = b ? RoofIsolationState.Closure : RoofIsolationState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_车顶隔离开关_2_断开, b => sw.TractionModel.Car7RoofIsolationState = b ? RoofIsolationState.Disconnect : RoofIsolationState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_车顶隔离开关_2_切除, b => sw.TractionModel.Car7RoofIsolationState = b ? RoofIsolationState.Removal : RoofIsolationState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_牵引变流器_11_闭合, b => sw.TractionModel.Car1TractionConverterState = b ? MainBreakState.Closure : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_牵引变流器_11_断开, b => sw.TractionModel.Car1TractionConverterState = b ? MainBreakState.Disconnect : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_牵引变流器_11_切除, b => sw.TractionModel.Car1TractionConverterState = b ? MainBreakState.Removal : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_牵引变流器_12_闭合, b => sw.TractionModel.Car3TractionConverterState = b ? MainBreakState.Closure : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_牵引变流器_12_断开, b => sw.TractionModel.Car3TractionConverterState = b ? MainBreakState.Disconnect : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_牵引变流器_12_切除, b => sw.TractionModel.Car3TractionConverterState = b ? MainBreakState.Removal : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_牵引变流器_21_闭合, b => sw.TractionModel.Car6TractionConverterState = b ? MainBreakState.Closure : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_牵引变流器_21_断开, b => sw.TractionModel.Car6TractionConverterState = b ? MainBreakState.Disconnect : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_牵引变流器_21_切除, b => sw.TractionModel.Car6TractionConverterState = b ? MainBreakState.Removal : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_牵引变流器_22_闭合, b => sw.TractionModel.Car8TractionConverterState = b ? MainBreakState.Closure : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_牵引变流器_22_断开, b => sw.TractionModel.Car8TractionConverterState = b ? MainBreakState.Disconnect : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_牵引变流器_22_切除, b => sw.TractionModel.Car8TractionConverterState = b ? MainBreakState.Removal : MainBreakState.Unkonw);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_1_蓝 , b => sw.TractionModel.Car2AssistPoweredUnitState = b ? VoltageChargerState.Blue : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_1_黄, b => sw.TractionModel.Car2AssistPoweredUnitState = b ? VoltageChargerState.Yellow : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_1_状态未知, b => sw.TractionModel.Car2AssistPoweredUnitState = b ? VoltageChargerState.Unknow : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_2_蓝, b => sw.TractionModel.Car4AssistPoweredUnit1State = b ? VoltageChargerState.Blue : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_2_黄, b => sw.TractionModel.Car4AssistPoweredUnit1State = b ? VoltageChargerState.Yellow : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_2_状态未知, b => sw.TractionModel.Car4AssistPoweredUnit1State = b ? VoltageChargerState.Unknow : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_3_蓝, b => sw.TractionModel.Car4AssistPoweredUnit2State = b ? VoltageChargerState.Blue : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_3_黄, b => sw.TractionModel.Car4AssistPoweredUnit2State = b ? VoltageChargerState.Yellow : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_3_状态未知, b => sw.TractionModel.Car4AssistPoweredUnit2State = b ? VoltageChargerState.Unknow : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_4_蓝, b => sw.TractionModel.Car5AssistPoweredUnit1State = b ? VoltageChargerState.Blue : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_4_黄, b => sw.TractionModel.Car5AssistPoweredUnit1State = b ? VoltageChargerState.Yellow : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_4_状态未知, b => sw.TractionModel.Car5AssistPoweredUnit1State = b ? VoltageChargerState.Unknow : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_5_蓝, b => sw.TractionModel.Car5AssistPoweredUnit1State = b ? VoltageChargerState.Blue : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_5_黄, b => sw.TractionModel.Car5AssistPoweredUnit1State = b ? VoltageChargerState.Yellow : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_5_状态未知, b => sw.TractionModel.Car5AssistPoweredUnit1State = b ? VoltageChargerState.Unknow : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_6_蓝, b => sw.TractionModel.Car7AssistPoweredUnitState = b ? VoltageChargerState.Blue : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_6_黄, b => sw.TractionModel.Car7AssistPoweredUnitState = b ? VoltageChargerState.Yellow : VoltageChargerState.Black);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb牵引_辅助供电单元_6_状态未知, b => sw.TractionModel.Car7AssistPoweredUnitState = b ? VoltageChargerState.Unknow : VoltageChargerState.Black);

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调紧急关, b => sw.AirConditionModel.TrainAirConditionState = b ? AirConditionState.EmergencyClose : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_动车组1_手动开启空调, b => sw.AirConditionModel.EMU1AirConditionState = b ? AirConditionState.ManiualOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_动车组1_自动开启空调, b => sw.AirConditionModel.EMU1AirConditionState = b ? AirConditionState.AutoOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调紧急关, b => sw.AirConditionModel.EMU1AirConditionState = b ? AirConditionState.EmergencyClose : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_11_手动开启空调, b => sw.AirConditionModel.OnecarAirCondition1State = b ? AirConditionState.ManiualOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_11_自动开启空调, b => sw.AirConditionModel.OnecarAirCondition1State = b ? AirConditionState.AutoOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调紧急关, b => sw.AirConditionModel.OnecarAirCondition1State = b ? AirConditionState.EmergencyClose : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_12_手动开启空调, b => sw.AirConditionModel.OnecarAirCondition2State = b ? AirConditionState.ManiualOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_12_自动开启空调, b => sw.AirConditionModel.OnecarAirCondition2State = b ? AirConditionState.AutoOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调紧急关, b => sw.AirConditionModel.OnecarAirCondition2State = b ? AirConditionState.EmergencyClose : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_13_手动开启空调, b => sw.AirConditionModel.OnecarAirCondition3State = b ? AirConditionState.ManiualOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_13_自动开启空调, b => sw.AirConditionModel.OnecarAirCondition3State = b ? AirConditionState.AutoOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调紧急关, b => sw.AirConditionModel.OnecarAirCondition3State = b ? AirConditionState.EmergencyClose : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_14_手动开启空调, b => sw.AirConditionModel.OnecarAirCondition4State = b ? AirConditionState.ManiualOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_14_自动开启空调, b => sw.AirConditionModel.OnecarAirCondition4State = b ? AirConditionState.AutoOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调紧急关, b => sw.AirConditionModel.OnecarAirCondition4State = b ? AirConditionState.EmergencyClose : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_15_手动开启空调, b => sw.AirConditionModel.OnecarAirCondition5State = b ? AirConditionState.ManiualOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_15_自动开启空调, b => sw.AirConditionModel.OnecarAirCondition5State = b ? AirConditionState.AutoOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调紧急关, b => sw.AirConditionModel.OnecarAirCondition5State = b ? AirConditionState.EmergencyClose : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_16_手动开启空调, b => sw.AirConditionModel.OnecarAirCondition6State = b ? AirConditionState.ManiualOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_16_自动开启空调, b => sw.AirConditionModel.OnecarAirCondition6State = b ? AirConditionState.AutoOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调紧急关, b => sw.AirConditionModel.OnecarAirCondition6State = b ? AirConditionState.EmergencyClose : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_17_手动开启空调, b => sw.AirConditionModel.OnecarAirCondition7State = b ? AirConditionState.ManiualOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_17_自动开启空调, b => sw.AirConditionModel.OnecarAirCondition7State = b ? AirConditionState.AutoOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调紧急关, b => sw.AirConditionModel.OnecarAirCondition7State = b ? AirConditionState.EmergencyClose : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_18_手动开启空调, b => sw.AirConditionModel.OnecarAirCondition8State = b ? AirConditionState.ManiualOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调_单车空调_18_自动开启空调, b => sw.AirConditionModel.OnecarAirCondition8State = b ? AirConditionState.AutoOpen : AirConditionState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空调紧急关, b => sw.AirConditionModel.OnecarAirCondition8State = b ? AirConditionState.EmergencyClose : AirConditionState.Close);



            if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_2))
            {
                sw.AirConditionModel.TemPeratureState11 = AirConditionTemperatureState.Three;
                sw.AirConditionModel.TemPeratureState12 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_2) == false)
            {
                sw.AirConditionModel.TemPeratureState11 = AirConditionTemperatureState.One;
                sw.AirConditionModel.TemPeratureState12 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_2))
            {
                sw.AirConditionModel.TemPeratureState11 = AirConditionTemperatureState.Two;
                sw.AirConditionModel.TemPeratureState12 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_2) == false)
            {
                sw.AirConditionModel.TemPeratureState11 = AirConditionTemperatureState.Zero;
                sw.AirConditionModel.TemPeratureState12 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_2))
            {
                sw.AirConditionModel.TemPeratureState11 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState12 = AirConditionTemperatureState.Two;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_2) == false)
            {
                sw.AirConditionModel.TemPeratureState11 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState12 = AirConditionTemperatureState.Zero;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_11_2))
            {
                sw.AirConditionModel.TemPeratureState11 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState12 = AirConditionTemperatureState.One;
            }
            else
            {
                sw.AirConditionModel.TemPeratureState11 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState12 = AirConditionTemperatureState.Four;
            }


            if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_2))
            {
                sw.AirConditionModel.TemPeratureState21 = AirConditionTemperatureState.Three;
                sw.AirConditionModel.TemPeratureState22 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_2) == false)
            {
                sw.AirConditionModel.TemPeratureState21 = AirConditionTemperatureState.One;
                sw.AirConditionModel.TemPeratureState22 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_2))
            {
                sw.AirConditionModel.TemPeratureState21 = AirConditionTemperatureState.Two;
                sw.AirConditionModel.TemPeratureState22 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_2) == false)
            {
                sw.AirConditionModel.TemPeratureState21 = AirConditionTemperatureState.Zero;
                sw.AirConditionModel.TemPeratureState22 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_2))
            {
                sw.AirConditionModel.TemPeratureState21 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState22 = AirConditionTemperatureState.Two;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_2) == false)
            {
                sw.AirConditionModel.TemPeratureState21 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState22 = AirConditionTemperatureState.Zero;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_12_2))
            {
                sw.AirConditionModel.TemPeratureState21 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState22 = AirConditionTemperatureState.One;
            }
            else
            {
                sw.AirConditionModel.TemPeratureState21 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState22 = AirConditionTemperatureState.Four;
            }


            if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_2))
            {
                sw.AirConditionModel.TemPeratureState31 = AirConditionTemperatureState.Three;
                sw.AirConditionModel.TemPeratureState32 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_2) == false)
            {
                sw.AirConditionModel.TemPeratureState31 = AirConditionTemperatureState.One;
                sw.AirConditionModel.TemPeratureState32 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_2))
            {
                sw.AirConditionModel.TemPeratureState31 = AirConditionTemperatureState.Two;
                sw.AirConditionModel.TemPeratureState32 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_2) == false)
            {
                sw.AirConditionModel.TemPeratureState31 = AirConditionTemperatureState.Zero;
                sw.AirConditionModel.TemPeratureState32 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_2))
            {
                sw.AirConditionModel.TemPeratureState31 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState32 = AirConditionTemperatureState.Two;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_2) == false)
            {
                sw.AirConditionModel.TemPeratureState31 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState32 = AirConditionTemperatureState.Zero;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_13_2))
            {
                sw.AirConditionModel.TemPeratureState31 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState32 = AirConditionTemperatureState.One;
            }
            else
            {
                sw.AirConditionModel.TemPeratureState31 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState32 = AirConditionTemperatureState.Four;
            }


            if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_2))
            {
                sw.AirConditionModel.TemPeratureState41 = AirConditionTemperatureState.Three;
                sw.AirConditionModel.TemPeratureState42 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_2) == false)
            {
                sw.AirConditionModel.TemPeratureState41 = AirConditionTemperatureState.One;
                sw.AirConditionModel.TemPeratureState42 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_2))
            {
                sw.AirConditionModel.TemPeratureState41 = AirConditionTemperatureState.Two;
                sw.AirConditionModel.TemPeratureState42 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_2) == false)
            {
                sw.AirConditionModel.TemPeratureState41 = AirConditionTemperatureState.Zero;
                sw.AirConditionModel.TemPeratureState42 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_2))
            {
                sw.AirConditionModel.TemPeratureState41 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState42 = AirConditionTemperatureState.Two;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_2) == false)
            {
                sw.AirConditionModel.TemPeratureState41 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState42 = AirConditionTemperatureState.Zero;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_14_2))
            {
                sw.AirConditionModel.TemPeratureState41 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState42 = AirConditionTemperatureState.One;
            }
            else
            {
                sw.AirConditionModel.TemPeratureState41 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState42 = AirConditionTemperatureState.Four;
            }


            if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_2))
            {
                sw.AirConditionModel.TemPeratureState51 = AirConditionTemperatureState.Three;
                sw.AirConditionModel.TemPeratureState52 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_2) == false)
            {
                sw.AirConditionModel.TemPeratureState51 = AirConditionTemperatureState.One;
                sw.AirConditionModel.TemPeratureState52 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_2))
            {
                sw.AirConditionModel.TemPeratureState51 = AirConditionTemperatureState.Two;
                sw.AirConditionModel.TemPeratureState52 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_2) == false)
            {
                sw.AirConditionModel.TemPeratureState51 = AirConditionTemperatureState.Zero;
                sw.AirConditionModel.TemPeratureState52 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_2))
            {
                sw.AirConditionModel.TemPeratureState51 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState52 = AirConditionTemperatureState.Two;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_2) == false)
            {
                sw.AirConditionModel.TemPeratureState51 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState52 = AirConditionTemperatureState.Zero;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_15_2))
            {
                sw.AirConditionModel.TemPeratureState51 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState52 = AirConditionTemperatureState.One;
            }
            else
            {
                sw.AirConditionModel.TemPeratureState51 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState52 = AirConditionTemperatureState.Four;
            }


            if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_2))
            {
                sw.AirConditionModel.TemPeratureState61 = AirConditionTemperatureState.Three;
                sw.AirConditionModel.TemPeratureState62 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_2) == false)
            {
                sw.AirConditionModel.TemPeratureState61 = AirConditionTemperatureState.One;
                sw.AirConditionModel.TemPeratureState62 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_2))
            {
                sw.AirConditionModel.TemPeratureState61 = AirConditionTemperatureState.Two;
                sw.AirConditionModel.TemPeratureState62 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_2) == false)
            {
                sw.AirConditionModel.TemPeratureState61 = AirConditionTemperatureState.Zero;
                sw.AirConditionModel.TemPeratureState62 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_2))
            {
                sw.AirConditionModel.TemPeratureState61 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState62 = AirConditionTemperatureState.Two;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_2) == false)
            {
                sw.AirConditionModel.TemPeratureState61 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState62 = AirConditionTemperatureState.Zero;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_16_2))
            {
                sw.AirConditionModel.TemPeratureState61 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState62 = AirConditionTemperatureState.One;
            }
            else
            {
                sw.AirConditionModel.TemPeratureState61 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState62 = AirConditionTemperatureState.Four;
            }


            if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_2))
            {
                sw.AirConditionModel.TemPeratureState71 = AirConditionTemperatureState.Three;
                sw.AirConditionModel.TemPeratureState72 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_2) == false)
            {
                sw.AirConditionModel.TemPeratureState71 = AirConditionTemperatureState.One;
                sw.AirConditionModel.TemPeratureState72 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_2))
            {
                sw.AirConditionModel.TemPeratureState71 = AirConditionTemperatureState.Two;
                sw.AirConditionModel.TemPeratureState72 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_2) == false)
            {
                sw.AirConditionModel.TemPeratureState71 = AirConditionTemperatureState.Zero;
                sw.AirConditionModel.TemPeratureState72 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_2))
            {
                sw.AirConditionModel.TemPeratureState71 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState72 = AirConditionTemperatureState.Two;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_2) == false)
            {
                sw.AirConditionModel.TemPeratureState71 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState72 = AirConditionTemperatureState.Zero;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_17_2))
            {
                sw.AirConditionModel.TemPeratureState71 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState72 = AirConditionTemperatureState.One;
            }
            else
            {
                sw.AirConditionModel.TemPeratureState71 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState72 = AirConditionTemperatureState.Four;
            }


            if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_2))
            {
                sw.AirConditionModel.TemPeratureState81 = AirConditionTemperatureState.Three;
                sw.AirConditionModel.TemPeratureState82 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_2) == false)
            {
                sw.AirConditionModel.TemPeratureState81 = AirConditionTemperatureState.One;
                sw.AirConditionModel.TemPeratureState82 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_2))
            {
                sw.AirConditionModel.TemPeratureState81 = AirConditionTemperatureState.Two;
                sw.AirConditionModel.TemPeratureState82 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_0) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_2) == false)
            {
                sw.AirConditionModel.TemPeratureState81 = AirConditionTemperatureState.Zero;
                sw.AirConditionModel.TemPeratureState82 = AirConditionTemperatureState.Four;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_2))
            {
                sw.AirConditionModel.TemPeratureState81 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState82 = AirConditionTemperatureState.Two;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_1) && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_2) == false)
            {
                sw.AirConditionModel.TemPeratureState81 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState82 = AirConditionTemperatureState.Zero;
            }
            else if (DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_0) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_1) == false && DataService.ReadService.GetInBoolOf(InBoolKeys.Inb空调_温度_18_2))
            {
                sw.AirConditionModel.TemPeratureState81 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState82 = AirConditionTemperatureState.One;
            }
            else
            {
                sw.AirConditionModel.TemPeratureState81 = AirConditionTemperatureState.Four;
                sw.AirConditionModel.TemPeratureState82 = AirConditionTemperatureState.Four;
            }


            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_动车组1_照明13, b => sw.IlluminationModel.EMU1IlluminationState = b ? IlluminationState.Illumination1_3 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_动车组1_照明1, b => sw.IlluminationModel.EMU1IlluminationState = b ? IlluminationState.Illumination1 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_全列车_照明13, b => sw.IlluminationModel.TrainIlluminationState = b ? IlluminationState.Illumination1_3 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_全列车_照明1, b => sw.IlluminationModel.TrainIlluminationState = b ? IlluminationState.Illumination1 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_11_照明13, b => sw.IlluminationModel.OnecarIllumination1State = b ? IlluminationState.Illumination1_3 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_11_照明1, b => sw.IlluminationModel.OnecarIllumination1State = b ? IlluminationState.Illumination1 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_12_照明13, b => sw.IlluminationModel.OnecarIllumination2State = b ? IlluminationState.Illumination1_3 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_12_照明1, b => sw.IlluminationModel.OnecarIllumination2State = b ? IlluminationState.Illumination1 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_13_照明13, b => sw.IlluminationModel.OnecarIllumination3State = b ? IlluminationState.Illumination1_3 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_13_照明1, b => sw.IlluminationModel.OnecarIllumination3State = b ? IlluminationState.Illumination1 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_14_照明13, b => sw.IlluminationModel.OnecarIllumination4State = b ? IlluminationState.Illumination1_3 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_14_照明1, b => sw.IlluminationModel.OnecarIllumination4State = b ? IlluminationState.Illumination1 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_15_照明13, b => sw.IlluminationModel.OnecarIllumination5State = b ? IlluminationState.Illumination1_3 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_15_照明1, b => sw.IlluminationModel.OnecarIllumination5State = b ? IlluminationState.Illumination1 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_16_照明13, b => sw.IlluminationModel.OnecarIllumination6State = b ? IlluminationState.Illumination1_3 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_16_照明1, b => sw.IlluminationModel.OnecarIllumination6State = b ? IlluminationState.Illumination1 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_17_照明13, b => sw.IlluminationModel.OnecarIllumination7State = b ? IlluminationState.Illumination1_3 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_17_照明1, b => sw.IlluminationModel.OnecarIllumination7State = b ? IlluminationState.Illumination1 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_18_照明13, b => sw.IlluminationModel.OnecarIllumination8State = b ? IlluminationState.Illumination1_3 : IlluminationState.Illumination0);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb照明_单车照明_18_照明1, b => sw.IlluminationModel.OnecarIllumination8State = b ? IlluminationState.Illumination1 : IlluminationState.Illumination0);


            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb922, b => sw.NetWorkCurrentLimitEnable = b);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb前窗加热常用模式, b =>
            {
                if (b)
                {
                    sw.FrontWindowHeatingType = FrontWindowHeatingType.CommonMode;
                    EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub牵引手柄检测通道1, false));
                    EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub牵引手柄检测通道2, false));
                }
                else
                {
                    sw.FrontWindowHeatingType = FrontWindowHeatingType.Close;
                }
            });
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb前窗加热解冻模式, b =>
            {
                if (b)
                {
                    sw.FrontWindowHeatingType = FrontWindowHeatingType.DefrostMode;
                    EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub牵引手柄检测通道1, false));
                    EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub牵引手柄检测通道2, true));
                }
                else
                {
                    sw.FrontWindowHeatingType = FrontWindowHeatingType.Close;
                }
            });
        }
        private void UpdateTitleModel(CommunicationDataChangedArgs args)
        {
            var t = ViewModel.Domain.Model.TitleModel;

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条ASC设置0为关闭1为开启, b => t.AscSettingState = b ? AscSettingState.Open : AscSettingState.Close);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条整备运行激活, b => t.PrepareRunActivationState = b ? PrepareRunActivationState.PrepareRunActivation : PrepareRunActivationState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条至少一个主断路器能够闭合, b => t.InfoMainBreakState = b ? InfoMainBreakState.AtLeastOneDisconnect : InfoMainBreakState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条全部主断路器已闭合, b => t.InfoMainBreakState = b ? InfoMainBreakState.AllDisconnect : InfoMainBreakState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条制动手柄应置于0位, b => t.SettingPlacedInZeroType = b ? SettingPlacedInZeroType.BreakHandle : SettingPlacedInZeroType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条牵引手柄应置于0位, b => t.SettingPlacedInZeroType = b ? SettingPlacedInZeroType.TractionHandle : SettingPlacedInZeroType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条运行速度设定应置于0位, b => t.SettingPlacedInZeroType = b ? SettingPlacedInZeroType.RunSpeed : SettingPlacedInZeroType.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条列车已就绪, b => t.TrainIsReadyState = b ? TrainIsReadyState.TrianIsReady : TrainIsReadyState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条远光灯强, b => t.InfoLightState = b ? InfoLightState.HighBeamLightStrong : InfoLightState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条列车管充风已被切断, b => t.TrainPipeCutoffState = b ? TrainPipeCutoffState.Cutoff : TrainPipeCutoffState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb列车管充风已被切断状态未知信息状态栏, b => t.TrainPipeCutoffState = b ? TrainPipeCutoffState.Unknow : TrainPipeCutoffState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条时速限制_公里小时, b => t.SpeedLimitState = b ? SpeedLimitState.SpeedLimit : SpeedLimitState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条至少一门仍开启, b => t.AtLeastOneDoorOpenState = b ? AtLeastOneDoorOpenState.AtLeastOneDoorOpen : AtLeastOneDoorOpenState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条自动安全装置, b => t.AutoSafeDeviceState = b ? AutoSafeDeviceState.AutoSafeDevice : AutoSafeDeviceState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条司机室变更模式, b => t.DriverRoomChangeModeState = b ? DriverRoomChangeModeState.DriverRoomChangeMode : DriverRoomChangeModeState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条与门通讯故障, b => t.InfoDoorState = b ? InfoDoorState.CommunicationFault : InfoDoorState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条火警闪烁, b => t.FireState = b ? FireState.Fire : FireState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb信息状态条司机室门已释放可操作, b => t.InfoDriverRoomDoorState = b ? InfoDriverRoomDoorState.Freed : InfoDriverRoomDoorState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb灯光状态位0位, b => t.InfoLightState = b ? InfoLightState.LightState0 : InfoLightState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb灯光状态位1位, b => t.InfoLightState = b ? InfoLightState.LightState1 : InfoLightState.Null);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb灯光状态位2位, b => t.InfoLightState = b ? InfoLightState.LightState2 : InfoLightState.Null);

            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf最高时速, f => t.HighestSpeed = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf当前运行速度, f => t.Speed = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf外部温度, f => t.OutTemperature = f);
        }
    }
}
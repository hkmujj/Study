using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Urban.ATC.Domain.Interface;
using Urban.ATC.Siemens.Model;
using Urban.ATC.Siemens.Resource.Internal;
using Urban.ATC.Siemens.WPF.Control.Config;
using Urban.ATC.Siemens.WPF.Control.Constant;
using Urban.ATC.Siemens.WPF.Control.Event;
using Urban.ATC.Siemens.WPF.Interface;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;
using Urban.Info.Interface.ACK;
using Urban.Info.Interface.ButtonReactivation;

namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class SiemensData : NotificationObject, IDataListener
    {
        private BrakeDetailsViewModel m_BrakeDetails;
        private MenuViewModel m_Menu;
        private GlobalViewModel m_Global;
        private RegionEViewModel m_RegionE;
        private RegionCViewModel m_RegionC;
        private RegionMViewModel m_RegionM;
        private RegionTViewModel m_RegionT;
        private RegionAViewModel m_RegionAViewModel;
        private RegionBViewModel m_RegionBViewModel;
        private RegionFViewModel m_RegionFViewModel;
        private Visibility m_MMIBack;
        private InputKeyBoardModel m_InputKeyBoard;
        private InputScreenModel m_InputScreen;
        private GeneralSrceenViewModel m_GeneralSrceen;
        private ButtonReactionModel m_ReactionModel;
        private Timer ScreenSaverTimer;
        public ICommand OperationCommand { get; private set; }
        public ICommunicationDataService DataService { get; set; }
        public ICommand ScreenSaverCommand { get; private set; }
        public SiemensData()
        {
            Menu = new MenuViewModel(this);
            RegionC = new RegionCViewModel();
            RegionE = new RegionEViewModel();
            RegionM = new RegionMViewModel();
            RegionT = new RegionTViewModel();
            Global = new GlobalViewModel();
            RegionBViewModel = new RegionBViewModel();
            RegionAViewModel = new RegionAViewModel();
            RegionFViewModel = new RegionFViewModel();
            BrakeDetails = new BrakeDetailsViewModel();
            InputKeyBoard = new InputKeyBoardModel(this);
            InputScreen = new InputScreenModel(this);
            GeneralSrceen = new GeneralSrceenViewModel();
            MMIBack = Visibility.Hidden;
            ButtonReactivationMgr = new ButtonReactivationMgr();
            ReactionModel = new ButtonReactionModel(this);
            RegionC.EventAggregator.GetEvent<NavigatorEvent>().Subscribe((args) =>
            {
                if (args.Region != RegionNames.Menu)
                {
                    RegionC.RegionManager.RequestNavigate(args.Region, args.Name);
                }
            }, ThreadOption.UIThread);
            m_Timer = new Timer((state) =>
            {
                RegionC.EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
                {
                    Region = RegionNames.MainRoot,
                    Name = ControlNames.DomainControl
                });
            });
            ScreenSaverTimer = new Timer((state) =>
              {
                  RegionC.EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
                  {
                      Region = RegionNames.MainRoot,
                      Name = ControlNames.ScreenSaverView
                  });
              });
            ScreenSaverCommand = new DelegateCommand(() =>
              {
                  RegionC.EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
                  {
                      Region = RegionNames.MainRoot,
                      Name = ControlNames.DomainControl
                  });
                  var index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.进入屏保界面];
                  if (DataService.ReadService.GetBoolAt(index))
                  {
                      ScreenSaverTimer.Change(5000, int.MaxValue);
                  }

              });
            OperationCommand = new DelegateCommand((() =>
              {
                  var index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.进入屏保界面];
                  if (DataService.ReadService.GetBoolAt(index))
                  {
                      ScreenSaverTimer.Change(5000, int.MaxValue);
                  }
              }));
        }

        public ButtonReactionModel ReactionModel
        {
            get { return m_ReactionModel; }
            set
            {
                if (Equals(value, m_ReactionModel))
                {
                    return;
                }
                m_ReactionModel = value;
                RaisePropertyChanged(() => ReactionModel);
            }
        }

        public ButtonReactivationMgr ButtonReactivationMgr { get; set; }

        public GeneralSrceenViewModel GeneralSrceen
        {
            get { return m_GeneralSrceen; }
            set
            {
                if (Equals(value, m_GeneralSrceen))
                {
                    return;
                }
                m_GeneralSrceen = value;
                RaisePropertyChanged(() => GeneralSrceen);
            }
        }

        public ACKManage ACKManage { get; set; }
        public IInputContent InputView { get; set; }

        public InputScreenModel InputScreen
        {
            get { return m_InputScreen; }
            set
            {
                if (Equals(value, m_InputScreen))
                {
                    return;
                }
                m_InputScreen = value;
                RaisePropertyChanged(() => InputScreen);
            }
        }

        public InputKeyBoardModel InputKeyBoard
        {
            get { return m_InputKeyBoard; }
            set
            {
                if (Equals(value, m_InputKeyBoard))
                {
                    return;
                }
                m_InputKeyBoard = value;
                RaisePropertyChanged(() => InputKeyBoard);
            }
        }

        public RegionFViewModel RegionFViewModel
        {
            get { return m_RegionFViewModel; }
            set
            {
                if (Equals(value, m_RegionFViewModel))
                {
                    return;
                }
                m_RegionFViewModel = value;
                RaisePropertyChanged(() => RegionFViewModel);
            }
        }

        public RegionBViewModel RegionBViewModel
        {
            get { return m_RegionBViewModel; }
            set
            {
                if (Equals(value, m_RegionBViewModel))
                {
                    return;
                }
                m_RegionBViewModel = value;
                RaisePropertyChanged(() => RegionBViewModel);
            }
        }

        public RegionAViewModel RegionAViewModel
        {
            get { return m_RegionAViewModel; }
            set
            {
                if (Equals(value, m_RegionAViewModel))
                {
                    return;
                }
                m_RegionAViewModel = value;
                RaisePropertyChanged(() => RegionAViewModel);
            }
        }

        public BrakeDetailsViewModel BrakeDetails
        {
            get { return m_BrakeDetails; }
            set
            {
                if (Equals(value, m_BrakeDetails))
                {
                    return;
                }
                m_BrakeDetails = value;
                RaisePropertyChanged(() => BrakeDetails);
            }
        }

        public MenuViewModel Menu
        {
            get { return m_Menu; }
            set
            {
                if (Equals(value, m_Menu))
                {
                    return;
                }
                m_Menu = value;
                RaisePropertyChanged(() => Menu);
            }
        }

        public RegionCViewModel RegionC
        {
            get { return m_RegionC; }
            set
            {
                if (Equals(value, m_RegionC))
                {
                    return;
                }
                m_RegionC = value;
                RaisePropertyChanged(() => RegionC);
            }
        }

        public RegionEViewModel RegionE
        {
            get { return m_RegionE; }
            set
            {
                if (Equals(value, m_RegionE))
                {
                    return;
                }
                m_RegionE = value;
                RaisePropertyChanged(() => RegionE);
            }
        }

        public RegionMViewModel RegionM
        {
            get { return m_RegionM; }
            set
            {
                if (Equals(value, m_RegionM))
                {
                    return;
                }
                m_RegionM = value;
                RaisePropertyChanged(() => RegionM);
            }
        }

        public RegionTViewModel RegionT
        {
            get { return m_RegionT; }
            set
            {
                if (Equals(value, m_RegionT))
                {
                    return;
                }
                m_RegionT = value;
                RaisePropertyChanged(() => RegionT);
            }
        }

        public GlobalViewModel Global
        {
            get { return m_Global; }
            set
            {
                if (Equals(value, m_Global))
                {
                    return;
                }
                m_Global = value;
                RaisePropertyChanged(() => Global);
            }
        }

        public Visibility MMIBack
        {
            get { return m_MMIBack; }
            set
            {
                if (value == m_MMIBack)
                {
                    return;
                }
                m_MMIBack = value;
                RaisePropertyChanged(() => MMIBack);
            }
        }

        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            UpdateEmergencyDetails(dataChangedArgs);
            UpdateObcd(dataChangedArgs);
            UpdateC3(dataChangedArgs);
            UpdateC4(dataChangedArgs);
            UpdateC1(dataChangedArgs);
            UpdateC2(dataChangedArgs);
            UpdateM1(dataChangedArgs);
            UpdateM2(dataChangedArgs);
            UpdateM3(dataChangedArgs);
            UpdateM4(dataChangedArgs);
            UpdateM5(dataChangedArgs);
            UpdateM6(dataChangedArgs);
            UpdateM7(dataChangedArgs);
            UpdateM9(dataChangedArgs);
            UpdateM10(dataChangedArgs);
            UpdateBrake(dataChangedArgs);
            UpdateMMIBack(dataChangedArgs);
            UpdateMessage(dataChangedArgs);
            UpdateButtonStatus(dataChangedArgs);

            UpdateTargetBar(dataChangedArgs);
        }

        private void UpdateButtonStatus(CommunicationDataChangedArgs<bool> args)
        {
            foreach (var key in args.NewValue.Keys.Where(key => ButtonReactivationMgr.AllData.ContainsKey(key)))
            {
                if (args.NewValue[key])
                {
                    ButtonReactivationMgr.ChangStatus(key);
                }
                else
                {
                    ButtonReactivationMgr.ReActiva(key);
                }
            }
            if (args.NewValue.ContainsKey(120))
            {
                ReactionModel.ButtonReactivation = args.NewValue[120];
            }
        }

        private readonly Timer m_Timer;
        private void UpdateMMIBack(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.MMI屏黑屏];
            if (dataChangedArgs.NewValue.ContainsKey(idex0))
            {
                MMIBack = dataChangedArgs.NewValue[idex0] ? Visibility.Visible : Visibility.Hidden;
                if (dataChangedArgs.NewValue[idex0])
                {
                    RegionC.EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
                    {
                        Region = RegionNames.MainRoot,
                        Name = ControlNames.StartView
                    });
                    m_Timer.Change(GlobalParam.StartConfig.StartTime, int.MaxValue);

                }
                else
                {
                    RegionC.EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
                    {
                        Region = RegionNames.MainRoot,
                        Name = ControlNames.BlackSrceenView
                    });
                }
            }
            idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.进入屏保界面];
            if (dataChangedArgs.NewValue.ContainsKey(idex0))
            {
                if (dataChangedArgs.NewValue[idex0])
                {
                    ScreenSaverTimer.Change(5000, int.MaxValue);
                }
                else
                {
                    ScreenSaverTimer.Change(int.MaxValue, int.MaxValue);
                    RegionC.EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
                    {
                        Region = RegionNames.MainRoot,
                        Name = ControlNames.DomainControl
                    });
                }
            }

            idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.菜单点击无反应];
            if (dataChangedArgs.NewValue.ContainsKey(idex0))
            {
                Menu.CanDownMenu = dataChangedArgs.NewValue[idex0];
            }

        }

        private void UpdateBrake(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.请求制动];
            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.触发紧急制动];

            if (dataChangedArgs.NewValue.ContainsKey(idex0) ||
                dataChangedArgs.NewValue.ContainsKey(idex1)
                )
            {
                RegionAViewModel.BrakeDetails.Type = BrakeDetailsType.Initial;
                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionAViewModel.BrakeDetails.Type = BrakeDetailsType.BrakingRequired;
                }
                if (DataService.ReadService.GetBoolAt(idex1))
                {
                    RegionAViewModel.BrakeDetails.Type = BrakeDetailsType.EnmergencyBrake;
                }
            }
        }

        private void UpdateM9(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.ATP故障];
            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.ATO故障];
            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.无线通信中断];

            if (dataChangedArgs.NewValue.ContainsKey(idex0) ||
                dataChangedArgs.NewValue.ContainsKey(idex1) || dataChangedArgs.NewValue.ContainsKey(idex2)
                )
            {
                RegionM.ErrorModel = ErrorModel.None;
                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionM.ErrorModel = ErrorModel.ATP;
                }
                if (DataService.ReadService.GetBoolAt(idex1))
                {
                    RegionM.ErrorModel = ErrorModel.ATO;
                }
                if (DataService.ReadService.GetBoolAt(idex2))
                {
                    RegionM.ErrorModel = ErrorModel.RAD;
                }
            }
        }

        private void UpdateM10(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.进入车辆段];
            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.在车辆段内行驶];
            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.释放速度];

            if (dataChangedArgs.NewValue.ContainsKey(idex0) ||
                dataChangedArgs.NewValue.ContainsKey(idex1) || dataChangedArgs.NewValue.ContainsKey(idex2)
                )
            {
                RegionM.SpecialModel = SpecialModel.Initial;

                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionM.SpecialModel = SpecialModel.DepotEntry;
                }
                if (DataService.ReadService.GetBoolAt(idex1))
                {
                    RegionM.SpecialModel = SpecialModel.OnDepot;
                }
                if (DataService.ReadService.GetBoolAt(idex2))
                {
                    RegionM.SpecialModel = SpecialModel.ReleaseSpeed;
                }
            }
        }

        private void UpdateM1(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.AM模式];
            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.SM模式];
            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.RM模式];
            if (dataChangedArgs.NewValue.ContainsKey(idex2) || dataChangedArgs.NewValue.ContainsKey(idex0) ||
                dataChangedArgs.NewValue.ContainsKey(idex1))
            {
                RegionM.DriveModel = DriveModel.None;
                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionM.DriveModel = DriveModel.ATO;
                }
                if (DataService.ReadService.GetBoolAt(idex1))
                {
                    RegionM.DriveModel = DriveModel.Supervised;
                }
                if (DataService.ReadService.GetBoolAt(idex2))
                {
                    RegionM.DriveModel = DriveModel.Restricted;
                }
            }
        }

        private void UpdateM2(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.IXL等级];
            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.ITC等级];
            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.CTC等级];
            if (dataChangedArgs.NewValue.ContainsKey(idex2) || dataChangedArgs.NewValue.ContainsKey(idex0) ||
                dataChangedArgs.NewValue.ContainsKey(idex1))
            {
                RegionM.ActualLevels = ActualLevels.Initial;
                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionM.ActualLevels = ActualLevels.Interlocking;
                }
                if (DataService.ReadService.GetBoolAt(idex1))
                {
                    RegionM.ActualLevels = ActualLevels.Intermittent;
                }
                if (DataService.ReadService.GetBoolAt(idex2))
                {
                    RegionM.ActualLevels = ActualLevels.Continuous;
                }
            }
        }

        private void UpdateM3(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.ARoffered];
            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.ARactive];
            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.DTROisoffered];
            var idex3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.DTROactive];
            if (dataChangedArgs.NewValue.ContainsKey(idex2) || dataChangedArgs.NewValue.ContainsKey(idex0) ||
                dataChangedArgs.NewValue.ContainsKey(idex1) || dataChangedArgs.NewValue.ContainsKey(idex3))
            {
                RegionM.ReverseModel = ReverseModel.Initial;
                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionM.ReverseModel = ReverseModel.AROffered;
                }
                if (DataService.ReadService.GetBoolAt(idex1))
                {
                    RegionM.ReverseModel = ReverseModel.ARActive;
                }
                if (DataService.ReadService.GetBoolAt(idex2))
                {
                    RegionM.ReverseModel = ReverseModel.DTRO;
                }
                if (DataService.ReadService.GetBoolAt(idex3))
                {
                    RegionM.ReverseModel = ReverseModel.DTROactive;
                }
            }
        }

        private void UpdateM4(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.超出停车范围];
            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.在停车范围];

            if (dataChangedArgs.NewValue.ContainsKey(idex0) ||
                dataChangedArgs.NewValue.ContainsKey(idex1))
            {
                RegionM.StopModel = StopModel.Initial;
                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionM.StopModel = StopModel.Outside;
                }
                if (DataService.ReadService.GetBoolAt(idex1))
                {
                    RegionM.StopModel = StopModel.Inside;
                }
            }
        }

        private void UpdateM5(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.左侧车门打开];
            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.右侧车门打开];
            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.两侧车门同时打开];
            var idex3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.先开左门];
            var idex4 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.先开右门];
            var idex5 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.不再监控车门];
            var idex6 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.允许释放车门];
            var idex7 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.左侧车门允许打开];
            var idex8 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.右侧车门允许打开];
            var idex9 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.两侧车门允许同时打开];

            if (dataChangedArgs.NewValue.ContainsKey(idex0) ||
                dataChangedArgs.NewValue.ContainsKey(idex1) || dataChangedArgs.NewValue.ContainsKey(idex2) ||
                dataChangedArgs.NewValue.ContainsKey(idex3) || dataChangedArgs.NewValue.ContainsKey(idex4) ||
                dataChangedArgs.NewValue.ContainsKey(idex5) || dataChangedArgs.NewValue.ContainsKey(idex6) ||
                dataChangedArgs.NewValue.ContainsKey(idex7) || dataChangedArgs.NewValue.ContainsKey(idex8) ||
                dataChangedArgs.NewValue.ContainsKey(idex9))
            {
                RegionM.DoorRelease = DoorRelease.Initial;
                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionM.DoorRelease = DoorRelease.OpenLeft;
                }
                if (DataService.ReadService.GetBoolAt(idex1))
                {
                    RegionM.DoorRelease = DoorRelease.Openright;
                }
                if (DataService.ReadService.GetBoolAt(idex2))
                {
                    RegionM.DoorRelease = DoorRelease.OpenBoth;
                }
                if (DataService.ReadService.GetBoolAt(idex3))
                {
                    RegionM.DoorRelease = DoorRelease.OpenLeftfirst;
                }
                if (DataService.ReadService.GetBoolAt(idex4))
                {
                    RegionM.DoorRelease = DoorRelease.OpenRightfirst;
                }
                if (DataService.ReadService.GetBoolAt(idex5))
                {
                    RegionM.DoorRelease = DoorRelease.NoDoorSupervision;
                }
                if (DataService.ReadService.GetBoolAt(idex6))
                {
                    RegionM.DoorRelease = DoorRelease.PermissiveReleaseDoor;
                }
                if (DataService.ReadService.GetBoolAt(idex7))
                {
                    RegionM.DoorRelease = DoorRelease.PermitLeft;
                }
                if (DataService.ReadService.GetBoolAt(idex8))
                {
                    RegionM.DoorRelease = DoorRelease.PermitRight;
                }
                if (DataService.ReadService.GetBoolAt(idex9))
                {
                    RegionM.DoorRelease = DoorRelease.PermitDouble;
                }
            }
        }

        private void UpdateM6(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.关闭门命令];
            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.要求离站];
            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.扣车];
            var idex3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.跳停];

            if (dataChangedArgs.NewValue.ContainsKey(idex0) ||
                dataChangedArgs.NewValue.ContainsKey(idex1) || dataChangedArgs.NewValue.ContainsKey(idex2) ||
                dataChangedArgs.NewValue.ContainsKey(idex3))
            {
                RegionM.DepartureType = DepartureType.None;
                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionM.DepartureType = DepartureType.DoorCloseOrder;
                }
                if (DataService.ReadService.GetBoolAt(idex1))
                {
                    RegionM.DepartureType = DepartureType.DepartureRequest;
                }
                if (DataService.ReadService.GetBoolAt(idex2))
                {
                    RegionM.DepartureType = DepartureType.Hold;
                }
                if (DataService.ReadService.GetBoolAt(idex3))
                {
                    RegionM.DepartureType = DepartureType.Skip;
                }
            }
        }

        private void UpdateM7(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车门手动打开手动关闭];
            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车门自动打开手动关闭];
            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车门自动打开自动关闭];

            if (dataChangedArgs.NewValue.ContainsKey(idex0) ||
                dataChangedArgs.NewValue.ContainsKey(idex1) || dataChangedArgs.NewValue.ContainsKey(idex2))
            {
                RegionM.DoorModel = DoorModel.None;
                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionM.DoorModel = DoorModel.MM;
                }
                if (DataService.ReadService.GetBoolAt(idex1))
                {
                    RegionM.DoorModel = DoorModel.AM;
                }
                if (DataService.ReadService.GetBoolAt(idex2))
                {
                    RegionM.DoorModel = DoorModel.AA;
                }
            }
        }

        private void UpdateC3(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.列车失去完整性];
            if (dataChangedArgs.NewValue.ContainsKey(idex0))
            {
                RegionC.TrainInteGrityC3 = TrainInteGrity.Initial;
                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionC.TrainInteGrityC3 = TrainInteGrity.TrainIntegrity;
                }
            }
        }

        private void UpdateC4(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.制动缸压力不正常];
            if (dataChangedArgs.NewValue.ContainsKey(idex0))
            {
                RegionC.TrainInteGrityC4 = TrainInteGrity.Initial;
                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionC.TrainInteGrityC4 = TrainInteGrity.BrakingPressure;
                }
            }
        }

        private void UpdateC2(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.最高模式RM];
            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.最高模式SM_I];
            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.最高模式SM_C];
            var idex3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.最高模式AM_I];
            var idex4 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.最高模式AM_C];
            var idex5 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.预选模式闪烁];
            if (dataChangedArgs.NewValue.ContainsKey(idex5))
            {
                RegionC.Visibile = dataChangedArgs.NewValue[idex5];
            }
            if (dataChangedArgs.NewValue.ContainsKey(idex2) || dataChangedArgs.NewValue.ContainsKey(idex0) ||
                dataChangedArgs.NewValue.ContainsKey(idex1) || dataChangedArgs.NewValue.ContainsKey(idex3) ||
                dataChangedArgs.NewValue.ContainsKey(idex4))
            {
                RegionC.MaximumMode = MaximumMode.Initial;
                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionC.MaximumMode = MaximumMode.RestrictedMode;
                }
                if (DataService.ReadService.GetBoolAt(idex1))
                {
                    RegionC.MaximumMode = MaximumMode.SMIntermittent;
                }
                if (DataService.ReadService.GetBoolAt(idex2))
                {
                    RegionC.MaximumMode = MaximumMode.SMContinuous;
                }
                if (DataService.ReadService.GetBoolAt(idex3))
                {
                    RegionC.MaximumMode = MaximumMode.AMIntermittent;
                }
                if (DataService.ReadService.GetBoolAt(idex4))
                {
                    RegionC.MaximumMode = MaximumMode.AMContinuous;
                }
            }
        }

        private void UpdateC1(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车辆正在牵引];
            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车辆为惰行];
            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车辆正在制动];

            if (dataChangedArgs.NewValue.ContainsKey(idex2) || dataChangedArgs.NewValue.ContainsKey(idex0) ||
                dataChangedArgs.NewValue.ContainsKey(idex1))
            {
                RegionC.DriveingBrake = DriveingBrakeType.Initial;
                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionC.DriveingBrake = DriveingBrakeType.Motoring;
                }
                if (DataService.ReadService.GetBoolAt(idex1))
                {
                    RegionC.DriveingBrake = DriveingBrakeType.Coasting;
                }
                if (DataService.ReadService.GetBoolAt(idex2))
                {
                    RegionC.DriveingBrake = DriveingBrakeType.Braking;
                }
            }
        }

        private void UpdateObcd(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车首OBCU激活绿色];
            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车首OBCU待机白色];
            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车首OBCU关闭红色];

            if (dataChangedArgs.NewValue.ContainsKey(idex2) || dataChangedArgs.NewValue.ContainsKey(idex0) ||
                dataChangedArgs.NewValue.ContainsKey(idex1))
            {
                RegionC.HeadOBCU = OBCUModel.None;
                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionC.HeadOBCU = OBCUModel.Level3;
                }
                if (DataService.ReadService.GetBoolAt(idex1))
                {
                    RegionC.HeadOBCU = OBCUModel.Level1;
                }
                if (DataService.ReadService.GetBoolAt(idex2))
                {
                    RegionC.HeadOBCU = OBCUModel.Level2;
                }
            }

            idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车尾OBCU激活绿色];
            idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车尾OBCU待机白色];
            idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车尾OBCU关闭红色];

            if (dataChangedArgs.NewValue.ContainsKey(idex2) || dataChangedArgs.NewValue.ContainsKey(idex0) ||
                dataChangedArgs.NewValue.ContainsKey(idex1))
            {
                RegionC.TailOBCU = OBCUModel.None;
                if (DataService.ReadService.GetBoolAt(idex0))
                {
                    RegionC.TailOBCU = OBCUModel.Level3;
                }
                if (DataService.ReadService.GetBoolAt(idex1))
                {
                    RegionC.TailOBCU = OBCUModel.Level1;
                }
                if (DataService.ReadService.GetBoolAt(idex2))
                {
                    RegionC.TailOBCU = OBCUModel.Level2;
                }
            }
        }

        private void UpdateEmergencyDetails(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var idx0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车辆空转打滑];
            var idx1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车辆施加EB];
            var idx2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.PSD未关闭];

            if (dataChangedArgs.NewValue.ContainsKey(idx0) || dataChangedArgs.NewValue.ContainsKey(idx1) ||
                dataChangedArgs.NewValue.ContainsKey(idx2))
            {
                RegionM.EmergencyModel = EmergencyModel.None;
                if (DataService.ReadService.GetBoolAt(idx0))
                {
                    RegionM.EmergencyModel = EmergencyModel.Slip;
                }
                if (DataService.ReadService.GetBoolAt(idx1))
                {
                    RegionM.EmergencyModel = EmergencyModel.EmergencyBrake;
                }
                if (DataService.ReadService.GetBoolAt(idx2))
                {
                    RegionM.EmergencyModel = EmergencyModel.PSDNotCLose;
                }
            }
        }

        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            UpdateA2Recgion(dataChangedArgs);
            // ((SpeedModel)(Speed.TargetSpeed)).Value = GetIfContains(dataChangedArgs, InFloatKeys.到目标点的距离, (float)Speed.TargetSpeed.Value);

            UpdateSpeed(dataChangedArgs);

            UpdateTReion(dataChangedArgs);
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
        }

        private void UpdateMessage(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            foreach (var key in dataChangedArgs.NewValue.Keys)
            {
                if (ACKManage.AllInfo.ContainsKey(key))
                {
                    if (dataChangedArgs.NewValue[key])
                    {
                        if (!ACKManage.HasInfo(key))
                        {
                            ACKManage.Add(key);
                        }
                    }
                    else
                    {
                        if (ACKManage.HasInfo(key))
                        {
                            ACKManage.Remove(key);
                        }
                    }
                }
            }
            var index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.信息确认];
            if (dataChangedArgs.NewValue.ContainsKey(index) && dataChangedArgs.NewValue[index])
            {
                if (ACKManage.GetCurrentInfo() != null)
                {
                    ACKManage.Affirm(ACKManage.GetCurrentInfo());
                }
            }
            if (ACKManage.CurrentInfo.Count != 0)
            {
                var tmp = ACKManage.GetCurrentInfo();
                RegionFViewModel.ChinaInfo = tmp.ChinaInfo;
                RegionFViewModel.EnglishInfo = tmp.EnglishInfo;
                RegionFViewModel.InfoLevl = (InfoLevl)tmp.InfoLevel;
                RegionFViewModel.ChinaFontSize = tmp.ChinaFontSize;
                RegionFViewModel.EnglishFontSize = tmp.EnglishFontSize;
            }
            else
            {
                RegionFViewModel.ChinaInfo = default(string);
                RegionFViewModel.EnglishInfo = default(string);
            }
        }

        private void UpdateA2Recgion(CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            RegionAViewModel.TagertNum = GetIfContains(dataChangedArgs, InFloatKeys.到目标点的距离,
                (float)RegionAViewModel.TagertNum);

            RegionAViewModel.TagertSpeed = GetIfContains(dataChangedArgs, InFloatKeys.目标点速度,
                (float)RegionAViewModel.TagertSpeed);
        }
        public char[] ConvertFloatArray(float[] data)
        {
            return data.SelectMany(BitConverter.GetBytes).Select(s => BitConverter.ToChar(new byte[] { s, 0 }, 0)).ToArray();
        }

        public float[] ConvertCharArray(char[] data)
        {
            var ba = data.Select(s => BitConverter.GetBytes(s)[0]).ToArray();
            var rlt = new List<float>(ba.Length / 4);
            for (int i = 0; i < ba.Length; i += 4)
            {
                rlt.Add(BitConverter.ToSingle(ba, i));
            }
            return rlt.ToArray();
        }
        private void UpdateTReion(CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            var key = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.车次号0];
            var ke1 = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.车次号1];

            if (dataChangedArgs.NewValue.Keys.Any(a => a == ke1 || a == key))
            {

                var tmp = new float[] { DataService.ReadService.GetFloatAt(key), DataService.ReadService.GetFloatAt(ke1) };

                var ds = tmp.SelectMany(BitConverter.GetBytes).Select(s => BitConverter.ToChar(new byte[] { s, 0 }, 0)).Where(w => !w.Equals('\0')).ToArray();

                RegionT.TripNumber = new string(ds);
            }


            //  RegionT.TripNumber = GetIfContains(dataChangedArgs, InFloatKeys.服务号, (float)RegionT.TripNumber);
            RegionT.DestinationNumber = GetIfContains(dataChangedArgs, InFloatKeys.目的地号,
                (float)RegionT.DestinationNumber);
            RegionT.CrewNumber = GetIfContains(dataChangedArgs, InFloatKeys.工号, (float)RegionT.CrewNumber);
        }

        private void UpdateSpeed(CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            RegionBViewModel.CurrentSpeed = GetIfContains(dataChangedArgs, InFloatKeys.列车当前速度,
                (float)RegionBViewModel.CurrentSpeed);
            RegionBViewModel.TagertSpeed = GetIfContains(dataChangedArgs, InFloatKeys.ATP推荐速度,
                (float)RegionBViewModel.TagertSpeed);
            RegionBViewModel.EmergencySpeed = GetIfContains(dataChangedArgs, InFloatKeys.紧急制动触发速度,
                (float)RegionBViewModel.EmergencySpeed);
        }

        public IViewContent MainView { get; set; }
        public IViewContent DoMainView { get; set; }

        private void UpdateTargetBar(CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            dataChangedArgs.UpdateIfContains(
                IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.目标距离刻度条淡绿色], b =>
                {
                    if (b)
                    {
                        RegionAViewModel.TargetBarType = TargetBarType.LightGreen;
                    }
                });

            dataChangedArgs.UpdateIfContains(
                IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.目标距离刻度条黄色], b =>
                {
                    if (b)
                    {
                        RegionAViewModel.TargetBarType = TargetBarType.Yellow;
                    }
                });

            dataChangedArgs.UpdateIfContains(
                IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.目标距离刻度条红色], b =>
                {
                    if (b)
                    {
                        RegionAViewModel.TargetBarType = TargetBarType.Red;
                    }
                });
            dataChangedArgs.UpdateIfContains(IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.目标距离条不显示],
                b =>
                {
                    RegionAViewModel.TagertNumVisibility = b ? Visibility.Hidden : Visibility.Visible;
                });
            dataChangedArgs.UpdateIfContains(IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.目标速度不显示],
               b =>
               {
                   RegionAViewModel.TagertSpeedVisibility = b ? Visibility.Hidden : Visibility.Visible;
               });
            dataChangedArgs.UpdateIfContains(IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.速度指针不显示],
                b =>
                {
                    RegionBViewModel.PointerVisibility = b ? Visibility.Hidden : Visibility.Visible;
                });
            dataChangedArgs.UpdateIfContains(IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.红标不显示],
               b =>
               {
                   RegionBViewModel.RedFlagVisibility = b ? Visibility.Hidden : Visibility.Visible;
               });
            dataChangedArgs.UpdateIfContains(IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.黄标不显示],
               b =>
               {
                   RegionBViewModel.YellowFlagVisibility = b ? Visibility.Hidden : Visibility.Visible;
               });
        }

        private float GetIfContains(CommunicationDataChangedArgs<float> dataChangedArgs, string key, float defaultValue)
        {
            if (dataChangedArgs.NewValue.ContainsKey(IndexConfigure.Instance.IndexFacade.InFloatDictionary[key]))
            {
                return dataChangedArgs.NewValue[IndexConfigure.Instance.IndexFacade.InFloatDictionary[key]];
            }
            return defaultValue;
        }
    }
}
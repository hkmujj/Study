using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.NRIET.Constant;
using Tram.CBTC.NRIET.Controller.Navigator.Maintence;
using Tram.CBTC.NRIET.Controller.Navigator.Running;
using Tram.CBTC.NRIET.View.Contents;
using Tram.CBTC.NRIET.View.Contents.Main;
using Tram.CBTC.NRIET.View.Contents.Main.RegionDeviceStatus;
using Tram.CBTC.NRIET.View.Contents.Main.RegionRunning;
using Tram.CBTC.NRIET.View.Contents.Main.RegionSpeed;

//using Tram.CBTC.Nriet.View.Layout;
//using Tram.CBTC.Nriet.View.Maintence;
//using Tram.CBTC.Nriet.View.Running;

namespace Tram.CBTC.NRIET.Controller.Navigator
{
    /// <summary>
    /// 因为CBTC的界面跳转没有动车复杂 ，所以用了一个类来处理
    /// </summary>
    [Export]
    public class NavigatorController : NotificationObject
    {
        private DomainController m_DomainController;

        private readonly IRegionManager m_RegionManager;

        private readonly IEventAggregator m_EventAggregator;

        public RunningNavigatorController RunningNavigator { get; private set; }

        public MaintenceNavigatorController MaintenceNavigator { get; private set; }


        private ICommand m_ToMainViewCommand;

        private ICommand m_ToStartViewCommand;

        private ICommand m_ToTrainNoActiveViewCommand;

        private ICommand m_TrainTimeTableMenuItemCommand;

        private ICommand m_SettingMenuItemCommand;

        private ICommand m_MaintenMenuItemCommand;

        private bool m_bOpenSettingMenu = false;

        private ICommand m_SettingMenuCommand;


        private ICommand m_RoadCtrlMenuCommand;

        private bool m_bOpenTrainTimeTableMenuItem = false;

        private bool m_bOpenSettingViewMenuItem = false;

        private bool m_bOpenMaintenViewMenuItem = false;


        private bool m_bOpenRoadCtrlViewMenuItem = false;


        /// <summary>
        /// 到主界面
        /// </summary>
        public ICommand ToMainViewCommand
        {

            get { return m_ToMainViewCommand; }
            private set
            {
                if (value.Equals(m_ToMainViewCommand))
                {
                    return;
                }

                m_ToMainViewCommand = value;

                RaisePropertyChanged(() => ToMainViewCommand);
            }
        }


        /// <summary>
        /// 到开始界面
        /// </summary>
        public ICommand ToStartViewCommand
        { 
            get { return m_ToStartViewCommand; }
            private set
            {
                if (value.Equals(m_ToStartViewCommand))
                {
                    return;
                }

                m_ToStartViewCommand = value;

                RaisePropertyChanged(() => ToStartViewCommand);
            }
        }



        /// <summary>
        /// 到本端/远端激活界面
        /// </summary>
        public ICommand ToTrainNoActiveViewCommand
        {
            get { return m_ToTrainNoActiveViewCommand; }
            private set
            {
                if (value.Equals(m_ToTrainNoActiveViewCommand))
                {
                    return;
                }

                m_ToTrainNoActiveViewCommand = value;

                RaisePropertyChanged(() => ToTrainNoActiveViewCommand);
            }
        }


        /// <summary>
        /// 时刻表菜单项
        /// </summary>
        public ICommand TrainTimeTableMenuItemCommand
        {

            get { return m_TrainTimeTableMenuItemCommand; }
            private set
            {
                if (value.Equals(m_TrainTimeTableMenuItemCommand))
                {
                    return;
                }

                m_TrainTimeTableMenuItemCommand = value;

                RaisePropertyChanged(() => TrainTimeTableMenuItemCommand);
            }
        }

        /// <summary>
        /// 设置菜单项
        /// </summary>
        public ICommand SettingMenuItemCommand
        {

            get { return m_SettingMenuItemCommand; }
            private set
            {
                if (value.Equals(m_SettingMenuItemCommand))
                {
                    return;
                }

                m_SettingMenuItemCommand = value;

                RaisePropertyChanged(() => SettingMenuItemCommand);
            }
        }


        /// <summary>
        /// 维护菜单项
        /// </summary>
        public ICommand MaintenMenuItemCommand
        {

            get { return m_MaintenMenuItemCommand; }
            private set
            {
                if (value.Equals(m_MaintenMenuItemCommand))
                {
                    return;
                }

                m_MaintenMenuItemCommand = value;

                RaisePropertyChanged(() => MaintenMenuItemCommand);
            }
        }


        /// <summary>
        /// 进路控制菜单项
        /// </summary>
        public ICommand RoadCtrlMenuCommand
        {

            get { return m_RoadCtrlMenuCommand; }
            private set
            {
                if (value.Equals(m_RoadCtrlMenuCommand))
                {
                    return;
                }

                m_RoadCtrlMenuCommand = value;

                RaisePropertyChanged(() => RoadCtrlMenuCommand);
            }
        }

        /// <summary>
        /// 是否打开设置菜单
        /// </summary>
        public bool IsOpenSettingMenu
        {
            get { return m_bOpenSettingMenu; }
            set
            {
                if (value == m_bOpenSettingMenu)
                {
                    return;
                }

                m_bOpenSettingMenu = value;

                RaisePropertyChanged(()=> IsOpenSettingMenu);
            }
        }

        /// <summary>
        /// 设置菜单
        /// </summary>
        public ICommand SettingMenuCommand
        {

            get { return　m_SettingMenuCommand; }
            private set
            {
                if (value.Equals(m_SettingMenuCommand))
                {
                    return;
                }

                m_SettingMenuCommand = value;

                RaisePropertyChanged(() => SettingMenuCommand);
            }
        }



        /// <summary>
        /// 是否打开时刻表菜单项
        /// </summary>
        public bool IsOpenTrainTimeTableMenuItem
        {
            get { return m_bOpenTrainTimeTableMenuItem; }
            set
            {
                if (value == m_bOpenTrainTimeTableMenuItem)
                {
                    return;
                }

                m_bOpenTrainTimeTableMenuItem = value;

                RaisePropertyChanged(() => IsOpenTrainTimeTableMenuItem);
            }
        }


        /// <summary>
        /// 是否打开设置菜单项
        /// </summary>
        public bool IsOpenSettingViewMenuItem
        {
            get { return m_bOpenSettingViewMenuItem; }
            set
            {
                if (value == m_bOpenSettingViewMenuItem)
                {
                    return;
                }

                m_bOpenSettingViewMenuItem = value;

                RaisePropertyChanged(() => IsOpenSettingViewMenuItem);
            }
        }


        /// <summary>
        /// 是否打开维护菜单项
        /// </summary>
        public bool IsOpenMaintenViewMenuItem
        {
            get { return m_bOpenMaintenViewMenuItem; }
            set
            {
                if (value == m_bOpenMaintenViewMenuItem)
                {
                    return;
                }

                m_bOpenMaintenViewMenuItem = value;

                RaisePropertyChanged(() => IsOpenMaintenViewMenuItem);
            }
        }


        /// <summary>
        /// 是否打开进路控制菜单项
        /// </summary>
        public bool IsOpenRoadCtrlViewMenuItem
        {
            get { return m_bOpenRoadCtrlViewMenuItem; }
            set
            {
                if (value == m_bOpenRoadCtrlViewMenuItem)
                {
                    return;
                }

                m_bOpenRoadCtrlViewMenuItem = value;

                if (m_bOpenRoadCtrlViewMenuItem)
                {
                    m_RegionManager.RequestNavigate(RegionNames.DeviceStatusContent, typeof(DeviceStatusViewHide).FullName);
                    m_RegionManager.RequestNavigate(RegionNames.SpeedContent, typeof(SpeedViewHide).FullName);
                    m_RegionManager.RequestNavigate(RegionNames.RunningContent, typeof(RunningViewHide).FullName);
                }
                else
                {
                    m_RegionManager.RequestNavigate(RegionNames.DeviceStatusContent, typeof(DeviceStatusView).FullName);
                    m_RegionManager.RequestNavigate(RegionNames.SpeedContent, typeof(SpeedView).FullName);
                    m_RegionManager.RequestNavigate(RegionNames.RunningContent, typeof(RunningView).FullName);
                }

                RaisePropertyChanged(() => IsOpenRoadCtrlViewMenuItem);
            }
        }


        [ImportingConstructor]
        [DebuggerStepThrough]
        public NavigatorController(IRegionManager regionManager, RunningNavigatorController runningNavigator, MaintenceNavigatorController maintenceNavigator, IEventAggregator eventAggregator)
        {
            m_RegionManager = regionManager;
            RunningNavigator = runningNavigator;
            MaintenceNavigator = maintenceNavigator;
            m_EventAggregator = eventAggregator;
        }

        public void Initalize(DomainController domainController)
        {
            RunningNavigator.Initalize();
            MaintenceNavigator.Initalize();

            ToMainViewCommand = new DelegateCommand(OnToMainView);
            ToStartViewCommand = new DelegateCommand(OnToStartView);
            ToTrainNoActiveViewCommand = new DelegateCommand(OnToTrainNoActiveView);
            SettingMenuItemCommand = new DelegateCommand(SettingMenuItem);
            MaintenMenuItemCommand = new DelegateCommand(MaintenMenuItem);
            SettingMenuCommand = new DelegateCommand<string>(SettingMenu);
            TrainTimeTableMenuItemCommand = new DelegateCommand(TrainTimeTableMenuItem);
            RoadCtrlMenuCommand = new DelegateCommand(RoadCtrlMenuItem);

            //ToRunningViewCommand = new DelegateCommand(OnToRunningView);
            //ToMaintenceViewCommand = new DelegateCommand(OnToMaintenceView, () => false);

            //m_EventAggregator.GetEvent<PowerStateChangedEvent>().Subscribe(ar => OnToMainView(), ThreadOption.UIThread);

            //OnToMainView();
        }

        private void OnToMainView()
        {
            IsOpenSettingMenu = false;

            m_RegionManager.RequestNavigate(RegionNames.ShellContent, typeof(MainView).FullName);
        }

        private void OnToStartView()
        {
            IsOpenSettingMenu = false;

            m_RegionManager.RequestNavigate(RegionNames.ShellContent, typeof(StartView).FullName);
        }

        private void OnToTrainNoActiveView()
        {
            IsOpenSettingMenu = false;

            m_RegionManager.RequestNavigate(RegionNames.ShellContent, typeof(TrainNoActiveView).FullName);
        }

        private void TrainTimeTableMenuItem()
        {
            IsOpenSettingMenu = false;

            IsOpenTrainTimeTableMenuItem = true;
        }

        private void SettingMenuItem()
        {
            IsOpenSettingMenu = false;

            IsOpenSettingViewMenuItem = true;
        }

        private void MaintenMenuItem()
        {
            IsOpenSettingMenu = false;

            IsOpenMaintenViewMenuItem = true;
        }

        private void SettingMenu(string strOpen)
        {
            IsOpenSettingMenu = strOpen == "1" ? true : false;
        }

        private void RoadCtrlMenuItem()
        {
            IsOpenSettingMenu = false;

            IsOpenRoadCtrlViewMenuItem = !IsOpenRoadCtrlViewMenuItem;
        }

    }
}
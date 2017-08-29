using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using CBTC.Infrasturcture.Events;
using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Model.Send;
using CBTC.Infrasturcture.Model.Test;
using CommonUtil.Util;
using DevExpress.Utils.Filtering.Internal;
using DevExpress.Xpf.Core.Native;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Extension;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.CBTC.BeiJiaoKong.Config;
using Subway.CBTC.BeiJiaoKong.Events;
using Subway.CBTC.BeiJiaoKong.Extentions;
using Subway.CBTC.BeiJiaoKong.Models.Domain;
using Subway.CBTC.BeiJiaoKong.ViewModel;
using Subway.CBTC.BeiJiaoKong.Views.Root;
using Subway.CBTC.BeiJiaoKong.Models;
using Subway.CBTC.BeiJiaoKong.Views.Shell;
using Subway.CBTC.BeiJiaoKong.Views.Shell.RegionE;
using Subway.CBTC.BeiJiaoKong.Views.Shell.RegionG;
using Subway.CBTC.BeiJiaoKong.Views.Shell.RegionM;

namespace Subway.CBTC.BeiJiaoKong.Controller
{
    /// <summary>
    /// 创 建 者：谭栋炜
    /// 创建时间：2017/1/16
    /// 修 改 者：谭栋炜
    /// 修改时间：2017/1/16
    /// </summary>
    [Export]
    public sealed class BeiJiaoKongController : ControllerBase<BeiJiaoKongViewModel>
    {
        public DevExpress.Mvvm.DelegateCommand<CommandParameter> LoadedCommand { get; private set; }
        private Timer ScreenSaverTimer;
        private System.Timers.Timer Timers_Timer;

        /// <summary>
        /// 构造函数
        /// </summary>
        public BeiJiaoKongController()
        {
            m_EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            m_RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            Navigator = new DevExpress.Mvvm.DelegateCommand<NavigatorEventArgs>(NavigatorMethod);
            m_EventAggregator.GetEvent<NavigatorEvent>().Subscribe(NavigatorMethod, ThreadOption.UIThread);

            LoadedCommand = new DevExpress.Mvvm.DelegateCommand<CommandParameter>(OnLoaded);
            m_EventAggregator.GetEvent<PowerStateChangedEvent>().Subscribe(OnPowerChanged, ThreadOption.UIThread);

            ButtonDownCommand = new DevExpress.Mvvm.DelegateCommand<string>(OnButtonDown);

            Timers_Timer = new System.Timers.Timer(1000);
            Timers_Timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimers_Timer);
            Timers_Timer.Enabled = true;

            ScreenSaverTimer = new Timer(OnTimer);
            ToScreenSaverCommand = new DelegateCommand(OnToScreenSaver);
        }

        void OnTimers_Timer(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (ViewModel.ScreenSaverDelay == 0)
            {
                return;
            }
            if (ViewModel.IsScreenSaver && ViewModel.CBTC.SignalInfo.Speed.CurrentSpeed.Value > 0.1)
            {
                ScreenSaverTimer.Change(ViewModel.ScreenSaverDelay, int.MaxValue);
            }
            else if (!ViewModel.IsScreenSaver && ViewModel.CBTC.SignalInfo.Speed.CurrentSpeed.Value > 0.1)
            {
                if (App.Current.GetMainDispatcher() != null)
                {
                    App.Current.GetMainDispatcher().BeginInvoke(new Action(delegate { ViewModel.IsScreenSaver = true; }));
                }
            }
        }  

        private void OnTimer(object state)
        {
            if (ViewModel.ScreenSaverDelay == 0)
            {
                return;
            }
            if (App.Current.GetMainDispatcher() != null)
            {
                App.Current.GetMainDispatcher().BeginInvoke(new Action(delegate { ViewModel.IsScreenSaver = false; }));
            }
        }
        
        private void OnToScreenSaver()
        {
            if (ViewModel.ScreenSaverDelay == 0)
            {
                return;
            }
            if (!ViewModel.IsScreenSaver)
            {
                ViewModel.IsScreenSaver = true;
            }
            ScreenSaverTimer.Change(ViewModel.ScreenSaverDelay, int.MaxValue);
        }

        private void OnButtonDown(string obj)
        {
            ISendInterface SendData = BeiJiaoKongViewModel.Domain.SendInterface;
            TestInfo testInfo = BeiJiaoKongViewModel.Domain.TestInfo;
            switch (obj)
            {
                case "试闸":
                    SendData.InputBrakeTestSwitch(new SendModel<bool>(testInfo.ButtonDownBrakeTest));
                    break;
                case "缓解":
                    SendData.InputBrakeTestRelease(new SendModel<bool>(testInfo.ButtonDownRemit));
                    break;
                case "广播\r测试":
                    SendData.InputBroadcastTest(new SendModel<bool>(testInfo.ButtonDownBroadcastTest));
                    break;
                case "点灯\r测试":
                    SendData.InputLampTest(new SendModel<bool>(testInfo.ButtonDownLightTest));
                    break;
            }
            if (GlobalParams.Instance.InitParam != null)
            {
                GlobalParams.Instance.InitParam.CommunicationDataService.ReadService.RaiseAllDataChanged();
            }
        }

        private void OnPowerChanged(PowerStateChangedEvent.Args obj)
        {
            if (obj.CurrentState == PowerState.Started)
            {
                //设置初始定时器时间
                ScreenSaverTimer.Change(ViewModel.ScreenSaverDelay, int.MaxValue);
                //设置屏保状态
                ViewModel.IsScreenSaver = true;

                if (ViewModel.TCTType == TCTType.ShenZhen)
                {
                    Navigator.Execute(new NavigatorEventArgs() { ViewType = typeof(RegionGView), ViewName = typeof(RegionGView).FullName });
                    Navigator.Execute(new NavigatorEventArgs() { ViewType = typeof(RegionEView), ViewName = typeof(RegionEView).FullName });
                    Navigator.Execute(new NavigatorEventArgs() { ViewType = typeof(RegionM7View), ViewName = typeof(RegionM7View).FullName });
                }
                else if (ViewModel.TCTType == TCTType.GuiYang)
                {
                    Navigator.Execute(new NavigatorEventArgs() { ViewType = typeof(RegionEGuiYangView), ViewName = typeof(RegionEGuiYangView).FullName });
                    Navigator.Execute(new NavigatorEventArgs() { ViewType = typeof(RegionGGuiYangView), ViewName = typeof(RegionGGuiYangView).FullName });
                    Navigator.Execute(new NavigatorEventArgs() { ViewType = typeof(RegionM7GuiYangView), ViewName = typeof(RegionM7GuiYangView).FullName });
                }

                if (ViewModel.CBTC.TrainInfo.ScreenSaverEnable)
                {
                    Navigator.Execute(new NavigatorEventArgs() { ViewType = typeof(ScreenSaver), ViewName = typeof(ScreenSaver).FullName });
                }
                else
                {
                    //m_RegionManager.RequestNavigate(RegionNames.RootRegion, typeof(MainView).FullName);
                    Navigator.Execute(new NavigatorEventArgs() { ViewType = typeof(MainView), ViewName = typeof(MainView).FullName });
                }
            }
        }


        public override void Initalize()
        {
            //ViewModel.CBTC.TrainInfo.ScreenSaverEnableChangedEvent += TrainInfo_ScreenSaverEnableChangedEvent;

            m_EventAggregator.GetEvent<ScreenSaverEnableChangedEvent>().Subscribe(TrainInfo_ScreenSaverEnableChangedEvent, ThreadOption.UIThread);
            InitalizePro();

            //设置初始定时器时间
            ScreenSaverTimer.Change(ViewModel.ScreenSaverDelay, int.MaxValue);
            //设置屏保状态
            ViewModel.IsScreenSaver = true;
        }

        private void TrainInfo_ScreenSaverEnableChangedEvent(ScreenSaverEnableChangedEvent.Args obj)
        {
            if (ViewModel.CBTC.TrainInfo.PowerState != PowerState.Stopped && obj.CurrentState)
            {
                Navigator.Execute(new NavigatorEventArgs() { ViewType = typeof(ScreenSaver), ViewName = typeof(ScreenSaver).FullName });
            }
            else
            {
                Navigator.Execute(new NavigatorEventArgs() { ViewType = typeof(MainView), ViewName = typeof(MainView).FullName });
            }
        }

        private void InitalizePro()
        {
            if (GlobalParams.Instance.InitParam != null)
            {
                var config = DataSerialization.DeserializeFromXmlFile<BeiJiaoKongProjectConfig>(
                      Path.Combine(GlobalParams.Instance.InitParam.AppConfig.AppPaths.ConfigDirectory,
                          BeiJiaoKongProjectConfig.FileName));
                ViewModel.TCTType = config.Type;
                ViewModel.ScreenSaverDelay = config.ScreenSaverDelay;
            }
            else
            {
                var config =
                    DataSerialization.DeserializeFromXmlFile<BeiJiaoKongProjectConfig>(
                        Path.Combine(".\\Subway.CBTC.BeiJiaoKong\\config\\", BeiJiaoKongProjectConfig.FileName));
                ViewModel.TCTType = config.Type;
                ViewModel.ScreenSaverDelay = config.ScreenSaverDelay;
            }

        }

        private void NavigatorMethod(NavigatorEventArgs args)
        {
            if (string.IsNullOrEmpty(args.ViewName))
            {
                return;
            }
            var region =
                Type.GetType(args.ViewName).GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault()
                    as ViewExportAttribute;
            if (region == null)
            {
                return;
            }
            m_RegionManager.RequestNavigate(region.RegionName, args.ViewName);
        }

        private void OnLoaded(CommandParameter obj)
        {

        }

        /// <summary>
        /// 导航
        /// </summary>
        public ICommand Navigator { get; private set; }
        /// <summary>
        /// 事件聚合器
        /// </summary>

        private readonly IEventAggregator m_EventAggregator;
        /// <summary>
        /// 区域管理器
        /// </summary>
        private readonly IRegionManager m_RegionManager;

        /// <summary>
        /// 按钮按下发送数据
        /// </summary>
        public ICommand ButtonDownCommand { get; private set; }
        
        /// <summary>
        /// 其余界面点击命令
        /// </summary>
        public ICommand ToScreenSaverCommand { get; private set; }
    }
}
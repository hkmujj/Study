using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Input;
using CBTC.Infrasturcture.Events;
using CBTC.Infrasturcture.Model.Constant;
using CommonUtil.Util;
using DevExpress.Mvvm;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.CBTC.BeiJiaoKong.Config;
using Subway.CBTC.BeiJiaoKong.Events;
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
    public class BeiJiaoKongController : ControllerBase<BeiJiaoKongViewModel>
    {
        public DelegateCommand<CommandParameter> LoadedCommand { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BeiJiaoKongController()
        {
            m_EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            m_RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            Navigator = new DelegateCommand<NavigatorEventArgs>(NavigatorMethod);
            m_EventAggregator.GetEvent<NavigatorEvent>().Subscribe(NavigatorMethod, ThreadOption.UIThread);

            LoadedCommand = new DelegateCommand<CommandParameter>(OnLoaded);
            m_EventAggregator.GetEvent<PowerStateChangedEvent>().Subscribe(OnPowerChanged, ThreadOption.UIThread);
        }

        private void OnPowerChanged(PowerStateChangedEvent.Args obj)
        {
            if (obj.CurrentState == PowerState.Started)
            {
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
            }
        }

        public override void Initalize()
        {
            ViewModel.CBTC.TrainInfo.ScreenSaverEnableChangedEvent += TrainInfo_ScreenSaverEnableChangedEvent;

            InitalizePro();
        }

        private void TrainInfo_ScreenSaverEnableChangedEvent(bool obj)
        {
            if (ViewModel.CBTC.TrainInfo.PowerState != PowerState.Stopped && obj)
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
            }
            else
            {
                var config =
                    DataSerialization.DeserializeFromXmlFile<BeiJiaoKongProjectConfig>(
                        Path.Combine(".\\Subway.CBTC.BeiJiaoKong\\config\\", BeiJiaoKongProjectConfig.FileName));
                ViewModel.TCTType = config.Type;
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
    }
}
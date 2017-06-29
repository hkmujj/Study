using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Input;
using CBTC.Infrasturcture.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Subway.CBTC.QuanLuTong.Constant;
using Subway.CBTC.QuanLuTong.Controller.Navigator.Maintence;
using Subway.CBTC.QuanLuTong.Controller.Navigator.Running;
using Subway.CBTC.QuanLuTong.View.Layout;
using Subway.CBTC.QuanLuTong.View.Maintence;
using Subway.CBTC.QuanLuTong.View.Running;

namespace Subway.CBTC.QuanLuTong.Controller.Navigator
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

        /// <summary>
        /// 到主界面
        /// </summary>
        public ICommand ToMainViewCommand { get; private set; }

        /// <summary>
        /// 运行主界面
        /// </summary>
        public ICommand ToRunningViewCommand { get; private set; }

        /// <summary>
        /// 维护中界面
        /// </summary>
        public ICommand ToMaintenceViewCommand { get; private set; }



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
            m_DomainController = domainController;

            ToMainViewCommand = new DelegateCommand(OnToMainView);
            ToRunningViewCommand = new DelegateCommand(OnToRunningView);
            ToMaintenceViewCommand = new DelegateCommand(OnToMaintenceView, () => false);

            m_EventAggregator.GetEvent<PowerStateChangedEvent>().Subscribe(ar => OnToMainView(), ThreadOption.UIThread);

            OnToMainView();
        }

        private void OnToMaintenceView()
        {
            MaintenceNavigator.ToDefaultView();
            m_RegionManager.RequestNavigate(RegionNames.ShellContent, typeof(MaintenceLayout).FullName);
        }

        private void OnToRunningView()
        {
            RunningNavigator.ToDefaultView();
            m_RegionManager.RequestNavigate(RegionNames.ShellContent, typeof(RunningLayout).FullName);
        }

        private void OnToMainView()
        {
            m_RegionManager.RequestNavigate(RegionNames.ShellContent, typeof(ShellContentLayout).FullName);
        }
    }
}
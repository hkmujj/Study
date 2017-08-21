using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using CommonUtil.Util;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine7.CusstomAttribute;
using Subway.ShenZhenLine7.Envets;
using Subway.ShenZhenLine7.Extention;
using Subway.ShenZhenLine7.Interfaces;
using Subway.ShenZhenLine7.Models;
using Subway.ShenZhenLine7.Resource.Keys;
using Subway.ShenZhenLine7.ViewModels;

namespace Subway.ShenZhenLine7.Controller.ViewModelController
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class ShenZhenLine7Controller : ControllerBase<ShenZhenLine7ViewModel>
    {
        private readonly DispatcherTimer m_Timer;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager">区域管理器</param>
        /// <param name="eventAggregator">事件聚合器</param>
        [ImportingConstructor]
        public ShenZhenLine7Controller(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            RegionManager = regionManager;
            EventAggregator = eventAggregator;
            eventAggregator.GetEvent<NavigatorEvent>().Subscribe(NavigatorToView, ThreadOption.UIThread);
            eventAggregator.GetEvent<BoolDataChangedEvent>().Subscribe((BoolChanedAction));
            eventAggregator.GetEvent<NavigatorToKeyEvent>().Subscribe((NavigatorToKey), ThreadOption.UIThread);
            NavigatorToKeyCommand = new DelegateCommand<string>(NavigatorToKey);
            m_Timer = new DispatcherTimer();
            m_Timer.Tick += (sener, args) =>
            {
                if (ViewModel != null)
                {
                    ViewModel.CurrentTime = DateTime.Now;
                }
            };
            m_Timer.Interval = new TimeSpan(0, 0, 1);
            m_Timer.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        [Import]
        public IStateFactory StateFactory { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public ICommand NavigatorToKeyCommand { get; private set; }
        private void NavigatorToKey(string s)
        {
            ViewModel.UpdateState(StateFactory.GetOrCreateState(s));
        }

        private void BoolChanedAction(DataChangedEventArgs<bool> e)
        {
            e.Data.UpdateIfContain(InBoolKeys.黑屏, b => ViewModel.IsStart = b);
            ViewModel.MasterPageViewModel.DoorViewModel.DoorUnits.ForEach(f => f.DataChanged(e.Data));
            ViewModel.MasterPageViewModel.AirConditionViewModel.AllAirConditionUnits.ForEach(f => f.DataChanged(e.Data));
            ViewModel.MasterPageViewModel.AssistViewModel.AssistUnits.ForEach(f => f.DataChanged(e.Data));
            ViewModel.MasterPageViewModel.EmergencyTalkViewModel.EnmergencyTalkUnits.ForEach(f => f.DataChanged(e.Data));
            ViewModel.MasterPageViewModel.BrakeViewModel.BrakeUnits.ForEach(f => f.DataChanged(e.Data));
            //故障
            e.Data.Where(w => w.Value).ForEach(f => ViewModel.EventInfoViewModel.EventManagerService.HappenFault(f.Key));
            //更新旁路状态
            ViewModel.BypassViewModel.AllUnits.ForEach(f => f.DataChanged(e.Data));
        }

        private void NavigatorToView(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return;
            }
            var parent = GetType().Assembly.GetType(s).GetCustomAttributes(typeof(ParentAttribute), false).FirstOrDefault() as ParentAttribute;
            if (parent != null)
            {
                NavigatorToView(parent.Type.FullName);
            }
            var att = GetType().Assembly.GetType(s)?.GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault() as ViewExportAttribute;
            if (att != null)
            {
                RegionManager.RequestNavigate(att.RegionName, s);
            }
            var borths = GetType().Assembly.GetType(s).GetCustomAttributes(typeof(BrotherAttribute), false).Cast<BrotherAttribute>().ToList();
            if (borths.Count != 0)
            {
                borths.ForEach(f => NavigatorToView(f.Type.FullName));
            }
        }

        /// <summary>
        /// 黑屏状态变化
        /// </summary>
        public void StartChanged()
        {
            if (ViewModel.IsStart)
            {
                EventAggregator.GetEvent<NavigatorToKeyEvent>().Publish(StateKeys.启动界面);
                ViewModel.AllCourceViewModel.ForEach(f => f.Value.Start());
                Task.Factory.StartNew(s =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    EventAggregator.GetEvent<NavigatorToKeyEvent>().Publish(StateKeys.主页面);
                }, null);
            }
            else
            {
                ViewModel.AllCourceViewModel.ForEach(f => f.Value.Clear());
                EventAggregator.GetEvent<NavigatorToKeyEvent>().Publish(StateKeys.黑屏);
            }
        }
        /// <summary>
        /// 区域管理器
        /// </summary>
        protected IRegionManager RegionManager { get; }
        /// <summary>
        /// 事件聚合器
        /// </summary>
        protected IEventAggregator EventAggregator { get; }
    }
}
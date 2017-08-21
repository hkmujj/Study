using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using DevExpress.Mvvm;
using LightRail.HMI.SZLHLF.Constant;
using LightRail.HMI.SZLHLF.ViewModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interfaces;
using LightRail.HMI.SZLHLF.Event;
using LightRail.HMI.SZLHLF.Interface;
using LightRail.HMI.SZLHLF.Model;
using LightRail.HMI.SZLHLF.View.Contents;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Service;

namespace LightRail.HMI.SZLHLF.Controller
{
    [Export]
    public class SZLHLFController : ControllerBase<SZLHLFViewModel>
    {
        //更新最新时间定时器
        private /*readonly*/ DispatcherTimer m_GetNowTimeTimer;

        public SZLHLFController()
        {
            m_EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            m_RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            Navigator = new DelegateCommand<NavigatorEventArgs>(NavigatorMethod);
            m_EventAggregator.GetEvent<NavigatorEvent>().Subscribe(NavigatorMethod, ThreadOption.UIThread);
            if (GlobalParam.Instance.InitParam != null)
            {
                GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<ICourseService>().CourseStateChanged += SZLHLFController_CourseStateChanged;
            }
        }

        [ImportMany]
        private List<Lazy<IModels>> AllModels;
        private void SZLHLFController_CourseStateChanged(object sender, CourseStateChangedArgs e)
        {
            switch (e.CourseService.CurrentCourseState)
            {
                case CourseState.Unknown:

                    break;
                case CourseState.Started:
                    AllModels.ForEach(f=>f.Value.Initialize());
                    m_RegionManager.RequestNavigate(RegionNames.ContentUpContent, typeof(RootContentView).FullName);
                    break;
                case CourseState.Stoped:
                    AllModels.ForEach(f => f.Value.Clear());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public override void Initalize()
        {
            m_GetNowTimeTimer = new DispatcherTimer();

            m_GetNowTimeTimer.Tick += (sender, args) =>
            {
                if (ViewModel.OtherViewModel.Model != null)
                {
                    ViewModel.OtherViewModel.Model.CurrentTime = DateTime.Now;
                }
            };

            m_GetNowTimeTimer.Interval = new TimeSpan(0, 0, 1);
            m_GetNowTimeTimer.Start();
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
            ViewModel.OtherViewModel.Model.CurViewTitle = args.CurViewTitle;
        }
        /// <summary>
        /// 导航
        /// </summary>
        public ICommand Navigator { get; private set; }

        /// <summary>
        /// 区域管理器
        /// </summary>
        private readonly IRegionManager m_RegionManager;

        /// <summary>
        /// 事件聚合器
        /// </summary>
        private readonly IEventAggregator m_EventAggregator;
        
    }
}
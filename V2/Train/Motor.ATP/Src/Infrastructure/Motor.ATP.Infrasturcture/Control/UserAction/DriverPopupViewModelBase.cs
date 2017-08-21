using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction
{
    public abstract class DriverPopupViewModelBase : NotificationObject, IDriverPopupView, INavigationAware
    {
        protected string PopViewTitleContent { set; get; }

        protected string TitleContent
        {
            set
            {
                if (m_TitleContent == value)
                {
                    return;
                }
                m_TitleContent = value;
                RaisePropertyChanged(() => TitleContent);
            }
            get
            {
                return m_TitleContent;
            }
        }

        protected string PopupViewName { get; set; }


        private IPopupViewService m_PopupViewService;

        private IDriverInputEventService m_DriverInputEventService;
        private IEventAggregator m_EventAggregator;
        private IATP m_ATP;
        private string m_TitleContent;

        protected IRegionManager RegionManager
        {
            get { return ServiceLocator.Current.GetInstance<IRegionManager>(); }
        }

        protected IPopupViewService PopupViewService
        {
            get { return m_PopupViewService ?? (m_PopupViewService = App.Current.ServiceManager.GetService<IPopupViewService>(ATP.ATPType.ToString())); }
        }

        protected IDriverInterface DriverInterface { private set; get; }

        public IATP ATP
        {
            private set
            {
                if (Equals(value, m_ATP))
                {
                    return;
                }

                m_ATP = value;
                RaisePropertyChanged(() => ATP);
            }
            get { return m_ATP; }
        }

        protected IDriverInputEventService DriverInputEventService
        {
            get { return m_DriverInputEventService ?? (m_DriverInputEventService = App.Current.ServiceManager.GetService<IDriverInputEventService>(ATP.ATPType.ToString())); }
        }

        protected IEventAggregator EventAggregator
        {
            get
            {
                return m_EventAggregator ?? (m_EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>());
            }
        }

        public void UpdateState(IDriverInterface lastInterface, IDriverInterface currentInterface)
        {
            DriverInterface = currentInterface;
            ATP = DriverInterface.ATP;
            UpdateState();
        }

        protected virtual void UpdateState()
        {

        }

        /// <summary>
        /// 响应， 增加弹出框的亮度调节
        /// </summary>
        /// <param name="driverInterface"></param>
        public virtual void ResponseAction(IDriverInterface driverInterface)
        {
            DriverInterface = driverInterface;

            PopupViewService.ActiveView(new PopViewParam(GetType(), TitleContent, PopupViewName, PopViewTitleContent));

            DriverInputEventService.DriverInputed += DriverInputEventServiceOnDriverInputed;
        }

        public virtual void FinishResponseAction(IDriverInterface driverInterface)
        {

            DriverInputEventService.DriverInputed -= DriverInputEventServiceOnDriverInputed;

            PopupViewService.ActiveView(null);

            DriverInterface = null;
        }
        protected virtual void DriverInputEventServiceOnDriverInputed(DriverInputEventArgs args)
        {
            ATP = DriverInterface.ATP;
        }

        /// <summary>Called when the implementer has been navigated to.</summary>
        /// <param name="navigationContext">The navigation context.</param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (DriverInterface != null)
            {
                ATP = DriverInterface.ATP;
            }
        }

        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns>
        /// <see langword="true" /> if this instance accepts the navigation request; otherwise, <see langword="false" />.
        /// </returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
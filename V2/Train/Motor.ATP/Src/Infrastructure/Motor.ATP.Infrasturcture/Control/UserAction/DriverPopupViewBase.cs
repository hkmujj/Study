//using Microsoft.Practices.Prism.Events;
//using Microsoft.Practices.Prism.Regions;
//using Microsoft.Practices.Prism.ViewModel;
//using Microsoft.Practices.ServiceLocation;
//using MMI.Facility.Interface;
//using Motor.ATP.CommonView.View.PopupView;
//using Motor.ATP.Infrasturcture.Interface;
//using Motor.ATP.Infrasturcture.Interface.Service;
//using Motor.ATP.Infrasturcture.Interface.UserAction;

//namespace Motor.ATP.Infrasturcture.Control.UserAction
//{
//    //public abstract class DriverPopupViewBase<T> : NotificationObject, IDriverPopupView where T : PopupViewBase
//    //{
//    //    protected T PopupControl { set; get; }


//    //    private IPopupViewService m_PopupViewService;

//    //    private IDriverInputEventService m_DriverInputEventService;
//    //    private IEventAggregatorProvider m_EventAggregator;

//    //    protected IRegionManager RegionManager
//    //    {
//    //        get { return ServiceLocator.Current.GetInstance<IRegionManager>(); }
//    //    }

//    //    protected IPopupViewService PopupViewService
//    //    {
//    //        get { return m_PopupViewService ?? (m_PopupViewService = App.Current.ServiceManager.GetService<IPopupViewService>()); }
//    //    }

//    //    protected IDriverInterface DriverInterface { private set; get; }

//    //    protected IATP ATP { get { return DriverInterface.ATP; } }

//    //    protected IDriverInputEventService DriverInputEventService
//    //    {
//    //        get { return m_DriverInputEventService ?? (m_DriverInputEventService = App.Current.ServiceManager.GetService<IDriverInputEventService>()); }
//    //    }

//    //    protected IEventAggregator EventAggregator
//    //    {
//    //        get
//    //        {
//    //            return m_EventAggregator != null
//    //                ? m_EventAggregator.EventAggregator
//    //                : (m_EventAggregator = App.Current.ServiceManager.GetService<IEventAggregatorProvider>())
//    //                    .EventAggregator;
//    //        }
//    //    }

//    //    public void UpdateState(IDriverInterface lastInterface, IDriverInterface currentInterface)
//    //    {
//    //        DriverInterface = currentInterface;

//    //        UpdateState();
//    //    }

//    //    protected virtual void UpdateState()
//    //    {
            
//    //    }

//    //    /// <summary>
//    //    /// 响应， 增加弹出框的亮度调节
//    //    /// </summary>
//    //    /// <param name="driverInterface"></param>
//    //    public virtual void ResponseAction(IDriverInterface driverInterface)
//    //    {
//    //        DriverInterface = driverInterface;

//    //        PopupControl.AddOpuaqueLayer(driverInterface.ATP.Other);

//    //        PopupViewService.Active(PopupControl);

//    //        DriverInputEventService.DriverInputed += DriverInputEventServiceOnDriverInputed;
//    //    }

//    //    public virtual void FinishResponseAction(IDriverInterface driverInterface)
//    //    {

//    //        DriverInputEventService.DriverInputed -= DriverInputEventServiceOnDriverInputed;

//    //        PopupViewService.Active(null);

//    //        PopupControl.ClearOpuaqueLayer();

//    //        DriverInterface = null;
//    //    }
//    //    protected virtual void DriverInputEventServiceOnDriverInputed(DriverInputEventArgs args)
//    //    {

//    //    }
//    //}
//}
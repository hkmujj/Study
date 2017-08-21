using System.Diagnostics.Contracts;
using CommonUtil.Controls;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Service;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Service;

namespace Motor.ATP.Infrasturcture.Model.UserAction
{
    public abstract class DriverActionResponserBase : IDriverActionResponser
    {
        /// <summary>
        /// 对应的可选择单元
        /// </summary>
        protected IDriverSelectableItem m_Target;

        private INotifyableDriverInputEventService m_NotifyableDriverInputEventService;
        private ICommunicationDataService m_DataService;
        private IEventAggregator m_EventAggregator;

        protected IDriverInterfaceFactory InterfaceFactory
        {
            get { return App.Current.ServiceManager.GetService<IDriverInterfaceFactory>(ATP.ATPType.ToString()); }
        }

        protected IDriverInterfaceController InterfaceController
        {
            get { return App.Current.ServiceManager.GetService<IDriverInterfaceController>(ATP.ATPType.ToString()); }
        }

        protected UserActionType UserActionType { private set; get; }

        protected INotifyableDriverInputEventService NotifyableDriverInputEventService
        {
            get
            {
                return m_NotifyableDriverInputEventService ??
                       (m_NotifyableDriverInputEventService =
                           App.Current.ServiceManager.GetService<INotifyableDriverInputEventService>(ATP.ATPType.ToString()) );
            }
        }

        protected IEventAggregator EventAggregator
        {
            get { return m_EventAggregator ?? (m_EventAggregator = App.Current.ServiceManager.GetService<IEventAggregatorProvider>().EventAggregator); }
        }

        protected DriverInterfaceKey Key { get { return m_Target.Parent.Parent.Id; } }

        protected IATP ATP
        {
            get { return m_Target.Parent.Parent.ATP; }
        }

        protected DriverActionResponserBase(IDriverSelectableItem item, UserActionType userActionType)
        {
            Contract.Requires(item != null);
            m_Target = item;
            UserActionType = userActionType;
        }

        /// <summary>
        /// 响应按键按下，DriverActionResponserBase 提供发送输入事件，用于弹出框响应
        /// </summary>
        public virtual void ResponseMouseDown()
        {
            NotifyMouseEnvet(MouseState.MouseDown);
        }

        /// <summary>
        /// 响应按键弹起，DriverActionResponserBase 提供发送输入事件，用于弹出框响应
        /// </summary>
        public virtual void ResponseMouseUp()
        {
            NotifyMouseEnvet(MouseState.MouseUp);
        }

        /// <summary>
        /// 响应按键点击
        /// </summary>
        public virtual void ResponseMouseClick()
        {

        }

        protected void NotifyMouseEnvet(MouseState mouseState)
        {
            NotifyableDriverInputEventService.OnDriverInputed(new DriverInputEventArgs(UserActionType,
                mouseState));
        }
    }
}
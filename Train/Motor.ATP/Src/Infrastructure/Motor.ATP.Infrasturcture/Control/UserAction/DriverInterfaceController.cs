using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Service;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Interface.UserAction.UpdateStateParam;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Events;

namespace Motor.ATP.Infrasturcture.Control.UserAction
{
    public class DriverInterfaceController : ATPPartialBase, IDriverInterfaceController
    {
        private IDriverInterface m_CurrentInterface;

        private readonly object m_UpdateDriverInterfaceLocker = new object();

        private readonly List<IDriverInterface> m_DriverInterfaceAfterButtonResponsedBuffer;

        public event Action<IDriverInterfaceController, IUpdateDriverInterfaceParam, IDriverInterface, IDriverInterface>
            CurrentInterfaceChanged;

        public event
            Func<IDriverInterfaceController, IUpdateDriverInterfaceParam, IDriverInterface, IDriverInterface, bool>
            CurrentInterfaceChanging;

        public IDriverInterfaceFactory DriverInterfaceFactory { get; protected set; }

        private IServiceManager ServiceManager { get { return App.Current.ServiceManager; } }

        public DriverInterfaceController(ATPDomain parent)
            : base(parent)
        {
            m_DriverInterfaceAfterButtonResponsedBuffer = new List<IDriverInterface>();

            var sm = App.Current.ServiceManager;
            sm.GetService<IInfomationService>(parent.ATPType.ToString()).InfomationBegin += OnInfomationBegin;
            sm.GetService<IInfomationService>(parent.ATPType.ToString()).InfomationEnd += OnInfomationEnd;
            sm.GetService<IInfomationService>(parent.ATPType.ToString()).InfomationEnsured += OnInfomationEnsured;

            sm.GetService<IClearDataService>(parent.ATPType.ToString()).ClearDataEvent += o => ClearWhenCourseStoped();

            var course = App.Current.ServiceManager.GetService<ICourseService>();
            course.CourseStateChanged += CourseOnCourseStateChanged;

            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ButtonResponseCompletedEvent>()
                .Subscribe(OnButtonResponseCompleted, ThreadOption.UIThread, true,
                    args => args.ATPType == parent.ATPType);
        }

        private void CourseOnCourseStateChanged(object sender, CourseStateChangedArgs e)
        {
            if (e.CourseService.CurrentCourseState == CourseState.Stoped)
            {
                ServiceManager.GetService<IClearDataService>(Parent.ATPType.ToString()).NotifyClearData(sender);
            }
        }

        protected virtual void ClearWhenCourseStoped()
        {
            m_DriverInterfaceAfterButtonResponsedBuffer.Clear();
            UpdateDriverInterface(DriverInterfaceKey.Root);
        }

        protected virtual void OnInfomationEnsured(IInfomationItem infomationItem)
        {

        }

        private void OnButtonResponseCompleted(ButtonResponseCompletedEvent.Args args)
        {
            m_DriverInterfaceAfterButtonResponsedBuffer.ForEach(e => UpdateDriverInterface(e));

            m_DriverInterfaceAfterButtonResponsedBuffer.Clear();
        }

        protected virtual void OnInfomationEnd(IInfomationItem obj)
        {
        }

        protected virtual void OnInfomationBegin(IInfomationItem obj)
        {
        }


        public IDriverInterface CurrentInterface
        {
            get { return m_CurrentInterface; }
            protected set
            {
                if (Equals(value, m_CurrentInterface))
                {
                    return;
                }

                m_CurrentInterface = value;
                RaisePropertyChanged(() => CurrentInterface);
            }
        }

        public IDriverInterfaceView DriverInterfaceView { get; protected set; }

        public void UpdateDriverInterface(IDriverInterface driverInterface,
            IUpdateDriverInterfaceParam updateParam = null)
        {
            lock (m_UpdateDriverInterfaceLocker)
            {
                if (driverInterface == null)
                {
                    throw new ArgumentNullException("driverInterface");
                }

                var ep = updateParam as IUpdateDriverInterfaceEventParam;
                driverInterface.CurrentInfomationItem = ep != null ? ep.InfomationItem : null;

                if (!OnCurrentInterfaceChanging(this, updateParam, CurrentInterface, driverInterface))
                {
                    return;
                }

                var old = CurrentInterface;

                if (CurrentInterface != null)
                {
                    CurrentInterface.NavigateFromThis();
                }

                driverInterface.NavigateToThis(CurrentInterface);
                CurrentInterface = driverInterface;
                CurrentInterface.DriverSelectable.UpdateStates();

                if (DriverInterfaceView != null)
                {
                    DriverInterfaceView.UpdateView(CurrentInterface);
                }

                OnCurrentInterfaceChanged(this, updateParam, old, CurrentInterface);
            }
        }

        public void UpdateDriverInterface(DriverInterfaceKey interfaceKey,
            IUpdateDriverInterfaceParam updateParam = null)
        {
            var di = DriverInterfaceFactory.GetOrCreateDriverInterface(interfaceKey);
            var ep = updateParam as IUpdateDriverInterfaceEventParam;
            if (ep != null)
            {
                di.CurrentInfomationItem = ep.InfomationItem;
            }
            UpdateDriverInterface(DriverInterfaceFactory.GetOrCreateDriverInterface(interfaceKey), updateParam);
        }

        /// <summary>
        /// 在按键响应完后更新接口
        /// </summary>
        public void UpdateDriverInterfaceAfterButtonResponsed(DriverInterfaceKey interfaceKey,
            IUpdateDriverInterfaceParam updateParam = null)
        {
            UpdateDriverInterfaceAfterButtonResponsed(DriverInterfaceFactory.GetOrCreateDriverInterface(interfaceKey), updateParam);
        }

        /// <summary>
        /// 在按键响应完后更新接口
        /// </summary>
        /// <param name="driverInterface"></param>
        /// <param name="updateParam"></param>
        public void UpdateDriverInterfaceAfterButtonResponsed(IDriverInterface driverInterface,
            IUpdateDriverInterfaceParam updateParam = null)
        {
            m_DriverInterfaceAfterButtonResponsedBuffer.Add(driverInterface);
        }

        protected virtual void OnCurrentInterfaceChanged(IDriverInterfaceController sender,
            IUpdateDriverInterfaceParam updateParam, IDriverInterface old, IDriverInterface current)
        {
            var handler = CurrentInterfaceChanged;
            if (handler != null)
                handler(sender, updateParam, old, current);
        }

        protected virtual bool OnCurrentInterfaceChanging(IDriverInterfaceController sender,
            IUpdateDriverInterfaceParam updateParam, IDriverInterface old, IDriverInterface current)
        {
            var handler = CurrentInterfaceChanging;
            return handler == null || handler(sender, updateParam, old, current);
        }
    }
}
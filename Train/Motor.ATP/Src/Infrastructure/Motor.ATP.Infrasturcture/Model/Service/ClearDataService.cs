using System;
using System.ComponentModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Events;
using Motor.ATP.Infrasturcture.Interface.Service;

namespace Motor.ATP.Infrasturcture.Model.Service
{
    public class ClearDataService : IClearDataService, IATPPartial
    {

        private readonly PushOperatorToUIThreadEvent m_PushOperatorToUIThreadEvent;

        private readonly Action<ClearDataService, object> m_ClearDataAction = (ds, sender) => ds.OnClearDataEvent(sender);

        public ClearDataService()
        {
            m_PushOperatorToUIThreadEvent = ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<PushOperatorToUIThreadEvent>();
        }

        public void NotifyClearData(object notifier, bool notifyInUiThread = true)
        {
            if (notifyInUiThread)
            {
                m_PushOperatorToUIThreadEvent.Publish(
                    new PushOperatorToUIThreadEvent.Args(m_ClearDataAction,
                        Parent.Identity, this, notifier));
            }
            else
            {
                OnClearDataEvent(notifier);
            }
        }

        protected virtual void OnClearDataEvent(object obj)
        {
            var handler = ClearDataEvent;
            if (handler != null)
                handler(obj);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// ATP 
        /// </summary>
        public IATP Parent { get; set; }

        public event Action<object> ClearDataEvent;
    }
}
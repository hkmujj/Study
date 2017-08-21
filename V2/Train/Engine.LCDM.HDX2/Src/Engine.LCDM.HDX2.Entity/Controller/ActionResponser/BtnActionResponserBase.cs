using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Microsoft.Practices.Prism.Events;

namespace Engine.LCDM.HDX2.Entity.Controller.ActionResponser
{
    public abstract class BtnActionResponserBase : IBtnActionResponser
    {
        [Import]
        protected IEventAggregator EventAggregator { private set; get; }

        protected CompositePresentationEvent<StateChangedEventArg> StateChangedEvent
        {
            get
            {
                return EventAggregator.GetEvent<CompositePresentationEvent<StateChangedEventArg>>();
            }
        }

        protected CompositePresentationEvent<T> GetEvent<T>()
        {
            return EventAggregator.GetEvent<CompositePresentationEvent<T>>();
        }

        /// <summary>
        /// 变换当前状态
        /// </summary>
        /// <param name="stateKey"></param>
        protected void ChangeStateTo(StateInterfaceKey stateKey)
        {
            StateChangedEvent.Publish(new StateChangedEventArg(stateKey));
        }

        public virtual void ResponseMouseDown()
        {

        }

        public virtual void ResponseMouseUp()
        {

        }
    }
}
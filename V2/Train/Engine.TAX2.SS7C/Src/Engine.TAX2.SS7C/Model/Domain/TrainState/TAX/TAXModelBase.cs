using Engine.TAX2.SS7C.Model.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.Model.Domain.TrainState.TAX
{
    public abstract class TAXModelBase : NotificationObject, ITAXCommunicationStateProvider
    {
        private TAX2CommunicationState m_TAX2CommunicationState;

        public TAX2CommunicationState TAX2CommunicationState
        {
            set
            {
                if (value == m_TAX2CommunicationState)
                {
                    return;
                }

                m_TAX2CommunicationState = value;
                RaisePropertyChanged(() => TAX2CommunicationState);
            }
            get { return m_TAX2CommunicationState; }
        }

        protected TAXModelBase()
        {
            TAX2CommunicationState = TAX2CommunicationState.Fault;
        }
    }
}
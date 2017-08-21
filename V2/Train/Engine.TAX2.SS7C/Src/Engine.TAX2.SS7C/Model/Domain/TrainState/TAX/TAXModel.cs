using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.Model.Domain.TrainState.TAX
{
    [Export]
    public class TAXModel :NotificationObject
    {
        private TAXModelBase m_CurrentSelectedModel;

        public TAXModelBase CurrentSelectedModel
        {
            get { return m_CurrentSelectedModel; }
            set
            {
                if (Equals(value, m_CurrentSelectedModel))
                {
                    return;
                }

                m_CurrentSelectedModel = value;
                RaisePropertyChanged(() => CurrentSelectedModel);
            }
        }
    }
}
using System.ComponentModel.Composition;
using Engine.TCMS.Turkmenistan.Model.BtnStragy;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.Turkmenistan.Model
{
    [Export]
    public class DomainModel : NotificationObject
    {
        private IStateInterface m_CurrentStateInterface;

        public IStateInterface CurrentStateInterface
        {
            private set
            {
                if (Equals(value, m_CurrentStateInterface))
                {
                    return;
                }

                m_CurrentStateInterface = value;
                m_CurrentStateInterface.UpdateState();
                RaisePropertyChanged(() => CurrentStateInterface);
            }
            get { return m_CurrentStateInterface; }
        }

        public void UpdateCurrentState(IStateInterface current)
        {
            CurrentStateInterface = current;
        }
    }
}
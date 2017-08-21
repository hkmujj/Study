using System.ComponentModel.Composition;
using Motor.TCMS.CRH400BF.Model.BtnStragy;
using Microsoft.Practices.Prism.ViewModel;
using Motor.TCMS.CRH400BF.ViewModel;

namespace Motor.TCMS.CRH400BF.Model
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StateModel : NotificationObject
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
                RaisePropertyChanged(() => CurrentStateInterface);
            }
            get { return m_CurrentStateInterface; }
        }

        public void UpdateCurrentState(IStateInterface current, DomainViewModel domainViewModel)
        {
            if (Equals(current, CurrentStateInterface))
            {
                return;
            }

            current.UpdateState(domainViewModel);
            CurrentStateInterface = current;
        }
    }
}
using System.ComponentModel.Composition;
using Engine.Angola.TCMS.Model.Domain;
using Engine.Angola.TCMS.Model.Screen;
using Microsoft.Practices.Prism.ViewModel;
using Engine.Angola.TCMS.Model.BtnStragy;

namespace Engine.Angola.TCMS.Model
{
    [Export]
    public class AngolaTCMSShellModel :NotificationObject
    {
        [ImportingConstructor]
        public AngolaTCMSShellModel(Other other, TCMSModel tcms)
        {
            Other = other;
            TCMS = tcms;
        }

        public Other Other { private set; get; }

        public TCMSModel TCMS { private set; get; }

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
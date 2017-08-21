using System.ComponentModel.Composition;
using LightRail.HMI.SZLHLF.Model.BtnStragy;

namespace LightRail.HMI.SZLHLF.Model
{
    [Export]
    public class SZLHLFModel : ModelBase
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
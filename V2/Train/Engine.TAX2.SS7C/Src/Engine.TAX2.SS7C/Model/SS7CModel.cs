using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Model.BtnStragy;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.Model
{
    [Export]
    public class SS7CModel : NotificationObject
    {
        private IStateInterface m_CurrentStateInterface;
        private bool m_IsVisible;

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

        public bool IsVisible
        {
            get { return m_IsVisible; }
            set
            {
                if (value == m_IsVisible)
                {
                    return;
                }

                m_IsVisible = value;
                RaisePropertyChanged(() => IsVisible);
            }
        }

        public void UpdateCurrentState(IStateInterface current)
        {
            CurrentStateInterface = current;
        }
    }
}
using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.BtnStragy;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model
{
    [Export]
    public class HXN5BModel : NotificationObject
    {
        private IStateInterface m_CurrentStateInterface;
        private string m_Title;
        private bool m_IsVisble;

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
                Title = value.Title;
                RaisePropertyChanged(() => CurrentStateInterface);
            }
            get { return m_CurrentStateInterface; }
        }

        public bool IsVisble
        {
            get { return m_IsVisble; }
            set
            {
                if (value == m_IsVisble)
                {
                    return;
                }

                m_IsVisble = value;
                RaisePropertyChanged(() => IsVisble);
            }
        }

        public string Title
        {
            get { return m_Title; }
            set
            {
                if (value == m_Title)
                {
                    return;
                }

                m_Title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public void UpdateCurrentState(IStateInterface current)
        {
            CurrentStateInterface = current;
        }
    }
}
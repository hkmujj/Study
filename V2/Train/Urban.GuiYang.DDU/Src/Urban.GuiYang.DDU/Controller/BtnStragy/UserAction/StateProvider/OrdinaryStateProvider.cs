using Microsoft.Practices.Prism.ViewModel;
using Urban.GuiYang.DDU.Model.BtnStragy;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Controller.BtnStragy.UserAction.StateProvider
{
    public class OrdinaryStateProvider : NotificationObject, IBtnStateProvider
    {
        private bool m_Visible;
        private NavigationButtonState m_ButtonState;
        private bool m_IsSelected;
        public BtnItem Parent { get; set; }

        public OrdinaryStateProvider()
        {
            Visible = true;
            ButtonState = NavigationButtonState.Normal;
        }

        public bool Visible
        {
            get { return m_Visible; }
            protected set
            {
                if (value == m_Visible)
                {
                    return;
                }

                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
        }

        public bool IsSelected
        {
            get { return m_IsSelected; }
            set
            {
                if (value == m_IsSelected)
                {
                    return;
                }

                m_IsSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }

        public NavigationButtonState ButtonState
        {
            get { return m_ButtonState; }
            protected set
            {
                if (value == m_ButtonState)
                {
                    return;
                }

                m_ButtonState = value;
                RaisePropertyChanged(() => ButtonState);
            }
        }

        public virtual void UpdateState()
        {
        }
    }
}
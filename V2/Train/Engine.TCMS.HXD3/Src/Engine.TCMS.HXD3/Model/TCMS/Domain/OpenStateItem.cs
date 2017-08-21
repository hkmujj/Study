using System.Diagnostics;
using Engine.TCMS.HXD3.Model.ConfigModel;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.Model.TCMS.Domain
{
    public class OpenStateItem : NotificationObject
    {
        private bool m_IsSelected;
        private OpenStateState m_State;

        [DebuggerStepThrough]
        public OpenStateItem(OpenStateConfig config)
        {
            Config = config;
            State = OpenStateState.Normal;
        }

        public OpenStateConfig Config { private set; get; }

        public OpenStateState State
        {
            set
            {
                if (value == m_State)
                {
                    return;
                }

                m_State = value;
                RaisePropertyChanged(() => State);
            }
            get { return m_State; }
        }

        public bool IsSelected
        {
            set
            {
                if (value == m_IsSelected)
                {
                    return;
                }

                m_IsSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
            get { return m_IsSelected; }
        }
    }
}
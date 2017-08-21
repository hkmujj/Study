using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
{
    [DebuggerDisplay("Enabled={Enabled}, Visible={Visible}, CanEnter={EnterOrQuitState}")]
    public class EnterableItemStateProvider : ItemStateProvider, IEnterableItemStateProvider
    {
        private EnterOrQuit m_EnterOrQuitState;

        [DebuggerStepThrough]
        public EnterableItemStateProvider(bool enable = true, bool visible = true, EnterOrQuit enterOrQuitStateOrQuit= EnterOrQuit.Enter)
        {
            Enabled = enable;
            Visible = visible;
            EnterOrQuitState = enterOrQuitStateOrQuit;
        }

        /// <summary>
        ///  是否可以进入
        /// </summary>
        public EnterOrQuit EnterOrQuitState
        {
            set
            {
                if (value == m_EnterOrQuitState)
                {
                    return;
                }

                m_EnterOrQuitState = value;
                RaisePropertyChanged(() => EnterOrQuitState);
            }
            get { return m_EnterOrQuitState; }
        }
    }

    [DebuggerDisplay("Enabled={Enabled}, Visible={Visible}")]
    public class ItemStateProvider : NotificationObject, IItemStateProvider
    {
        private bool m_Enabled;
        private bool m_Visible;

        [DebuggerStepThrough]
        public ItemStateProvider(bool enable = true, bool visible = true)
        {
            Enabled = enable;
            Visible = visible;
        }

        public bool Enabled
        {
            get { return m_Enabled; }
            set
            {
                if (value == m_Enabled)
                {
                    return;
                }

                m_Enabled = value;
                RaisePropertyChanged(() => Enabled);
            }
        }

        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (value == m_Visible)
                {
                    return;
                }

                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
        }
    }
}
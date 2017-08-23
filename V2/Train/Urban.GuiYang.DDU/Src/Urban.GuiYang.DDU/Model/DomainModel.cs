using System.ComponentModel.Composition;
using DevExpress.Mvvm;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.WPFInfrastructure.Interactivity;
using Urban.GuiYang.DDU.Model.BtnStragy;
using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.Model.Train;

namespace Urban.GuiYang.DDU.Model
{
    [Export]
    public class DomainModel : NotificationObject
    {
        private IStateInterface m_CurrentStateInterface;
        private bool m_Visible;
        private NavigationButtonState m_BypassState;
        private NavigationButtonState m_FaultSate;

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

        public DelegateCommand<CommandParameter> LoadedCommand { get; set; }


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
        /// <summary>
        /// 旁路按钮状态
        /// </summary>
        public NavigationButtonState BypassState
        {
            get { return m_BypassState; }
            set
            {
                if (value == m_BypassState)
                    return;

                m_BypassState = value;
                RaisePropertyChanged(() => BypassState);
            }
        }
        /// <summary>
        /// 故障按钮状态
        /// </summary>
        public NavigationButtonState FaultSate
        {
            get { return m_FaultSate; }
            set
            {
                if (value == m_FaultSate)
                    return;

                m_FaultSate = value;
                RaisePropertyChanged(() => FaultSate);
            }
        }

        public void UpdateCurrentState(IStateInterface current)
        {
            CurrentStateInterface = current;
        }
    }
}
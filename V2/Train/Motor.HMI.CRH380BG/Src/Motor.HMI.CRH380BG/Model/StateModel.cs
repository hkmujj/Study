using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380BG.Model.BtnStragy;
using Motor.HMI.CRH380BG.ViewModel;

namespace Motor.HMI.CRH380BG.Model
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StateModel : NotificationObject
    {
        private IStateInterface m_CurrentStateInterface;
        private bool m_CompiledVisible3;
        private string m_Title;
        private bool m_IsVisble;


        public StateViewModel ViewModel { get; set; }

        public ViewLocation ViewLocation { get { return ViewModel.DomainViewModel.ViewLocation; } }

        public IStateInterface CurrentStateInterface
        {
            private set
            {
                if (Equals(value, m_CurrentStateInterface))
                {
                    return;
                }

                m_CurrentStateInterface = value;
                m_CurrentStateInterface.UpdateState(ViewModel);
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

        public bool CompiledVisible3
        {
            get { return m_CompiledVisible3; }
            set
            {
                if (value == m_CompiledVisible3)
                {
                    return;
                }

                m_CompiledVisible3 = value;
                RaisePropertyChanged(() => CompiledVisible3);
            }
        }

        public void UpdateCurrentState(IStateInterface current)
        {
            CurrentStateInterface = current;
        }

    }
}
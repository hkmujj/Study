using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Model.BtnStragy;

namespace Subway.TCMS.LanZhou.Model
{
    [Export]
    public class DomainModel : NotificationObject
    {
        private IStateInterface m_CurrentStateInterface;
        private Type m_CurrentContentViewType;

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

        public DelegateCommand TrainFaultPageReturnCommand { get; set; }

        public Type CurrentContentViewType
        {
            get { return m_CurrentContentViewType; }
            set
            {
                if (value == m_CurrentContentViewType)
                {
                    return;
                }
                m_CurrentContentViewType = value;
                RaisePropertyChanged(() => CurrentContentViewType);
            }
        }

        

        public void UpdateCurrentState(IStateInterface current)
        {
            if (CurrentStateInterface != null && current != null)
            {
                current.CurrentSelectedBtn = CurrentStateInterface.CurrentSelectedBtn;
            }

            CurrentStateInterface = current;
        }
    }
}
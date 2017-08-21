using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.WPFInfrastructure.Interactivity;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300H.Subsys.ViewModel
{
    public class HardwareButtonViewModel : NotificationObject, IHardwareButtonViewModel
    {
        private DelegateCommand<CommandParameter> m_ButtonEventCommand;

        public DelegateCommand<CommandParameter> ButtonEventCommand
        {
            set
            {
                if (Equals(value, m_ButtonEventCommand))
                {
                    return;
                }
                m_ButtonEventCommand = value;
                RaisePropertyChanged(() => ButtonEventCommand);
            }
            get { return m_ButtonEventCommand; }
        }

        public event Action<UserActionType, MouseButtonState> ButtonEvent;

        ICommand IHardwareButtonViewModel.ButtonEventCommand
        {
            get { return ButtonEventCommand; }
        }

        public HardwareButtonViewModel()
        {
            ButtonEventCommand = new DelegateCommand<CommandParameter>(OnButtonEvent, CanButtonEvent);
        }

        private bool CanButtonEvent(CommandParameter arg)
        {
            return true;
        }

        private void OnButtonEvent(CommandParameter obj)
        {
            if ( obj.Parameter is UserActionType && obj.EventArgs is MouseButtonEventArgs)
            {
                OnButtonEvent((UserActionType)obj.Parameter, ((MouseButtonEventArgs)obj.EventArgs).ButtonState);
            }
        }

        protected virtual void OnButtonEvent(UserActionType arg1, MouseButtonState arg2)
        {
            if (ButtonEvent != null)
            {
                ButtonEvent.Invoke(arg1, arg2);
            }
        }
    }
}
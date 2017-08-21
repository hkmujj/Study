using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Motor.ATP.Domain.Interface.UserAction
{
    public interface IHardwareButtonViewModel : INotifyPropertyChanged
    {
        event Action<UserActionType, MouseButtonState> ButtonEvent;

        ICommand ButtonEventCommand { get; }
    }
}
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHardwareButtonViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        event Action<UserActionType, MouseButtonState> ButtonEvent;

        /// <summary>
        /// 
        /// </summary>
        ICommand ButtonEventCommand { get; }
    }
}
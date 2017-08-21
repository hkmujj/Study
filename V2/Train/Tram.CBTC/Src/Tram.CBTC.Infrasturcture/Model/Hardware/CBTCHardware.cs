using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.WPFInfrastructure.Interactivity;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Hardware.Button;

namespace Tram.CBTC.Infrasturcture.Model.Hardware
{
    public class CBTCHardware : NotificationObject
    {
        public ReadOnlyCollection<HardwareButton> HardwareButtonCollection { get; private set; }

        public CBTCHardware()
        {
            HardwareButtonCollection = typeof(UserActionType).GetEnumValues()
                .Cast<UserActionType>()
                .Select(s => new HardwareButton(s))
                .ToList()
                .AsReadOnly();

            ButtonEventCommand = new DelegateCommand<CommandParameter>(OnButtonEvent, CanButtonEvent);
        }

        protected virtual void OnButtonEvent(CommandParameter obj)
        {
            if (obj.Parameter is UserActionType && obj.EventArgs is MouseButtonEventArgs)
            {
                OnButtonEvent((UserActionType) obj.Parameter, ((MouseButtonEventArgs) obj.EventArgs).ButtonState);
            }
        }

        protected virtual bool CanButtonEvent(CommandParameter arg)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public event Action<UserActionType, MouseButtonState> ButtonEvent;

        /// <summary>
        /// 
        /// </summary>
        public ICommand ButtonEventCommand { get; protected set; }

        public HardwareButton FindHardwareButton(UserActionType userActionType)
        {
            return HardwareButtonCollection.First(f => f.Type == userActionType);
        }

        protected virtual void OnButtonEvent(UserActionType actionType, MouseButtonState mouseButtonState)
        {
            if (ButtonEvent != null)
                ButtonEvent.Invoke(actionType, mouseButtonState);
        }
    }
}
using System.Diagnostics;
using CommonUtil.Controls;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Model
{
    [DebuggerDisplay("Type={Type}, MouseState={MouseState}")]
    public class HardwareButton : NotificationObject, IHardwareButton
    {
        private MouseState m_MouseState;

        [DebuggerStepThrough]
        public HardwareButton(UserActionType type)
        {
            Type = type;
            MouseState = MouseState.MouseUp;
        }

        public UserActionType Type { get; set; }

        public MouseState MouseState
        {
            get { return m_MouseState; }
            set
            {
                if (value == m_MouseState)
                {
                    return;
                }
                m_MouseState = value;
                RaisePropertyChanged(() => MouseState);
            }
        }
    }
}
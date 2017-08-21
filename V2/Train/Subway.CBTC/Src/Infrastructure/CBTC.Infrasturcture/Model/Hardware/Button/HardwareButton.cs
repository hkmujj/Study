using System.Diagnostics;
using CBTC.Infrasturcture.Model.Constant;
using CommonUtil.Controls;
using Microsoft.Practices.Prism.ViewModel;

namespace CBTC.Infrasturcture.Model.Hardware.Button
{
    [DebuggerDisplay("Type={Type}, MouseState={MouseState}")]
    public class HardwareButton : NotificationObject
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
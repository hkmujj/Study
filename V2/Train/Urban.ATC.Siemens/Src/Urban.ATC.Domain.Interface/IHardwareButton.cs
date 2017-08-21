using System.ComponentModel;
using CommonUtil.Controls;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Interface
{
    public interface IHardwareButton : INotifyPropertyChanged
    {
        /// <summary>
        /// 按键类型
        /// </summary>
        UserActionType Type { get; }

        /// <summary>
        /// 按键状态
        /// </summary>
        MouseState MouseState { get; }
    }
}
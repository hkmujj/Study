using System.ComponentModel;
using CommonUtil.Controls;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 
    /// </summary>
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
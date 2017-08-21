using System.ComponentModel;

namespace Motor.ATP.Domain.Interface.UserAction
{
    /// <summary>
    /// 边缘的按键
    /// </summary>
    public interface IATPButton : IEnabled, IVisibility, INotifyPropertyChanged
    {
        /// <summary>
        /// 位置
        /// </summary>
        UserActionLocation Location { get; }

        /// <summary>
        /// 按键内容 
        /// </summary>
        ATPButtonContent ButtonContent { get; }

        /// <summary>
        /// 响应按钮
        /// </summary>
        /// <param name="atp"></param>
        void OnMouseDown(IATP atp);

    }
}

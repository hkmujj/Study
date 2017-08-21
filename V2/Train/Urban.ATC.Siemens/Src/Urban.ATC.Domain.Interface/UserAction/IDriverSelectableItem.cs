using System;
using System.ComponentModel;
using System.Drawing;

namespace Motor.ATP.Domain.Interface.UserAction
{
    public interface IDriverSelectableItem : INotifyPropertyChanged
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        long Id { get; }

        /// <summary>
        /// 用户作用类型
        /// </summary>
        Enum UserActionType { get; }

        string Content { get; }

        Image Backgroud { get; }

        bool Enabled { get; }

        bool Visible { get; }

        /// <summary>
        ///  外边是否可见
        /// </summary>
        bool OutlineVisible { set; get; }

        /// <summary>
        /// 事件响应
        /// </summary>
        IDriverActionResponser ActionResponser { get; }

        /// <summary>
        /// 父结点
        /// </summary>
        IDriverSelectable Parent { set; get; }

        object Tag { get; }

        void Initalize();

        void UpdateStates();
    }
}
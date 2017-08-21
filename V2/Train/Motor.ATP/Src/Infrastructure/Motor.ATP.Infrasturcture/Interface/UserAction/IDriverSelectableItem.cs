using System;
using System.ComponentModel;
using System.Drawing;

namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 司机可选项目
    /// </summary>
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

        /// <summary>
        /// 内容
        /// </summary>
        string Content { get; }

        /// <summary>
        /// 背景
        /// </summary>
        Image Backgroud { get; }

        /// <summary>
        /// 可用性
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// 可见
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        object Tag { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        void Initalize();

        /// <summary>
        /// 
        /// </summary>
        void UpdateStates();
    }
}
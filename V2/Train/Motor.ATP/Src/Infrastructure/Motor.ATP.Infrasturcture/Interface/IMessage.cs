using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Motor.ATP.Infrasturcture.Interface.Infomation;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 消息区的消息
    /// </summary>
    public interface IMessage : IATPPartial, IVisibility
    {
        /// <summary>
        /// 当前正在显示的第一个索引
        /// </summary>
        int CurrentFirstIndex { get; }

        /// <summary>
        /// 当前正在显示的第一个索引对应的值 
        /// </summary>
        IMessageItem CurrentFirstItem { get; }

        /// <summary>
        /// 正显示的个数
        /// </summary>
        int ShowingCount { set; get; }


        /// <summary>
        /// 
        /// </summary>
        bool CanNavigateUp { get; }

        /// <summary>
        /// 
        /// </summary>
        bool CanNavigateDown { get; }

        /// <summary>
        ///  向上可见性
        /// </summary>
        bool CanNavigateUpVisible { set; get; }

        /// <summary>
        /// 向下可见性
        /// </summary>
        bool CanNavigateDownVisible { set; get; }

        /// <summary>
        /// 正在显示的
        /// </summary>
        IEnumerable<IMessageItem> ShowingMessageCollection { get; }

        /// <summary>
        /// 消息集合
        /// </summary>
        ObservableCollection<IMessageItem> MessageCollection { get; }

    }

    /// <summary>
    /// 
    /// </summary>
    public interface IMessageItem : IIdentityProvider<IInfomationItemContent>, INotifyPropertyChanged
    {
        /// <summary>
        /// 消息来源
        /// </summary>
        IInfomationItem InfomationItem { get; }

        /// <summary>
        /// 时间戳
        /// </summary>
        DateTime TimeStamp { set; get; }

        /// <summary>
        /// 时间差,如果需要记时，用此字段。
        /// </summary>
        TimeSpan TimeDifference { get; }

        /// <summary>
        /// 消息样式 
        /// </summary>
        MessageStyle Style { set; get; }

        /// <summary>
        /// 内容
        /// </summary>
        string Content { get; }

        /// <summary>
        /// 确认OK
        /// </summary>
        void ConfirmOk();

        /// <summary>
        /// 确认取消
        /// </summary>
        void ConfirmCancel();

        /// <summary>
        /// 获得显示内容
        /// </summary>
        /// <returns></returns>
        string GetDisplayContent();

    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageStyle
    {
        /// <summary>
        /// 一般消息，
        /// </summary>
        Ordinary,

        /// <summary>
        /// 带闪框的消息
        /// </summary>
        FlashFrame,
    }
}

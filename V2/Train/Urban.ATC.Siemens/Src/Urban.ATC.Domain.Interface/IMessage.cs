using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Motor.ATP.Domain.Interface.Infomation;

namespace Motor.ATP.Domain.Interface
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


        bool CanNavigateUp { get; }

        bool CanNavigateDown { get; }

        /// <summary>
        /// 正在显示的
        /// </summary>
        IEnumerable<IMessageItem> ShowingMessageCollection { get; }

        /// <summary>
        /// 消息集合
        /// </summary>
        ObservableCollection<IMessageItem> MessageCollection { get; }

    }

    public interface IMessageItem : IIdentityProvider<IInfomationItemContent>, INotifyPropertyChanged
    {
        /// <summary>
        /// 消息来源
        /// </summary>
        IInfomationItem InfomationItem { get; }

        /// <summary>
        /// 时间戳
        /// </summary>
        DateTime TimeStamp { get; }

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

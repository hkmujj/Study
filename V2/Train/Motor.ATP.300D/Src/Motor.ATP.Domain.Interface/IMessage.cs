using System;
using System.Collections.ObjectModel;

namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 消息区的消息
    /// </summary>
    public interface IMessage : IATPPartial
    {
        /// <summary>
        /// 消息集合
        /// </summary>
        ObservableCollection<IMessageItem> MessageCollection { get; }
    }

    public interface IMessageItem : IIdentityProvide<int>
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        DateTime TimeStamp { get; }

        /// <summary>
        /// 消息样式 
        /// </summary>
        MessageStyle Style { get; }

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

    }

    public enum MessageStyle
    {
        Normal,

        Ok,

        OkCancel,
    }
}

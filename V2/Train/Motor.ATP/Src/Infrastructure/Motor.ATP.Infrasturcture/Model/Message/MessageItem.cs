using System;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Infomation;

namespace Motor.ATP.Infrasturcture.Model.Message
{
    /// <summary>
    /// 消息的显示类，用于直接binding的类
    /// </summary>
    public class MessageItem : NotificationObject, IMessageItem, ICloneable<MessageItem>
    {
        private DateTime? m_TimeStamp;
        private MessageStyle m_Style;
        private string m_Content;

        object IIdentityProvider.Identity
        {
            get { return Identity; }
        }

        /// <summary>
        /// Identity 唯一标识 
        /// </summary>
        public IInfomationItemContent Identity
        {
            get { return InfomationItem.Content; }
        }

        /// <summary>
        /// 消息来源
        /// </summary>
        public IInfomationItem InfomationItem { get; set; }

        public DateTime? TimeStamp
        {
            get { return m_TimeStamp; }
            set
            {
                m_TimeStamp = value;
                RaisePropertyChanged(() => TimeStamp);
            }
        }

        /// <summary>
        /// OK 被确认
        /// </summary>
        public event Action<MessageItem> OkConfirmed;

        /// <summary>
        /// cancel 被确认
        /// </summary>
        public event Action<MessageItem> CancelConfirmed;


        /// <summary>
        /// 时间差,如果需要记时，用此字段。 = -infomation.StartTime 或者 0
        /// </summary>
        public TimeSpan TimeDifference { set; get; }

        /// <summary>
        /// 消息样式 
        /// </summary>
        public MessageStyle Style
        {
            get { return m_Style; }
            set
            {
                m_Style = value;
                RaisePropertyChanged(() => Style);
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return m_Content; }
            set
            {
                m_Content = value;
                RaisePropertyChanged(() => Content);
            }
        }

        /// <summary>
        /// 确认OK
        /// </summary>
        public virtual void ConfirmOk()
        {
            OnOkConfirmed(this);
        }

        /// <summary>
        /// 确认取消
        /// </summary>
        public virtual void ConfirmCancel()
        {
            OnCancelConfirmed(this);
        }

        public string GetDisplayContent()
        {
            if (TimeStamp.HasValue)
            {
                return TimeStamp.Value.ToString("T") + "  " + Content;
            }

            return Content;
        }

        protected virtual void OnCancelConfirmed(MessageItem obj)
        {
            Action<MessageItem> handler = CancelConfirmed;
            if (handler != null)
            {
                handler(obj);
            }
        }

        protected virtual void OnOkConfirmed(MessageItem obj)
        {
            Action<MessageItem> handler = OkConfirmed;
            if (handler != null)
            {
                handler(obj);
            }
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public MessageItem Clone()
        {
            var msg = new MessageItem()
            {
                TimeStamp = DateTime.Now,
                TimeDifference =  TimeDifference,
                Content = Content,
                Style = Style,
                InfomationItem = InfomationItem
            };

            msg.OkConfirmed += OnOkConfirmed;

            msg.CancelConfirmed += OnCancelConfirmed;

            return msg;
        }
    }
}
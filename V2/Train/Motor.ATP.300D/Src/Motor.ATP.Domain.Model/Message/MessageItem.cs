using System;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.Message
{
    public class MessageItem :  IMessageItem, ICloneable<MessageItem>
    {
        object IIdentityProvide.Identity
        {
            get { return Identity; }
        }

        /// <summary>
        /// Identity 唯一标识 
        /// </summary>
        public int Identity { get; set; }

        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// OK 被确认
        /// </summary>
        public event Action<MessageItem> OkConfirmed;

        /// <summary>
        /// cancel 被确认
        /// </summary>
        public event Action<MessageItem> CancelConfirmed;


        /// <summary>
        /// 消息样式 
        /// </summary>
        public MessageStyle Style { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

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
            var msg = new MessageItem() { Identity = Identity, TimeStamp = DateTime.Now, Content = Content, Style = Style };

            msg.OkConfirmed += OnOkConfirmed;

            msg.CancelConfirmed += OnCancelConfirmed;

            return msg;
        }
    }
}
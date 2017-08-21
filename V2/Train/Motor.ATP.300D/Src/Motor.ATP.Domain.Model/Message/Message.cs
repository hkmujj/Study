using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.Message
{
    public class Message : ATPPartialBase, IMessage
    {
        private ObservableCollection<IMessageItem> m_MessageCollection;

        protected Dictionary<int, MessageItem> m_MessageItemTemperateDictionary;

        public Message()
            : base(null)
        {
            m_MessageCollection = new ObservableCollection<IMessageItem>();
            // TODO recorde by file 
            m_MessageItemTemperateDictionary = new Dictionary<int, MessageItem>()
                                               {
                                                   {
                                                       1, new MessageItem()
                                                          {
                                                              Identity = 1,
                                                              Style = MessageStyle.Normal,
                                                              Content = "消息1消息2消息3消息4消息5消息6消息7消息8",
                                                              TimeStamp = DateTime.Now,
                                                          }

                                                   },
                                                   {
                                                       2, new MessageItem()
                                                          {
                                                              Identity = 1,
                                                              Style = MessageStyle.Normal,
                                                              Content = "消息1消息2消息",
                                                              TimeStamp = DateTime.Now,
                                                          }
                                                   }
                                               };
        }

        /// <summary>
        /// 根据 ID 生成消息元素
        /// </summary>
        /// <param name="id"></param>
        public virtual MessageItem GenerateMessageItem(int id)
        {
            if (m_MessageItemTemperateDictionary == null)
            {
                throw new ArgumentNullException("m_MessageItemTemperateDictionary");
            }

            if (!m_MessageItemTemperateDictionary.ContainsKey(id))
            {
                throw new KeyNotFoundException(string.Format("can not found messageitem where id = {0}", id));
            }
            var msgTemp = m_MessageItemTemperateDictionary[id];

            var msg = msgTemp.Clone();

            m_MessageCollection.Add(msg);

            return msg;

        }

        public ObservableCollection<IMessageItem> MessageCollection
        {
            get { return m_MessageCollection; }
            set { m_MessageCollection = value;  }
        }
    }
}

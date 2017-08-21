using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.Infomation;
using Motor.ATP.Domain.Model.Infomation;

namespace Motor.ATP.Domain.Model.Message
{
    public class Message : ATPPartialBase, IMessage
    {
        public const int MaxShowingCount = 4;

        private ObservableCollection<IMessageItem> m_MessageCollection;

        protected Dictionary<int, MessageItem> m_MessageItemTemperateDictionary;
        private int m_CurrentFirstIndex;
        private int m_ShowingCount;
        private bool m_Visible;
        private IMessageItem m_CurrentFirstItem;

        public Message(ATPDomain atp)
            : base(atp)
        {
            Visible = true;
            MessageCollection = new ObservableCollection<IMessageItem>();
            MessageCollection.CollectionChanged +=
                (sender, args) =>
                {
                    CurrentFirstIndex = 0;
                    RaisePropertyChanged(() => CanNavigateDown);
                    RaisePropertyChanged(() => CanNavigateUp);
                    RaisePropertyChanged(() => ShowingMessageCollection);
                };
            ShowingCount = MaxShowingCount;
            atp.InfomationService.InfomationBegin += InfomationServiceOnInfomationBegin;
            atp.InfomationService.InfomationEnd += InfomationServiceOnInfomationEnd;
        }

        private void InfomationServiceOnInfomationEnd(IInfomationItem infomationItem)
        {
            MessageCollection.Remove(
                MessageCollection.LastOrDefault(f => f.Identity == infomationItem.Identity));
        }

        private void InfomationServiceOnInfomationBegin(IInfomationItem infomationItem)
        {
            var item = GenerateMessageItem(infomationItem);
            if (
                MessageCollection.LastOrDefault(
                    f => !f.InfomationItem.Content.IsOnlyTextInfo() && f.Style != MessageStyle.Ordinary) != null)
            {
                MessageCollection.Insert(MessageCollection.Count - 1, item);
            }
            else
            {
                MessageCollection.Add(item);
            }
        }

        /// <summary>
        /// 根据 infomation 生成消息元素
        /// </summary>
        public virtual IMessageItem GenerateMessageItem(IInfomationItem infomation)
        {
            return infomation.ToMessageItem();
        }

        public int CurrentFirstIndex
        {
            get { return m_CurrentFirstIndex; }
            set
            {
                m_CurrentFirstIndex = value;
                CurrentFirstItem = ShowingMessageCollection.FirstOrDefault();
                RaisePropertyChanged(() => CurrentFirstIndex);
                RaisePropertyChanged(() => CanNavigateDown);
                RaisePropertyChanged(() => CanNavigateUp);
                RaisePropertyChanged(() => ShowingMessageCollection);
            }
        }

        public IMessageItem CurrentFirstItem
        {
            get { return m_CurrentFirstItem; }
            private set
            {
                if (Equals(value, m_CurrentFirstItem))
                {
                    return;
                }
                m_CurrentFirstItem = value;
                RaisePropertyChanged(() => CurrentFirstItem);
            }
        }

        public int ShowingCount
        {
            get { return m_ShowingCount; }
            set
            {
                if (value == m_ShowingCount)
                {
                    return;
                }
                if (value < 0 )
                {
                    m_ShowingCount = 0;
                }
                if (value > MaxShowingCount)
                {
                    m_ShowingCount = MaxShowingCount;
                }
                m_ShowingCount = value;
                RaisePropertyChanged(() => ShowingCount);
                RaisePropertyChanged(() => CanNavigateDown);
                RaisePropertyChanged(() => ShowingMessageCollection);
            }
        }

        public bool CanNavigateUp { get { return CurrentFirstIndex > 0; } }

        public bool CanNavigateDown { get { return CurrentFirstIndex + ShowingCount < MessageCollection.Count; } }

        public IEnumerable<IMessageItem> ShowingMessageCollection
        {
            get { return MessageCollection.Reverse().Skip(CurrentFirstIndex).Take(ShowingCount); }
        }

        public ObservableCollection<IMessageItem> MessageCollection
        {
            get { return m_MessageCollection; }
            private set
            {
                m_MessageCollection = value;
                RaisePropertyChanged(() => MessageCollection);
                RaisePropertyChanged(() => ShowingMessageCollection);
            }
        }

        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (value == m_Visible)
                {
                    return;
                }
                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
        }
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Model.Infomation;

namespace Motor.ATP.Infrasturcture.Model.Message
{
    public class Message : ATPPartialBase, IMessage
    {
        public const int MaxShowingCount = 4;

        private ObservableCollection<IMessageItem> m_MessageCollection;

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        protected readonly List<IMessageItem> m_TimeChangableMessageCache;
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        protected readonly object m_TimeChangableMessageCacheLocker;

        private int m_CurrentFirstIndex;
        private int m_ShowingCount;
        private bool m_Visible;
        private bool m_CanNavigateUpVisible;
        private bool m_CanNavigateDownVisible;    
        private IMessageItem m_CurrentFirstItem;

        private readonly Predicate<IMessageItem> m_ShowingMessageFilter;

        public Message(ATPDomain atp, Predicate<IMessageItem> showingMessageFilter = null)
            : base(atp)
        {
            m_ShowingMessageFilter = showingMessageFilter ?? (msg => true);

            Visible = true;
            CanNavigateDownVisible = true;
            CanNavigateUpVisible = true;
            
            if (atp.InfomationService.GetInformationCreater().HasAnyNeedTimeInfomation())
            {
                m_TimeChangableMessageCache = new List<IMessageItem>();
                m_TimeChangableMessageCacheLocker = new object();
                atp.GlobalParam.GlobalTimer.Timer1S += GlobalTimerOnTimer1S;
            }

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

        protected virtual void GlobalTimerOnTimer1S(object sender, EventArgs eventArgs)
        {
            if (m_TimeChangableMessageCache != null)
            {
                lock (m_TimeChangableMessageCacheLocker)
                {
                    m_TimeChangableMessageCache.ForEach(e =>
                    {
                        try
                        {
                            e.TimeStamp = ATP.Other.ShowingDateTime + e.TimeDifference;
                            ATP.SendInterface.OnMessageTimeChanged(e);
                        }
                        catch (Exception)
                        {
                        }
                    });
                }
            }
        }

        protected virtual void InfomationServiceOnInfomationEnd(IInfomationItem infomationItem)
        {
            var item = MessageCollection.LastOrDefault(f => f.Identity == infomationItem.Identity);
            MessageCollection.Remove(item);

            if (m_TimeChangableMessageCache != null)
            {
                lock (m_TimeChangableMessageCacheLocker)
                {
                    m_TimeChangableMessageCache.Remove(item);
                }
            }
        }

        protected virtual void InfomationServiceOnInfomationBegin(IInfomationItem infomationItem)
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

            if (m_TimeChangableMessageCache != null && item.InfomationItem.Content.TimeShowType.NeedTime())
            {
                lock (m_TimeChangableMessageCacheLocker)
                {
                    m_TimeChangableMessageCache.Add(item);
                }
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

        public bool CanNavigateDown { get { return CurrentFirstIndex + ShowingCount < MessageCollection.Count(w => m_ShowingMessageFilter(w)); } }

        public IEnumerable<IMessageItem> ShowingMessageCollection
        {
            get { return MessageCollection.Where(w => m_ShowingMessageFilter(w)).Reverse().Skip(CurrentFirstIndex).Take(ShowingCount); }
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

        public bool CanNavigateUpVisible
        {
            get { return m_CanNavigateUpVisible; }
            set
            {
                if (value == m_CanNavigateUpVisible)
                {
                    return;
                }
                m_CanNavigateUpVisible = value;
                RaisePropertyChanged(() => CanNavigateUpVisible);
            }
        }

        public bool CanNavigateDownVisible
        {
            get { return m_CanNavigateDownVisible; }
            set
            {
                if (value == m_CanNavigateDownVisible)
                {
                    return;
                }
                m_CanNavigateDownVisible = value;
                RaisePropertyChanged(() => CanNavigateDownVisible);
            }
        }
    }
}

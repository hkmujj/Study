using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Infomation;

namespace Motor.ATP.Infrasturcture.Control.Infomation
{
    public class InfomationService : ATPPartialBase, IInfomationService
    {

        public event Action<IInfomationItem> InfomationBegin;

        public event Action<IInfomationItem> InfomationEnd;
        public event Action<IInfomationItem> InfomationEnsured;
        public event Action AllInfomationEnsureCompleted;

        public IInfomationItem CurrentEnsureingInfomation
        {
            get
            {
               return m_EnsureInfomationCache.GetCurrentEnsuringItemContent();
            }
        }

        public IInformationCreater InformationCreater { get; set; }

        private readonly IEnsureInfomationCache m_EnsureInfomationCache;

        private readonly List<IInfomationItem> m_CreatedInfomationItems;

        public InfomationService(ATPDomain parent, IInformationCreater informationCreater)
            : base(parent)
        {
            Contract.Requires(informationCreater != null);
            InformationCreater = informationCreater;
            m_EnsureInfomationCache = new QueueEnsureInfomationCache();
            m_CreatedInfomationItems = new List<IInfomationItem>();
            informationCreater.InfomationCreated += InformationCreaterOnInfomationCreated;
            informationCreater.InfomationDeleted += InformationCreaterOnInfomationDeleted;
            informationCreater.InfomationEnsured += InformationCreaterOnInfomationEnsured;
            informationCreater.InfomationCleared += InformationCreaterOnInfomationCleared;
        }


        public void Initalize()
        {
            InformationCreater.Initalize();
        }


        public IInfomationItem EnsureCurrentInfomation(InformationEnsureParam ensureParam = null)
        {
            var current = CurrentEnsureingInfomation;

            if (current == null)
            {
                return current;
            }

            current = EnsureMsgItem(current.Content);

            if (CurrentEnsureingInfomation != null)
            {
                OnInfomationBegin(CurrentEnsureingInfomation);
            }

            return current;
        }

        /// <summary>
        /// 确认指定消息
        /// </summary>
        /// <param name="informations"></param>
        /// <returns></returns>
        public ReadOnlyCollection<IInfomationItem> EnsureInfomationItems(IEnumerable<IInfomationItemContent> informations)
        {
            if (informations == null || informations == Enumerable.Empty<IInfomationItemContent>())
            {
                return null;
            }

            var ensured  = informations.Select(EnsureMsgItem).Where(w => w != null).ToList();

            if (CurrentEnsureingInfomation != null)
            {
                OnInfomationBegin(CurrentEnsureingInfomation);
            }
            else
            {
                OnAllInfomationEnsureCompleted();
            }

            return ensured.AsReadOnly();
        }

        private IInfomationItem EnsureMsgItem(IInfomationItemContent info)
        {
            var msg = ATP.Message.MessageCollection.LastOrDefault(l => l.InfomationItem.Content == info);
            if (msg != null)
            {
                msg.Style = MessageStyle.Ordinary;

                OnInfomationEnsured(msg.InfomationItem);

                if (msg.InfomationItem.Content.AutoCancelType.DeleteAfterOk())
                {
                    ATP.Message.MessageCollection.Remove(msg);
                }

                m_EnsureInfomationCache.Remove(msg.InfomationItem);

                return msg.InfomationItem;
            }

            return null;
        }

        private void InformationCreaterOnInfomationDeleted(ReadOnlyCollection<IInfomationItemContent> infomationItems)
        {
            foreach (var item in infomationItems.Reverse())
            {
                var info = new InfomationItem(item);
                if (!item.IsOnlyTextInfo())
                {
                    m_EnsureInfomationCache.Remove(info);
                }
                OnInfomationEnd(info);
            }
        }


        private void InformationCreaterOnInfomationEnsured(ReadOnlyCollection<IInfomationItemContent> infomationItemContents)
        {
            EnsureInfomationItems(infomationItemContents);
        }

        private void InformationCreaterOnInfomationCleared()
        {
            foreach (var item in m_CreatedInfomationItems)
            {
                OnInfomationEnd(item);
            }

            m_EnsureInfomationCache.Clear();
            m_CreatedInfomationItems.Clear();
        }

        private void InformationCreaterOnInfomationCreated(ReadOnlyCollection<IInfomationItemContent> infomationItems)
        {
            foreach (var item in infomationItems)
            {
                var info = new InfomationItem(item) {StartTime = Parent.Other.ShowingDateTime};
                if (!item.IsOnlyTextInfo())
                {
                    if (CurrentEnsureingInfomation != null)
                    {
                        m_EnsureInfomationCache.Add(info);
                    }
                    else
                    {
                        m_EnsureInfomationCache.Add(info);
                        OnInfomationBegin(info);
                    }
                }
                else
                {
                    OnInfomationBegin(info);
                }
            }
        }

        protected virtual void OnInfomationBegin(IInfomationItem infomation)
        {
            var handler = InfomationBegin;
            if (handler != null)
            {
                m_CreatedInfomationItems.Add(infomation);
                handler(infomation);
            }
        }

        protected virtual void OnInfomationEnd(IInfomationItem infomation)
        {
            var handler = InfomationEnd;
            if (handler != null)
            {
                handler(infomation);
            }
        }


        public void SetInformationCreater(IInformationCreater infomationCreater)
        {
            if (InformationCreater != null)
            {
                InformationCreater.InfomationCreated -= InformationCreaterOnInfomationCreated;
                InformationCreater.InfomationDeleted -= InformationCreaterOnInfomationDeleted;
                InformationCreater.InfomationEnsured -= InformationCreaterOnInfomationEnsured;
            }

            InformationCreater = infomationCreater;

            if (InformationCreater != null)
            {
                InformationCreater.InfomationCreated += InformationCreaterOnInfomationCreated;
                InformationCreater.InfomationDeleted += InformationCreaterOnInfomationDeleted;
                InformationCreater.InfomationEnsured += InformationCreaterOnInfomationEnsured;
            }
        }


        public IInformationCreater GetInformationCreater()
        {
            //此处待钟成义完善
            return InformationCreater;
        }

        protected virtual void OnInfomationEnsured(IInfomationItem obj)
        {
            var handler = InfomationEnsured;
            if (handler != null)
                handler(obj);
        }

        protected virtual void OnAllInfomationEnsureCompleted()
        {
            var handler = AllInfomationEnsureCompleted;
            if (handler != null)
                handler();
        }
    }
}
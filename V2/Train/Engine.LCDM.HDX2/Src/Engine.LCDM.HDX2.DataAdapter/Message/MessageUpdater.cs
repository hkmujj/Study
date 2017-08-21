using System;
using System.Collections.Specialized;
using System.Linq;
using System.Timers;
using Engine.LCDM.HDX2.Entity.Model.Domain;

namespace Engine.LCDM.HDX2.DataAdapter.Message
{
    public class MessageUpdater : IDisposable
    {
        private MessageManager m_MessageManager;

        private LCDMModel m_DomainModel;

        private readonly Timer m_UpdateMessageTimer;

        private readonly object m_Locker = new object();

        public MessageUpdater(MessageManager messageManager, LCDMModel domainModel)
        {
            m_MessageManager = messageManager;
            m_DomainModel = domainModel;

            m_MessageManager.OccursedMessageCollection.CollectionChanged += OccursedMessageCollectionOnCollectionChanged;

            m_UpdateMessageTimer = new Timer(3500) { AutoReset = true };
            m_UpdateMessageTimer.Elapsed += (sender, args) => UpdateMessage();
        }

        private void OccursedMessageCollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            lock (m_Locker)
            {
                if (!m_MessageManager.OccursedMessageCollection.Contains(m_DomainModel.CurrentMessage))
                {
                    m_DomainModel.CurrentMessage = m_MessageManager.OccursedMessageCollection.FirstOrDefault();
                }
            }
        }

        public void Run()
        {
            m_UpdateMessageTimer.Start();
        }

        private void UpdateMessage()
        {
            lock (m_Locker)
            {
                if (m_DomainModel.CurrentMessage == null)
                {
                    m_DomainModel.CurrentMessage = m_MessageManager.OccursedMessageCollection.FirstOrDefault();
                }
                else
                {
                    var next = (m_MessageManager.OccursedMessageCollection.IndexOf(m_DomainModel.CurrentMessage) + 1)%
                               m_MessageManager.OccursedMessageCollection.Count;
                    m_DomainModel.CurrentMessage = m_MessageManager.OccursedMessageCollection[next];
                }
            }
        }

        public void Dispose()
        {
            m_UpdateMessageTimer.Stop();
        }
    }
}
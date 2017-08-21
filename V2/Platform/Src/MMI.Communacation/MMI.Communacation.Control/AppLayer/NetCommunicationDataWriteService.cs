using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MMI.Communacation.Control.AppLayer.SendDataStrategy;
using MMI.Communacation.Interface.AppLayer;
using MMI.Facility.DataType.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Communacation.Control.AppLayer
{
    class NetCommunicationDataWriteService : CommunicatonDataWriteServiceBase
    {

        private static readonly Dictionary<string, Sender> SendGroupTimerDictionary = new Dictionary<string, Sender>();

        public NetCommunicationDataWriteService(IConfig config, ICommunacationDataSender netService,INetProjectDataConfig netProjectDataConfig , CommunicationDataEntity dataEntity,
            string sendTimerGroup)
            : base(config, netProjectDataConfig, dataEntity)
        {
            if (!SendGroupTimerDictionary.ContainsKey(sendTimerGroup))
            {
                var sernder = new Sender(netService, this, config, netProjectDataConfig);
                SendGroupTimerDictionary.Add(sendTimerGroup, sernder);
            }
        }

        public override void Dispose()
        {
            if (SendGroupTimerDictionary.Any())
            {
                foreach (var kvp in SendGroupTimerDictionary)
                {
                    kvp.Value.Dispose();
                }
                SendGroupTimerDictionary.Clear();
            }
            base.Dispose();
        }

        internal class  Sender : IDisposable
        {
            private readonly Timer m_SendDataTimer;

            private readonly IDataSendStrategy m_SendDataStrategy;

            private bool m_Sending;

            private readonly object m_SendLocker = new object();

            public Sender(ICommunacationDataSender netService, NetCommunicationDataWriteService writeService,
                IConfig config, INetProjectDataConfig netProjectDataConfig)
            {
                m_Sending = false;

                m_SendDataTimer = new Timer(SendDatas, null, 1000, 20);

                m_SendDataStrategy =
                    DataSendStrategyFactory.Instance.CreateDataSendStrategy(
                        new DataSendStrategyFactory.CreateParam(netService, writeService, config, netProjectDataConfig));
            }

            private void SendDatas(object state)
            {
                if (!m_Sending)
                {
                    lock (m_SendLocker)
                    {
                        if (!m_Sending)
                        {
                            m_Sending = true;
                            m_SendDataStrategy.SendDatas();
                            m_Sending = false;
                        }
                    }
                }
            }

            public void Dispose()
            {
                m_SendDataTimer.Dispose();
            }
        }
    }
}

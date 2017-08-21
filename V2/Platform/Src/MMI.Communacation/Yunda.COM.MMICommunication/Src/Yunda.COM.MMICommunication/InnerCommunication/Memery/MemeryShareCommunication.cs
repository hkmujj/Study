using System;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Threading.Tasks;
using Yunda.COM.MMICommunication.Model;

namespace Yunda.COM.MMICommunication.InnerCommunication.Memery
{
    class MemeryShareCommunication : IInnerCommunication, IDisposable
    {
        public const string MemeryNamePrefix = "Yunda.COM.MMICommunication.InnerCommunication.Memery+";

        public const string HostInfName = MemeryNamePrefix + "Infloats";
        public const string HostInbName = MemeryNamePrefix + "Inbools";
        public const string HostOufName = MemeryNamePrefix + "Outfloats";
        public const string HostOubName = MemeryNamePrefix + "Outbools";
        public const string ToMMISemaphoreName = MemeryNamePrefix + "ToMMISemaphoreName";
        public const string ToOthSemaphoreName = MemeryNamePrefix + "ToOthSemaphoreName";

        private MemoryMappedFile m_ToOthfMappedFile;
        private MemoryMappedFile m_ToOthbMappedFile;
        private MemoryMappedFile m_ToMMIfMappedFile;
        private MemoryMappedFile m_ToMMIbMappedFile;

        private IInitalizeParam m_InitalizeParam;

        private EventWaitHandle m_ToMMIEvent;
        private EventWaitHandle m_ToOthEvent;

        private bool m_IsActiving;

        private Task m_MonitorDataTask;

        public event Action<byte[], float[]> Received;

        private EventWaitHandle m_MonitorDataEvent;
        private EventWaitHandle m_SendDataEvent;


        private MemoryMappedViewAccessor m_ReadFStream;
        private MemoryMappedViewAccessor m_ReadBStream;

        private MemoryMappedViewAccessor m_WriteFStream;
        private MemoryMappedViewAccessor m_WriteBStream;

        private readonly object m_ReadLocker = new object();
        private readonly object m_WriteLocker = new object();

        private byte[] m_SendBBuffer;
        private float[] m_SendFBuffer;
        private byte[] m_RecvBBuffer;
        private float[] m_RecvFBuffer;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initalize(IInitalizeParam initalizeParam)
        {
            m_InitalizeParam = initalizeParam;
            m_IsActiving = true;

            m_ToOthfMappedFile = MemoryMappedFile.CreateOrOpen(HostInfName, 600*8);
            m_ToOthbMappedFile = MemoryMappedFile.CreateOrOpen(HostInbName, 4800/8);
            m_ToMMIfMappedFile = MemoryMappedFile.CreateOrOpen(HostOufName, 200*8);
            m_ToMMIbMappedFile = MemoryMappedFile.CreateOrOpen(HostOubName, 1600/8);
            m_ToMMIEvent = new EventWaitHandle(true, EventResetMode.ManualReset, ToMMISemaphoreName);
            m_ToOthEvent = new EventWaitHandle(true, EventResetMode.ManualReset, ToOthSemaphoreName);

            if (initalizeParam.IsHost)
            {
                m_MonitorDataEvent = m_ToMMIEvent;
                m_SendDataEvent = m_ToOthEvent;

                m_ReadBStream = m_ToMMIbMappedFile.CreateViewAccessor();
                m_ReadFStream = m_ToMMIfMappedFile.CreateViewAccessor();
                m_WriteBStream = m_ToOthbMappedFile.CreateViewAccessor();
                m_WriteFStream = m_ToOthfMappedFile.CreateViewAccessor();

                m_SendBBuffer = new byte[4800/8];
                m_SendFBuffer = new float[600];

                m_RecvBBuffer = new byte[1600/8];
                m_RecvFBuffer = new float[200];
            }
            else
            {
                m_MonitorDataEvent = m_ToOthEvent;
                m_SendDataEvent = m_ToMMIEvent;
                m_ReadBStream = m_ToOthbMappedFile.CreateViewAccessor();
                m_ReadFStream = m_ToOthfMappedFile.CreateViewAccessor();
                m_WriteBStream = m_ToMMIbMappedFile.CreateViewAccessor();
                m_WriteFStream = m_ToMMIfMappedFile.CreateViewAccessor();

                m_SendBBuffer = new byte[1600 / 8];
                m_SendFBuffer = new float[200 ];

                m_RecvBBuffer = new byte[4800 / 8];
                m_RecvFBuffer = new float[600 ];
            }
            
            m_MonitorDataTask = new Task(OnMinitorData);
            m_MonitorDataTask.Start();
        }

        private void OnMinitorData()
        {
            while (m_IsActiving)
            {
                m_MonitorDataEvent.WaitOne();
                m_MonitorDataEvent.Reset();
                if (m_IsActiving)
                {
                    ThreadPool.QueueUserWorkItem(DecodeAndNotifyData);
                }
            }
        }

        private void DecodeAndNotifyData(object state)
        {
            lock (m_ReadLocker)
            {
                m_ReadBStream.ReadArray(0, m_RecvBBuffer, 0, m_RecvBBuffer.Length);
                m_ReadFStream.ReadArray(0, m_RecvFBuffer, 0, m_RecvFBuffer.Length);
                OnReceived(m_RecvBBuffer, m_RecvFBuffer);
            }
        }

        public void Uninitalize()
        {
            m_ToMMIEvent.Set();
            m_ToOthEvent.Set();

            m_IsActiving = false;

            Dispose();
        }

        public void Send(byte[] boolBytes, float[] floats)
        {
            lock (m_WriteLocker)
            {
                m_WriteBStream.WriteArray(0, boolBytes, 0, boolBytes.Length);
                m_WriteFStream.WriteArray(0 , floats, 0, floats.Length);
                m_SendDataEvent.Set();
            }
        }

        /// <summary>执行与释放或重置非托管资源相关的应用程序定义的任务。</summary>
        public void Dispose()
        {
        }

        protected virtual void OnReceived(byte[] arg1, float[] arg2)
        {
            if (Received != null)
            {
                Received(arg1, arg2);
            }
        }
    }
}
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using CommonUtil.Util;
using MMI.Communacation.Interface.AppLayer;
using MMI.Facility.Interface;

namespace MMI.Communacation.Control.PresentationLayer.BroadcastStragy
{
    internal class LazyRecvRealTimeDataBroadcastStragy : RecvRealTimeDataBroadcastStragy
    {

        private readonly ConcurrentDictionary<ProjectType, ProjectRecvDataModel> m_ProjectRecvDataModels;

        private volatile bool m_CourseEndFlag = false;

        public LazyRecvRealTimeDataBroadcastStragy(IIPresentationLayerDataBroadcastProvider broadcastProvider)
            : base(broadcastProvider)
        {
            var all = Enum.GetValues(typeof(ProjectType))
                .Cast<ProjectType>().Distinct().ToArray();

            m_ProjectRecvDataModels =
                new ConcurrentDictionary<ProjectType, ProjectRecvDataModel>(all.ToDictionary(kvp => kvp,
                    kvp => new ProjectRecvDataModel(this, kvp)));
        }

        public override void OnDataReceived(ProjectType type, NetDatas data)
        {
            if (!m_CourseEndFlag)
            {
                m_ProjectRecvDataModels[type].OnDataReceived(data);
            }
        }

        public override void Dispose()
        {
            foreach (var kvp in m_ProjectRecvDataModels)
            {
                kvp.Value.Dispose();
            }
        }

        public override void OnCourseStarted()
        {
            m_CourseEndFlag = false;
        }

        public override void OnCourseStopped()
        {
            m_CourseEndFlag = true;
            foreach (var kv in m_ProjectRecvDataModels)
            {
                kv.Value.ClearDataBuffer();
            }
        }


        private class ProjectRecvDataModel : IDisposable
        {
            private readonly LazyRecvRealTimeDataBroadcastStragy m_BroadcastStragy;

            private volatile bool m_RecordCompleted;

            private readonly ConcurrentQueue<RecvDataRecordModel> m_RecordedModelCollection;

            /// <summary>
            /// 准备广播的
            /// </summary>
            private readonly NetDatas m_DataBuffer;

            private readonly ProjectType m_ProjectType;

            private readonly object m_BufferLocker = new object();

            private readonly object m_OperateLocker = new object();

            private readonly ConcurrentQueue<NetDatas> m_NetDataseQueue;

            private Thread m_BroadcasThread;

            private volatile bool m_Broadcasting;

            /// <summary>
            /// 广播之前的
            /// </summary>
            private SortedSet<NetDatas> m_BeforeSendNetDataBuffer;

            public ProjectRecvDataModel(LazyRecvRealTimeDataBroadcastStragy broadcastStragy, ProjectType projectType)
            {
                m_BroadcastStragy = broadcastStragy;
                m_ProjectType = projectType;
                m_RecordCompleted = false;
                m_DataBuffer = new NetDatas(new ReceiveDataModel<bool>() { StartIndex = 0, DataList = new List<bool>() },
                    new ReceiveDataModel<float>() { StartIndex = 0, DataList = new List<float>() }, projectType);
                m_RecordedModelCollection = new ConcurrentQueue<RecvDataRecordModel>();
                m_NetDataseQueue = new ConcurrentQueue<NetDatas>();
                m_Broadcasting = true;
                m_BroadcasThread = new Thread(QueueBroadcast);
                m_BroadcasThread.Start();
            }

            private void QueueBroadcast()
            {
                while (m_Broadcasting)
                {
                    lock (m_OperateLocker)
                    {
                        if (m_RecordedModelCollection.Any() &&m_NetDataseQueue.Count >= m_RecordedModelCollection.Count)
                        {
                            for (int i = 0; i < m_RecordedModelCollection.Count; i++)
                            {
                                NetDatas data;

                                if (m_NetDataseQueue.TryDequeue(out data))
                                {
                                    m_BeforeSendNetDataBuffer.Add(data);
                                }
                            }

                            if (m_BeforeSendNetDataBuffer.Count != m_RecordedModelCollection.Count)
                            {
                                LogMgr.Error("收到的网络数据没有正常广播");
                                m_BeforeSendNetDataBuffer.Clear();
                                continue;
                            }

                            foreach (var datase in m_BeforeSendNetDataBuffer)
                            {
                                AddDataToBuffer(datase);
                            }

                            m_BeforeSendNetDataBuffer.Clear();

                            BroadcastDataRecvThenClearBuffer();
                        }
                        else
                        {
                            Thread.Sleep(20);
                        }
                    }
                }
            }

            public void OnDataReceived(NetDatas data)
            {

                if (m_RecordCompleted)
                {
                    m_NetDataseQueue.Enqueue(data);
                }
                else
                {
                    var first = m_RecordedModelCollection.FirstOrDefault();
                    if (first != null)
                    {
                        if (first.BoolStartIndex != data.ReceivedBools.StartIndex)
                        {
                            m_RecordedModelCollection.Enqueue(new RecvDataRecordModel(m_ProjectType,
                                data.ReceivedBools.StartIndex));
                        }
                        else
                        {
                            m_RecordCompleted = true;
                            m_BeforeSendNetDataBuffer = new SortedSet<NetDatas>(new NetDataRecvBoolStartIndexCompare());
                        }
                    }
                    else
                    {
                        if (data.ReceivedBools.StartIndex == 0)
                        {
                            m_RecordedModelCollection.Enqueue(new RecvDataRecordModel(m_ProjectType,
                                data.ReceivedBools.StartIndex));
                        }
                    }
                }
            }

            private void AddDataToBuffer(NetDatas data)
            {
                lock (m_BufferLocker)
                {
                    m_DataBuffer.ReceivedBools.DataList.AddRange(data.ReceivedBools.DataList);

                    m_DataBuffer.ReceivedFloats.DataList.AddRange(data.ReceivedFloats.DataList);
                }
            }

            private void BroadcastDataRecvThenClearBuffer()
            {
                m_BroadcastStragy.BroadcastProvider.OnDataReceived(m_ProjectType, m_DataBuffer);

                ClearDataBuffer();
            }

            public void ClearDataBuffer()
            {
                lock (m_BufferLocker)
                {
                    m_DataBuffer.ReceivedBools.DataList.Clear();
                    m_DataBuffer.ReceivedFloats.DataList.Clear();
                }
            }

            public void Dispose()
            {
                m_Broadcasting = false;
            }
        }

        private class RecvDataRecordModel
        {
            [DebuggerStepThrough]
            public RecvDataRecordModel(ProjectType projectType, int boolStartIndex)
            {
                ProjectType = projectType;
                BoolStartIndex = boolStartIndex;
            }

            public ProjectType ProjectType { private set; get; }

            public int BoolStartIndex { private set; get; }
        }

    }
}
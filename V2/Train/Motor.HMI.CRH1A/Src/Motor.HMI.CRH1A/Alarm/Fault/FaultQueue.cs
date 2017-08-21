using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Motor.HMI.CRH1A.Alarm.Fault
{
    /// <summary>
    /// 故障队列
    /// </summary>
    public class FaultQueue
    {
        /// <summary>
        /// 需要弹框, 界面不响应的故障
        /// </summary>
        private readonly Queue<ExceptionData> m_PupupFault;

        /// <summary>
        /// A 类故障, 需要切换界面的故障,界面可以响应
        /// </summary>
        private readonly Queue<ExceptionData> m_ATypeFault;

        /// <summary>
        /// 用户自定义的
        /// </summary>
        private readonly Queue<ExceptionData> m_UserDefineFault;

        /// <summary>
        /// 一般故障,只需要界面显示就可
        /// </summary>
        private readonly Queue<ExceptionData> m_NormalFault;

        /// <summary>
        /// 故障历史
        /// </summary>
        private readonly List<ExceptionData> m_FaultHistory;

        /// <summary>
        /// 手动故障
        /// </summary>
        private readonly List<ExceptionData> m_ManualFault;

        /// <summary>
        /// 故障历史
        /// </summary>
        public ReadOnlyCollection<ExceptionData> FaultHistory { private set; get; }

        /// <summary>
        /// 激活的故障, 即没有  Fixed 的
        /// </summary>
        private readonly List<ExceptionData> m_ActiveFault;

        /// <summary>
        /// 所有已确认的, 没有解决的 故障
        /// </summary>
        public ReadOnlyCollection<ExceptionData> AllSuredActiveFault { private set; get; }

        public ReadOnlyCollection<ExceptionData> AllManualFault { private set; get; }

        public FaultQueue()
        {
            m_ATypeFault = new Queue<ExceptionData>();
            m_PupupFault = new Queue<ExceptionData>();
            m_NormalFault = new Queue<ExceptionData>();
            m_FaultHistory = new List<ExceptionData>();
            m_ActiveFault = new List<ExceptionData>();
            m_ManualFault = new List<ExceptionData>();
            m_UserDefineFault = new Queue<ExceptionData>();

            AllSuredActiveFault = new ReadOnlyCollection<ExceptionData>(m_ActiveFault);
            FaultHistory = new ReadOnlyCollection<ExceptionData>(m_FaultHistory);
            AllManualFault = new ReadOnlyCollection<ExceptionData>(m_ManualFault);
        }

        /// <summary>
        /// 获取等确认的故障
        /// </summary>
        /// <returns></returns>
        public ExceptionData PeekFault()
        {
            if (m_PupupFault.Any())
            {
                return m_PupupFault.Peek();
            }
            if (m_UserDefineFault.Any())
            {
                return m_UserDefineFault.Peek();
            }
            if (m_ATypeFault.Any())
            {
                return m_ATypeFault.Peek();
            }
            if (m_NormalFault.Any())
            {
                return m_NormalFault.Peek();
            }
            return null;
        }

        /// <summary>
        /// 解决了故障
        /// </summary>
        /// <param name="data"></param>
        public void FixFault(ExceptionData data)
        {
            m_FaultHistory.Add(data);
            m_ActiveFault.Remove(data);
            var all = GetAllFaultList();
            all.Remove(data);
            ReEnqueueFault(all, true);

        }

        /// <summary>
        /// 确认故障, 确认是有顺序的
        /// </summary>
        public void SureFault()
        {
            DequeueFault();
        }

        public ExceptionData DequeueFault()
        {
            ExceptionData data = null;
            if (m_PupupFault.Any())
            {
                data = m_PupupFault.Dequeue();
            }
            else if (m_UserDefineFault.Any())
            {
                data = m_UserDefineFault.Dequeue();
            }
            else if (m_ATypeFault.Any())
            {
                data = m_ATypeFault.Dequeue();
            }

            else if (m_NormalFault.Any())
            {
                data = m_NormalFault.Dequeue();
            }

            if (data != null)
            {
                if (data.State == FaultState.SureAndFixed
                    || data.State == FaultState.Fixed)
                {
                    m_FaultHistory.Add(data);
                    m_ActiveFault.Remove(data);
                }
            }
            return data;
        }


        public void EnqueueFault(ExceptionData data)
        {
            EnqueueFault(data, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="hasActive">是否已经激活过</param>
        private void EnqueueFault(ExceptionData data, bool hasActive)
        {
            if (data.SenderType == FaultSenderType.Manaul)
            {
                return;
            }

            switch (data.ExType)
            {
                case FaultType.OperError :
                    m_PupupFault.Enqueue(data);
                    break;
                case FaultType.Passenger :
                    m_NormalFault.Enqueue(data);
                    break;
                case FaultType.A :
                    m_ATypeFault.Enqueue(data);
                    break;
                case FaultType.B :
                    //m_NormalFault.Enqueue(data);
                    //break;
                case FaultType.E :
                    //m_NormalFault.Enqueue(data);
                    //break;
                case FaultType.Manaul :
                    //m_NormalFault.Enqueue(data);
                    //break;
                case FaultType.Event :
                    m_NormalFault.Enqueue(data);
                    break;
                case FaultType.Warning :
                    //m_PupupFault.Enqueue(data);
                    //break;
                case FaultType.Info :
                    m_PupupFault.Enqueue(data);
                    break;
                case FaultType.UserDefinedSystemHalted :
                case FaultType.UserDefinedSystemSleep :
                    m_UserDefineFault.Enqueue(data);
                    break;
                default :
                    throw new ArgumentOutOfRangeException();
            }

            if (!hasActive)
            {
                m_ActiveFault.Add(data);
            }
        }

        public void EnqueueManualFault(IEnumerable<ExceptionData> faultDatas)
        {
            var faults = faultDatas.Where(w => w.SenderType == FaultSenderType.Manaul).ToList();
            m_ManualFault.AddRange(faults);
            m_ActiveFault.AddRange(faults);
        }

        public void ReEnqueueFault(IEnumerable<ExceptionData> faultDatas)
        {
            ClearAllFault();
            foreach (var data in faultDatas)
            {
                EnqueueFault(data);
            }
        }

        private void ReEnqueueFault(IEnumerable<ExceptionData> faultDatas, bool hasActive)
        {
            ClearAllFault();
            foreach (var data in faultDatas)
            {
                EnqueueFault(data, hasActive);
            }
        }

        public bool Contains(ExceptionData data)
        {
            return m_PupupFault.Any(f => f.ExLogNo == data.ExLogNo)
                   || m_UserDefineFault.Any(f => f.ExLogNo == data.ExLogNo)
                   || m_ATypeFault.Any(f => f.ExLogNo == data.ExLogNo)
                   || m_NormalFault.Any(f => f.ExLogNo == data.ExLogNo)
                   || m_ActiveFault.Except(m_ManualFault).Any(a => a.ExLogNo == data.ExLogNo);
        }

        public ExceptionData FindByLogicNo(int falutLogicNo)
        {
            return m_ActiveFault.Except(m_ManualFault).First(f => f.ExLogNo == falutLogicNo);
        }

        public bool Any()
        {
            return m_NormalFault.Any() || m_PupupFault.Any() || m_ATypeFault.Any() || m_UserDefineFault.Any();
        }

        public int Count()
        {
            return m_NormalFault.Count + m_PupupFault.Count + m_ATypeFault.Count + m_UserDefineFault.Count;
        }


        public ReadOnlyCollection<ExceptionData> GetAllFault()
        {
            var all = GetAllFaultList();
            return all.AsReadOnly();
        }

        private List<ExceptionData> GetAllFaultList()
        {
            var all = new List<ExceptionData>(Count());
            all.AddRange(m_PupupFault);
            all.AddRange(m_ATypeFault);
            all.AddRange(m_NormalFault);
            all.AddRange(m_ManualFault);
            all.AddRange(m_UserDefineFault);
            return all;
        }

        /// <summary>
        /// 清除所有等确认的故障
        /// </summary>
        private void ClearAllFault()
        {
            m_PupupFault.Clear();
            m_NormalFault.Clear();
            m_ATypeFault.Clear();
            m_UserDefineFault.Clear();
        }

    }
}

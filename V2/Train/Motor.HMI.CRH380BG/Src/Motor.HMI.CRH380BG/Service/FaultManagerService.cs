using MMI.Facility.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Interface;

namespace Motor.HMI.CRH380BG.Service
{
    /// <summary>
    /// 故障管理服务
    /// </summary>
    public class FaultManagerService : IService
    {

        public readonly Dictionary<int, IFaultInfo> m_AllEvent;
        private readonly int m_PageNum;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="infos">IFaultInfo 枚举器</param>
        /// <param name="pageNum">每页多少条</param>
        public FaultManagerService(IEnumerable<IFaultInfo> infos, int pageNum)
        {
            m_PageNum = pageNum;
            m_AllEvent = infos.ToDictionary(t => t.Index, t => t);
            //m_AllEvent = new Dictionary<int, IFaultInfo>();
            m_CurrentInfos = new List<IFaultInfo>();
            m_WaitConfirmInfos = new Queue<IFaultInfo>();
            ConfirmChanged += FaultManagerService_ConfirmChanged;
            CurrentRow = 1;
        }

        public void Initialize()
        {
            CurrentRow = 1;
        }

        public void Clear()
        {
            m_CurrentInfos.Clear();
            m_WaitConfirmInfos.Clear();
        }

        private void FaultManagerService_ConfirmChanged()
        {
            var count =m_CurrentInfos.Count;
            MaxRow = count;
            if (CurrentRow > MaxRow)
            {
                CurrentRow = MaxRow;
                OnCurrentFaultChanged();
            }
        }

        private readonly IList<IFaultInfo> m_CurrentInfos;
        private int m_MaxRow = 1;

        /// <summary>
        /// 等待确认的事件
        /// </summary>
        private Queue<IFaultInfo> m_WaitConfirmInfos;


        /// <summary>
        /// 发生故障
        /// </summary>
        /// <param name="index">故障逻辑号</param>
        public void HappenFault(int index)
        {
            if (m_AllEvent.ContainsKey(index))
            {
                var tmp = m_AllEvent[index].Clone<IFaultInfo>();
                tmp.HappenTime = DateTime.Now;
                //m_CurrentInfos.Add(tmp);
                //m_HistoryInfos.Add(tmp);
                m_CurrentInfos.Insert(0,tmp);
                //添加待确认队列
                m_WaitConfirmInfos.Enqueue(tmp);
                OnConfirmChanged();
                OnCurrentFaultChanged();
            }
        }

        public void DeleteFault(int index)
        {
            if (m_AllEvent.ContainsKey(index))
            {
                var tmp = m_CurrentInfos.FirstOrDefault(f => f.Index == index);
                if (tmp == default(IFaultInfo))
                {
                    return;
                }
                m_CurrentInfos.Remove(tmp);
                OnCurrentFaultChanged();
            }
        }

        /// <summary>
        /// 当前故障改变后
        /// </summary>
        public event Action CurrentFaultChanged;

        /// <summary>
        /// 故障确认后
        /// </summary>
        public event Action ConfirmChanged;

        /// <summary>
        /// 故障确认
        /// </summary>
        /// <param name="info"></param>
        public void ConfirmFault(IFaultInfo info)
        {
            info.IsConfirm = true;
            OnConfirmChanged();
        }

        /// <summary>
        /// 当前行
        /// </summary>
        public int CurrentRow { get; private set; }

        /// <summary>
        /// 最大行
        /// </summary>
        public int MaxRow
        {
            get { return m_MaxRow; }
            private set
            {
                m_MaxRow = value < 1 ? 1 : value;
            }
        }

        /// <summary>
        /// 下一行
        /// </summary>
        public void NextRow()
        {
            if (CurrentRow < MaxRow)
            {
                CurrentRow++;
            }
        }

        /// <summary>
        /// 上一行
        /// </summary>
        public void LastRow()
        {
            if (CurrentRow > 1)
            {
                CurrentRow--;
            }
        }

        /// <summary>
        /// 获取需要显示的故障
        /// </summary>
        public IEnumerable<IFaultInfo> GetPageInfo()
        {
           return m_CurrentInfos.Skip(m_PageNum * (CurrentRow - 1)).Take(m_PageNum);
        }

        /// <summary>
        /// 获取故障总数
        /// </summary>
        public int GetTotalCount()
        {
            return m_CurrentInfos.Count;
        }

        
        /// <summary>
        /// 获取没有确认的故障数量
        /// </summary>
        public int GetUnConfirmFaultCount
        {
            get { return m_WaitConfirmInfos.Count; }
            private set { }
        }

        /// <summary>
        /// 获取没有确认的故障
        /// </summary>
        /// <returns></returns>
        public IFaultInfo GetUnConfirmFault()
        {
            if (m_WaitConfirmInfos.Count > 0)
            {
                return m_WaitConfirmInfos.Dequeue();
            }

            return null;

            //return m_CurrentInfos.FirstOrDefault(f => !f.IsConfirm);
        }

        private void OnCurrentFaultChanged()
        {
            if (CurrentFaultChanged != null) CurrentFaultChanged();
        }

        private void OnConfirmChanged()
        {
            if (ConfirmChanged != null) ConfirmChanged();
        }

        /// <summary>
        /// 获取每页最大显示数量
        /// </summary>
        public int GetMaxPageNum()
        {
            return m_PageNum;
        }
    }
}

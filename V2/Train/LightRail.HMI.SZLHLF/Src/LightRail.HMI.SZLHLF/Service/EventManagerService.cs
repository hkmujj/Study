using System;
using System.Collections.Generic;
using System.Linq;
using LightRail.HMI.SZLHLF.Interface;
using LightRail.HMI.SZLHLF.Model.EventModel;
using MMI.Facility.Interface.Service;


namespace LightRail.HMI.SZLHLF.Service
{
    /// <summary>
    /// 事件管理服务
    /// </summary>
    public class EventManagerService : IService
    {

        public readonly Dictionary<int, IEventInfo> m_AllEvent;
        private readonly int m_PageNum;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="infos">IEventInfo 枚举器</param>
        /// <param name="pageNum">每页多少条</param>
        public EventManagerService(IEnumerable<IEventInfo> infos, int pageNum)
        {
            m_PageNum = pageNum;
            m_AllEvent = infos.ToDictionary(t => t.Index, t => t);
            m_CurrentInfos = new List<IEventInfo>();
            m_HistoryInfos = new List<IEventInfo>();
            m_WaitConfirmInfos = new Queue<IEventInfo>();
            SetEveltLevel(EventLevel.Normal);
            ConfirmChanged += EventManagerService_ConfirmChanged;
            CurrentPage = 1;
        }

        public void Initialize()
        {
            CurrentPage = 1;
        }

        public void Clear()
        {
            m_CurrentInfos.Clear();
            m_HistoryInfos.Clear();
            m_WaitConfirmInfos.Clear();
        }

        private void EventManagerService_ConfirmChanged()
        {
            var count = CurrentLevel == EventLevel.Normal ? m_CurrentInfos.Count : m_CurrentInfos.Count(w => w.Level == CurrentLevel);
            MaxPage = (count + m_PageNum - 1) / m_PageNum;
            if (CurrentPage > MaxPage)
            {
                CurrentPage = MaxPage;
                OnCurrentEventChanged();
            }
        }

        private readonly IList<IEventInfo> m_CurrentInfos;
        private readonly IList<IEventInfo> m_HistoryInfos;
        private int m_MaxPage = 1;

        /// <summary>
        /// 等待确认的事件
        /// </summary>
        private Queue<IEventInfo> m_WaitConfirmInfos;


        /// <summary>
        /// 发生故障
        /// </summary>
        /// <param name="index">故障逻辑号</param>
        public void HappenFault(int index)
        {
            if (m_AllEvent.ContainsKey(index))
            {
                var tmp = m_AllEvent[index].Clone<IEventInfo>();
                tmp.HappenTime = DateTime.Now;
                //m_CurrentInfos.Add(tmp);
                //m_HistoryInfos.Add(tmp);
                m_CurrentInfos.Insert(0,tmp);
                m_HistoryInfos.Insert(0,tmp);
                //添加待确认队列
                m_WaitConfirmInfos.Enqueue(tmp);
                OnConfirmChanged();
                OnCurrentEventChanged();
            }
        }

        public void DeleteFault(int index)
        {
            if (m_AllEvent.ContainsKey(index))
            {
                var tmp = m_CurrentInfos.FirstOrDefault(f => f.Index == index);
                if (tmp == default(IEventInfo))
                {
                    return;
                }
                m_CurrentInfos.Remove(tmp);
                OnCurrentEventChanged();
            }
        }

        /// <summary>
        /// 当前事件改变后
        /// </summary>
        public event Action CurrentEventChanged;

        /// <summary>
        /// 事件确认后
        /// </summary>
        public event Action ConfirmChanged;

        /// <summary>
        /// 故障确认
        /// </summary>
        /// <param name="info"></param>
        public void ConfirmFault(IEventInfo info)
        {
            info.IsConfirm = true;
            OnConfirmChanged();
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// 最大页
        /// </summary>
        public int MaxPage
        {
            get { return m_MaxPage; }
            private set
            {
                m_MaxPage = value < 1 ? 1 : value;
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public void NextPage()
        {
            if (CurrentPage < MaxPage)
            {
                CurrentPage++;
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        public void LastPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
            }
        }

        /// <summary>
        /// 获取需要显示的故障
        /// </summary>
        public IEnumerable<IEventInfo> GetPageInfo()
        {
            if (HistoryOrCurrent)
            {

                if (CurrentLevel == EventLevel.Normal)
                {
                    return m_CurrentInfos.Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                }
                return m_CurrentInfos.Where(w => w.Level == CurrentLevel).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
            }
            else
            {
                if (CurrentLevel == EventLevel.Normal)
                {
                    return m_HistoryInfos.Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                }
                return m_HistoryInfos.Where(w => w.Level == CurrentLevel).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
            }
        }

        /// <summary>
        /// 获取故障总数
        /// </summary>
        public int GetTotalCount()
        {
            if (HistoryOrCurrent)
            {
                if (CurrentLevel == EventLevel.Normal)
                {
                    return m_CurrentInfos.Count;
                }
                return m_CurrentInfos.Count(w => w.Level == CurrentLevel);
            }
            else
            {
                if (CurrentLevel == EventLevel.Normal)
                {
                    return m_HistoryInfos.Count;
                }
                return m_HistoryInfos.Count(w => w.Level == CurrentLevel);
            }
        }

        /// <summary>
        /// 当前显示的故障级别
        /// </summary>
        public EventLevel CurrentLevel { get; private set; }

        /// <summary>
        /// 设置当前故障级别
        /// </summary>
        /// <param name="levl"></param>
        public void SetEveltLevel(EventLevel levl)
        {
            CurrentLevel = levl;
            CurrentPage = 1;
            var count = 0;
            if (HistoryOrCurrent)
            {
                //count = levl == EventLevel.Normal ? m_CurrentInfos.Count(w => !w.IsConfirm) : m_CurrentInfos.Count(w => w.Level == levl && !w.IsConfirm);
                count = levl == EventLevel.Normal ? m_CurrentInfos.Count : m_CurrentInfos.Count(w => w.Level == levl);
            }
            else
            {
                count = levl == EventLevel.Normal ? m_HistoryInfos.Count : m_HistoryInfos.Count(w => w.Level == levl);
            }
            MaxPage = (count + m_PageNum - 1) / m_PageNum;
        }

        /// <summary>
        /// 当前故障=true  历史故障=false
        /// </summary>
        public bool HistoryOrCurrent { get; private set; }

        /// <summary>
        /// 设置当前故障或者历史故障
        /// </summary>
        public void SetHistoryOrCurrent(bool temp)
        {
            HistoryOrCurrent = temp;
            CurrentPage = 1;
            var count = 0;
            if (HistoryOrCurrent)
            {
                //count = CurrentLevel == EventLevel.Normal ? m_CurrentInfos.Count(w => !w.IsConfirm) : m_CurrentInfos.Count(w => w.Level == CurrentLevel && !w.IsConfirm);
                count = CurrentLevel == EventLevel.Normal ? m_CurrentInfos.Count : m_CurrentInfos.Count(w => w.Level == CurrentLevel);
            }
            else
            {
                count = CurrentLevel == EventLevel.Normal ? m_HistoryInfos.Count : m_CurrentInfos.Count(w => w.Level == CurrentLevel);
            }
            MaxPage = (count + m_PageNum - 1) / m_PageNum;
        }

        /// <summary>
        /// 获取没有确认的事件数量
        /// </summary>
        public int GetUnConfirmEventCount
        {
            get { return m_WaitConfirmInfos.Count; }
            private set { }
        }

        /// <summary>
        /// 获取没有确认的事件
        /// </summary>
        /// <returns></returns>
        public IEventInfo GetUnConfirmEvent()
        {
            if (m_WaitConfirmInfos.Count > 0)
            {
                return m_WaitConfirmInfos.Dequeue();
            }

            return null;

            //return m_CurrentInfos.FirstOrDefault(f => !f.IsConfirm);
        }

        /// <summary>
        /// 
        /// </summary>
        public EventLevel GetHightLevel()
        {

            var tmp = m_CurrentInfos.Where(w => !w.IsConfirm).OrderByDescending(o => o.Level).FirstOrDefault();
            if (tmp == default(IEventInfo))
            {
                return EventLevel.Normal;
            }
            return tmp.Level;
        }

        private void OnCurrentEventChanged()
        {
            if (CurrentEventChanged != null) CurrentEventChanged();
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
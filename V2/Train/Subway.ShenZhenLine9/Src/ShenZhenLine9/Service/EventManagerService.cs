using System;
using System.Collections.Generic;
using System.Linq;
using MMI.Facility.Interface.Service;
using Subway.ShenZhenLine9.Interfaces;

namespace Subway.ShenZhenLine9.Service
{
    /// <summary>
    /// 事件管理服务
    /// </summary>
    public class EventManagerService : IService
    {

        private readonly Dictionary<int, IEventInfo> m_AllEvent;
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
            SetEveltLevel(EventLevel.Normal);
            ConfirmChanged += EventManagerService_ConfirmChanged;
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
        /// 发生故障
        /// </summary>
        /// <param name="index">故障逻辑号</param>
        public void HappenFault(int index)
        {
            if (m_AllEvent.ContainsKey(index))
            {
                var tmp = m_AllEvent[index].Clone<IEventInfo>();
                tmp.HappenTime = DateTime.Now;
                m_CurrentInfos.Add(tmp);
                m_HistoryInfos.Add(tmp);
                OnConfirmChanged();
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
        public int CurrentPage { get; private set; } = 1;

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
        /// 
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
        /// 获取当前页所有信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IEventInfo> GetCurrentPageInfo()
        {
            if (CurrentLevel == EventLevel.Normal)
            {
                return m_CurrentInfos.Where(w => !w.IsConfirm).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
            }
            return m_CurrentInfos.Where(w => w.Level == CurrentLevel && !w.IsConfirm).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
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
            var count = levl == EventLevel.Normal ? m_CurrentInfos.Count : m_CurrentInfos.Count(w => w.Level == levl);
            MaxPage = (count + m_PageNum - 1) / m_PageNum;

        }
        /// <summary>
        /// 获取没有确认的事件
        /// </summary>
        /// <returns></returns>
        public IEventInfo GetUnConfirmEvent()
        {
            return m_CurrentInfos.FirstOrDefault(f => !f.IsConfirm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
            CurrentEventChanged?.Invoke();
        }

        private void OnConfirmChanged()
        {
            ConfirmChanged?.Invoke();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using MMI.Facility.Interface.Service;
using Motor.HMI.CRH380D.Interfaces;
using Motor.HMI.CRH380D.Models.State;


namespace Motor.HMI.CRH380D.Service
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
            m_WaitConfirmInfos = new List<IEventInfo>();
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
            var count = 0;
            switch (CurrentEventPageState)
            {
                //故障界面
                case EventPageState.Fault:
                    if (HistoryOrCurrent)
                    {
                        count = m_CurrentInfos.Count;
                    }
                    else
                    {
                        count = m_HistoryInfos.Count;
                    }
                    break;
                //警报界面
                case EventPageState.Warring:
                    if (HistoryOrCurrent)
                    {
                        switch (CurrentSystem)
                        {
                            case EventSystemState.默认:
                                count = m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring));
                                break;
                            case EventSystemState.高压:
                                count = m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.高压);
                                break;
                            case EventSystemState.牵引:
                                count = m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.牵引);
                                break;
                            case EventSystemState.车门:
                                count = m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.车门);
                                break;
                            case EventSystemState.控制和通讯:
                                count = m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.控制和通讯);
                                break;
                            case EventSystemState.辅助供电:
                                count = m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.辅助供电);
                                break;
                            case EventSystemState.制动:
                                count = m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.制动);
                                break;
                            case EventSystemState.空调:
                                count = m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.空调);
                                break;
                            case EventSystemState.前部:
                                count = m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.前部);
                                break;
                            case EventSystemState.蓄电池供电:
                                count = m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.蓄电池供电);
                                break;
                            case EventSystemState.供风:
                                count = m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.供风);
                                break;
                            case EventSystemState.卫生设备:
                                count = m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.卫生设备);
                                break;
                            case EventSystemState.转向架:
                                count = m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.转向架);
                                break;
                            case EventSystemState.信息系统:
                                count = m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.信息系统);
                                break;
                            case EventSystemState.火灾探测器:
                                count = m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.火灾探测器);
                                break;
                            default:
                                count = 0;
                                break;
                        }
                    }
                    else
                    {
                        count = m_HistoryInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring));

                    }
                    break;
                //未知
                case EventPageState.Normal:
                    count = 0;
                    break;
            }

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
        //private Queue<IEventInfo> m_WaitConfirmInfos;
        private List<IEventInfo> m_WaitConfirmInfos;


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
                //m_WaitConfirmInfos.Enqueue(tmp);
                //m_WaitConfirmInfos = new Queue<IEventInfo>(m_WaitConfirmInfos.OrderBy(o => o.Level));
                m_WaitConfirmInfos.Add(tmp);
                m_WaitConfirmInfos = m_WaitConfirmInfos.OrderBy(o => o.Index).ToList();
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
            switch (CurrentEventPageState)
            {
                    //故障界面
                case EventPageState.Fault: 
                    if (HistoryOrCurrent)
                    {
                            return m_CurrentInfos.Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                    }
                    else
                    {
                            return m_HistoryInfos.Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                    }
                    //警报界面
                case EventPageState.Warring: 
                    if (HistoryOrCurrent)
                    {
                        switch (CurrentSystem)
                        {
                            case EventSystemState.默认:
                                return m_CurrentInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring)).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                            case EventSystemState.高压:
                                return m_CurrentInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.高压).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                            case EventSystemState.牵引:
                                return m_CurrentInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.牵引).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                            case EventSystemState.车门:
                                return m_CurrentInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.车门).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                            case EventSystemState.控制和通讯:
                                return m_CurrentInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.控制和通讯).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                            case EventSystemState.辅助供电:
                                return m_CurrentInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.辅助供电).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                            case EventSystemState.制动:
                                return m_CurrentInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.制动).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                            case EventSystemState.空调:
                                return m_CurrentInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.空调).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                            case EventSystemState.前部:
                                return m_CurrentInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.前部).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                            case EventSystemState.蓄电池供电:
                                return m_CurrentInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.蓄电池供电).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                            case EventSystemState.供风:
                                return m_CurrentInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.供风).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                            case EventSystemState.卫生设备:
                                return m_CurrentInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.卫生设备).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                            case EventSystemState.转向架:
                                return m_CurrentInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.转向架).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                            case EventSystemState.信息系统:
                                return m_CurrentInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.信息系统).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                            case EventSystemState.火灾探测器:
                                return m_CurrentInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.火灾探测器).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                            default:
                                return null;
                        }
                    }
                    else
                    {
                        return m_HistoryInfos.Where(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring)).Skip(m_PageNum * (CurrentPage - 1)).Take(m_PageNum);
                        
                    }
                    //未知
                    case EventPageState.Normal:
                    break;
            }
            return null;
        }

        public bool GetEventSystemStatus(EventSystemState system)
        {
            switch (system)
            {
                case EventSystemState.默认:
                    return m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring)) > 0 ? true : false;
                case EventSystemState.高压:
                    return m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.高压) > 0 ? true : false;
                case EventSystemState.牵引:
                    return m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.牵引) > 0 ? true : false;
                case EventSystemState.车门:
                    return m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.车门) > 0 ? true : false;
                case EventSystemState.控制和通讯:
                    return m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.控制和通讯) > 0 ? true : false;
                case EventSystemState.辅助供电:
                    return m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.辅助供电) > 0 ? true : false;
                case EventSystemState.制动:
                    return m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.制动) > 0 ? true : false;
                case EventSystemState.空调:
                    return m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.空调) > 0 ? true : false;
                case EventSystemState.前部:
                    return m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.前部) > 0 ? true : false;
                case EventSystemState.蓄电池供电:
                    return m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.蓄电池供电) > 0 ? true : false;
                case EventSystemState.供风:
                    return m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.供风) > 0 ? true : false;
                case EventSystemState.卫生设备:
                    return m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.卫生设备) > 0 ? true : false;
                case EventSystemState.转向架:
                    return m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.转向架) > 0 ? true : false;
                case EventSystemState.信息系统:
                    return m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.信息系统) > 0 ? true : false;
                case EventSystemState.火灾探测器:
                    return m_CurrentInfos.Count(w => (w.Level == EventLevel.AWarring || w.Level == EventLevel.BWarring) && w.System == EventSystemState.火灾探测器) > 0 ? true : false;
                default:
                    return false;
            }
        }

        /// <summary>
        ///  事件页面（故障/警报）
        /// </summary>
        public EventPageState CurrentEventPageState { get; private set; }

        /// <summary>
        /// 设置当前事件页面（故障/警报）
        /// </summary>
        /// <param name="levl"></param>
        public void SetEventPageState(EventPageState eventPageState)
        {
            CurrentEventPageState = eventPageState;
        }

        /// <summary>
        /// 当前显示的故障所属系统（主要针对警报）
        /// </summary>
        private EventSystemState CurrentSystem { get; set; }

        /// <summary>
        /// 设置当前故障所属系统（主要针对警报）  备注：此时警报从所有发生过的故障中查询，后期根据具体需求调整
        /// </summary>
        /// <param name="system"></param>
        public void SetEventSystem(EventSystemState system)
        {
            CurrentSystem = system;
        }

        /// <summary>
        /// 当前故障=true(未处理的故障)  历史故障=false（已经处理完成的故障）
        /// </summary>
        private bool HistoryOrCurrent { get; set; }

        /// <summary>
        /// 设置当前故障或者历史故障
        /// </summary>
        public void SetHistoryOrCurrent(bool temp)
        {
            HistoryOrCurrent = temp;
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
        public IEventInfo GetUnConfirmEvent(bool IsRemove)
        {
            IEventInfo tmp = null;
            if (m_WaitConfirmInfos.Count > 0)
            {
                //return m_WaitConfirmInfos.Dequeue();
                tmp = m_WaitConfirmInfos.FirstOrDefault();
                if (IsRemove)
                {
                    m_WaitConfirmInfos.RemoveAt(0);
                }
            }

            return tmp;
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
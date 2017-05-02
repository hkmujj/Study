using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Subway.WuHanLine6.FaultInfos
{
    /// <summary>
    /// 故障信息管理类
    /// </summary>
    [Export]
    public class FaultInfoManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FaultInfoManager(int pageNum)
        {
            PageNum = pageNum;
            CurrenPage = 1;
            MaxPage = 1;
            CurrentData = new List<FaultInfo>();
            HistoryDate = new List<FaultInfo>();
            m_DisplayInfo = new List<FaultInfo>();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public void NextPage()
        {
            if (CurrenPage < MaxPage)
            {
                CurrenPage++;
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        public void LastPage()
        {
            if (CurrenPage > 1)
            {
                CurrenPage--;
            }
        }

        /// <summary>
        /// 故障管理类默认构造函数
        /// </summary>
        public FaultInfoManager() : this(20)
        {
            Level = FaultLevel.Level12;
        }

        /// <summary>
        /// 最大页数更改事件
        /// </summary>
        public event Action<int> MaxPageChanged;

        /// <summary>
        /// 当前页更改
        /// </summary>
        public event Action<int> CurrentPageChanged;

        /// <summary>
        /// 最大条数更改
        /// </summary>
        public event Action<int> CountChanged;

        private int m_Count;
        private int m_MaxPage;
        private int m_CurrenPage;
        private IEnumerable<FaultInfo> m_DisplayInfo;

        /// <summary>
        /// 类型
        /// </summary>
        public FaultCurrent Type { get; private set; }

        /// <summary>
        /// 改变类型
        /// </summary>
        /// <param name="cur"></param>
        public void ChangingType(FaultCurrent cur)
        {
            Type = cur;
        }

        /// <summary>
        /// 故障条数
        /// </summary>
        public int Count
        {
            get { return m_Count; }
            set
            {
                m_Count = value;
                OnCountChanged(value);
            }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrenPage
        {
            get { return m_CurrenPage; }
            private set
            {
                if (m_CurrenPage == value)
                {
                    return;
                }
                m_CurrenPage = value;
                OnCurrentPageChanged(value);
            }
        }

        /// <summary>
        /// 最大页
        /// </summary>
        public int MaxPage
        {
            get { return m_MaxPage; }
            private set
            {
                if (m_MaxPage == value)
                {
                    return;
                }
                m_MaxPage = value;
                OnMaxPageChanged(value);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public int PageNum { get; private set; }

        /// <summary>
        /// 所有故障
        /// </summary>
        public IDictionary<int, FaultInfo> AllDate { get; private set; }

        /// <summary>
        /// 当前数据
        /// </summary>
        public IList<FaultInfo> CurrentData { get; set; }

        /// <summary>
        /// 历史数据
        /// </summary>
        public IList<FaultInfo> HistoryDate { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize(IEnumerable<FaultInfo> infos)
        {
            AllDate = infos.ToDictionary(t => t.LogicNumber, t => t);
        }

        /// <summary>
        /// 添加故障
        /// </summary>
        /// <param name="key">故障逻辑号</param>
        public void Add(int key)
        {
            if (AllDate.ContainsKey(key))
            {
                var tmp = AllDate[key].Clone();
                tmp.HappendTime = DateTime.Now;
                CurrentData.Add(tmp);
                HistoryDate.Add(tmp);
                ChangedDisplay();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Reset(FaultLevel level, FaultCurrent current)
        {
            Level = level;
            Type = current;
            CurrenPage = 1;
            ChangedDisplay();
        }

        private void ChangedDisplay()
        {
            switch (Type)
            {
                case FaultCurrent.Current:
                    ChangedCurrent();
                    break;

                case FaultCurrent.Day:
                    ChangeDay();
                    break;

                case FaultCurrent.Histort:
                    ChangeHistort();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            DateChanged();
        }

        private void DateChanged()
        {
            Count = m_DisplayInfo.Count();
            MaxPage = (Count + PageNum - 1) / PageNum;
            if (MaxPage < 1)
            {
                MaxPage = 1;
            }
            if (CurrenPage > MaxPage)
            {
                CurrenPage = MaxPage;
            }
        }

        private void ChangedCurrent()
        {
            switch (Level)
            {
                case FaultLevel.Red:
                    m_DisplayInfo = CurrentData.Where(w => w.Level == FaultLevel.Red);
                    break;

                case FaultLevel.Yellow:
                    m_DisplayInfo = CurrentData.Where(w => w.Level == FaultLevel.Yellow);
                    break;

                case FaultLevel.Event:
                    m_DisplayInfo = CurrentData.Where(w => w.Level == FaultLevel.Event);
                    break;

                case FaultLevel.Level12:
                    m_DisplayInfo = CurrentData.Where(w => w.Level == FaultLevel.Red || w.Level == FaultLevel.Yellow);
                    break;

                case FaultLevel.All:
                    m_DisplayInfo = CurrentData.Where(w => w.Level == FaultLevel.Red || w.Level == FaultLevel.Yellow || w.Level == FaultLevel.Event);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ChangeDay()
        {
            switch (Level)
            {
                case FaultLevel.Red:
                    m_DisplayInfo = HistoryDate.Where(w => w.HappendTime.Day == DateTime.Now.Day).Where(w => w.Level == FaultLevel.Red);
                    break;

                case FaultLevel.Yellow:
                    m_DisplayInfo = HistoryDate.Where(w => w.HappendTime.Day == DateTime.Now.Day).Where(w => w.Level == FaultLevel.Yellow);
                    break;

                case FaultLevel.Event:
                    m_DisplayInfo = HistoryDate.Where(w => w.HappendTime.Day == DateTime.Now.Day).Where(w => w.Level == FaultLevel.Event);
                    break;

                case FaultLevel.Level12:
                    m_DisplayInfo = HistoryDate.Where(w => w.HappendTime.Day == DateTime.Now.Day).Where(w => w.Level == FaultLevel.Red || w.Level == FaultLevel.Yellow);
                    break;

                case FaultLevel.All:
                    m_DisplayInfo = HistoryDate.Where(w => w.HappendTime.Day == DateTime.Now.Day).Where(w => w.Level == FaultLevel.Red || w.Level == FaultLevel.Yellow || w.Level == FaultLevel.Event);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ChangeHistort()
        {
            switch (Level)
            {
                case FaultLevel.Red:
                    m_DisplayInfo = HistoryDate.Where(w => w.Level == FaultLevel.Red);
                    break;

                case FaultLevel.Yellow:
                    m_DisplayInfo = HistoryDate.Where(w => w.Level == FaultLevel.Yellow);
                    break;

                case FaultLevel.Event:
                    m_DisplayInfo = HistoryDate.Where(w => w.Level == FaultLevel.Event);
                    break;

                case FaultLevel.Level12:
                    m_DisplayInfo = HistoryDate.Where(w => w.Level == FaultLevel.Red || w.Level == FaultLevel.Yellow);
                    break;

                case FaultLevel.All:
                    m_DisplayInfo = HistoryDate.Where(w => w.Level == FaultLevel.Red || w.Level == FaultLevel.Yellow || w.Level == FaultLevel.Event);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 当前显示等级
        /// </summary>
        public FaultLevel Level { get; private set; }

        /// <summary>
        /// 改变等级
        /// </summary>
        /// <param name="level"></param>
        public void ChangedLevel(FaultLevel level)
        {
            Level = level;
            ChangedDisplay();
        }

        /// <summary>
        /// 移除当前故障
        /// </summary>
        /// <param name="key">故障逻辑号</param>
        public void Remove(int key)
        {
            var tmp = CurrentData.FirstOrDefault(f => f.LogicNumber == key);
            if (tmp == default(FaultInfo))
            {
                return;
            }
            tmp.EndTime = DateTime.Now;
            CurrentData.Remove(tmp);
            ChangedDisplay();
        }

        /// <summary>
        /// 获取当前页信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FaultInfo> GetCurrentPageInfo()
        {
            return m_DisplayInfo.Skip((CurrenPage - 1) * PageNum).Take(PageNum);
        }

        /// <summary>
        /// 最大页更改
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void OnMaxPageChanged(int obj)
        {
            MaxPageChanged?.Invoke(obj);
        }

        /// <summary>
        /// 最大页更改
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void OnCurrentPageChanged(int obj)
        {
            CurrentPageChanged?.Invoke(obj);
        }

        /// <summary>
        /// 最大条数更改
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void OnCountChanged(int obj)
        {
            CountChanged?.Invoke(obj);
        }
    }

    /// <summary>
    /// 故障
    /// </summary>
    public enum FaultCurrent
    {
        /// <summary>
        /// 当前
        /// </summary>
        Current,

        /// <summary>
        /// 今天
        /// </summary>
        Day,

        /// <summary>
        /// 历史
        /// </summary>
        Histort,
    }
}
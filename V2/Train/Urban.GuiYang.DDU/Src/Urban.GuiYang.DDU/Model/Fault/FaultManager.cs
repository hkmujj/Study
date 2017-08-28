using System;
using System.Collections.Generic;
using System.Linq;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Model.Fault
{
    public class FaultManager
    {
        public FaultManager()
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                AllInfos = ExcelParser.Parser<FaultInfo>(GlobalParam.Instance.InitParam.AppConfig.AppPaths.ConfigDirectory)
                    .ToDictionary(y => y.ID, t => t);
            }
            else
            {
                AllInfos = new Dictionary<int, FaultInfo>();
            }

        }
        private readonly Dictionary<int, FaultInfo> AllInfos;
        public const int CurrentPageNum = 9;
        public const int HistoryPageNum = 10;

        public FaultInfo GetInfo()
        {
            return CurrentInfo.OrderByDescending(o => o.HapendTime).FirstOrDefault();
        }
        public int CurrentPage
        {
            get
            {
                return m_CurrentPage;
            }
            set
            {
                if (m_CurrentPage == value)
                {
                    return;
                }
                m_CurrentPage = value;
                OnCurrentPageChanged();
                OnCurrentInfoChanged();
            }
        }

        public int MaxPage
        {
            get
            {
                return m_MaxPage;
            }
            set
            {
                if (MaxPage == value)
                {
                    return;
                }

                m_MaxPage = value == 0 ? 1 : value;
                OnMaxPageChanged();
                OnCurrentInfoChanged();
            }
        }

        public int CurrentCount { get { return CurrentInfo.Count; } }
        public int HistoryCount { get { return HistoryInfo.Count; } }
        private List<FaultInfo> CurrentInfo = new List<FaultInfo>();
        private List<FaultInfo> HistoryInfo = new List<FaultInfo>();

        private int m_CurrentPage;
        private int m_MaxPage;
        public event Action CurrentPageChanged;
        public event Action MaxPageChanged;
        public event Action CurrentInfoChanged;
        private bool m_isCurrent = false;
        public void RestPage(bool isCurrent)
        {
            m_isCurrent = isCurrent;
            CurrentPage = 1;
            OperationPage();
            OnCurrentInfoChanged();

        }

        private void OperationPage()
        {
            if (m_isCurrent)
            {
                MaxPage = (CurrentInfo.Count + CurrentPageNum - 1) / CurrentPageNum;
            }
            else
            {
                MaxPage = (HistoryInfo.Count + HistoryPageNum - 1) / HistoryPageNum;
            }
        }
        public void NextPage()
        {
            if (CurrentPage < MaxPage)
            {
                CurrentPage++;
            }
        }

        public void LastPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
            }
        }

        public List<FaultInfo> GetCurrentInfo()
        {
            return CurrentInfo.OrderByDescending(o => o.HapendTime).Skip(CurrentPageNum * (CurrentPage - 1)).Take(CurrentPageNum).ToList();
        }

        public List<FaultInfo> GetHistoryInfo()
        {
            return HistoryInfo.OrderByDescending(o => o.HapendTime).Skip(HistoryPageNum * (CurrentPage - 1)).Take(HistoryPageNum).ToList();
        }

        public void InfoChanged(int id, bool value)
        {
            if (AllInfos.ContainsKey(id))
            {
                if (value)
                {
                    var tmp = AllInfos[id].Clone();
                    tmp.HapendTime = DateTime.Now;
                    CurrentInfo.Add(tmp);
                    HistoryInfo.Add(tmp);
                }
                else
                {
                    var tmp = CurrentInfo.FirstOrDefault(f => f.ID == id);
                    if (tmp != null)
                    {
                        tmp.ConfirmTime = DateTime.Now;
                        CurrentInfo.Remove(tmp);
                    }
                }
                OnCurrentInfoChanged();
                OperationPage();
            }

        }
        protected virtual void OnCurrentPageChanged()
        {
            var handler = CurrentPageChanged;
            if (handler != null)
                handler();
        }

        protected virtual void OnMaxPageChanged()
        {
            var handler = MaxPageChanged;
            if (handler != null)
                handler();
        }

        protected virtual void OnCurrentInfoChanged()
        {
            var handler = CurrentInfoChanged;
            if (handler != null)
                handler();
        }
    }
}

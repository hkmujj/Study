using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using Excel.Interface;
using Urban.Phillippine.View.EventArgs;
using Urban.Phillippine.View.Extend;
using Urban.Phillippine.View.Interface;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Constant
{
    [Export(typeof(IFaultManager))]
    public sealed class FaultManager : IFaultManager, IDataClear
    {

        private int m_CurrentPage;
        private int m_MaxPage;
        private IList<IFaultInfo> m_DisplayInfo;
        public FaultManager()
        {
            CurrentInfo = new List<IFaultInfo>();
            HistoryInfo = new List<IFaultInfo>();
            CurrentData = new List<IFaultInfo>();
            AllInfo = new Dictionary<int, IFaultInfo>();
            MaxPage = 1;
            CurrentPage = 1;
            m_DisplayInfo = CurrentInfo;
            OnMaxPageChanged(new FaultChangedArgs<int>(MaxPage, MaxPage));
            OnCurrentPageChanged(new FaultChangedArgs<int>(CurrentPage, CurrentPage));
            OnCurrentChanged(new FaultChangedArgs<IList<IFaultInfo>>(CurrentInfo, CurrentInfo));
            CurrentChanged += (args) =>
            {
                if (m_DisplayInfo.Count % PageNum == 0)
                {
                    MaxPage = m_DisplayInfo.Count/PageNum;
                }
                else
                {
                    MaxPage = m_DisplayInfo.Count / PageNum + 1;
                }
            };
        }
        public void Add(IFaultInfo tPara)
        {
            var tmp = CurrentInfo.ToList().Find(f => f.Logic == tPara.Logic);
            if (tmp == null)
            {
                tPara.Time = DateTime.Now;
                CurrentInfo.Add(tPara);
                HistoryInfo.Add(tPara);
                OnCurrentChanged(new FaultChangedArgs<IList<IFaultInfo>>(CurrentInfo, CurrentInfo));
            }
        }

        public void Add(int tPara)
        {
            if (AllInfo.ContainsKey(tPara))
            {
                var tmp = AllInfo[tPara].Clone() as IFaultInfo;
                if (tmp != null)
                {
                    Add(tmp);
                }
            }
        }

        public void Remove(int tPara)
        {
            var tmp = CurrentInfo.ToList().Find(f => f.Logic == tPara);
            if (tmp != null)
            {
                Remove(tmp);
            }
        }

        public void Remove(IFaultInfo tPara)
        {
            if (tPara != null)
            {
                if (CurrentInfo.Remove(tPara))
                {
                    OnCurrentChanged(new FaultChangedArgs<IList<IFaultInfo>>(CurrentInfo, CurrentInfo));
                }
            }
        }

        public void Affirm(int tPara)
        {

        }

        public void Affirm(IFaultInfo tPara)
        {

        }

        public void LoadFile(string path)
        {
            var tmp = ExcelParser.Parser<FaultInfo>(path);
            AllInfo = tmp.ToDictionary(s => s.Logic, s => s as IFaultInfo);
            var type = typeof(FaultInfo);
            var at = type.GetCustomAttributes(true).FirstOrDefault(w => w is ExcelLocationAttribute) as ExcelLocationAttribute;
            if (at != null)
            {
                FileName = Path.Combine(path, at.File);
            }

        }

        public string FileName { get; private set; }
        public IDictionary<int, IFaultInfo> AllInfo { get; private set; }
        public IList<IFaultInfo> CurrentInfo { get; private set; }
        public IList<IFaultInfo> HistoryInfo { get; private set; }
        public event Action CurrentInfoChanged;
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

        public IList<IFaultInfo> GetCurrent()
        {
            return m_DisplayInfo.Skip((CurrentPage - 1) * PageNum).Take(PageNum).ToList();
        }

        public int MaxPage
        {
            get { return m_MaxPage; }
            private set
            {
                if (value == m_MaxPage)
                {
                    return;
                }
                m_MaxPage = value;
                var old = m_MaxPage;
                m_MaxPage = value;
                OnMaxPageChanged(new FaultChangedArgs<int>(old, value));
            }
        }

        public int CurrentPage
        {
            get { return m_CurrentPage; }
            private set
            {
                if (m_CurrentPage == value)
                {
                    return;
                }
                var old = m_CurrentPage;
                m_CurrentPage = value;
                OnCurrentPageChanged(new FaultChangedArgs<int>(old, value));
            }
        }

        public int PageNum { get; set; }

        public IList<IFaultInfo> CurrentData { get; private set; }
        public bool Init(int pageNum)
        {
            if (pageNum <= 0)
            {
                return false;
            }
            PageNum = pageNum;
            return true;
        }
        public event Action<FaultChangedArgs<int>> MaxPageChanged;
        public event Action<FaultChangedArgs<int>> CurrentPageChanged;
        public event Action<FaultChangedArgs<IList<IFaultInfo>>> CurrentChanged;
        public FaultType CurrentType { get; private set; }
        public void SetCurrentType(FaultType type)
        {
            CurrentType = type;
            switch (type)
            {
                case FaultType.Current:
                    m_DisplayInfo = CurrentInfo;
                    OnCurrentChanged(new FaultChangedArgs<IList<IFaultInfo>>(CurrentInfo, CurrentInfo));
                    CurrentPage = 1;
                    break;
                case FaultType.History:
                    m_DisplayInfo = HistoryInfo;
                    MaxPage = 1;
                    OnCurrentChanged(new FaultChangedArgs<IList<IFaultInfo>>(CurrentInfo, CurrentInfo));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(type.ToString(), type, null);
            }
        }

        public void Clear()
        {
            CurrentInfo.Clear();
            HistoryInfo.Clear();
            CurrentData.Clear();
            CurrentPage = 1;
        }

        public void Clear(object obj, Type type)
        {

        }

        private void OnMaxPageChanged(FaultChangedArgs<int> obj)
        {
            if(MaxPageChanged != null)
            {
                MaxPageChanged.Invoke(obj);
            }
        }

        private void OnCurrentPageChanged(FaultChangedArgs<int> obj)
        {
            if(CurrentPageChanged != null)
            {
                CurrentPageChanged.Invoke(obj);
            }
        }

        private void OnCurrentChanged(FaultChangedArgs<IList<IFaultInfo>> obj)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged.Invoke(obj);
            }
        }

        private void OnCurrentInfoChanged()
        {
            if (CurrentInfoChanged != null)
            {
                CurrentInfoChanged.Invoke();
            }
        }
    }
}
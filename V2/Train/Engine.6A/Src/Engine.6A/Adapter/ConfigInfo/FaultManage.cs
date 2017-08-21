using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Engine._6A.Enums;
using Engine._6A.Interface;
using Excel.Interface;

namespace Engine._6A.Adapter.ConfigInfo
{
    public class FaultManage : IFaultManage
    {
        public FaultType CurrentFaultType { get; private set; }

        public void SetCurrentType(FaultType type)
        {
            CurrentFaultType = type;
            OnCurrentInfoChanged();
        }

        public void SetCurrentType(int type)
        {
            SetCurrentType((FaultType)type);
        }
        public FaultManage(int pageNum)
        {
            PageNum = pageNum;
            CurrentFaultType = FaultType.Fault;
            DisplayList = new List<IFaultInfo>();
            AllInfo = new Dictionary<int, IFaultInfo>();
            CurrentInfo = new List<IFaultInfo>();
            HistoryInfo = new List<IFaultInfo>();
            CurrentInfoChanged += () =>
            {

                switch (CurrentFaultType)
                {
                    case FaultType.Fault:
                    case FaultType.Event:
                        DisplayList = CurrentInfo.Where(w => w.Type == CurrentFaultType).ToList();
                        break;
                    case FaultType.HistoryFault:
                        DisplayList = HistoryInfo.Where(w => w.Type == FaultType.Fault).ToList();
                        break;

                }
                if (DisplayList.Count == 0)
                {
                    CurrentPage = 1;
                    MaxPage = 1;
                }
                else
                {
                    if (DisplayList.Count % PageNum == 0)
                    {
                        var current = Convert.ToInt32(
                            ((double)DisplayList.Count / (double)PageNum).ToString(CultureInfo.InvariantCulture)
                                .Split('.')[0]);
                        if (CurrentPage > current)
                        {
                            CurrentPage = current;
                        }
                        MaxPage = current;
                    }
                    else
                    {
                        var current = Convert.ToInt32(
                            ((double)DisplayList.Count / (double)PageNum).ToString(CultureInfo.InvariantCulture)
                                .Split('.')[0]) + 1;
                        if (CurrentPage > current)
                        {
                            CurrentPage = current;
                        }
                        MaxPage = current;
                    }
                }
            };
        }
        public void Add(IFaultInfo tPara)
        {
            tPara.DataTime = DateTime.Now;
            CurrentInfo.Add(tPara);
            HistoryInfo.Add(tPara);
            OnCurrentInfoChanged();
        }

        public void Add(int tPara)
        {
            if (AllInfo.ContainsKey(tPara) && !HasFault(tPara))
            {
                Add(AllInfo[tPara]);
            }
        }

        public void Remove(int tPara)
        {
            if (AllInfo.ContainsKey(tPara) && HasFault(tPara))
            {
                Remove(AllInfo[tPara]);
            }
        }

        public void Remove(IFaultInfo tPara)
        {
            var tmp = CurrentInfo.Where(w => w.LogicId == tPara.LogicId).ToList();
            if (tmp.Count != 0)
            {
                CurrentInfo.Remove(tmp[0]);
                OnCurrentInfoChanged();
            }
        }

        public void Affirm(int tPara)
        {

        }

        public void Affirm(IFaultInfo tPara)
        {

        }

        private bool HasFault(int logic)
        {
            return CurrentInfo.Any(a => a.LogicId == logic);
        }
        public void LoadFile(string path)
        {
            AllInfo.Clear();
            CurrentInfo.Clear();
            HistoryInfo.Clear();
            CurrentPage = 1;
            AllInfo = ExcelParser.Parser<FaultInfo>(path).ToDictionary(s => s.LogicId, s => (IFaultInfo)s);

        }

        public string FileName { get; private set; }
        public IDictionary<int, IFaultInfo> AllInfo { get; private set; }
        public IList<IFaultInfo> CurrentInfo { get; private set; }
        public IList<IFaultInfo> HistoryInfo { get; private set; }
        public event Action CurrentInfoChanged;
        public event Action DispalyInfoChnged;
        public void NextPage()
        {
            if (CurrentPage < MaxPage)
            {
                CurrentPage++;
                OnCurrentInfoChanged();
            }
        }

        public void LastPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                OnCurrentInfoChanged();
            }
        }

        public IList<IFaultInfo> DisplayList { get; private set; }

        public void Reset(FaultType type)
        {
            SetCurrentType(type);
            CurrentPage = 1;
            OnCurrentInfoChanged();
        }
        public IList<IFaultInfo> GetCurrent()
        {
            var list = DisplayList;
            switch (CurrentFaultType)
            {
                case FaultType.Fault:
                case FaultType.Event:
                    CurrentData = CurrentInfo.Where(w => w.Type == CurrentFaultType).Skip((CurrentPage - 1) * PageNum).Take(PageNum).ToList();
                    break;
                case FaultType.HistoryFault:
                    CurrentData = HistoryInfo.Where(w => w.Type == FaultType.Fault).Skip((CurrentPage - 1) * PageNum).Take(PageNum).ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return CurrentData;
        }

        public int MaxPage { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageNum { get; private set; }
        public IList<IFaultInfo> CurrentData { get; private set; }

        protected virtual void OnCurrentInfoChanged()
        {
            var handler = CurrentInfoChanged;
            if (handler != null)
            {
                handler();
                OnDispalyInfoChnged();
            }
        }

        protected virtual void OnDispalyInfoChnged()
        {
            var handler = DispalyInfoChnged;
            if (handler != null)
            {
                handler();
            }
        }
    }
}
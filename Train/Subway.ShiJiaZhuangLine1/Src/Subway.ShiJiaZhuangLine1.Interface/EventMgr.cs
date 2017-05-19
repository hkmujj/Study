using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Subway.ShiJiaZhuangLine1.Interface
{
    public class EventMgr : IEventMgr
    {


        public EventMgr(int pageNum)
        {
            PageNum = pageNum;
            AllData = new Dictionary<int, IEventInfo>();
            HistoryEvent = new List<IEventInfo>();
            CurrentData = new List<IEventInfo>();
            CurrentEvent = new List<IEventInfo>();
            DisplayEvent = new List<IEventInfo>();
        }

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
                OnMaxPageChanged();
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
                m_CurrentPage = value;
                OnCurrentPageChanged();
            }
        }

        public int PageNum { get; private set; }
        public IList<IEventInfo> CurrentData { get; private set; }

        /// <summary>
        /// Ê×Ò³
        /// </summary>
        public void FristPage()
        {
            CurrentPage = 1;
            OnCurrentPageChanged();
        }

        public void NextPage()
        {
            if (MaxPage > CurrentPage)
            {
                CurrentPage++;
                OnCurrentPageChanged();
            }
        }

        public void LastPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                OnCurrentPageChanged();
            }
        }


        public IList<IEventInfo> GetCurrent()
        {
            OnCofirmChanged();
            if (m_LastLevel != EventLevel.Normal)
            {
                Sort(m_LastLevel);
            }
            return DisplayEvent = CurrentEvent.Skip((CurrentPage - 1) * PageNum).Take(PageNum).ToList();

        }

        public string Name { get; private set; }
        public Dictionary<int, IEventInfo> AllData { get; private set; }
        public void Load(string file)
        {
            if (File.Exists(file))
            {
                Name = file;
                var allLine = File.ReadAllLines(file, Encoding.Default);
                foreach (var tmp in from line in allLine.Skip(1)
                                    select line.Split(';') into slip
                                    where slip.Length == 9
                                    select new EventInfo
                                    {
                                        Number = Convert.ToInt32(slip[0]),
                                        LogicId = Convert.ToInt32(slip[1]),
                                        FaultCode = slip[2],
                                        Level = (EventLevel)(Convert.ToInt32(slip[3])),
                                        Description = slip[6],
                                        Handbook = slip[7].Replace("#", "\r\n"),
                                        Guideline = slip[8].Replace("#", "\r\n"),
                                        System = slip[5],
                                        CarNumber = slip[4]
                                    })
                {
                    AllData.Add(tmp.LogicId, tmp);


                }

            }
            else
            {
                throw new FileNotFoundException(file);
            }
        }

        public IList<IEventInfo> HistoryEvent { get; private set; }
        public IList<IEventInfo> CurrentEvent { get; private set; }
        public IList<IEventInfo> DisplayEvent { get; private set; }
        public int MaxFalut { get; private set; }


        public bool HasEvent(IEventInfo info)
        {
            var tmp = CurrentData.FirstOrDefault(f => f.LogicId == info.LogicId);
            return tmp != null;
        }

        public bool HasEvent(int logic)
        {
            return AllData.ContainsKey(logic) && HasEvent(AllData[logic]);
        }

        public IEventInfo GetEvent(int logic)
        {
            return AllData.ContainsKey(logic) ? AllData[logic] : null;

        }

        public event Action MaxFaultChanged;
        public event Action CurrentPageChanged;
        public event Action MaxPageChanged;
        public event Action CofirmChanged;

        public void Clear()
        {
            CurrentData.Clear();
            CurrentEvent.Clear();
            CurrentPage = 1;
            MaxFalut = 0;
            MaxPage = 1;
            OnMaxFaultChanged();
            OnCurrentPageChanged();
            OnMaxPageChanged();
            OnCurrentPageChanged();
        }

        public void Cofirm(int logic)
        {
            if (AllData.ContainsKey(logic))
            {
                Cofirm(AllData[logic]);
            }
        }

        public void Cofirm(IEventInfo info)
        {
            var tmp = CurrentEvent.Where(w => !w.IsCofirm).FirstOrDefault(f => f.LogicId == info.LogicId);
            if (tmp != null)
            {
                tmp.IsCofirm = true;
                tmp.CofirmDateTime = DateTime.Now;

            }
            tmp = CurrentData.Where(w => !w.IsCofirm).FirstOrDefault(f => f.LogicId == info.LogicId);
            if (tmp != null)
            {
                tmp.IsCofirm = true;
                tmp.CofirmDateTime = DateTime.Now;
            }
            OnCofirmChanged();
        }

        public void Add(int logic)
        {
            if (AllData.ContainsKey(logic))
            {
                var tmp = AllData[logic].Clone().ConverterToType<IEventInfo>();
                tmp.IsCofirm = false;
                tmp.HappenDateTime = DateTime.Now;
                CurrentData.Add(tmp);
                CurrentEvent.Add(tmp);
                ValueChanging();
            }
        }

        public void Remove(int logic)
        {
            if (HasEvent(logic))
            {
                var tmp = AllData[logic];
                CurrentData.Remove(tmp);
                CurrentEvent.Remove(tmp);
                ValueChanging();
            }
        }

        private EventLevel m_LastLevel;
        private int m_CurrentPage;
        private int m_MaxPage;

        public void Sort(EventLevel level)
        {
            // if (level != m_LastLevel)
            {
                CurrentEvent = CurrentData.Where(w => w.Level == level).ToList();
                CurrentPage = 1;
                MaxPage = CurrentEvent.Count % PageNum == 0 ? CurrentEvent.Count % PageNum : 1;
                m_LastLevel = level;
            }
        }

        public void Reset()
        {
            CurrentPage = 1;
            CurrentEvent = new List<IEventInfo>(CurrentData.ToArray());
            m_LastLevel = EventLevel.Normal;
        }

        public void InitPara()
        {
            CurrentPage = 1;
            CurrentEvent = new List<IEventInfo>(CurrentData.ToArray());
            MaxPage = 1;
            m_LastLevel = EventLevel.Normal;
        }

        void ValueChanging()
        {
            MaxPage = CurrentEvent.Count % PageNum == 0 ? CurrentEvent.Count / PageNum : CurrentEvent.Count / PageNum + 1;
            MaxFalut = CurrentEvent.Count;
            OnMaxFaultChanged();
            OnMaxPageChanged();
            OnCofirmChanged();
        }
        protected virtual void OnCurrentPageChanged()
        {
            var handler = CurrentPageChanged;
            handler?.Invoke();
        }

        protected virtual void OnMaxPageChanged()
        {
            MaxPageChanged?.Invoke();
        }

        protected virtual void OnMaxFaultChanged()
        {
            var handler = MaxFaultChanged;
            handler?.Invoke();
        }

        protected virtual void OnCofirmChanged()
        {
            CofirmChanged?.Invoke();
        }
    }
}
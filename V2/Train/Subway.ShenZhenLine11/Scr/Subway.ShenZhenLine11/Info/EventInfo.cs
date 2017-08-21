using Subway.ShenZhenLine11.Annotations;
using Subway.ShenZhenLine11.Extention;
using Subway.ShenZhenLine11.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Subway.ShenZhenLine11.Info
{
    public class EventInfo : ICloneable, INotifyPropertyChanged
    {
        private bool m_IsSelect;

        public int LogicID { get; set; }

        public int Number { get; set; }
        public int FaultNum { get; set; }

        public static EventInfo Empty = new EventInfo();

        public bool IsSelect
        {
            get { return m_IsSelect; }

            set
            {
                if (value == m_IsSelect)
                {
                    return;
                }
                m_IsSelect = value;
                OnPropertyChanged(nameof(IsSelect));
            }
        }

        public DateTime Time { get; set; }

        public DateTime ConfirmTime { get; set; }

        public bool Confirm { get; set; }

        public EventLevel EventLevel { get; set; }

        public string FaultContent { get; set; }

        public string Emergency { get; set; }

        public object Clone()
        {
            var tmp = new EventInfo();
            tmp.Emergency = Emergency;
            tmp.EventLevel = EventLevel;
            tmp.LogicID = LogicID;
            tmp.FaultContent = FaultContent;
            tmp.FaultNum = FaultNum;

            return tmp;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class EventManager : IManager<EventInfo>
    {
        private int m_CurrentPage = 1;
        private int m_MaxPage = 1;

        public string FileName { get; private set; } = "EventInfo.txt";

        public IDictionary<int, EventInfo> AllInfo { get; private set; }

        public IList<EventInfo> Display { get; private set; } = new List<EventInfo>();

        public IList<EventInfo> History { get; private set; } = new List<EventInfo>();

        public IList<EventInfo> Current { get; private set; } = new List<EventInfo>();

        public int MaxPage
        {
            get { return m_MaxPage; }

            private set
            {
                var old = m_MaxPage;
                m_MaxPage = value;
                OnMaxPageChanged(old, value);
            }
        }

        public int CurrentPage
        {
            get { return m_CurrentPage; }

            private set
            {
                var old = m_CurrentPage;
                m_CurrentPage = value;
                OnCurrentPageChanged(old, value);
                OnCurrentChanged(null, GetCurrent().ToList());
            }
        }

        public int PageNum { get; private set; } = 15;

        public void Add(int key)
        {
            if (AllInfo.ContainsKey(key))
            {
                var tmp = Cast(AllInfo[key].Clone());
                tmp.Time = DateTime.Now;
                Current.Add(tmp);
                History.Add(tmp);
                Changed();
            }
        }

        public void Confirm(int key)
        {
            var tmp = Current.FirstOrDefault(f => f.LogicID == key);
            if (tmp != null)
            {
                tmp.ConfirmTime = DateTime.Now;
                tmp.Confirm = true;
                Changed();
            }
        }

        public void Remove(int key)
        {
            var tmp = Current.FirstOrDefault(f => f.LogicID == key);
            if (tmp != null)
            {
                var tmp1 = Current.Count;
                Current.Remove(tmp);
                OnMaxNumChanged(tmp1, Current.Count);
                Changed();
            }
        }

        public void NextPage()
        {
            if (CurrentPage < MaxPage)
            {
                CurrentPage++;
                Changed();
            }
        }

        public void LastPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                Changed();
            }
        }

        private void Changed()
        {
            MaxPage = (Current.Count + PageNum - 1) / PageNum;
            if (CurrentPage > MaxPage)
            {
                CurrentPage = MaxPage;
            }
            OnCurrentChanged(null, GetCurrent().ToList());
            OnMaxPageChanged(0, MaxPage);
            OnCurrentPageChanged(0, CurrentPage);
            OnMaxNumChanged(0, Current.Count);
        }

        public void Load(string path)
        {
            var file = Path.Combine(path, FileName);
            AllInfo = File.ReadAllLines(file, Encoding.Default)
                .Where(w => !w.StartsWith(";;;;"))
                .Select(s => s.Split(';'))
                .Where(w => w.Length == 5)
                .Select(s => new EventInfo()
                {
                    FaultNum = s[0].ToInt(),
                    LogicID = s[1].ToInt(),
                    EventLevel = (EventLevel)s[2].ToInt(),
                    FaultContent = s[3],
                    Emergency = s[4],
                }).ToDictionary(t => t.LogicID, t => t);
        }

        public EventInfo Cast(object obj)
        {
            return (EventInfo)obj;
        }

        public void Reset()
        {
            Current.Clear();
            History.Clear();
            MaxPage = 1;
            CurrentPage = 1;
        }

        public IEnumerable<EventInfo> GetCurrent()
        {
            //return Current.Where(w => !w.Confirm).Skip(((CurrentPage - 1) * PageNum)).Take(PageNum);
            return Current.Skip(((CurrentPage - 1) * PageNum)).Take(PageNum);
        }

        public event Action<IList<EventInfo>, IList<EventInfo>> CurrentChanged;

        public event Action<int, int> MaxPageChanged;

        public event Action<int, int> CurrentPageChanged;

        public event Action<int, int> MaxNumChanged;

        protected virtual void OnMaxNumChanged(int arg1, int arg2)
        {
            MaxNumChanged?.Invoke(arg1, arg2);
        }

        protected virtual void OnCurrentPageChanged(int arg1, int arg2)
        {
            CurrentPageChanged?.Invoke(arg1, arg2);
        }

        protected virtual void OnMaxPageChanged(int arg1, int arg2)
        {
            MaxPageChanged?.Invoke(arg1, arg2);
        }

        protected virtual void OnCurrentChanged(IList<EventInfo> arg1, IList<EventInfo> arg2)
        {
            CurrentChanged?.Invoke(arg1, arg2);
        }

        public EventLevel GetHightLevel()
        {
            var tmp = Current.Where(w => !w.Confirm).FirstOrDefault(f => f.EventLevel == EventLevel.Serious);
            if (tmp != null)
            {
                return tmp.EventLevel;
            }
            var tmp1 = Current.Where(w => !w.Confirm).FirstOrDefault(f => f.EventLevel == EventLevel.Medium);
            if (tmp1 != null)
            {
                return tmp1.EventLevel;
            }

            return EventLevel.Noaml;
        }
    }

    public enum EventLevel
    {
        Noaml,
        [Description("轻微")]
        Light = 1,

        [Description("中等")]
        Medium = 2,

        [Description("严重")]
        Serious = 3,

        [Description("临时")]
        Temporary = 4,
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Subway.ShenZhenLine11.Extention;
using Subway.ShenZhenLine11.Interface;

namespace Subway.ShenZhenLine11.Info
{
    public class EnmergencyBordercastManager : IManager<EmergencyBordercastInfo>
    {
        private int m_PageNum = 8;
        private int m_CurrentPage = 1;
        private int m_MaxPage = 1;
        private IList<EmergencyBordercastInfo> m_Display = new List<EmergencyBordercastInfo>();
        public string FileName { get; } = "EnmergencyBoradcast.txt";
        public IDictionary<int, EmergencyBordercastInfo> AllInfo { get; private set; } = new Dictionary<int, EmergencyBordercastInfo>();

        public IList<EmergencyBordercastInfo> Display
        {
            get { return m_Display; }
            private set
            {
                var old = m_Display;
                m_Display = value;
                OnCurrentChanged(old, value);
            }
        }

        public IList<EmergencyBordercastInfo> History { get; private set; } = new List<EmergencyBordercastInfo>();
        public IList<EmergencyBordercastInfo> Current { get; private set; } = new List<EmergencyBordercastInfo>();

        public int MaxPage
        {
            get { return m_MaxPage; }
            private set
            {
                if (m_MaxPage == value)
                {
                    return;
                }

                var old = m_MaxPage;
                OnMaxPageChanged(old, value);
                m_MaxPage = value;
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
                OnCurrentPageChanged(old, value);
            }
        }

        public int PageNum
        {
            get { return m_PageNum; }
            private set { m_PageNum = value; }
        }

        public void Add(int key)
        {
            if (AllInfo.ContainsKey(key))
            {
                var tmp = AllInfo[key];
                Current.Add(tmp);
                History.Add(tmp);
                Changed();
            }
        }

        public void Confirm(int key)
        {

        }

        public void Remove(int key)
        {
            var tmp = Current.FirstOrDefault(f => f.Num == key);
            Current.Remove(tmp);
            Changed();
        }

        void Changed()
        {
            Display = Current.Skip((CurrentPage - 1) * PageNum).Take(PageNum).ToList();
            OnMaxNumChanged(0, Current.Count);
            if (Current.Count % PageNum == 0)
            {
                MaxPage = Current.Count / PageNum;
            }
            else
            {
                MaxPage = Current.Count / PageNum + 1;
            }
            if (CurrentPage > MaxPage)
            {
                CurrentPage = MaxPage;
            }

        }
        public void NextPage()
        {
            if (CurrentPage < MaxPage)
            {
                CurrentPage++;
                Changed();
                Current.ToList().ForEach(f => f.IsSelcet = false);
            }
        }

        public void LastPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                Changed();
                Current.ToList().ForEach(f => f.IsSelcet = false);
            }
        }

        public void Load(string path)
        {
            var file = Path.Combine(path, FileName);
            foreach (var tmp in from source in File.ReadAllLines(file, Encoding.Default).Skip(1)
                                select source.Split(';') into slip
                                where slip.Length == 2
                                select new EmergencyBordercastInfo
                                {
                                    Num = slip[0].ToInt(),
                                    Content = slip[1]
                                })
            {
                AllInfo.Add(tmp.Num, tmp);
            }
            Current = AllInfo.Values.ToList();
            Current.ToList().ForEach(f =>
            {
                f.SelectChanged += (args) =>
                {
                    foreach (var info in Current.Where(w => w.Num != args.Num))
                    {
                        info.IsSelcet = false;
                    }

                };
            });
            Changed();
        }

        public EmergencyBordercastInfo Cast(object obj)
        {
            return obj as EmergencyBordercastInfo;
        }

        public void Reset()
        {
            CurrentPage = 1;
            MaxPage = (Current.Count + PageNum - 1) / PageNum;
        }


        public event Action<IList<EmergencyBordercastInfo>, IList<EmergencyBordercastInfo>> CurrentChanged;
        public event Action<int, int> MaxPageChanged;
        public event Action<int, int> CurrentPageChanged;
        public event Action<int, int> MaxNumChanged;

        protected virtual void OnCurrentChanged(IList<EmergencyBordercastInfo> arg1, IList<EmergencyBordercastInfo> arg2)
        {
            CurrentChanged?.Invoke(arg1, arg2);
        }

        protected virtual void OnMaxPageChanged(int arg1, int arg2)
        {
            MaxPageChanged?.Invoke(arg1, arg2);
        }

        protected virtual void OnCurrentPageChanged(int arg1, int arg2)
        {
            CurrentPageChanged?.Invoke(arg1, arg2);
        }

        protected virtual void OnMaxNumChanged(int arg1, int arg2)
        {
            MaxNumChanged?.Invoke(arg1, arg2);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using Urban.Info.Interface;

namespace Engine.HMI.SS3B.View.Model
{
    public class MessageMgr : IManage<int, MessageInfo>, IPaging<MessageInfo>
    {

        public MessageMgr(int pageNum)
        {
            CurrentInfo = new List<MessageInfo>();
            HistoryInfo = new List<MessageInfo>();
            PageNum = pageNum;
            CurrentData = new List<MessageInfo>();
            AllInfo = new Dictionary<int, MessageInfo>();
            FaultChanged += ResetPage;
        }
        public event Action FaultChanged;
        protected virtual void OnFaultChanged()
        {
            var handler = FaultChanged;
            if (handler != null)
            {
                handler();
            }
        }

        public void Add(MessageInfo tPara)
        {
            tPara.Time = DateTime.Now;
            CurrentInfo.Add(tPara);
            HistoryInfo.Add(tPara);
            OnFaultChanged();

        }

        public void Add(int tPara)
        {
            if (AllInfo.ContainsKey(tPara))
            {
                Add(AllInfo[tPara].Clone());
            }
        }

        public void Remove(int tPara)
        {
            if (AllInfo.ContainsKey(tPara))
            {
                Remove(AllInfo[tPara]);
            }
        }

        public void Remove(MessageInfo tPara)
        {
            var tmp = CurrentInfo.FirstOrDefault(f => f.LogicID == tPara.LogicID);
            if (tmp != null)
            {
                tmp.IsCurrent = true;
            }
            CurrentInfo.Remove(tmp);
            OnFaultChanged();
        }

        private void ResetPage()
        {
            ((List<MessageInfo>)HistoryInfo).Sort();
            if (HistoryInfo.Count == 0)
            {
                CurrentPage = 1;
                MaxPage = 1;
            }
            else
            {
                if (HistoryInfo.Count % PageNum == 0)
                {
                    var current = Convert.ToInt32((((double)HistoryInfo.Count) / (double)PageNum).ToString(CultureInfo.InvariantCulture).Split('.')[0]);
                    MaxPage = current;
                    if (CurrentPage > MaxPage)
                    {
                        CurrentPage = MaxPage;
                    }
                }
                else
                {
                    var current = Convert.ToInt32((((double)HistoryInfo.Count) / (double)PageNum).ToString(CultureInfo.InvariantCulture).Split('.')[0]) + 1;
                    MaxPage = current;
                    if (CurrentPage > MaxPage)
                    {
                        CurrentPage = MaxPage;
                    }
                }



            }
        }
        public void Affirm(int tPara)
        {

        }

        public void Affirm(MessageInfo tPara)
        {

        }

        public void LoadFile(string path)
        {
            CurrentPage = 1;
            MaxPage = 1;
            FileName = path;
            var allLine = File.ReadAllLines(path, Encoding.Default);
            foreach (var source in allLine.Skip(1))
            {
                var slip = source.Split(';');
                if (slip.Length != 3)
                {
                    return;
                }
                var tmp = new MessageInfo();
                tmp.LogicID = int.Parse(slip[0]);
                tmp.Level = slip[1];
                tmp.Content = slip[2];
                AllInfo.Add(tmp.LogicID, tmp);

            }
        }

        public string FileName { get; private set; }
        public IDictionary<int, MessageInfo> AllInfo { get; private set; }
        public IList<MessageInfo> CurrentInfo { get; private set; }
        public IList<MessageInfo> HistoryInfo { get; private set; }
        public event Action CurrentInfoChanged;

        public void NextPage()
        {
            if (CurrentPage < MaxPage)
            {
                CurrentPage++;
                OnFaultChanged();
            }
        }

        public void LastPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                OnFaultChanged();
            }
        }

        public IList<MessageInfo> GetCheckModelFault()
        {
            return GetALL(SetNum(HistoryInfo.Skip((CurrentPage - 1) * 18).Take(18).ToList()));
        }

        private IList<MessageInfo> SetNum(IList<MessageInfo> info)
        {
            for (int i = 0; i < info.Count; i++)
            {
                info[i].Number = i + 1;
            }
            return info;
        }

        private IList<MessageInfo> GetALL(IList<MessageInfo> info)
        {
            while (info.Count < 18)
            {
                info.Add(MessageInfo.Empty);
            }
            return info;
        }
        public MessageInfo GetNewInfo()
        {
            return CurrentInfo.OrderByDescending(o => o.Time).FirstOrDefault();
        }
        public IList<MessageInfo> GetDeafauList()
        {
            CurrentPage = 1;
            return GetCurrent();
        }
        public IList<MessageInfo> GetCurrent()
        {
            return CurrentData = HistoryInfo.Skip((CurrentPage - 1) * PageNum).Take(PageNum).ToList();
        }

        public int MaxPage { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageNum { get; private set; }
        public IList<MessageInfo> CurrentData { get; private set; }
    }

    public class MessageInfo : NotificationObject, IComparable<MessageInfo>
    {
        static MessageInfo()
        {
            Empty = new MessageInfo();
            Empty.IsCurrent = true;
        }

        public static MessageInfo Empty;

        private bool m_IsCurrent;
        public int? Number { get; set; }
        public int LogicID { get; set; }
        public string Content { get; set; }
        public string Level { get; set; }
        public DateTime Time { get; set; }

        public bool IsCurrent
        {
            get { return m_IsCurrent; }
            set
            {
                if (value == m_IsCurrent)
                {
                    return;
                }
                m_IsCurrent = value;
                RaisePropertyChanged(() => IsCurrent);
            }
        }

        public MessageInfo Clone()
        {
            var tmp = new MessageInfo();
            tmp.LogicID = LogicID;
            tmp.Content = Content;
            tmp.Level = Level;
            tmp.IsCurrent = false;
            return tmp;
        }

        public int CompareTo(MessageInfo other)
        {
            var result = 0;
            if (Time > other.Time)
            {
                result = -1;
            }
            else

                if (Time == other.Time)
            {
                result = 0;
            }
            else
            {
                result = 1;
            }
            return result;
        }


    }
}

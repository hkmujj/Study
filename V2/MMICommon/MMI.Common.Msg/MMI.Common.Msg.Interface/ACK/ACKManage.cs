using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MMI.Common.Msg.Interface.ACK
{
    public class ACKManage : IACKManage
    {
        public event Action CurrentInfoChanged;
        public string FileName { get; private set; }
        public IDictionary<int, IACKDetail> AllInfo { get; private set; }
        public IList<IACKDetail> CurrentInfo { get; private set; }
        public IList<IACKDetail> HistoryInfo { get; private set; }

        public ACKManage()
        {
            AllInfo = new Dictionary<int, IACKDetail>();
            CurrentInfo = new List<IACKDetail>();
            HistoryInfo = new List<IACKDetail>();
        }
        public void Add(IACKDetail tPara)
        {
            if (HasInfo(tPara.LogicID))
            {
                return;
            }
            tPara.HappenTime = DateTime.Now;
            CurrentInfo.Add(tPara);
            HistoryInfo.Add(tPara);
        }

        public void Add(int tPara)
        {
            if (AllInfo.ContainsKey(tPara))
            {
                Add(AllInfo[tPara]);
            }
        }

        public void Remove(int tPara)
        {
            if (AllInfo.ContainsKey(tPara))
            {
                Remove(AllInfo[tPara]);
            }
        }

        public void Remove(IACKDetail tPara)
        {
            CurrentInfo.Remove(tPara);
            var tmp = HistoryInfo.FirstOrDefault(f => f.LogicID == tPara.LogicID);
            if (tmp != default(IACKDetail))
            {
                tmp.RemoveTime = DateTime.Now;
            }

        }

        public void Affirm(int tPara)
        {
            if (AllInfo.ContainsKey(tPara))
            {
                Affirm(AllInfo[tPara]);
            }
        }

        public void Affirm(IACKDetail tPara)
        {
            Remove(tPara);
            var tmp = HistoryInfo.FirstOrDefault(f => f.LogicID == tPara.LogicID);
            if (tmp != default(IACKDetail))
            {
                tmp.AffirmTime = DateTime.Now;
            }

        }

        public void LoadFile(string path)
        {
            if (File.Exists(path))
            {
                FileName = path;
                var allLine = File.ReadAllLines(path, Encoding.Default);
                foreach (var tmp in from line in allLine.Skip(1)
                                    select line.Split(';') into slip
                                    where slip.Length == 6
                                    select
                                        new ACKDetail(
                                        Convert.ToInt32(slip[0]),
                                        Convert.ToInt32(slip[1]),
                                        slip[2].Trim().Replace("#", "\r").Replace('$',' '),
                                        slip[3].Trim().Replace('#', '\r').Replace('$', ' '), double.Parse(slip[4]), double.Parse(slip[5])))
                {
                    AllInfo.Add(tmp.LogicID, tmp);
                }
            }
            else
            {
                throw new FileNotFoundException(path);
            }
        }

        public IACKDetail GetCurrentInfo()
        {
            if (CurrentInfo.Count != 0)
            {
                return CurrentInfo[0];
            }
            return null;
        }

        public bool HasInfo(int logic)
        {

            var tmp = CurrentInfo.FirstOrDefault(f => f.LogicID == logic);
            return tmp != null;
        }
    }
}
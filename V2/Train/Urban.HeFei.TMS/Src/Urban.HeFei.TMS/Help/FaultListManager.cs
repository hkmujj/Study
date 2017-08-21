using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using CommonUtil.Controls.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Urban.HeFei.TMS.Help
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class FaultListManager : baseClass
    {
        public override string GetInfo()
        {
            return "故障管理类";
        }

        private GDIButton m_Button;
        public override bool init(ref int nErrorObjectIndex)
        {
            Load();
            return true;
        }

        public Dictionary<int, FaultInfo> AllFaultInfo { get; set; }
        /// <summary>
        /// 当前故障变更
        /// </summary>
        public static event Action<FaultInfo, int> FaultInfoChanged;
        /// <summary>
        /// 历史故障变更
        /// </summary>
        public static event Action<FaultInfo> HistoryChanged;
        private List<FaultInfo> m_Current = new List<FaultInfo>();
        private readonly List<FaultInfo> m_History = new List<FaultInfo>();
        public void Add(int index)
        {
            if (AllFaultInfo.ContainsKey(index) && m_Current.Find(f => f.LogicIndex == index) == null)
            {
                var tmp = AllFaultInfo[index].Clone();
                tmp.Time = DateTime.Now;
                m_Current.Add(tmp);
                OnFaultInfoChanged(tmp, 1);
            }
        }

        public void Remove(int index)
        {
            if (AllFaultInfo.ContainsKey(index) && m_Current.Find(f => f.LogicIndex == index) != null)
            {
                var tmp = m_Current.FirstOrDefault(f => f.LogicIndex == index);
                if (tmp != null)
                {
                    var tmpHis = tmp.Clone();
                    tmpHis.Time = tmp.Time;
                    tmpHis.RemoveTime = DateTime.Now;
                    OnFaultInfoChanged(tmp, 0);
                    m_Current.Remove(tmp);
                    m_History.Add(tmpHis);
                    OnHistoryChanged(tmpHis);
                }
            }
        }
        private void Load()
        {
            AllFaultInfo = File.ReadLines(Path.Combine(AppPaths.ConfigDirectory, FaultInfo.FileName),
                Encoding.Default)
                .Where(w => !w.StartsWith(";;;;"))
                .Select(s => s.Split(';'))
                .Where(w => w.Length == 5)
                .Select(
                    s => new FaultInfo()
                    {
                        LogicIndex = int.Parse(s[0]),
                        CarNum = s[1],
                        FaultCode = s[2],
                        FaultName = s[3],
                        Level = int.Parse(s[4])
                    }).ToDictionary(t => t.LogicIndex, t => t);
        }

        public override void paint(Graphics g)
        {

            foreach (var info in AllFaultInfo)
            {
                if (BoolList[info.Key])
                {
                    Add(info.Key);
                }
                else
                {
                    Remove(info.Key);
                }
            }
            //g.FillRectangle(Brushes.Red, 0, 90, 800, 506);
            base.paint(g);
        }

        private static void OnHistoryChanged(FaultInfo obj)
        {
            HistoryChanged?.Invoke(obj);
        }

        private static void OnFaultInfoChanged(FaultInfo arg1, int arg2)
        {
            FaultInfoChanged?.Invoke(arg1, arg2);
        }
    }

    public class FaultInfo
    {
        public const string FileName = "故障信息.txt";
        /// <summary>
        /// 故障逻辑号
        /// </summary>
        public int LogicIndex { get; set; }
        /// <summary>
        /// 故障代码
        /// </summary>
        public string FaultCode { get; set; }
        /// <summary>
        /// 车辆编号
        /// </summary>
        public string CarNum { get; set; }
        /// <summary>
        /// 故障名称 
        /// </summary>
        public string FaultName { get; set; }

        public int Level { get; set; }
        /// <summary>
        /// 故障发生时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 故障结束时间
        /// </summary>
        public DateTime RemoveTime { get; set; }

        public FaultInfo Clone()
        {
            var tmp = new FaultInfo();
            tmp.LogicIndex = LogicIndex;
            tmp.FaultCode = FaultCode;
            tmp.CarNum = CarNum;
            tmp.FaultName = FaultName;
            tmp.Level = Level;
            return tmp;

        }

    }
}
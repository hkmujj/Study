using MMI.Facility.Interface.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Urban.ATC.Domain.Interface;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Siemens.Model
{
    public class MessageMgr
    {
        public Dictionary<int, IMessgeInfo> AllMessage;

        /// <summary>
        /// 未确认信息列表
        /// </summary>
        public List<int> CurrentMsgList { get; private set; }

        public MessageMgr()
        {
            AllMessage = new Dictionary<int, IMessgeInfo>();
            CurrentMsgList = new List<int>();
        }

        public IMessgeInfo GetCurrentFirstMessge()
        {
            return CurrentMsgList.Count != 0 ? AllMessage[CurrentMsgList[0]] : null;
        }

        public void AddMsg(int logic)
        {
            if (AllMessage.ContainsKey(logic) && !CurrentMsgList.Contains(logic))
            {
                CurrentMsgList.Add(logic);
            }
        }

        public void Read(int logic)
        {
            if (CurrentMsgList.Contains(logic))
            {
                CurrentMsgList.Remove(logic);
            }
        }

        public void InitMsg(SubsystemInitParam initPara)
        {
            var file = Path.Combine(initPara.DataPackage.Config.SystemDicrectory.BaseDirectory, "Config\\MessageInfo.txt");
            var allLine = File.ReadAllLines(file, Encoding.Default);
            foreach (var s in allLine.Skip(1))
            {
                var slip = s.Split(new[] { '\t' });
                if (slip.Length >= 3)
                {
                    var msg = new MessgeInfo(slip[2].Replace('#', '\r'), Convert.ToInt32(slip[0]), (InfoLevl)Convert.ToInt32(slip[1]));

                    AllMessage.Add(msg.LogicID, msg);
                }
            }
        }
    }
}
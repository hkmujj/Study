using System.Collections.Generic;

namespace Motor.HMI.CRH1A.Alarm.Fault
{
    public class EventStatic
    {

        ///// <summary>
        ///// 当前活动故障列表
        ///// </summary>
        //public static Dictionary<int, bool> CurrentActiveEventList = new Dictionary<int, bool>();//当前事件列表


        public static SortedList<int, FaultHelpInfo> HelpInfos = new SortedList<int,FaultHelpInfo>();//帮助信息
    }
}
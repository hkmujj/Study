using System.Collections.Generic;

namespace Motor.HMI.CRH1A.Alarm.Fault
{
    public class EventStatic
    {

        ///// <summary>
        ///// ��ǰ������б�
        ///// </summary>
        //public static Dictionary<int, bool> CurrentActiveEventList = new Dictionary<int, bool>();//��ǰ�¼��б�


        public static SortedList<int, FaultHelpInfo> HelpInfos = new SortedList<int,FaultHelpInfo>();//������Ϣ
    }
}
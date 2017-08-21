using MsgReceiveMod;

namespace Urban.QingDao3Line.MMI
{
    class FaultAlarm:FaultMsgOrdinary
    {
        /// <summary>
        /// 警报类型
        /// </summary>
        public string AlarmType { get; set; }

        public string Level { get; set; }

    }
}

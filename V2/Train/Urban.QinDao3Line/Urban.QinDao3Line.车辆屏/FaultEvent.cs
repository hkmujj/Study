using MsgReceiveMod;

namespace Urban.QingDao3Line.MMI
{
    class FaultEvent:FaultMsgOrdinary
    {
        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 故障名字
        /// </summary>
        public string StackName { get; set; }
    }
}

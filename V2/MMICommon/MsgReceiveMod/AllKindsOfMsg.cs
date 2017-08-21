using System;
using System.Drawing;

namespace MsgReceiveMod
{
    /// <summary>
    /// 最基本的消息结构
    /// </summary>
    public class BaseMsgContent : ILevelAndTime
    {
        /// <summary>
        /// 消息逻辑号
        /// </summary>
        public int MsgLogicID { get; set; }

        /// <summary>
        /// 消息代码
        /// </summary>
        public string MsgID { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string MsgContent { get; set; }

        /// <summary>
        /// 消息结束时间
        /// </summary>
        public DateTime MsgOverTime { get; set; }

        /// <summary>
        /// 故障是否已结束
        /// </summary>
        public bool FaultBeOver { get; set; }

        private int _theSameMsgNumb = 1;
        /// <summary>
        /// 同一条故障的个数
        /// </summary>
        public int TheSameMsgNumb { get { return _theSameMsgNumb; } set { _theSameMsgNumb = value; } } 

        #region ILevelAndTime 成员

        /// <summary>
        /// 消息接收时间
        /// </summary>
        public DateTime MsgReceiveTime { get; set; }

        /// <summary>
        /// 消息等级
        /// </summary>
        public MsgLevels TheMsgLevel { get; set; }
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime AffirmTime { get; set; }
        /// <summary>
        /// 消息已读标记
        /// </summary>
        public bool TheMsgFlag { get; set; }

        #endregion
    }

    /// <summary>
    /// 普遍性消息内容
    /// </summary>
    public class FaultMsgOrdinary : BaseMsgContent
    {
        /// <summary>
        /// 故障排除
        /// 文字解决方案
        /// </summary>
        public string FaultSolutionStr { get; set; }

        /// <summary>
        /// 图片解决方案
        /// </summary>
        public Image FaultSolutionImage { get; set; }

        /// <summary>
        /// 消息对应需要往外发送的逻辑位
        /// </summary>
        public int MsgSendLogicID { get; set; }
    }

}

using System;

namespace MsgReceiveMod
{
    /// <summary>
    /// 消息接收时间与消息的等级
    /// 做成接口主要用来排序用
    /// </summary>
    public interface ILevelAndTime
    {
        /// <summary>
        /// 消息接收时间
        /// </summary>
        DateTime MsgReceiveTime { get; set; }

        /// <summary>
        /// 消息等级
        /// </summary>
        MsgLevels TheMsgLevel { get; set; }

        /// <summary>
        /// 消息已读标记
        /// </summary>
        bool TheMsgFlag { get; set; }
    }
}

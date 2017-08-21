using System;
using System.Collections.Generic;

namespace MsgReceiveMod
{
    /// <summary>
    /// 消息排序
    /// </summary>
    public class MsgSort : IComparer<ILevelAndTime>
    {
        /// <summary>
        /// 默认按时间顺序排序
        /// </summary>
        public SortCriteriaOfMsg MsgSortBy = SortCriteriaOfMsg.TimeUp;

        #region IComparer<ILevelAndTime> 成员

        /// <summary>
        /// 排序比较
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>1表示x大于y,-1表示x小于y,0表示x等于y</returns>
        public int Compare(ILevelAndTime x, ILevelAndTime y)
        {
            switch (MsgSortBy)
            {
                case SortCriteriaOfMsg.TimeUp:
                    if (DateTime.Compare(x.MsgReceiveTime, y.MsgReceiveTime) > 0)
                        return 1;
                    if (DateTime.Compare(x.MsgReceiveTime, y.MsgReceiveTime) < 0)
                        return -1;
                    return 0;

                case SortCriteriaOfMsg.TimeDown:
                    if (DateTime.Compare(x.MsgReceiveTime, y.MsgReceiveTime) > 0)
                        return -1;
                    return DateTime.Compare(x.MsgReceiveTime, y.MsgReceiveTime) < 0 ? 1 : 0;

                case SortCriteriaOfMsg.Level:
                    if (x.TheMsgLevel > y.TheMsgLevel)
                        return 1;
                    if (x.TheMsgLevel < y.TheMsgLevel)
                        return -1;
                    return 0;
                case SortCriteriaOfMsg.TimeThenLevel:
                    if (DateTime.Compare(x.MsgReceiveTime, y.MsgReceiveTime) > 0)
                        return 1;
                    if (DateTime.Compare(x.MsgReceiveTime, y.MsgReceiveTime) < 0)
                        return -1;
                    if (x.TheMsgLevel > y.TheMsgLevel)
                        return 1;
                    if (x.TheMsgLevel < y.TheMsgLevel)
                        return -1;
                    return 0;
                case SortCriteriaOfMsg.LevelThenTime:
                    if (x.TheMsgLevel > y.TheMsgLevel)
                        return 1;
                    if (x.TheMsgLevel < y.TheMsgLevel)
                        return -1;
                    if (DateTime.Compare(x.MsgReceiveTime, y.MsgReceiveTime) > 0)
                        return 1;
                    if (DateTime.Compare(x.MsgReceiveTime, y.MsgReceiveTime) < 0)
                        return -1;
                    return 0;
                case SortCriteriaOfMsg.Flag:
                    if (!x.TheMsgFlag && y.TheMsgFlag)
                        return 1;
                    if (x.TheMsgFlag && !y.TheMsgFlag)
                        return -1;
                    return 0;
                case SortCriteriaOfMsg.FlagThenLevelThenTime:
                    if (!x.TheMsgFlag && y.TheMsgFlag)
                        return 1;
                    if (x.TheMsgFlag && !y.TheMsgFlag)
                        return -1;
                    if (x.TheMsgLevel > y.TheMsgLevel)
                        return 1;
                    if (x.TheMsgLevel < y.TheMsgLevel)
                        return -1;
                    if (DateTime.Compare(x.MsgReceiveTime, y.MsgReceiveTime) > 0)
                        return 1;
                    if (DateTime.Compare(x.MsgReceiveTime, y.MsgReceiveTime) < 0)
                        return -1;
                    return 0;
                case SortCriteriaOfMsg.FlagThenTimeThenLevel:
                    if (!x.TheMsgFlag && y.TheMsgFlag)
                        return 1;
                    if (x.TheMsgFlag && !y.TheMsgFlag)
                        return -1;
                    if (DateTime.Compare(x.MsgReceiveTime, y.MsgReceiveTime) > 0)
                        return 1;
                    if (DateTime.Compare(x.MsgReceiveTime, y.MsgReceiveTime) < 0)
                        return -1;
                    if (x.TheMsgLevel > y.TheMsgLevel)
                        return 1;
                    if (x.TheMsgLevel < y.TheMsgLevel)
                        return -1;
                    return 0;
                default:
                    return 0;
            }
        }
        #endregion
    }
}

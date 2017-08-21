using System;
using System.Collections.Generic;
using System.Linq;
using MsgReceiveMod;

namespace Motor.ATP._300T.共用
{
    public enum SpecialHanding
    {
        无 = 0,
        结束后删除 = 1,
        一分钟后自动结束并发送数据 = 2,
        执行制动测试 = 6,
    }

    public class Message : FaultMsgOrdinary
    {
        /// <summary>
        /// 给信号发送位
        /// </summary>
        public List<int[]> SendValueToCpu;

        /// <summary>
        /// 按下确认
        /// </summary>
        public bool BeSure { get; set; }

        /// <summary>
        /// 消息收到时跳转到的菜单视图
        /// </summary>
        public int MsgStartThenJumpView { get; set; }

        /// <summary>
        /// 消息结束时跳转到的菜单视图
        /// </summary>
        public int[] MsgOverThenJumpViewArr { get; set; }

        /// <summary>
        /// 非正常消息
        /// </summary>
        public bool AbnormalMsg { get; set; }

        /// <summary>
        /// 特殊处理方式
        /// </summary>
        public SpecialHanding TheSpecialHanding { get; set; }

        /// <summary>
        /// 消息已跳转
        /// </summary>
        public bool Jumped { get; set; }
    }

    public class MsgHandlerATP<T> : MsgHandler<T> where T : Message
    {
        public MsgHandlerATP()
        {
            specialMsgList = new List<T>();
            abnormalMsgList = new List<T>();
        }

        /// <summary>
        /// 添加新消息
        /// 根据消息等级添加到相应消息列表
        /// </summary>
        /// <param name="newMsg"></param>
        public override void AddNewMsg(T newMsg)
        {
            if (newMsg.AbnormalMsg)
            {
                abnormalMsgList.Add(newMsg);
                MsgListSort(ref abnormalMsgList, SortCriteriaOfMsg.FlagThenLevelThenTime);
            }
            else
            {
                if (newMsg.TheMsgLevel == MsgLevels.Level0)
                {
                    specialMsgList.Add(newMsg);
                }
                else
                {
                    base.AddNewMsg(newMsg);
                }
                
            }
        }

        /// <summary>
        /// 某条消息已阅，只对应SpecialMsgList
        /// </summary>
        /// <param name="theMsgInList">消息所在位置</param>
        public override void MsgBeRead(int theMsgInList)
        {
            if (specialMsgList.Count == 0) return;
            foreach (var tmp in specialMsgList)
            {
                //当前消息标记为已读
                tmp.TheMsgFlag = true;
            }
        }

        /// <summary>
        /// 清空所有消息列表
        /// </summary>
        public override void ClearAllList()
        {
            base.ClearAllList();
            specialMsgList.Clear();
        }

        public void CurrentMsgListSort(SortCriteriaOfMsg sortCriteria)
        {
            MsgListSort(ref allMsgsList, sortCriteria);
            MsgListSort(ref specialMsgList, sortCriteria);
        }

        /// <summary>
        /// 收到某条消息的结束命令，并赋值结束时间
        /// </summary>
        /// <param name="msgLogicId"></param>
        /// <param name="overTime"></param>
        public void MsgOver(int msgLogicId, DateTime overTime)
        {
            foreach (var tmp in allMsgsList.Where(tmp => tmp.MsgLogicID == msgLogicId && !tmp.FaultBeOver))
            {
                tmp.MsgOverTime = overTime;
                tmp.FaultBeOver = true;
                if (tmp.TheSpecialHanding != SpecialHanding.结束后删除) continue;
                allMsgsList.Remove(tmp);
                break;
            }
            if (specialMsgList.Count == 0) return;
            foreach (var tmp in specialMsgList.Where(tmp => tmp.MsgLogicID == msgLogicId))
            {
                specialMsgList.Remove(tmp);
                break;
            }
        }

        protected List<T> specialMsgList;
        /// <summary>
        /// 特殊消息列表
        /// </summary>
        public List<T> SpecialMsgList { get { return specialMsgList; } }

        protected List<T> abnormalMsgList;
        /// <summary>
        /// 非正常消息列表
        /// </summary>
        public List<T> AbnormalMsgList { get { return abnormalMsgList; } }
    }
}

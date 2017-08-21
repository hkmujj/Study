using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace MsgReceiveMod
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MsgHandlerFault0<T> : MsgHandler<T> where T : BaseMsgContent
    {
        /// <summary>
        /// 
        /// </summary>
        public MsgHandlerFault0()
        {
            currentMsgList = new List<T>();
        }

        /// <summary>
        /// 往消息列表添加新消息
        /// </summary>
        /// <param name="newMsg"></param>
        public override void AddNewMsg(T newMsg)
        {
            newMsg.MsgReceiveTime = DateTime.Now;
            base.AddNewMsg(newMsg);
            currentMsgList.Add(newMsg);
        }

        /// <summary>
        /// 往消息列表添加新消息
        /// </summary>
        /// <param name="newMsg"></param>
        /// <param name="theTime">设定时间</param>
        public virtual void AddNewMsg(T newMsg, DateTime theTime)
        {
            newMsg.MsgReceiveTime = theTime;
            base.AddNewMsg(newMsg);
            currentMsgList.Add(newMsg);
        }

        /// <summary>
        /// 某条消息已阅
        /// </summary>
        /// <param name="theMsgLogicId">消息在所有消息中位置</param>
        public override void MsgBeRead(int theMsgLogicId)
        {
            foreach (T tmp in currentMsgList)
            {
                if (tmp.MsgLogicID != theMsgLogicId) continue;
                //当前消息标记已读
                tmp.TheMsgFlag = true;
                tmp.AffirmTime = DateTime.Now;
                break;
            }
            base.MsgBeRead(theMsgLogicId);

        }

        /// <summary>
        /// 清空所有消息列表
        /// </summary>
        public override void ClearAllList()
        {
            base.ClearAllList();
            currentMsgList.Clear();
        }

        /// <summary>
        /// 当前消息列表按某个标准进行排序
        /// </summary>
        /// <param name="sortCriteria">消息排序方法</param>
        public void CurrentMsgListSort(SortCriteriaOfMsg sortCriteria)
        {
            MsgListSort(ref currentMsgList, sortCriteria);
        }

        /// <summary>
        /// 收到某条消息的结束命令
        /// </summary>
        /// <param name="msgLogicId">消息逻辑号</param>
        public virtual void MsgOver(int msgLogicId)
        {
            MsgOver(msgLogicId, DateTime.Now);
        }

        /// <summary>
        /// 收到某条消息的结束命令，并赋值结束时间
        /// </summary>
        /// <param name="msgLogicId">消息逻辑号</param>
        /// <param name="overTime">消息结束时间</param>
        public virtual void MsgOver(int msgLogicId, DateTime overTime)
        {
            foreach (T tmp in allMsgsList)
            {
                if (tmp.MsgLogicID != msgLogicId || tmp.FaultBeOver) continue;
                tmp.MsgOverTime = overTime;
                tmp.FaultBeOver = true;
            }

            foreach (T tmp in currentMsgList)
            {
                if (tmp.MsgLogicID != msgLogicId) continue;
                currentMsgList.Remove(tmp);
                break;
            }
        }

        /// <summary>
        /// 当前未结束消息重置已读
        /// </summary>
        public virtual void MsgReadAllReset()
        {
            foreach (T tmp in currentMsgList)
            {
                tmp.TheMsgFlag = false;
                tmp.AffirmTime = new DateTime();
            }
        }

        /// <summary>
        /// 当前选中未结束消息重置已读
        /// </summary>
        /// <param name="msgLogicId"></param>
        public virtual void CurrentMsgRest(int msgLogicId)
        {
            if (CurrentMsgList.Find(f => f.MsgLogicID == msgLogicId) != null)
            {
                var tmp = CurrentMsgList.FindIndex(f => f.MsgLogicID == msgLogicId);
                CurrentMsgList[tmp].TheMsgFlag = false;
                CurrentMsgList[tmp].AffirmTime = new DateTime();

            }
        }
        protected List<T> currentMsgList;
        /// <summary>
        /// 当前消息
        /// </summary>
        public List<T> CurrentMsgList { get { return currentMsgList; } }
    }
}
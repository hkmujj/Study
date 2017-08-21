using System;
using System.Collections.Generic;
using System.Linq;
using MsgReceiveMod;

namespace Motor.HMI.CRH5G.底层共用.消息
{
    public class CRH5MsgHandler<T> : MsgHandlerFault0<T> where T : BaseMsgContent
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unreadCurrentMsgSort">当前未读故障初始排序方法</param>
        public CRH5MsgHandler(SortCriteriaOfMsg unreadCurrentMsgSort)
        {
            m_UnFlagCurrentMsgList = new List<T>();
            m_UnFlagMsgSortName = unreadCurrentMsgSort;
        }

        /// <summary>
        /// 消息列表中添加新消息
        /// 所有消息、当前消息、当前未读消息
        /// </summary>
        /// <param name="newMsg"></param>
        public override void AddNewMsg(T newMsg)
        {
            base.AddNewMsg(newMsg);
            m_UnFlagCurrentMsgList.Add(newMsg);

            UnFlagMsgListSort(m_UnFlagMsgSortName);
        }

        public override void AddNewMsg(T newMsg, DateTime theTime)
        {
            base.AddNewMsg(newMsg, theTime);

            m_UnFlagCurrentMsgList.Add(newMsg);

            UnFlagMsgListSort(m_UnFlagMsgSortName);
        }

        /// <summary>
        /// 消息已阅
        /// </summary>
        /// <param name="theMsgLogicId"></param>
        public override void MsgBeRead(int theMsgLogicId)
        {
            foreach (var tmp in m_UnFlagCurrentMsgList.Where(tmp => tmp.MsgLogicID == theMsgLogicId))
            {
                m_UnFlagCurrentMsgList.Remove(tmp);
                break;
            }
            base.MsgBeRead(theMsgLogicId);
        }

        /// <summary>
        /// 故障结束
        /// </summary>
        /// <param name="msgLogicId">消息逻辑号</param>
        /// <param name="overTime">消息结束时间</param>
        public override void MsgOver(int msgLogicId, DateTime overTime)
        {
            foreach (T tmp in m_UnFlagCurrentMsgList.Where(tmp => tmp.MsgLogicID == msgLogicId))
            {
                m_UnFlagCurrentMsgList.Remove(tmp);
                break;
            }
        }

        /// <summary>
        /// 故障列表清空
        /// </summary>
        public override void ClearAllList()
        {
            base.ClearAllList();
            m_UnFlagCurrentMsgList.Clear();
        }

        /// <summary>
        /// 未读消息排序
        /// </summary>
        /// <param name="sortCriteria"></param>
        public void UnFlagMsgListSort(SortCriteriaOfMsg sortCriteria)
        {
            MsgListSort(ref m_UnFlagCurrentMsgList, sortCriteria);
        }

        protected List<T> m_UnFlagCurrentMsgList;
        /// <summary>
        /// 未读当前故障
        /// </summary>
        public List<T> UnFlagCurrentMsgList { get { return m_UnFlagCurrentMsgList; } }

        /// <summary>
        /// 排序方式
        /// </summary>
        private readonly SortCriteriaOfMsg m_UnFlagMsgSortName;
    }
}

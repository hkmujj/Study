using System;
using System.Collections.Generic;

namespace MsgReceiveMod
{
    /// <summary>
    /// 消息接收处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MsgHandler<T> where T : BaseMsgContent
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MsgHandler()
        {
            //TODO:
            allMsgsList = new List<T>();
        }

        /// <summary>
        /// 往消息列表添加新消息
        /// </summary>
        /// <param name="newMsg"></param>
        public virtual void AddNewMsg(T newMsg)
        {
            allMsgsList.Add(newMsg);
        }

        /// <summary>
        /// 某条消息已阅
        /// </summary>
        /// <param name="theMsgLogicId">消息逻辑号</param>
        public virtual void MsgBeRead(int theMsgLogicId)
        {
            foreach (var variable in AllMsgsList)
            {
                if (variable.MsgLogicID != theMsgLogicId || variable.FaultBeOver) continue;
                variable.TheMsgFlag = true;
                break;
            }
        }

        /// <summary>
        /// 清空所有消息列表
        /// </summary>
        public virtual void ClearAllList()
        {
            allMsgsList.Clear();
        }

        /// <summary>
        /// 消息列表按某个标准进行排序
        /// </summary>
        /// <param name="sortCriteria"></param>
        public void AllMsgListSort(SortCriteriaOfMsg sortCriteria)
        {
            MsgListSort(ref allMsgsList, sortCriteria);
        }

        /// <summary>
        /// 消息排序
        /// </summary>
        /// <param name="beSortList">需要排序的列表</param>
        /// <param name="sortCriteria">排序方式</param>
        protected void MsgListSort(ref List<T> beSortList, SortCriteriaOfMsg sortCriteria)
        {
            MsgSort.MsgSortBy = sortCriteria;

            List<ILevelAndTime> tempAllMsgsList = beSortList.ConvertAll(new Converter<T, ILevelAndTime>(BaseMsgContentToILevelAndTime));
            tempAllMsgsList.Sort(MsgSort);

            beSortList = tempAllMsgsList.ConvertAll(new Converter<ILevelAndTime, T>(ILevelAndTimeToBaseMsgContent));
        }

        private ILevelAndTime BaseMsgContentToILevelAndTime(T temp)
        {
            return temp;
        }

        private T ILevelAndTimeToBaseMsgContent(ILevelAndTime temp)
        {
            return temp as T;
        }

// ReSharper disable once InconsistentNaming
        protected List<T> allMsgsList;
        /// <summary>
        /// 所有故障列表
        /// </summary>
        public List<T> AllMsgsList { get { return allMsgsList; } }

        protected MsgSort MsgSort = new MsgSort();

    }
}

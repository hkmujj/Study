using System;

namespace MsgReceiveMod
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KmMmiMsgHandler<T> : MsgHandlerFault0<T> where T : BaseMsgContent
    {
        /// <summary>
        /// 
        /// </summary>
        public KmMmiMsgHandler()
        {
            _highMsg = default(T);
        }

        public override void AddNewMsg(T newMsg)
        {
            base.AddNewMsg(newMsg);
            if (newMsg.TheMsgLevel != MsgLevels.Level0) return;
            //存在高优先级消息的话重置计数器
            if (_highMsg != null) _timeCount = 0;

            _needShowHighMsg = true;
            _highMsg = newMsg;
        }

        public override void MsgOver(int msgLogicId, DateTime overTime)
        {
            foreach (T tmp in allMsgsList)
            {
                if (tmp.MsgLogicID != msgLogicId || !tmp.FaultBeOver) continue;
                tmp.MsgOverTime = overTime;
                tmp.FaultBeOver = true;
            }

            foreach (T tmp in currentMsgList)
            {
                if (_highMsg != null && tmp.MsgID == _highMsg.MsgID)
                {
                    _needShowHighMsg = false;
                    _highMsg = null;
                }
                if (tmp.MsgLogicID != msgLogicId) continue;
                currentMsgList.Remove(tmp);
                return;
            }

        }

        public override void MsgOver(int msgLogicId)
        {
            foreach (T tmp in allMsgsList)
            {
                if (tmp.MsgLogicID != msgLogicId || tmp.FaultBeOver) continue;
                tmp.MsgOverTime = DateTime.Now;
                tmp.FaultBeOver = true;
            }

            foreach (T tmp in currentMsgList)
            {
                if (_highMsg != null && tmp.MsgID == _highMsg.MsgID)
                {
                    _needShowHighMsg = false;
                    _highMsg = null;
                }
                if (tmp.MsgLogicID != msgLogicId) continue;
                currentMsgList.Remove(tmp);
                return;
            }
        }

        int _timeCount;

        /// <summary>
        /// 改变消息显示状态
        /// -1表示没有高优先级的消息要显示
        /// 0表示当前高优先级消息显示时间到，不显示
        /// 1表示当前高优先级消息显示中
        /// </summary>
        /// <param name="maxTimeCount"></param>
        public int MsgHasShowed(int maxTimeCount)
        {
            if (_highMsg == null)
            {
                _timeCount = 0;
                _needShowHighMsg = false;
                return -1;
            }

            _timeCount++;
            if (_timeCount <= maxTimeCount) return 1;
            _highMsg = null;
            _needShowHighMsg = false;
            _timeCount = 0;
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortCriteria"></param>
        public void MsgListSort(SortCriteriaOfMsg sortCriteria)
        {
            AllMsgListSort(sortCriteria);
            CurrentMsgListSort(sortCriteria);
        }

        private bool _needShowHighMsg;
        /// <summary>
        /// 是否需要显示高优先级消息
        /// </summary>
        public bool NeedShowHighMsg { get { return _needShowHighMsg; } }

        private T _highMsg;

        /// <summary>
        /// 
        /// </summary>
        public T HighMsg
        {
            get { return _highMsg; }
        }
    }
}

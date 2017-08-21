using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace KumM_MMI.MMI_Message
{
    public class MessageEnf
    {
        public MessageEnf()
        {
            _objCreatNumb++;
        }

        /// <summary>
        /// 收到消息
        /// </summary>
        public void NewMsgAdd()
        {
            _msgSequence++;
        }

        #region ::::::::::::::::::::::::: 读消息表得到的信息 ::::::::::::::::::::::::::::
        private int _msgID;
        /// <summary>
        /// 消息ID
        /// 消息列表里可以多处出现
        /// </summary>
        public int MsgID { get { return _msgID; } set { _msgID = value; } }

        private string _msgName;
        /// <summary>
        /// 消息名
        /// </summary>
        public string MsgName { get { return _msgName; } set { _msgName = value; } }

        private int _msgPriority;
        /// <summary>
        /// 消息优先级
        /// 数字越小优先级越高
        /// 优先级不能为负数
        /// </summary>
        public int MesPriority
        {
            get { return _msgPriority; }
            set
            {
                if (value >= 0)
                    _msgPriority = value;
            }
        }

        private string _msgSolution;
        /// <summary>
        /// 解决方法
        /// </summary>
        public string MsgSolution { get { return _msgSolution; } set { _msgSolution = value; } }

        private Image _msgImg;
        /// <summary>
        /// 故障等级表示的图片
        /// </summary>
        public Image MsgImg { get { return _msgImg; } set { _msgImg = value; } }

        private Image _msgSolutionImg;
        /// <summary>
        /// 故障解决方法图片
        /// </summary>
        public Image MsgSolutionImg { get { return _msgSolutionImg; } set { _msgSolutionImg = value; } }
        #endregion

        #region :::::::::::::::::::::: 消息状态变化后添加的信息 :::::::::::::::::::::::::
        private bool _msgHasShowed;
        /// <summary>
        /// 消息是否已经显示
        /// </summary>
        public bool MsgHasShowed
        {
            get { return _msgHasShowed; }
            set 
            {
                if (_msgPriority == 0)
                {
                    _msgHasShowed = value;
                }
            }
        }

        private DateTime _msgStartTime;
        /// <summary>
        /// 消息发生时间
        /// </summary>
        public DateTime MsgStartTime { get { return _msgStartTime; } set { _msgStartTime = value; } }

        private DateTime _msgOverTime;
        /// <summary>
        /// 消息结束时间
        /// </summary>
        public DateTime MsgOverTime { get { return _msgOverTime; } set { _msgOverTime = value; } }

        private bool _msgIsOver;
        /// <summary>
        /// 故障是否结束
        /// </summary>
        public bool MsgIsOver { get { return _msgIsOver; } set { _msgIsOver = value; } }

        private int _msgIndex;
        /// <summary>
        /// 消息在列表中的序列编号
        /// 唯一
        /// </summary>
        public int MsgIndex { get { return _msgIndex; } set { _msgIndex = value; } }
        #endregion

        private static int _msgSequence = -1;
        /// <summary>
        /// 用来记录消息列表中所有消息个数
        /// </summary>
        public int MsgSequence { get { return _msgSequence; } }

        private static int _objCreatNumb = 0;
        /// <summary>
        /// 被实例化次数
        /// </summary>
        public static int ObjCreatNumb { get { return _objCreatNumb; } }
    }

    public class MessageDis
    {
        /// <summary>
        /// 消息添加到列表中
        /// </summary>
        /// <param name="msgObj"></param>
        public void AddSorted(MessageEnf msgObj)
        {
            bool isNeedAddList = false;      //判断当前消息是否要添加到消息列表
            _catchNewMsg = false;

            if (_msgRecList.Count <= 0)     //列表为空
            {
                isNeedAddList = true;
            }
            else
            {
                foreach (MessageEnf obj in _msgRecList.Values)
                {
                    //是否有相同且未结束的消息存在，存在就不需要添加
                    if (obj.MsgID.Equals(msgObj.MsgID) && !obj.MsgIsOver)
                    {
                        isNeedAddList = false;
                        break;
                    }
                    else isNeedAddList = true;
                }
            }

            _listIsOpering = true;

            //将消息添加到消息列表
            if (isNeedAddList)
            {
                msgObj.MsgStartTime = DateTime.Now;                 //当前时间添加到消息发生时间中
                msgObj.NewMsgAdd();                               //消息唯一标识
                if (!_msgRecList.ContainsKey(msgObj.MsgSequence))
                {
                    msgObj.MsgIndex = msgObj.MsgSequence;
                    _msgRecList.Add(msgObj.MsgIndex, msgObj);                            //所有消息
                    _noOverMsgRecList.Add(msgObj.MsgIndex, msgObj);                      //所有未结束消息
                    if (msgObj.MesPriority == 0)                        //高优先级
                    {
                        _allHighPriMsgRecList.Add(msgObj.MsgIndex, msgObj);              //所有高优先级消息
                        _noOverHighPriMsgRecList.Add(msgObj.MsgIndex, msgObj);           //未结束高优先级消息
                        _showHighPriMsg = msgObj;                            //需要显示的高优先级消息
                        _needShowHighMsg = true;
                        timeCount = 0;
                    }
                    else
                        _catchNewMsg = true;
                }
            }
            _listIsOpering = false;
            
        }

        int timeCount = 0;
        /// <summary>
        /// 改变消息显示状态
        /// -1表示没有高优先级的消息要显示
        /// 0表示当前高优先级消息显示时间到，不显示
        /// 1表示当前高优先级消息显示中
        /// </summary>
        /// <param name="msgObj"></param>
        public int MsgHasShowed()
        {
            if (_showHighPriMsg == emptyHighPriMsg)
            {
                timeCount = 0;
                _needShowHighMsg = false;
                return -1;
            }

            timeCount++;
            if (timeCount > 50)
            {
                _listIsOpering = true;

                //更新各列表
                _msgRecList[_showHighPriMsg.MsgIndex].MsgHasShowed = true;
                _allHighPriMsgRecList[_showHighPriMsg.MsgIndex].MsgHasShowed = true;
                _noOverHighPriMsgRecList[_showHighPriMsg.MsgIndex].MsgHasShowed = true;

                _showHighPriMsg = emptyHighPriMsg;
                _needShowHighMsg = false;

                _listIsOpering = false;

                return 0;
            }

            return 1;
        }

        /// <summary>
        /// 改变消息结束状态和结束时间
        /// </summary>
        /// <param name="msgObj"></param>
        public void MsgHasOver(int msgObjID)
        {
            if (_msgRecList.Count <= 0) return;

            bool beMsg = false;     //是否存在需要改变状态的元素
            int Index = 0;          //元素的位置

            foreach (MessageEnf obj in _noOverMsgRecList.Values)
            {
                if (obj.MsgID == msgObjID)
                {
                    Index = obj.MsgIndex;
                    beMsg = true;
                }
            }

            if (beMsg)
            {
                DateTime dt = DateTime.Now; //故障结束时间

                _listIsOpering = true;

                //更新所有消息列表的状态
                _msgRecList[Index].MsgIsOver = true;
                _msgRecList[Index].MsgOverTime = dt;

                //从当前消息列表移除此消息
                _noOverMsgRecList.Remove(Index);

                if (_msgRecList[Index].MesPriority == 0)
                {
                    //更新所有高优先级消息列表
                    _allHighPriMsgRecList[Index].MsgIsOver = true;
                    _allHighPriMsgRecList[Index].MsgOverTime = dt;

                    //从当前高优先级消息队列移除此消息
                    _noOverHighPriMsgRecList.Remove(Index);

                    //更新需要显示高优先级消息列表
                    if (_showHighPriMsg.MsgIndex == _msgRecList[Index].MsgIndex)
                    {
                        //消息都结束了，自然不需要显示了
                        _showHighPriMsg = emptyHighPriMsg;
                        _needShowHighMsg = false;
                    }                    
                }

                _listIsOpering = false;
            }
        }

        /// <summary>
        /// 课程结束，清空所有列表
        /// </summary>
        public void ClassOver()
        {
            _msgRecList.Clear();
            _allHighPriMsgRecList.Clear();
            _catchNewMsg = false;
            _listIsOpering = false;
            _needShowHighMsg = false;
            _noOverHighPriMsgRecList.Clear();
            _noOverMsgRecList.Clear();
            _showHighPriMsg = emptyHighPriMsg;
        }
        ///////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        private MessageEnf emptyHighPriMsg = new MessageEnf();

        private bool _catchNewMsg = false;
        /// <summary>
        /// 是否捕获到新的低优先级消息
        /// </summary>
        public bool CatchNewMsg { get { return _catchNewMsg; } }

        private bool _listIsOpering = false;
        /// <summary>
        /// 列表是否正在进行添加/删除操作
        /// </summary>
        public bool ListIsOpering { get { return _listIsOpering; } }

        private bool _needShowHighMsg = false;
        /// <summary>
        /// 高优先级消息是否有新消息
        /// </summary>
        public bool NeedShowHighMsg { get { return _needShowHighMsg; } }

        #region ::::::::::::::::::::::::: 消息列表 ::::::::::::::::::::::::::::::::::::
        private Dictionary<int, MessageEnf> _msgRecList = new Dictionary<int, MessageEnf>();
        /// <summary>
        /// 所有消息接收列表
        /// </summary>
        public Dictionary<int, MessageEnf> MsgRecList { get { return _msgRecList; } }

        private Dictionary<int, MessageEnf> _noOverMsgRecList = new Dictionary<int, MessageEnf>();
        /// <summary>
        /// 所有未结束的消息接收列表
        /// </summary>
        public Dictionary<int, MessageEnf> NoOverMsgRecList { get { return _noOverMsgRecList; } }

        private Dictionary<int, MessageEnf> _allHighPriMsgRecList = new Dictionary<int, MessageEnf>();
        /// <summary>
        /// 所有高优先级消息接收列表
        /// </summary>
        public Dictionary<int, MessageEnf> AllHighPriMsgRecList { get { return _allHighPriMsgRecList; } }

        private Dictionary<int, MessageEnf> _noOverHighPriMsgRecList = new Dictionary<int, MessageEnf>();
        /// <summary>
        /// 未结束高优先级消息接收列表
        /// </summary>
        public Dictionary<int, MessageEnf> NoOverHighPriMsgRecList { get { return _noOverHighPriMsgRecList; } }

        private MessageEnf _showHighPriMsg = new MessageEnf();
        /// <summary>
        /// 需要显示的高优先级消息
        /// </summary>
        public MessageEnf ShowHighPriMsg { get { return _showHighPriMsg; } }

        #endregion
    }
}

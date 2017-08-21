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
        /// �յ���Ϣ
        /// </summary>
        public void NewMsgAdd()
        {
            _msgSequence++;
        }

        #region ::::::::::::::::::::::::: ����Ϣ��õ�����Ϣ ::::::::::::::::::::::::::::
        private int _msgID;
        /// <summary>
        /// ��ϢID
        /// ��Ϣ�б�����Զദ����
        /// </summary>
        public int MsgID { get { return _msgID; } set { _msgID = value; } }

        private string _msgName;
        /// <summary>
        /// ��Ϣ��
        /// </summary>
        public string MsgName { get { return _msgName; } set { _msgName = value; } }

        private int _msgPriority;
        /// <summary>
        /// ��Ϣ���ȼ�
        /// ����ԽС���ȼ�Խ��
        /// ���ȼ�����Ϊ����
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
        /// �������
        /// </summary>
        public string MsgSolution { get { return _msgSolution; } set { _msgSolution = value; } }

        private Image _msgImg;
        /// <summary>
        /// ���ϵȼ���ʾ��ͼƬ
        /// </summary>
        public Image MsgImg { get { return _msgImg; } set { _msgImg = value; } }

        private Image _msgSolutionImg;
        /// <summary>
        /// ���Ͻ������ͼƬ
        /// </summary>
        public Image MsgSolutionImg { get { return _msgSolutionImg; } set { _msgSolutionImg = value; } }
        #endregion

        #region :::::::::::::::::::::: ��Ϣ״̬�仯����ӵ���Ϣ :::::::::::::::::::::::::
        private bool _msgHasShowed;
        /// <summary>
        /// ��Ϣ�Ƿ��Ѿ���ʾ
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
        /// ��Ϣ����ʱ��
        /// </summary>
        public DateTime MsgStartTime { get { return _msgStartTime; } set { _msgStartTime = value; } }

        private DateTime _msgOverTime;
        /// <summary>
        /// ��Ϣ����ʱ��
        /// </summary>
        public DateTime MsgOverTime { get { return _msgOverTime; } set { _msgOverTime = value; } }

        private bool _msgIsOver;
        /// <summary>
        /// �����Ƿ����
        /// </summary>
        public bool MsgIsOver { get { return _msgIsOver; } set { _msgIsOver = value; } }

        private int _msgIndex;
        /// <summary>
        /// ��Ϣ���б��е����б��
        /// Ψһ
        /// </summary>
        public int MsgIndex { get { return _msgIndex; } set { _msgIndex = value; } }
        #endregion

        private static int _msgSequence = -1;
        /// <summary>
        /// ������¼��Ϣ�б���������Ϣ����
        /// </summary>
        public int MsgSequence { get { return _msgSequence; } }

        private static int _objCreatNumb = 0;
        /// <summary>
        /// ��ʵ��������
        /// </summary>
        public static int ObjCreatNumb { get { return _objCreatNumb; } }
    }

    public class MessageDis
    {
        /// <summary>
        /// ��Ϣ��ӵ��б���
        /// </summary>
        /// <param name="msgObj"></param>
        public void AddSorted(MessageEnf msgObj)
        {
            bool isNeedAddList = false;      //�жϵ�ǰ��Ϣ�Ƿ�Ҫ��ӵ���Ϣ�б�
            _catchNewMsg = false;

            if (_msgRecList.Count <= 0)     //�б�Ϊ��
            {
                isNeedAddList = true;
            }
            else
            {
                foreach (MessageEnf obj in _msgRecList.Values)
                {
                    //�Ƿ�����ͬ��δ��������Ϣ���ڣ����ھͲ���Ҫ���
                    if (obj.MsgID.Equals(msgObj.MsgID) && !obj.MsgIsOver)
                    {
                        isNeedAddList = false;
                        break;
                    }
                    else isNeedAddList = true;
                }
            }

            _listIsOpering = true;

            //����Ϣ��ӵ���Ϣ�б�
            if (isNeedAddList)
            {
                msgObj.MsgStartTime = DateTime.Now;                 //��ǰʱ����ӵ���Ϣ����ʱ����
                msgObj.NewMsgAdd();                               //��ϢΨһ��ʶ
                if (!_msgRecList.ContainsKey(msgObj.MsgSequence))
                {
                    msgObj.MsgIndex = msgObj.MsgSequence;
                    _msgRecList.Add(msgObj.MsgIndex, msgObj);                            //������Ϣ
                    _noOverMsgRecList.Add(msgObj.MsgIndex, msgObj);                      //����δ������Ϣ
                    if (msgObj.MesPriority == 0)                        //�����ȼ�
                    {
                        _allHighPriMsgRecList.Add(msgObj.MsgIndex, msgObj);              //���и����ȼ���Ϣ
                        _noOverHighPriMsgRecList.Add(msgObj.MsgIndex, msgObj);           //δ���������ȼ���Ϣ
                        _showHighPriMsg = msgObj;                            //��Ҫ��ʾ�ĸ����ȼ���Ϣ
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
        /// �ı���Ϣ��ʾ״̬
        /// -1��ʾû�и����ȼ�����ϢҪ��ʾ
        /// 0��ʾ��ǰ�����ȼ���Ϣ��ʾʱ�䵽������ʾ
        /// 1��ʾ��ǰ�����ȼ���Ϣ��ʾ��
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

                //���¸��б�
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
        /// �ı���Ϣ����״̬�ͽ���ʱ��
        /// </summary>
        /// <param name="msgObj"></param>
        public void MsgHasOver(int msgObjID)
        {
            if (_msgRecList.Count <= 0) return;

            bool beMsg = false;     //�Ƿ������Ҫ�ı�״̬��Ԫ��
            int Index = 0;          //Ԫ�ص�λ��

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
                DateTime dt = DateTime.Now; //���Ͻ���ʱ��

                _listIsOpering = true;

                //����������Ϣ�б��״̬
                _msgRecList[Index].MsgIsOver = true;
                _msgRecList[Index].MsgOverTime = dt;

                //�ӵ�ǰ��Ϣ�б��Ƴ�����Ϣ
                _noOverMsgRecList.Remove(Index);

                if (_msgRecList[Index].MesPriority == 0)
                {
                    //�������и����ȼ���Ϣ�б�
                    _allHighPriMsgRecList[Index].MsgIsOver = true;
                    _allHighPriMsgRecList[Index].MsgOverTime = dt;

                    //�ӵ�ǰ�����ȼ���Ϣ�����Ƴ�����Ϣ
                    _noOverHighPriMsgRecList.Remove(Index);

                    //������Ҫ��ʾ�����ȼ���Ϣ�б�
                    if (_showHighPriMsg.MsgIndex == _msgRecList[Index].MsgIndex)
                    {
                        //��Ϣ�������ˣ���Ȼ����Ҫ��ʾ��
                        _showHighPriMsg = emptyHighPriMsg;
                        _needShowHighMsg = false;
                    }                    
                }

                _listIsOpering = false;
            }
        }

        /// <summary>
        /// �γ̽�������������б�
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
        /// �Ƿ񲶻��µĵ����ȼ���Ϣ
        /// </summary>
        public bool CatchNewMsg { get { return _catchNewMsg; } }

        private bool _listIsOpering = false;
        /// <summary>
        /// �б��Ƿ����ڽ������/ɾ������
        /// </summary>
        public bool ListIsOpering { get { return _listIsOpering; } }

        private bool _needShowHighMsg = false;
        /// <summary>
        /// �����ȼ���Ϣ�Ƿ�������Ϣ
        /// </summary>
        public bool NeedShowHighMsg { get { return _needShowHighMsg; } }

        #region ::::::::::::::::::::::::: ��Ϣ�б� ::::::::::::::::::::::::::::::::::::
        private Dictionary<int, MessageEnf> _msgRecList = new Dictionary<int, MessageEnf>();
        /// <summary>
        /// ������Ϣ�����б�
        /// </summary>
        public Dictionary<int, MessageEnf> MsgRecList { get { return _msgRecList; } }

        private Dictionary<int, MessageEnf> _noOverMsgRecList = new Dictionary<int, MessageEnf>();
        /// <summary>
        /// ����δ��������Ϣ�����б�
        /// </summary>
        public Dictionary<int, MessageEnf> NoOverMsgRecList { get { return _noOverMsgRecList; } }

        private Dictionary<int, MessageEnf> _allHighPriMsgRecList = new Dictionary<int, MessageEnf>();
        /// <summary>
        /// ���и����ȼ���Ϣ�����б�
        /// </summary>
        public Dictionary<int, MessageEnf> AllHighPriMsgRecList { get { return _allHighPriMsgRecList; } }

        private Dictionary<int, MessageEnf> _noOverHighPriMsgRecList = new Dictionary<int, MessageEnf>();
        /// <summary>
        /// δ���������ȼ���Ϣ�����б�
        /// </summary>
        public Dictionary<int, MessageEnf> NoOverHighPriMsgRecList { get { return _noOverHighPriMsgRecList; } }

        private MessageEnf _showHighPriMsg = new MessageEnf();
        /// <summary>
        /// ��Ҫ��ʾ�ĸ����ȼ���Ϣ
        /// </summary>
        public MessageEnf ShowHighPriMsg { get { return _showHighPriMsg; } }

        #endregion
    }
}

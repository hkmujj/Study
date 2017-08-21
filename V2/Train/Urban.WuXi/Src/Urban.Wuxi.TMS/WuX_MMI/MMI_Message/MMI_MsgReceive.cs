using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.PublicUI;
using MMI_Message;

namespace KumM_MMI.MMI_Message
{
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class MMI_MsgReceive : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "消息接收模块";
        }

        public override int getInBoolParaCount()
        {
            return 0;
        }

        public override int getInFloatParaCount()
        {
            return 0;
        }

        public override int getUIParaCount()
        {
            return 0;
        }

        /////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////
        public override string GetTypeName()
        {
            return "MMI_MsgReceive";
        }

        public override bool initObject(string nPara)
        {
            return base.initObject(nPara);
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        public override bool canSetStringList(int nPara)
        {
            switch (nPara)
            {
                case 0:
                    readTxtID = 10;
                    return true;
                default:
                    return false;
            }
        }

        public override void addStringByLine(int nIndex, string cStr)
        {
            if (readTxtID == 10)
            {
                String[] split = cStr.Split(new char[] { '\t' });
                String[] tmp = new String[4];
                int i = 0;
                foreach (string s in split)
                {
                    if (s.Trim() != "")
                    {
                        if (i < 4)
                            tmp[i] = s;
                        i++;
                    }
                    if (i == 4)
                    {
                        _AllMsgList.Add(int.Parse(tmp[0]), tmp);
                        return;
                    }
                }
            }
        }
        #endregion

        #region :::::::::::::::::::::::::: event override   :::::::::::::::::::::::::
        public override void paint(Graphics dcGs)
        {
            RecAndDispMsg();
            base.paint(dcGs);
        }
        #endregion

        #region ::::::::::::::::::::::::::   ex funes    :::::::::::::::::::::::::
        string[] strTmp;
        /// <summary>
        /// 接受和处理消息
        /// </summary>
        private void RecAndDispMsg()
        {
            _receiveNewMsg = false;
            for (int i = 0; i < 100; i++)
            {
                //消息开始
                if (BoolList[100 + i] && !BoolOldList[100 + i] && _AllMsgList.ContainsKey(100 + i))
                {
                    MessageEnf msgTmp = new MessageEnf();
                    strTmp = _AllMsgList[100 + i];
                    msgTmp.MsgID = Convert.ToInt32(strTmp[1]);
                    msgTmp.MsgName = strTmp[2];
                    msgTmp.MesPriority = Convert.ToInt32(strTmp[3]);
                    if (!msgInf.ListIsOpering)
                        msgInf.AddSorted(msgTmp);
                    _receiveNewMsg = msgInf.CatchNewMsg;
                }
            }
            for (int i = 0; i < 100; i++)
            {
                //消息结束
                if (BoolOldList[100 + i] && !BoolList[100 + i] && _AllMsgList.ContainsKey(100 + i))
                {
                    strTmp = _AllMsgList[100 + i];
                    if (!msgInf.ListIsOpering)
                        msgInf.MsgHasOver(Convert.ToInt32(strTmp[1]));
                }
            }
        }
        #endregion

        #region :::::::::::::::::::::::::: init funes :::::::::::::::::::::::::
        private void InitData()
        {
            _AllMsgList = new SortedDictionary<int, string[]>();

            msgInf = new MessageDis();
        }

        /// <summary>
        /// 消息列表
        /// </summary>
        public SortedDictionary<int, string[]> _AllMsgList;

        /// <summary>
        /// 所有发生过的消息
        /// </summary>
        public static MessageDis msgInf;

        /// <summary>
        /// 读取文本信息的编号
        /// </summary>
        private int readTxtID;

        private static bool _receiveNewMsg;
        /// <summary>
        /// 收到新消息
        /// </summary>
        public static bool ReceiveNewMsg { get { return _receiveNewMsg; } }
        #endregion
    }
}

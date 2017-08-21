using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.PublicUI;
using MMI.Facility.Interface.Data;
using MsgReceiveMod;

namespace KumM_MMI.MMI_Message
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class MMI_MsgReceive : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "消息接收模块";
        }



        public override bool init(ref int nErrorObjectIndex)
        {
            LoadFile();
            InitData();

            return true;
        }

        private void LoadFile()
        {
            var file = Path.Combine(RecPath, "..\\config\\消息通知.txt");
            foreach (var line in File.ReadAllLines(file, Encoding.Default))
            {
                AddStringByLine(line);
            }
        }

        public void AddStringByLine(string cStr)
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
            for (int i = 0; i < 100; i++)
            {
                //消息开始
                if (BoolList[100 + i] && !HasMsg(100 + i) && _AllMsgList.ContainsKey(100 + i))
                {
                    FaultMsgOrdinary msgTmp = new FaultMsgOrdinary();
                    strTmp = _AllMsgList[100 + i];
                    msgTmp.MsgID = strTmp[1];
                    msgTmp.MsgContent = strTmp[2];
                    msgTmp.TheMsgLevel = (MsgLevels)Enum.ToObject(typeof(MsgLevels), (Convert.ToInt32(strTmp[3])));
                    msgTmp.MsgLogicID = 100 + i;
                    msgTmp.MsgReceiveTime = this.NowTime();

                    msgInf.AddNewMsg(msgTmp);

                }
            }
            for (int i = 0; i < 100; i++)
            {
                //消息结束
                if (!BoolList[100 + i] && HasMsg(100 + i) && _AllMsgList.ContainsKey(100 + i))
                {
                    strTmp = _AllMsgList[100 + i];
                    msgInf.MsgOver(100 + i, this.NowTime());
                }
            }
        }

        private bool HasMsg(int logic)
        {
            return msgInf.CurrentMsgList.Any(msgOrdinary => msgOrdinary.MsgLogicID == logic);
        }

        #endregion

        #region :::::::::::::::::::::::::: init funes :::::::::::::::::::::::::
        private void InitData()
        {
            msgInf = new KmMmiMsgHandler<FaultMsgOrdinary>();
        }

        /// <summary>
        /// 消息列表
        /// </summary>
        public SortedDictionary<int, string[]> _AllMsgList = new SortedDictionary<int, string[]>();

        /// <summary>
        /// 所有发生过的消息
        /// </summary>
        public static KmMmiMsgHandler<FaultMsgOrdinary> msgInf;

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

    [GksDataType(DataType.isMMIObjectClass)]
    public class MMI_ClassBegin : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "MMI课程开始";
        }

        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics dcGs)
        {

        }
        #endregion

    }

    [GksDataType(DataType.isMMIObjectClass)]
    public class MMI_Black : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "MMI黑屏";
        }


        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[0], 1, 0);
            append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[1], 1, 0);

            return true;
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics dcGs)
        {
            GetValue();
            base.paint(dcGs);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void GetValue()
        {
            if (BoolList[UIObj.InBoolList[0]])
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            if (BoolList[UIObj.InBoolList[1]])
            {
                append_postCmd(CmdType.ChangePage, 4, 0, 0);
            }
        }
        #endregion

    }

    [GksDataType(DataType.isMMIObjectClass)]
    public class MMI_ClassOver : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "MMI课程结束视图";
        }

        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 1)
            {
                _msgClear = true;
            }
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics dcGs)
        {
            GetValue();
            base.paint(dcGs);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void GetValue()
        {
            if (_msgClear)
            {
                MMI_MsgReceive.msgInf.ClearAllList();
            }

        }
        #endregion

        private bool _msgClear = false;

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MsgReceiveMod;

namespace LightRail.ATC.Casco.MMI_Message
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
            InitData();
            LoadNessgeInfo();
            return true;
        }

        void LoadNessgeInfo()
        {
            var file = Path.Combine(RecPath, "..\\config\\消息通知.txt");
            var allLine = File.ReadAllLines(file, Encoding.Default);
            foreach (var line in allLine)
            {
                var slip = line.Split(new[] { '\t' });
                int i = 0;
                var tmp = new string[4];
                foreach (string s in slip)
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
                        break;
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
        public static List<int> taggerList = new List<int>();
        /// <summary>
        /// 接受和处理消息
        /// </summary>
        private void RecAndDispMsg()
        {
            for (int i = 0; i < 100; i++)
            {
                //消息开始
                if (BoolList[100 + i] && !taggerList.Contains(100 + i) && _AllMsgList.ContainsKey(100 + i))
                {
                    FaultMsgOrdinary msgTmp = new FaultMsgOrdinary();
                    strTmp = _AllMsgList[100 + i];
                    msgTmp.MsgID = strTmp[1];
                    msgTmp.MsgContent = strTmp[2];
                    msgTmp.TheMsgLevel = (MsgLevels)Enum.ToObject(typeof(MsgLevels), (Convert.ToInt32(strTmp[3])));
                    msgTmp.MsgLogicID = 100 + i;
                    msgTmp.MsgReceiveTime = TheTimeNow.CurrentTime;

                    MsgInf.AddNewMsg(msgTmp);
                    taggerList.Add(i + 100);
                }
            }
            for (int i = 0; i < 100; i++)
            {
                //消息结束
                if (!BoolList[100 + i] && taggerList.Contains(100 + i) && _AllMsgList.ContainsKey(100 + i))
                {
                    strTmp = _AllMsgList[100 + i];
                    MsgInf.MsgOver(100 + i, TheTimeNow.CurrentTime);
                    taggerList.Remove(100 + i);
                }
            }
        }
        #endregion

        #region :::::::::::::::::::::::::: init funes :::::::::::::::::::::::::
        private void InitData()
        {
            MsgInf = new KmMmiMsgHandler<FaultMsgOrdinary>();
        }

        /// <summary>
        /// 消息列表
        /// </summary>
        public SortedDictionary<int, string[]> _AllMsgList = new SortedDictionary<int, string[]>();

        /// <summary>
        /// 所有发生过的消息
        /// </summary>
        public static KmMmiMsgHandler<FaultMsgOrdinary> MsgInf;

        /// <summary>
        /// 读取文本信息的编号
        /// </summary>
        private int m_ReadTxtID;

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
            GetValue();
            base.paint(dcGs);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void GetValue()
        {
            currentCycleValue = Convert.ToInt32(FloatList[199]);
            if (currentCycleValue != 0)
            {
                MMI_Main.MMI_Main.ClassBegin = true;
                if (currentCycleValue != lastCycleValue)
                {
                    TheD_Value = currentCycleValue - (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second);
                }
            }
            else
            {
                TheD_Value = 0;
            }
            lastCycleValue = currentCycleValue;
            append_postCmd(CmdType.SetFloatValue, 699, 0, TheD_Value);
        }
        #endregion

        int currentCycleValue;
        int lastCycleValue;
        public static int TheD_Value;
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


        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //1


        //6
        public override bool init(ref int nErrorObjectIndex)
        {
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
            if (BoolList[1])
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
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
                MMI_MsgReceive.MsgInf.ClearAllList();
                MMI_ClassBegin.TheD_Value = 0;
                MMI_MsgReceive.taggerList.Clear();
            }

        }
        #endregion

        private bool _msgClear;

    }

    public static class TheTimeNow
    {
        public static DateTime CurrentTime
        {
            get
            {
                return DateTime.Now.AddSeconds(MMI_ClassBegin.TheD_Value);
            }
        }
    }
}

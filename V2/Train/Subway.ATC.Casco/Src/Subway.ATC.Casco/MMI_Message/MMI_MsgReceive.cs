using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using CommonUtil.Util;
using MMI.Facility.Interface.Attribute;
using MsgReceiveMod;

namespace Subway.ATC.Casco.MMI_Message
{

    [GksDataType(DataType.isMMIObjectClass)]
    public class MMI_MsgReceive : ATCBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "消息接收模块";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            LoadNotifyInfos();

            InitData();
            return true;
        }

        private void LoadNotifyInfos()
        {
            var file = Path.Combine(AppConfig.AppPaths.ConfigDirectory, "消息通知.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] split = cStr.Split(new char[] { '\t' });
                string[] tmp = new string[4];
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
                    }
                }
            }

            LogMgr.Debug(string.Format("read {0} completed, read {1} messages.", file, _AllMsgList.Count));
        }

        #endregion

        #region :::::::::::::::::::::::::: event override   :::::::::::::::::::::::::
        public override void paint(Graphics g)
        {
            RecAndDispMsg();
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
                if (BoolList[100 + i] && _AllMsgList.ContainsKey(100 + i) && msgInf.CurrentMsgList.Find(f => f.MsgLogicID == 100 + i) == null)
                {
                    FaultMsgOrdinary msgTmp = new FaultMsgOrdinary();
                    strTmp = _AllMsgList[100 + i];
                    msgTmp.MsgID = strTmp[1];
                    msgTmp.MsgContent = strTmp[2];
                    msgTmp.TheMsgLevel = (MsgLevels)Enum.ToObject(typeof(MsgLevels), (Convert.ToInt32(strTmp[3])));
                    msgTmp.MsgLogicID = 100 + i;
                    msgTmp.MsgReceiveTime = TheTimeNow.CurrentTime;

                    msgInf.AddNewMsg(msgTmp);

                }
                else if (BoolOldList[100 + i] && !BoolList[100 + i] && _AllMsgList.ContainsKey(100 + i)) //消息结束
                {
                    strTmp = _AllMsgList[100 + i];
                    msgInf.MsgOver(Convert.ToInt32(strTmp[0]));
                    //msgInf.MsgOver(Convert.ToInt32(strTmp[1]), TheTimeNow.CurrentTime);
                }
            }
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

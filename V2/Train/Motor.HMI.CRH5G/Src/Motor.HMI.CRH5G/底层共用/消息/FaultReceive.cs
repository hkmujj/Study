using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH5G.Staus;
using MsgReceiveMod;

namespace Motor.HMI.CRH5G.底层共用.消息
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class FaultReceive : CRH5GBase
    {
        //故障词典
        private readonly Dictionary<int, string[]> m_FaultInfoDict = new Dictionary<int, string[]>();

        public static List<int> HappendErrorList { private set; get; }

        public static CRH5MsgHandler<FaultMsg5> MsgInf { get; private set; }


        //2
        public override string GetInfo()
        {
            return "故障接收模块";
        }

        //6
        public override bool Initalize()
        {
            InitData();

            UIObj.InBoolList.AddRange(m_FaultInfoDict.Keys);

            var appConfig = IConfig.AppConfigs.FirstOrDefault(f => f.AppName == ProjectName);
            Debug.Assert(appConfig != null, "appConfig != null");
            ReadFile(Path.Combine(appConfig.AppPaths.ConfigDirectory, "故障信息.txt"));

            SetFaultWhenDebug();

            return true;
        }

        private void SetFaultWhenDebug()
        {
            foreach (var s in m_FaultInfoDict.Keys.Reverse().Take(33))
            {
                append_postCmd(CmdType.SetInBoolValue, s, 1, 0);
            }
        }

        protected override void ParseLine(int line, string content)
        {
            content = content.Replace("\\n", "\n");
            string[] split = content.Split(new char[] {'\t'});
            string[] tmp = new string[9];
            int i = 0;
            foreach (string s in split)
            {
                if (s.Trim() != "")
                {
                    if (i < 9)
                    {
                        tmp[i] = s;
                    }
                    i++;
                }
                if (i == 9)
                {
                    m_FaultInfoDict.Add(int.Parse(tmp[0]), tmp);
                }
            }
        }

        public override void Paint(Graphics g)
        {
            //tandw Update Only TD Screen Has Fault date:2015/05/28 Start
            if (TitleScreen.ScreenIdentification == ScreenIdentification.ScreenTd)
            {
                GetFault();
            }
            //tandw Update Only TD Screen Has Fault date:2015/05/28 End
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == 1 && nParaB == 100)
            {
                MsgInf.ClearAllList();
            }
        }


        private void GetFault()
        {
            foreach (var key in m_FaultInfoDict.Keys)
            {
                //消息开始
                if (!HappendErrorList.Contains(key) && BoolList[key])
                {
                    HappendErrorList.Add(key);
                    var msgTmp = new FaultMsg5();
                    var strTmp = m_FaultInfoDict[key];
                    msgTmp.MsgLogicID = int.Parse(strTmp[0]);
                    msgTmp.MsgID = strTmp[1];
                    msgTmp.SubSysName = strTmp[2];
                    msgTmp.TheMsgLevel = (MsgLevels) Convert.ToInt32(strTmp[3]);
                    msgTmp.MsgContent = strTmp[4];
                    msgTmp.FaultSolutionStr = strTmp[5];
                    msgTmp.MsgSendLogicID = Convert.ToInt32(strTmp[6]);
                    msgTmp.MsgShowType = (MsgShowType) Convert.ToInt32(strTmp[7]);
                    msgTmp.TrainID = Convert.ToInt32(strTmp[8]);
                    msgTmp.MsgReceiveTime = CurrentTime;

                    MsgInf.AddNewMsg(msgTmp, CurrentTime);
                    append_postCmd(CmdType.ChangePage, 101, 0, 0);
                    if (MsgInf.UnFlagCurrentMsgList.Count > 1)
                    {
                        TitleScreen.BtnStr12[1] = "→";
                    }
                }
                //消息结束
                else if (HappendErrorList.Contains(key) && !BoolList[key])
                {
                    if (MsgInf.UnFlagCurrentMsgList.Find(f => f.MsgLogicID == key) == null)
                    {
                        HappendErrorList.Remove(key);
                        MsgInf.MsgOver(key,CurrentTime);
                    }
                }
            }
        }

        private void InitData()
        {
            HappendErrorList = new List<int>();
            MsgInf = new CRH5MsgHandler<FaultMsg5>(SortCriteriaOfMsg.Level);
        }

    }
}

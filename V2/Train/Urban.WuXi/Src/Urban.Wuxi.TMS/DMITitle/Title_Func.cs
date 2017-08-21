using System;
using System.Drawing;
using MMI.Facility.Interface.Data;
using MsgReceiveMod;

namespace Urban.Wuxi.TMS.DMITitle
{
    public partial class Title
    {
        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics g)
        {
            DrawOn(g);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 1)
            {
                GetValue();
                m_CurrentView = nParaB;
                if (nParaB == 14 || nParaB > 20)
                    m_IsExistHelp = false;
                else
                    m_IsExistHelp = true;

                switch (nParaB)
                {
                    case 11:
                        m_TitleName = "运行";
                        m_IsShowCar = true;
                        m_IsShowDirection = true;
                        break;
                    case 12:
                        m_TitleName = "车辆状态";
                        m_IsShowCar = true;
                        m_IsShowDirection = true;
                        break;
                    case 13:
                        m_TitleName = "空调";
                        m_IsShowCar = true;
                        m_IsShowDirection = false;
                        break;
                    case 14:
                        m_TitleName = "故障";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 15:
                        m_TitleName = "网络通讯";
                        m_IsShowCar = true;
                        m_IsShowDirection = false;
                        break;
                    case 16:
                        m_TitleName = "运行帮助";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 17:
                        m_TitleName = "车辆状态帮助";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 18:
                        m_TitleName = "空调帮助";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 20:
                        m_TitleName = "网络通讯帮助";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 29:
                        m_TitleName = "紧急广播";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 30:
                        m_TitleName = "站点设置";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 31:
                        m_TitleName = "旁路";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 21:
                        m_TitleName = "登录";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 22:
                        m_TitleName = "检修";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 23:
                        m_TitleName = "轮径设置";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 24:
                        m_TitleName = "车号设置";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 25:
                        m_TitleName = "加速度测试";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 26:
                        m_TitleName = "测试";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 27:
                        m_TitleName = "数据记录";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 28:
                        m_TitleName = "USB下载";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 43:
                        m_TitleName = "烟火报警";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    case 45:
                        m_TitleName = "故障提示";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                    default:
                        m_TitleName = "";
                        m_IsShowCar = false;
                        m_IsShowDirection = false;
                        break;
                }
            }
        }

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < m_Regions.Count; ++index)
            {
                if (m_Regions[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    m_ButtonIsDown[0] = true;
                    break;
                case 1:
                    ButtonReset();
                    m_ButtonIsDown[1] = true;
                    break;
                case 2:
                    ButtonReset();
                    m_ButtonIsDown[2] = true;
                    break;
                case 3:
                    ButtonReset();
                    m_ButtonIsDown[3] = true;
                    break;
                case 4:
                    ButtonReset();
                    m_ButtonIsDown[4] = true;
                    break;
                case 5:
                    ButtonReset();
                    m_ButtonIsDown[5] = true;
                    break;
                case 6:
                    if (m_IsExistHelp)
                    {
                        ButtonReset();
                        m_ButtonIsDown[6] = true;
                    }
                    break;
                case 7:
                    ButtonReset();
                    m_ButtonIsDown[7] = true;
                    break;
                case 8: m_ButtonIsDown[8] = true;
                    var tmp = m_MsgInfList.CurrentMsgList.FindAll(f =>!f.TheMsgFlag);
                    if (tmp!=null&&tmp.Count > 0)
                    {
                        OnPost(CmdType.ChangePage, 45, 0, 0);

                    }

                    break;
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < m_Regions.Count; ++index)
            {
                if (m_Regions[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    m_ButtonIsDown[0] = false;
                    OnPost(CmdType.ChangePage, 19, 0, 0);
                    break;
                case 1:
                    OnPost(CmdType.ChangePage, 11, 0, 0);
                    break;
                case 2:
                    OnPost(CmdType.ChangePage, 12, 0, 0);
                    break;
                case 3:
                    OnPost(CmdType.ChangePage, 13, 0, 0);
                    break;
                case 4:
                    OnPost(CmdType.ChangePage, 14, 0, 0);
                    break;
                case 5:
                    OnPost(CmdType.ChangePage, 15, 0, 0);
                    break;
                case 6:
                    if (m_CurrentView == 11)
                        OnPost(CmdType.ChangePage, 16, 0, 0);
                    else if (m_CurrentView == 12)
                        OnPost(CmdType.ChangePage, 17, 0, 0);
                    else if (m_CurrentView == 13)
                        OnPost(CmdType.ChangePage, 18, 0, 0);
                    else if (m_CurrentView == 15)
                        OnPost(CmdType.ChangePage, 20, 0, 0);
                    break;
                case 7:
                    OnPost(CmdType.ChangePage, 21, 0, 0);
                    break;

            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void GetValue()
        {
            //接收bool数据
            for (int i = 0; i < UIObj.InBoolList.Count; i++)
            {
                m_BisValue[i] = BoolList[UIObj.InBoolList[i]];
            }
            //发送bool数据
            for (int i = 0; i < UIObj.OutBoolList.Count; i++)
            {
                m_SetBisValue[i] = BoolList[UIObj.OutBoolList[i]];
                m_SetValueNumb[i] = UIObj.OutBoolList[i];
            }
            //接收float数据
            for (int i = 0; i < UIObj.InFloatList.Count; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }
            //发送float数据
            for (int i = 0; i < UIObj.OutFloatList.Count; i++)
            {
                m_SetFValue[i] = FloatList[UIObj.OutFloatList[i]];
                m_SetFValueNumb[i] = UIObj.OutFloatList[i];
            }

            RecAndDispMsg();

            GkColor();

            if (!BoolList[m_BoolIds[7]])
                OnPost(CmdType.ChangePage, 41, 0, 0);
        }

        /// <summary>
        /// 车站名
        /// </summary>
        /// <param name="stationNum"></param>
        /// <param name="stationName"></param>
        /// <returns></returns>
        private String TrainStation(int stationNum, String stationName)
        {
            m_StationList.TryGetValue(stationNum, out stationName);

            return stationName;

        }

        /// <summary>
        /// 接受和处理消息
        /// </summary>
        private void RecAndDispMsg()
        {
            foreach (var index in AllMsgList.Keys)
            {
                if (BoolList[index] && !m_FaultLogicIDList.Contains(index))
                {
                    var msgTmp = new FaultMsgOrdinary()
                    {
                        MsgLogicID = index,
                        MsgID = AllMsgList[index][1],
                        MsgContent = AllMsgList[index][2],
                        TheMsgLevel = (MsgLevels)Convert.ToInt32(AllMsgList[index][3]),
                        FaultSolutionStr = AllMsgList[index][4]
                    };

                    m_MsgInfList.AddNewMsg(msgTmp);
                    m_FaultLogicIDList.Add(index);
                }
                else if (m_FaultLogicIDList.Contains(index) && !BoolList[index])
                {
                    m_MsgInfList.MsgOver(index, DateTime.Now);
                    m_FaultLogicIDList.Remove(index);
                }
            }
        }

        /// <summary>
        /// 工况区颜色
        /// </summary>
        private void GkColor()
        {
            for (int i = 0; i < 9; i++)
            {
                if (BoolList[m_BoolIds[8] + i])
                {
                    m_RectColor[0] = 1;
                    break;
                }
                else
                    m_RectColor[0] = 0;
            }
            for (int i = 0; i < 11; i++)
            {
                if (BoolList[m_BoolIds[9] + i])
                {
                    m_RectColor[1] = 1;
                    break;
                }
                else
                    m_RectColor[1] = 0;
            }

            for (int i = 0; i < 12; i++)
            {
                if (BoolList[m_BoolIds[10] + i])
                {
                    m_RectColor[2] = 1;
                    break;
                }
                else
                    m_RectColor[2] = 0;
            }

            for (int i = 0; i < 4; i++)
            {
                if (BoolList[m_BoolIds[11] + i])
                {
                    m_RectColor[3] = 1;
                    break;
                }
                else
                    m_RectColor[3] = 0;
            }

            for (int i = 0; i < 12; i++)
            {
                if (BoolList[m_BoolIds[12] + i])
                {
                    m_RectColor[4] = 1;
                    break;
                }
                else
                    m_RectColor[4] = 0;
            }
        }
        #endregion

    }
}

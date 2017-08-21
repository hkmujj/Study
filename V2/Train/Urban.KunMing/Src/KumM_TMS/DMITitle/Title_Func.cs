using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using KumM_TMS.课程开始结束亮度;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.PublicUI;
using MMI.Facility.Interface.Data;
using MsgReceiveMod;

namespace KumM_TMS.DMITitle
{
    public partial class Title : baseClass
    {
        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::

        public override void paint(Graphics dcGs)
        {
            GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 1)
            {
                currentView = nParaB;
                if (nParaB == 14 || nParaB > 20)
                {
                    isExistHelp = false;
                }
                else
                {
                    isExistHelp = true;
                }

                switch (nParaB)
                {
                    case 11:
                        titleName = "运行";
                        isShowCar = true;
                        isShowDirection = true;
                        break;
                    case 12:
                        titleName = "车辆状态";
                        isShowCar = true;
                        isShowDirection = true;
                        break;
                    case 13:
                        titleName = "空调";
                        isShowCar = true;
                        isShowDirection = false;
                        break;
                    case 14:
                        titleName = "故障";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 15:
                        titleName = "网络通讯";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 16:
                        titleName = "运行帮助";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 17:
                        titleName = "车辆状态帮助";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 18:
                        titleName = "空调帮助";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 20:
                        titleName = "网络通讯帮助";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 29:
                        titleName = "紧急广播";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 30:
                        titleName = "站点设置";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 31:
                        titleName = "旁路";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 21:
                        titleName = "登录";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 22:
                        titleName = "检修";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 23:
                        titleName = "轮径设置";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 24:
                        titleName = "车号设置";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 25:
                        titleName = "加速度测试";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 26:
                        titleName = "测试";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 27:
                        titleName = "数据记录";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    case 28:
                        titleName = "USB下载";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                    default:
                        titleName = "";
                        isShowCar = false;
                        isShowDirection = false;
                        break;
                }
            }
        }

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    buttonIsDown[0] = true;
                    break;
                case 1:
                    ButtonReset();
                    buttonIsDown[1] = true;
                    break;
                case 2:
                    ButtonReset();
                    buttonIsDown[2] = true;
                    break;
                case 3:
                    ButtonReset();
                    buttonIsDown[3] = true;
                    break;
                case 4:
                    ButtonReset();
                    buttonIsDown[4] = true;
                    break;
                case 5:
                    ButtonReset();
                    buttonIsDown[5] = true;
                    break;
                case 6:
                    if (isExistHelp)
                    {
                        ButtonReset();
                        buttonIsDown[6] = true;
                    }
                    break;
                case 7:
                    ButtonReset();
                    buttonIsDown[7] = true;
                    break;
                case 8:
                    buttonIsDown[8] = true;
                    break;
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                {
                    break;
                }
            }
            switch (index)
            {
                case 0:
                    buttonIsDown[0] = false;
                    append_postCmd(CmdType.ChangePage, 19, 0, 0);
                    break;
                case 1:
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
                case 2:
                    append_postCmd(CmdType.ChangePage, 12, 0, 0);
                    break;
                case 3:
                    append_postCmd(CmdType.ChangePage, 13, 0, 0);
                    break;
                case 4:
                    append_postCmd(CmdType.ChangePage, 14, 0, 0);
                    break;
                case 5:
                    append_postCmd(CmdType.ChangePage, 15, 0, 0);
                    break;
                case 6:
                    switch (currentView)
                    {
                        case 11:
                            append_postCmd(CmdType.ChangePage, 16, 0, 0);
                            break;
                        case 12:
                            append_postCmd(CmdType.ChangePage, 17, 0, 0);
                            break;
                        case 13:
                            append_postCmd(CmdType.ChangePage, 18, 0, 0);
                            break;
                        case 15:
                            append_postCmd(CmdType.ChangePage, 20, 0, 0);
                            break;
                    }
                    break;
                case 7:
                    append_postCmd(CmdType.ChangePage, 21, 0, 0);
                    break;
                case 8:
                    if (MsgInf.CurrentMsgList.Count != 0)
                    {
                        append_postCmd(CmdType.ChangePage, 14, 0, 0);
                    }
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
                bValue[i] = BoolList[UIObj.InBoolList[i]];
            }
            //发送bool数据
            for (int i = 0; i < UIObj.OutBoolList.Count; i++)
            {
                setBValue[i] = BoolList[UIObj.OutBoolList[i]];
                setBValueNumb[i] = UIObj.OutBoolList[i];
            }
            //接收float数据
            for (int i = 0; i < UIObj.InFloatList.Count; i++)
            {
                fValue[i] = FloatList[UIObj.InFloatList[i]];
            }
            //发送float数据
            for (int i = 0; i < UIObj.OutFloatList.Count; i++)
            {
                setFValue[i] = FloatList[UIObj.OutFloatList[i]];
                setFValueNumb[i] = UIObj.OutFloatList[i];
            }

            D_Value = OutFloatList[299];

            RecAndDispMsg();

            //黑屏
            if (!BoolList[UIObj.InBoolList[6]])
                append_postCmd(CmdType.ChangePage, 40, 0, 0);
        }

        /// <summary>
        /// 车站名
        /// </summary>
        /// <param name="stationNum"></param>
        /// <param name="stationName"></param>
        /// <returns></returns>
        private String TrainStation(int stationNum, String stationName)
        {
            _StationList.TryGetValue(stationNum, out stationName);

            return stationName;

        }

        private string[] strTmp;
        private static int CurrentFaultNumb = 0;

        /// <summary>
        /// 接受和处理消息
        /// </summary>
        private void RecAndDispMsg()
        {
            _receiveNewMsg = false;
            for (int i = 0; i < msgLen; i++)
            {
                //消息开始
                if (BoolList[msgBeginAdress + i] && !HasFault(msgBeginAdress + i) &&
                    _AllMsgList.ContainsKey(msgBeginAdress + i))
                {
                    FaultMsgOrdinary msgTmp = new FaultMsgOrdinary();
                    strTmp = _AllMsgList[msgBeginAdress + i];
                    msgTmp.MsgID = strTmp[1];
                    msgTmp.MsgLogicID = msgBeginAdress + i;
                    msgTmp.MsgContent = strTmp[2];
                    msgTmp.TheMsgLevel = (MsgLevels) Enum.ToObject(typeof (MsgLevels), (Convert.ToInt32(strTmp[3])));
                    msgTmp.FaultSolutionStr = strTmp[4];
                    msgTmp.MsgReceiveTime = this.CurrentTime();

                    _msgInf.AddNewMsg(msgTmp);
                    CurrentFaultNumb++;
                }
                //消息结束
                else if (!BoolList[msgBeginAdress + i] && HasFault(msgBeginAdress + i) &&
                         _AllMsgList.ContainsKey(msgBeginAdress + i))
                {
                    strTmp = _AllMsgList[msgBeginAdress + i];
                    _msgInf.MsgOver(msgBeginAdress + i, this.CurrentTime());
                    CurrentFaultNumb--;
                }

            }

            //当出现BoolList中没有打勾，但是有当前消息时
            if (_msgInf.CurrentMsgList.Count != CurrentFaultNumb)
            {
                foreach (var msgTmp in _msgInf.CurrentMsgList)
                {
                    if (!BoolList[msgTmp.MsgLogicID] && _AllMsgList.ContainsKey(msgTmp.MsgLogicID))
                    {
                        strTmp = _AllMsgList[msgTmp.MsgLogicID];
                        _msgInf.MsgOver(msgTmp.MsgLogicID, this.CurrentTime());
                        CurrentFaultNumb--;
                    }
                }
            }
        }

        private bool HasFault(int logic)
        {
            return _msgInf.CurrentMsgList.Any(msgOrdinary => msgOrdinary.MsgLogicID == logic);
        }

        #endregion

    }
}

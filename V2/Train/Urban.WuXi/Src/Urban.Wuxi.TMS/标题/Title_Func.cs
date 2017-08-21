using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.PublicUI;
using MMI.Facility.Interface.Data;
using MMI_Message;

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
                if (nParaB == 14 || nParaB == 29)
                    isExistHelp = false;
                else
                    isExistHelp = true;

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
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
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
                    if (currentView == 11)
                        append_postCmd(CmdType.ChangePage, 16, 0, 0);
                    else if (currentView == 12)
                        append_postCmd(CmdType.ChangePage, 17, 0, 0);
                    else if (currentView == 13)
                        append_postCmd(CmdType.ChangePage, 18, 0, 0);
                    else if (currentView == 15)
                        append_postCmd(CmdType.ChangePage, 20, 0, 0);
                    break;
                case 7:
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
            for (int i = 0; i < this.UIObj.InBoolList.Count; i++)
            {
                bValue[i] = BoolList[this.UIObj.InBoolList[i]];
            }
            //发送bool数据
            for (int i = 0; i < this.UIObj.OutBoolList.Count; i++)
            {
                setBValue[i] = BoolList[this.UIObj.OutBoolList[i]];
                setBValueNumb[i] = this.UIObj.OutBoolList[i];
            }
            //接收float数据
            for (int i = 0; i < this.UIObj.InFloatList.Count; i++)
            {
                fValue[i] = FloatList[this.UIObj.InFloatList[i]];
            }
            //发送float数据
            for (int i = 0; i < this.UIObj.OutFloatList.Count; i++)
            {
                setFValue[i] = FloatList[this.UIObj.OutFloatList[i]];
                setFValueNumb[i] = this.UIObj.OutFloatList[i];
            }

            RecAndDispMsg();
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

        string[] strTmp;
        /// <summary>
        /// 接受和处理消息
        /// </summary>
        private void RecAndDispMsg()
        {
            _receiveNewMsg = false;
            for (int i = 0; i < msgLen; i++)
            {
                //消息开始
                if (BoolList[msgBeginAdress + i] && !BoolOldList[msgBeginAdress + i] && _AllMsgList.ContainsKey(msgBeginAdress + i))
                {
                    MessageEnf msgTmp = new MessageEnf();
                    strTmp = _AllMsgList[msgBeginAdress + i];
                    msgTmp.MsgID = Convert.ToInt32(strTmp[1]);
                    msgTmp.MsgName = strTmp[2];
                    msgTmp.MesPriority = Convert.ToInt32(strTmp[3]);
                    msgTmp.MsgSolution = strTmp[4];
                    if (!_msgInf.ListIsOpering)
                        _msgInf.AddSorted(msgTmp);
                }
            }
            for (int i = 0; i < msgLen; i++)
            {
                //消息结束
                if (BoolOldList[msgBeginAdress + i] && !BoolList[msgBeginAdress + i] && _AllMsgList.ContainsKey(msgBeginAdress + i))
                {
                    strTmp = _AllMsgList[msgBeginAdress + i];
                    if (!_msgInf.ListIsOpering)
                        _msgInf.MsgHasOver(Convert.ToInt32(strTmp[1]));
                }
            }
        }
        #endregion

    }
}

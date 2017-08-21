using System;
using System.Drawing;
using MMI.Facility.Interface.Data;
using Subway.ATC.Casco.Common;
using Subway.ATC.Casco.MMI_Message;

namespace Subway.ATC.Casco.MMI_Main
{
    // ReSharper disable once InconsistentNaming
    public partial class MMI_Main : ATCBaseClass
    {
        int count = 0;
        bool liveAdd = true;

        float[] targetDistance = { 0, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000 };
        int timeCount = 0;

        private string m_DefaultServiceID;

        public override void paint(Graphics dcGs)
        {
            GetValue();
            DrawOn(dcGs);
            Screensaver.Draw(dcGs);
        }

        private Image GetSpeedPoionterImage(SpeedPointerState state)
        {
            switch (state)
            {
                case SpeedPointerState.Normal:
                    return Img[0];
                case SpeedPointerState.Warning:
                    return Img[1];
                case SpeedPointerState.Emergent:
                    return Img[2];
                default:
                    return Img[0];
            }
        }

        public override bool mouseDown(Point nPoint)
        {
            LastMouseDownTime = DateTime.Now;
            int index = 0;
            if (m_BtnViewId == 0) index = 0;
            else if (m_BtnViewId == 1) index = 19;
            else if (m_BtnViewId == 2 || m_BtnViewId == 3) index = 26;
            else if (m_BtnViewId == 4) index = 6;
            //确认
            if (BValue[43])
            {
                index = 30;
            }
            for (; index < Rect.Count; index++)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            #region ::::::::: 主界面 :::::::::::::
            if (m_BtnViewId == 0)
            {
                switch (index)
                {
                    case 0:         //向上
                        if (m_BtnCanDown[0])
                        {
                            ButtonIsDown[0] = true;
                        }
                        break;
                    case 1:         //向下
                        if (m_BtnCanDown[1])
                        {
                            ButtonIsDown[1] = true;
                        }
                        break;
                    case 2:         //声音
                        if (m_BtnCanDown[2])
                        {
                            ButtonIsDown[2] = true;
                        }
                        break;
                    case 3:         //亮度
                        if (m_BtnCanDown[3])
                        {
                            ButtonIsDown[3] = true;
                        }
                        break;
                    case 4:         //菜单
                        if (m_BtnCanDown[4])
                        {
                            ButtonIsDown[4] = true;
                        }
                        break;
                    case 5:
                        if (m_BtnCanDown[5])
                        {
                            ButtonIsDown[5] = true;
                        }
                        break;
                    case 30:
                        ButtonIsDown[30] = true;
                        break;
                    case 31:
                        ButtonIsDown[31] = true;
                        break;
                }
            }
            #endregion

            #region :::::::: 菜单界面 ::::::::::::
            //菜单
            if (m_BtnViewId == 1)
            {
                switch (index)
                {
                    case 19://语言
                        ButtonIsDown[19] = true;
                        break;
                    case 20://测试
                        ButtonIsDown[20] = true;
                        break;
                    case 21://状态
                        ButtonIsDown[21] = true;
                        break;
                    case 22://重启
                        ButtonIsDown[22] = true;
                        break;
                    case 25://关闭
                        ButtonIsDown[25] = true;
                        break;
                }
            }
            #endregion

            #region :::::::: 声音界面 ::::::::::::
            if (m_BtnViewId == 2)
            {
                switch (index)
                {
                    case 26:
                        ButtonIsDown[26] = true;
                        break;
                    case 27:
                        ButtonIsDown[27] = true;
                        break;
                    case 28:
                        ButtonIsDown[28] = true;
                        break;
                }
            }
            #endregion

            #region :::::::: 亮度界面 ::::::::::::
            if (m_BtnViewId == 3)
            {
                switch (index)
                {
                    case 26:
                        ButtonIsDown[26] = true;
                        break;
                    case 27:
                        ButtonIsDown[27] = true;
                        break;
                    case 28:
                        ButtonIsDown[28] = true;
                        break;
                    case 29:
                        ButtonIsDown[29] = true;
                        break;
                }
            }
            #endregion

            #region :::::::: 工号界面 ::::::::::::
            if (m_BtnViewId == 4)
            {
                switch (index)
                {
                    case 6:
                        ButtonIsDown[6] = true;
                        break;
                    case 7:
                        ButtonIsDown[7] = true;
                        break;
                    case 8:
                        ButtonIsDown[8] = true;
                        break;
                    case 9:
                        ButtonIsDown[9] = true;
                        break;
                    case 10:
                        ButtonIsDown[10] = true;
                        break;
                    case 11:
                        ButtonIsDown[11] = true;
                        break;
                    case 12:
                        ButtonIsDown[12] = true;
                        break;
                    case 13:
                        ButtonIsDown[13] = true;
                        break;
                    case 14:
                        ButtonIsDown[14] = true;
                        break;
                    case 15:
                        ButtonIsDown[15] = true;
                        break;
                    case 16:
                        ButtonIsDown[16] = true;
                        break;
                    case 17:
                        ButtonIsDown[17] = true;
                        break;
                    case 18:
                        ButtonIsDown[18] = true;
                        break;
                }
            }
            #endregion
            return true;
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            if (m_BtnViewId == 0) index = 0;
            else if (m_BtnViewId == 1) index = 19;
            else if (m_BtnViewId == 2 || m_BtnViewId == 3) index = 26;
            else if (m_BtnViewId == 4) index = 6;
            //确认
            if (BValue[43])
            {
                index = 30;
            }
            for (; index < Rect.Count; index++)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            #region ::::::::: 主界面 :::::::::::::
            if (m_BtnViewId == 0)
            {
                switch (index)
                {
                    case 0:         //向上
                        if (m_BtnCanDown[0])
                        {
                            ButtonIsDown[0] = false;
                            if (m_RowId > 0) m_RowId--;
                            if (m_RowId == 0 && m_TimeIn) m_ShowMsgSign = true;
                        }
                        break;
                    case 1:         //向下
                        if (m_BtnCanDown[1])
                        {
                            ButtonIsDown[1] = false;
                            m_RowId++;
                            m_ShowMsgSign = false;
                        }
                        break;
                    case 2:         //声音
                        if (m_BtnCanDown[2])
                        {
                            ButtonIsDown[2] = false;
                            m_BtnViewId = 2;
                        }
                        break;
                    case 3:         //亮度
                        if (m_BtnCanDown[3])
                        {
                            ButtonIsDown[3] = false;
                            m_BtnViewId = 3;
                        }
                        break;
                    case 4:         //菜单
                        if (m_BtnCanDown[4])
                        {
                            ButtonIsDown[4] = false;
                            m_BtnViewId = 1;
                        }
                        break;
                    case 5:         //工号
                        if (m_BtnCanDown[5])
                        {
                            ButtonIsDown[5] = false;
                            m_BtnViewId = 4;
                        }
                        break;
                    case 30:
                        ButtonIsDown[30] = false;
                        SetBoolValue(SetBValueNumb[0], 2);
                        append_postCmd(CmdType.SetBoolValue, SetBValueNumb[0], 1, 0);
                        break;
                    case 31:
                        ButtonIsDown[31] = false;
                        SetBoolValue(SetBValueNumb[0], 2);
                        append_postCmd(CmdType.SetBoolValue, SetBValueNumb[1], 1, 0);
                        break;
                }
            }
            #endregion

            #region :::::::: 菜单界面 ::::::::::::
            //菜单
            if (m_BtnViewId == 1)
            {
                switch (index)
                {
                    case 19://语言
                        ButtonIsDown[19] = false;
                        break;
                    case 20://测试
                        ButtonIsDown[20] = false;
                        break;
                    case 21://状态
                        ButtonIsDown[21] = false;
                        break;
                    case 22://重启
                        ButtonIsDown[22] = false;
                        break;
                    case 25://关闭
                        ButtonIsDown[25] = false;
                        m_BtnViewId = 0;
                        break;
                }
            }
            #endregion

            #region :::::::: 声音界面 ::::::::::::
            if (m_BtnViewId == 2)
            {
                switch (index)
                {
                    case 26:
                        ButtonIsDown[26] = false;
                        if (m_SoundProgress > 0) m_SoundProgress--;
                        break;
                    case 27:
                        ButtonIsDown[27] = false;
                        if (m_SoundProgress < 5) m_SoundProgress++;
                        break;
                    case 28:
                        ButtonIsDown[28] = false;
                        m_BtnViewId = 0;
                        break;
                }
            }
            #endregion

            #region :::::::: 亮度界面 ::::::::::::
            if (m_BtnViewId == 3)
            {
                switch (index)
                {
                    case 26:
                        ButtonIsDown[26] = false;
                        if (_brightProgress > 0) _brightProgress--;
                        break;
                    case 27:
                        ButtonIsDown[27] = false;
                        if (_brightProgress < 5) _brightProgress++;
                        break;
                    case 28:
                        ButtonIsDown[28] = false;
                        m_BtnViewId = 0;
                        break;
                    case 29:
                        ButtonIsDown[29] = false;
                        _brightProgress = 5;
                        break;
                }
            }
            #endregion

            #region :::::::: 工号界面 ::::::::::::
            if (m_BtnViewId == 4)
            {
                switch (index)
                {
                    case 6:
                        ButtonIsDown[6] = false;
                        CrowNumbSet("delete");
                        break;
                    case 7:
                        ButtonIsDown[7] = false;
                        CrowNumbSet("1");
                        break;
                    case 8:
                        ButtonIsDown[8] = false;
                        CrowNumbSet("2");
                        break;
                    case 9:
                        ButtonIsDown[9] = false;
                        CrowNumbSet("3");
                        break;
                    case 10:
                        ButtonIsDown[10] = false;
                        CrowNumbSet("4");
                        break;
                    case 11:
                        ButtonIsDown[11] = false;
                        CrowNumbSet("5");
                        break;
                    case 12:
                        ButtonIsDown[12] = false;
                        CrowNumbSet("6");
                        break;
                    case 13:
                        ButtonIsDown[13] = false;
                        CrowNumbSet("7");
                        break;
                    case 14:
                        ButtonIsDown[14] = false;
                        CrowNumbSet("8");
                        break;
                    case 15:
                        ButtonIsDown[15] = false;
                        CrowNumbSet("9");
                        break;
                    case 16:
                        ButtonIsDown[16] = false;
                        CrowNumbSet("close");
                        break;
                    case 17:
                        ButtonIsDown[17] = false;
                        CrowNumbSet("0");
                        break;
                    case 18:
                        ButtonIsDown[18] = false;
                        CrowNumbSet("OK");
                        break;
                }
            }
            #endregion

            return true;
        }

        /// <summary>
        /// 循环数据
        /// </summary>
        private void GetValue()
        {
            //接收bool数据
            for (int i = 0; i < UIObj.InBoolList.Count; i++)
            {
                BValue[i] = BoolList[UIObj.InBoolList[i]];
                OldbValue[i] = BoolOldList[UIObj.InBoolList[i]];
            }
            //发送bool数据
            for (int i = 0; i < UIObj.OutBoolList.Count; i++)
            {
                SetBValue[i] = OutBoolList[UIObj.OutBoolList[i]];
                SetBValueNumb[i] = UIObj.OutBoolList[i];
            }
            //接收float数据
            for (int i = 0; i < UIObj.InFloatList.Count; i++)
            {
                FValue[i] = FloatList[UIObj.InFloatList[i]];
                OldfValue[i] = FloatOldList[UIObj.InFloatList[i]];
            }
            //发送float数据
            for (int i = 0; i < UIObj.OutFloatList.Count; i++)
            {
                SetFValue[i] = OutFloatList[UIObj.OutFloatList[i]];
                SetFValueNumb[i] = UIObj.OutFloatList[i];
            }

            //收到新消息
            if (MMI_MsgReceive.ReceiveNewMsg)
            {
                m_TimeIn = true;
                m_ShowMsgSign = true;
                timeCount = 0;
                m_RowId = 0;
            }
            //是否在计时
            m_TimeUp = TimeIsUp();

            m_BtnCanDown[4] = FValue[0] == 0 ? true : false;

            if (BValue[43])
                m_BtnViewId = 0;
            else
                SetBoolValue(SetBValueNumb[0], 2);

            if (!BoolList[1])
                append_postCmd(CmdType.ChangePage, 3, 0, 0);
            ElectricKeyOff = BoolList[UIObj.InBoolList[56]];

        }

        private void ChangedScreensaverStatus(bool value)
        {
            if (value)
            {
                LastMouseDownTime = DateTime.Now;
                m_ScreensaverTimer.Change(0, 1000);
            }
            else
            {
                Screensaver.IsScreensaver = false;
                m_ScreensaverTimer.Change(int.MaxValue, int.MaxValue);
            }
        }
        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            //DrawFrame(e);
            DrawAreaA(e);
            DrawAreaB(e);
            DrawAreaC(e);
            DrawAreaD(e);
            DrawAreaE(e);
            DrawAreaF(e);
            DrawAreaG(e);
            DrawAreaK(e);
            DrawAreaM(e);
            DrawAreaN(e);
            DrawAreaT(e);

            switch (m_BtnViewId)
            {
                case 1:
                    DrawMenu(e);
                    break;
                case 2:
                    DrawSound(e);
                    break;
                case 3:
                    DrawBrightness(e);
                    break;
                case 4:
                    DrawCrewNumb(e);
                    break;
            }

            //确定按钮
            if (BValue[43])
            {
                DrawACK(e);
            }
            //e.DrawString(MMI_Message.MessageEnf.ObjCreatNumb.ToString(),
            //    FormatStyle.Font18, FormatStyle.WhiteSolidBrush,
            //    new Point(600, 400));
        }

        /// <summary>
        /// 车站名
        /// </summary>
        /// <param name="stationNum"></param>
        /// <param name="stationName"></param>
        /// <returns></returns>
        private string TrainStation(int stationNum, string stationName)
        {
            StationList.TryGetValue(stationNum, out stationName);

            if (stationName == null) stationName = "- - -";

            return stationName;
        }

        /// <summary>
        /// 生命显示条
        /// </summary>
        private void LifeIndicator()
        {
            if (LiftIndic == 7)
            {
                liveAdd = false;
            }
            else if (LiftIndic == 1)
            {
                liveAdd = true;
            }
            if (liveAdd) LiftIndic++;
            else LiftIndic--;
            //if (count > 5)
            //    liftIndic++;
            //if (liftIndic > 8)
            //    liftIndic = 1;
        }

        /// <summary>
        /// 消息指示计时
        /// </summary>
        /// <returns></returns>
        private bool TimeIn()
        {
            timeCount++;
            if (timeCount > 40)
            {
                timeCount = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 当为true时消息指示器消失
        /// </summary>
        /// <returns></returns>
        private bool TimeIsUp()
        {
            if (m_TimeIn && TimeIn())
            {
                m_TimeIn = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 发送bool清零
        /// </summary>
        /// <param name="setBoolId"></param>
        /// <param name="length"></param>
        private void SetBoolValue(int setBoolId, int length)
        {
            for (int i = 0; i < length; i++)
            {
                append_postCmd(CmdType.SetBoolValue, setBoolId + i, 0, 0);
            }
        }

        /// <summary>
        /// 工号设置
        /// </summary>
        /// <param name="numb"></param>
        private void CrowNumbSet(string numb)
        {
            switch (numb)
            {
                case "delete":
                    if (CrowNumb.Length > 0) CrowNumb = string.Empty;
                    break;
                case "1":
                    if (CrowNumb.Length < 3) CrowNumb += numb;
                    break;
                case "2":
                    if (CrowNumb.Length < 3) CrowNumb += numb;
                    break;
                case "3":
                    if (CrowNumb.Length < 3) CrowNumb += numb;
                    break;
                case "4":
                    if (CrowNumb.Length < 3) CrowNumb += numb;
                    break;
                case "5":
                    if (CrowNumb.Length < 3) CrowNumb += numb;
                    break;
                case "6":
                    if (CrowNumb.Length < 3) CrowNumb += numb;
                    break;
                case "7":
                    if (CrowNumb.Length < 3) CrowNumb += numb;
                    break;
                case "8":
                    if (CrowNumb.Length < 3) CrowNumb += numb;
                    break;
                case "9":
                    if (CrowNumb.Length < 3) CrowNumb += numb;
                    break;
                case "close":
                    if (CrowNumb.Length > 0) CrowNumb = string.Empty;
                    m_BtnViewId = 0;
                    break;
                case "0":
                    if (CrowNumb.Length < 3) CrowNumb += numb;
                    break;
                case "OK":
                    if (CrowNumb != string.Empty)
                        append_postCmd(CmdType.SetFloatValue, SetFValueNumb[0], 0, Convert.ToInt32(CrowNumb));
                    if (CrowNumb.Length > 0) CrowNumb = string.Empty;
                    m_BtnViewId = 0;
                    break;
            }
        }

        /// <summary>
        /// 目标点距离
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private float TargetDistance(float dis)
        {
            float distance = 0;
            for (int i = 0; i < 11; i++)
            {
                if (dis > targetDistance[10])
                {
                    distance = 242f;
                }
                else if (dis > targetDistance[i] && dis < targetDistance[i + 1])
                {
                    distance = 24.2f * i + 24.2f / (targetDistance[i + 1] - targetDistance[i]) * (dis - targetDistance[i]);
                }
                else if (dis == targetDistance[i])
                {
                    distance = 24.2f * i;
                }
            }
            return distance;
        }

        /// <summary>
        /// 发车时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string Drived(float time)
        {
            return time == 0.0f ? "" :
                ((int)time / 3600).ToString("00") + " : " +
                (((int)time % 3600) / 60).ToString("00") + " : " +
                (((int)time % 3600) % 60).ToString("00");
        }
    }
}

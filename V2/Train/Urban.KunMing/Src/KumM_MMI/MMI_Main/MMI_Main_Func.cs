using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.PublicUI;
using KumM_MMI.MMI_Message;

namespace KumM_MMI
{
    public partial class MMI_Main : baseClass
    {
        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics dcGs)
        {
            GetValue();
            //if (FloatList[199] != 0)
            //{
            //    ClassBegin = true;
            //    TimeCounter.ClassBegin();
            //    append_postCmd(CmdType.SetFloatValue, 299, 0, float.Parse(TimeCounter.TheTimeBeginToNow.ToString()));
            //    TheTimeNow.GetTheTimeOfClassBegin(Convert.ToInt32(FloatList[199]));
            //}
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            if (btnViewId == 0) index = 0;
            else if (btnViewId == 1) index = 19;
            else if (btnViewId == 2 || btnViewId == 3) index = 26;
            else if (btnViewId == 4) index = 6;
            //确认
            if (bValue[43])
            {
                index = 30;
            }
            for (; index < Rect.Count; index++)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            #region ::::::::: 主界面 :::::::::::::
            if (btnViewId == 0)
            {
                switch (index)
                {
                    case 0:         //向上
                        if (btnCanDown[0])
                        {
                            buttonIsDown[0] = true;
                        }
                        break;
                    case 1:         //向下
                        if (btnCanDown[1])
                        {
                            buttonIsDown[1] = true;
                        }
                        break;
                    case 2:         //声音
                        if (btnCanDown[2])
                        {
                            buttonIsDown[2] = true;
                        }
                        break;
                    case 3:         //亮度
                        if (btnCanDown[3])
                        {
                            buttonIsDown[3] = true;
                        }
                        break;
                    case 4:         //菜单
                        if (btnCanDown[4])
                        {
                            buttonIsDown[4] = true;
                        }
                        break;
                    case 5:
                        if (btnCanDown[5])
                        {
                            buttonIsDown[5] = true;
                        }
                        break;
                    case 30:
                        buttonIsDown[30] = true;
                        break;
                    case 31:
                        buttonIsDown[31] = true;
                        break;
                }
            }
            #endregion

            #region :::::::: 菜单界面 ::::::::::::
            //菜单
            if (btnViewId == 1)
            {
                switch (index)
                {
                    case 19://语言
                        buttonIsDown[19] = true;
                        break;
                    case 20://测试
                        buttonIsDown[20] = true;
                        break;
                    case 21://状态
                        buttonIsDown[21] = true;
                        break;
                    case 22://重启
                        buttonIsDown[22] = true;
                        break;
                    case 25://关闭
                        buttonIsDown[25] = true;
                        break;
                }
            }
            #endregion

            #region :::::::: 声音界面 ::::::::::::
            if (btnViewId == 2)
            {
                switch (index)
                {
                    case 26:
                        buttonIsDown[26] = true;
                        break;
                    case 27:
                        buttonIsDown[27] = true;
                        break;
                    case 28:
                        buttonIsDown[28] = true;
                        break;
                }
            }
            #endregion

            #region :::::::: 亮度界面 ::::::::::::
            if (btnViewId == 3)
            {
                switch (index)
                {
                    case 26:
                        buttonIsDown[26] = true;
                        break;
                    case 27:
                        buttonIsDown[27] = true;
                        break;
                    case 28:
                        buttonIsDown[28] = true;
                        break;
                    case 29:
                        buttonIsDown[29] = true;
                        break;
                }
            }
            #endregion

            #region :::::::: 工号界面 ::::::::::::
            if (btnViewId == 4)
            {
                switch (index)
                {
                    case 6:
                        buttonIsDown[6] = true;
                        break;
                    case 7:
                        buttonIsDown[7] = true;
                        break;
                    case 8:
                        buttonIsDown[8] = true;
                        break;
                    case 9:
                        buttonIsDown[9] = true;
                        break;
                    case 10:
                        buttonIsDown[10] = true;
                        break;
                    case 11:
                        buttonIsDown[11] = true;
                        break;
                    case 12:
                        buttonIsDown[12] = true;
                        break;
                    case 13:
                        buttonIsDown[13] = true;
                        break;
                    case 14:
                        buttonIsDown[14] = true;
                        break;
                    case 15:
                        buttonIsDown[15] = true;
                        break;
                    case 16:
                        buttonIsDown[16] = true;
                        break;
                    case 17:
                        buttonIsDown[17] = true;
                        break;
                    case 18:
                        buttonIsDown[18] = true;
                        break;
                }
            }
            #endregion
            return true;
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            if (btnViewId == 0) index = 0;
            else if (btnViewId == 1) index = 19;
            else if (btnViewId == 2 || btnViewId == 3) index = 26;
            else if (btnViewId == 4) index = 6;
            //确认
            if (bValue[43])
            {
                index = 30;
            }
            for (; index < Rect.Count; index++)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            #region ::::::::: 主界面 :::::::::::::
            if (btnViewId == 0)
            {
                switch (index)
                {
                    case 0:         //向上
                        if (btnCanDown[0])
                        {
                            buttonIsDown[0] = false;
                            if (RowId > 0) RowId--;
                            if (RowId == 0 && timeIn) showMsgSign = true;
                        }
                        break;
                    case 1:         //向下
                        if (btnCanDown[1])
                        {
                            buttonIsDown[1] = false;
                            RowId++;
                            showMsgSign = false;
                        }
                        break;
                    case 2:         //声音
                        if (btnCanDown[2])
                        {
                            buttonIsDown[2] = false;
                            btnViewId = 2;
                        }
                        break;
                    case 3:         //亮度
                        if (btnCanDown[3])
                        {
                            buttonIsDown[3] = false;
                            btnViewId = 3;
                        }
                        break;
                    case 4:         //菜单
                        if (btnCanDown[4])
                        {
                            buttonIsDown[4] = false;
                            btnViewId = 1;
                        }
                        break;
                    case 5:         //工号
                        if (btnCanDown[5])
                        {
                            buttonIsDown[5] = false;
                            btnViewId = 4;
                        }
                        break;
                    case 30:
                        buttonIsDown[30] = false;
                        SetBoolValue(setBValueNumb[0], 2);
                        append_postCmd(CmdType.SetBoolValue, setBValueNumb[0], 1, 0);
                        break;
                    case 31:
                        buttonIsDown[31] = false;
                        SetBoolValue(setBValueNumb[0], 2);
                        append_postCmd(CmdType.SetBoolValue, setBValueNumb[1], 1, 0);
                        break;
                }
            }
            #endregion

            #region :::::::: 菜单界面 ::::::::::::
            //菜单
            if (btnViewId == 1)
            {
                switch (index)
                {
                    case 19://语言
                        buttonIsDown[19] = false;
                        break;
                    case 20://测试
                        buttonIsDown[20] = false;
                        break;
                    case 21://状态
                        buttonIsDown[21] = false;
                        break;
                    case 22://重启
                        buttonIsDown[22] = false;
                        break;
                    case 25://关闭
                        buttonIsDown[25] = false;
                        btnViewId = 0;
                        break;
                }
            }
            #endregion

            #region :::::::: 声音界面 ::::::::::::
            if (btnViewId == 2)
            {
                switch (index)
                {
                    case 26:
                        buttonIsDown[26] = false;
                        if (soundProgress > 0) soundProgress--;
                        break;
                    case 27:
                        buttonIsDown[27] = false;
                        if (soundProgress < 5) soundProgress++;
                        break;
                    case 28:
                        buttonIsDown[28] = false;
                        btnViewId = 0;
                        break;
                }
            }
            #endregion

            #region :::::::: 亮度界面 ::::::::::::
            if (btnViewId == 3)
            {
                switch (index)
                {
                    case 26:
                        buttonIsDown[26] = false;
                        if (_brightProgress > 0) _brightProgress--;
                        break;
                    case 27:
                        buttonIsDown[27] = false;
                        if (_brightProgress < 5) _brightProgress++;
                        break;
                    case 28:
                        buttonIsDown[28] = false;
                        btnViewId = 0;
                        break;
                    case 29:
                        buttonIsDown[29] = false;
                        _brightProgress = 5;
                        break;
                }
            }
            #endregion

            #region :::::::: 工号界面 ::::::::::::
            if (btnViewId == 4)
            {
                switch (index)
                {
                    case 6:
                        buttonIsDown[6] = false;
                        CrowNumbSet("delete");
                        break;
                    case 7:
                        buttonIsDown[7] = false;
                        CrowNumbSet("1");
                        break;
                    case 8:
                        buttonIsDown[8] = false;
                        CrowNumbSet("2");
                        break;
                    case 9:
                        buttonIsDown[9] = false;
                        CrowNumbSet("3");
                        break;
                    case 10:
                        buttonIsDown[10] = false;
                        CrowNumbSet("4");
                        break;
                    case 11:
                        buttonIsDown[11] = false;
                        CrowNumbSet("5");
                        break;
                    case 12:
                        buttonIsDown[12] = false;
                        CrowNumbSet("6");
                        break;
                    case 13:
                        buttonIsDown[13] = false;
                        CrowNumbSet("7");
                        break;
                    case 14:
                        buttonIsDown[14] = false;
                        CrowNumbSet("8");
                        break;
                    case 15:
                        buttonIsDown[15] = false;
                        CrowNumbSet("9");
                        break;
                    case 16:
                        buttonIsDown[16] = false;
                        CrowNumbSet("close");
                        break;
                    case 17:
                        buttonIsDown[17] = false;
                        CrowNumbSet("0");
                        break;
                    case 18:
                        buttonIsDown[18] = false;
                        CrowNumbSet("OK");
                        break;
                }
            }
            #endregion

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        /// <summary>
        /// 循环数据
        /// </summary>
        private void GetValue()
        {
            //接收bool数据
            for (int i = 0; i < this.UIObj.InBoolList.Count; i++)
            {
                bValue[i] = BoolList[this.UIObj.InBoolList[i]];
                oldbValue[i] = BoolOldList[this.UIObj.InBoolList[i]];
            }
            //发送bool数据
            for (int i = 0; i < this.UIObj.OutBoolList.Count; i++)
            {
                setBValue[i] = OutBoolList[this.UIObj.OutBoolList[i]];
                setBValueNumb[i] = this.UIObj.OutBoolList[i];
            }
            //接收float数据
            for (int i = 0; i < this.UIObj.InFloatList.Count; i++)
            {
                fValue[i] = FloatList[this.UIObj.InFloatList[i]];
                oldfValue[i] = FloatOldList[this.UIObj.InFloatList[i]];
            }
            //发送float数据
            for (int i = 0; i < this.UIObj.OutFloatList.Count; i++)
            {
                setFValue[i] = OutFloatList[this.UIObj.OutFloatList[i]];
                setFValueNumb[i] = this.UIObj.OutFloatList[i];
            }

            //收到新消息
            if (MMI_MsgReceive.ReceiveNewMsg)
            {
                timeIn = true;
                showMsgSign = true;
                timeCount = 0;
                RowId = 0;
            }
            //是否在计时
            timeUp = TimeIsUp();

            btnCanDown[4] = fValue[0] == 0 ? true : false;

            if (bValue[43])
                btnViewId = 0;
            else
                SetBoolValue(setBValueNumb[0], 2);

            if (!BoolList[UIObj.InBoolList[52]] && !BoolList[UIObj.InBoolList[51]])
                append_postCmd(CmdType.ChangePage, 0, 0, 0);
        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            //DrawFrame(e);

          
            if (BoolList[UIObj.InBoolList[51]])
            {
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
                switch (btnViewId)
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
                if (bValue[43])
                {
                    DrawACK(e);
                }
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
        private String TrainStation(int stationNum, String stationName)
        {
            _StationList.TryGetValue(stationNum, out stationName);

            if (stationName == null) stationName = "- - -";

            return stationName;
        }

        int count = 0;
        bool liveAdd = true;
        /// <summary>
        /// 生命显示条
        /// </summary>
        private void LifeIndicator()
        {
            if (liftIndic == 7)
            {
                liveAdd = false;
            }
            else if (liftIndic == 1)
            {
                liveAdd = true;
            }
            if (liveAdd) liftIndic++;
            else liftIndic--;
            //if (count > 5)
            //    liftIndic++;
            //if (liftIndic > 8)
            //    liftIndic = 1;
        }

        int timeCount = 0;
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
            if (timeIn && TimeIn())
            {
                timeIn = false;
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
        private void CrowNumbSet(String numb)
        {
            switch (numb)
            {
                case "delete":
                    if (crowNumb.Length > 0) crowNumb = string.Empty;
                    break;
                case "1":
                    if (crowNumb.Length < 3) crowNumb += numb;
                    break;
                case "2":
                    if (crowNumb.Length < 3) crowNumb += numb;
                    break;
                case "3":
                    if (crowNumb.Length < 3) crowNumb += numb;
                    break;
                case "4":
                    if (crowNumb.Length < 3) crowNumb += numb;
                    break;
                case "5":
                    if (crowNumb.Length < 3) crowNumb += numb;
                    break;
                case "6":
                    if (crowNumb.Length < 3) crowNumb += numb;
                    break;
                case "7":
                    if (crowNumb.Length < 3) crowNumb += numb;
                    break;
                case "8":
                    if (crowNumb.Length < 3) crowNumb += numb;
                    break;
                case "9":
                    if (crowNumb.Length < 3) crowNumb += numb;
                    break;
                case "close":
                    if (crowNumb.Length > 0) crowNumb = string.Empty;
                    btnViewId = 0;
                    break;
                case "0":
                    if (crowNumb.Length < 3) crowNumb += numb;
                    break;
                case "OK":
                    if (crowNumb != string.Empty)
                        append_postCmd(CmdType.SetFloatValue, setFValueNumb[0], 0, Convert.ToInt32(crowNumb));
                    if (crowNumb.Length > 0) crowNumb = string.Empty;
                    btnViewId = 0;
                    break;
            }
        }

        float[] targetDistance = { 0, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000 };
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
        #endregion

    }
}

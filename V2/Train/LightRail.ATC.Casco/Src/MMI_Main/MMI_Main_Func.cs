using System;
using System.Collections;
using System.Drawing;
using LightRail.ATC.Casco.MMI_Message;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;

namespace LightRail.ATC.Casco.MMI_Main
{
    public partial class MMI_Main : baseClass
    {
        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics dcGs)
        {
            GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            switch (m_BtnViewId)
            {
                case 0:
                    index = 0;
                    break;
                case 1:
                    index = 18;
                    break;
                case 2:
                    index = 4;
                    break;
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
                            buttonIsDown[0] = true;
                        }
                        break;
                    case 1:         //向下
                        if (m_BtnCanDown[1])
                        {
                            buttonIsDown[1] = true;
                        }
                        break;
                    case 2:         //工号
                        if (m_BtnCanDown[2])
                        {
                            buttonIsDown[2] = true;
                        }
                        break;
                    case 3:         //Menu
                        if (m_BtnCanDown[3])
                        {
                            buttonIsDown[3] = true;
                        }
                        break;
                    case 24:
                        buttonIsDown[24] = true;
                        break;
                    case 25:
                        buttonIsDown[25] = true;
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
                    case 18://LanguageSelect
                        buttonIsDown[18] = true;
                        break;
                    case 19://DeviceAdjust
                        buttonIsDown[19] = true;
                        break;
                    case 20://AlarmTest
                        buttonIsDown[20] = true;
                        break;
                    case 21://CleanScreen
                        buttonIsDown[21] = true;
                        break;
                    case 22://DMIStatus
                        buttonIsDown[22] = true;
                        break;
                    case 23://Reboot
                        buttonIsDown[23] = true;
                        break;
                    case 24://Close
                        buttonIsDown[24] = true;
                        break;
                }
            }
            #endregion

            #region :::::::: 工号界面 ::::::::::::
            if (m_BtnViewId == 2)
            {
                switch (index)
                {
                    case 4:
                        buttonIsDown[4] = true;
                        break;
                    case 5:
                        buttonIsDown[5] = true;
                        break;
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
                }
            }
            #endregion
            return true;
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            switch (m_BtnViewId)
            {
                case 0:
                    index = 0;
                    break;
                case 1:
                    index = 18;
                    break;
                case 2:
                    index = 4;
                    break;
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
                            buttonIsDown[0] = false;
                            if (m_RowId > 0) m_RowId--;
                            if (m_RowId == 0 && m_TimeIn) m_ShowMsgSign = true;
                        }
                        break;
                    case 1:         //向下
                        if (m_BtnCanDown[1])
                        {
                            buttonIsDown[1] = false;
                            m_RowId++;
                            m_ShowMsgSign = false;
                        }
                        break;
                    case 2:         //Menu
                        if (m_BtnCanDown[2])
                        {
                            buttonIsDown[2] = false;
                            m_BtnViewId = 1;
                        }
                        break;
                    case 3:         //工号
                        if (m_BtnCanDown[3])
                        {
                            buttonIsDown[3] = false;
                            m_BtnViewId = 2;
                        }
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
                    case 18://LanguageSelect
                        buttonIsDown[18] = false;
                        break;
                    case 19://DeviceAdjust
                        buttonIsDown[19] = false;
                        break;
                    case 20://AlarmTest
                        buttonIsDown[20] = false;
                        break;
                    case 21://CleanScreen
                        buttonIsDown[21] = false;
                        break;
                    case 22://DMIStatus
                        buttonIsDown[22] = false;
                        break;
                    case 23://Reboot
                        buttonIsDown[23] = false;
                        break;
                    case 24://Close
                        buttonIsDown[24] = false;
                        m_BtnViewId = 0;
                        break;
                }
            }
            #endregion

            #region :::::::: 工号界面 ::::::::::::
            if (m_BtnViewId == 2)
            {
                switch (index)
                {
                    case 4:
                        buttonIsDown[4] = false;
                        CrowNumbSet("ok");
                        break;
                    case 5:
                        buttonIsDown[5] = false;
                        CrowNumbSet("close");
                        break;
                    case 6:
                        buttonIsDown[6] = false;
                        CrowNumbSet("1");
                        break;
                    case 7:
                        buttonIsDown[7] = false;
                        CrowNumbSet("2");
                        break;
                    case 8:
                        buttonIsDown[8] = false;
                        CrowNumbSet("3");
                        break;
                    case 9:
                        buttonIsDown[9] = false;
                        CrowNumbSet("4");
                        break;
                    case 10:
                        buttonIsDown[10] = false;
                        CrowNumbSet("5");
                        break;
                    case 11:
                        buttonIsDown[11] = false;
                        CrowNumbSet("6");
                        break;
                    case 12:
                        buttonIsDown[12] = false;
                        CrowNumbSet("7");
                        break;
                    case 13:
                        buttonIsDown[13] = false;
                        CrowNumbSet("8");
                        break;
                    case 14:
                        buttonIsDown[14] = false;
                        CrowNumbSet("9");
                        break;
                    case 15:
                        buttonIsDown[15] = false;
                        CrowNumbSet("C");
                        break;
                    case 16:
                        buttonIsDown[16] = false;
                        CrowNumbSet("0");
                        break;
                    case 17:
                        buttonIsDown[17] = false;
                        CrowNumbSet("back");
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
                //setFValue[i] = FloatList[this.UIObj.OutFloatList[i]];
                setFValueNumb[i] = this.UIObj.OutFloatList[i];
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
            timeUp = TimeIsUp();

            m_BtnCanDown[2] = fValue[0] == 0 ? true : false;

            if (bValue[43])
                m_BtnViewId = 0;
            else
                SetBoolValue(setBValueNumb[0], 2);

            if (!BoolList[1])
                append_postCmd(CmdType.ChangePage, 3, 0, 0);
        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            

            DrawA(e);
            DrawB(e);
            DrawC(e);
            DrawE(e);
            DrawD(e);
            DrawG(e);
            DrawM(e);
            DrawT(e);
            switch (m_BtnViewId)
            {
                case 1:
                    DrawMenu(e);
                    break;
                case 2:
                    DrawCrewNumb(e);
                    break;
            }
            DrawAtc(e);
        }

        /// <summary>
        /// 车站名
        /// </summary>
        /// <param name="stationNum"></param>
        /// <param name="stationName"></param>
        /// <returns></returns>
        private String TrainStation(int stationNum, String stationName)
        {
            StationList.TryGetValue(stationNum, out stationName);

            if (stationName == null) stationName = "- - -";

            return stationName;
        }

        bool m_LiveAdd = true;
        /// <summary>
        /// 生命显示条
        /// </summary>
        private void LifeIndicator()
        {
            switch (liftIndic)
            {
                case 7:
                    m_LiveAdd = false;
                    break;
                case 1:
                    m_LiveAdd = true;
                    break;
            }
            if (m_LiveAdd) liftIndic++;
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
        private void CrowNumbSet(String numb)
        {
            switch (numb)
            {
                case "C":
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
                    m_BtnViewId = 0;
                    break;
                case "back":
                    if (crowNumb.Length > 0)
                    {
                        crowNumb = crowNumb.Substring(0, crowNumb.Length - 1);
                    }
                    break;
                case "0":
                    if (crowNumb.Length < 3) crowNumb += numb;
                    break;
                case "ok":
                    if (crowNumb != string.Empty)
                        append_postCmd(CmdType.SetFloatValue, setFValueNumb[0], 0, Convert.ToInt32(crowNumb));
                    if (crowNumb.Length > 0) crowNumb = string.Empty;
                    m_BtnViewId = 0;
                    break;
            }
        }

        readonly float[] m_TargetDistance = { 1, 2, 5, 10, 20, 50, 100, 200, 500, 750 };
        readonly Hashtable m_HtTarget = new Hashtable { { 1000f, 244 }, { 500f, 228 }, { 200f, 195 }, { 100f, 171 }, { 50f, 145 }, { 20f, 112 }, { 10f, 87 }, { 5f, 63 }, { 2f, 30 }, { 1f, 5 } };

        /// <summary>
        /// 目标点距离
        /// </summary>
        /// <param name="dis"></param>
        /// <returns></returns>
        private float TargetDistance(float dis)
        {
            var interval = 5;
            float distance = 0;
            if (m_HtTarget.ContainsKey(dis))
            {
                distance = float.Parse(m_HtTarget[dis].ToString());
            }
            else
            {
                if (dis >= 1000)
                {
                    distance = float.Parse(m_HtTarget[1000].ToString());
                }
                else if (1000 > dis && dis > 500)
                {
                    distance = (dis - 500) / 250 * 16 + 228;
                }
                else if (500 > dis && dis > 200)
                {
                    distance = (dis - 200) / 300 * 33 + 195;
                }
                else if (200 > dis && dis > 100)
                {
                    distance = (dis - 100) / 100 * 24 + 171;
                }
                else if (100 > dis && dis > 50)
                {
                    distance = (dis - 50) / 50 * 26 + 145;
                }
                else if (50 > dis && dis > 20)
                {
                    distance = (dis - 20) / 30 * 33 + 112;
                }
                else if (20 > dis && dis > 10)
                {
                    distance = (dis - 10) / 10 * 25 + 87;
                }
                else if (10 > dis && dis > 5)
                {
                    distance = (dis - 5) / 5 * 24 + 63;
                }
                else if (5 > dis && dis > 2)
                {
                    distance = (dis - 2) / 3 * 33 + 30;

                }
                else if (2 > dis && dis > 1)
                {
                    distance = (dis - 1) / 1 * 25 + 5;
                }
                else
                {
                    distance = 0;
                }
            }

            return distance == 0 ? 0 : distance - interval;
        }
        #endregion
        /// <summary>
        /// 设置表盘以及字体的颜色
        /// </summary>
        /// <param name="color">需要设置的颜色</param>
        private void SetColor(Color color)
        {
            m_DialPlate.ContentColor = color;
        }
        /// <summary>
        /// 根据颜色设置指针颜色图片
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>返回图片</returns>
        private Image SetPointer(Color color)
        {
            if (color == Color.Red)
            {
                return Img[24];
            }
            return color == Color.FromArgb(234, 145, 0) ? Img[25] : Img[23];
        }
    }
}

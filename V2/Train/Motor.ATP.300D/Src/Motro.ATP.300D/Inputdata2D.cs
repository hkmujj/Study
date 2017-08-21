using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._300D.Common;

namespace Motor.ATP._300D
{
    [GksDataType(DataType.isMMIObjectClass)]
    class Inputdata2D : ATPBase, IButtonResponser
    {
        int FC_X = FrameButton2D.FrameChange_X;
        int FC_Y = FrameButton2D.FrameChange_Y;

        private String[] Strtmp = new String[9];
        private readonly StringFormat m_DrawFormat = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
        private readonly RectTextInfo[] m_PropertyLocations = new RectTextInfo[9];
        private int keyflag = 0;
        private int flagmenu = 0;
        private FloatStringInterpreter m_FloatStringInterpreter;

        /// <summary>
        /// 制动百分比
        /// </summary>
        public static int BrakePercent { private set; get; }

        /// <summary>
        /// 列车长度
        /// </summary>
        public static int TrainLength { private set; get; }

        /// <summary>
        /// 最高速度 
        /// </summary>
        public static int MaxSpeed { private set; get; }

        static Inputdata2D()
        {
            BrakePercent = 100;
            TrainLength = 402;
            MaxSpeed = 300;
        }

        public override bool Initalize()
        {
            ButtonVeiw.Instance.AddResponser(this);
            m_FloatStringInterpreter = new FloatStringInterpreter();
            if (UIObj.ParaList.Count >= 0)
            {
                InitDate();
            }
            else
            {
                return false;
            }

            return true;
        }

        public override void paint(Graphics g)
        {
            RefreshData();
            ResponseDown(Background2D.PressedButtonIndex);
            OnDraw(g);
        }

        private void RefreshData()
        {
            var trainLine = new float[2] { FloatList[UIObj.InFloatList[2]], FloatList[UIObj.InFloatList[3]] };
            var trainLineChar = m_FloatStringInterpreter.InterpreterFloatArray(trainLine);
            trainLineChar.CopyTo(DMIMainMenu2D.TrainNumber, 0);
        }

        public void OnMouseDown(ButtonType buttonType)
        {
            var btnIndex = buttonType;

            if (flagmenu == 0)
            {
                if (OnButtonDownOfInput(btnIndex))
                {
                    return;
                }
            }
            if (flagmenu == 1)
            {
                OnButtonDownOfConfirm(btnIndex);
            }

            if (flagmenu == 2)
            {
                OnButtonDownOfAfterConfirmInput(btnIndex);
            }

            if (flagmenu == 3)
            {
                OnButtonDownOfSelectPage(btnIndex);
            }
        }

        private void OnButtonDownOfSelectPage(ButtonType btnIndex)
        {
            switch (btnIndex)
            {
                case ButtonType.F1:
                    append_postCmd(CmdType.ChangePage, 101, 0, 0);
                    break;
                case ButtonType.F2:
                    append_postCmd(CmdType.ChangePage, 102, 0, 0);
                    break;
                case ButtonType.F3:
                    append_postCmd(CmdType.ChangePage, 120, 0, 0);
                    break;
                case ButtonType.F6:
                    append_postCmd(CmdType.ChangePage, 106, 0, 0);
                    break;
                case ButtonType.F8:
                    append_postCmd(CmdType.ChangePage, 100, 0, 0);
                    break;
            }
        }

        private void OnButtonDownOfAfterConfirmInput(ButtonType btnIndex)
        {
            if (btnIndex == ButtonType.ATPConfirm)
            {
                DMIMainMenu2D.ControlButtonContentCollection[0] = "司机号";
                DMIMainMenu2D.ControlButtonContentCollection[1] = "车次号";
                DMIMainMenu2D.ControlButtonContentCollection[2] = "列车\n数据";
                DMIMainMenu2D.ControlButtonContentCollection[3] = "";
                DMIMainMenu2D.ControlButtonContentCollection[4] = "";
                DMIMainMenu2D.ControlButtonContentCollection[5] = "列车数\n据浏览";
                DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                DMIMainMenu2D.ControlButtonContentCollection[7] = "主显示\n器";
                DMIMainMenu2D.FlashState = -1;


                DMIMainMenu2D.ClearInputButtonContents();

                DMIMainMenu2D.Popuptxt = " ";
                flagmenu = 3;
                BrakePercent = int.Parse(Strtmp[0]) * 100 + int.Parse(Strtmp[1]) * 10 + int.Parse(Strtmp[2]);
                TrainLength = int.Parse(Strtmp[3]) * 100 + int.Parse(Strtmp[4]) * 10 + int.Parse(Strtmp[5]);
                MaxSpeed = int.Parse(Strtmp[6]) * 100 + int.Parse(Strtmp[7]) * 10 + int.Parse(Strtmp[8]);

                Background2D.bfirstshuju = true;

                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, 20);

                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[1], 0, MaxSpeed);
                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[2], 0, TrainLength);
                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[3], 0, BrakePercent);
            }
        }

        private bool OnButtonDownOfInput(ButtonType btnIndex)
        {
            switch (btnIndex)
            {
                case ButtonType.F2:
                    if (keyflag > 0)
                    {
                        keyflag--;
                    }
                    else
                    {
                        keyflag = 8;
                    }
                    break;
                case ButtonType.F3:
                    if (keyflag < 8)
                    {
                        keyflag++;
                    }
                    else
                    {
                        keyflag = 0;
                    }
                    break;
                case ButtonType.F4:
                    if (keyflag >= 3)
                    {
                        keyflag = keyflag - 3;
                    }
                    else
                    {
                        keyflag = keyflag + 6;
                    }
                    break;
                case ButtonType.F5:
                    if (keyflag <= 5)
                    {
                        keyflag = keyflag + 3;
                    }
                    else
                    {
                        keyflag = keyflag - 6;
                    }
                    break;
                case ButtonType.F6: //正确
                    flagmenu = 1;
                    DMIMainMenu2D.ControlButtonContentCollection[0] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[1] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[2] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[3] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[4] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[5] = "数据确认";
                    DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[7] = "取消";
                    DMIMainMenu2D.FlashState = 5;

                    DMIMainMenu2D.ClearInputButtonContents();

                    return true;

                case ButtonType.F8: //取消
                    append_postCmd(CmdType.ChangePage, 100, 0, 0);
                    break;
            }

            if (btnIndex >= 0 && btnIndex <= ButtonType.B0)
            {
                Strtmp[keyflag] = ((int)btnIndex + 1).ToString();
                if (btnIndex == ButtonType.B0)
                {
                    Strtmp[keyflag] = "0";
                }

                if (keyflag < 8)
                {
                    keyflag++;
                }
                else
                {
                    keyflag = 0;
                }
            }

            for (int j = 0; j < 8; j++)
            {
                m_PropertyLocations[j].Setstr(Strtmp[j]);
            }
            return false;
        }

        private void OnButtonDownOfConfirm(ButtonType btnIndex)
        {
            switch (btnIndex)
            {
                case ButtonType.F6: //确认
                    flagmenu = 2;
                    DMIMainMenu2D.ControlButtonContentCollection[0] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[1] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[2] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[3] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[4] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[5] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[7] = "";
                    DMIMainMenu2D.FlashState = -1;


                    DMIMainMenu2D.ClearInputButtonContents();

                    DMIMainMenu2D.Popuptxt = "请按压确认按钮";
                    break;
                case ButtonType.F8: //取消
                    flagmenu = 0;
                    DMIMainMenu2D.ControlButtonContentCollection[0] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[1] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[2] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[3] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[4] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[5] = "确认";
                    DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[7] = "取消";
                    DMIMainMenu2D.FlashState = 5;


                    DMIMainMenu2D.InputButtonContentCollection[0] = "1";
                    DMIMainMenu2D.InputButtonContentCollection[1] = "2";
                    DMIMainMenu2D.InputButtonContentCollection[2] = "3";
                    DMIMainMenu2D.InputButtonContentCollection[3] = "4";
                    DMIMainMenu2D.InputButtonContentCollection[4] = "5";
                    DMIMainMenu2D.InputButtonContentCollection[5] = "6";
                    DMIMainMenu2D.InputButtonContentCollection[6] = "7";
                    DMIMainMenu2D.InputButtonContentCollection[7] = "8";
                    DMIMainMenu2D.InputButtonContentCollection[8] = "9";
                    DMIMainMenu2D.InputButtonContentCollection[9] = "0";
                    break;
            }
        }

        public override string GetInfo()
        {
            return "输入列车数据";
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {

                switch ((ViewType)nParaB)
                {
                    case ViewType.InputTrainData://输入列车数据
                        InitDate();
                        break;
                }
            }
        }
        public void InitDate()
        {
            DMIMainMenu2D.ControlButtonContentCollection[0] = "";
            DMIMainMenu2D.ControlButtonContentCollection[1] = "";
            DMIMainMenu2D.ControlButtonContentCollection[2] = "";
            DMIMainMenu2D.ControlButtonContentCollection[3] = "";
            DMIMainMenu2D.ControlButtonContentCollection[4] = "";
            DMIMainMenu2D.ControlButtonContentCollection[5] = "确认";
            DMIMainMenu2D.ControlButtonContentCollection[6] = "";
            DMIMainMenu2D.ControlButtonContentCollection[7] = "取消";
            DMIMainMenu2D.FlashState = 5;


            DMIMainMenu2D.InputButtonContentCollection[0] = "1";
            DMIMainMenu2D.InputButtonContentCollection[1] = "2";
            DMIMainMenu2D.InputButtonContentCollection[2] = "3";
            DMIMainMenu2D.InputButtonContentCollection[3] = "4";
            DMIMainMenu2D.InputButtonContentCollection[4] = "5";
            DMIMainMenu2D.InputButtonContentCollection[5] = "6";
            DMIMainMenu2D.InputButtonContentCollection[6] = "7";
            DMIMainMenu2D.InputButtonContentCollection[7] = "8";
            DMIMainMenu2D.InputButtonContentCollection[8] = "9";
            DMIMainMenu2D.InputButtonContentCollection[9] = "0";

            Strtmp[0] = "1";
            Strtmp[1] = "0";
            Strtmp[2] = "0";
            Strtmp[3] = "4";
            Strtmp[4] = "0";
            Strtmp[5] = "2";
            Strtmp[6] = "3";
            Strtmp[7] = "0";
            Strtmp[8] = "0";

            for (int i = 0; i < 9; i++)
            {
                m_PropertyLocations[i] = new RectTextInfo();
                m_PropertyLocations[i].SetTextColor(0, 0, 0, FormatStyle.StrFont, 30, 1);
                m_PropertyLocations[i].SetBKColor(255, 255, 255);
                m_PropertyLocations[i].SetRect(296 + 27 * (i % 3) + FC_X, 36 + ((int)(i / 3)) * 47 + FC_Y, 25, 45);
                m_PropertyLocations[i].Setstr(Strtmp[i]);
            }
            keyflag = 0;
            flagmenu = 0;
        }

        public void OnDraw(Graphics g)
        {

            DrawTitles(g, GetTrainId());

            if (flagmenu < 3)
            {
                DrawInputData(g);
            }

            if (flagmenu == 0)
            {
                DrawControlButtonContent(g);
            }

            if (flagmenu == 1 || flagmenu == 2 || flagmenu == 3)
            {
                DrawPropertyValues(g);
            }
        }

        private void DrawPropertyValues(Graphics g)
        {
            g.DrawString(Strtmp[0] + Strtmp[1] + Strtmp[2], FormatStyle.Font30y, FormatStyle.YellowBrush, new RectangleF(426 + FC_X, 36 + FC_Y, 150, 45), m_DrawFormat);
            g.DrawString("0" + Strtmp[3] + Strtmp[4] + Strtmp[5], FormatStyle.Font30y, FormatStyle.YellowBrush, new RectangleF(426 + FC_X, 36 + 47 + FC_Y, 150, 45), m_DrawFormat);
            g.DrawString(Strtmp[6] + Strtmp[7] + Strtmp[8], FormatStyle.Font30y, FormatStyle.YellowBrush, new RectangleF(426 + FC_X, 36 + 47 * 2 + FC_Y, 150, 45), m_DrawFormat);

            g.DrawString("0 0", FormatStyle.Font30y, FormatStyle.YellowBrush, new RectangleF(426 + FC_X, 36 + 47 * 3 + FC_Y, 150, 45), m_DrawFormat);
            g.DrawString("9R/P", FormatStyle.Font30y, FormatStyle.YellowBrush, new RectangleF(426 + FC_X, 36 + 47 * 4 + FC_Y, 150, 45), m_DrawFormat);
            g.DrawString("1 7.0", FormatStyle.Font30y, FormatStyle.YellowBrush, new RectangleF(426 + FC_X, 36 + 47 * 5 + FC_Y, 150, 45), m_DrawFormat);
            g.DrawString("0 0", FormatStyle.Font30y, FormatStyle.YellowBrush, new RectangleF(426 + FC_X, 36 + 47 * 6 + FC_Y, 150, 45), m_DrawFormat);
            g.DrawString("0", FormatStyle.Font30y, FormatStyle.YellowBrush, new RectangleF(426 + FC_X, 36 + 47 * 7 + FC_Y, 150, 45), m_DrawFormat);
        }

        private string GetTrainId()
        {
            return new string(DMIMainMenu2D.TrainNumber);
        }

        private void DrawTitles(Graphics g, string id)
        {
            g.DrawString("列车  " + id, FormatStyle.Font12, FormatStyle.WhiteBrush, 13 + FC_X, 9 + FC_Y);

            g.DrawString("输入列车数据", FormatStyle.Font12, FormatStyle.WhiteBrush, 166 + FC_X, 9 + FC_Y);
            g.DrawString(Background2D.StrtimeY, FormatStyle.Font12, FormatStyle.WhiteBrush, 564 + FC_X, 9 + FC_Y);
            g.DrawString(Background2D.StrtimeH, FormatStyle.Font12, FormatStyle.WhiteBrush, 646 + FC_X, 9 + FC_Y);

            g.DrawString("制动百分比：", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(0 + FC_X, 44 + FC_Y, 288, 28), m_DrawFormat);
            g.DrawString("列车长度：", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(0 + FC_X, 44 + 47 + FC_Y, 288, 28), m_DrawFormat);
            g.DrawString("最高速度：", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(0 + FC_X, 44 + 47 * 2 + FC_Y, 288, 28), m_DrawFormat);
            g.DrawString("列车类型：", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(0 + FC_X, 44 + 47 * 3 + FC_Y, 288, 28), m_DrawFormat);
            g.DrawString("制动类型：", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(0 + FC_X, 44 + 47 * 4 + FC_Y, 288, 28), m_DrawFormat);
            g.DrawString("轴重：", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(0 + FC_X, 44 + 47 * 5 + FC_Y, 288, 28), m_DrawFormat);
            g.DrawString("限界：", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(0 + FC_X, 44 + 47 * 6 + FC_Y, 288, 28), m_DrawFormat);
            g.DrawString("气密性：", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(0 + FC_X, 44 + 47 * 7 + FC_Y, 288, 28), m_DrawFormat);
        }

        private void DrawControlButtonContent(Graphics g)
        {
            g.DrawImage(Images[0], 750 + FC_X, 100 + FC_Y, Images[0].Width, Images[0].Height);
            g.DrawImage(Images[1], 750 + FC_X, 175 + FC_Y, Images[1].Width, Images[1].Height);
            g.DrawImage(Images[2], 750 + FC_X, 175 + 75 + FC_Y, Images[2].Width, Images[2].Height);
            g.DrawImage(Images[3], 750 + FC_X, 175 + 150 + FC_Y, Images[3].Width, Images[3].Height);
        }

        private void DrawInputData(Graphics g)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i == keyflag && flagmenu == 0)
                {
                    if (Background2D.TimeNeedFlash)
                    {
                        g.DrawString(Strtmp[i], FormatStyle.Font30, FormatStyle.WhiteBrush, 296 + 27 * (i % 3) + FC_X, 36 + ((int)(i / 3)) * 47 + FC_Y);
                    }
                    else
                    {
                        m_PropertyLocations[i].OnDraw(g);
                    }
                    continue;
                }
                m_PropertyLocations[i].OnDraw(g);
            }
            g.DrawString("0 0", FormatStyle.Font30, FormatStyle.WhiteBrush, new RectangleF(265 + FC_X, 36 + 47 * 3 + FC_Y, 117, 45), m_DrawFormat);
            g.DrawString("9R/P", FormatStyle.Font30, FormatStyle.WhiteBrush, new RectangleF(265 + FC_X, 36 + 47 * 4 + FC_Y, 117, 45), m_DrawFormat);
            g.DrawString("1 7.0", FormatStyle.Font30, FormatStyle.WhiteBrush, new RectangleF(265 + FC_X, 36 + 47 * 5 + FC_Y, 117, 45), m_DrawFormat);
            g.DrawString("0 0", FormatStyle.Font30, FormatStyle.WhiteBrush, new RectangleF(265 + FC_X, 36 + 47 * 6 + FC_Y, 117, 45), m_DrawFormat);
            g.DrawString("0", FormatStyle.Font30, FormatStyle.WhiteBrush, new RectangleF(265 + FC_X, 36 + 47 * 7 + FC_Y, 117, 45), m_DrawFormat);
        }

        public bool ResponseDown(ButtonType btnType)
        {
            if (!UIObj.ViewList.Contains(CurrentViewIdex))
            {
                return false;
            }
            OnMouseDown(btnType);
            return true;
        }

        public bool ResponseUp(ButtonType btnType)
        {
            return false;
        }
    }
}
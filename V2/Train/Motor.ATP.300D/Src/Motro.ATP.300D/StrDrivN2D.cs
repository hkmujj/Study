using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._300D.Common;

namespace Motor.ATP._300D
{
    //司机号
    [GksDataType(DataType.isMMIObjectClass)]
    class StrDrivN2D : ATPBase, IButtonResponser
    {
        int FC_X = FrameButton2D.FrameChange_X;
        int FC_Y = FrameButton2D.FrameChange_Y;

        private readonly String[] m_Strtmp = new String[8];
        private readonly StringFormat m_DrawFormat = new StringFormat();
        private readonly RectTextInfo[] m_Rect = new RectTextInfo[8];
        private int m_CurrentInputLocation = 0;
        private bool m_Inputfirst = true;

        private const int MaxInputCount = 8;
        private readonly char[] m_StrTainN1 = new char[MaxInputCount];
        private readonly char[] m_StrDrivN1 = new char[MaxInputCount];
        private ViewType m_CurrentViewIndex = 0;

        private UserActionDataInterpreter m_ActionDataInterpreter;

        private FloatStringInterpreter m_FloatStringInterpreter;

        public override bool Initalize()
        {
            m_ActionDataInterpreter = new UserActionDataInterpreter();
            m_FloatStringInterpreter = new FloatStringInterpreter();
            ButtonVeiw.Instance.AddResponser(this);
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
            ResponseDown(Background2D.PressedButtonIndex);

            OnDraw(g);
        }



        private void OnMouseDown(ButtonType btnIndex)
        {

            InitalizeFirstInputEnv(btnIndex);

            OnControlButtonDown(btnIndex);

            OnInputButtonDown(btnIndex);

            RefreshView();
        }

        private void RefreshView()
        {
            if (!m_Inputfirst)
            {
                for (var i = 0; i < 8; i++)
                {
                    m_Rect[i].Setstr(m_Strtmp[i]);
                }
            }
        }

        private void OnInputButtonDown(ButtonType btnIndex)
        {
            if (btnIndex < ButtonType.B1 && btnIndex > ButtonType.BSwitch || btnIndex == ButtonType.None)
            {
                return;
            }

            var data = m_ActionDataInterpreter.Interpreter(btnIndex);

            UpdateCurrentSelected(btnIndex);

            if (data != char.MinValue)
            {
                switch (m_CurrentViewIndex)
                {
                    case ViewType.DriverID:
                        m_StrDrivN1[m_CurrentInputLocation] = data;
                        break;
                    case ViewType.TrainNumber:
                        m_StrTainN1[m_CurrentInputLocation] = data;
                        break;
                }
                m_Strtmp[m_CurrentInputLocation] = data.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void UpdateCurrentSelected(ButtonType btnIndex)
        {
            if (m_ActionDataInterpreter.InputEnded)
            {
                m_ActionDataInterpreter.KnowInputEnded();

                if (m_ActionDataInterpreter.LastInterpreterValue.IsNumber()
                    || (m_ActionDataInterpreter.LastInterpreterValue.IsCharacter()
                    && m_ActionDataInterpreter.LastType != btnIndex
                    && m_ActionDataInterpreter.LastType != ButtonType.BSwitch))
                {
                    if (m_CurrentInputLocation < 7)
                    {
                        ++m_CurrentInputLocation;
                    }
                }
            }

            if (m_CurrentInputLocation == -1)
            {
                m_CurrentInputLocation = 0;
            }
        }

        private void OnControlButtonDown(ButtonType btnIndex)
        {
            switch (btnIndex)
            {
                case ButtonType.F2:
                    if (m_CurrentInputLocation > 0)
                    {
                        m_CurrentInputLocation--;
                    }
                    break;
                case ButtonType.F3:
                    if (m_CurrentInputLocation >= 1 && m_CurrentInputLocation < MaxInputCount - 1)
                    {
                        m_CurrentInputLocation++;
                    }
                    break;
                case ButtonType.F6: //正确
                    m_ActionDataInterpreter.Clear();
                    switch (m_CurrentViewIndex)
                    {
                        case ViewType.DriverID:
                            Background2D.bfirstsijihao = true;
                            append_postCmd(CmdType.ChangePage, 104, 0, 0);
                            break;
                        case ViewType.TrainNumber:
                            Background2D.bfirstcheci = true;
                            int k = 0;
                            for (var j = MaxInputCount - 1; j >= 0; j--, k++)
                            {
                                if (m_Strtmp[j] == "")
                                {
                                    continue;
                                }
                                if (m_Strtmp[j] != "*" && m_Strtmp[j] != " " && m_Strtmp[j] != "")
                                {
                                    DMIMainMenu2D.TrainNumber[7 - k] = m_Strtmp[j][0];
                                }
                                else
                                {
                                    DMIMainMenu2D.TrainNumber[7 - k] = Char.MinValue;
                                }
                                //append_postCmd(CmdType.SetFloatValue, 207 - k, DMIMainMenu2D.TrainNumber[7 - k], DMIMainMenu2D.TrainNumber[7 - k]);
                            }

                            var ca = new char[8];
                            m_Strtmp.Where(w => !string.IsNullOrEmpty(w) && w != "*" && w != " ").Select(s => s[0]).ToArray().CopyTo(ca, 0);
                            var rlt = m_FloatStringInterpreter.InterpreterCharArray(ca);
                            append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[2], 0, rlt[0]);
                            append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[3], 0, rlt[1]);

                            append_postCmd(CmdType.ChangePage, 120, 0, 0);
                            break;
                    }
                    break;
                case ButtonType.F8: //取消
                    m_ActionDataInterpreter.Clear();
                    if (m_CurrentViewIndex == ViewType.DriverID && Background2D.bfirstsijihao)
                    {
                        append_postCmd(CmdType.ChangePage, 100, 0, 0);
                    }
                    else if (m_CurrentViewIndex == ViewType.TrainNumber && Background2D.bfirstcheci)
                    {
                        append_postCmd(CmdType.ChangePage, 100, 0, 0);
                    }
                    break;
            }
        }

        private void InitalizeFirstInputEnv(ButtonType btnIndex)
        {
            if (btnIndex >= 0 && btnIndex <= ButtonType.B0 && m_Inputfirst)
            {
                m_Inputfirst = false;
                for (int k = 0; k < 8; k++)
                {
                    m_Strtmp[k] = "";
                }
            }
        }

        public override string GetInfo()
        {
            return "司机号车次号";
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                switch ((ViewType)nParaB)
                {
                    case ViewType.DriverID://司机号
                        m_CurrentViewIndex = ViewType.DriverID;
                        InitDate();

                        break;
                    case ViewType.TrainNumber://车次号
                        m_CurrentViewIndex = ViewType.TrainNumber;
                        InitDate();
                        break;
                }
            }
        }
        public void InitDate()
        {
            RefreshButtonContent();

            for (int i = 0; i < 8; i++)
            {
                m_Rect[i] = new RectTextInfo();
                m_Rect[i].SetTextColor(0, 0, 0, FormatStyle.StrFont, 30, 1, (int)StringAlignment.Near);
                m_Rect[i].SetBKColor(255, 255, 255);
                m_Rect[i].SetRect(458 + 28 * i + FC_X, 210 + FC_Y, 25, 45);
                m_Rect[i].Setstr("*");
            }
            for (int k = 0; k < 8; k++)
            {
                m_Strtmp[k] = "*";
                m_StrTainN1[k] = char.MinValue;
                m_StrDrivN1[k] = char.MinValue;
            }
            m_DrawFormat.Alignment = StringAlignment.Center;
            m_DrawFormat.LineAlignment = StringAlignment.Center;
            m_CurrentInputLocation = -1;
            m_Inputfirst = true;
        }

        private void RefreshButtonContent()
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

            switch (m_CurrentViewIndex)
            {
                case ViewType.DriverID:
                    DMIMainMenu2D.InputButtonContentCollection[0] = "1\n.";
                    DMIMainMenu2D.InputButtonContentCollection[1] = "2\nabc";
                    DMIMainMenu2D.InputButtonContentCollection[2] = "3\ndef";
                    DMIMainMenu2D.InputButtonContentCollection[3] = "4\nghi";
                    DMIMainMenu2D.InputButtonContentCollection[4] = "5\njkl";
                    DMIMainMenu2D.InputButtonContentCollection[5] = "6\nmno";
                    DMIMainMenu2D.InputButtonContentCollection[6] = "7\npqrs";
                    DMIMainMenu2D.InputButtonContentCollection[7] = "8\ntuv";
                    DMIMainMenu2D.InputButtonContentCollection[8] = "9\nwxyz";
                    DMIMainMenu2D.InputButtonContentCollection[9] = "0";
                    break;
                case ViewType.TrainNumber:
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

        public void OnDraw(Graphics g)
        {
            DrawInputTitle(g);

            DrawInput(g);

            DrawControlButtonContent(g);
        }

        private void DrawInputTitle(Graphics g)
        {
            switch (m_CurrentViewIndex)
            {
                case ViewType.DriverID:
                    g.DrawString("输入司机号", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(419 + FC_X, 4 + FC_Y, 302, 28), m_DrawFormat);
                    g.DrawString("司机号:", FormatStyle.Font16, FormatStyle.WhiteBrush, 458 + FC_X, 167 + FC_Y);
                    break;
                case ViewType.TrainNumber:
                    g.DrawString("输入车次号", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(419 + FC_X, 4 + FC_Y, 302, 28), m_DrawFormat);
                    g.DrawString("车次号:", FormatStyle.Font16, FormatStyle.WhiteBrush, 458 + FC_X, 167 + FC_Y);
                    break;
            }
        }

        private void DrawInput(Graphics g)
        {
            for (int i = 0; i < 8; i++)
            {
                if (i == m_CurrentInputLocation)
                {
                    if (Background2D.TimeNeedFlash)
                    {
                        g.DrawString(m_Strtmp[i], FormatStyle.Font30, FormatStyle.WhiteBrush, 458 + 28 * i + FC_X, 210 + FC_Y);
                    }
                    else
                    {
                        m_Rect[i].OnDraw(g);
                    }
                    continue;
                }
                m_Rect[i].OnDraw(g);
            }
        }

        private void DrawControlButtonContent(Graphics g)
        {
            if (m_CurrentInputLocation > 0)
            {
                g.DrawImage(Images[0], 750 + FC_X, 100 + FC_Y, Images[0].Width, Images[0].Height);
                g.DrawImage(Images[1], 750 + FC_X, 175 + FC_Y, Images[1].Width, Images[1].Height);
            }
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
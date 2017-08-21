using System;
using System.Drawing;
using Engine.TCMS.HXD3D.底层共用;

namespace Engine.TCMS.HXD3D.HXD3D
{
    public class Keyboard
    {
        // Fields
        private readonly Image[] m_AllImgsArr;

        private Image[] m_BaseBtnImgs;
        private RectangleF m_BaseImgRect;
        public static bool[] BtnIsDown;
        private SolidBrush[] m_ClearSoidBrush;
        private string m_DialogueInfo = string.Empty;
        private SolidBrush[] m_EscSolidBrush;
        private bool m_IsOver;
        private readonly ButtonState[] m_KeyboardBtns = new ButtonState[11];
        private readonly KeyboardStyle m_KeyboardStyle;
        private readonly string[] m_Keys = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        private readonly string m_KeyTitle = string.Empty;
        private bool m_NeedDrawBlueRect;
        private ButtonState[] m_OtherBtns;
        private float m_OutputVal;
        private string m_OutWords = string.Empty;
        private readonly bool m_PasswordView;
        private RectangleF[] m_RectImgsRectArr;
        private RectangleF[] m_RectsArr;
        private Region[] m_TheBtnRegionArr;
        private readonly string m_ThePassword = null;
        private SolidBrush[] m_TheSolidBrush;
        private string m_ViewWords = string.Empty;
        private readonly string[] m_StrNumb = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "清除" };
        private readonly string[] m_Str1 = new string[2];
        private readonly string[] m_Str2 = new string[1];

        // Methods
        public Keyboard(Image[] baseImages, RectangleF baseImgRect, KeyboardStyle keyboardStyle, params string[] strArr)
        {
            m_AllImgsArr = baseImages;
            m_BaseImgRect = baseImgRect;
            m_KeyboardStyle = keyboardStyle;
            switch (m_KeyboardStyle)
            {
                case KeyboardStyle.密码键盘之发送取消:
                    if (string.IsNullOrEmpty(strArr[0]))
                    {
                        m_ThePassword = "123";
                        break;
                    }
                    m_ThePassword = strArr[0];
                    m_PasswordView = true;
                    break;

                case KeyboardStyle.密码键盘之确定取消:
                    if (string.IsNullOrEmpty(strArr[0]))
                    {
                        m_ThePassword = "123";
                    }
                    else
                    {
                        m_ThePassword = strArr[0];
                        m_PasswordView = true;
                    }
                    m_KeyTitle = strArr[1];
                    m_Str1[0] = strArr[2];
                    m_Str1[1] = strArr[3];
                    goto Label_0263;

                case KeyboardStyle.输入键盘之ok和取消:
                    m_PasswordView = false;
                    m_KeyTitle = strArr[1];
                    m_Str1[0] = strArr[2];
                    m_Str1[1] = strArr[3];
                    m_DialogueInfo = strArr[4];
                    goto Label_0263;

                case KeyboardStyle.输入键盘之存储:
                    m_PasswordView = false;
                    m_KeyTitle = strArr[1];
                    m_Str2[0] = strArr[2];
                    goto Label_0263;

                default:
                    goto Label_0263;
            }
            m_KeyTitle = strArr[1];
            m_Str1[0] = strArr[2];
            m_Str1[1] = strArr[3];
            m_DialogueInfo = strArr[4];
            Label_0263:
            KeyboardInit();
        }

        private void DrawKeyboardState(Graphics e)
        {
            int num;
            e.DrawImage(m_AllImgsArr[0], m_BaseImgRect);
            for (num = 0; num < m_KeyboardBtns.Length - 1; num++)
            {
                m_KeyboardBtns[num].DrawTheBtn(e, BtnIsDown[num], m_TheSolidBrush);
            }
            m_KeyboardBtns[m_KeyboardBtns.Length - 1].DrawTheBtn(e, BtnIsDown[m_KeyboardBtns.Length - 1], m_ClearSoidBrush);
            for (num = 0; num < m_OtherBtns.Length - 1; num++)
            {
                m_OtherBtns[num].DrawTheBtn(e, BtnIsDown[11 + num], m_TheSolidBrush);
            }
            m_OtherBtns[1].DrawTheBtn(e, BtnIsDown[12], m_EscSolidBrush);
            if (m_OutWords != string.Empty)
            {
                m_KeyboardBtns[10].BtnRecover();
                m_OtherBtns[0].BtnRecover();
                e.DrawString(m_PasswordView ? m_ViewWords : m_OutWords, FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_RectsArr[13], FontsItems.TheAlignment(FontRelated.靠左));
                for (num = 0; num < 10; num++)
                {
                    m_KeyboardBtns[num].BtnStateChange(m_OutWords.Length > 2);
                }
            }
            else
            {
                m_KeyboardBtns[10].BtnStateChange(true);
                m_OtherBtns[0].BtnStateChange(true);
                for (num = 0; num < 10; num++)
                {
                    m_KeyboardBtns[num].BtnRecover();
                }
            }
            e.DrawString(m_OutWords.Length > 0 ? m_DialogueInfo : "请输入密码", FontsItems.DefaultFont, SolidBrushsItems.YellowBrush1, m_RectsArr[14], FontsItems.TheAlignment(FontRelated.靠左));
            if (!m_PasswordView)
            {
                e.DrawString(m_DialogueInfo, FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_RectsArr[14], FontsItems.TheAlignment(FontRelated.靠左));
            }
            e.DrawString(m_KeyTitle, FontsItems.Fonts_Regular(FontName.黑体, 10.5f, true), SolidBrushsItems.WhiteBrush, m_PasswordView ? m_RectsArr[15] : m_RectsArr[14], FontsItems.TheAlignment(FontRelated.靠左));
        }

        public bool GetBoolFromKeyboard(Graphics e, ref bool needDraw)
        {
            DrawKeyboardState(e);
            if (m_PasswordView && m_IsOver)
            {
                needDraw = false;
                if (m_OutWords == m_ThePassword)
                {
                    m_OutWords = string.Empty;
                    m_ViewWords = string.Empty;
                    m_DialogueInfo = string.Empty;
                    m_IsOver = false;
                    return true;
                }
                m_OutWords = string.Empty;
                m_ViewWords = string.Empty;
                m_DialogueInfo = string.Empty;
                m_IsOver = false;
                return false;
            }
            return false;
        }

        public float GetFloatFromKeyboard(Graphics e, ref bool needDraw)
        {
            DrawKeyboardState(e);
            m_OutputVal = Convert.ToInt32(string.IsNullOrEmpty(m_OutWords) ? "0" : m_OutWords);
            if (m_IsOver)
            {
                m_OutWords = string.Empty;
                m_ViewWords = string.Empty;
                m_DialogueInfo = string.Empty;
                IsOver = false;
            }
            return m_OutputVal;
        }

        private void GetRectangle(ref RectangleF[] rect)
        {
            for (var i = 0; i < rect.Length; i++)
            {
                rect[i] = Coordinate.TransformCoord(rect[i]);
                rect[i].X += m_BaseImgRect.X;
                rect[i].Y += m_BaseImgRect.Y;
            }
        }

        private void InitRectangles()
        {
            int num;
            int num2;
            if (m_KeyboardStyle == KeyboardStyle.输入键盘之存储)
            {
                m_RectsArr = new RectangleF[15];
                for (num = 0; num < 3; num++)
                {
                    num2 = 0;
                    while (num2 < 3)
                    {
                        m_RectsArr[num2 + num * 3] = new RectangleF(0xea + num2 * 0x30, 0x12 + num * 0x30, 44f, 44f);
                        num2++;
                    }
                }
                m_RectsArr[9] = new RectangleF(234f, 162f, 44f, 44f);
                m_RectsArr[10] = new RectangleF(282f, 162f, 92f, 44f);
                m_RectsArr[11] = new RectangleF(50f, 162f, 120f, 44f);
                m_RectsArr[12] = new RectangleF(18f, 74f, 184f, 29f);
                m_RectsArr[13] = new RectangleF(18f, 36f, 184f, 29f);
                m_RectsArr[14] = new RectangleF(3f, 3f, 200f, 25f);
                m_RectImgsRectArr = new RectangleF[12];
                for (num = 0; num < 12; num++)
                {
                    m_RectImgsRectArr[num].X = m_RectsArr[num].X + 2f;
                    m_RectImgsRectArr[num].Y = m_RectsArr[num].Y + 2f;
                    m_RectImgsRectArr[num].Width = m_RectsArr[num].Width - 4f;
                    m_RectImgsRectArr[num].Height = m_RectsArr[num].Height - 4f;
                }
            }
            else
            {
                m_RectsArr = new RectangleF[0x10];
                for (num = 0; num < 3; num++)
                {
                    for (num2 = 0; num2 < 3; num2++)
                    {
                        m_RectsArr[num2 + num * 3] = new RectangleF(0xea + num2 * 0x30, 0x12 + num * 0x30, 44f, 44f);
                    }
                }
                m_RectsArr[9] = new RectangleF(234f, 162f, 44f, 44f);
                m_RectsArr[10] = new RectangleF(282f, 162f, 92f, 44f);
                for (num = 0; num < 2; num++)
                {
                    m_RectsArr[11 + num] = new RectangleF(0x12 + num * 0x60, 162f, 88f, 44f);
                }
                m_RectsArr[13] = new RectangleF(18f, 74f, 184f, 29f);
                m_RectsArr[14] = new RectangleF(18f, 36f, 184f, 29f);
                m_RectsArr[15] = new RectangleF(3f, 3f, 200f, 25f);
                m_RectImgsRectArr = new RectangleF[13];
                for (num = 0; num < 13; num++)
                {
                    m_RectImgsRectArr[num].X = m_RectsArr[num].X + 2f;
                    m_RectImgsRectArr[num].Y = m_RectsArr[num].Y + 2f;
                    m_RectImgsRectArr[num].Width = m_RectsArr[num].Width - 4f;
                    m_RectImgsRectArr[num].Height = m_RectsArr[num].Height - 4f;
                }
            }
            GetRectangle(ref m_RectsArr);
            GetRectangle(ref m_RectImgsRectArr);
        }

        private void KeyboardInit()
        {
            int num;
            InitRectangles();
            m_TheBtnRegionArr = new Region[m_RectsArr.Length - 3];
            for (num = 0; num < m_TheBtnRegionArr.Length; num++)
            {
                m_TheBtnRegionArr[num] = new Region(m_RectsArr[num]);
            }
            BtnIsDown = new bool[m_TheBtnRegionArr.Length];
            m_BaseBtnImgs = new Image[2];
            if (m_AllImgsArr.Length > 2)
            {
                m_BaseBtnImgs[0] = m_AllImgsArr[2];
                m_BaseBtnImgs[1] = m_AllImgsArr[1];
            }
            for (num = 0; num < 11; num++)
            {
                m_KeyboardBtns[num] = new ButtonState(m_RectsArr[num], m_BaseBtnImgs, m_RectImgsRectArr[num], m_StrNumb[num], false);
            }
            m_KeyboardBtns[10].BtnStateChange(false);
            if (m_KeyboardStyle == KeyboardStyle.输入键盘之存储)
            {
                m_OtherBtns = new ButtonState[] { new ButtonState(m_RectsArr[11], m_BaseBtnImgs, m_RectImgsRectArr[11], m_Str2[1], true) };
                m_OtherBtns[0].BtnStateChange(true);
            }
            else
            {
                m_OtherBtns = new ButtonState[2];
                for (num = 0; num < 2; num++)
                {
                    m_OtherBtns[num] = new ButtonState(m_RectsArr[11 + num], m_BaseBtnImgs, m_RectImgsRectArr[11 + num], m_Str1[num], true);
                }
                m_OtherBtns[0].BtnStateChange(true);
            }
            m_TheSolidBrush = new SolidBrush[] { SolidBrushsItems.BlackBrush, SolidBrushsItems.WhiteBrush, SolidBrushsItems.BlackBrush, SolidBrushsItems.Grey1, SolidBrushsItems.Grey2, CommonRes.BlueBrush };
            m_EscSolidBrush = new SolidBrush[] { CommonRes.BlackBrush, CommonRes.WhiteBrush, CommonRes.BlackBrush };
            m_ClearSoidBrush = new SolidBrush[] { CommonRes.ThinReds };
        }

        public void KeyboardResponseMouseDown(Point nPoint)
        {
            int num;
            for (num = 0; num < 11; num++)
            {
                if (!(!m_TheBtnRegionArr[num].IsVisible(nPoint) || m_KeyboardBtns[num].IsLocked))
                {
                    BtnIsDown[num] = true;
                    break;
                }
            }
            for (num = 11; num < (m_KeyboardStyle == KeyboardStyle.输入键盘之存储 ? 12 : 13); num++)
            {
                if (!(!m_TheBtnRegionArr[num].IsVisible(nPoint) || m_OtherBtns[num - 11].IsLocked))
                {
                    BtnIsDown[num] = true;
                    break;
                }
            }
        }

        public void KeyboardResponseMouseUp(Point nPoint)
        {
            int num;
            for (num = 0; num < 11; num++)
            {
                if (m_TheBtnRegionArr[num].IsVisible(nPoint) && !m_KeyboardBtns[num].IsLocked)
                {
                    BtnIsDown[num] = false;
                    if (num == 10)
                    {
                        m_OutWords = string.Empty;
                        m_ViewWords = string.Empty;
                        m_DialogueInfo = string.Empty;
                    }
                    else
                    {
                        m_OutWords = m_OutWords + m_Keys[num];
                        m_ViewWords = m_ViewWords + "*";
                    }
                    break;
                }
            }
            for (num = 11; num < (m_KeyboardStyle == KeyboardStyle.输入键盘之存储 ? 12 : 13); num++)
            {
                if (m_TheBtnRegionArr[num].IsVisible(nPoint) && !m_OtherBtns[num - 11].IsLocked)
                {
                    switch (num)
                    {
                        case 11:
                            BtnIsDown[12] = false;
                            break;

                        case 12:
                            BtnIsDown[12] = true;
                            break;
                    }
                    if (m_PasswordView)
                    {
                        switch (num)
                        {
                            case 11:
                                m_IsOver = m_OutWords == m_ThePassword;
                                m_DialogueInfo = m_OutWords == m_ThePassword ? "密码正确！" : "密码错误！";
                                break;

                            case 12:
                                m_IsOver = true;
                                break;
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case 11:
                            case 12:
                                m_IsOver = true;
                                break;
                        }
                    }
                    break;
                }
            }
        }

        // Properties
        public bool IsOver
        {
            get
            {
                return m_IsOver;
            }

            set
            {
                m_IsOver = value;
            }
        }

        public string OutWords
        {
            get
            {
                return m_OutWords;
            }
        }
    }
}
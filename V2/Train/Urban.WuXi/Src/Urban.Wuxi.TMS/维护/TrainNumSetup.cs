using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.Wuxi.TMS.维护
{
    /// <summary>
    /// 车号设置
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class TrainNumSetup : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "车号设置";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics dcGs)
        {
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < m_Regions.Count; ++index)
            {
                if (m_Regions[index].IsVisible(nPoint))
                    break;
            }

            if (index >= 0 && index < 13)
            {
                m_ButtonIsDown[index] = true;
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
                    InputPassWord("1");
                    break;
                case 1:
                    m_ButtonIsDown[1] = false;
                    InputPassWord("2");
                    break;
                case 2:
                    m_ButtonIsDown[2] = false;
                    InputPassWord("3");
                    break;
                case 3:
                    m_ButtonIsDown[3] = false;
                    InputPassWord("4");
                    break;
                case 4:
                    m_ButtonIsDown[4] = false;
                    InputPassWord("5");
                    break;
                case 5:
                    m_ButtonIsDown[5] = false;
                    InputPassWord("6");
                    break;
                case 6:
                    m_ButtonIsDown[6] = false;
                    InputPassWord("7");
                    break;
                case 7:
                    m_ButtonIsDown[7] = false;
                    InputPassWord("8");
                    break;
                case 8:
                    m_ButtonIsDown[8] = false;
                    InputPassWord("9");
                    break;
                case 9:
                    m_ButtonIsDown[9] = false;
                    InputPassWord("0");
                    break;
                case 10:
                    m_ButtonIsDown[10] = false;
                    if (!m_Input.Equals(String.Empty))
                    {
                        m_Input = m_Input.Remove(m_Input.Length - 1);
                    }
                    break;
                case 11:
                    m_ButtonIsDown[11] = false;
                    if (m_Input != null)
                    {
                        OnPost(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, int.Parse(m_Input));
                        OnPost(CmdType.ChangePage, 22, 0, 0);
                        m_Input = string.Empty;
                    }
                    break;
                case 12:
                    m_ButtonIsDown[12] = false;
                    OnPost(CmdType.ChangePage, 22, 0, 0);
                    m_Input = string.Empty;
                    break;
                default:
                    return false;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void InputPassWord(string Numb)
        {
            if (m_Input.Length < 6) m_Input += Numb;
        }

        private void DrawOn(Graphics e)
        {
            m_Keyboard.DrawKeyboard(e, ref m_ButtonIsDown, m_DrawFormat);

            e.DrawString("原车号", FormatStyle.m_Font14, FormatStyle.m_WhiteBrush, new Rectangle(80, 220, 70, 50), m_RightFormat);
            e.DrawString("新车号", FormatStyle.m_Font14, FormatStyle.m_WhiteBrush, new Rectangle(80, 273, 70, 50), m_RightFormat);
            for (int i = 0; i < 2; i++)
            {
                e.FillRectangle(FormatStyle.m_WhiteBrush, m_Rects[16 + i]);
            }

            //e.DrawString(((int)fValue[0]).ToString("00000"), FormatStyle.Font20, FormatStyle.BlackBrush, _rects[18], drawFormat);
            e.DrawString(m_Input, FormatStyle.m_Font20, FormatStyle.m_BlackBrush, m_Rects[19], m_DrawFormat);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            m_DrawFormat.LineAlignment = (StringAlignment)1;
            m_DrawFormat.Alignment = (StringAlignment)1;

            m_RightFormat.LineAlignment = (StringAlignment)2;
            m_RightFormat.Alignment = (StringAlignment)1;

            m_LeftFormat.LineAlignment = (StringAlignment)0;
            m_LeftFormat.Alignment = (StringAlignment)1;

            m_PDrawPoint = new PointF[20];

            m_Rects = new RectangleF[120];

            m_Images = new Image[15];

            m_ButtonIsDown = new bool[15];

            m_Regions = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::: _rects ::::::::::::::::::::::
            m_Rects[0] = new RectangleF(0, 0, 800, 30);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    m_Rects[1 + i * 3 + j] = new RectangleF(376 + j * 127, 170 + i * 57, 125, 55);
                }
            }
            m_Rects[10] = new RectangleF(376, 341, 125, 55);
            m_Rects[11] = new RectangleF(503, 341, 252, 55);
            for (int i = 0; i < 2; i++)
            {
                m_Rects[12 + i] = new RectangleF(376 + i * 254, 420, 125, 55);
            }

            m_Rects[14] = new RectangleF(80, 220, 70, 50);
            m_Rects[15] = new RectangleF(80, 273, 70, 50);

            for (int i = 0; i < 2; i++)
            {
                m_Rects[16 + i] = new RectangleF(150, 220 + 53 * i, 100, 50);
            }

            m_Rects[18] = new RectangleF(150, 220, 100, 50);
            m_Rects[19] = new RectangleF(150, 273, 100, 50);

            //MoveScreen
            for (int i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: point ::::::::::::::::::::::
            m_PDrawPoint[0] = new PointF(376, 170);

            //MoveScreen
            for (int i = 0; i < m_PDrawPoint.Length; i++)
            {
                m_PDrawPoint[i].X = (m_PDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_PDrawPoint[i].Y = (m_PDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: _regions :::::::::::::::::::::::
            for (int i = 0; i < 13; i++)
            {
                m_Regions.Add(new Region(m_Rects[1 + i]));
            }
            #endregion
            
            m_Keyboard = new NumbKeyboard(m_PDrawPoint[0]);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        public String[] m_Str3 = new String[6] { "840", "840", "840", "840", "840", "840" };
        public String[] m_Str4 = new String[6] { "TC1", "MP1", "M1", "TC2", "MP2", "M2" };

        public String m_Numb = string.Empty;
        public String m_Input = string.Empty;

        NumbKeyboard m_Keyboard;
        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::
        /// <summary>
        /// 坐标集
        /// </summary>
        private PointF[] m_PDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        private RectangleF[] m_Rects;

        /// <summary>
        /// 图片集
        /// </summary>
        private Image[] m_Images;

        /// <summary>
        /// 键是否按下
        /// </summary>
        private bool[] m_ButtonIsDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        private List<Region> m_Regions;
        #endregion#
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.Wuxi.TMS.维护
{
    /// <summary>
    /// 轮径设置
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class WheelDia : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "轮径设置";
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
            else if (index >= 13 && index < 19)
            {
                for (int i = 0; i < 6; i++)
                {
                    m_ButtonIsDown[13 + i] = false;
                }
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
            if (index >= 0 && index < 13)
            {
                m_ButtonIsDown[index] = false;
            }
            switch (index)
            {
                case 0:
                    WheelSet("1");
                    break;
                case 1:
                    WheelSet("2");
                    break;
                case 2:
                    WheelSet("3");
                    break;
                case 3:
                    WheelSet("4");
                    break;
                case 4:
                    WheelSet("5");
                    break;
                case 5:
                    WheelSet("6");
                    break;
                case 6:
                    WheelSet("7");
                    break;
                case 7:
                    WheelSet("8");
                    break;
                case 8:
                    WheelSet("9");
                    break;
                case 9:
                    WheelSet("0");
                    break;
                case 10:
                    if (!m_Input[m_Flagmenu].Equals(String.Empty))
                    {
                        m_Input[m_Flagmenu] = m_Input[m_Flagmenu].Remove(m_Input[m_Flagmenu].Length - 1);
                    }
                    break;
                case 11:
                    OnPost(CmdType.ChangePage, 22, 0, 0);
                    m_Input[m_Flagmenu] = string.Empty;
                    break;
                case 12:
                    OnPost(CmdType.ChangePage, 22, 0, 0);
                    m_Input[m_Flagmenu] = string.Empty;
                    break;
                case 13:
                    m_Flagmenu = 0;
                    break;
                case 14:
                    m_Flagmenu = 1;
                    break;
                case 15:
                    m_Flagmenu = 2;
                    break;
                case 16:
                    m_Flagmenu = 3;
                    break;
                case 17:
                    m_Flagmenu = 4;
                    break;
                case 18:
                    m_Flagmenu = 5;
                    break;
                default:
                    return false;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void WheelSet(String Numb)
        {
            if (m_Input[m_Flagmenu].Length < 3) m_Input[m_Flagmenu] += Numb;
        }

        private void DrawOn(Graphics e)
        {
            m_Keyboard.DrawKeyboard(e, ref m_ButtonIsDown, m_DrawFormat);

            for (int i = 0; i < 6; i++)
            {
                e.DrawImage(m_Images[0], m_Rects[14 + i]);
            }

            e.DrawString("原轮径", FormatStyle.m_Font12, FormatStyle.m_WhiteBrush, m_Rects[20], m_RightFormat);
            e.DrawString("新轮径", FormatStyle.m_Font12, FormatStyle.m_WhiteBrush, m_Rects[21], m_RightFormat);

            for (int i = 0; i < 2; i++)
            {
                e.FillRectangle(FormatStyle.m_WhiteBrush, m_Rects[22 + i]);
            }

            e.DrawImage(m_Images[1], m_Rects[14 + m_Flagmenu]);
            e.DrawString(m_Str3[m_Flagmenu], FormatStyle.m_Font16, FormatStyle.m_BlackBrush, m_Rects[24], m_DrawFormat);
            e.DrawString(m_Input[m_Flagmenu], FormatStyle.m_Font16, FormatStyle.m_BlackBrush, m_Rects[25], m_DrawFormat);

            for (int i = 0; i < 6; i++)
            {
                e.DrawString(m_Str4[i], FormatStyle.m_Font16, FormatStyle.m_BlackBrush, m_Rects[14 + i], m_DrawFormat);
            }
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

            m_ButtonIsDown = new bool[20];

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

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    m_Rects[14 + i * 3 + j] = new RectangleF(50 + j * 102, 170 + i * 62, 102, 62);
                }
            }

            m_Rects[20] = new RectangleF(40, 400, 70, 40);
            m_Rects[21] = new RectangleF(180, 400, 70, 40);

            for (int i = 0; i < 2; i++)
            {
                m_Rects[22 + i] = new RectangleF(110 + i * 140, 400, 70, 40);
            }

            m_Rects[24] = new RectangleF(110, 400, 70, 40);
            m_Rects[25] = new RectangleF(250, 400, 70, 40);


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
            for (int i = 0; i < 19; i++)
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

        int m_Flagmenu = 0;

        public String m_Numb = String.Empty;
        public String m_Str = String.Empty;
        public String[] m_Input = new String[6] { "", "", "", "", "", "" };


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
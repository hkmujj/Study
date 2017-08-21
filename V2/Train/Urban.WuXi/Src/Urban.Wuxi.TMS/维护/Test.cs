using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.Wuxi.TMS.维护
{
    /// <summary>
    /// 测试
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Test : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "测试";
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
                {
                    m_ButtonIsDown[index] = true;
                    break;
                }
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < m_Regions.Count; ++index)
            {
                if (m_Regions[index].IsVisible(nPoint))
                {
                    m_ButtonIsDown[index] = false;
                    break;
                }
            }
            switch (index)
            {
                case 4:
                    OnPost(CmdType.ChangePage, 22, 0, 0);
                    break;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void DrawOn(Graphics e)
        {
            //e.FillRectangle(WhiteBrush, new Rectangle(374, 168, 380, 227));
            e.DrawLine(FormatStyle.m_WhitePen, m_PDrawPoint[0], m_PDrawPoint[1]);
            e.DrawLine(FormatStyle.m_MediumGreyPen, m_PDrawPoint[2], m_PDrawPoint[3]);

            for (int i = 0; i < 5; i++)
            {
                if (m_ButtonIsDown[i])
                    e.DrawImage(m_Images[2], m_Rects[1 + i]);
                else
                    e.DrawImage(m_Images[0], m_Rects[1 + i]);
            }
            e.DrawString("返回", FormatStyle.m_Font12, FormatStyle.m_BlackBrush, m_Rects[5], m_DrawFormat);

            for (int i = 0; i < 20; i++)
            {
                if (m_ButtonIsDown[5 + i])
                    e.DrawImage(m_Images[2], m_Rects[6 + i]);
                else
                    e.DrawImage(m_Images[1], m_Rects[6 + i]);
                e.DrawString(m_Str1[i], FormatStyle.m_Font12, FormatStyle.m_BlackBrush, m_Rects[6 + i], m_DrawFormat);
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

            m_ButtonIsDown = new bool[25];

            m_Regions = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::: _rects ::::::::::::::::::::::
            m_Rects[0] = new RectangleF(0, 0, 800, 30);

            for (int i = 0; i < 5; i++)
            {
                m_Rects[1 + i] = new RectangleF(708, 115 + i * 85, 80, 70);
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m_Rects[6 + i * 4 + j] = new RectangleF(75 + j * 150, 120 + i * 78, 107, 71);
                }

                m_Rects[22 + i] = new RectangleF(75 + i * 150, 450, 107, 71);
            }


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
            m_PDrawPoint[0] = new PointF(0, 100);
            m_PDrawPoint[1] = new PointF(800, 100);

            m_PDrawPoint[2] = new PointF(700, 101);
            m_PDrawPoint[3] = new PointF(700, 550);

            for (int i = 0; i < m_PDrawPoint.Length; i++)
            {
                m_PDrawPoint[i].X = (m_PDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_PDrawPoint[i].Y = (m_PDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: _regions :::::::::::::::::::::::
            for (int i = 0; i < 25; i++)
            {
                m_Regions.Add(new Region(m_Rects[1 + i]));
            }
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        public String[] m_Str1 = new String[20] { "备用_1", "电制动\n切除", "合主断", "VVVF隔离", "分主断", "备用_6", "备用_7", 
            "备用_8", "备用_9", "备用_10", "备用_11", "备用_12", "备用_13", "备用_14", "备用_15", "备用_16", "备用", "备用", "备用", "备用" };
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
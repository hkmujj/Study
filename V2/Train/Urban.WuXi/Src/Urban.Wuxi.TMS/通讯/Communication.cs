using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;

namespace Urban.Wuxi.TMS.通讯
{
    /// <summary>
    /// 通讯状态
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Communication : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "通讯状态";
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
            GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public override bool mouseDown(Point nPoint)
        {
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void DrawFrame(Graphics e)
        {
            for (int i = 0; i < 73; i++)
            {
                e.DrawLine(FormatStyle.m_WhitePen, m_PointsList1[i], m_PointsList2[i]);
            }
            for (int i = 0; i < 64; i++)
            {
                if (m_BValue[i * 3])
                    e.DrawImage(m_Img[1], m_RectsList[i]);
                else if (m_BValue[i * 3 + 1])
                    e.DrawImage(m_Img[2], m_RectsList[i]);
                else
                    e.DrawImage(m_Img[0], m_RectsList[i]);

                e.DrawString(FormatStyle.m_Str11[i], FormatStyle.m_Font12B,
                    FormatStyle.m_BlackBrush, m_RectsList[i], m_DrawFormat);

            }
            //画六个门
            for (int i = 0; i < 6; i++)
            {
                e.DrawImage(m_Img[i + 3], rect1[i]);
            }
        }

        private void DrawOn(Graphics e)
        {
            DrawFrame(e);
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

            m_BValue = new bool[200];
            rect1 = new RectangleF[6];

            m_PDrawPoint = new PointF[200];

            m_Rects = new RectangleF[200];

            m_PointsList1 = new List<PointF>();

            m_PointsList2 = new List<PointF>();

            m_RectsList = new List<RectangleF>();

            m_Img = new Image[10];

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::   rects    :::::::::::::::::::
            for (int i = 0; i < 6; i++)
            {
                //6个REP
                m_Rects[i] = new Rectangle(143 + i * 95, 190, 60, 30);
            }
            for (int i = 0; i < 8; i++)
            {
                m_Rects[6 + i] = new RectangleF(20, 230 + i * 35, 60, 30);
                m_Rects[14 + i] = new RectangleF(110, 230 + i * 35, 60, 30);
                m_Rects[22 + i] = new RectangleF(193, 230 + i * 35, 60, 30);
                m_Rects[30 + i] = new RectangleF(288, 230 + i * 35, 60, 30);
                m_Rects[38 + i] = new RectangleF(473, 230 + i * 35, 60, 30);
                m_Rects[46 + i] = new RectangleF(568, 230 + i * 35, 60, 30);
                m_Rects[54 + i] = new RectangleF(651, 230 + i * 35, 60, 30);
                m_Rects[62 + i] = new RectangleF(741, 230 + i * 35, 60, 30);
            }

            //MoveScreen
            for (int i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + FormatStyle.ScreenMoveX - 10.0f) * FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }

            for (int i = 0; i < m_Rects.Length; i++)
            {
                if (i == 16 || i == 23 || i == 25 || i == 47 || i == 49 || i == 56) continue;
                m_RectsList.Add(m_Rects[i]);
            }
            #endregion

            #region ::::::::::::::::::::::: pDrawPoint :::::::::::::::::::
            //1
            m_PDrawPoint[0] = new Point(125, 160);
            m_PDrawPoint[100] = new Point(695, 160);

            for (int i = 0; i < 6; i++)
            {
                //第一行的竖线
                m_PDrawPoint[1 + i] = new Point(173 + i * 95, 160);
                m_PDrawPoint[101 + i] = new Point(173 + i * 95, 190);
            }
            for (int i = 0; i < 8; i++)
            {
                //第一列的横线长15
                m_PDrawPoint[7 + i] = new PointF(80, 245 + i * 35);
                m_PDrawPoint[107 + i] = new PointF(95, 245 + i * 35);

                //第二列的横线15
                m_PDrawPoint[15 + i] = new PointF(95, 245 + i * 35);
                m_PDrawPoint[115 + i] = new PointF(110, 245 + i * 35);

                //第三列的横线15
                m_PDrawPoint[23 + i] = new PointF(253, 245 + i * 35);
                m_PDrawPoint[123 + i] = new PointF(268, 245 + i * 35);

                //第四列的横线15
                m_PDrawPoint[31 + i] = new PointF(348, 245 + i * 35);
                m_PDrawPoint[131 + i] = new PointF(363, 245 + i * 35);

                //第五列的横线15
                m_PDrawPoint[39 + i] = new PointF(458, 245 + i * 35);
                m_PDrawPoint[139 + i] = new PointF(473, 245 + i * 35);

                //第六列的横线15
                m_PDrawPoint[47 + i] = new PointF(553, 245 + i * 35);
                m_PDrawPoint[147 + i] = new PointF(568, 245 + i * 35);

                //第七列的横线15
                m_PDrawPoint[55 + i] = new PointF(711, 245 + i * 35);
                m_PDrawPoint[155 + i] = new PointF(726, 245 + i * 35);

                //第八列的横线15
                m_PDrawPoint[63 + i] = new PointF(726, 245 + i * 35);
                m_PDrawPoint[163 + i] = new PointF(741, 245 + i * 35);
            }

            //第二至第五列的竖线
            for (int i = 0; i < 4; i++)
            {
                m_PDrawPoint[71 + i] = new Point(268 + i * 95, 220);
                m_PDrawPoint[171 + i] = new Point(268 + i * 95, 505);
            }

            for (int i = 0; i < 2; i++)
            {
                //第一和第六列的竖线
                m_PDrawPoint[75 + i] = new Point(95 + i * 631, 205);
                m_PDrawPoint[175 + i] = new Point(95 + i * 631, 505);

                //REP行的2条横线
                m_PDrawPoint[77 + i] = new Point(95 + i * 583, 205);
                m_PDrawPoint[177 + i] = new Point(143 + i * 583, 205);
            }

            //MoveScreen
            for (int i = 0; i < m_PDrawPoint.Length; i++)
            {
                m_PDrawPoint[i].X = (m_PDrawPoint[i].X + FormatStyle.ScreenMoveX - 10.0f) * FormatStyle.Scale;
                m_PDrawPoint[i].Y = (m_PDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }

            for (int i = 0; i < 100; i++)
            {
                if (i == 17 || i == 24 || i == 26 || i == 48 || i == 50 || i == 57) continue;
                m_PointsList1.Add(m_PDrawPoint[i]);
            }
            for (int i = 100; i < 200; i++)
            {
                if (i == 117 || i == 124 || i == 126 || i == 148 || i == 150 || i == 157) continue;
                m_PointsList2.Add(m_PDrawPoint[i]);
            }
            //六个门
            for (int i = 0; i < 6; i++)
            {
                rect1[i] = new RectangleF(120 + i * 95, 110, 80, 50);
            }
            #endregion
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void GetValue()
        {
            //接收bool数据
            for (int i = 0; i < UIObj.InBoolList.Count; i++)
            {
                m_BValue[i] = BoolList[UIObj.InBoolList[i]];
            }
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        /// <summary>
        /// 坐标集1
        /// </summary>
        List<PointF> m_PointsList1;

        /// <summary>
        /// 坐标集2
        /// </summary>
        List<PointF> m_PointsList2;

        /// <summary>
        /// 矩形框集1
        /// </summary>
        List<RectangleF> m_RectsList;

        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::
        /// <summary>
        /// 当前周期接收数字量
        /// </summary>
        private bool[] m_BValue;

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
        /// 六个房间矩形框
        private RectangleF[] rect1;
        private Image[] m_Img;

        #endregion#
        #endregion
    }

    /// <summary>
    /// 通讯状态帮助
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class CommunicationHelp : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "通讯状态帮助";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics g)
        {
            //GetValue();
            DrawOn(g);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void DrawOn(Graphics e)
        {
            e.DrawImage(m_Images[0], m_Rects[0]);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {

            m_Rects = new RectangleF[5];

            m_Images = new Image[10];


            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            m_Rects[0] = new RectangleF(0, 95, 800, 456);

            //MoveScreen
            for (int i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::

        /// <summary>
        /// 矩形框集
        /// </summary>
        private RectangleF[] m_Rects;

        /// <summary>
        /// 图片集
        /// </summary>
        private Image[] m_Images;

        #endregion#
        #endregion
    }
}

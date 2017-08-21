using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;

namespace Urban.Wuxi.TMS.车辆状态
{
    /// <summary>
    /// 车辆状态
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class CarState : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "车辆状态";
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

        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < m_Regions.Count; index++)
            {
                if (m_Regions[index].IsVisible(point))
                    break;
            }
            switch (index)
            {
                case 0:
                    m_ButtonIsDown[0] = true;
                    break;
            }

            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < m_Regions.Count; index++)
            {
                if (m_Regions[index].IsVisible(point))
                    break;
            }
            switch (index)
            {
                case 0:
                    m_ButtonIsDown[0] = false;
                    break;
            }
            return base.mouseUp(point);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        /// <summary>
        /// 框架
        /// </summary>
        /// <param name="g"></param>
        private void DrawFrame(Graphics g)
        {
            for (int i = 0; i < 70; i++)
            {
                g.DrawRectangle(FormatStyle.m_WhitePen, m_Rects[i].X, m_Rects[i].Y, m_Rects[i].Width, m_Rects[i].Height);
            }
            for (int i = 0; i < 10; i++)
            {
                g.DrawString(FormatStyle.m_Str7[i], FormatStyle.m_Font12B,
                    FormatStyle.m_WhiteBrush, m_Rects[i], m_DrawFormat);
            }
            for (int i = 0; i < 6; i++)
            {
                g.DrawLine(FormatStyle.m_WhitePen, m_PDrawPoint[i], m_PDrawPoint[i + 6]);
            }

            for (int i = 0; i < m_LinesArr.Length; i++)
            {
                g.DrawString("-", FormatStyle.m_Font18B, FormatStyle.m_WhiteBrush,
                    m_Rects[m_LinesArr[i]], m_DrawFormat);
            }

            for (int i = 0; i < 3; i++)
            {
                g.DrawRectangle(FormatStyle.m_WhitePen, m_Rects[83 + i].X, m_Rects[83 + i].Y, m_Rects[83 + i].Width, m_Rects[83 + i].Height);
            }
            g.DrawString("制动自检", FormatStyle.m_Font14B, FormatStyle.m_WhiteBrush,
                m_Rects[83], m_DrawFormat);
        }

        /// <summary>
        /// 屏上数据
        /// </summary>
        /// <param name="g"></param>
        private void DrawValue(Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                //主断
                if (BoolList[m_BoolIds[0] + i * 2])
                    g.FillRectangle(FormatStyle.m_GreenBrush,
                        m_Rects[11 + i].X, m_Rects[11 + i].Y, 
                        m_Rects[11 + i].Width - 1, m_Rects[11 + i].Height - 1);
                else if (BoolList[m_BoolIds[1] + i * 2])
                    g.FillRectangle(FormatStyle.m_YellowBrush,
                        m_Rects[11 + i].X, m_Rects[11 + i].Y,
                        m_Rects[11 + i].Width - 1, m_Rects[11 + i].Height - 1);
                else
                    g.FillRectangle(FormatStyle.m_OrangeBrush,
                        m_Rects[11 + i].X, m_Rects[11 + i].Y,
                        m_Rects[11 + i].Width - 1, m_Rects[11 + i].Height - 1);

                //中间电压
                g.DrawString(Convert.ToInt32(FloatList[m_FoolatIds[0]+i]).ToString(),
                    FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_Rects[17 + i], m_DrawFormat);

                //电机电流
                g.DrawString(Convert.ToInt32(FloatList[m_FoolatIds[1]+i]).ToString(),
                    FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_Rects[23 + i], m_DrawFormat);

            }

            for (int i = 0; i < 2; i++)
            {
                //SIV
                if (BoolList[m_BoolIds[2] + i * 2])
                    g.FillRectangle(FormatStyle.m_GreenBrush,
                        m_Rects[28 + i * 5].X, m_Rects[28 + i * 5].Y,
                        m_Rects[28 + i * 5].Width - 1, m_Rects[28 + i * 5].Height - 1);
                //if (BoolList[_BoolIds[8 + i * 2]])
                //    e.FillRectangle(FormatStyle.GreenBrush, _rects[28 + i * 5]);
                else if (BoolList[m_BoolIds[3] + i * 2])
                    g.FillRectangle(FormatStyle.m_RedBrush,
                        m_Rects[28 + i * 5].X, m_Rects[28 + i * 5].Y,
                        m_Rects[28 + i * 5].Width - 1, m_Rects[28 + i * 5].Height - 1);
                else
                    g.FillRectangle(FormatStyle.m_OrangeBrush,
                        m_Rects[28 + i * 5].X, m_Rects[28 + i * 5].Y,
                        m_Rects[28 + i * 5].Width - 1, m_Rects[28 + i * 5].Height - 1);


                //380输出电压
                g.DrawString(Convert.ToInt32(FloatList[m_FoolatIds[2]+i]).ToString(),
                    FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_Rects[34 + i * 5], m_DrawFormat);

                //110V输出
                g.DrawString(Convert.ToInt32(FloatList[m_FoolatIds[3]+i]).ToString(),
                    FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_Rects[40 + i * 5], m_DrawFormat);

                //充电电流
                g.DrawString(Convert.ToInt32(FloatList[m_FoolatIds[4]+ i]).ToString(),
                    FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_Rects[46 + i * 5], m_DrawFormat);
                                
            }

            //总风压力
            for (int i = 0; i < 6; i++)
            {
                g.DrawString(Convert.ToInt32(FloatList[m_FoolatIds[5]+i]).ToString(),
                    FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_Rects[52 + i], m_DrawFormat);
            }

            for (int i = 0; i < 12; i++)
            {
                //空簧压力
                g.DrawString(Convert.ToInt32(FloatList[m_FoolatIds[6]+i]).ToString(),
                    FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_Rects[87 + i], m_DrawFormat);
                //制动缸压力
                g.DrawString(Convert.ToInt32(FloatList[m_FoolatIds[7]+i]).ToString(),
                    FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_Rects[71 + i], m_DrawFormat);
            }

            //RFK
            if (BoolList[m_BoolIds[5]])
                g.FillRectangle(FormatStyle.m_GreenBrush,
                    m_Rects[30].X, m_Rects[30].Y, 
                    m_Rects[30].Width - 1, m_Rects[30].Height - 1);
            else if (BoolList[m_BoolIds[4]])
                g.FillRectangle(FormatStyle.m_BgrBursh,
                    m_Rects[30].X, m_Rects[30].Y, 
                    m_Rects[30].Width - 1, m_Rects[30].Height - 1);
            else
                g.FillRectangle(FormatStyle.m_OrangeBrush,
                    m_Rects[30].X, m_Rects[30].Y,
                    m_Rects[30].Width - 1, m_Rects[30].Height - 1);
            g.DrawString("KMK", FormatStyle.m_Font14, 
                BoolList[m_BoolIds[5]] ? FormatStyle.m_BlackBrush : FormatStyle.m_WhiteBrush,
                m_Rects[30], m_DrawFormat);
            //画门 Tc1 M11 M21 M12 M22 Tc2
            for (int i = 0; i < 6; i++)
            {
                g.DrawImage(m_Images[2+i],m_Rectangle[i]);
            }
        }

        private void DrawOn(Graphics e)
        {
            DrawValue(e);
            DrawFrame(e);

            if (m_ButtonIsDown[0])
                e.DrawImage(m_Images[1], m_Rects[86]);
            else
                e.DrawImage(m_Images[0], m_Rects[86]);

            e.DrawString("自检开始", FormatStyle.m_Font12B, FormatStyle.m_BlackBrush,
                m_Rects[86], m_DrawFormat);
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

            m_Rects = new RectangleF[150];
            m_Rectangle=new RectangleF[10];
         
            m_Images = new Image[10];

            m_ButtonIsDown = new bool[10];

            m_Regions = new List<Region>();
            m_BoolIds = new List<int>();
            m_FoolatIds=new List<int>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::::: _rects :::::::::::::::::::::::::::::::::
            for (int i = 0; i < 10; i++)
            {
                m_Rects[i] = new Rectangle(10, 170 + i * 29, 115, 29);

                for (int j = 0; j < 6; j++)
                {
                    m_Rects[10 + i * 6 + j] = new Rectangle(125 + j * 95, 170 + i * 29, 95, 29);
                }
            }

            //制动缸压力
            for (int i = 0; i < 6; i++)
            {
                m_Rects[71 + i * 2] = new Rectangle(125 + i * 95, 431, 48, 29);
                m_Rects[72 + i * 2] = new Rectangle(173 + i * 95, 431, 48, 29);
            }

            //自检
            m_Rects[83] = new Rectangle(10, 460, 115, 48);
            for (int i = 0; i < 2; i++)
            {
                m_Rects[84 + i] = new Rectangle(125 + i * 285, 460, 285, 48);
            }
            m_Rects[86] = new Rectangle(700, 460, 80, 45);

            //空簧压力
            for (int i = 0; i < 6; i++)
            {

                m_Rects[87 + i * 2] = new Rectangle(125 + i * 95, 402, 48, 29);
                m_Rects[88 + i * 2] = new Rectangle(173 + i * 95, 402, 48, 29);
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

            #region ::::::::::::::::::::::::::: _pDrawPoint :::::::::::::::::::::::::::
            for (int i = 0; i < 6; i++)
            {
                m_PDrawPoint[i] = new Point(172 + i * 95, 402);
                m_PDrawPoint[i + 6] = new Point(172 + i * 95, 460);
            }

            //MoveScreen
            for (int i = 0; i < m_PDrawPoint.Length; i++)
            {
                m_PDrawPoint[i].X = (m_PDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_PDrawPoint[i].Y = (m_PDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            //Tc1 M11 M21 M22 M12 Tc2车
            for (int i = 0; i < 6; i++)
            {
                m_Rectangle[i]=new RectangleF(133+i*95,110,80,50);
            }
            for (int index = 0; index < UIObj.InBoolList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InBoolList[index + 1]; i++)
                {
                    m_BoolIds.Add(UIObj.InBoolList[index] + i);
                }
            }
            for (int index = 0; index < UIObj.InFloatList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InFloatList[index + 1]; i++)
                {
                    m_FoolatIds.Add(UIObj.InFloatList[index] + i);
                }
            }
            #endregion

            #region :::::::::::::::::::::::::::::::: _regions ::::::::::::::::::::::::::::::::::::::
            m_Regions.Add(new Region(m_Rects[86]));
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        private readonly int[] m_LinesArr = new int[] { 10, 15, 16, 21, 22, 27, 29, 31, 32, 35, 36, 37, 38, 41, 42, 43, 44, 47, 48, 49, 50 };
        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::
        /// <summary>
        /// 坐标集
        /// </summary>
        private PointF[] m_PDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        private RectangleF[] m_Rects;

        private RectangleF[] m_Rectangle;
        
      

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

        /// <summary>
        /// bool逻辑号
        /// </summary>
        private List<int> m_BoolIds;

        private List<int> m_FoolatIds;

        #endregion#

        #endregion
    }

    /// <summary>
    /// 车辆状态帮助
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class CarStateHelp : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "车辆状态帮助";
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
            //GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
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
            m_Rects = new RectangleF[20];

            m_Images = new Image[10];

            new List<Region>();
          

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }
          
            m_Rects[0] = new RectangleF(50, 200, 598, 250);

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

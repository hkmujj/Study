using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;

namespace Urban.Wuxi.TMS
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Lightness : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "亮度调节";
        }


        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 1)
            {
                if (nParaB == 19)
                    m_IsDraw = true;
                else
                    m_IsDraw = false;
            }
        }

        public override void paint(Graphics dcGs)
        {
            if (m_IsDraw)
            {
                DrawOn(dcGs);
            }
            base.paint(dcGs);
        }
        /// <summary>
        /// 鼠标弹起的状态
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            while (index < 2)
            {
                if (m_Regions[index].IsVisible(nPoint))
                {
                    break;
                }
                index++;
            }
            switch (index)
            {
                case 0:
                    if (m_LightNum > 0)
                        m_LightNum--;
                    break;
                case 1:
                    if (m_LightNum < 5)
                        m_LightNum++;
                    break;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            e.DrawLine(FormatStyle.m_MediumGreyPen, m_PDrawPoint[0], m_PDrawPoint[1]);

            for (int i = 0; i < 2; i++)
            {
                e.DrawImage(m_Images[i + 1], m_Rects[i]);
            }

            for (int i = 0; i < 5; i++)
            {
                e.DrawImage((i + 1) > m_LightNum ? m_Images[3] : m_Images[4], m_Rects[7 + i]);

                e.DrawImage(m_Images[0], m_Rects[2 + i]);
            }

            e.FillRectangle(m_BlackBrush[m_LightNum], m_Rects[12]);
            e.DrawString("亮度调节",FormatStyle.m_Font12B,FormatStyle.m_WhiteBrush,340,20,m_DrawFormat);      
            e.DrawString("亮度调节",FormatStyle.m_Font18B,FormatStyle.m_WhiteBrush,340,365,m_DrawFormat);
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

            m_Rects = new RectangleF[50];

            m_Images = new Image[20];


            m_Regions = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::::::: _rects ::::::::::::::::::::::::::::::::::::
            //暗/亮
            for (int i = 0; i < 2; i++)
            {
                m_Rects[i] = new Rectangle(188+ i * 282, 260, 50, 75);
            }
          
            //亮度条
            for (int i = 0; i < 5; i++)
            {
                m_Rects[7 + i] = new Rectangle(250 + i * 45, 260, 30, 75);
            }
            m_Rects[12] = new Rectangle(0, 0, 1000, 600);


            //MoveScreen
            for (int i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }
            #endregion
        
            #endregion

            #region ::::::::::::::::::::::::::::::::::: _regions :::::::::::::::::::::::::::::::::::::
            for (int i = 0; i < 7; i++)
            {
                m_Regions.Add(new Region(m_Rects[i]));
            }
            #endregion
        }


        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        /// <summary>
        /// 是否在亮度调节画面
        /// </summary>
        public bool m_IsDraw = false;

        /// <summary>
        /// 绿色的键值
        /// </summary>
        public int m_LightNum = 5;

        public static SolidBrush[] m_BlackBrush = new SolidBrush[6] {
            new SolidBrush(Color.FromArgb(175, 0, 0, 0)),
            new SolidBrush(Color.FromArgb(140, 0, 0, 0)),
            new SolidBrush(Color.FromArgb(105, 0, 0, 0)),
            new SolidBrush(Color.FromArgb(70, 0, 0, 0)),
            new SolidBrush(Color.FromArgb(35, 0, 0, 0)),
            new SolidBrush(Color.FromArgb(0, 0, 0, 0))
        };
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
        /// 按键列表
        /// </summary>
        private List<Region> m_Regions;
        #endregion#
        #endregion
    }
}

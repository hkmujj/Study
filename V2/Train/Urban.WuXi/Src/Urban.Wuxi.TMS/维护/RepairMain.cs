using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.Wuxi.TMS.维护
{
    /// <summary>
    /// 检修主界面
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class RepairMain : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "检修主界面";
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
            for (; index < m_Rect.Count; ++index)
            {
                if (m_Rect[index].IsVisible(nPoint))
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
            for (; index < m_Rect.Count; ++index)
            {
                if (m_Rect[index].IsVisible(nPoint))
                    break;
            }
            if (index >= 0 && index < 13)
            {
                m_ButtonIsDown[index] = false;
            }
            switch (index)
            {
                case 2:
                    OnPost(CmdType.ChangePage, 23, 0, 0);
                    break;
                case 3:
                    OnPost(CmdType.ChangePage, 24, 0, 0);
                    break;
                case 4:
                    OnPost(CmdType.ChangePage, 25, 0, 0);
                    break;
                case 5:
                    OnPost(CmdType.ChangePage, 26, 0, 0);
                    break;
                case 7:
                    OnPost(CmdType.ChangePage, 27, 0, 0);
                    break;
                case 8:
                    OnPost(CmdType.ChangePage, 28, 0, 0);
                    break;
                default:
                    return false;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void DrawOn(Graphics e)
        {
            e.DrawString("设  定", FormatStyle.m_Font22, FormatStyle.m_WhiteBrush, m_Rects[1], m_LeftFormat);
            e.DrawLine(FormatStyle.m_WhitePen, m_PDrawPoint[0], m_PDrawPoint[1]);
            e.DrawString("记录与下载", FormatStyle.m_Font22, FormatStyle.m_WhiteBrush, m_Rects[23], m_LeftFormat);
            e.DrawLine(FormatStyle.m_WhitePen, m_PDrawPoint[2], m_PDrawPoint[3]);
            e.DrawString("查  询", FormatStyle.m_Font22, FormatStyle.m_WhiteBrush, m_Rects[24], m_LeftFormat);
            e.DrawLine(FormatStyle.m_WhitePen, m_PDrawPoint[4], m_PDrawPoint[5]);

            for (int i = 0; i < 13; i++)
            {
                if (m_ButtonIsDown[i])
                    e.DrawImage(m_Images[1], m_Rects[25 + i]);
                else
                    e.DrawImage(m_Images[0], m_Rects[25 + i]);
                e.DrawString(m_Str1[i], FormatStyle.m_Font12, FormatStyle.m_WhiteBrush, m_Rects[25 + i], m_DrawFormat);
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

            m_ButtonIsDown = new bool[15];

            m_Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::: _rects ::::::::::::::::::::::
            m_Rects[0] = new RectangleF(0, 0, 800, 30);
            m_Rects[1] = new RectangleF(20, 120, 300, 40);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    m_Rects[i * 7 + j + 2] = new RectangleF(60 + j * 95, 180 + i * 150, 90, 50);
                }
            }
            m_Rects[23] = new RectangleF(20, 270, 300, 40);
            m_Rects[24] = new RectangleF(20, 420, 300, 40);

            for (int i = 0; i < 6; i++)
            {
                m_Rects[25 + i] = m_Rects[2 + i];
            }
            for (int i = 0; i < 2; i++)
            {
                m_Rects[31 + i] = m_Rects[9 + i];
            }
            for (int i = 0; i < 5; i++)
            {
                m_Rects[33 + i] = m_Rects[16 + i];
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
            m_PDrawPoint[0] = new PointF(60, 160);
            m_PDrawPoint[1] = new PointF(700, 160);

            m_PDrawPoint[2] = new PointF(60, 310);
            m_PDrawPoint[3] = new PointF(700, 310);

            m_PDrawPoint[4] = new PointF(60, 460);
            m_PDrawPoint[5] = new PointF(700, 460);

            for (int i = 0; i < m_PDrawPoint.Length; i++)
            {
                m_PDrawPoint[i].X = (m_PDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_PDrawPoint[i].Y = (m_PDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: _rect :::::::::::::::::::::::
            for (int i = 0; i < 6; i++)
            {
                m_Rect.Add(new Region(m_Rects[2 + i]));
            }
            for (int i = 0; i < 2; i++)
            {
                m_Rect.Add(new Region(m_Rects[9 + i]));
            }
            for (int i = 0; i < 5; i++)
            {
                m_Rect.Add(new Region(m_Rects[16 + i]));
            }
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        public String[] m_Str1 = new String[13] { "时间", "密码", "轮径", "车号", "加速度测试", "测试", "控制参数", "数据记录", "USB", "端口数据", "版本", "I / O", "参数明细" };
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
        private List<Region> m_Rect;
        #endregion#
        #endregion
    }
}
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.Wuxi.TMS.运行
{
    /// <summary>
    /// 紧急广播
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class EmergencyRadio : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "紧急广播";
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
        }

        private void SetSoundID(int soundId)
        {
            if (soundId >= 0 && soundId < 29)
            {
                for (int i = 0; i < 29; i++)
                {
                    m_ButtonIsDown[i] = false;
                }
                m_ButtonIsDown[soundId] = true;
                //发送声音编号
                //OnPost()
            }
        }

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < m_Regions.Count; ++index)
            {
                if (m_Regions[index].IsVisible(nPoint))
                {
                    SetSoundID(index);
                    break;
                }
            }
            if (index == 29)
                m_ButtonIsDown[29] = true;
            else
            {
                OnPost(CmdType.SetBoolValue, this.GetOutboolStartIndex() + 200 + index, 1, 0);
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
                {
                    break;
                }
            }
            if (index == 29)
            {
                m_ButtonIsDown[29] = false;
                OnPost(CmdType.ChangePage, 11, 0, 0);
            }
            else
            {
                m_ButtonIsDown[index] = false;
                OnPost(CmdType.SetBoolValue, this.GetOutboolStartIndex() + 200 + index, 0, 0);

            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        /// <summary>
        /// 框架
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrame(Graphics e)
        {
            e.FillRectangle(FormatStyle.m_MediumGreySolidBrush,
                m_Rects[0]);
        }

        /// <summary>
        /// 填充数值
        /// </summary>
        /// <param name="e"></param>
        private void DrawValue(Graphics e)
        {
            for (int i = 0; i < 30; i++)
            {
                if (m_ButtonIsDown[i])
                    e.DrawImage(m_Images[1], m_Rects[1 + i]);
                else
                    e.DrawImage(m_Images[0], m_Rects[1 + i]);

                e.DrawString(FormatStyle.m_Str14[i], FormatStyle.m_Font14,
                    FormatStyle.m_BlackBrush, m_Rects[1 + i], m_DrawFormat);
            }
        }


        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            DrawFrame(e);
            DrawValue(e);
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

            m_Rects = new RectangleF[200];

            m_Images = new Image[30];

            m_ButtonIsDown = new bool[30];

            m_Regions = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::::::::: _rects :::::::::::::::::::::::::::::::::::
            m_Rects[0] = new Rectangle(5, 100, 790, 445);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    m_Rects[1 + i * 10 + j] = new Rectangle(6 + i * 267, 104 + j * 44, 255, 35);
                }
            }
            m_Rects[30] = new Rectangle(565, 500, 190, 35);

            //MoveScreen
            for (int i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::::::::::: _regions ::::::::::::::::::::::::::::::::::::::
            for (int i = 0; i < 30; i++)
            {
                m_Regions.Add(new Region(m_Rects[1 + i]));
            }
            #endregion

            #region :::::::::::::::::::::::::::::::: point ::::::::::::::::::::::::::::::::::::::

            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        private int[] m_Lines = new int[6] { 0, 2, 3, 5, 6, 11 };
        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::

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


    /// <summary>
    /// 旁路
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Bypass : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "旁路";
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

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < m_Regions.Count; ++index)
            {
                if (m_Regions[index].IsVisible(nPoint))
                {
                    break;
                }
            }
            switch (index)
            {
                case 0:
                    m_ButtonIsDown[0] = true;
                    break;
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
                    break;
                }
            }
            switch (index)
            {
                case 0:
                    m_ButtonIsDown[0] = false;
                    OnPost(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        /// <summary>
        /// 填充数值
        /// </summary>
        /// <param name="e"></param>
        private void DrawValue(Graphics e)
        {
            for (int i = 0; i < 2; i++)
            {
                e.DrawImage(m_Images[3 - i], m_Rects[i]);
                e.DrawString(FormatStyle.m_Str16[i], FormatStyle.m_Font12,
                    FormatStyle.m_WhiteBrush, m_Rects[i + 2], m_DrawFormat);
            }
            for (int i = 0; i < 4; i++)
            {
                //e.DrawRectangle(FormatStyle.WhitePen, _rects[4 + i]);
                e.DrawString(FormatStyle.m_Str16[i + 2], FormatStyle.m_Font12,
                    FormatStyle.m_WhiteBrush, m_Rects[4 + i], m_RightFormat);

                e.DrawImage(BoolList[m_BoolIds[i]] ? m_Images[3] : m_Images[2], m_Rects[16 + i]);

            }
            for (int i = 0; i < 2; i++)
            {
                //e.DrawRectangle(FormatStyle.WhitePen, _rects[4 + i]);
                e.DrawString(FormatStyle.m_Str16[i + 7], FormatStyle.m_Font12,
                    FormatStyle.m_WhiteBrush, m_Rects[8 + i], m_RightFormat);

                e.DrawImage(BoolList[m_BoolIds[4 + i]] ? m_Images[3] : m_Images[2], m_Rects[20 + i]);

            }
            for (int i = 0; i < 1; i++)
            {
                //e.DrawRectangle(FormatStyle.WhitePen, _rects[4 + i]);
                e.DrawString(FormatStyle.m_Str16[9], FormatStyle.m_Font12,
                    FormatStyle.m_WhiteBrush, m_Rects[10 + i], m_RightFormat);

                e.DrawImage(BoolList[m_BoolIds[6 + i]] ? m_Images[3] : m_Images[2], m_Rects[22 + i]);

            }

            //
            e.DrawLine(FormatStyle.m_MediumGreyPen, m_PDrawPoint[0], m_PDrawPoint[1]);

            //菜单
            for (int i = 0; i < 8; i++)
            {
                e.DrawImage(m_Images[0], m_Rects[28 + i]);
            }
            if (m_ButtonIsDown[0])
                e.DrawImage(m_Images[1], m_Rects[35]);
            //
            e.DrawString("返回", FormatStyle.m_Font14, FormatStyle.m_BlackBrush,
                m_Rects[35], m_DrawFormat);

        }


        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            DrawValue(e);
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

            m_RightFormat.LineAlignment = (StringAlignment)1;
            m_RightFormat.Alignment = (StringAlignment)2;

            m_LeftFormat.LineAlignment = (StringAlignment)0;
            m_LeftFormat.Alignment = (StringAlignment)1;

            m_PDrawPoint = new PointF[200];

            m_Rects = new RectangleF[200];

            m_Images = new Image[30];

            m_ButtonIsDown = new bool[30];

            m_Regions = new List<Region>();
            m_BoolIds = new List<int>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }
            for (int index = 0; index < UIObj.InBoolList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InBoolList[index + 1]; i++)
                {
                    m_BoolIds.Add(UIObj.InBoolList[index] + i);
                }
            }
            #region :::::::::::::::::::::::::::::: _rects :::::::::::::::::::::::::::::::::::
            //
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects[i * 2 + j] = new Rectangle(20 + i * 80, 115 + j * 35, 80, 30);
                }
                for (int k = 0; k < 6; k++)
                {
                    m_Rects[4 + i * 6 + k] = new Rectangle(50 + i * 250, 210 + k * 45, 160, 30);
                    m_Rects[16 + i * 6 + k] = new Rectangle(210 + i * 250, 210 + k * 45, 80, 30);
                }
            }
            //
            for (int i = 0; i < 8; i++)
            {
                m_Rects[28 + i] = new Rectangle(710, 100 + i * 56, 89, 54);
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

            #region :::::::::::::::::::::::::::::::: _regions ::::::::::::::::::::::::::::::::::::::
            m_Regions.Add(new Region(m_Rects[35]));
            #endregion

            #region :::::::::::::::::::::::::::::::: point ::::::::::::::::::::::::::::::::::::::
            m_PDrawPoint[0] = new Point(705, 95);
            m_PDrawPoint[1] = new Point(705, 550);

            //MoveScreen
            for (int i = 0; i < m_PDrawPoint.Length; i++)
            {
                m_PDrawPoint[i].X = (m_PDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_PDrawPoint[i].Y = (m_PDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        List<string> m_Stations = new List<string>();
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
        /// <summary>
        /// 逻辑号
        /// </summary>
        private List<int> m_BoolIds;

        #endregion#

        #endregion
    }

}
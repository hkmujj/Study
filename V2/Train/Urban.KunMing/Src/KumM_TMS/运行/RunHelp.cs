using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace KumM_TMS.运行
{
    /// <summary>
    /// 运行帮助
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    internal class RunHelp : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::

        public override string GetInfo()
        {
            return "运行帮助";
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
            e.DrawImage(m_Img[0], m_Rects[0]);
        }

        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            m_DrawFormat.LineAlignment = StringAlignment.Center;
            m_DrawFormat.Alignment = StringAlignment.Center;

            m_RightFormat.LineAlignment = StringAlignment.Far;
            m_RightFormat.Alignment = StringAlignment.Center;

            m_LeftFormat.LineAlignment = StringAlignment.Near;
            m_LeftFormat.Alignment = StringAlignment.Center;

            m_PDrawPoint = new PointF[5];

            m_Rects = new RectangleF[5];

            m_Img = new Image[10];

            m_ButtonIsDown = new bool[10];

            m_Rect = new List<Region>();

            for (var i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            m_Rects[0] = new RectangleF(0, 100, 798, 450);

            //MoveScreen
            for (var i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + FormatStyle.ScreenMoveX)*FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + FormatStyle.ScreenMoveY)*FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }
        }

        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::

        /// <summary>
        /// 坐标集
        /// </summary>
        internal PointF[] m_PDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        internal RectangleF[] m_Rects;

        /// <summary>
        /// 图片集
        /// </summary>
        internal Image[] m_Img;

        /// <summary>
        /// 键是否按下
        /// </summary>
        internal bool[] m_ButtonIsDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        internal List<Region> m_Rect;

        #endregion#

        #endregion
    }
}
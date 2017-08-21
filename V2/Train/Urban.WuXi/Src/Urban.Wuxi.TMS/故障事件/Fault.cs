using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Urban.Wuxi.TMS.DMITitle;

namespace Urban.Wuxi.TMS.故障事件
{
    /// <summary>
    /// 故障事件
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Fault : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "故障事件";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }
        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2) return;
            if (nParaB != 14) return;
            if (nParaC == 14) return;
            m_RowId = -1;
            m_CurrentPage = 0;
            m_MenuId = 0;
        }

        public override void paint(Graphics dcGs)
        {
            DrawOn(dcGs);
        }


        public override bool mouseDown(Point nPoint)
        {
            int index = 0;

            //行点击
            for (; index < m_Regions1.Count; index++)
            {
                if (!m_Regions1[index].IsVisible(nPoint)) continue;
                switch (m_MenuId)
                {
                    case 0: //当前故障
                        if (m_CurrentPage < Title.MsgInfList.CurrentMsgList.Count / 10)
                            m_RowId = index;
                        else if (Title.MsgInfList.CurrentMsgList.Count % 10 > index)
                            m_RowId = index;
                        else
                            m_RowId = -1;
                        break;
                    case 1: //历史故障
                        if (m_CurrentPage < Title.MsgInfList.AllMsgsList.Count / 10)
                            m_RowId = index;
                        else if (Title.MsgInfList.AllMsgsList.Count % 10 >= index)
                            m_RowId = index;
                        else
                            m_RowId = -1;
                        break;
                    case 2:
                        break;
                }
            }

            //右侧按钮点击
            for (index = 0; index < m_Regions.Count; index++)
            {
                if (m_Regions[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    m_ButtonIsDown[0] = true;//当前

                    break;
                case 1:
                    m_ButtonIsDown[1] = true;//所有

                    break;
                case 2:
                    m_BtnCanDowns[2] = true;//解决方案
                    break;
                case 3://
                    break;
                case 4://
                    break;
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < 5; index++)
            {
                if (m_Regions[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    if (m_BtnCanDown[0])
                    {
                        ChangeButtonState(0);
                        m_MenuId = 0;
                        m_CurrentPage = 0;
                        m_RowId = -1;
                    }
                    break;
                case 1:
                    if (m_BtnCanDown[1])
                    {
                        ChangeButtonState(1);
                        m_MenuId = 1;
                        m_CurrentPage = 0;
                        m_RowId = -1;
                    }
                    break;
                case 2:
                    if (m_BtnCanDown[2] && m_RowId >= 0)
                    {
                        switch (m_MenuId)
                        {
                            case 0:
                                m_SelectCurrentList = true;
                                break;
                            case 1:
                                m_SelectCurrentList = false;
                                break;
                        }
                        m_IDInFaultList = (m_MenuId == 0 ? Title.MsgInfList.CurrentMsgList.Count : Title.MsgInfList.AllMsgsList.Count) - (m_CurrentPage * 10 + m_RowId) - 1;

                        ChangeButtonState(2);
                        m_MenuId = 2;
                        m_RowId = -1;
                        m_ButtonIsDown[2] = false;
                    }
                    break;
                case 3:
                    if (m_BtnCanDown[3])
                    {

                        m_ButtonIsDown[3] = false;

                        switch (m_MenuId)
                        {
                            case 0:
                            case 1:
                                if (m_CurrentPage > 0)
                                    m_CurrentPage--;
                                break;
                            case 2:
                                if (m_IDInFaultList >= 0 && m_IDInFaultList < (m_SelectCurrentList ? Title.MsgInfList.CurrentMsgList.Count - 1 : Title.MsgInfList.AllMsgsList.Count - 1))
                                    m_IDInFaultList++;

                                break;
                        }

                    }

                    break;
                case 4:
                    if (m_BtnCanDown[4])
                    {
                        m_ButtonIsDown[4] = false;

                        switch (m_MenuId)
                        {
                            case 0:
                            case 1:
                                if (m_CurrentPage < m_DefPage) m_CurrentPage++;
                                break;
                            case 2:
                                if (m_IDInFaultList > 0 && m_IDInFaultList < (m_SelectCurrentList ? Title.MsgInfList.CurrentMsgList.Count : Title.MsgInfList.AllMsgsList.Count)) m_IDInFaultList--;
                                break;
                        }
                    }
                    break;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void ChangeButtonState(int buttonNumb)
        {
            for (int i = 0; i < 3; i++)
            {
                m_ButtonIsDown[i] = false;
            }
            m_ButtonIsDown[buttonNumb] = true;
        }
        /// <summary>
        /// 当前故障个数
        /// </summary>
        int m_NoOverDefNumb;
        /// <summary>
        /// 历史故障个数
        /// </summary>
        int m_AllDefNumb;
        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="e"></param>
        /// <summary>
        /// 故障解决方法界面
        /// </summary>
        /// <param name="e"></param>        
        public void DrDef(Graphics e)
        {

            if (m_SelectCurrentList ? Title.MsgInfList.CurrentMsgList.Count > m_IDInFaultList : Title.MsgInfList.AllMsgsList.Count > m_IDInFaultList && m_IDInFaultList >= 0)
            {
                e.DrawString(m_SelectCurrentList ? Title.MsgInfList.CurrentMsgList[m_IDInFaultList].MsgContent : Title.MsgInfList.AllMsgsList[m_IDInFaultList].MsgContent
                , FormatStyle.m_Font18B, FormatStyle.m_WhiteBrush, 20, 125);
                e.DrawString(m_SelectCurrentList ? Title.MsgInfList.CurrentMsgList[m_IDInFaultList].FaultSolutionStr : Title.MsgInfList.AllMsgsList[m_IDInFaultList].FaultSolutionStr,
                    FormatStyle.m_Font18, FormatStyle.m_WhiteBrush, m_FaultSolutionStr);


            }

            e.DrawRectangle(FormatStyle.m_WhitePen, 20, 110, 700, 100);
            e.DrawRectangle(FormatStyle.m_WhitePen, 20, 240, 700, 300);

        }

        private void DrawOn(Graphics e)
        {
            if (m_MenuId == 0)
            {
                if (m_RowId >= 0 && m_RowId + m_CurrentPage * 10 < Title.MsgInfList.CurrentMsgList.Count)
                    e.FillRectangle(FormatStyle.m_ThinBlue, m_Rect1[m_RowId]);
            }
            else
            {
                if (m_RowId >= 0 && m_RowId + m_CurrentPage * 10 < Title.MsgInfList.AllMsgsList.Count)
                    e.FillRectangle(FormatStyle.m_ThinBlue, m_Rect1[m_RowId]);
            }

            m_NoOverDefNumb = Title.MsgInfList.CurrentMsgList.Count;
            m_AllDefNumb = Title.MsgInfList.AllMsgsList.Count;
            if (m_MenuId == 0)
            {
                m_DefPage = Convert.ToInt32(m_NoOverDefNumb / 10);
                DrawCurrentDef(e);
            }
            else if (m_MenuId == 1)
            {
                m_DefPage = Convert.ToInt32(m_AllDefNumb / 10);
                DrawAllDef(e);
            }
            else if (m_MenuId == 2)
            {
                DrDef(e);
            }

            DrawFrame(e);
        }

        /// <summary>
        /// 框架
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrame(Graphics e)
        {
            if (m_MenuId != 2)
            {
                for (int i = 0; i < 5; i++)
                {
                    //等级 代码 故障内容 时间
                    e.FillRectangle(FormatStyle.m_DarkGreyBrush, m_Rects[i]);
                    e.DrawString(FormatStyle.m_Str12[i], FormatStyle.m_Font14B,
                        FormatStyle.m_WhiteBrush, m_Rects[i], m_DrawFormat);
                }

                //顶端灰色线条
                e.DrawLine(FormatStyle.m_DarkGreyPen, m_PDrawPoint[0], m_PDrawPoint[1]);

                //编号
                for (int i = 0; i < 10; i++)
                {
                    e.DrawString((m_CurrentPage * 10 + i + 1).ToString(), FormatStyle.m_Font12B,
                        FormatStyle.m_WhiteBrush, m_Rects[5 + i * 5], m_DrawFormat);
                }
            }
            //当前故障按键，历史故障按键，故障解决方法按键。
            for (int i = 0; i < 2; i++)
            {
                e.DrawImage(m_ButtonIsDown[i] ? m_Img[0] : m_Img[3], m_Rects[75 + i]);

                e.DrawString(FormatStyle.m_Str13[i], FormatStyle.m_Font12B,
                    FormatStyle.m_BlackBrush, m_Rects[75 + i], m_DrawFormat);
            }
            e.DrawImage(m_ButtonIsDown[2] ? m_Img[0] : m_Img[3], m_Rects[77]);

            e.DrawString(FormatStyle.m_Str13[2], FormatStyle.m_Font12B,
                FormatStyle.m_BlackBrush, m_Rects[77], m_DrawFormat);
            e.DrawImage(m_ButtonIsDown[3] ? m_Img[1] : m_Img[4], m_Rects[78]);

            e.DrawImage(m_ButtonIsDown[4] ? m_Img[2] : m_Img[5], m_Rects[79]);
        }

        /// <summary>
        /// 当前故障
        /// </summary>
        /// <param name="e"></param>
        private void DrawCurrentDef(Graphics e)
        {
            if (m_NoOverDefNumb <= 0)
                return;
            for (int i = m_NoOverDefNumb - 1, j = 0; i >= 0; i--, j++)
            {
                if ((m_NoOverDefNumb - 1 - i) >= (m_CurrentPage * 10) && (m_NoOverDefNumb - 1 - i) < (m_CurrentPage + 1) * 10)
                {
                    int tmpRowIndex = j % 10;

                    e.DrawString(Convert.ToInt32(Title.MsgInfList.CurrentMsgList[i].TheMsgLevel).ToString(), FormatStyle.m_Font12B,
                        FormatStyle.m_WhiteBrush, m_Rects[6 + tmpRowIndex * 5], m_DrawFormat);
                    e.DrawString(Title.MsgInfList.CurrentMsgList[i].MsgID, FormatStyle.m_Font12B,
                        FormatStyle.m_WhiteBrush, m_Rects[7 + tmpRowIndex * 5], m_DrawFormat);
                    e.DrawString(Title.MsgInfList.CurrentMsgList[i].MsgContent, FormatStyle.m_Font12B,
                        FormatStyle.m_WhiteBrush, m_Rects[8 + tmpRowIndex * 5], m_DrawFormat);
                    e.DrawString(Title.MsgInfList.CurrentMsgList[i].MsgReceiveTime.ToString("yyyy-MM-dd hh:mm:ss"), FormatStyle.m_Font12B,
                        FormatStyle.m_WhiteBrush, m_Rects[55 + tmpRowIndex * 2], m_DrawFormat);

                }
            }
        }

        /// <summary>
        /// 所有故障
        /// </summary>
        /// <param name="e"></param>
        private void DrawAllDef(Graphics e)
        {
            if (m_AllDefNumb <= 0)
                return;
            for (int i = m_AllDefNumb - 1, j = 0; i >= 0; i--, j++)
            {
                if ((m_AllDefNumb - 1 - i) >= (m_CurrentPage * 10) && (m_AllDefNumb - 1 - i) < (m_CurrentPage + 1) * 10)
                {
                    int tmpRowIndex = j % 10;
                    e.DrawString(Convert.ToInt32(Title.MsgInfList.AllMsgsList[i].TheMsgLevel).ToString(), FormatStyle.m_Font12B,
                        FormatStyle.m_WhiteBrush, m_Rects[6 + tmpRowIndex * 5], m_DrawFormat);
                    e.DrawString(Title.MsgInfList.AllMsgsList[i].MsgID, FormatStyle.m_Font12B,
                        FormatStyle.m_WhiteBrush, m_Rects[7 + tmpRowIndex * 5], m_DrawFormat);
                    e.DrawString(Title.MsgInfList.AllMsgsList[i].MsgContent, FormatStyle.m_Font12B,
                        FormatStyle.m_WhiteBrush, m_Rects[8 + tmpRowIndex * 5], m_DrawFormat);
                    e.DrawString(Title.MsgInfList.AllMsgsList[i].MsgReceiveTime.ToString("yyyy-MM-dd hh:mm:ss"), FormatStyle.m_Font12B,
                        FormatStyle.m_WhiteBrush, m_Rects[55 + tmpRowIndex * 2], m_DrawFormat);
                    if (Title.MsgInfList.AllMsgsList[i].FaultBeOver)
                    {
                        e.DrawString(Title.MsgInfList.AllMsgsList[i].MsgOverTime.ToString("yyyy-MM-dd hh:mm:ss"), FormatStyle.m_Font12B,
                            FormatStyle.m_WhiteBrush, m_Rects[56 + tmpRowIndex * 2], m_DrawFormat);
                    }
                }
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

            m_Rects = new RectangleF[200];
            m_Rect1 = new RectangleF[20];


            m_Img = new Image[10];

            m_ButtonIsDown = new bool[10];

            m_BtnCanDown = new bool[10];
            m_BtnCanDowns = new bool[10];
            for (int i = 0; i < m_BtnCanDown.Length; i++)
            {
                m_BtnCanDown[i] = true;
            }
            for (int i = 0; i < m_BtnCanDowns.Length; i++)
            {
                m_BtnCanDowns[i] = true;
            }
            m_Regions = new List<Region>();
            m_Regions1 = new List<Region>();


            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region ::::::::::::::::::::: rects :::::::::::::::::::::::::::::::
            for (int i = 0; i < 11; i++)
            {
                m_Rects[0 + i * 5] = new Rectangle(5, 105 + 40 * i, 50, 40);
                m_Rects[1 + i * 5] = new Rectangle(57, 105 + 40 * i, 50, 40);
                m_Rects[2 + i * 5] = new Rectangle(109, 105 + 40 * i, 75, 40);
                m_Rects[3 + i * 5] = new Rectangle(186, 105 + 40 * i, 340, 40);
                m_Rects[4 + i * 5] = new Rectangle(528, 105 + 40 * i, 170, 40);
            }
            for (int i = 0; i < 10; i++)
            {
                m_Rect1[i] = new RectangleF(5, 145 + 40 * i, 700, 40);
            }
            for (int i = 0; i < 10; i++)
            {
                m_Rects[55 + i * 2] = new RectangleF(528, 145 + 40 * i, 170, 20);
                m_Rects[56 + i * 2] = new RectangleF(528, 165 + 40 * i, 170, 20);
            }
            for (int i = 0; i < 5; i++)
            {
                m_Rects[75 + i] = new Rectangle(730, 150 + 70 * i, 60, 52);
            }

            //MoveScreen
            for (int i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }
            m_Rects[80] = new RectangleF(20, 110, 700, 40);
            m_Rects[81] = new RectangleF(20, 160, 700, 400);
            #endregion

            #region :::::::::::::::::::: pDrawPoint :::::::::::::::::::::::::::
            m_PDrawPoint[0] = new Point(710, 95);
            m_PDrawPoint[1] = new Point(710, 549);

            //MoveScreen
            for (int i = 0; i < m_PDrawPoint.Length; i++)
            {
                m_PDrawPoint[i].X = (m_PDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_PDrawPoint[i].Y = (m_PDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region ::::::::::::::::::::: Region ::::::::::::::::::::::::
            for (int i = 0; i < 5; i++)
            {
                m_Regions.Add(new Region(m_Rects[75 + i]));
            }
            for (int i = 0; i < 10; i++)
            {
                m_Regions1.Add(new Region(m_Rect1[i]));
            }

            #endregion

            #region ::::::::::::::::::::: isButtonDown ::::::::::::::::::::
            m_ButtonIsDown[0] = true;
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat m_DrawFormat = new StringFormat();
        private readonly RectangleF m_FaultSolutionStr = new RectangleF(20, 250, 680, 300);
        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();
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
        /// 选中矩形框集
        /// </summary>
        private RectangleF[] m_Rect1;
        /// <summary>
        /// 图片集
        /// </summary>
        private Image[] m_Img;

        /// <summary>
        /// 按键
        /// </summary>
        private bool[] m_ButtonIsDown;

        /// <summary>
        /// 键是否能按下
        /// </summary>
        private bool[] m_BtnCanDown;

        private bool[] m_BtnCanDowns;

        /// <summary>
        /// 故障总页码
        /// </summary>
        private int m_DefPage;

        /// <summary>
        /// 当前故障页码
        /// </summary>
        private int m_CurrentPage;

        /// <summary>
        /// 所在行号
        /// </summary>
        private int m_RowId = -1;

        /// <summary>
        /// 0为当前故障
        /// 1为历史故障
        /// 2为故障提示
        /// </summary>
        private int m_MenuId = 0;

        /// <summary>
        /// 按键列表
        /// </summary>
        private List<Region> m_Regions;
        /// <summary>
        /// 选列表
        /// </summary>
        private List<Region> m_Regions1;

        /// <summary>
        /// 当前故障列表中的解决方案
        /// </summary>
        private bool m_SelectCurrentList;

        /// <summary>
        /// 故障下标
        /// </summary>
        private int m_IDInFaultList;

        #endregion#

        #endregion
    }
}

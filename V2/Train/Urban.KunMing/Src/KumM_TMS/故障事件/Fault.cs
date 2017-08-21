using System;
using System.Collections.Generic;
using System.Drawing;
using KumM_TMS.DMITitle;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace KumM_TMS.故障事件
{
    /// <summary>
    /// 故障事件
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Fault : baseClass
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
            if (nParaA == 2)
            {
                if (nParaB == 14 && nParaC != 14)
                {
                    RowId = -1;
                    currentPage = 0;
                    MenuId = 0;
                }
            }
        }

        public override void paint(Graphics dcGs)
        {
            //GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < 5; index++)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 2:
                    if (btnCanDown[2])
                    {
                        buttonIsDown[2] = true;
                    }
                    break;
                case 3:
                    if (MenuId == 2)
                    {
                        buttonIsDown[3] = true;
                    }
                    else
                    {
                        if (btnCanDown[3])
                        {
                            buttonIsDown[3] = true;
                        }
                    }
                    break;
                case 4:
                    if (MenuId == 2)
                    {
                        buttonIsDown[4] = true;
                    }
                    else
                    {
                        if (btnCanDown[4])
                        {
                            buttonIsDown[4] = true;

                        }
                    }
                    break;
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < 5; index++)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    if (btnCanDown[0])
                    {
                        ChangeButtonState(0);
                        MenuId = 0;
                        RowId = -1;
                        currentPage = 0;
                    }
                    break;
                case 1:
                    if (btnCanDown[1])
                    {
                        ChangeButtonState(1);
                        MenuId = 1;
                        RowId = -1;
                        currentPage = 0;
                    }
                    break;
                case 2:
                    if (btnCanDown[2])
                    {
                        buttonIsDown[2] = false;
                        MenuId = 2;
                    }
                    break;
                case 3:
                    if (MenuId == 2)
                    {
                        buttonIsDown[3] = false;
                    }
                    else
                    {
                        if (btnCanDown[3])
                        {
                            buttonIsDown[3] = false;
                            if (currentPage > 0)
                                currentPage--;
                        }
                    }
                    break;
                case 4:
                    if (MenuId == 2)
                    {
                        buttonIsDown[4] = false;
                    }
                    else
                    {
                        if (btnCanDown[4])
                        {
                            buttonIsDown[4] = false;
                            if (currentPage < defPage)
                                currentPage++;
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
                buttonIsDown[i] = false;
            }
            buttonIsDown[buttonNumb] = true;
        }

        int noOverDefNumb = 0;
        int allDefNumb = 0;
        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            noOverDefNumb = Title.MsgInf.CurrentMsgList.Count;
            allDefNumb = Title.MsgInf.AllMsgsList.Count;
            if (MenuId == 0)
            {
                defPage = Convert.ToInt32(noOverDefNumb / 10);
                DrawCurrentDef(e);
            }
            else if (MenuId == 1)
            {
                defPage = Convert.ToInt32(allDefNumb / 10);
                DrawAllDef(e);
            }
            DrawFrame(e);
        }

        /// <summary>
        /// 框架
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrame(Graphics e)
        {            
            for (int i = 0; i < 5; i++)
            {
                //等级 代码 故障内容 时间
                e.FillRectangle(FormatStyle.DarkGreyBrush, rects[i]);
                e.DrawString(FormatStyle.Str12[i], FormatStyle.Font14B,
                   FormatStyle.WhiteBrush, rects[i], drawFormat);
            }
            //
            e.DrawLine(FormatStyle.DarkGreyPen, pDrawPoint[0], pDrawPoint[1]);

            //
            for (int i = 0; i < 10; i++)
            {
                e.DrawString((currentPage * 10 + i + 1).ToString(), FormatStyle.Font12B,
                    FormatStyle.WhiteBrush, rects[5 + i * 5], drawFormat);
            }

            for (int i = 0; i < 2; i++)
            {
                if (buttonIsDown[i])
                    e.DrawImage(Img[0], rects[75 + i]);
                else
                    e.DrawImage(Img[3], rects[75 + i]);

                e.DrawString(FormatStyle.Str13[i], FormatStyle.Font12B,
                    FormatStyle.BlackBrush, rects[75 + i], drawFormat);
            }
            if (buttonIsDown[3])
                e.DrawImage(Img[1], rects[78]);
            else
                e.DrawImage(Img[4], rects[78]);

            if (buttonIsDown[4])
                e.DrawImage(Img[2], rects[79]);
            else
                e.DrawImage(Img[5], rects[79]);
        }

        /// <summary>
        /// 当前故障
        /// </summary>
        /// <param name="e"></param>
        private void DrawCurrentDef(Graphics e)
        {
            if (noOverDefNumb <= 0)
                return;
            else
            {
                for (int i = noOverDefNumb - 1, j = 0; i >= 0; i--, j++)
                {
                    if ((noOverDefNumb - 1 - i) >= (currentPage * 10) && (noOverDefNumb - 1 - i) < (currentPage + 1) * 10)
                    {
                        int tmpRowIndex = j % 10;
                        e.DrawString(((int)Title.MsgInf.CurrentMsgList[i].TheMsgLevel).ToString(), FormatStyle.Font12B,
                            FormatStyle.WhiteBrush, rects[6 + tmpRowIndex * 5], drawFormat);
                        e.DrawString(Title.MsgInf.CurrentMsgList[i].MsgID, FormatStyle.Font12B,
                            FormatStyle.WhiteBrush, rects[7 + tmpRowIndex * 5], drawFormat);
                        e.DrawString(Title.MsgInf.CurrentMsgList[i].MsgContent, FormatStyle.Font12B,
                            FormatStyle.WhiteBrush, rects[8 + tmpRowIndex * 5], drawFormat);
                        e.DrawString(Title.MsgInf.CurrentMsgList[i].MsgReceiveTime.ToString("yyyy-MM-dd hh:mm:ss"), FormatStyle.Font12B,
                            FormatStyle.WhiteBrush, rects[55 + tmpRowIndex * 2], drawFormat);
                    }
                }
            }
        }

        /// <summary>
        /// 所有故障
        /// </summary>
        /// <param name="e"></param>
        private void DrawAllDef(Graphics e)
        {
            if (allDefNumb <= 0)
                return;
            else
            {
                for (int i = allDefNumb - 1, j = 0; i >= 0; i--, j++)
                {
                    if ((allDefNumb - 1 - i) >= (currentPage * 10) && (allDefNumb - 1 - i) < (currentPage + 1) * 10)
                    {
                        int tmpRowIndex = j % 10;
                        e.DrawString(((int)Title.MsgInf.AllMsgsList[i].TheMsgLevel).ToString(), FormatStyle.Font12B,
                            FormatStyle.WhiteBrush, rects[6 + tmpRowIndex * 5], drawFormat);
                        e.DrawString(Title.MsgInf.AllMsgsList[i].MsgID, FormatStyle.Font12B,
                            FormatStyle.WhiteBrush, rects[7 + tmpRowIndex * 5], drawFormat);
                        e.DrawString(Title.MsgInf.AllMsgsList[i].MsgContent, FormatStyle.Font12B,
                            FormatStyle.WhiteBrush, rects[8 + tmpRowIndex * 5], drawFormat);
                        e.DrawString(Title.MsgInf.AllMsgsList[i].MsgReceiveTime.ToString("yyyy-MM-dd hh:mm:ss"), FormatStyle.Font12B,
                            FormatStyle.WhiteBrush, rects[55 + tmpRowIndex * 2], drawFormat);
                        if (Title.MsgInf.AllMsgsList[i].FaultBeOver)
                        {
                            e.DrawString(Title.MsgInf.AllMsgsList[i].MsgOverTime.ToString("yyyy-MM-dd hh:mm:ss"), FormatStyle.Font12B,
                                FormatStyle.WhiteBrush, rects[56 + tmpRowIndex * 2], drawFormat);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 故障解决方法
        /// </summary>
        /// <param name="e"></param>
        private void DefSolution(Graphics e)
        {

        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;

            RightFormat.LineAlignment = StringAlignment.Far;
            RightFormat.Alignment = StringAlignment.Center;

            LeftFormat.LineAlignment = StringAlignment.Near;
            LeftFormat.Alignment = StringAlignment.Center;

            pDrawPoint = new PointF[20];

            rects = new RectangleF[200];

            Img = new Image[10];

            buttonIsDown = new bool[10];

            btnCanDown = new bool[10];
            for (int i = 0; i < btnCanDown.Length; i++)
            {
                btnCanDown[i] = true;
            }

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region ::::::::::::::::::::: rects :::::::::::::::::::::::::::::::
            for (int i = 0; i < 11; i++)
            {
                rects[0 + i * 5] = new Rectangle(5, 105 + 40 * i, 50, 40);
                rects[1 + i * 5] = new Rectangle(57, 105 + 40 * i, 50, 40);
                rects[2 + i * 5] = new Rectangle(109, 105 + 40 * i, 75, 40);
                rects[3 + i * 5] = new Rectangle(186, 105 + 40 * i, 340, 40);
                rects[4 + i * 5] = new Rectangle(528, 105 + 40 * i, 170, 40);
            }
            for (int i = 0; i < 10; i++)
            {
                rects[55 + i * 2] = new RectangleF(528, 145 + 40 * i, 170, 20);
                rects[56 + i * 2] = new RectangleF(528, 165 + 40 * i, 170, 20);
            }
            for (int i = 0; i < 5; i++)
            {
                rects[75 + i] = new Rectangle(730, 150 + 70 * i, 60, 52);
            }

            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::: pDrawPoint :::::::::::::::::::::::::::
            pDrawPoint[0] = new Point(710, 95);
            pDrawPoint[1] = new Point(710, 549);

            //MoveScreen
            for (int i = 0; i < pDrawPoint.Length; i++)
            {
                pDrawPoint[i].X = (pDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                pDrawPoint[i].Y = (pDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region ::::::::::::::::::::: Region ::::::::::::::::::::::::
            for (int i = 0; i < 5; i++)
            {
                Rect.Add(new Region(rects[75 + i]));
            }
            #endregion

            #region ::::::::::::::::::::: isButtonDown ::::::::::::::::::::
            buttonIsDown[0] = true;
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();
        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::
        /// <summary>
        /// 坐标集
        /// </summary>
        internal PointF[] pDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        internal RectangleF[] rects;

        /// <summary>
        /// 图片集
        /// </summary>
        internal Image[] Img;

        /// <summary>
        /// 按键
        /// </summary>
        internal bool[] buttonIsDown;

        /// <summary>
        /// 键是否能按下
        /// </summary>
        internal bool[] btnCanDown;

        /// <summary>
        /// 故障总页码
        /// </summary>
        internal int defPage;

        /// <summary>
        /// 当前故障页码
        /// </summary>
        internal int currentPage = 0;

        /// <summary>
        /// 所在行号
        /// </summary>
        internal int RowId = -1;

        /// <summary>
        /// 0为当前故障
        /// 1为历史故障
        /// 2为故障提示
        /// </summary>
        internal int MenuId = 0;

        /// <summary>
        /// 按键列表
        /// </summary>
        internal List<Region> Rect;
        #endregion#
        #endregion
    }
}

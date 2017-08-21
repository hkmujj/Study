using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace KumM_TMS.课程开始结束亮度
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Lightness : baseClass
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
                    isDraw = true;
                else
                    isDraw = false;
            }
        }

        public override void paint(Graphics dcGs)
        {
            if (isDraw)
            {
                DrawOn(dcGs);
            }
            base.paint(dcGs);
        }
        public override bool mouseDown(Point nPoint)
        {
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            while (index < 2)
            {
                if (Rect[index].IsVisible(nPoint))
                {
                    break;
                }
                index++;
            }
            switch (index)
            {
                case 0:
                    if (lightNum > 0)
                        lightNum--;
                    break;
                case 1:
                    if (lightNum < 5)
                        lightNum++;
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
            e.DrawLine(FormatStyle.MediumGreyPen, pDrawPoint[0], pDrawPoint[1]);

            for (int i = 0; i < 2; i++)
            {
                e.DrawImage(Img[i + 1], rects[i]);
            }

            for (int i = 0; i < 5; i++)
            {
                e.DrawImage((i + 1) > lightNum ? Img[3] : Img[4], rects[7 + i]);

                e.DrawImage(Img[0], rects[2 + i]);
            }

            e.FillRectangle(BlackBrush[lightNum], rects[12]);
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

            rects = new RectangleF[50];

            Img = new Image[20];

            buttonIsDown = new bool[10];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::::::: rects ::::::::::::::::::::::::::::::::::::
            //暗/亮
            for (int i = 0; i < 2; i++)
            {
                rects[i] = new Rectangle(170 + i * 305, 260, 70, 70);
            }
            //右侧按钮
            for (int i = 0; i < 5; i++)
            {
                rects[2 + i] = new Rectangle(728, 115 + i * 85, 60, 52);
            }
            //亮度条
            for (int i = 0; i < 5; i++)
            {
                rects[7 + i] = new Rectangle(250 + i * 45, 260, 35, 70);
            }
            rects[12] = new Rectangle(0, 0, 800, 600);


            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::::::: point ::::::::::::::::::::::::::::
            pDrawPoint[0] = new Point(720, 95);
            pDrawPoint[1] = new Point(720, 550);

            //MoveScreen
            for (int i = 0; i < pDrawPoint.Length; i++)
            {
                pDrawPoint[i].X = (pDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                pDrawPoint[i].Y = (pDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region ::::::::::::::::::::::::::::::::::: Rect :::::::::::::::::::::::::::::::::::::
            for (int i = 0; i < 7; i++)
            {
                Rect.Add(new Region(rects[i]));
            }
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        /// <summary>
        /// 是否在亮度调节画面
        /// </summary>
        public bool isDraw = false;

        /// <summary>
        /// 绿色的键值
        /// </summary>
        public int lightNum = 5;

        public static SolidBrush[] BlackBrush = new SolidBrush[6] {
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
        /// 键是否按下
        /// </summary>
        internal bool[] buttonIsDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        internal List<Region> Rect;
        #endregion#
        #endregion
    }

    [GksDataType(DataType.isMMIObjectClass)]
    public class TMS_Black : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "TMS黑屏";
        }



        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[0], 1, 0);
            append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[1], 1, 0);

            return true;
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics dcGs)
        {
            GetValue();
            //dcGs.FillRectangle(new SolidBrush(Color.FromArgb(0, 200, 0)), new Rectangle(100, 200, 300, 300));
            //dcGs.DrawString("牵引变流器", new Font("黑体", 20, FontStyle.Bold), new SolidBrush(Color.FromArgb(0, 94, 0)), new Point(199, 299));
            //dcGs.DrawString("牵引变流器", new Font("黑体", 20, FontStyle.Bold), new SolidBrush(Color.FromArgb(101, 241, 101)), new Point(202, 302));
            //dcGs.DrawString("牵引变流器", new Font("黑体", 20, FontStyle.Bold), new SolidBrush(Color.FromArgb(0, 129, 0)), new Point(201, 301));
            //dcGs.DrawString("牵引变流器", new Font("黑体", 20), new SolidBrush(Color.Black), new Point(201, 339));

            base.paint(dcGs);

        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void GetValue()
        {
            if (BoolList[UIObj.InBoolList[0]])
                append_postCmd(CmdType.ChangePage, 41, 0, 0);
            if (BoolList[UIObj.InBoolList[1]])
            {
                append_postCmd(CmdType.ChangePage, 11, 0, 0);
            }

        }
        #endregion

    }

    [GksDataType(DataType.isMMIObjectClass)]
    public class TMS_Classover : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "TMS课程结束视图";
        }



        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 1)
            {
                _msgClear = false;
            }
            else if (nParaA == 2)
            {
                _msgClear = true;
            }
        }
        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics dcGs)
        {
            GetValue();
            //dcGs.FillRectangle(new SolidBrush(Color.FromArgb(0, 200, 0)), new Rectangle(100, 200, 300, 300));
            //dcGs.DrawString("牵引变流器", new Font("黑体", 20, FontStyle.Bold), new SolidBrush(Color.FromArgb(0, 94, 0)), new Point(199, 299));
            //dcGs.DrawString("牵引变流器", new Font("黑体", 20, FontStyle.Bold), new SolidBrush(Color.FromArgb(101, 241, 101)), new Point(202, 302));
            //dcGs.DrawString("牵引变流器", new Font("黑体", 20, FontStyle.Bold), new SolidBrush(Color.FromArgb(0, 129, 0)), new Point(201, 301));
            //dcGs.DrawString("牵引变流器", new Font("黑体", 20), new SolidBrush(Color.Black), new Point(201, 339));

            base.paint(dcGs);

        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void GetValue()
        {
            if (_msgClear)
            {
                DMITitle.Title.MsgInf.ClearAllList();
                for (int i = 0; i < 1600; i++)
                {
                    //清空发送bool数据
                    append_postCmd(CmdType.SetBoolValue, 1600 + i, 0, 0);
                }
                for (int i = 0; i < 100; i++)
                {
                    //清空发送float数据
                    append_postCmd(CmdType.SetFloatValue, 200 + i, 0, 0);
                }
            }
        }
        #endregion

        private bool _msgClear = false;
    }
}

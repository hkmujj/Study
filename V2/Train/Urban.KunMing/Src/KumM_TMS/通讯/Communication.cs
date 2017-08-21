using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace KumM_TMS.通讯
{
    /// <summary>
    /// 通讯状态
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Communication : baseClass
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
            for (int i = 0; i < 51; i++)
            {
                e.DrawLine(FormatStyle.WhitePen, pDrawPoint[i], pDrawPoint[100 + i]);
            }

            for (int i = 0; i < 62; i++)
            {
                e.FillRectangle(FormatStyle.MediumGreySolidBrush, rects[i]);
            }
        }

        private void DrawValue(Graphics e)
        {
            //1 VCM
            DrawFunValue(e, 890, 2, 70);
            //2 REP
            DrawFunValue(e, 896, 6, 72);
            //3 DXM1 -- FDS
            DrawFunValue(e, 914, 6, 78);
            //3 HMI -- BCU
            DrawFunValue(e, 932, 6, 84);
            //4 HMI -- BCU
            DrawFunValue(e, 1037, 6, 90);
            //4 DXM1 -- FDS
            DrawFunValue(e, 1019, 6, 96);
            //5 DXM1 -- HAVC2
            DrawFunValue(e, 968, 6, 102);
            //6 DXM1 -- HAVC2
            DrawFunValue(e, 986, 6, 108);
            //7
            DrawFunValue(e, 1061, 4, 133);
            DrawFunValue(e, 1079, 4, 139);
            //9
            DrawFunValue(e, 1058, 1, 132);
            DrawFunValue(e, 1073, 1, 137);
            DrawFunValue(e, 1076, 1, 138);
            DrawFunValue(e, 1091, 1, 143);
            //10 SIV
            DrawFunValue(e, 950, 1, 120);
            DrawFunValue(e, 1055, 1, 121);
            //11 DXM1 -- HAVC2
            DrawFunValue(e, 953, 5, 122);
            //12 DXM1 -- HAVC2
            DrawFunValue(e, 1004, 5, 127);

            //
            for (int i = 0; i < 62; i++)
            {
                e.DrawRectangle(FormatStyle.BlackPen, rects[70 + i].X, rects[70 + i].Y, rects[70 + i].Width, rects[70 + i].Height);
                e.DrawString(FormatStyle.Str11[i], FormatStyle.Font12B,
                    FormatStyle.BlackBrush, rects[70 + i], drawFormat);
            }
            for (int i = 0; i < 6; i++)
            {
                e.DrawString("1", FormatStyle.Font12B, FormatStyle.BlackBrush,
                    rects[132 + i], drawFormat);
                e.DrawString("2", FormatStyle.Font12B, FormatStyle.BlackBrush,
                    rects[138 + i], drawFormat);
                e.DrawString("EDCU", FormatStyle.Font12B, FormatStyle.WhiteBrush,
                    rects[144 + i], drawFormat);
            }
        }

        /// <summary>
        /// 状态填入
        /// </summary>
        /// <param name="e">绘图参数</param>
        /// <param name="initBool">初始位在boollist的位置</param>
        /// <param name="max">循环最大值</param>
        /// <param name="initRects">rects位置</param>
        private void DrawFunValue(Graphics e, int initBool, int max, int initRects)
        {
            for (int i = 0; i < max; i++)
            {
                if (BoolList[initBool + i * 3])
                    e.FillRectangle(FormatStyle.LightGreenBrush, rects[initRects + i]);
                else if (BoolList[initBool + 1 + i * 3])
                    e.FillRectangle(FormatStyle.RedBrush, rects[initRects + i]);
            }
        }

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
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;

            RightFormat.LineAlignment = StringAlignment.Far;
            RightFormat.Alignment = StringAlignment.Center;

            LeftFormat.LineAlignment = StringAlignment.Near;
            LeftFormat.Alignment = StringAlignment.Center;

            bValue = new bool[20];
            oldbValue = new bool[20];
            setBValue = new bool[10];
            setBValueNumb = new int[10];

            fValue = new float[10];
            oldfValue = new float[10];
            setFValue = new float[5];
            setFValueNumb = new int[5];

            pDrawPoint = new PointF[200];

            rects = new RectangleF[200];

            Img = new Image[10];

            buttonIsDown = new bool[10];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::   rects    :::::::::::::::::::
            for (int i = 0; i < 2; i++)
            {
                //1
                //VCM
                rects[i] = new Rectangle(65 + i * 612, 110, 60, 30);
            }
            for (int i = 0; i < 6; i++)
            {
                //2
                rects[2 + i] = new Rectangle(141 + i * 92, 155, 60, 30);
                //3
                rects[8 + i] = new Rectangle(20, 195 + i * 35, 60, 30);
                rects[14 + i] = new Rectangle(110, 195 + i * 35, 60, 30);
                //4
                rects[20 + i] = new Rectangle(632, 195 + i * 35, 60, 30);
                rects[26 + i] = new Rectangle(722, 195 + i * 35, 60, 30);
                //5
                rects[32 + i] = new Rectangle(280, 195 + i * 35, 60, 30);
                //6
                rects[38 + i] = new Rectangle(462, 195 + i * 35, 60, 30);
            }
            for (int i = 0; i < 4; i++)
            {
                //7
                rects[44 + i] = new Rectangle(223 + i * 92, 465, 80, 50);
            }
            for (int i = 0; i < 2; i++)
            {
                //9
                rects[48 + i] = new Rectangle(55 + i * 612, 465, 80, 50);
                //10
                rects[50 + i] = new Rectangle(110 + i * 522, 405, 60, 30);
            }
            for (int i = 0; i < 5; i++)
            {
                //11
                rects[52 + i] = new Rectangle(188, 195 + i * 35, 60, 30);
                //12
                rects[57 + i] = new Rectangle(556, 195 + i * 35, 60, 30);
            }

            ///////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////
            for (int i = 0; i < 2; i++)
            {
                //1
                rects[i + 70] = new Rectangle(66 + i * 612, 111, 57, 27);
            }
            for (int i = 0; i < 6; i++)
            {
                //2
                rects[2 + i + 70] = new Rectangle(142 + i * 92, 156, 57, 27);
                //3
                rects[8 + i + 70] = new Rectangle(21, 196 + i * 35, 57, 27);
                rects[14 + i + 70] = new Rectangle(111, 196 + i * 35, 57, 27);
                //4
                rects[20 + i + 70] = new Rectangle(633, 196 + i * 35, 57, 27);
                rects[26 + i + 70] = new Rectangle(723, 196 + i * 35, 57, 27);
                //5
                rects[32 + i + 70] = new Rectangle(281, 196 + i * 35, 57, 27);
                //6
                rects[38 + i + 70] = new Rectangle(463, 196 + i * 35, 57, 27);
            }
            for (int i = 0; i < 4; i++)
            {
                //7
                rects[44 + i + 70] = new Rectangle(224 + i * 92, 466, 77, 47);
            }
            for (int i = 0; i < 2; i++)
            {
                //9
                rects[48 + i + 70] = new Rectangle(56 + i * 612, 466, 77, 47);
                //10
                rects[50 + i + 70] = new Rectangle(111 + i * 522, 406, 57, 27);
            }
            for (int i = 0; i < 5; i++)
            {
                //11
                rects[52 + i + 70] = new Rectangle(189, 196 + i * 35, 57, 27);
                //12
                rects[57 + i + 70] = new Rectangle(557, 196 + i * 35, 57, 27);
            }

            ////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////
            //1 1 1 1 1 1
            rects[132] = new Rectangle(56, 466, 77, 22);
            for (int i = 0; i < 4; i++)
            {
                rects[133 + i] = new Rectangle(224 + i * 92, 466, 77, 22);

            }
            rects[137] = new Rectangle(668, 466, 77, 22);

            //2 2 2 2 2 2
            rects[138] = new Rectangle(56, 491, 77, 22);
            for (int i = 0; i < 4; i++)
            {
                rects[139 + i] = new Rectangle(224 + i * 92, 491, 77, 22);

            }
            rects[143] = new Rectangle(668, 491, 77, 22);

            //EDCU
            rects[144] = new Rectangle(56, 513, 77, 20);
            for (int i = 0; i < 4; i++)
            {
                rects[145 + i] = new Rectangle(224 + i * 92, 513, 77, 20);
            }
            rects[149] = new Rectangle(668, 513, 77, 20);

            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region ::::::::::::::::::::::: pDrawPoint :::::::::::::::::::
            //1
            pDrawPoint[0] = new Point(125, 125);
            pDrawPoint[100] = new Point(677, 125);

            for (int i = 0; i < 6; i++)
            {
                //2
                pDrawPoint[1 + i] = new Point(171 + i * 92, 125);
                pDrawPoint[101 + i] = new Point(171 + i * 92, 155);

                //3
                pDrawPoint[7 + i] = new Point(80, 210 + i * 35);
                pDrawPoint[107 + i] = new Point(110, 210 + i * 35);

                //4
                pDrawPoint[13 + i] = new Point(692, 210 + i * 35);
                pDrawPoint[113 + i] = new Point(722, 210 + i * 35);

                //5
                pDrawPoint[19 + i] = new Point(340, 210 + i * 35);
                pDrawPoint[119 + i] = new Point(355, 210 + i * 35);

                //6
                pDrawPoint[25 + i] = new Point(447, 210 + i * 35);
                pDrawPoint[125 + i] = new Point(462, 210 + i * 35);
            }

            for (int i = 0; i < 4; i++)
            {
                //7
                pDrawPoint[31 + i] = new Point(263 + i * 92, 185);
                pDrawPoint[131 + i] = new Point(263 + i * 92, 465);
            }

            for (int i = 0; i < 2; i++)
            {
                //8
                pDrawPoint[35 + i] = new Point(95 + i * 566, 160);
                pDrawPoint[135 + i] = new Point(141 + i * 566, 160);

                //9
                pDrawPoint[37 + i] = new Point(95 + i * 612, 160);
                pDrawPoint[137 + i] = new Point(95 + i * 612, 465);

                //10
                pDrawPoint[39 + i] = new Point(95 + i * 597, 420);
                pDrawPoint[139 + i] = new Point(110 + i * 597, 420);
            }

            for (int i = 0; i < 5; i++)
            {
                //11
                pDrawPoint[41 + i] = new Point(248, 210 + i * 35);
                pDrawPoint[141 + i] = new Point(263, 210 + i * 35);

                //12
                pDrawPoint[46 + i] = new Point(539, 210 + i * 35);
                pDrawPoint[146 + i] = new Point(556, 210 + i * 35);
            }

            //MoveScreen
            for (int i = 0; i < pDrawPoint.Length; i++)
            {
                pDrawPoint[i].X = (pDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                pDrawPoint[i].Y = (pDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
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
                bValue[i] = BoolList[UIObj.InBoolList[i]];
                oldbValue[i] = BoolOldList[UIObj.InBoolList[i]];
            }
            //发送bool数据
            for (int i = 0; i < UIObj.OutBoolList.Count; i++)
            {
                setBValue[i] = BoolList[UIObj.OutBoolList[i]];
                setBValueNumb[i] = UIObj.OutBoolList[i];
            }
            //接收float数据
            for (int i = 0; i < UIObj.InFloatList.Count; i++)
            {
                fValue[i] = FloatList[UIObj.InFloatList[i]];
                oldfValue[i] = FloatOldList[UIObj.InFloatList[i]];
            }
            //发送float数据
            for (int i = 0; i < UIObj.OutFloatList.Count; i++)
            {
                setFValue[i] = FloatList[UIObj.OutFloatList[i]];
                setFValueNumb[i] = UIObj.OutFloatList[i];
            }
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::
        /// <summary>
        /// 当前周期接收数字量
        /// </summary>
        internal bool[] bValue;

        /// <summary>
        /// 前一个周期接收的数字量
        /// </summary>
        internal bool[] oldbValue;

        /// <summary>
        /// 发送的数字量
        /// </summary>
        internal bool[] setBValue;

        /// <summary>
        /// 发送的数字量在boollist中的序号
        /// </summary>
        internal int[] setBValueNumb;

        /// <summary>
        /// 接收模拟量
        /// </summary>
        internal float[] fValue;

        /// <summary>
        /// 前一个周期接收的模拟量
        /// </summary>
        internal float[] oldfValue;

        /// <summary>
        /// 发送的模拟量
        /// </summary>
        internal float[] setFValue;

        /// <summary>
        /// 发送的模拟量在floatlist中的序号
        /// </summary>
        internal int[] setFValueNumb;

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

    /// <summary>
    /// 通讯状态帮助
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class CommunicationHelp : baseClass
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
            e.DrawImage(Img[0], rects[0]);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            pDrawPoint = new PointF[5];

            rects = new RectangleF[5];

            Img = new Image[10];

            buttonIsDown = new bool[10];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            rects[0] = new RectangleF(0, 100, 798, 450);

            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
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
}

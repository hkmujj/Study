using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.PublicUI;
using MMI.Facility.Interface.Data;

namespace DialScreen
{
    /// <summary>
    /// 表盘
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class DialScreen : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "表盘";
        }
        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::


        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }


        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics g)
        {
            GetValue();

            DrawOn(g);
        }

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            if (index >= 0 && index < 13)
            {
                buttonIsDown[index] = true;
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    buttonIsDown[0] = false;
                    break;
                case 1:
                    buttonIsDown[1] = false;
                    break;
                case 2:
                    buttonIsDown[2] = false;
                    break;
                case 3:
                    buttonIsDown[3] = false;
                    break;
                case 4:
                    buttonIsDown[4] = false;
                    break;
                case 5:
                    buttonIsDown[5] = false;
                    break;
                case 6:
                    buttonIsDown[6] = false;
                    break;
                case 7:
                    buttonIsDown[7] = false;
                    break;
                case 8:
                    buttonIsDown[8] = false;
                    break;
                case 9:
                    buttonIsDown[9] = false;
                    break;
                case 10:
                    buttonIsDown[10] = false;
                    break;
                case 11:
                    buttonIsDown[11] = false;
                    break;
                case 12:
                    buttonIsDown[12] = false;
                    break;
                default:
                    return false;
            }
            return true;
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        /// <summary>
        /// 循环数据
        /// </summary>
        private void GetValue()
        {
            //接收float数据
            for (int i = 0; i < UIObj.InFloatList.Count; i++)
            {
                fValue[i] = FloatList[UIObj.InFloatList[i]];
                oldfValue[i] = FloatOldList[UIObj.InFloatList[i]];
            }

            //test1 = Test(test1, 0.5f, 120f);
            //test2 = Test(test2, 35f, 12000f);
            //test3 = Test(test3, 25f, 12000f);
        }

        float test1 = 0;
        float test2 = 0;
        float test3 = 0;
        private float Test(float test, float scale, float MaxNumb)
        {
            test += scale;
            if (test >= MaxNumb) test = 0;
            return test;
        }

        private void DrawOn(Graphics g)
        {
            g.FillRectangle(FormatStyle.BlackSolidBrush, rects[0]);
            for (int i = 0; i < 2; i++)
            {
                g.DrawImage(Img[i], rects[i + 1]);
            }

            //e.DrawString(test1.ToString(), FormatStyle.Font12B, FormatStyle.WhiteSolidBrush,
            //    new PointF(20, 180));
            //e.DrawString(test2.ToString(), FormatStyle.Font12B, FormatStyle.WhiteSolidBrush,
            //    new PointF(20, 500));
            //e.DrawString(test3.ToString(), FormatStyle.Font12B, FormatStyle.WhiteSolidBrush,
            //    new PointF(20, 700));
            speedPointer.PaintPointer(g, Img[2], ref fValue[0]);
            airPressure1.PaintPointer(g, Img[3], ref fValue[1]);
            airPressure2.PaintPointer(g, Img[4], ref fValue[2]);
        }

        private void AddinPointAndSizes()
        {
            float tmp;
            foreach (string str in UIObj.ParaList)
            {
                if (float.TryParse(str, out tmp))
                {
                    DialPoint_Sizes.Add(tmp);
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
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;

            RightFormat.LineAlignment = StringAlignment.Far;
            RightFormat.Alignment = StringAlignment.Center;

            LeftFormat.LineAlignment = StringAlignment.Near;
            LeftFormat.Alignment = StringAlignment.Center;

            fValue = new float[5];
            oldfValue = new float[5];

            pDrawPoint = new PointF[20];

            rects = new RectangleF[120];

            Img = new Image[15];

            buttonIsDown = new bool[15];

            Rect = new List<Region>();

            for (int i = 0; i < 5; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            DialPoint_Sizes = new List<float>();

            AddinPointAndSizes();
            #region :::::::::::::::::::::::: point ::::::::::::::::::::::
            pDrawPoint[0] = new PointF(DialPoint_Sizes[0], DialPoint_Sizes[1]);
            pDrawPoint[1] = new PointF(DialPoint_Sizes[2], DialPoint_Sizes[3]);

            pDrawPoint[2] = new PointF(DialPoint_Sizes[4], DialPoint_Sizes[5]);
            pDrawPoint[3] = new PointF(DialPoint_Sizes[6], DialPoint_Sizes[7]);



            #endregion

            #region :::::::::::::::::::::::: rects ::::::::::::::::::::::
            rects[0] = new RectangleF(0, 0, 480, 854);

            //速度表
            rects[1] = new RectangleF(pDrawPoint[0], new Size(Img[0].Width, Img[0].Height));
            //气表
            rects[2] = new RectangleF(pDrawPoint[2], new Size(Img[0].Width, Img[0].Height));

            #endregion


            #region :::::::::::::::::::::::: Rect :::::::::::::::::::::::
            for (int i = 0; i < 13; i++)
            {
                Rect.Add(new Region(rects[1 + i]));
            }
            #endregion

            speedPointer = new SpeedPointer(246.4f, -33.2f, 120f, pDrawPoint[0], pDrawPoint[1]);
            airPressure1 = new SpeedPointer(270f, -45f, 1200f, pDrawPoint[2], pDrawPoint[3]);
            airPressure2 = new SpeedPointer(270f, -45f, 1200f, pDrawPoint[2], pDrawPoint[3]);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        public String[] str1 = new String[9] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public String[] str2 = new String[2] { "确定", "取消" };

        public SpeedPointer speedPointer;

        public SpeedPointer airPressure1;

        public SpeedPointer airPressure2;
        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::
        /// <summary>
        /// 接收模拟量
        /// </summary>
        internal float[] fValue;

        /// <summary>
        /// 前一个周期接收的模拟量
        /// </summary>
        internal float[] oldfValue;

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

        /// <summary>
        /// 表盘坐标点以及尺寸大小
        /// </summary>
        private List<float> DialPoint_Sizes;
        #endregion#
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.DataType;

using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using System.Drawing.Drawing2D;

namespace KumM_MMI
{
    /// <summary>
    /// 字体画笔线条格式
    /// </summary>
    public class FormatStyle
    {
        public static int menu = 0;
        public const int Center = 2;
        public const String strFont = "Arial";

        //线条
        //白色
        public static Pen WhitePen = new Pen(Color.FromArgb(255, 255, 255), 1);
        public static Pen WhitePen2 = new Pen(Color.FromArgb(255, 255, 255), 2);
        public static Pen WhitePen3 = new Pen(Color.FromArgb(255, 255, 255), 3);
        public static Pen WhitePen4 = new Pen(Color.FromArgb(255, 255, 255), 4);
        //黑色
        public static Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0), 1);
        public static Pen BlackPen2 = new Pen(Color.FromArgb(0, 0, 0), 2);
        //浅灰、中灰、深灰
        public static Pen LightGreyPen = new Pen(Color.FromArgb(195, 195, 195));
        public static Pen MediumGreyPen = new Pen(Color.FromArgb(150, 150, 150));
        public static Pen DarkGreyPen = new Pen(Color.FromArgb(85, 85, 85));
        //蓝色、深蓝
        public static Pen BluePen = new Pen(Color.FromArgb(3, 17, 34));
        public static Pen VeryDarkBluePen = new Pen(Color.FromArgb(4, 12, 25));
        //淡绿、深绿
        public static Pen LightGreenPen = new Pen(Color.FromArgb(45, 144, 51));
        public static Pen LightGreenPen_2 = new Pen(Color.FromArgb(45, 144, 51), 2);
        public static Pen DarkGreenPen = new Pen(Color.FromArgb(12, 58, 12));
        //黄色、深黄色、橙色
        public static Pen YellowPen = new Pen(Color.FromArgb(223, 223, 0), 2);
        public static Pen DarkYellowPen = new Pen(Color.FromArgb(117, 105, 0));
        public static Pen OrangePen = new Pen(Color.FromArgb(234, 145, 0));
        //红色
        public static Pen RedPen = new Pen(Color.FromArgb(191, 0, 2));
        public static Pen RedPen_2 = new Pen(Color.FromArgb(191, 0, 2), 2);
        //灰蓝色、蓝灰色、淡蓝灰色
        public static Pen PurplePen = new Pen(Color.FromArgb(255, 0, 255), 1);
        public static Pen Gary_BluePen = new Pen(Color.FromArgb(81, 91, 109));
        public static Pen Blue_GrayPen = new Pen(Color.FromArgb(37, 69, 93));
        public static Pen LightBlue_GrayPen = new Pen(Color.FromArgb(128, 139, 158));

        //画笔
        //白色
        public static SolidBrush WhiteSolidBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        //黑色
        public static SolidBrush BlackSolidBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        //浅灰、中灰、深灰
        public static SolidBrush LightGreySolidBrush = new SolidBrush(Color.FromArgb(195, 195, 195));
        public static SolidBrush MediumGreySolidBrush = new SolidBrush(Color.FromArgb(150, 150, 150));
        public static SolidBrush DarkGreySolidBrush = new SolidBrush(Color.FromArgb(85, 85, 85));
        //蓝色、深蓝
        public static SolidBrush BlueSolidBrush = new SolidBrush(Color.FromArgb(3, 17, 34));
        public static SolidBrush VeryDarkBlueSolidBrush = new SolidBrush(Color.FromArgb(4, 12, 25));
        //淡绿、深绿
        public static SolidBrush LightGreenSolidBrush = new SolidBrush(Color.FromArgb(45, 144, 51));
        public static SolidBrush DarkGreenSolidBrush = new SolidBrush(Color.FromArgb(12, 58, 12));
        //青色
        public static SolidBrush BluenessSolidBrush = new SolidBrush(Color.FromArgb(0, 194, 194));
        //黄色、深黄色、橙色
        public static SolidBrush YellowSolidBrush = new SolidBrush(Color.FromArgb(223, 223, 0));
        public static SolidBrush DarkYellowSolidBrush = new SolidBrush(Color.FromArgb(117, 105, 0));
        public static SolidBrush OrangeSolidBrush = new SolidBrush(Color.FromArgb(234, 145, 0));
        //红色
        public static SolidBrush RedSolidBrush = new SolidBrush(Color.FromArgb(191, 0, 2));
        //灰蓝色、蓝灰色、淡蓝灰色
        public static SolidBrush Gary_BlueSolidBrush = new SolidBrush(Color.FromArgb(81, 91, 109));
        public static SolidBrush Blue_GraySolidBrush = new SolidBrush(Color.FromArgb(37, 69, 93));
        public static SolidBrush LightBlue_GraySolidBrush = new SolidBrush(Color.FromArgb(128, 139, 158));

        #region :::::::::::::::::::::::::::::::: 字体 ::::::::::::::::::::::::::::::::::::::::::
        public static Font Font10 = new Font(strFont, 10f);
        public static Font Font12 = new Font(strFont, 12f);
        public static Font Font14 = new Font(strFont, 14f);
        public static Font Font16 = new Font(strFont, 16f);
        public static Font Font18 = new Font(strFont, 18f);
        public static Font Font20 = new Font(strFont, 20f);
        public static Font Font22 = new Font(strFont, 22f);
        public static Font Font24 = new Font(strFont, 24f);
        public static Font Font26 = new Font(strFont, 26f);
        public static Font Font32 = new Font(strFont, 32f);
        public static Font Font34 = new Font(strFont, 34f);
        public static Font Font38 = new Font(strFont, 38f);
        public static Font Font64 = new Font(strFont, 64f);

        public static Font Font8 = new Font(strFont, 8f, FontStyle.Bold);
        public static Font Font10B = new Font(strFont, 10f, FontStyle.Bold);
        public static Font Font12B = new Font(strFont, 12f, FontStyle.Bold);
        public static Font Font14B = new Font(strFont, 14f, FontStyle.Bold);
        public static Font Font16B = new Font(strFont, 16f, FontStyle.Bold);
        public static Font Font18B = new Font(strFont, 18f, FontStyle.Bold);
        public static Font Font20B = new Font(strFont, 20f, FontStyle.Bold);
        public static Font Font22B = new Font(strFont, 22f, FontStyle.Bold);
        public static Font Font24B = new Font(strFont, 24f, FontStyle.Bold);
        public static Font Font26B = new Font(strFont, 26f, FontStyle.Bold);
        public static Font Font32B = new Font(strFont, 32f, FontStyle.Bold);
        public static Font Font34B = new Font(strFont, 34f, FontStyle.Bold);
        public static Font Font38B = new Font(strFont, 38f, FontStyle.Bold);
        public static Font Font64B = new Font(strFont, 64f, FontStyle.Bold);
        #endregion
    }

    /// <summary>
    /// 表盘
    /// </summary>
    public class Dial
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="中心点"></param>
        /// <param name="刻度"></param>
        /// <param name="所有线段数"></param>
        /// <param name="每点代表的角度"></param>
        /// <param name="初始角度"></param>
        /// <param name="半径"></param>
        /// <param name="长刻度"></param>
        /// <param name="短刻度"></param>
        /// <param name="长短刻度比"></param>
        /// <param name="刻度宽短"></param>
        /// <param name="刻度宽长"></param>
        /// <param name="刻度字体颜色"></param>
        public Dial(PointF 中心点, string[] 刻度, int 所有线段数,
            double 每点代表的角度, double 初始角度, int 半径,
            int 长刻度, int 短刻度, int 长短刻度比,
            Pen 刻度宽短, Pen 刻度宽长, SolidBrush 刻度字体颜色)
        {
            drawFormat.Alignment = (StringAlignment)1;
            drawFormat.LineAlignment = (StringAlignment)1;

            point = 中心点;

            scaleStr = 刻度;
            rectKedu = new PointF[刻度.Length];

            pointKedu1 = new PointF[所有线段数];
            pointKedu2 = new PointF[所有线段数];
            numb = 所有线段数;

            minRadian = 每点代表的角度;

            initRadian = 初始角度;

            bili = 长短刻度比;

            penKeduShort = 刻度宽短;
            penKeduLong = 刻度宽长;

            textbrush = 刻度字体颜色;

            DialInit(半径, 长刻度, 短刻度);

        }

        /// <summary>
        /// 刻度算法
        /// </summary>
        private void DialInit(int 半径, int 长刻度, int 短刻度)
        {
            double angle;
            float sinAngle;
            float cosAngle;

            int j = 0;
            for (int i = 0; i < numb; i++)
            {
                angle = (i * minRadian + initRadian) * Math.PI / 180.0;
                sinAngle = (float)Math.Sin(angle);
                cosAngle = (float)Math.Cos(angle);
                if (i % bili == 0)
                {
                    pointKedu1[i].X = point.X + 半径 * scale * cosAngle;
                    pointKedu1[i].Y = point.Y + 半径 * scale * sinAngle;

                    pointKedu2[i].X = point.X + (半径 - 长刻度) * scale * cosAngle;
                    pointKedu2[i].Y = point.Y + (半径 - 长刻度) * scale * sinAngle;

                    rectKedu[j].X = point.X + (半径 - 长刻度 - 30) * scale * cosAngle;
                    rectKedu[j].Y = point.Y + (半径 - 长刻度 - 30) * scale * sinAngle;
                    j++;
                }
                else
                {
                    pointKedu1[i].X = point.X + 半径 * scale * cosAngle;
                    pointKedu1[i].Y = point.Y + 半径 * scale * sinAngle;

                    pointKedu2[i].X = point.X + (半径 - 短刻度) * scale * cosAngle;
                    pointKedu2[i].Y = point.Y + (半径 - 短刻度) * scale * sinAngle;
                }
            }
        }

        /// <summary>
        /// 表盘绘制
        /// </summary>
        /// <param name="e"></param>
        public void OnDraw(Graphics e, Font font)
        {
            e.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = 0; i < numb; i++)
            {
                if (i % bili == 0)
                {
                    e.DrawLine(penKeduLong, pointKedu1[i], pointKedu2[i]);
                }
                else
                {

                    e.DrawLine(penKeduShort, pointKedu1[i], pointKedu2[i]);
                }
            }
            for (int i = 0; i < scaleStr.Length; i++)
            {
                e.DrawString(scaleStr[i], font, textbrush, rectKedu[i], drawFormat);
            }

        }

        #region ::::::::::::::::::::::: init :::::::::::::::::::::::::::
        /// <summary>
        /// 放大缩小系数
        /// </summary>
        internal const float scale = 1.0f;

        /// <summary>
        /// 刻度点1
        /// </summary>
        public PointF[] pointKedu1;

        /// <summary>
        /// 刻度点2
        /// </summary>
        public PointF[] pointKedu2;

        /// <summary>
        /// 1点速度代表的弧度
        /// </summary>
        internal double minRadian;

        /// <summary>
        /// 初始弧度
        /// </summary>
        internal double initRadian;

        /// <summary>
        /// 表盘线段总数
        /// </summary>
        int numb = 0;

        /// <summary>
        /// 线段代表的数量
        /// </summary>
        private string[] scaleStr;

        /// <summary>
        /// 表盘上的
        /// </summary>
        public string numbstr;

        /// <summary>
        /// 表盘中心点
        /// </summary>
        public PointF point = new PointF();

        /// <summary>
        /// 刻度位置
        /// </summary>
        public PointF[] rectKedu;

        /// <summary>
        /// 刻度宽短
        /// </summary>
        internal Pen penKeduShort;

        /// <summary>
        /// 刻度宽长
        /// </summary>
        internal Pen penKeduLong;

        /// <summary>
        /// 字体颜色
        /// </summary>
        public SolidBrush textbrush;

        /// <summary>
        /// 刻度长短比例，短：长
        /// </summary>
        private int bili;

        StringFormat drawFormat = new StringFormat();
        #endregion
    }

    /// <summary>
    /// 速度指针
    /// </summary>
    public class SpeedPointer
    {
        /// <summary>
        /// 原始图片
        /// </summary>
        private Image originalPic;

        /// <summary>
        /// 需要绘制的图片
        /// </summary>
        private Image drawPic;

        /// <summary>
        /// 需要画的最大角度
        /// </summary>
        private float dialAnglev;

        /// <summary>
        /// 指针初始角度
        /// </summary>
        private float initalAnglev;

        /// <summary>
        /// 指针最大值
        /// </summary>
        private float maxSpeed;

        /// <summary>
        /// 绘图顶点
        /// </summary>
        private PointF topPoint;

        /// <summary>
        /// 绘图中心点
        /// </summary>
        private PointF centralPoint;

        private Matrix matrix;

        /// <summary>
        /// 转动角度
        /// </summary>
        private float anglev = 0;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="maxAnglev"></param>
        /// <param name="initAnglev"></param>
        /// <param name="maxValue"></param>
        /// <param name="apexPoint"></param>
        /// <param name="centrePoint"></param>
        public SpeedPointer(float maxAnglev, float initAnglev, float maxValue, PointF apexPoint, PointF centrePoint)
        {
            dialAnglev = maxAnglev;
            initalAnglev = initAnglev;
            maxSpeed = maxValue;
            topPoint = apexPoint;
            centralPoint = centrePoint;
        }

        /// <summary>
        /// 绘指针
        /// </summary>
        /// <param name="g"></param>
        /// <param name="speed"></param>
        public void PaintPointer(Graphics g, Image tmpPic, ref float speed)
        {
            if (speed <= maxSpeed)
            {
                anglev = speed * dialAnglev / maxSpeed + initalAnglev;
            }
            matrix = g.Transform;
            matrix.RotateAt(anglev, centralPoint);
            g.Transform = matrix;
            g.DrawImage(tmpPic, topPoint);
            matrix.Reset();
            g.Transform = matrix;
        }
    }

    /// <summary>
    /// 变化矩形条
    /// </summary>
    public class NeedChangeLength
    {
        /// <summary>
        /// 当前要画的高度值
        /// </summary>
        private float ViewValue = 0;

        /// <summary>
        /// 需要增加的高度量
        /// </summary>
        private float tmpNeedChangeLength = 0;

        /// <summary>
        /// 递增量
        /// </summary>
        private float tmpStepLength = 5;

        /// <summary>
        /// 黄色画笔
        /// </summary>
        public static SolidBrush yellowBrush = new SolidBrush(Color.Yellow);

        /// <summary>
        /// 绘图起点
        /// </summary>
        PointF drawPoint;

        /// <summary>
        /// 进度条宽度
        /// </summary>
        private int rectWidth;

        /// <summary>
        /// 对应比例
        /// </summary>
        private float rectScale;

        public NeedChangeLength(PointF point, int width, float dizeng, float scale)
        {
            drawPoint = point;
            rectWidth = width;
            tmpStepLength = dizeng;
            rectScale = scale;
        }

        /// <summary>
        /// 绘制纵条
        /// </summary>
        /// <param name="e"></param>
        /// <param name="currentValue"></param>
        /// <param name="drawType"></param>
        public void DrawRectangle(Graphics e, ref float currentValue, int drawType)
        {
            if (ViewValue > currentValue)
            {
                if (ViewValue - tmpStepLength >= currentValue)
                {
                    tmpNeedChangeLength = -tmpStepLength;
                }
                else
                {
                    tmpNeedChangeLength = currentValue - ViewValue;
                }
            }
            else if (ViewValue < currentValue)
            {
                if (ViewValue + tmpStepLength <= currentValue)
                {
                    tmpNeedChangeLength = tmpStepLength;
                }
                else
                {
                    tmpNeedChangeLength = currentValue - ViewValue;
                }
            }
            else
            {
                tmpNeedChangeLength = 0;
            }
            ViewValue += tmpNeedChangeLength;

            switch (drawType)
            {
                case 1://横左
                    e.FillRectangle(yellowBrush, new RectangleF(new PointF(drawPoint.X - ViewValue * rectScale, drawPoint.Y), new SizeF(ViewValue * rectScale, rectWidth)));
                    break;
                case 2://横右
                    e.FillRectangle(yellowBrush, new RectangleF(drawPoint, new SizeF(ViewValue * rectScale, rectWidth)));
                    break;
                case 3://纵上
                    e.FillRectangle(yellowBrush, new RectangleF(new PointF(drawPoint.X, drawPoint.Y - ViewValue * rectScale), new SizeF(rectWidth, ViewValue * rectScale)));
                    break;
                case 4://纵下
                    e.FillRectangle(yellowBrush, new RectangleF(drawPoint, new SizeF(rectWidth, ViewValue * rectScale)));
                    break;
                default:
                    break;
            }

        }
    }

    /// <summary>
    /// 闪烁
    /// </summary>
    public class FlashTimer
    {
        /// <summary>
        /// 闪烁用递增量
        /// </summary>
        int flashCount = 0;

        /// <summary>
        /// 闪烁间隔时间
        /// </summary>
        int flashTime = 0;

        public FlashTimer(int interval)
        {
            flashTime = interval;
        }

        /// <summary>
        /// 闪烁判断
        /// </summary>
        /// <returns></returns>
        public bool IsNeedFlash()
        {
            if (flashCount < flashTime * 5)
            {
                ++flashCount;
                return true;
            }
            else if (flashCount >= flashTime * 5 && flashCount < flashTime * 10)
            {
                ++flashCount;
                return false;
            }
            else
            {
                flashCount = 0;
                return false;
            }
        }
    }

}

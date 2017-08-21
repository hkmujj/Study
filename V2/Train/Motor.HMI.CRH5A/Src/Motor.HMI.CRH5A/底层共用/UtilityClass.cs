using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Motor.HMI.CRH5A.底层共用
{
    //画笔
    public static class SolidBrushsItems
    {
        /// <summary>
        /// Color.Black
        /// </summary>
        public static readonly SolidBrush BlackBrush = new SolidBrush(Color.Black);

        /// <summary>
        /// Color.White
        /// </summary>
        public static readonly SolidBrush WhiteBrush = new SolidBrush(Color.White);

        /// <summary>
        /// Color.FromArgb(200, 0, 0)
        /// </summary>
        public static readonly SolidBrush RedBrush1 = new SolidBrush(Color.FromArgb(200, 0, 0));

        /// <summary>
        /// Color.FromArgb(0, 200, 0)
        /// </summary>
        public static readonly SolidBrush GreenBrush1 = new SolidBrush(Color.FromArgb(0, 200, 0));

        /// <summary>
        /// Color.FromArgb(12, 82, 0)
        /// </summary>
        public static readonly SolidBrush GreenBrush2 = new SolidBrush(Color.FromArgb(12, 82, 0));

        /// <summary>
        /// Color.FromArgb(0, 125, 0)
        /// </summary>
        public static readonly SolidBrush GreenBrush3 = new SolidBrush(Color.FromArgb(0, 125, 0));

        /// <summary>
        /// Color.FromArgb(101, 241, 101)
        /// </summary>
        public static readonly SolidBrush GreenBrush4 = new SolidBrush(Color.FromArgb(101, 241, 101));

        /// <summary>
        /// Color.FromArgb(0, 94, 0)
        /// </summary>
        public static readonly SolidBrush GreenBrush5 = new SolidBrush(Color.FromArgb(0, 94, 0));

        /// <summary>
        /// Color.FromArgb(0, 0, 200)
        /// </summary>
        public static readonly SolidBrush BlueBrush1 = new SolidBrush(Color.FromArgb(0, 0, 200));

        /// <summary>
        /// Color.FromArgb(0, 128, 128)
        /// </summary>
        public static readonly SolidBrush BlueBrush2 = new SolidBrush(Color.FromArgb(0, 128, 128));

        /// <summary>
        /// Color.FromArgb(103, 141, 178)
        /// </summary>
        public static readonly SolidBrush BlueBrush3 = new SolidBrush(Color.FromArgb(103, 141, 178));

        /// <summary>
        /// Color.FromArgb(200, 200, 0)
        /// </summary>
        public static readonly SolidBrush YellowBrush1 = new SolidBrush(Color.FromArgb(200, 200, 0));
        public static readonly SolidBrush YellowBrush2 = new SolidBrush(Color.FromArgb(255, 255, 0));
        /// <summary>
        /// Color.FromArgb(93, 93, 93)
        /// </summary>
        public static readonly SolidBrush Grey1 = new SolidBrush(Color.FromArgb(93, 93, 93));

        /// <summary>
        /// Color.FromArgb(13, 13, 13)
        /// </summary>
        public static readonly SolidBrush Grey2 = new SolidBrush(Color.FromArgb(13, 13, 13));

        /// <summary>
        /// Color.FromArgb(192, 192, 192)
        /// </summary>
        public static readonly SolidBrush Grey3 = new SolidBrush(Color.FromArgb(192, 192, 192));
    }

    //线条
    public static class PenItems
    {
        public static readonly Pen WhitePen = new Pen(SolidBrushsItems.WhiteBrush, 1);

        public static Pen GetThePen(SolidBrush theColor, float theWidth)
        {
            return new Pen(theColor, theWidth);
        }
    }

    //字体格式
    public static class FontsItems
    {
        public static readonly Font DefaultFont = new Font("宋体", 10.5f * Coordinate.Scaling, FontStyle.Regular);
        public static readonly Font Font12 = new Font("宋体", 12f * Coordinate.Scaling, FontStyle.Regular);
        public static readonly Font Font14 = new Font("宋体", 14f * Coordinate.Scaling, FontStyle.Regular);
        public static readonly Font Font18 = new Font("宋体", 18f * Coordinate.Scaling, FontStyle.Regular);
        public static readonly Font Font36 = new Font("宋体", 36f * Coordinate.Scaling, FontStyle.Regular);
        private static Font m_FontEx;

        public static Font Fonts_Regular(FontName strFont, float fontNumb, bool isBold)
        {
            m_FontEx = new Font(strFont.ToString(),
                fontNumb * Coordinate.Scaling,
                GetFontStyle(isBold) ? FontStyle.Bold : FontStyle.Regular);
            return m_FontEx;
        }

        private static bool GetFontStyle(bool isBold)
        {
            if (Coordinate.Scaling < 1)
                return false;
            else
            {
                if (isBold) return true;
                else return false;
            }
        }

        private static readonly Dictionary<FontRelated, StringFormat> StringFormatDictionary = new Dictionary<FontRelated, StringFormat>();

        public static StringFormat TheAlignment(FontRelated fontrelate)
        {
            if (StringFormatDictionary.ContainsKey(fontrelate))
            {
                return StringFormatDictionary[fontrelate];
            }

            var format = new StringFormat();
            switch (fontrelate)
            {
                case FontRelated.居中:
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    break;
                case FontRelated.靠左:
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Near;
                    break;
                case FontRelated.靠右:
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Far;
                    break;
                case FontRelated.靠左上:
                    format.LineAlignment = StringAlignment.Near;
                    format.Alignment = StringAlignment.Near;
                    break;
                default:
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    break;
            }
            StringFormatDictionary.Add(fontrelate, format);
            return format;
        }
    }

    /// <summary>
    /// 变化矩形条
    /// </summary>
    public class NeedChangeLength
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rect">所要画的矩形条的最大状态</param>
        /// <param name="maxValue">到顶的最大显示值</param>
        /// <param name="theDirection">矩形条的增长方向</param>
        /// <param name="needIncrease">是否需要有递增的状态</param>
        public NeedChangeLength(RectangleF rect, float maxValue, RectRiseDirection theDirection, bool needIncrease)
        {
            m_TheRectangleF = rect;
            m_TheMaxValue = maxValue;
            m_TheRectDirection = theDirection;
            m_IsNeedIncrease = needIncrease;

            //内部初始化

        }

        /// <summary>
        /// 画矩形
        /// </summary>
        /// <param name="gs">绘图对象参数</param>
        /// <param name="currentValue">当前收到的要画的值</param>
        /// <param name="brush">画笔颜色</param>
        public void DrawRectangle(Graphics gs, ref float currentValue, SolidBrush brush)
        {
            m_TheBrush = brush;
            float height;
            switch (m_TheRectDirection)
            {
                case RectRiseDirection.上:
                    m_TheScale = m_TheRectangleF.Height / m_TheMaxValue;
                    height = m_IsNeedIncrease ? (ReturnTheVariation(ref currentValue) * m_TheScale) : currentValue * m_TheScale;
                    gs.FillRectangle(m_TheBrush,
                        new RectangleF(m_TheRectangleF.X, m_TheRectangleF.Y + m_TheRectangleF.Height - height, m_TheRectangleF.Width, height));
                    break;
                case RectRiseDirection.下:
                    m_TheScale = m_TheRectangleF.Height / m_TheMaxValue;
                    height = m_IsNeedIncrease ? (ReturnTheVariation(ref currentValue) * m_TheScale) : currentValue * m_TheScale;
                    gs.FillRectangle(m_TheBrush,
                        new RectangleF(m_TheRectangleF.X, m_TheRectangleF.Y, m_TheRectangleF.Width, height));
                    break;
                case RectRiseDirection.左:
                    m_TheScale = m_TheRectangleF.Width / m_TheMaxValue;
                    height = m_IsNeedIncrease ? (ReturnTheVariation(ref currentValue) * m_TheScale) : currentValue * m_TheScale;
                    gs.FillRectangle(m_TheBrush,
                        new RectangleF(m_TheRectangleF.X + m_TheRectangleF.Width - height, m_TheRectangleF.Y, height, m_TheRectangleF.Height));
                    break;
                case RectRiseDirection.右:
                    m_TheScale = m_TheRectangleF.Width / m_TheMaxValue;
                    height = m_IsNeedIncrease ? (ReturnTheVariation(ref currentValue) * m_TheScale) : currentValue * m_TheScale;
                    gs.FillRectangle(m_TheBrush,
                        new RectangleF(m_TheRectangleF.X, m_TheRectangleF.Y, height, m_TheRectangleF.Height));
                    break;
            }
        }

        private float ReturnTheVariation(ref float theValue)
        {
            if (m_ViewValue > theValue)
            {
                if (m_ViewValue - m_TmpStrpLength >= theValue)
                    m_TmpNeedChangeLength = -TmpStrpLength;
                else
                    m_TmpNeedChangeLength = theValue - m_ViewValue;
            }
            else if (m_ViewValue < theValue)
            {
                if (m_ViewValue + m_TmpStrpLength <= theValue)
                    m_TmpNeedChangeLength = m_TmpStrpLength;
                else
                    m_TmpNeedChangeLength = theValue - m_ViewValue;
            }
            else
                m_TmpNeedChangeLength = 0;

            m_ViewValue += m_TmpNeedChangeLength;

            return m_ViewValue;
        }

        //当前要画的高度值
        private float m_ViewValue = 0;

        //需要增加的高度量
        private float m_TmpNeedChangeLength = 0;

        //递增量
        private float m_TmpStrpLength = 5;

        /// <summary>
        /// 递增量
        /// </summary>
        public float TmpStrpLength
        {
            get { return m_TmpStrpLength; }
            set { m_TmpStrpLength = value; }
        }

        //画笔
        private SolidBrush m_TheBrush;

        //所要画的矩形条满格状态大小
        private RectangleF m_TheRectangleF;

        //所要画的矩形条满格状态所表示的值
        private float m_TheMaxValue;

        //是否需要递增状态
        private bool m_IsNeedIncrease;

        //矩形条递增方向
        private RectRiseDirection m_TheRectDirection;

        //比例尺
        private float m_TheScale;
    }

    /// <summary>
    /// 闪烁
    /// </summary>
    public class FlashTimer
    {
        /// <summary>
        /// 闪烁用递增量
        /// </summary>
        private int m_FlashCount = 0;

        /// <summary>
        /// 闪烁间隔时间
        /// </summary>
        private int m_FlashTime = 0;

        public FlashTimer(int interval)
        {
            m_FlashTime = interval;
        }

        /// <summary>
        /// 闪烁判断
        /// </summary>
        /// <returns></returns>
        public bool IsNeedFlash()
        {
            if (m_FlashCount < m_FlashTime * 5)
            {
                ++m_FlashCount;
                return true;
            }
            else if (m_FlashCount >= m_FlashTime * 5 && m_FlashCount < m_FlashTime * 10)
            {
                ++m_FlashCount;
                return false;
            }
            else
            {
                m_FlashCount = 0;
                return false;
            }
        }
    }

    public class TheCountdown
    {
        public TheCountdown(int maxTime)
        {
            m_MaxTime = maxTime;
        }

        public int Counter()
        {
            if (m_MaxTime > 0)
            {
                m_Count++;
                if (m_Count >= 5)
                {
                    m_MaxTime--;
                    m_Count = 0;
                }
            }
            return m_MaxTime;
        }

        public void CounterOver()
        {
            m_MaxTime = 0;
            m_Count = 0;
        }

        public void CounterStart()
        {
            m_MaxTime = 60;
            m_Count = 0;
        }

        private int m_MaxTime;

        private int m_Count;
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
        public Dial(PointF 中心点,
                    string[] 刻度,
                    int 所有线段数,
                    double 每点代表的角度,
                    double 初始角度,
                    int 半径,
                    int 长刻度,
                    int 短刻度,
                    int 长短刻度比,
                    Pen 刻度宽短,
                    Pen 刻度宽长,
                    SolidBrush 刻度字体颜色)
        {
            m_DrawFormat.Alignment = (StringAlignment)1;
            m_DrawFormat.LineAlignment = (StringAlignment)1;

            Point = 中心点;

            m_ScaleStr = 刻度;
            RectKedu = new PointF[刻度.Length];

            PointKedu1 = new PointF[所有线段数];
            PointKedu2 = new PointF[所有线段数];
            m_Numb = 所有线段数;

            MinRadian = 每点代表的角度;

            InitRadian = 初始角度;

            m_Bili = 长短刻度比;

            PenKeduShort = 刻度宽短;
            PenKeduLong = 刻度宽长;

            Textbrush = 刻度字体颜色;

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
            for (int i = 0; i < m_Numb; i++)
            {
                angle = (i * MinRadian + InitRadian) * Math.PI / 180.0;
                sinAngle = (float)Math.Sin(angle);
                cosAngle = (float)Math.Cos(angle);
                if (i % m_Bili == 0)
                {
                    PointKedu1[i].X = Point.X + 半径 * Scale * cosAngle;
                    PointKedu1[i].Y = Point.Y + 半径 * Scale * sinAngle;

                    PointKedu2[i].X = Point.X + (半径 - 长刻度) * Scale * cosAngle;
                    PointKedu2[i].Y = Point.Y + (半径 - 长刻度) * Scale * sinAngle;

                    RectKedu[j].X = Point.X + (半径 - 长刻度 - 30) * Scale * cosAngle;
                    RectKedu[j].Y = Point.Y + (半径 - 长刻度 - 30) * Scale * sinAngle;
                    j++;
                }
                else
                {
                    PointKedu1[i].X = Point.X + 半径 * Scale * cosAngle;
                    PointKedu1[i].Y = Point.Y + 半径 * Scale * sinAngle;

                    PointKedu2[i].X = Point.X + (半径 - 短刻度) * Scale * cosAngle;
                    PointKedu2[i].Y = Point.Y + (半径 - 短刻度) * Scale * sinAngle;
                }
            }
        }

        /// <summary>
        /// 表盘绘制
        /// </summary>
        /// <param name="e"></param>
        public void OnDraw(Graphics e, Font font)
        {
            e.SmoothingMode = SmoothingMode.AntiAlias;
            for (int i = 0; i < m_Numb; i++)
            {
                if (i % m_Bili == 0)
                {
                    e.DrawLine(PenKeduLong, PointKedu1[i], PointKedu2[i]);
                }
                else
                {

                    e.DrawLine(PenKeduShort, PointKedu1[i], PointKedu2[i]);
                }
            }
            for (int i = 0; i < m_ScaleStr.Length; i++)
            {
                e.DrawString(m_ScaleStr[i], font, Textbrush, RectKedu[i], m_DrawFormat);
            }

        }

        #region ::::::::::::::::::::::: init :::::::::::::::::::::::::::

        /// <summary>
        /// 放大缩小系数
        /// </summary>
        internal const float Scale = 1.0f;

        /// <summary>
        /// 刻度点1
        /// </summary>
        public PointF[] PointKedu1;

        /// <summary>
        /// 刻度点2
        /// </summary>
        public PointF[] PointKedu2;

        /// <summary>
        /// 1点速度代表的弧度
        /// </summary>
        internal double MinRadian;

        /// <summary>
        /// 初始弧度
        /// </summary>
        internal double InitRadian;

        /// <summary>
        /// 表盘线段总数
        /// </summary>
        private int m_Numb = 0;

        /// <summary>
        /// 线段代表的数量
        /// </summary>
        private string[] m_ScaleStr;

        /// <summary>
        /// 表盘上的
        /// </summary>
        public string Numbstr;

        /// <summary>
        /// 表盘中心点
        /// </summary>
        public PointF Point = new PointF();

        /// <summary>
        /// 刻度位置
        /// </summary>
        public PointF[] RectKedu;

        /// <summary>
        /// 刻度宽短
        /// </summary>
        internal Pen PenKeduShort;

        /// <summary>
        /// 刻度宽长
        /// </summary>
        internal Pen PenKeduLong;

        /// <summary>
        /// 字体颜色
        /// </summary>
        public SolidBrush Textbrush;

        /// <summary>
        /// 刻度长短比例，短：长
        /// </summary>
        private int m_Bili;

        private StringFormat m_DrawFormat = new StringFormat();

        #endregion
    }

    /// <summary>
    /// 速度指针
    /// </summary>
    public class SpeedPointer
    {
/*
        /// <summary>
        /// 原始图片
        /// </summary>
        private Image originalPic;
*/

        /// <summary>
        /// 需要绘制的图片
        /// </summary>
        private Image m_DrawPic;

        /// <summary>
        /// 需要画的最大角度
        /// </summary>
        private float m_DialAnglev;

        /// <summary>
        /// 需要画的最小角度
        /// </summary>
        private float m_MiniAnglev;

        /// <summary>
        /// 指针初始角度
        /// </summary>
        private float m_InitalAnglev;

        /// <summary>
        /// 指针最大值
        /// </summary>
        private float m_MaxSpeed;

        /// <summary>
        /// 指针最小值
        /// </summary>
        private float m_MiniSpeed;

        /// <summary>
        /// 绘图顶点
        /// </summary>
        private RectangleF m_BackImgRect;

        /// <summary>
        /// 绘图中心点
        /// </summary>
        private PointF m_CentralPoint;

        private Matrix m_Matrix;

        /// <summary>
        /// 转动角度
        /// </summary>
        private float m_Anglev = 0;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="maxAnglev">最大角度</param>
        /// <param name="miniAnglev">最小角度</param>
        /// <param name="initAnglev">初始角度</param>
        /// <param name="maxValue">指针最大值</param>
        /// <param name="miniValue">指针最小值</param>
        /// <param name="apexRect">绘图位置</param>
        /// <param name="centrePoint">绘图中心点</param>
        /// <param name="pointerImg">指针图片</param>
        public SpeedPointer(float maxAnglev,
                            float miniAnglev,
                            float initAnglev,
                            float maxValue,
                            float miniValue,
                            RectangleF apexRect,
                            PointF centrePoint,
                            Image pointerImg)
        {
            m_DialAnglev = maxAnglev;
            m_MiniAnglev = miniAnglev;
            m_InitalAnglev = initAnglev;
            m_MaxSpeed = maxValue;
            m_MiniSpeed = miniValue;
            m_BackImgRect = apexRect;
            m_CentralPoint = centrePoint;
            m_DrawPic = pointerImg;
        }

        public void PaintPointerNormal(Graphics g, float speed)
        {
            PaintPointer(g, m_DrawPic, ref speed);
            //if (speed <= maxSpeed)
            //{
            //    anglev = speed / maxSpeed * dialAnglev + initalAnglev;
            //}
            //matrix = g.Transform;
            //matrix.RotateAt(anglev, centralPoint);
            //g.Transform = matrix;
            //g.DrawImage(drawPic, backImgRect);
            //matrix.Reset();
            //g.Transform = matrix;
        }

        /// <summary>
        /// 绘指针(指针会变化颜色等)
        /// </summary>
        /// <param name="g"></param>
        /// <param name="speed"></param>
        public void PaintPointerColor(Graphics g, Image tmpPic, ref float speed)
        {
            PaintPointer(g, tmpPic, ref speed);
        }

        private void PaintPointer(Graphics g, Image tmpPic, ref float speed)
        {
            if (speed >= 0 && speed <= m_MaxSpeed)
            {
                m_Anglev = speed / m_MaxSpeed * m_DialAnglev + m_InitalAnglev;
            }
            else if (speed < 0 && speed >= m_MiniSpeed)
            {
                m_Anglev = speed / m_MiniSpeed * m_MiniAnglev + m_InitalAnglev;
            }
            var old = g.Transform;
            m_Matrix = g.Transform.Clone();
            m_Matrix.RotateAt(m_Anglev, m_CentralPoint);
            g.Transform = m_Matrix;
            g.DrawImage(tmpPic, m_BackImgRect);
            g.Transform = old;
        }
    }

    public enum TrainInside
    {
        /// <summary>
        /// 8车正常方向
        /// </summary>
        Normal8,

        /// <summary>
        /// 8车换端
        /// </summary>
        Inside8,

        /// <summary>
        /// 16车正常方向
        /// </summary>
        Normal16,

        /// <summary>
        /// 16车换端
        /// </summary>
        Inside16
    }
}

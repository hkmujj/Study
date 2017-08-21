using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Util;

namespace Motor.ATP._300T.共用
{
    //画笔

    //线条
    public static class PenItems
    {
        public static readonly Pen WhitePen2 = new Pen(SolidBrushsItems.WhiteBrush, 2);

        public static readonly Pen YellowPen2 = new Pen(SolidBrushsItems.YellowBrush, 2);
        public static readonly Pen YellowPen9 = new Pen(SolidBrushsItems.YellowBrush, 9);
        public static readonly Pen YellowPen18 = new Pen(SolidBrushsItems.YellowBrush, 18);

        public static readonly Pen OrangePen15 = new Pen(SolidBrushsItems.OrangeBrush, 15);

        public static readonly Pen DarkGrayPen10 = new Pen(SolidBrushsItems.DarkGrayBrush, 10);

        public static readonly Pen GrayPen9 = new Pen(SolidBrushsItems.GrayBrush, 9f);
        public static readonly Pen GrayPen18 = new Pen(SolidBrushsItems.GrayBrush, 18f);

        public static readonly Pen RedPen9 = new Pen(SolidBrushsItems.RedBrush, 9f);
        public static readonly Pen RedPen15 = new Pen(SolidBrushsItems.RedBrush, 15f);
        public static readonly Pen RedPen18 = new Pen(SolidBrushsItems.RedBrush, 18f);

    }

    //字体格式
    public static class FontsItems
    {
        static FontsItems()
        {
            StrFormatDict = new Dictionary<FontRelated, StringFormat>();
        }
        private static readonly Dictionary<FontRelated, StringFormat> StrFormatDict;

        public static readonly Font DefaultFont = new Font("宋体", 10.5f * Coordinate.Scaling, FontStyle.Regular);
        public static readonly Font Font12YouR = new Font("幼圆", 12f * Coordinate.Scaling, FontStyle.Regular);
        public static readonly Font Font14YouR = new Font("幼圆", 14f * Coordinate.Scaling, FontStyle.Regular);

        public static readonly Font Font12YouB = new Font("幼圆", 12f * Coordinate.Scaling, FontStyle.Bold);
        public static readonly Font Font13YouB = new Font("幼圆", 13f * Coordinate.Scaling, FontStyle.Bold);
        public static readonly Font Font14YouB = new Font("幼圆", 14f * Coordinate.Scaling, FontStyle.Bold);
        public static readonly Font Font16YouB = new Font("幼圆", 16f * Coordinate.Scaling, FontStyle.Bold);
        public static readonly Font Font17YouB = new Font("幼圆", 17f * Coordinate.Scaling, FontStyle.Bold);
        public static readonly Font Font18YouB = new Font("幼圆", 18f * Coordinate.Scaling, FontStyle.Bold);
        public static readonly Font Font20YouB = new Font("幼圆", 20f * Coordinate.Scaling, FontStyle.Bold);
        public static readonly Font Font22YouB = new Font("幼圆", 22f * Coordinate.Scaling, FontStyle.Bold);
        public static readonly Font Font24YouB = new Font("幼圆", 24f * Coordinate.Scaling, FontStyle.Bold);

        public static readonly Font Font12DefB = new Font("Arial", 12f * Coordinate.Scaling, FontStyle.Bold);
        public static readonly Font Font18DefB = new Font("Arial", 18f * Coordinate.Scaling, FontStyle.Bold);

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
            return !(Coordinate.Scaling < 1) && isBold;
        }

        public static StringFormat TheAlignment(FontRelated fontrelate)
        {
            if (StrFormatDict.ContainsKey(fontrelate))
            {
                return StrFormatDict[fontrelate];
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
                case FontRelated.靠左下:
                    format.LineAlignment = StringAlignment.Far;
                    format.Alignment = StringAlignment.Near;
                break;
                default:
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    break;
            }
            StrFormatDict.Add(fontrelate, format);
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
        public NeedChangeLength(RectangleF rect, float maxValue, RectRiseDirection theDirection)
        {
            m_TheRectangleF = rect;
            m_TheMaxValue = maxValue;
            m_TheRectDirection = theDirection;

            //内部初始化

        }

        /// <summary>
        /// 画矩形
        /// </summary>
        /// <param name="gs">绘图对象参数</param>
        /// <param name="currentValue">当前收到的要画的值</param>
        /// <param name="brush">画笔颜色</param>
        public void DrawRectangle(Graphics gs, float currentValue, SolidBrush brush)
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
        private float m_ViewValue;

        //需要增加的高度量
        private float m_TmpNeedChangeLength;

        //画笔
        private SolidBrush m_TheBrush;

        //所要画的矩形条满格状态大小
        private RectangleF m_TheRectangleF;

        //所要画的矩形条满格状态所表示的值
        private readonly float m_TheMaxValue;

        //矩形条递增方向
        private readonly RectRiseDirection m_TheRectDirection;

        //比例尺
        private float m_TheScale;

        //是否要递增
        private bool m_IsNeedIncrease;
        /// <summary>
        /// 是否需要有递增的状态
        /// </summary>
        public bool IsNeedIncrease { get { return m_IsNeedIncrease; } set { m_IsNeedIncrease = value; } }

        //递增量
        private float m_TmpStrpLength = 5;
        /// <summary>
        /// 递增量
        /// </summary>
        public float TmpStrpLength { get { return m_TmpStrpLength; } set { m_TmpStrpLength = value; } }
    }

    /// <summary>
    /// 对数坐标的矩形
    /// </summary>
    public class NeedChangeLogRectangle
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rect">变化矩形条最大状态</param>
        /// <param name="maxValue">矩形条最大状态下所表示的值</param>
        /// <param name="minValue">矩形条最小状态下所表示的值</param>
        /// <param name="dirction">增长方向</param>
        /// <param name="baseOfLog">log底数</param>
        public NeedChangeLogRectangle(RectangleF rect, float maxValue, float minValue, RectRiseDirection dirction, int baseOfLog)
        {
            m_TheRect = rect;
            m_MaxValue = maxValue;
            m_MinValue = minValue;
            m_Dirction = dirction;
            m_BaseOfLog = baseOfLog;
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="e"></param>
        /// <param name="currentValue">当前值</param>
        /// <param name="brush">矩形条颜色</param>
        public void DrawRect(Graphics e, float currentValue, SolidBrush brush)
        {
            m_TheBrush = brush;
            var rectHeight = GetRectHeight(currentValue);
            switch (m_Dirction)
            {
                case RectRiseDirection.上:
                    e.FillRectangle(m_TheBrush,
                        new RectangleF(m_TheRect.X, m_TheRect.Y + m_TheRect.Height - rectHeight, m_TheRect.Width, rectHeight));
                    break;
                case RectRiseDirection.下:
                    e.FillRectangle(m_TheBrush,
                        new RectangleF(m_TheRect.X, m_TheRect.Y + rectHeight, m_TheRect.Width, rectHeight));
                    break;
                case RectRiseDirection.左:
                    e.FillRectangle(m_TheBrush,
                        new RectangleF(m_TheRect.X - rectHeight, m_TheRect.Y, rectHeight, m_TheRect.Height));
                    break;
                case RectRiseDirection.右:
                    e.FillRectangle(m_TheBrush,
                        new RectangleF(m_TheRect.X + rectHeight, m_TheRect.Y, rectHeight, m_TheRect.Height));
                    break;
            }
        }

        private float GetRectHeight(float currentValue)
        {
            switch (m_Dirction)
            {
                case RectRiseDirection.上:
                case RectRiseDirection.下:
                    var tmpA = Math.Log(m_MaxValue, m_BaseOfLog);
                    var tmpB = Math.Log(m_MinValue, m_BaseOfLog);
                    var tmpC = Math.Log(currentValue, m_BaseOfLog);
                    var tmpD = m_TheRect.Height / tmpA;
                    var tmpE = ( tmpD - tmpB ) * ( tmpC - tmpB );
                    return (float) tmpE;
                case RectRiseDirection.左:
                case RectRiseDirection.右:
                    return (float)
                        (m_TheRect.Width / (Math.Log(m_MaxValue, m_BaseOfLog) - Math.Log(m_MinValue, m_BaseOfLog))
                               * (Math.Log(currentValue, m_BaseOfLog) - Math.Log(m_MinValue, m_BaseOfLog)));
            }
            return 0f;
        }

        //
        private RectangleF m_TheRect;
        //
        private readonly float m_MaxValue;
        //
        private readonly float m_MinValue;
        //
        private readonly int m_BaseOfLog;
        //画笔
        private SolidBrush m_TheBrush;

        private RectRiseDirection m_Dirction;
        public RectRiseDirection Dirction 
        {
            set
            {
                m_Dirction = value;
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
        int m_FlashCount;

        /// <summary>
        /// 闪烁间隔时间
        /// </summary>
        readonly int m_FlashTime;

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
            if (m_FlashCount >= m_FlashTime * 5 && m_FlashCount < m_FlashTime * 10)
            {
                ++m_FlashCount;
                return false;
            }
            m_FlashCount = 0;
            return false;
        }
    }

    /// <summary>
    /// 倒计时
    /// </summary>
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
        public Dial(PointF 中心点, string[] 刻度, int 所有线段数,
            double 每点代表的角度, double 初始角度, int 半径,
            int 长刻度, int 短刻度, int 长短刻度比,
            Pen 刻度宽短, Pen 刻度宽长, SolidBrush 刻度字体颜色)
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
            var j = 0;
            for (var i = 0; i < m_Numb; i++)
            {
                var angle = (i * MinRadian + InitRadian) * Math.PI / 180.0;
                var sinAngle = (float)Math.Sin(angle);
                var cosAngle = (float)Math.Cos(angle);
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
        /// <param name="font"></param>
        public void OnDraw(Graphics e, Font font)
        {
            e.SmoothingMode = SmoothingMode.AntiAlias;
            for (var i = 0; i < m_Numb; i++)
            {
                e.DrawLine(i%m_Bili == 0 ? PenKeduLong : PenKeduShort, PointKedu1[i], PointKedu2[i]);
            }
            for (var i = 0; i < m_ScaleStr.Length; i++)
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
        readonly int m_Numb;

        /// <summary>
        /// 线段代表的数量
        /// </summary>
        private readonly string[] m_ScaleStr;

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
        private readonly int m_Bili;

        readonly StringFormat m_DrawFormat = new StringFormat();
        #endregion
    }

    /// <summary>
    /// 速度指针
    /// </summary>
    public class SpeedPointer
    {
        /// <summary>
        /// 需要绘制的图片
        /// </summary>
        private readonly Image m_DrawPic;

        /// <summary>
        /// 需要画的最大角度
        /// </summary>
        private readonly float m_DialAnglev;

        /// <summary>
        /// 需要画的最小角度
        /// </summary>
        private readonly float m_MiniAnglev;

        /// <summary>
        /// 指针初始角度
        /// </summary>
        private readonly float m_InitalAnglev;

        /// <summary>
        /// 指针最大值
        /// </summary>
        private readonly float m_MaxSpeed;

        /// <summary>
        /// 指针最小值
        /// </summary>
        private readonly float m_MiniSpeed;

        /// <summary>
        /// 绘图顶点
        /// </summary>
        private readonly RectangleF m_BackImgRect;

        /// <summary>
        /// 绘图中心点
        /// </summary>
        private readonly PointF m_CentralPoint;

        private Matrix m_Matrix;

        /// <summary>
        /// 转动角度
        /// </summary>
        private float m_Anglev;

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
        public SpeedPointer(float maxAnglev, float miniAnglev,
                            float initAnglev,
                            float maxValue, float miniValue,
                            RectangleF apexRect, PointF centrePoint, Image pointerImg)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="speed"></param>
        public void PaintPointerNormal(Graphics g, float speed)
        {
            PaintPointer(g, m_DrawPic, speed);
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
        /// <param name="tmpPic"></param>
        /// <param name="speed"></param>
        public void PaintPointerColor(Graphics g, Image tmpPic, float speed)
        {
            PaintPointer(g, tmpPic, speed);
        }

        private void PaintPointer(Graphics g, Image tmpPic, float speed)
        {
            if (speed >= m_MiniSpeed && speed <= m_MaxSpeed)
            {
                m_Anglev = (speed - m_MiniSpeed) / (m_MaxSpeed - m_MiniSpeed) * m_DialAnglev + m_InitalAnglev;
            }
            else if (speed < 0 && speed >= m_MiniSpeed)
            {
                m_Anglev = speed / m_MiniSpeed * m_MiniAnglev + m_InitalAnglev;
            }
            var old = g.Transform.Clone();
            m_Matrix = g.Transform;
            m_Matrix.RotateAt(m_Anglev, m_CentralPoint);
            g.Transform = m_Matrix;
            g.DrawImage(tmpPic, m_BackImgRect);
            m_Matrix.Reset();
            g.Transform = old;
        }
    }

    public class Circle
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxAnglev">最大角度</param>
        /// <param name="initAnglev">初始角度</param>
        /// <param name="maxValue">圆弧最大值</param>
        /// <param name="miniValue">圆弧最小值</param>
        /// <param name="apexRect">绘图位置</param>
        public Circle(float maxAnglev,
                            float initAnglev,
                            float maxValue, float miniValue,
                            RectangleF apexRect)
        {
            m_MaxAnglev = maxAnglev;
            m_InitAnglev = initAnglev;
            m_MaxValue = maxValue;
            m_MiniValue = miniValue;
            m_ApexRect = apexRect;
        }

        /// <summary>
        /// 绘制固定起点圆弧
        /// </summary>
        /// <param name="g"></param>
        /// <param name="speed">所要画的圆弧大小</param>
        /// <param name="thePen">圆弧曲线</param>
        public void PaintCircle(Graphics g, float speed, Pen thePen)
        {
            if (speed >= m_MiniValue && speed <= m_MaxValue)
            {
                m_Anglev = (speed - m_MiniValue) / (m_MaxValue - m_MiniValue) * m_MaxAnglev;
            }

            try
            {
                g.DrawArc(thePen, m_ApexRect, m_InitAnglev, m_Anglev);
            }
            catch (Exception e)
            {
                AppLog.Fatal(string.Format("PaintCircle error . {0}", e));
            }

        }

        /// <summary>
        /// 绘制起点不固定圆弧
        /// </summary>
        /// <param name="gs"></param>
        /// <param name="speed"></param>
        /// <param name="initValue"></param>
        /// <param name="thePen"></param>
        public void PaintCircle(Graphics gs, float speed, float initValue, Pen thePen)
        {
            if (speed >= m_MiniValue && speed <= m_MaxValue)
            {
                m_Anglev = (speed - m_MiniValue) / (m_MaxValue - m_MiniValue) * m_MaxAnglev;
            }
            var initAnglev = m_InitAnglev + ((initValue - m_MiniValue) / (m_MaxValue - m_MiniValue)) * m_MaxAnglev;
            var test = Math.Abs(m_Anglev) - (Math.Abs(m_InitAnglev) - Math.Abs(initAnglev));
            GC.Collect();
            gs.DrawArc(thePen, m_ApexRect, initAnglev, test);
        }

        public void PaintCircleHook(Graphics gs, float speed, float hookAnglev, Pen thePen)
        {
            gs.DrawArc(thePen, m_ApexRect, 
                m_InitAnglev + ((speed - m_MiniValue) / (m_MaxValue - m_MiniValue)) * m_MaxAnglev - hookAnglev,
                hookAnglev);
        }

        /// <summary>
        /// 最大角度
        /// </summary>
        private readonly float m_MaxAnglev;

        /// <summary>
        /// 初始角度
        /// </summary>
        private readonly float m_InitAnglev;

        /// <summary>
        /// 圆弧最大时表示的值
        /// </summary>
        private readonly float m_MaxValue;

        /// <summary>
        /// 圆弧最小时表示的值
        /// </summary>
        private readonly float m_MiniValue;

        /// <summary>
        /// 绘图位置
        /// </summary>
        private readonly RectangleF m_ApexRect;

        /// <summary>
        /// 转动角度
        /// </summary>
        private float m_Anglev;      

    }
}

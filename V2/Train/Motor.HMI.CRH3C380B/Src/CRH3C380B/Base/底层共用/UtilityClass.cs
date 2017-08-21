using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.底层共用
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
        /// Color.FromArgb(0, 255, 0)
        /// </summary>
        public static readonly SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 0));

        /// <summary>
        /// Color.FromArgb(200, 200, 0)
        /// </summary>
        public static readonly SolidBrush YellowBrush1 = new SolidBrush(Color.FromArgb(192, 129, 0));

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

        /// <summary>
        /// Color.FromArgb(118, 118, 118)
        /// </summary>
        public static readonly SolidBrush Grey4 = new SolidBrush(Color.FromArgb(118, 118, 118));
    }

    //线条
    public static class PenItems
    {
        public static readonly Pen WhiltPen = new Pen(SolidBrushsItems.WhiteBrush, 1);
        public static readonly Pen BlackPen = new Pen(SolidBrushsItems.BlackBrush, 1);

        public static Pen GetThePen(SolidBrush theColor, float theWidth)
        {
            return new Pen(theColor, theWidth);
        }
    }

    //字体格式
    public static class FontsItems
    {
        public readonly static Font FontC10 = new Font("宋体", 10f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontC11 = new Font("宋体", 11f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontC12 = new Font("宋体", 12f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontC14 = new Font("宋体", 14f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontC16 = new Font("宋体", 16f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontC18 = new Font("宋体", 18f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontC20 = new Font("宋体", 20f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontC22 = new Font("宋体", 22f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontC24 = new Font("宋体", 24f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontC10B = new Font("宋体", 10f*Coordinate.Scaling, FontStyle.Bold);
        public readonly static Font FontC11B = new Font("宋体", 11f*Coordinate.Scaling, FontStyle.Bold);
        public readonly static Font FontC12B = new Font("宋体", 12f*Coordinate.Scaling, FontStyle.Bold);
        public readonly static Font FontC14B = new Font("宋体", 14f*Coordinate.Scaling, FontStyle.Bold);
        public readonly static Font FontC16B = new Font("宋体", 16f*Coordinate.Scaling, FontStyle.Bold);
        public readonly static Font FontC18B = new Font("宋体", 18f*Coordinate.Scaling, FontStyle.Bold);
        public readonly static Font FontC20B = new Font("宋体", 20f*Coordinate.Scaling, FontStyle.Bold);
        public readonly static Font FontC22B = new Font("宋体", 22f*Coordinate.Scaling, FontStyle.Bold);
        public readonly static Font FontC24B = new Font("宋体", 24f*Coordinate.Scaling, FontStyle.Bold);

        public readonly static Font FontE12 = new Font("Arial", 12f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontE14 = new Font("Arial", 14f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontE16 = new Font("Arial", 16f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontE18 = new Font("Arial", 18f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontE20 = new Font("Arial", 20f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontE22 = new Font("Arial", 22f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontE24 = new Font("Arial", 24f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontE26 = new Font("Arial", 26f*Coordinate.Scaling, FontStyle.Regular);
        public readonly static Font FontE12B = new Font("Arial", 12f*Coordinate.Scaling, FontStyle.Bold);
        public readonly static Font FontE14B = new Font("Arial", 14f*Coordinate.Scaling, FontStyle.Bold);
        public readonly static Font FontE16B = new Font("Arial", 16f*Coordinate.Scaling, FontStyle.Bold);
        public readonly static Font FontE18B = new Font("Arial", 18f*Coordinate.Scaling, FontStyle.Bold);
        public readonly static Font FontE20B = new Font("Arial", 20f*Coordinate.Scaling, FontStyle.Bold);
        public readonly static Font FontE22B = new Font("Arial", 22f*Coordinate.Scaling, FontStyle.Bold);
        public readonly static Font FontE24B = new Font("Arial", 24f*Coordinate.Scaling, FontStyle.Bold);
        public readonly static Font FontE26B = new Font("Arial", 26f*Coordinate.Scaling, FontStyle.Bold);

        private static Font m_FontEx;

        public static Font Fonts_Regular(FontName strFont, float fontNumb, bool isBold)
        {
            m_FontEx = new Font(strFont.ToString(),
                fontNumb*Coordinate.Scaling,
                GetFontStyle(isBold) ? FontStyle.Bold : FontStyle.Regular);
            return m_FontEx;
        }

        private static bool GetFontStyle(bool isBold)
        {
            return !(Coordinate.Scaling < 1) && isBold;
        }

        private static readonly Dictionary<FontRelated, StringFormat> FormatDictionary =
            new Dictionary<FontRelated, StringFormat>();

        public static StringFormat TheAlignment(FontRelated fontrelate)
        {
            if (FormatDictionary.ContainsKey(fontrelate))
            {
                return FormatDictionary[fontrelate];
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
                case FontRelated.靠中上:
                    format.LineAlignment = StringAlignment.Near;
                    format.Alignment = StringAlignment.Center;
                    break;
                case FontRelated.靠右上:
                    format.LineAlignment = StringAlignment.Near;
                    format.Alignment = StringAlignment.Far;
                    break;
                case FontRelated.靠左下:
                    format.LineAlignment = StringAlignment.Far;
                    format.Alignment = StringAlignment.Near;
                    break;
                case FontRelated.靠中下:
                    format.LineAlignment = StringAlignment.Far;
                    format.Alignment = StringAlignment.Center;
                    break;
                case FontRelated.靠右下:
                    format.LineAlignment = StringAlignment.Far;
                    format.Alignment = StringAlignment.Far;
                    break;
                default:
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    break;
            }

            FormatDictionary.Add(fontrelate, format);

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
            TheRectangleF = rect;
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
        /// <param name="pen">外边框</param>
        public void DrawRectangle(Graphics gs, float currentValue, SolidBrush brush, Pen pen)
        {
            var theValue = currentValue <= m_TheMaxValue ? currentValue : m_TheMaxValue;
            m_TheBrush = brush;
            float height;
            switch (m_TheRectDirection)
            {
                case RectRiseDirection.上:
                    m_TheScale = TheRectangleF.Height/m_TheMaxValue;
                    height = m_IsNeedIncrease ? (ReturnTheVariation(ref theValue)*m_TheScale) : theValue*m_TheScale;
                    gs.FillRectangle(m_TheBrush,
                        new RectangleF(TheRectangleF.X, TheRectangleF.Y + TheRectangleF.Height - height,
                            TheRectangleF.Width, height));
                    gs.DrawRectangle(pen, TheRectangleF.X, TheRectangleF.Y + TheRectangleF.Height - height,
                        TheRectangleF.Width, height);
                    break;
                case RectRiseDirection.下:
                    m_TheScale = TheRectangleF.Height/m_TheMaxValue;
                    height = m_IsNeedIncrease ? (ReturnTheVariation(ref theValue)*m_TheScale) : theValue*m_TheScale;
                    gs.FillRectangle(m_TheBrush,
                        new RectangleF(TheRectangleF.X, TheRectangleF.Y, TheRectangleF.Width, height));
                    break;
                case RectRiseDirection.左:
                    m_TheScale = TheRectangleF.Width/m_TheMaxValue;
                    height = m_IsNeedIncrease ? (ReturnTheVariation(ref theValue)*m_TheScale) : theValue*m_TheScale;
                    gs.FillRectangle(m_TheBrush,
                        new RectangleF(TheRectangleF.X + TheRectangleF.Width - height, TheRectangleF.Y, height,
                            TheRectangleF.Height));
                    break;
                case RectRiseDirection.右:
                    m_TheScale = TheRectangleF.Width/m_TheMaxValue;
                    height = m_IsNeedIncrease ? (ReturnTheVariation(ref theValue)*m_TheScale) : theValue*m_TheScale;
                    gs.FillRectangle(m_TheBrush,
                        new RectangleF(TheRectangleF.X, TheRectangleF.Y, height, TheRectangleF.Height));
                    break;
            }
        }

        private float ReturnTheVariation(ref float theValue)
        {
            if (m_ViewValue > theValue)
            {
                if (m_ViewValue - m_TmpStrpLength >= theValue)
                {
                    m_TmpNeedChangeLength = -TmpStrpLength;
                }
                else
                {
                    m_TmpNeedChangeLength = theValue - m_ViewValue;
                }
            }
            else if (m_ViewValue < theValue)
            {
                if (m_ViewValue + m_TmpStrpLength <= theValue)
                {
                    m_TmpNeedChangeLength = m_TmpStrpLength;
                }
                else
                {
                    m_TmpNeedChangeLength = theValue - m_ViewValue;
                }
            }
            else
            {
                m_TmpNeedChangeLength = 0;
            }

            m_ViewValue += m_TmpNeedChangeLength;

            return m_ViewValue;
        }

        //当前要画的高度值
        private float m_ViewValue;

        //需要增加的高度量
        private float m_TmpNeedChangeLength;

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
        public RectangleF TheRectangleF { private set; get; }

        //所要画的矩形条满格状态所表示的值
        private readonly float m_TheMaxValue;

        //是否需要递增状态
        private readonly bool m_IsNeedIncrease;

        //矩形条递增方向
        private readonly RectRiseDirection m_TheRectDirection;

        //比例尺
        private float m_TheScale;
    }

    /// <summary>
    /// 闪烁计时器
    /// </summary>
    public class TimeCounter
    {
        public TimeCounter(float intervalTime)
        {
            m_IntervalTime = intervalTime;
        }

        /// <summary>
        /// 闪烁开始
        /// </summary>
        /// <param name="intervalTime">闪烁间隔,单位秒</param>
        /// <returns></returns>
        public bool FlashBegin(float intervalTime)
        {
            if (m_Count < intervalTime*5)
            {
                m_Count++;
                return true;
            }

            if (m_Count >= intervalTime*5 && m_Count < intervalTime*10)
            {
                m_Count++;
                return false;
            }

            m_Count = 0;
            return false;
        }

        public bool FlashBegin()
        {
            if (m_Count < m_IntervalTime*5)
            {
                m_Count++;
                return true;
            }

            if (m_Count >= m_IntervalTime*5 && m_Count < m_IntervalTime*10)
            {
                m_Count++;
                return false;
            }

            m_Count = 0;
            return false;
        }

        private readonly float m_IntervalTime;
        private int m_Count;
    }

    public class TimeWriter
    {
        public void TimeBegin(DateTime beginTime)
        {
            TheTime = beginTime;
        }

        public void TimeOver()
        {
            TheTime = new DateTime();
        }

        public DateTime TheTime { get; private set; }
    }

    public class TheTimeCounter
    {
        public bool TimeUp(DateTime currentTime, int secondCount)
        {
            if (m_TimeSet)
            {
                return (currentTime - m_TimeBegins.TheTime).Seconds >= secondCount;
            }

            m_TimeBegins.TimeBegin(currentTime);
            m_TimeSet = true;

            return secondCount == 0;
        }

        public void ResetCounter()
        {
            m_TimeSet = false;
            m_TimeBegins.TimeOver();
        }

        private bool m_TimeSet;
        private readonly TimeWriter m_TimeBegins = new TimeWriter();
    }

    /// <summary>
    /// 下标题按钮框状态显示
    /// </summary>
    public class RectOfButtonState
    {
        public RectOfButtonState(RectangleF rect, string str)
        {
            m_BtnRect = rect;
            BtnStr = str;
        }

        public void Draw(baseClass obj, Graphics gs, bool btnDown)
        {
            if (btnDown)
            {
                gs.FillRectangle(obj.FindNeighbourObject<DMITitle>().NightMode
                    ? SolidBrushsItems.BlackBrush
                    : SolidBrushsItems.WhiteBrush,
                    m_BtnRect);
                gs.DrawString(BtnStr, FontsItems.FontC11,
                    obj.FindNeighbourObject<DMITitle>().NightMode
                        ? SolidBrushsItems.WhiteBrush
                        : SolidBrushsItems.BlackBrush,
                    m_BtnRect, FontsItems.TheAlignment(FontRelated.居中));
            }
            else
            {
                gs.DrawString(BtnStr, FontsItems.FontC11,
                    obj.FindNeighbourObject<DMITitle>().NightMode
                        ? SolidBrushsItems.BlackBrush
                        : SolidBrushsItems.WhiteBrush,
                    m_BtnRect, FontsItems.TheAlignment(FontRelated.居中));
            }
        }

        private readonly RectangleF m_BtnRect;

        /// <summary>
        /// 按钮框内容
        /// </summary>
        public string BtnStr { get; set; }
    }

    /// <summary>
    /// 制动试验结果
    /// </summary>
    public class BrakeTestResult
    {
        public BrakeTestResult(string testName)
        {
            TestIsOver = false;
            TestName = testName;
        }

        /// <summary>
        /// 试验完成
        /// </summary>
        /// <param name="overTime">完成时间</param>
        public void TestOver(DateTime overTime)
        {
            if (TestIsOver)
            {
                return;
            }

            TestIsOver = true;
            OverTime = overTime;
        }

        /// <summary>
        /// 试验重置
        /// </summary>
        public void ResetTest()
        {
            TestIsOver = false;
            OverTime = new DateTime();
        }

        /// <summary>
        /// 试验名称
        /// </summary>
        public string TestName { get; private set; }

        /// <summary>
        /// 制动试验是否完成
        /// </summary>
        public bool TestIsOver { get; private set; }

        /// <summary>
        /// 试验结束时间
        /// </summary>
        public DateTime OverTime { get; private set; }
    }
}
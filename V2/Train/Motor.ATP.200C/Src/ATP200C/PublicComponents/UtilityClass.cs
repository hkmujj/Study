using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using ES.Facility.PublicModule.Memo;

namespace ATP200C.PublicComponents
{
    //画笔
    public static class SolidBrushsItems
    {
        /// <summary>
        /// 黑色
        /// Color.Black
        /// </summary>
        public static readonly SolidBrush BlackBrush = new SolidBrush(Color.Black);

        /// <summary>
        /// 白色
        /// Color.White
        /// </summary>
        public static readonly SolidBrush WhiteBrush = new SolidBrush(Color.White);

        /// <summary>
        /// 红色
        /// Color.FromArgb(191, 0, 2)
        /// </summary>
        public static readonly SolidBrush RedBrush = new SolidBrush(Color.FromArgb(191, 0, 2));

        /// <summary>
        /// 黄色
        /// Color.FromArgb(223, 223, 0)
        /// </summary>
        public static readonly SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(223, 223, 0));

        /// <summary>
        /// 橙色
        /// Color.FromArgb(234, 145, 0)
        /// </summary>
        public static readonly SolidBrush OrangeBrush = new SolidBrush(Color.FromArgb(234, 145, 0));

        /// <summary>
        /// 灰色
        /// Color.FromArgb(195, 195, 195)
        /// </summary>
        public static readonly SolidBrush GrayBrush = new SolidBrush(Color.FromArgb(195, 195, 195));

        /// <summary>
        /// 中灰色
        /// Color.FromArgb(150, 150, 150)
        /// </summary>
        public static readonly SolidBrush MediumGreyBrush = new SolidBrush(Color.FromArgb(150, 150, 150));

        /// <summary>
        /// 暗灰色
        /// Color.FromArgb(85, 85, 85)
        /// </summary>
        public static readonly SolidBrush DarkGrayBrush = new SolidBrush(Color.FromArgb(85, 85, 85));

        /// <summary>
        /// 蓝色
        /// Color.FromArgb(10, 15, 80)
        /// </summary>
        public static readonly SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(10, 15, 80));

        /// <summary>
        /// 绿色
        /// Color.FromArgb(0, 255, 0)
        /// </summary>
        public static readonly SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 255, 0));

        /// <summary>
        /// 背景色
        /// Color.FromArgb(3, 17, 34)
        /// </summary>
        public static readonly SolidBrush BKGBrush = new SolidBrush(Color.FromArgb(26, 26, 26));

        /// <summary>
        /// 可用按键颜色
        /// Color.FromArgb(20, 17, 47)
        /// </summary>
        public static readonly SolidBrush BtnAbledBrush = new SolidBrush(Color.FromArgb(20, 17, 47));

        /// <summary>
        /// 不可用按键颜色
        /// Color.FromArgb(40, 40, 40)
        /// </summary>
        public static readonly SolidBrush BtnDisabledBrush = new SolidBrush(Color.FromArgb(40, 40, 40));

        /// <summary>
        /// MRSP颜色
        /// Color.FromArgb(0, 0, 200)
        /// </summary>
        public static readonly SolidBrush MRSPBrush = new SolidBrush(Color.FromArgb(0, 0, 200));

        /// <summary>
        /// MRSP背景颜色
        /// Color.FromArgb(10, 24, 70)
        /// </summary>
        public static readonly SolidBrush MRSPBKGBrush = new SolidBrush(Color.FromArgb(10, 24, 70));

        /// <summary>
        /// 输入界面背景色
        /// Color.FromArgb(10, 15, 60)
        /// </summary>
        public static readonly SolidBrush InputBrush = new SolidBrush(Color.FromArgb(10, 15, 60));

        /// <summary>
        /// Color.FromArgb(85, 85, 85)
        /// </summary>
        public static readonly SolidBrush InputBKGBrush = new SolidBrush(Color.FromArgb(85, 85, 85));
    }

    //线条
    /// <summary>
    /// 
    /// </summary>
    public static class PenItems
    {
        public static readonly Pen WhitePen1 = new Pen(SolidBrushsItems.WhiteBrush, 1);
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
            _StrFormatDict = new Dictionary<FontRelated, StringFormat>();
        }
        private static Dictionary<FontRelated, StringFormat> _StrFormatDict;

        /// <summary>
        /// 
        /// </summary>
        public readonly static Font DefaultFont = new Font("宋体", 10.5f, FontStyle.Regular);

        /// <summary>
        /// 
        /// </summary>
        public readonly static Font Font12_You_R = new Font("幼圆", 12f, FontStyle.Regular);

        /// <summary>
        /// 
        /// </summary>
        public readonly static Font Font14_You_R = new Font("幼圆", 14f, FontStyle.Regular);

        /// <summary>
        /// 
        /// </summary>
        public readonly static Font Font12_You_B = new Font("幼圆", 12f, FontStyle.Bold);

        /// <summary>
        /// 
        /// </summary>
        public readonly static Font Font13_You_B = new Font("幼圆", 13f, FontStyle.Bold);

        /// <summary>
        /// 
        /// </summary>
        public readonly static Font Font14_You_B = new Font("幼圆", 14f, FontStyle.Bold);

        /// <summary>
        /// 
        /// </summary>
        public readonly static Font Font16_You_B = new Font("幼圆", 16f, FontStyle.Bold);

        /// <summary>
        /// 
        /// </summary>
        public readonly static Font Font17_You_B = new Font("幼圆", 17f, FontStyle.Bold);

        /// <summary>
        /// 
        /// </summary>
        public readonly static Font Font18_You_B = new Font("幼圆", 18f, FontStyle.Bold);

        /// <summary>
        /// 
        /// </summary>
        public readonly static Font Font12_Def_B = new Font("Arial", 12f, FontStyle.Bold);

        /// <summary>
        /// 
        /// </summary>
        public readonly static Font Font18_Def_B = new Font("Arial", 18f, FontStyle.Bold);

        /// <summary>
        /// 
        /// </summary>
        public readonly static Font Font28_Def_B = new Font("Arial", 28f, FontStyle.Bold);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fontrelate"></param>
        /// <returns></returns>
        public static StringFormat TheAlignment(FontRelated fontrelate)
        {
            if (_StrFormatDict.ContainsKey(fontrelate))
            {
                return _StrFormatDict[fontrelate];
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
            _StrFormatDict.Add(fontrelate, format);
            return format;
        }
    }

    public static class SeparateString
    {

        public static int[] GetStringToIntArr(this string strArr, params char[] separator)
        {
            var tmpStrArr = strArr.Split(separator);
            var tmpIntArr = new int[tmpStrArr.Length];
            for (var i = 0; i < tmpIntArr.Length; i++)
            {
                tmpIntArr[i] = int.Parse(tmpStrArr[i]);
            }
            return tmpIntArr;
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="nScore">需要解析的文本</param>
        /// <param name="firstChar">前一个符号</param>
        /// <param name="lastChar">后一个符号</param>
        /// <returns>数据编号列表</returns>
        public static List<int> SplitStr(string nScore, string firstChar, string lastChar)
        {
            var anyList = new List<int>();

            var tmpSouceStr = nScore;    //[发送的数据]
            var tmpStr = string.Empty;   //发送的数据
            var outBoolStr = string.Empty;   //发送的数据.Trim()

            if (StringHelper.findStringByKey(tmpSouceStr, firstChar, lastChar, ref tmpStr))
            {
                outBoolStr = tmpStr.Trim();
            }

            var sendPart = outBoolStr.Split(new[] { ' ', ',' });  //发送的数据解析成数组


            //判断是否解析成功
            foreach (var tmpstr in sendPart)
            {
                int tmpint;
                if (int.TryParse(tmpstr, out tmpint))
                    anyList.Add(tmpint);
            }

            return anyList;
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
            _theRectangleF = rect;
            _theMaxValue = maxValue;
            _theRectDirection = theDirection;

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
            _theBrush = brush;
            float height;
            switch (_theRectDirection)
            {
                case RectRiseDirection.上:
                    _theScale = _theRectangleF.Height / _theMaxValue;
                    height = _isNeedIncrease ? (ReturnTheVariation(ref currentValue) * _theScale) : currentValue * _theScale;
                    gs.FillRectangle(_theBrush,
                        new RectangleF(_theRectangleF.X, _theRectangleF.Y + _theRectangleF.Height - height, _theRectangleF.Width, height));
                    break;
                case RectRiseDirection.下:
                    _theScale = _theRectangleF.Height / _theMaxValue;
                    height = _isNeedIncrease ? (ReturnTheVariation(ref currentValue) * _theScale) : currentValue * _theScale;
                    gs.FillRectangle(_theBrush,
                        new RectangleF(_theRectangleF.X, _theRectangleF.Y, _theRectangleF.Width, height));
                    break;
                case RectRiseDirection.左:
                    _theScale = _theRectangleF.Width / _theMaxValue;
                    height = _isNeedIncrease ? (ReturnTheVariation(ref currentValue) * _theScale) : currentValue * _theScale;
                    gs.FillRectangle(_theBrush,
                        new RectangleF(_theRectangleF.X + _theRectangleF.Width - height, _theRectangleF.Y, height, _theRectangleF.Height));
                    break;
                case RectRiseDirection.右:
                    _theScale = _theRectangleF.Width / _theMaxValue;
                    height = _isNeedIncrease ? (ReturnTheVariation(ref currentValue) * _theScale) : currentValue * _theScale;
                    gs.FillRectangle(_theBrush,
                        new RectangleF(_theRectangleF.X, _theRectangleF.Y, height, _theRectangleF.Height));
                    break;
            }
        }

        private float ReturnTheVariation(ref float theValue)
        {
            if (_viewValue > theValue)
            {
                if (_viewValue - _tmpStrpLength >= theValue)
                    _tmpNeedChangeLength = -TmpStrpLength;
                else
                    _tmpNeedChangeLength = theValue - _viewValue;
            }
            else if (_viewValue < theValue)
            {
                if (_viewValue + _tmpStrpLength <= theValue)
                    _tmpNeedChangeLength = _tmpStrpLength;
                else
                    _tmpNeedChangeLength = theValue - _viewValue;
            }
            else
                _tmpNeedChangeLength = 0;

            _viewValue += _tmpNeedChangeLength;

            return _viewValue;
        }

        //当前要画的高度值
        private float _viewValue;

        //需要增加的高度量
        private float _tmpNeedChangeLength;

        //画笔
        private SolidBrush _theBrush;

        //所要画的矩形条满格状态大小
        private RectangleF _theRectangleF;

        //所要画的矩形条满格状态所表示的值
        private readonly float _theMaxValue;

        //矩形条递增方向
        private readonly RectRiseDirection _theRectDirection;

        //比例尺
        private float _theScale;

        //是否要递增
        private bool _isNeedIncrease;
        /// <summary>
        /// 是否需要有递增的状态
        /// </summary>
        public bool IsNeedIncrease { get { return _isNeedIncrease; } set { _isNeedIncrease = value; } }

        //递增量
        private float _tmpStrpLength = 5;
        /// <summary>
        /// 递增量
        /// </summary>
        public float TmpStrpLength { get { return _tmpStrpLength; } set { _tmpStrpLength = value; } }
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
            _theRect = rect;
            _maxValue = maxValue;
            _minValue = minValue;
            _dirction = dirction;
            _baseOfLog = baseOfLog;
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="e"></param>
        /// <param name="currentValue">当前值</param>
        /// <param name="brush">矩形条颜色</param>
        public void DrawRect(Graphics e, float currentValue, SolidBrush brush)
        {
            _theBrush = brush;
            float rectHeight = GetRectHeight(currentValue);
            switch (_dirction)
            {
                case RectRiseDirection.上:
                    e.FillRectangle(_theBrush,
                        new RectangleF(_theRect.X, _theRect.Y + _theRect.Height - rectHeight, _theRect.Width, rectHeight));
                    break;
                case RectRiseDirection.下:
                    e.FillRectangle(_theBrush,
                        new RectangleF(_theRect.X, _theRect.Y + rectHeight, _theRect.Width, rectHeight));
                    break;
                case RectRiseDirection.左:
                    e.FillRectangle(_theBrush,
                        new RectangleF(_theRect.X - rectHeight, _theRect.Y, rectHeight, _theRect.Height));
                    break;
                case RectRiseDirection.右:
                    e.FillRectangle(_theBrush,
                        new RectangleF(_theRect.X + rectHeight, _theRect.Y, rectHeight, _theRect.Height));
                    break;
            }
        }

        private float GetRectHeight(float currentValue)
        {
            switch (_dirction)
            {
                case RectRiseDirection.上:
                case RectRiseDirection.下:
                    return (float)
                        (_theRect.Height / (Math.Log(_maxValue, _baseOfLog) - Math.Log(_minValue, _baseOfLog))
                               * (Math.Log(currentValue, _baseOfLog) - Math.Log(_minValue, _baseOfLog)));
                case RectRiseDirection.左:
                case RectRiseDirection.右:
                    return (float)
                        (_theRect.Width / (Math.Log(_maxValue, _baseOfLog) - Math.Log(_minValue, _baseOfLog))
                               * (Math.Log(currentValue, _baseOfLog) - Math.Log(_minValue, _baseOfLog)));
            }
            return 0f;
        }

        //
        private RectangleF _theRect;
        //
        private readonly float _maxValue;
        //
        private readonly float _minValue;
        //
        private readonly int _baseOfLog;
        //画笔
        private SolidBrush _theBrush;

        private RectRiseDirection _dirction;
        public RectRiseDirection Dirction 
        {
            set
            {
                _dirction = value;
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
        int _flashCount;

        /// <summary>
        /// 闪烁间隔时间
        /// </summary>
        readonly int _flashTime;

        public FlashTimer(int interval)
        {
            _flashTime = interval;
        }

        /// <summary>
        /// 闪烁判断
        /// </summary>
        /// <returns></returns>
        public bool IsNeedFlash()
        {
            if (_flashCount < _flashTime * 5)
            {
                ++_flashCount;
                return true;
            }
            if (_flashCount >= _flashTime * 5 && _flashCount < _flashTime * 10)
            {
                ++_flashCount;
                return false;
            }
            _flashCount = 0;
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
            _maxTime = maxTime;
        }

        public int Counter()
        {
            if (_maxTime > 0)
            {
                _count++;
                if (_count >= 5)
                {
                    _maxTime--;
                    _count = 0;
                }
            }
            return _maxTime;
        }

        public void CounterOver()
        {
            _maxTime = 0;
            _count = 0;
        }

        public void CounterStart()
        {
            _maxTime = 60;
            _count = 0;
        }

        private int _maxTime;

        private int _count;
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
            _drawFormat.Alignment = (StringAlignment)1;
            _drawFormat.LineAlignment = (StringAlignment)1;

            Point = 中心点;

            _scaleStr = 刻度;
            RectKedu = new PointF[刻度.Length];

            PointKedu1 = new PointF[所有线段数];
            PointKedu2 = new PointF[所有线段数];
            _numb = 所有线段数;

            MinRadian = 每点代表的角度;

            InitRadian = 初始角度;

            _bili = 长短刻度比;

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
            int j = 0;
            for (int i = 0; i < _numb; i++)
            {
                double angle = (i * MinRadian + InitRadian) * Math.PI / 180.0;
                float sinAngle = (float)Math.Sin(angle);
                float cosAngle = (float)Math.Cos(angle);
                if (i % _bili == 0)
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
            for (int i = 0; i < _numb; i++)
            {
                e.DrawLine(i%_bili == 0 ? PenKeduLong : PenKeduShort, PointKedu1[i], PointKedu2[i]);
            }
            for (int i = 0; i < _scaleStr.Length; i++)
            {
                e.DrawString(_scaleStr[i], font, Textbrush, RectKedu[i], _drawFormat);
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
        readonly int _numb;

        /// <summary>
        /// 线段代表的数量
        /// </summary>
        private readonly string[] _scaleStr;

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
        private readonly int _bili;

        readonly StringFormat _drawFormat = new StringFormat();
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
        private readonly Image _drawPic;

        /// <summary>
        /// 需要画的最大角度
        /// </summary>
        private readonly float _dialAnglev;

        /// <summary>
        /// 需要画的最小角度
        /// </summary>
        private readonly float _miniAnglev;

        /// <summary>
        /// 指针初始角度
        /// </summary>
        private readonly float _initalAnglev;

        /// <summary>
        /// 指针最大值
        /// </summary>
        private readonly float _maxSpeed;

        /// <summary>
        /// 指针最小值
        /// </summary>
        private readonly float _miniSpeed;

        /// <summary>
        /// 绘图顶点
        /// </summary>
        private readonly RectangleF _backImgRect;

        /// <summary>
        /// 绘图中心点
        /// </summary>
        private readonly PointF _centralPoint;

        private Matrix _matrix;

        /// <summary>
        /// 转动角度
        /// </summary>
        private float _anglev;

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
            _dialAnglev = maxAnglev;
            _miniAnglev = miniAnglev;
            _initalAnglev = initAnglev;
            _maxSpeed = maxValue;
            _miniSpeed = miniValue;
            _backImgRect = apexRect;
            _centralPoint = centrePoint;
            _drawPic = pointerImg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="speed"></param>
        public void PaintPointerNormal(Graphics g, float speed)
        {
            PaintPointer(g, _drawPic, speed);
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
            if (speed >= _miniSpeed && speed <= _maxSpeed)
            {
                _anglev = (speed - _miniSpeed) / (_maxSpeed - _miniSpeed) * _dialAnglev + _initalAnglev;
            }
            else if (speed < 0 && speed >= _miniSpeed)
            {
                _anglev = speed / _miniSpeed * _miniAnglev + _initalAnglev;
            }
            _matrix = g.Transform;
            _matrix.RotateAt(_anglev, _centralPoint);
            g.Transform = _matrix;
            g.DrawImage(tmpPic, _backImgRect);
            _matrix.Reset();
            g.Transform = _matrix;
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
            _maxAnglev = maxAnglev;
            _initAnglev = initAnglev;
            _maxValue = maxValue;
            _miniValue = miniValue;
            _apexRect = apexRect;
        }

        /// <summary>
        /// 绘制固定起点圆弧
        /// </summary>
        /// <param name="gs"></param>
        /// <param name="speed">所要画的圆弧大小</param>
        /// <param name="thePen">圆弧曲线</param>
        public void PaintCircle(Graphics gs, float speed, Pen thePen)
        {
            if (speed >= _miniValue && speed <= _maxValue)
            {
                _anglev = (speed - _miniValue) / (_maxValue - _miniValue) * _maxAnglev;
            }

            gs.DrawArc(thePen, _apexRect, _initAnglev, _anglev);

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
            if (speed >= _miniValue && speed <= _maxValue)
            {
                _anglev = (speed - _miniValue) / (_maxValue - _miniValue) * _maxAnglev;
            }
            float initAnglev = _initAnglev + ((initValue - _miniValue) / (_maxValue - _miniValue)) * _maxAnglev;
            float test = Math.Abs(_anglev) - (Math.Abs(_initAnglev) - Math.Abs(initAnglev));
            GC.Collect();
            gs.DrawArc(thePen, _apexRect, initAnglev, test);
        }

        public void PaintCircleHook(Graphics gs, float speed, float hookAnglev, Pen thePen)
        {
            gs.DrawArc(thePen, _apexRect, 
                _initAnglev + ((speed - _miniValue) / (_maxValue - _miniValue)) * _maxAnglev - hookAnglev,
                hookAnglev);
        }

        /// <summary>
        /// 最大角度
        /// </summary>
        private readonly float _maxAnglev;

        /// <summary>
        /// 初始角度
        /// </summary>
        private readonly float _initAnglev;

        /// <summary>
        /// 圆弧最大时表示的值
        /// </summary>
        private readonly float _maxValue;

        /// <summary>
        /// 圆弧最小时表示的值
        /// </summary>
        private readonly float _miniValue;

        /// <summary>
        /// 绘图位置
        /// </summary>
        private readonly RectangleF _apexRect;

        /// <summary>
        /// 转动角度
        /// </summary>
        private float _anglev;      

    }

    public class ButtonBar
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="btnRectF">按钮大小</param>
        /// <param name="btnContentStr">按钮显示文字</param>
        /// <param name="btnContentPic">按钮显示图片</param>
        /// <param name="rectPen">是否需要画边线框</param>
        public ButtonBar(RectangleF btnRectF, string btnContentStr, Image btnContentPic, bool rectPen)
        {
            _btnRectF = btnRectF;
            _btnContentStr = string.IsNullOrEmpty(btnContentStr) ? "" : btnContentStr;
            if (btnContentPic != null) _btnContentPic = btnContentPic;
            _isDrawPenRect = rectPen;
        }

        /// <summary>
        /// 绘制按钮
        /// </summary>
        /// <param name="gs"></param>
        public void DrawBtn(Graphics gs)
        {
            gs.FillRectangle(_btnLocked || string.IsNullOrEmpty(_btnContentStr) ?
            _lockedBrush  :  _underPainting, _btnRectF);
            if (_btnContentPic != null)
                gs.DrawImage(_btnContentPic, _btnRectF);
            else
                gs.DrawString(_btnContentStr, FontsItems.Font16_You_B,
                    SolidBrushsItems.WhiteBrush, _btnRectF,
                    FontsItems.TheAlignment(FontRelated.居中));

            if (_isDrawPenRect)
                gs.DrawRectangle(PenItems.WhitePen2, Rectangle.Round(_btnRectF));
        }

        private bool _btnPress;
        /// <summary>
        /// 按钮按下
        /// </summary>
        public bool BtnPress 
        { 
            get { return _btnPress; } 
            set 
            {
                _btnPress = !_btnLocked && value;
            }
        }

        //当前按钮被锁定
        private bool _btnLocked;
        /// <summary>
        /// 按钮锁定
        /// </summary>
        public bool BtnLocked
        {
            get { return _btnLocked; }
            set { _btnLocked = value; }
        }

        private Image _btnContentPic;
        /// <summary>
        /// 按钮内容，均以图片形式定义
        /// </summary>
        public Image BtnContentPic { get { return _btnContentPic; } set { if (!_btnLocked) _btnContentPic = value; } }

        private string _btnContentStr;
        public string BtnContentStr { get { return _btnContentStr; } set { _btnContentStr = value; } }

        //默认底色
        private SolidBrush _underPainting = SolidBrushsItems.BtnAbledBrush;
        /// <summary>
        /// 按钮没有锁定时底色
        /// </summary>
        public SolidBrush UnderPainting { get { return _underPainting; } set { if (!_btnLocked) _underPainting = value; } }

        //按钮被锁定时底色，默认值
        private readonly SolidBrush _lockedBrush = SolidBrushsItems.DarkGrayBrush;

        //按钮内容范围框
        private readonly RectangleF _btnRectF;

        private bool _isDrawPenRect;
        /// <summary>
        /// 是否需要画边线框
        /// </summary>
        public bool IsDrwaPenRect { get { return _isDrawPenRect; } set { _isDrawPenRect = value; } }

    }
}

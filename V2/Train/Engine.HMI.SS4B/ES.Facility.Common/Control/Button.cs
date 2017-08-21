using ES.JCTMS.Common.Control.Common;
using System;
using System.Drawing;

namespace ES.JCTMS.Common.Control
{
    /// <summary>
    ///     功能描述：按钮Style
    ///     创建人：lih
    ///     创建时间：2014-07-14
    /// </summary>
    public class ButtonStyle : IStyle
    {
        /// <summary>
        ///     读取或设置字体样式属性
        /// </summary>
        public FontStyle_ES FontStyle { get; set; }

        /// <summary>
        ///     读取或设置控件响应鼠标左键按下时的背景图片属性
        /// </summary>
        public Image DownImage { get; set; }

        /// <summary>
        ///     读取或设置控件无效时的背景图片属性
        /// </summary>
        public Image DisableImage { get; set; }

        /// <summary>
        ///     读取或设置按钮背景图片属性
        /// </summary>
        public Image Background { get; set; }
    }

    /// <summary>
    ///     功能描述：自定义按钮
    ///     创建人：lih
    ///     创建时间：2014-07-15
    /// </summary>
    public class Button : IControl, IDisposable
    {
        private readonly bool _selfReplication; //按钮自复标识

        /// <summary>
        ///     构造函数：根据按钮文本信息、所在矩形区域、按钮标识、按钮Style、自复标识等信息构造按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        /// <param name="rect">按钮矩形区域</param>
        /// <param name="id">按钮ID</param>
        /// <param name="style">按钮样式</param>
        /// <param name="selfReplication">自复标识，默认为自复按钮</param>
        public Button(string text, RectangleF rect, int id, ButtonStyle style, bool selfReplication = true)
        {
            Text = text;
            _rect = rect;
            ID = id;
            if (style.DisableImage == null)
            {
                style.DisableImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"/Resource/Btn_Disable.png");
            }
            Style = style;
            _selfReplication = selfReplication;

            _tempdownTextBrush = (((ButtonStyle)Style).FontStyle.TextDownBrush != null)
                ? ((ButtonStyle)Style).FontStyle.TextDownBrush
                : ((ButtonStyle)Style).FontStyle.TextBrush;
            _tempupTextBrush = (((ButtonStyle)Style).FontStyle.TextUpBrush != null)
                ? ((ButtonStyle)Style).FontStyle.TextUpBrush
                : ((ButtonStyle)Style).FontStyle.TextBrush;

            _textFont = ((ButtonStyle)Style).FontStyle.Font;

            _textRect = new RectangleF(_rect.X, _rect.Y + 3, _rect.Width, _rect.Height);
            _textSF = ((ButtonStyle)Style).FontStyle.StringFormat;

            _buttonDownImage = ((ButtonStyle)Style).DownImage;
        }

        /// <summary>
        ///     按钮绘制（需在界面绘制函数中实时调用）
        /// </summary>
        /// <param name="dcGs"></param>
        public void Paint(Graphics dcGs)
        {
            if (!_enable) //按钮不能用，绘制灰色背景
            {
                dcGs.DrawImage(((ButtonStyle)Style).DisableImage, _rect);

                dcGs.DrawString(Text, ((ButtonStyle)Style).FontStyle.Font, _enableSolibrush, _textRect, _textSF);

                _isReplication = true;
                return;
            }

            if (!_isReplication) //按钮未复位，绘制按下图片背景
            {
                dcGs.DrawImage(((ButtonStyle)Style).DownImage, _rect);

                dcGs.DrawString(Text, ((ButtonStyle)Style).FontStyle.Font, _tempdownTextBrush, _textRect, _textSF);
                //绘制按钮文本
            }
            else //按钮复位，绘制常规图片背景
            {
                dcGs.DrawImage(Style.Background, _rect);
                dcGs.DrawString(Text, ((ButtonStyle)Style).FontStyle.Font, _tempupTextBrush, _textRect, _textSF);
                //绘制按钮文本
            }
        }

        /// <summary>
        ///     IDisposable接口函数：根据需要添加功能
        /// </summary>
        public void Dispose()
        {
            //按钮释放资源
        }

        /// <summary>
        ///     添加或移除按钮点击事件响应函数属性
        /// </summary>
        public event EventHandle_ClickEvent ClickEvent
        {
            add { _clickEvent += value; }
            remove { _clickEvent -= value; }
        }

        private EventHandle_ClickEvent _clickEvent;

        public event EventHandle_ClickEvent MouseDownEvent
        {
            add { _mouseDownEvent += value; }
            remove { _mouseDownEvent -= value; }
        }

        private EventHandle_ClickEvent _mouseDownEvent;

        /// <summary>
        ///     读取或设置按钮文本属性
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        ///     读取按钮标识ID属性
        /// </summary>
        public int ID { get; } = -1;

        /// <summary>
        ///     读取按钮Style属性（使用时需转换为ButtonStyle）
        /// </summary>
        public IStyle Style { get; set; }

        /// <summary>
        ///     读取或设置按钮使能开关属性
        /// </summary>
        public bool Enable
        {
            get { return _enable; }
            set
            {
                if (_enable == value)
                {
                    return;
                }

                _enable = value;
            }
        }

        private bool _enable = true;

        /// <summary>
        ///     读取或设置按钮所在矩形区域属性
        /// </summary>
        public RectangleF Rect
        {
            get { return _rect; }
            set
            {
                if (_rect == value)
                {
                    return;
                }

                _rect = value;
            }
        }

        private RectangleF _rect;

        /// <summary>
        ///     读取或设置是否获得焦点属性（功能后续添加）
        /// </summary>
        public bool IsFocus
        {
            get { return _isFocus; }
            set
            {
                if (_isFocus == value)
                {
                    return;
                }

                _isFocus = value;
            }
        }

        private bool _isFocus;

        /// <summary>
        ///     读取或设置是否复位属性（按钮是否弹起，用户程序使用该属性实现按钮的复位）
        /// </summary>
        public bool IsReplication
        {
            get { return _isReplication; }
            set
            {
                if (_isReplication == value)
                {
                    return;
                }

                _isReplication = value;
            }
        }

        private bool _isReplication = true;

        private readonly SolidBrush _tempdownTextBrush;
        private readonly SolidBrush _tempupTextBrush;

        private Font _textFont;

        private readonly RectangleF _textRect;
        private readonly StringFormat _textSF;

        private Image _buttonDownImage;
        private readonly SolidBrush _enableSolibrush = new SolidBrush(Color.FromArgb(150, 150, 150));

        /// <summary>
        ///     鼠标按下功能函数（该函数需在界面检测鼠标按下函数中调用）
        /// </summary>
        /// <param name="p">光标所在坐标</param>
        public void MouseDown(Point p)
        {
            if (!_enable) //按钮不可用，函数返回
            {
                return;
            }

            if (_rect.Contains(p)) //光标坐标在按钮矩形区域内，实现按钮按下功能
            {
                if (_mouseDownEvent != null)
                {
                    _mouseDownEvent(this, new ClickEventArgs<int>(ID));
                }
                _isFocus = true; //按钮获得焦点（后续可能会使用到）
                _isReplication = !_isReplication; //按钮复位属性取反
            }
        }

        /// <summary>
        ///     鼠标弹起功能函数（该函数需在界面检测鼠标弹起函数中调用）
        /// </summary>
        /// <param name="p">光标所在坐标</param>
        public void MouseUp(Point p)
        {
            if (!_enable) //按钮不可用，函数返回
            {
                return;
            }

            if (_rect.Contains(p)) //光标坐标在按钮矩形区域内，实现按钮弹起功能
            {
                if (_selfReplication) //为自复按钮，并复位
                {
                    _isReplication = true;
                }

                if (_clickEvent != null) //触发按钮按下事件
                {
                    _clickEvent(this, new ClickEventArgs<int>(ID));
                }
            }
        }
    }
}
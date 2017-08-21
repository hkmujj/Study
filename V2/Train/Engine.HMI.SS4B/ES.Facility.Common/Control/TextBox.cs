using ES.JCTMS.Common.Control.Common;
using System;
using System.Drawing;

namespace ES.JCTMS.Common.Control
{
    /// <summary>
    ///     功能描述：TextBoxStyle
    ///     创建人：lih
    ///     创建时间：2014-07-15
    /// </summary>
    public class TextBoxStyle : IStyle
    {
        /// <summary>
        ///     读取或设置按钮获取焦点时的背景图片属性
        /// </summary>
        public Image BackgroundFocus { get; set; }

        /// <summary>
        ///     读取或设置字体样式属性
        /// </summary>
        public FontStyle_ES FontStyle { get; set; }

        /// <summary>
        ///     背景画笔颜色
        /// </summary>
        public SolidBrush BackgroundBrushes { get; set; }

        /// <summary>
        ///     Group
        ///     读取或设置按钮背景图片属性
        /// </summary>
        public Image Background { get; set; }
    }

    /// <summary>
    ///     功能描述：自定义TextBox
    ///     创建人：lih
    ///     创建时间：2014-07-15
    /// </summary>
    public class TextBox : IControl, IDisposable
    {
        private readonly Pen _framePen;
        private EventHandle_ClickEvent _clickEvent;
        private int _cursorCount;
        private bool _enable = true;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="style"></param>
        /// <param name="id"></param>
        public TextBox(RectangleF rect, TextBoxStyle style, int id)
        {
            _framePen = new Pen(Brushes.Black, 1.6f);
            _rect = rect;
            Style = style;
            ID = id;
        }

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

        /// <summary>
        ///     TextBox控件绘制函数（需在界面Paint函数中调用）
        /// </summary>
        /// <param name="dcGs"></param>
        public void Paint(Graphics dcGs)
        {
            if (((TextBoxStyle)Style).BackgroundBrushes != null)
            {
                dcGs.FillRectangle(((TextBoxStyle)Style).BackgroundBrushes, _rect);
            }

            if (_isFocus)
            {
                if (((TextBoxStyle)Style).BackgroundFocus != null)
                {
                    dcGs.DrawImage(((TextBoxStyle)Style).BackgroundFocus, _rect);
                }
                dcGs.DrawString(
                    Text + cursor(),
                    ((TextBoxStyle)Style).FontStyle.Font,
                    ((TextBoxStyle)Style).FontStyle.TextBrush,
                    _rect,
                    ((TextBoxStyle)Style).FontStyle.StringFormat
                    );
            }
            else
            {
                if (((TextBoxStyle)Style).Background != null)
                {
                    dcGs.DrawImage(((TextBoxStyle)Style).Background, _rect);
                }
                dcGs.DrawString(
                    Text,
                    ((TextBoxStyle)Style).FontStyle.Font,
                    ((TextBoxStyle)Style).FontStyle.TextBrush,
                    _rect,
                    ((TextBoxStyle)Style).FontStyle.StringFormat
                    );
            }
        }

        /// <summary>
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        ///     添加或移除Text点击事件
        /// </summary>
        public event EventHandle_ClickEvent ClickEvent
        {
            add { _clickEvent += value; }
            remove { _clickEvent -= value; }
        }

        /// <summary>
        ///     鼠标按下处理函数（需在界面MouseDown函数中调用）
        /// </summary>
        /// <param name="p">点击点</param>
        public void MouseDown(Point p)
        {
            if (!_enable)
            {
                return;
            }

            if (_rect.Contains(p))
            {
                _isFocus = true;
                if (_clickEvent != null)
                {
                    _clickEvent(this, new ClickEventArgs<int>(ID));
                }
            }
        }

        /// <summary>
        ///     光标计数器方法
        /// </summary>
        /// <returns></returns>
        private string cursor()
        {
            _cursorCount++;
            if (_cursorCount < 4)
            {
                _cursorCount++;
                return "|";
            }
            if (_cursorCount >= 4 && _cursorCount < 8)
            {
                _cursorCount++;
                return "";
            }
            _cursorCount = 0;
            return "";
        }

        /// <summary>
        ///     读取或设置按钮文本属性
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        ///     边框颜色
        /// </summary>
        public SolidBrush FrameBrushes
        {
            get { return _frameBrushes; }
            set
            {
                _frameBrushes = value;
                _framePen.Brush = _frameBrushes;
            }
        }

        private SolidBrush _frameBrushes = new SolidBrush(Color.Black);

        /// <summary>
        ///     读取按钮标识ID属性
        /// </summary>
        public int ID { get; } = -1;

        /// <summary>
        ///     读取按钮Style属性（使用时需转换为ButtonStyle）
        /// </summary>
        public IStyle Style { get; }

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
    }
}
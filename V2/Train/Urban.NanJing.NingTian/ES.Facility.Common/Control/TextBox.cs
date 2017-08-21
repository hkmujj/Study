using System;
using System.Drawing;
using ES.Facility.Common.Control.Common;

namespace ES.Facility.Common.Control
{
    /// <summary>
    /// 功能描述：TextBoxStyle
    /// 创建人：唐林
    /// 创建时间：2014-07-15
    /// </summary>
    public class TextBoxStyle : IStyle
    {
        /// <summary>Group
        /// 读取或设置按钮背景图片属性
        /// </summary>
        public Image Background { get; set; }

        /// <summary>
        /// 读取或设置按钮获取焦点时的背景图片属性
        /// </summary>
        public Image BackgroundFocus { get; set; }

        /// <summary>
        /// 读取或设置字体样式属性
        /// </summary>
        public FontStyle_ES FontStyle { get; set; }
    }

    /// <summary>
    /// 功能描述：自定义TextBox
    /// 创建人：唐林
    /// 创建时间：2014-07-15
    /// </summary>
    public class TextBox : IControl, IDisposable
    {
        private int _cursorCount = 0;

        #region 属性/变量
        /// <summary>
        /// 读取或设置按钮文本属性
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 读取按钮标识ID属性
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// 读取按钮Style属性（使用时需转换为ButtonStyle）
        /// </summary>
        public IStyle Style { get; private set; }

        /// <summary>
        /// 读取或设置按钮所在矩形区域属性
        /// </summary>
        public RectangleF Rect
        {
            get { return _rect; }
            set
            {
                if (_rect == value)
                    return;

                _rect = value;
            }
        }
        private RectangleF _rect;

        /// <summary>
        /// 读取或设置是否获得焦点属性（功能后续添加）
        /// </summary>
        public bool IsFocus
        {
            get { return _isFocus; }
            set
            {
                if (_isFocus == value)
                    return;

                _isFocus = value;
            }
        }
        private bool _isFocus = false;
        #endregion

        /// <summary>
        /// 添加或移除Text点击事件
        /// </summary>
        public event EventHandle_ClickEvent ClickEvent
        {
            add { _clickEvent += value; }
            remove { _clickEvent -= value; }
        }
        private EventHandle_ClickEvent _clickEvent;

        /// <summary>
        /// 读取或设置按钮使能开关属性
        /// </summary>
        public bool Enable
        {
            get { return _enable; }
            set
            {
                if (_enable == value)
                    return;

                _enable = value;
            }
        }
        private bool _enable = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="style"></param>
        /// <param name="id"></param>
        public TextBox(RectangleF rect, TextBoxStyle style, int id)
        {
            Text = string.Empty;
            _rect = rect;
            Style = style;
            ID = id;
        }

        /// <summary>
        /// 鼠标按下处理函数（需在界面MouseDown函数中调用）
        /// </summary>
        /// <param name="p">点击点</param>
        public void MouseDown(Point p)
        {
            if (!_enable)
                return;

            if (_rect.Contains(p))
            {
                _isFocus = true;
                if (_clickEvent != null)
                    _clickEvent(this, new ClickEventArgs<int>(ID));
            }
        }

        /// <summary>
        /// TextBox控件绘制函数（需在界面Paint函数中调用）
        /// </summary>
        /// <param name="g"></param>
        public void Paint(Graphics g)
        {
            if (_isFocus)
            {
                g.DrawImage(((TextBoxStyle)Style).BackgroundFocus, _rect);
                g.DrawString(
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
                    g.DrawImage(((TextBoxStyle)Style).Background, _rect);
                g.DrawString(
                    Text,
                    ((TextBoxStyle)Style).FontStyle.Font,
                    ((TextBoxStyle)Style).FontStyle.TextBrush,
                    _rect,
                    ((TextBoxStyle)Style).FontStyle.StringFormat
                    );
            }
        }

        /// <summary>
        /// 光标计数器方法
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
            else if (_cursorCount >= 4 && _cursorCount < 8)
            {
                _cursorCount++;
                return "";
            }
            else
            {
                _cursorCount = 0;
                return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
        }
    }
}

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
        private Int32 _cursorCount = 0;

        #region 属性/变量
        /// <summary>
        /// 读取或设置按钮文本属性
        /// </summary>
        public String Text
        {
            get { return this._text; }
            set { this._text = value; }
        }
        private String _text = String.Empty;

        /// <summary>
        /// 读取按钮标识ID属性
        /// </summary>
        public Int32 ID
        {
            get { return this._id; }
        }
        private Int32 _id = -1;

        /// <summary>
        /// 读取按钮Style属性（使用时需转换为ButtonStyle）
        /// </summary>
        public IStyle Style
        {
            get { return _style; }
        }
        private IStyle _style;

        /// <summary>
        /// 读取或设置按钮所在矩形区域属性
        /// </summary>
        public RectangleF Rect
        {
            get { return this._rect; }
            set
            {
                if (this._rect == value)
                    return;

                this._rect = value;
            }
        }
        private RectangleF _rect;

        /// <summary>
        /// 读取或设置是否获得焦点属性（功能后续添加）
        /// </summary>
        public Boolean IsFocus
        {
            get { return this._isFocus; }
            set
            {
                if (this._isFocus == value)
                    return;

                this._isFocus = value;
            }
        }
        private Boolean _isFocus = false;
        #endregion

        /// <summary>
        /// 添加或移除Text点击事件
        /// </summary>
        public event EventHandle_ClickEvent ClickEvent
        {
            add { this._clickEvent += value; }
            remove { this._clickEvent -= value; }
        }
        private EventHandle_ClickEvent _clickEvent;

        /// <summary>
        /// 读取或设置按钮使能开关属性
        /// </summary>
        public Boolean Enable
        {
            get { return this._enable; }
            set
            {
                if (this._enable == value)
                    return;

                this._enable = value;
            }
        }
        private Boolean _enable = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="style"></param>
        /// <param name="id"></param>
        public TextBox(RectangleF rect, TextBoxStyle style, Int32 id)
        {
            this._rect = rect;
            this._style = style;
            this._id = id;
        }

        /// <summary>
        /// 鼠标按下处理函数（需在界面MouseDown函数中调用）
        /// </summary>
        /// <param name="p">点击点</param>
        public void MouseDown(Point p)
        {
            if (!this._enable)
                return;

            if (_rect.Contains(p))
            {
                this._isFocus = true;
                if (this._clickEvent != null)
                    this._clickEvent(this, new ClickEventArgs<int>(this._id));
            }
        }

        /// <summary>
        /// TextBox控件绘制函数（需在界面Paint函数中调用）
        /// </summary>
        /// <param name="dcGs"></param>
        public void Paint(Graphics dcGs)
        {
            if (this._isFocus)
            {
                dcGs.DrawImage(((TextBoxStyle)this.Style).BackgroundFocus, this._rect);
                dcGs.DrawString(
                    this._text + this.cursor(),
                    ((TextBoxStyle)this.Style).FontStyle.Font,
                    ((TextBoxStyle)this.Style).FontStyle.TextBrush,
                    this._rect,
                    ((TextBoxStyle)this.Style).FontStyle.StringFormat
                    );
            }
            else
            {
                if (((TextBoxStyle)this.Style).Background != null)
                    dcGs.DrawImage(((TextBoxStyle)this.Style).Background, this._rect);
                dcGs.DrawString(
                    this._text,
                    ((TextBoxStyle)this.Style).FontStyle.Font,
                    ((TextBoxStyle)this.Style).FontStyle.TextBrush,
                    this._rect,
                    ((TextBoxStyle)this.Style).FontStyle.StringFormat
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

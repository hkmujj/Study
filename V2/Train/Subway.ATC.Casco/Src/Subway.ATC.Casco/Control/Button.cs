using System;
using System.Drawing;
using Subway.ATC.Casco.Control.Common;

namespace Subway.ATC.Casco.Control
{
    /// <summary>
    /// 功能描述：按钮Style
    /// 创建人：唐林
    /// 创建时间：2014-07-14
    /// </summary>
    public class ButtonStyle : IStyle
    {
        /// <summary>
        /// 读取或设置字体样式属性
        /// </summary>
        public FontStyle_ES FontStyle { get; set; }

        /// <summary>
        /// 读取或设置控件响应鼠标左键按下时的背景图片属性
        /// </summary>
        public Image DownImage { get; set; }

        /// <summary>
        /// 读取或设置控件无效时的背景图片属性
        /// </summary>
        public Image DisableImage { get; set; }

        /// <summary>
        /// 读取或设置按钮背景图片属性
        /// </summary>
        public Image Background { get; set; }
    }

    /// <summary>
    /// 功能描述：自定义按钮
    /// 创建人：唐林
    /// 创建时间：2014-07-15
    /// </summary>
    public class Button : IControl, IDisposable
    {
        private bool _selfReplication;//按钮自复标识

        #region 事件/代理
        /// <summary>
        /// 添加或移除按钮点击事件响应函数属性
        /// </summary>
        public event EventHandle_ClickEvent ClickEvent
        {
            add { this._clickEvent += value; }
            remove { this._clickEvent -= value; }
        }
        private EventHandle_ClickEvent _clickEvent;

        public event EventHandle_ClickEvent MouseDownEvent
        {
            add { this._mouseDownEvent += value; }
            remove { this._mouseDownEvent -= value; }
        }
        private EventHandle_ClickEvent _mouseDownEvent;
        #endregion

        #region 属性/变量
        /// <summary>
        /// 读取或设置按钮文本属性
        /// </summary>
        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }
        private string _text = string.Empty;

        /// <summary>
        /// 读取按钮标识ID属性
        /// </summary>
        public int ID
        {
            get { return this._id; }
        }
        private int _id = -1;

        /// <summary>
        /// 读取按钮Style属性（使用时需转换为ButtonStyle）
        /// </summary>
        public IStyle Style
        {
            get { return _style; }
        }
        private IStyle _style;

        /// <summary>
        /// 读取或设置按钮使能开关属性
        /// </summary>
        public bool Enable
        {
            get { return this._enable; }
            set
            {
                if (this._enable == value)
                    return;

                this._enable = value;
            }
        }
        private bool _enable = true;

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
        public bool IsFocus
        {
            get { return this._isFocus; }
            set
            {
                if (this._isFocus == value)
                    return;

                this._isFocus = value;
            }
        }
        private bool _isFocus = false;

        /// <summary>
        /// 读取或设置是否复位属性（按钮是否弹起，用户程序使用该属性实现按钮的复位）
        /// </summary>
        public bool IsReplication
        {
            get { return this._isReplication; }
            set
            {
                if (this._isReplication == value)
                    return;

                this._isReplication = value;
            }
        }
        private bool _isReplication = true;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数：根据按钮文本信息、所在矩形区域、按钮标识、按钮Style、自复标识等信息构造按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        /// <param name="rect">按钮矩形区域</param>
        /// <param name="id">按钮ID</param>
        /// <param name="style">按钮样式</param>
        /// <param name="selfReplication">自复标识，默认为自复按钮</param>
        public Button(string text, RectangleF rect, int id, ButtonStyle style, bool selfReplication = true)
        {
            this._text = text;
            this._rect = rect;
            this._id = id;
            if (style.DisableImage == null) style.DisableImage = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/Resource/Btn_Disable.png");
            this._style = style;
            this._selfReplication = selfReplication;
        }
        #endregion

        #region 鼠标检测函数
        /// <summary>
        /// 鼠标按下功能函数（该函数需在界面检测鼠标按下函数中调用）
        /// </summary>
        /// <param name="p">光标所在坐标</param>
        public void MouseDown(Point p)
        {
            if (!this._enable)//按钮不可用，函数返回
                return;

            if (this._rect.Contains(p))//光标坐标在按钮矩形区域内，实现按钮按下功能
            {
                if(_mouseDownEvent!=null)
                    _mouseDownEvent(this, new ClickEventArgs<int>(this._id));
                this._isFocus = true;//按钮获得焦点（后续可能会使用到）
                this._isReplication = !this._isReplication;//按钮复位属性取反
            }
        }

        /// <summary>
        /// 鼠标弹起功能函数（该函数需在界面检测鼠标弹起函数中调用）
        /// </summary>
        /// <param name="p">光标所在坐标</param>
        public void MouseUp(Point p)
        {
            if (!this._enable)//按钮不可用，函数返回
                return;

            if (this._rect.Contains(p))//光标坐标在按钮矩形区域内，实现按钮弹起功能
            {
                if (this._selfReplication)//为自复按钮，并复位
                {
                    this._isReplication = true;
                }

                if (this._clickEvent != null)//触发按钮按下事件
                    this._clickEvent(this, new ClickEventArgs<int>(this._id));
            }
        }
        #endregion

        #region 按钮绘制函数
        /// <summary>
        /// 按钮绘制（需在界面绘制函数中实时调用）
        /// </summary>
        /// <param name="dcGs"></param>
        public void Paint(Graphics dcGs)
        {
            if (!this._enable)//按钮不能用，绘制灰色背景
            {
                dcGs.DrawImage(((ButtonStyle)this._style).DisableImage, this._rect);
                dcGs.DrawString(
                    this._text,
                    ((ButtonStyle)this._style).FontStyle.Font,
                    new SolidBrush(Color.FromArgb(150, 150, 150)),
                    new RectangleF(this._rect.X, this._rect.Y + 3, this._rect.Width, this._rect.Height),
                    ((ButtonStyle)this._style).FontStyle.StringFormat
                    );
                this._isReplication = true;
                return;
            }

            if (!this._isReplication)//按钮未复位，绘制按下图片背景
            {
                dcGs.DrawImage(((ButtonStyle)this._style).DownImage, this._rect);
            }
            else//按钮复位，绘制常规图片背景
            {
                dcGs.DrawImage(this._style.Background, this._rect);
            }
            dcGs.DrawString(
                this._text,
                ((ButtonStyle)this._style).FontStyle.Font,
                ((ButtonStyle)this._style).FontStyle.TextBrush,
                new RectangleF(this._rect.X, this._rect.Y + 3, this._rect.Width, this._rect.Height),
                ((ButtonStyle)this._style).FontStyle.StringFormat
                );//绘制按钮文本
        }
        #endregion

        #region IDisposable接口函数
        /// <summary>
        /// IDisposable接口函数：根据需要添加功能
        /// </summary>
        public void Dispose()
        {
            //按钮释放资源
        }
        #endregion
    }
}

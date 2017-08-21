using ES.JCTMS.Common.Control.Common;
using System.Drawing;

namespace ES.JCTMS.Common.Control
{
    public class LabelStyle : IStyle
    {
        /// <summary>
        ///     读取或设置字体样式属性
        /// </summary>
        public FontStyle_ES FontStyle { get; set; }

        /// <summary>
        ///     读取或设置背景画刷属性（当不使用图片作为背景时，使用画刷对背景进行绘制）
        /// </summary>
        public Brush BackgroundBrush { get; set; }

        /// <summary>
        ///     读取或设置边框画笔属性（不设置该属性，控件将没有边框）
        /// </summary>
        public Pen BorderPen { get; set; }

        /// <summary>
        ///     读取或设置控件背景图片属性
        /// </summary>
        public Image Background { get; set; }
    }

    /// <summary>
    ///     功能描述：文本显示框
    ///     创建人：lih
    ///     创建时间：2014-07-16
    /// </summary>
    public class Label : IControl
    {
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="rect">所在矩形区域</param>
        /// <param name="style">样式</param>
        /// <param name="id">id</param>
        public Label(string text, RectangleF rect, LabelStyle style, int id)
        {
            Text = text;
            _rect = rect;
            Style = style;
            ID = id;
        }

        /// <summary>
        ///     Label绘制（需在界面Paint函数中调用）
        /// </summary>
        /// <param name="dcGs"></param>
        public void Paint(Graphics dcGs)
        {
            //if (((LabelStyle)this._style).Background != null)
            //{
            //    dcGs.DrawImage(((LabelStyle)this._style).Background, this._rect);
            //}
            //else if (((LabelStyle)this._style).BackgroundBrush != null)
            //{
            //    dcGs.DrawRectangle(new Pen(new SolidBrush(Color.Black)), _rect.X, _rect.Y, _rect.Width, _rect.Height);
            //    dcGs.FillRectangle(((LabelStyle)this._style).BackgroundBrush, new RectangleF(_rect.X + 1, _rect.Y + 1, _rect.Width - 2, _rect.Height - 2));
            //}
            //else
            //{
            //    dcGs.DrawRectangle(new Pen(new SolidBrush(Color.Black)), _rect.X, _rect.Y, _rect.Width, _rect.Height);
            //}

            if (((LabelStyle)Style).BorderPen != null)
            {
                dcGs.DrawRectangle(((LabelStyle)Style).BorderPen, _rect.X, _rect.Y, _rect.Width, _rect.Height);
                if (((LabelStyle)Style).Background != null)
                {
                    dcGs.DrawImage(((LabelStyle)Style).Background,
                        new RectangleF(_rect.X + 1, _rect.Y + 1, _rect.Width - 1, _rect.Height - 1));
                }
                else if (((LabelStyle)Style).BackgroundBrush != null)
                {
                    dcGs.FillRectangle(((LabelStyle)Style).BackgroundBrush,
                        new RectangleF(_rect.X + 1, _rect.Y + 1, _rect.Width - 1, _rect.Height - 1));
                }
            }
            else
            {
                if (((LabelStyle)Style).Background != null)
                {
                    dcGs.DrawImage(((LabelStyle)Style).Background, _rect);
                }
                else if (((LabelStyle)Style).BackgroundBrush != null)
                {
                    dcGs.FillRectangle(((LabelStyle)Style).BackgroundBrush, _rect);
                }
            }

            dcGs.DrawString(
                Text,
                ((LabelStyle)Style).FontStyle.Font,
                ((LabelStyle)Style).FontStyle.TextBrush,
                new RectangleF(_rect.X - 2, _rect.Y + 2, _rect.Width + 4, _rect.Height),
                ((LabelStyle)Style).FontStyle.StringFormat
                );
        }

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
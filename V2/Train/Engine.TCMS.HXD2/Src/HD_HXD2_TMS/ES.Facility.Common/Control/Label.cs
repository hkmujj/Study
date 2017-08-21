using System;
using System.Drawing;
using ES.Facility.Common.Control.Common;

namespace ES.Facility.Common.Control
{
    public class LabelStyle:IStyle
    {
        /// <summary>
        /// 读取或设置控件背景图片属性
        /// </summary>
        public Image Background { get; set; }

        /// <summary>
        /// 读取或设置字体样式属性
        /// </summary>
        public FontStyle_ES FontStyle { get; set; }

        /// <summary>
        /// 读取或设置背景画刷属性（当不使用图片作为背景时，使用画刷对背景进行绘制）
        /// </summary>
        public Brush BackgroundBrush { get; set; }
        /// <summary>
        /// 读取或设置边框画笔属性（不设置该属性，控件将没有边框）
        /// </summary>
        public Pen BorderPen { get; set; }
    }

    /// <summary>
    /// 功能描述：文本显示框
    /// 创建人：唐林
    /// 创建时间：2014-07-16
    /// </summary>
    public class Label : IControl
    {
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
        /// 构造函数
        /// </summary>
        /// <param name="rect">所在矩形区域</param>
        /// <param name="style">样式</param>
        /// <param name="id">id</param>
        public Label(String text,RectangleF rect,LabelStyle style,Int32 id)
        {
            this._text = text;
            this._rect = rect;
            this._style = style;
            this._id = id;
        }

        /// <summary>
        /// Label绘制（需在界面Paint函数中调用）
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

            if (((LabelStyle)this._style).BorderPen != null)
            {
                dcGs.DrawRectangle(((LabelStyle)this._style).BorderPen, _rect.X, _rect.Y, _rect.Width, _rect.Height);
                if (((LabelStyle)this._style).Background != null)
                {
                    dcGs.DrawImage(((LabelStyle)this._style).Background, new RectangleF(_rect.X + 1, _rect.Y + 1, _rect.Width - 1, _rect.Height - 1));
                }
                else if (((LabelStyle)this._style).BackgroundBrush != null)
                {
                    dcGs.FillRectangle(((LabelStyle)this._style).BackgroundBrush, new RectangleF(_rect.X + 1, _rect.Y + 1, _rect.Width - 1, _rect.Height - 1));
                }
            }
            else
            {
                if (((LabelStyle)this._style).Background != null)
                {
                    dcGs.DrawImage(((LabelStyle)this._style).Background, this._rect);
                }
                else if (((LabelStyle)this._style).BackgroundBrush != null)
                {
                    dcGs.FillRectangle(((LabelStyle)this._style).BackgroundBrush, this._rect);
                }
            }

            dcGs.DrawString(
                this._text, 
                ((LabelStyle)this._style).FontStyle.Font, 
                ((LabelStyle)this._style).FontStyle.TextBrush, 
                new RectangleF(_rect.X-2, _rect.Y + 2, _rect.Width+4, _rect.Height), 
                ((LabelStyle)this._style).FontStyle.StringFormat
                );
        }
    }
}

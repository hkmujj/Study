using System.Drawing;

namespace CommonUtil.Controls
{
    /// <summary>
    /// 无边框，无背景的字符
    /// </summary>
    public class Label : CommonInnerControlBase
    {
        /// <summary>
        /// 内容
        /// </summary>
        public Brush Brush { set; get; }

        /// <summary>
        /// 文本字体
        /// </summary>
        public Font Font { set; get; }

        /// <summary>
        /// 文本内容
        /// </summary>
        public string Text { set; get; }

        /// <summary>
        ///  文本格式, 默认居中
        /// </summary>
        public StringFormat Format { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Label()
        {
            Brush = Brushes.Black;
            Font = SystemFonts.DefaultFont;
            Format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            if (!Visible)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(Text) && Font != null && Brush != null && Format != null)
            {
                g.DrawString(Text, Font, Brush, OutLineRectangle, Format);
            }
        }
    }
}
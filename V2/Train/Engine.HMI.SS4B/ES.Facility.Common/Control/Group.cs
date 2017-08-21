using System.Drawing;

namespace ES.JCTMS.Common.Control
{
    /// <summary>
    ///     功能描述：自定义“组”
    ///     创建人：lih
    ///     创建时间：2014-07-16
    /// </summary>
    public class Group
    {
        private readonly Font _font; //字体
        private readonly StringFormat _sf; //对齐方式
        private readonly string _text = string.Empty; //标题
        private Rectangle _rect; //矩形区域

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="text">标题</param>
        /// <param name="font">字体</param>
        /// <param name="rect">矩形区域</param>
        public Group(string text, Font font, Rectangle rect)
        {
            _text = text;
            _rect = rect;
            _font = font;
            _sf = new StringFormat();
            _sf.Alignment = StringAlignment.Center;
            _sf.LineAlignment = StringAlignment.Center;
        }

        /// <summary>
        ///     控件绘制（需在界面Paint函数中调用）
        /// </summary>
        /// <param name="dcGs"></param>
        public void Paint(Graphics dcGs)
        {
            dcGs.DrawRectangle(new Pen(Color.White), _rect);
            dcGs.FillRectangle(new SolidBrush(Color.Black),
                new RectangleF(_rect.X + _rect.Width / 2 - 80, _rect.Y - 15, 160, 30));
            dcGs.DrawString(_text, _font, new SolidBrush(Color.White),
                new RectangleF(_rect.X - 3, _rect.Y - 10, _rect.Width, 20), _sf);
        }
    }
}
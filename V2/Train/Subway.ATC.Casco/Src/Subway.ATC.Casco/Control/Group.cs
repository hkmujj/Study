using System.Drawing;

namespace Subway.ATC.Casco.Control
{
    /// <summary>
    /// 功能描述：自定义“组”
    /// 创建人：唐林
    /// 创建时间：2014-07-16
    /// </summary>
    public class Group
    {
        private string _text = string.Empty;//标题
        private Rectangle _rect;//矩形区域
        private Font _font;//字体
        private StringFormat _sf;//对齐方式

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text">标题</param>
        /// <param name="font">字体</param>
        /// <param name="rect">矩形区域</param>
        public Group(string text, Font font, Rectangle rect)
        {
            this._text = text;
            this._rect = rect;
            this._font = font;
            this._sf = new StringFormat();
            this._sf.Alignment = StringAlignment.Center;
            this._sf.LineAlignment = StringAlignment.Center;
        }

        /// <summary>
        /// 控件绘制（需在界面Paint函数中调用）
        /// </summary>
        /// <param name="dcGs"></param>
        public void Paint(Graphics dcGs)
        {
            dcGs.DrawRectangle(new Pen(Color.White), this._rect);
            dcGs.FillRectangle(new SolidBrush(Color.Black), new RectangleF(_rect.X + _rect.Width / 2 - 80, _rect.Y - 15, 160, 30));
            dcGs.DrawString(this._text, this._font, new SolidBrush(Color.White), new RectangleF(_rect.X - 3, _rect.Y - 10, _rect.Width, 20), this._sf);
        }
    }
}

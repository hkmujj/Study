using System;
using System.Drawing;

namespace ES.Facility.Common.Control
{
    /// <summary>
    /// 功能描述：自定义“组”
    /// 创建人：唐林
    /// 创建时间：2014-07-16
    /// </summary>
    public class Group
    {
        private String m_Text = String.Empty;//标题
        private Rectangle m_Rect;//矩形区域
        private Font m_Font;//字体
        private StringFormat m_Sf;//对齐方式

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text">标题</param>
        /// <param name="font">字体</param>
        /// <param name="rect">矩形区域</param>
        public Group(String text, Font font, Rectangle rect)
        {
            m_Text = text;
            m_Rect = rect;
            m_Font = font;
            m_Sf = new StringFormat();
            m_Sf.Alignment = StringAlignment.Center;
            m_Sf.LineAlignment = StringAlignment.Center;
        }

        /// <summary>
        /// 控件绘制（需在界面Paint函数中调用）
        /// </summary>
        /// <param name="g"></param>
        public void Paint(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.White), m_Rect);
            g.FillRectangle(new SolidBrush(Color.Black), new RectangleF(m_Rect.X + m_Rect.Width / 2 - 80, m_Rect.Y - 15, 160, 30));
            g.DrawString(m_Text, m_Font, new SolidBrush(Color.White), new RectangleF(m_Rect.X - 3, m_Rect.Y - 10, m_Rect.Width, 20), m_Sf);
        }
    }
}

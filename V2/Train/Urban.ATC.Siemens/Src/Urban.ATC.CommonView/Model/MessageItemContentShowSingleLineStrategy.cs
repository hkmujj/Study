using System.Drawing;
using Motor.ATP.CommonView.Interface;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.CommonView.View.RegionE;

namespace Motor.ATP.CommonView.Model
{
    /// <summary>
    /// 只显示一行
    /// </summary>
    public class MessageItemContentShowSingleLineStrategy : IMessageItemContentShowStrategy
    {
        public int LineCount { get { return 1; } }

        private readonly StringFormat m_StringFormat = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };

        public void Paint(MessageItemControl control, Graphics g)
        {
            var font = FindModestFont(g, control);
            g.DrawString(control.MessageItem.GetDisplayContent(), font, control.ContentBrush, control.GetActureRectangle(), m_StringFormat);
        }

        private Font FindModestFont(Graphics g, MessageItemControl control)
        {
            var fontProto = control.Font;

            var content = control.MessageItem.GetDisplayContent();
            for (var i = (int) fontProto.Size; i > 0; --i)
            {
                var font = new Font(fontProto.FontFamily, i);
                var size = g.MeasureString(content, font);
                if (size.Width <= control.Width && size.Height <= control.Height)
                {
                    return font;
                }
                font.Dispose();
            }
            return new Font(fontProto.FontFamily, 1);
        }
    }
}
using System.Diagnostics.Contracts;
using System.Drawing;
using Motor.ATP.CommonView.Interface;
using Motor.ATP.CommonView.View.RegionE;

namespace Motor.ATP.CommonView.Model
{
    public class MessageItemContentShowMutiLineStrategy : IMessageItemContentShowStrategy
    {
        public int LineCount { get; private set; }

        public void Paint(MessageItemControl control, Graphics g)
        {
            Contract.Requires(control.MessageItem != null);
            var display = control.MessageItem.GetDisplayContent();
            LineCount = 1;
            if (g.MeasureString(display, control.Font).Width > control.Width)
            {
                LineCount = 2;
                control.Height = control.Height * 2;
            }
        }
    }
}
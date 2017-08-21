using System.Drawing;
using CommonUtil.Controls;

namespace CRH2MMI.PowerVoltage
{
    internal class PowerVoltageUnit
    {
        public GDIRectText ValueText { set; get; }

        public Progress ValueBar { set; get; }

        public void OnPaint(Graphics g)
        {
            ValueText.OnPaint(g);

            ValueBar.OnPaint(g);
        }
    }
}

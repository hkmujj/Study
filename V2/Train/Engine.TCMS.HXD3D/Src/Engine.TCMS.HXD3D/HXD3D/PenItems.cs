using System.Drawing;

namespace Engine.TCMS.HXD3D.HXD3D
{
    public static class PenItems
    {
        // Fields
        public static readonly Pen YellowPen = new Pen(SolidBrushsItems.YellowBrush1, 3f);

        // Methods
        public static Pen GetThePen(SolidBrush theColor, float theWidth)
        {
            return new Pen(theColor, theWidth);
        }
    }
}
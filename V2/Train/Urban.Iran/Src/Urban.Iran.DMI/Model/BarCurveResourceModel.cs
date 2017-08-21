using System.Diagnostics;
using System.Drawing;

namespace Urban.Iran.DMI.Model
{
    public class BarCurveResourceModel 
    {
        public Pen Pen { private set; get; }

        public Rectangle OutLineRectangle { set; get; }

        public SpeedState SpeedState { private set; get; }

        [DebuggerStepThrough]
        public BarCurveResourceModel(SpeedState speedState, Pen pen, Rectangle outLineRectangle)
        {
            Pen = pen;
            OutLineRectangle = outLineRectangle;
            SpeedState = speedState;
        }
    }
}
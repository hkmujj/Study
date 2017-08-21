using System.Drawing;
using System.Windows.Forms;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Domain.Model.SpeedMonitoringSection
{
    public class CoordinateAxis : ICoordinateAxis
    {
        public CoordinateAxis(float value, float locationPercent, Padding padding, string text = null)
        {
            Text = text;
            Value = value;
            Padding = padding;
            LocationPercent = locationPercent;
        }

        public float Value { get; set; }

        public float LocationPercent { get; set; }

        public Padding Padding { get; private set; }

        public Pen AxisPen { get; set; }

        public string Text { get; set; }

        public Font TextFont { get; set; }

        public StringFormat TextFormat { get; set; }

        public Brush TextBrush { get; set; }
    }
}
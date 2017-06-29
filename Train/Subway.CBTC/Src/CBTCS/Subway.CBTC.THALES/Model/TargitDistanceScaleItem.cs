using System.Diagnostics;

namespace Subway.CBTC.THALES.Model
{

    [DebuggerDisplay("DegreeLength={DegreeLength},Value={Value},DegreeLocation={DegreeLocation}")]
    public class TargitDistanceScaleItem : ITargitDistanceScaleItem
    {
        public double Value { get; set; }
        public double DegreeLength { get; set; }
        public double DegreeLocation { get; set; }
        public string Text { get; set; }
        [DebuggerStepThrough]
        public TargitDistanceScaleItem(double value, double degreeLength, double degreeLocation, string text = "")
        {
            Value = value;
            DegreeLength = degreeLength;
            DegreeLocation = degreeLocation;
            Text = text;
        }

    }
}

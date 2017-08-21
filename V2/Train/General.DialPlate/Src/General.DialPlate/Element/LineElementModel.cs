using System.Diagnostics;
using System.Drawing;

namespace General.DialPlate.Element
{
    [DebuggerDisplay(
        "ImageName={ImageName} Region={Region} LogicIndex={LogicIndex} MinValue={MinValue} MaxValue={MaxValue}")]
    public class LineElementModel : IElementModel
    {
        public LineElementModel(string imageName, RectangleF region, int logicIndex, float minValue, float maxValue,
            float minValueDrawAngle, float maxValueDrawAngle, bool visibleWhenLessThanMin = true, bool visibleWhenLargerThanMax = true)
        {
            ImageName = imageName;
            Region = region;
            LogicIndex = logicIndex;
            MinValue = minValue;
            MaxValue = maxValue;
            MinValueDrawAngle = minValueDrawAngle;
            MaxValueDrawAngle = maxValueDrawAngle;
            VisibleWhenLessThanMin = visibleWhenLessThanMin;
            VisibleWhenLargerThanMax = visibleWhenLargerThanMax;
        }

        public string ImageName { private set; get; }

        public RectangleF Region { private set; get; }

        public int LogicIndex { private set; get; }

        public float MinValue { private set; get; }

        public float MaxValue { private set; get; }

        public float MinValueDrawAngle { private set; get; }

        public float MaxValueDrawAngle { private set; get; }

        public bool VisibleWhenLessThanMin { private set; get; }

        public bool VisibleWhenLargerThanMax { private set; get; }

        public float ConvertValueToDrawAngle(float value)
        {
            return (value - MinValue) * (MaxValueDrawAngle - MinValueDrawAngle) / (MaxValue - MinValue) + MinValueDrawAngle;
        }
    }
}
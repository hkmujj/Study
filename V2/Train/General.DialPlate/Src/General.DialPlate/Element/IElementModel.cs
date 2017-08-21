using System.Drawing;

namespace General.DialPlate.Element
{
    public interface IElementModel
    {
        string ImageName {   get; }

        RectangleF Region {   get; }

        int LogicIndex {   get; }

        float MinValue {   get; }

        float MaxValue {   get; }

        float MinValueDrawAngle {   get; }

        float MaxValueDrawAngle {   get; }


        bool VisibleWhenLessThanMin {  get; }

        bool VisibleWhenLargerThanMax {  get; }


        float ConvertValueToDrawAngle(float value);
    }
}
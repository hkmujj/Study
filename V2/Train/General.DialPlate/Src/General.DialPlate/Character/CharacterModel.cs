using System.Drawing;

namespace General.DialPlate.Character
{
    public class CharacterModel
    {
        public CharacterModel(Font font, Color color, StringFormat stringFormat, RectangleF rectangleF, int logicIndex, string numberFormat, bool outlineVisible, float rotateAngle)
        {
            Font = font;
            Color = color;
            StringFormat = stringFormat;
            RectangleF = rectangleF;
            LogicIndex = logicIndex;
            NumberFormat = numberFormat;
            OutlineVisible = outlineVisible;
            RotateAngle = rotateAngle;
        }

        public Font Font { private set; get; }

        public Color Color { private set; get; }

        public StringFormat StringFormat { private set; get; }

        public RectangleF RectangleF { private set; get; }

        public int LogicIndex { private set; get; }

        public string NumberFormat { private set; get; }

        public bool OutlineVisible { private set; get; }

        public float RotateAngle { private set; get; }
    }
}
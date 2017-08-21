using Engine.TPX21F.HXN5B.Model.Interface;

namespace Engine.TPX21F.HXN5B.Model.ViewSource
{
    public class ValueDialPlateDegree : IValueDialPlateDegree
    {
        public ValueDialPlateDegree(float value, float angle, float lenght, string text = null)
        {
            Text = text;
            Lenght = lenght;
            Angle = angle;
            Value = value;
        }

        public float Value { get; private set; }

        public float Angle { get; private set; }

        public float Lenght { get; private set; }

        public string Text { get; private set; }
    }
}
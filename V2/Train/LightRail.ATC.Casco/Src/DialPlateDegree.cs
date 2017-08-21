using System.Diagnostics;
using System.Drawing;

namespace LightRail.ATC.Casco
{
    [DebuggerDisplay("Speed={Speed} Angle={Angle} Legth={Legth} Text={Text}")]
    public class DialPlateDegree
    {
        [DebuggerStepThrough]
        public DialPlateDegree(float speed, float angle, float legth, Pen degreePen, string text, Brush textBrush, Font txtFont)
        {
            Speed = speed;
            Angle = angle;
            Legth = legth;
            Text = text;
            DegreePen = degreePen;
            TextBrush = textBrush;
            TxtFont = txtFont;
        }

        public float Speed { private set; get; }

        /// <summary>
        /// 角度 。从 0 开始的
        /// </summary>
        public float Angle { private set; get; }

        public float Legth { private set; get; }

        public Pen DegreePen { private set; get; }

        public string Text { private set; get; }

        public Brush TextBrush { private set; get; }

        public Font TxtFont { private set; get; }

    }
}
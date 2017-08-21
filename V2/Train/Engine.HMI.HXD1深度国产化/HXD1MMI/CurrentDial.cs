using System.Drawing;

namespace HXD1.DeepDomestic
{
    public class CurrentDial : Dial
    {
        public RectText _1ARectText
        {
            get
            {
                return _1A;
            }
        }
        private RectText _1A;

        public RectText _1BRectText
        {
            get
            {
                return _1B;
            }
        }
        private RectText _1B;

        private PointF _unitPoint;
        private Point _stringPoint;

        private Point _1APoint;
        private Point _1BPoint;

        public CurrentDial(Rectangle rect, int perValue, int count, string label, Color penColor, int everyCount)
            : base(rect, perValue, count, label, penColor, everyCount)
        {
            _unitPoint = new PointF(_startPoint.X - 50, _startPoint.Y - 27.5f * _perHeight);
            _stringPoint = new Point(_startPoint.X - 20, _startPoint.Y + 66);
            _1A = new RectText(new Rectangle(_startPoint.X - 8, _startPoint.Y + 3, 65, 25), "", 14, 1, Common.WhiteColor, Common.BlackColor, Common.WhiteColor, 1, true);
            _1B = new RectText(new Rectangle(_startPoint.X - 8, _startPoint.Y + 30, 65, 25), "", 14, 1, Common.WhiteColor, Common.BlackColor, Common.WhiteColor, 1, true);
            _1APoint = new Point(_startPoint.X - 35, _startPoint.Y + 3);
            _1BPoint = new Point(_startPoint.X - 35, _startPoint.Y + 30);
        }

        public override void OnDraw(Graphics g)
        {
            base.OnDraw(g);
            g.DrawString("[A]", _font, _penBrush, _unitPoint);
            g.DrawString(Label, _font, _penBrush, _stringPoint);



            _1A.OnDraw(g);
            _1B.OnDraw(g);

            g.DrawString("1A", Common._12Font, Common.WhiteBrush, _1APoint);
            g.DrawString("1B", Common._12Font, Common.WhiteBrush, _1BPoint);
        }


    }
}

using System.Drawing;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 压力
    /// </summary>
    public class PressureDial : Dial
    {
        private PointF _p1;
        private PointF _p2;

        private PointF _p3;
        private PointF _p4;

        private PointF _p5;
        private PointF _p6;

        private PointF _p7;
        private PointF _p8;

        private PointF _p9;
        private PointF _p10;

        private Pen _pen1;
        private Pen _pen2;
        private Pen _pen3;

        private Point _stringPoint;
        private PointF _unitPoint;




        public PressureDial(Rectangle rect, int perValue, int count, string label, Color penColor, int everyCount)
            : base(rect, perValue, count, label, penColor, everyCount)
        {
            _p1 = new PointF(Rect.X, _startPoint.Y - 17.5f * _perHeight);
            _p2 = new PointF(_p1.X + Rect.Width, _p1.Y);

            _p3 = new PointF(Rect.X, _startPoint.Y - 19f * _perHeight);
            _p4 = new PointF(_p3.X + Rect.Width, _p3.Y);

            _p5 = new PointF(Rect.X, _startPoint.Y - 22.5f * _perHeight);
            _p6 = new PointF(_p5.X + Rect.Width, _p5.Y);

            _p7 = new PointF(Rect.X, _startPoint.Y - 30f * _perHeight);
            _p8 = new PointF(_p7.X + Rect.Width, _p7.Y);

            _p9 = new PointF(Rect.X, _startPoint.Y - 31f * _perHeight);
            _p10 = new PointF(_p9.X + Rect.Width, _p9.Y);


            _pen1 = new Pen(Color.Red, 3);
            _pen2 = new Pen(Color.Blue, 3);
            _pen3 = new Pen(Color.Green, 3);

            _stringPoint = new Point(_startPoint.X - 20, _startPoint.Y + 20);
            _unitPoint = new PointF(_startPoint.X - 50, _startPoint.Y - 29 * _perHeight);
        }


        protected override void ValueChange()
        {
            if (_value < 17.5f)
            {
                FillColor = Color.Red;
            }
            else if (_value < 19)
            {
                FillColor = Color.Blue;
            }
            else if (_value < 22.5)
            {
                FillColor = Color.Green;
            }
            else if (_value < 30)
            {
                FillColor = Color.Green;
            }
            else
            {
                FillColor = Color.Red;
            }

        }


        public override void OnDraw(Graphics g)
        {

            base.OnDraw(g);
            g.DrawLine(_pen1, _p1, _p2);
            g.DrawLine(_pen2, _p3, _p4);
            g.DrawLine(_pen3, _p5, _p6);
            g.DrawLine(_pen3, _p7, _p8);
            g.DrawLine(_pen1, _p9, _p10);

            g.DrawString(Label, _font, _penBrush, _stringPoint);
            g.DrawString("[kV]", _font, _penBrush, _unitPoint);
        }
    }
}

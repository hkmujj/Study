using System.Drawing;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 速度表盘
    /// </summary>
    public class ForceDial : Dial
    {
        private PointF _unitPoint;
        private Point _stringPoint;
        public Triangle _triangle;

        public ForceDial(Rectangle rect, int perValue, int count, string label, Color penColor, int everyCount, bool isDrawUnit = true)
            : base(rect, perValue, count, label, penColor, everyCount, isDrawUnit)
        {
            _unitPoint = new PointF(_startPoint.X - 50, _startPoint.Y - 19 * _perHeight);
            _stringPoint = new Point(_startPoint.X + 3, _startPoint.Y + 20);
            _triangle = new Triangle(new Point(Rect.Right, Rect.Bottom - 8), new Point(Rect.Right - 30, Rect.Bottom), new Point(Rect.Right, Rect.Bottom + 8), Common.WhiteColor, Common.WhiteColor);
        }

        public float DesireValue
        {
            get
            {
                return _desireVlaue;
            }
            set
            {
                _desireVlaue = value;
                _triangle._points = new Point[3] { new Point(Rect.Right, Rect.Bottom - (int)(_desireVlaue / PerValue * _perHeight) - 5), new Point(Rect.Right - 20, Rect.Bottom - (int)(_desireVlaue / PerValue * _perHeight)), new Point(Rect.Right, Rect.Bottom - (int)(_desireVlaue / PerValue * _perHeight) + 5) };
            }
        }
        private float _desireVlaue;

        protected override void ValueChange()
        {

        }

        public Color TriangleColor
        {
            set
            {
                _triangle._brush.Color = value;
            }
        }

        public override void OnDraw(Graphics g)
        {
            base.OnDraw(g);

            g.DrawString(Label, _font, _penBrush, _stringPoint);

            if (_isDrawUnit)
                g.DrawString("[kN]", _font, _penBrush, _unitPoint);
            _triangle.OnDraw(g);
        }
    }

    public class Triangle
    {
        private Point _p1;
        private Point _p2;
        private Point _p3;
        private Color _penColor;
        private Color _brushColor;

        private Pen _pen;

        public SolidBrush _brush;
        public Point[] _points;

        public Triangle(Point p1, Point p2, Point p3, Color penColor, Color brushColor)
        {
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _penColor = penColor;
            _brushColor = brushColor;

            _points = new Point[] { _p1, _p2, _p3 };
            _pen = new Pen(_penColor);
            _brush = new SolidBrush(_brushColor);
        }

        public void OnDraw(Graphics g)
        {
            //  g.DrawLines(_pen, _points);
            g.FillPolygon(_brush, _points);
        }

    }
}

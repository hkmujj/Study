using System.Drawing;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 刻度盘
    /// </summary>
    public class Dial
    {
        public Rectangle Rect;
        public int PerValue;
        public int Count;
        public string Label;
        public Color PenColor;
        public int EveryCount;

        private Pen _pen;
        private Point _endPoint;
        private Point _stringPoint;
        private Rectangle _fillRect;
        private SolidBrush _fillBrush;

        protected bool _isDrawUnit;
        protected Brush _penBrush;
        protected Font _font = new Font("Arial", 16);
        protected float _perHeight;
        protected Point _startPoint;
        public Dial(Rectangle rect, int perValue, int count, string label, Color penColor, int everyCount, bool isDrawUnit = true)
        {
            Rect = rect;
            PerValue = perValue;
            Count = count;
            Label = label;
            PenColor = penColor;
            EveryCount = everyCount;

            _isDrawUnit = isDrawUnit;
            _pen = new Pen(PenColor);
            _startPoint = new Point(Rect.X - 3, Rect.Y + Rect.Height);
            _endPoint = new Point(Rect.X - 3, Rect.Y);
            _perHeight = (float)(Rect.Height) / Count;
            _penBrush = new SolidBrush(PenColor);
            _stringPoint = new Point(_startPoint.X - 20, _startPoint.Y + 20);

            _fillBrush = new SolidBrush(_fillColor);
        }

        public Color FillColor
        {
            get
            {
                return _fillColor;
            }
            set
            {
                _fillColor = value;
                _fillBrush.Color = (_fillColor);
            }
        }
        protected Color _fillColor = Color.Blue;

        protected virtual void ValueChange() { }


        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value >=PerValue * Count ? PerValue * Count : value;
                if (_value > 0)
                    _fillRect = new Rectangle(Rect.X + 1, (int)(Rect.Y + Rect.Height - _value / PerValue * _perHeight), Rect.Width - 1, (int)(_value / PerValue * _perHeight) + 1);
                ValueChange();
            }
        }
        protected float _value;

        public virtual void OnDraw(Graphics g)
        {
            g.DrawRectangle(_pen, Rect);
            if (_isDrawUnit)
            {
                g.DrawLine(_pen, _startPoint, _endPoint);
                for (int i = 0; i < Count; i++)
                {
                    PointF startPoint = new PointF(_startPoint.X, _startPoint.Y - i * _perHeight);
                    PointF endPoint;
                    if (i % EveryCount == 0)
                    {
                        endPoint = new PointF(startPoint.X - 10, startPoint.Y);
                        string value = (i * PerValue).ToString();
                        g.DrawLine(_pen, startPoint, endPoint);
                        g.DrawString(value, _font, _penBrush, new PointF(endPoint.X - 40, endPoint.Y - 10));
                    }
                    else
                    {
                        endPoint = new PointF(startPoint.X - 5, startPoint.Y);
                        g.DrawLine(_pen, startPoint, endPoint);
                    }
                }
            }
            if (_value > 0)
                g.FillRectangle(_fillBrush, _fillRect);


        }

    }
}

using System.Drawing;

namespace HXD1.DeepDomestic
{
    class MainViewRectText
    {
        private Point _startPoint;
        private Pen _pen = new Pen(Color.White);
        private string _describeString;

        private StringFormat _stringFormat1 = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
        private StringFormat _stringFormat2 = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Far };
        private StringFormat _stringFormat3 = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near };

        public string Value
        {
            set
            {
                _value = value;
            }
            get
            {
                return _value;
            }
        }
        private string _value;

        private string _defaultLabel;
        private string _unit;
        private SolidBrush _penSolidBrush = new SolidBrush(Color.White);

        public Color SilidBrushColor
        {
            set
            {
                _penSolidBrush.Color = value;
            }
        }

        private SolidBrush _brush = new SolidBrush(Color.White);
        private Point _unitPoint;
        private Point _stringPoint;



        private Rectangle _rect1;
        private Rectangle _rect2;

        public MainViewRectText(Point point, string defaultLabel, string unit, string describeString)
        {
            _startPoint = point;
            _defaultLabel = defaultLabel;
            _unit = unit;
            _describeString = describeString;

            _rect1 = new Rectangle(_startPoint.X, _startPoint.Y, 60, 40);
            _rect2 = new Rectangle(_startPoint.X + 60, _startPoint.Y, 84, 40);

            _unitPoint = new Point(_rect2.Right + 2, _rect2.Bottom - 25);
            _stringPoint = new Point(_rect1.X + 30, _rect1.Y + 2);
        }

        public void OnDraw(Graphics g)
        {
            g.DrawRectangle(_pen, _rect1);
            g.DrawRectangle(_pen, _rect2);

            if (!string.IsNullOrEmpty(_value))
            {

                if (_value.Equals("0"))
                {
                    g.DrawString(_defaultLabel, Common._16Font, _penSolidBrush, _rect2, _stringFormat3);
                }
                else
                {
                    g.DrawString(_value, Common._16Font, _penSolidBrush, _rect2, _stringFormat3);
                }
            }


            g.DrawString(_unit, Common._12Font, _brush, _rect2, _stringFormat2);
            g.DrawString(_describeString, Common._12Font, _brush, _rect1, _stringFormat1);
        }
    }
}

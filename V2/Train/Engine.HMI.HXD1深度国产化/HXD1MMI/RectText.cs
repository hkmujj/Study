using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;


namespace HXD1.DeepDomestic
{

    /// <summary>
    /// 主要基类，是类似于Winform的label控件
    /// </summary>
    public class RectText
    {
        //public bool IsSetVlaue
        //{
        //    get
        //    {
        //        return _isSetValue;
        //    }
        //    set
        //    {
        //        _isSetValue = value;
        //    }
        //}
        public bool _isSetValue;

        private bool _isFilllAll;
        private bool _isImmutable = true;

        private Rectangle _fillRect;

        public Rectangle RectPosition { get; set; }

        public String Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                _isSetValue = true;
            }
        }

        private string _text;

        public float FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
                _font = new Font("Arial", _fontSize);
            }
        }

        private float _fontSize;

        public int TextFormat
        {
            get
            {
                return _textFormat;
            }
            set
            {
                _textFormat = value;
                _stringFormat = new StringFormat()
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = (StringAlignment) _textFormat
                };
            }
        }

        private int _textFormat;

        public Color TextColor
        {
            get
            {
                return _textColor;
            }
            set
            {
                _textColor = value;
                _textBrush.Color = _textColor;
            }
        }

        private Color _textColor;


        public Color BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                if (_backgroundColor != value)
                {
                    _backgroundColor = value;
                    _backGroundBrush.Color = _backgroundColor;
                }
            }
        }

        private Color _backgroundColor;

        public Color PenColor
        {
            get
            {
                return _penColor;
            }
            set
            {
                _penColor = value;
                _pen.Color = _penColor;
            }
        }

        private Color _penColor;


        public int PenWide
        {
            get
            {
                return _penWide;
            }
            set
            {
                _penWide = value;
                _pen.Width = _penWide;
            }
        }

        private int _penWide;

        public bool IsDrawRectFrm { get; set; }

        public Image ImagePicture
        {
            get
            {
                return _imagePicture;
            }
            set
            {
                _imagePicture = value;
                _isSetValue = true;
            }
        }

        private Image _imagePicture;

        private SolidBrush _textBrush;
        protected SolidBrush _backGroundBrush;
        private Font _font;
        private Pen _pen;
        private StringFormat _stringFormat;


        public List<Point> PointList
        {
            get
            {
                if (_pointList == null)
                {
                    _pointList = new List<Point>()
                    {
                        new Point(RectPosition.X + RectPosition.Width/3, RectPosition.Y),
                        new Point(RectPosition.X + 2*RectPosition.Width/3, RectPosition.Y),
                        new Point(RectPosition.Right, RectPosition.Y + RectPosition.Height/3),
                        new Point(RectPosition.Right, RectPosition.Y + 2*RectPosition.Height/3),
                        new Point(RectPosition.X + 2*RectPosition.Width/3, RectPosition.Bottom),
                        new Point(RectPosition.X + RectPosition.Width/3, RectPosition.Bottom),
                        new Point(RectPosition.X, RectPosition.Y + RectPosition.Height/3*2),
                        new Point(RectPosition.X, RectPosition.Y + RectPosition.Height/3),
                    };
                }
                return _pointList;
            }
        }

        private List<Point> _pointList;
        public bool IsVisible { get; set; }

        public RectText(Rectangle rect, string text, float labelSize, int textFormat, Color textColor, Color backcolor,
            Color penColor, int penWide, bool isDrawFrame, Image image = null, bool isImmutable = false,
            bool isFillAll = false)
        {
            IsVisible = true;
            RectPosition = rect;
            _text = text;
            _fontSize = labelSize;
            _textFormat = textFormat;
            _textColor = textColor;
            _backgroundColor = backcolor;
            _penColor = penColor;
            _penWide = penWide;
            IsDrawRectFrm = isDrawFrame;
            _imagePicture = image;
            _isImmutable = isImmutable;
            _isFilllAll = isFillAll;

            _fillRect = new Rectangle(RectPosition.X + 2, RectPosition.Y + 2, RectPosition.Width - 4,
                RectPosition.Height - 4);
            _textBrush = new SolidBrush(_textColor);
            _backGroundBrush = new SolidBrush(_backgroundColor);
            _font = new Font("Arial", _fontSize);
            _pen = new Pen(_penColor, _penWide);
            _stringFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = (StringAlignment) _textFormat
            };
        }

        public RectText()
        {
            RectPosition = new Rectangle();
        }

        public void StatusOnDraw(Graphics g)
        {
            if (IsVisible)
            {
                if (_backGroundBrush != null)
                {
                    g.FillRectangle(_backGroundBrush, !_isFilllAll ? _fillRect : RectPosition);
                }
                if (IsDrawRectFrm)
                {
                    g.DrawRectangle(_pen, RectPosition);
                }
                if (_textBrush != null && !string.IsNullOrEmpty(_text))
                {
                    g.DrawString(_text, _font, _textBrush, RectPosition, _stringFormat);
                }
            }

        }

        public virtual void OnDraw(Graphics g)
        {
            if (_backGroundBrush != null)
            {
                g.FillRectangle(_backGroundBrush, !_isFilllAll ? _fillRect : RectPosition);
            }
            if (!_isImmutable)
            {
                if (_isSetValue)
                {
                    if (_imagePicture != null)
                    {
                        g.DrawImage(_imagePicture, RectPosition);
                    }

                    if (_textBrush != null && !string.IsNullOrEmpty(_text))
                    {
                        g.DrawString(_text, _font, _textBrush, RectPosition, _stringFormat);
                    }
                    _isSetValue = false;
                }
            }
            else
            {
                if (_imagePicture != null)
                {
                    g.DrawImage(_imagePicture, RectPosition);
                }

                if (_textBrush != null && !string.IsNullOrEmpty(_text))
                {
                    g.DrawString(_text, _font, _textBrush, RectPosition, _stringFormat);
                }
            }

            if (IsDrawRectFrm)
            {
                g.DrawRectangle(_pen, RectPosition);
            }
        }
    }

    public class BoolRectText : RectText
    {
        public BoolRectText(Rectangle rect, string text, float labelSize, int textFormat, Color textColor,
            Color backcolor, Color penColor, int penWide, bool isDrawFrame, Image image = null, bool isImmutable = false,
            IReadOnlyDictionary<int, bool> boolList = null, int rightIndex = 0, int wrongIndex = 0)
            : base(
                rect, text, labelSize, textFormat, textColor, backcolor, penColor, penWide, isDrawFrame, image,
                isImmutable)
        {
            _boolList = boolList;
            _rightIndex = rightIndex;
            _wrongIndex = wrongIndex;
        }

        private IReadOnlyDictionary<int, bool> _boolList;
        private int _rightIndex;
        private int _wrongIndex;

        public override void OnDraw(Graphics g)
        {
            if (_boolList != null)
            {
                if (_boolList[_rightIndex])
                {
                    _backGroundBrush.Color = Common.ControlColor;

                }

                else if (_boolList[_wrongIndex])
                {
                    _backGroundBrush.Color = Color.Red;
                }
                else
                {
                    _backGroundBrush.Color = Color.Gray;
                }
            }
            base.OnDraw(g);

        }
    }
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using YunDa.JC.MMI.Common.Extensions;
using YunDa.JC.MMI.Common;
using YunDa.JC.MMI.Common.Extensions;

namespace CommonControls
{
    public enum Alignment
    {
        Center,
        Near,
        Far
    }

    [Serializable]
    public partial class DefLabel : PictureBox
    {

        [Description("文本信息"), Category("自定义属性")]
        [Browsable(true)]
        public String DefText
        {
            set
            {
                //if (_defText == value) return;
                this._defText = value;
                //this.InvokeInvalidate();
                this.InvokeInvalidate();
            }
            get { return this._defText; }
        }
        private String _defText = "";

        [Description("单位"), Category("自定义属性")]
        [Browsable(true)]
        public String DefUnit
        {
            set
            {
                this._defUnit = value;
                this.InvokeInvalidate();
            }
            get { return this._defUnit; }
        }
        private String _defUnit = "";

        [Description("是否添加斜线"), Category("自定义属性")]
        [Browsable(true)]
        public bool ObliqueLine
        {
            set
            {
                this._obliqueLine = value;
                Invalidate();
            }
            get { return this._obliqueLine; }
        }
        private bool _obliqueLine = false;

        [Description("文本垂直方向偏移"), Category("自定义属性")]
        [Browsable(true)]
        public float YOffset
        {
            set
            {
                _yOffset = value;
                Invalidate();
            }
            get { return _yOffset; }
        }
        private float _yOffset;

        [Description("文本水平方向偏移"), Category("自定义属性")]
        [Browsable(true)]
        public float XOffset
        {
            set
            {
                _xOffset = value;
                Invalidate();
            }
            get { return _xOffset; }
        }
        private float _xOffset;

        [Description("字体画刷"), Category("自定义属性")]
        [Browsable(true)]
        public Color FontBrush
        {
            get { return _fontBrush; }
            set { _fontBrush = value;
                _fontBrushReal.Color=value;
                Invalidate();
            }
        }
        private Color _fontBrush;

        private SolidBrush _fontBrushReal = new SolidBrush(Color.White);

        [Description("背景颜色"), Category("自定义属性")]
        [Browsable(true)]
        public Color BackColorNew
        {
            get { return _backColorNew; }
            set
            {
                _backColorNew = value;
                BackColor = value;
                Invalidate();
            }
        }
        private Color _backColorNew = Color.Black;

        [Description("边框颜色"), Category("自定义属性")]
        [Browsable(true)]
        public Color BorderColor
        {
            set
            {
                _borderColor = value;
                
                _borderPen.Color = value;
                //_borderPen = new Pen(new SolidBrush(value), _borderWidth);
            }
            get { return _borderColor; }
        }
        private Color _borderColor = Color.White;

        [Description("边框粗细"), Category("自定义属性")]
        [Browsable(true)]
        public float BorderWidth
        {
            set
            {
                _borderWidth = value;

                _borderPen.Width = value;
                //_borderPen = new Pen(_borderColor, value);
            }
            get { return _borderWidth; }
        }
        private float _borderWidth = 1;

        [Description("文本垂直方向对齐方式"), Category("自定义属性")]
        [Browsable(true)]
        public Alignment Vertical
        {
            get { return _vertical; }
            set
            {
                if (_vertical == value) return;
                _vertical = value;
                _sf.LineAlignment = (StringAlignment)Enum.Parse(typeof(StringAlignment), value.ToString());
            }
        }
        private Alignment _vertical = Alignment.Center;

        [Description("文本水平方向对齐方式"), Category("自定义属性")]
        [Browsable(true)]
        public Alignment Horizontal
        {
            get { return _horizontal; }
            set
            {
                if (_horizontal == value) return;
                _horizontal = value;
                _sf.Alignment = (StringAlignment)Enum.Parse(typeof(StringAlignment), value.ToString());
            }
        }
        private Alignment _horizontal = Alignment.Center;

        private StringFormat _sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        private Pen _borderPen =(Pen) Pens.White.Clone();
        private Pen _linPen = new Pen(Color.White, 2);
        public DefLabel()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //_fontBrushReal = new SolidBrush(_fontBrush);

            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.DrawRectangle(
                _borderPen,
                0,
                0,
                Size.Width - 1,
                Size.Height - 1
                );

            //文本
            string text = "";
            if (this._defText.Contains("_"))
            {
                string[] texts = this.DefText.Split('_');
                for (int i = 0; i < texts.Length; i++)
                {
                    if (i > 0) text += "\r\n";
                    text += texts[i];
                }
                e.Graphics.DrawString(
                _defUnit,
                Font,
                Brushes.Red,
                new RectangleF(
                    0 + XOffset,
                    Size.Height - e.Graphics.MeasureString(_defUnit, Font).Height + YOffset,
                    Size.Width,
                    e.Graphics.MeasureString(_defUnit, Font).Height
                    ),
                _sf
                );
            }
            else
            {
                text = _defText;
                e.Graphics.DrawString(
                _defUnit,
                Font,
                Brushes.Red,
                new RectangleF(
                    Size.Width - e.Graphics.MeasureString(_defUnit, Font).Width + XOffset,
                    0 + YOffset,
                    e.Graphics.MeasureString(_defUnit, Font).Width,
                    Size.Height
                    ),
                _sf
                );
            }

            if (_obliqueLine) { e.Graphics.DrawLine(_linPen, new Point(0, 0), new Point(Size.Width - 1, Size.Height - 1)); }
            if (this._defText.Contains("&"))
            {
                string[] texting = this.DefText.Split('&');
                e.Graphics.DrawString(
                texting[0],
                Font,
                _fontBrushReal,
                new RectangleF(
                    Width - e.Graphics.MeasureString(texting[0], Font).Width - 2 - XOffset,
                    0 + YOffset,
                    e.Graphics.MeasureString(texting[0], Font).Width + 2,
                    Height / 2 + 2
                    ),
                _sf
                );
                e.Graphics.DrawString(
                texting[1],
                Font,
                _fontBrushReal,
                new RectangleF(
                    0 + XOffset,
                    Size.Height / 2 + YOffset - 2,
                    e.Graphics.MeasureString(texting[0], Font).Width,
                    Size.Height / 2 + 2),
                _sf
                );
            }
            else
            {
                e.Graphics.DrawString(
                    text,
                    Font,
                    _fontBrushReal,
                    new RectangleF(
                        0 + XOffset,
                        0 + YOffset,
                        Size.Width,
                        Size.Height),
                    _sf
                    );
            }
        }

        public DefLabel CopyThis()
        {
            DefLabel d = new DefLabel();
            //d.Margin = this.Margin;
            d.BorderColor = this.BorderColor;
            d.BorderStyle = this.BorderStyle;
            d.BorderWidth = this.BorderWidth;
            d.BackColor = this.BackColor;
            //d.Size = this.Size;
            d.DefText = "0";
            d.FontBrush = this.FontBrush;
            d.Font = this.Font;

            return d;
        }
    }
}

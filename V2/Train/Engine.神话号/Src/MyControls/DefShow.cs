using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using YunDa.JC.MMI.Common;

namespace MyControls
{
    [Serializable]
    public partial class DefShow : UserControl
    {

        [Description("文本信息"), Category("自定义属性")]
        [Browsable(true)]
        public String DefText
        {
            set
            {
                this._defText = value;
                Invalidate();
            }
            get { return this._defText; }
        }
        private String _defText = "";

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
            set { _fontBrush = value; }
        }
        private Color _fontBrush;

        [Description("边框颜色"), Category("自定义属性")]
        [Browsable(true)]
        public Color BorderColor
        {
            set
            {
                _borderColor = value;
                _borderPen = new Pen(new SolidBrush(value), _borderWidth);
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
                _borderPen = new Pen(_borderColor, value);
            }
            get { return _borderWidth; }
        }
        private float _borderWidth = 1;

        [Description("文本垂直方向对齐方式"), Category("自定义属性")]
        [Browsable(true)]
        public CommonControls.Alignment Vertical
        {
            get { return _vertical; }
            set
            {
                if (_vertical == value) return;
                _vertical = value;
                _sf.LineAlignment = (StringAlignment)Enum.Parse(typeof(StringAlignment), value.ToString());
            }
        }
        private CommonControls.Alignment _vertical = CommonControls.Alignment.Center;

        [Description("文本水平方向对齐方式"), Category("自定义属性")]
        [Browsable(true)]
        public CommonControls.Alignment Horizontal
        {
            get { return _horizontal; }
            set
            {
                if (_horizontal == value) return;
                _horizontal = value;
                _sf.Alignment = (StringAlignment)Enum.Parse(typeof(StringAlignment), value.ToString());
            }
        }
        private CommonControls.Alignment _horizontal = CommonControls.Alignment.Center;

        private StringFormat _sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        private Pen _borderPen = Pens.White;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("输入的信号"), Category("自定义属性")]
        [Browsable(true)]
        public List<BoolConfigInfo> BoolData
        {
            set
            { 
                _boolData = value;
            }
            get 
            {
                return _boolData;
            }
        }
        private List<BoolConfigInfo> _boolData = new List<BoolConfigInfo>();

        public void SetColor()
        {
            if (_boolData.Count >= 2)
            {
                if (!_boolData[0].InputData && !_boolData[1].InputData)
                {
                    BackColor = Color.Gray;
                }
                else
                {
                    if (_boolData[0].InputData && _boolData[1].InputData)
                    {
                        BackColor = Color.Lime;
                    }
                    else
                    {
                        BackColor = Color.Red;
                    }
                }
            }
            if (_boolData.Count == 1)
            {
                if (_boolData[0].InputData)
                {
                    BackColor = Color.Lime;
                }
                else
                {
                    BackColor = Color.Red;
                }
            }
            Invalidate();
        }
        public DefShow()
        {
            InitializeComponent();
            SetStyle(
               ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer, true);
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
            }
            else
            {
                text = _defText;
            }
            _brush.Color = FontBrush;
            e.Graphics.DrawString(
                text,
                Font,
                _brush,
                new RectangleF(
                    0 + XOffset,
                    0 + YOffset,
                    Size.Width,
                    Size.Height),
                _sf
                );
        }
        private SolidBrush _brush = new SolidBrush(Color.White);
        public DefShow CopyThis()
        {
            DefShow d = new DefShow();
            d.BorderColor = this.BorderColor;
            d.BorderStyle = this.BorderStyle;
            d.BorderWidth = this.BorderWidth;
            d.BackColor = this.BackColor;
            d.DefText = "0";
            d.FontBrush = this.FontBrush;
            d.Font = this.Font;
            return d;
        }
    }
}

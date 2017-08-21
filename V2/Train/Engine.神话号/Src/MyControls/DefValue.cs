using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using YunDa.JC.MMI.Common;

namespace MyControls
{

    [Serializable]
    public partial class DefValue : UserControl
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
        private String _defText = "-";

        [Description("保留几位小数"), Category("自定义属性")]
        [Browsable(true)]
        public int DataKeeping
        {
            set
            {
                this._dataKeeping = value;
                Invalidate();
            }
            get { return this._dataKeeping; }
        }
        private int _dataKeeping = 0;

        [Description("单位"), Category("自定义属性")]
        [Browsable(true)]
        public String DefUnit
        {
            set
            {
                this._defUnit = value;
                Invalidate();
            }
            get { return this._defUnit; }
        }
        private String _defUnit = "";

        [Description("输入数据"), Category("自定义属性")]
        [Browsable(true)]
        public FloatConfigInfo FloatData
        {
            set
            {
                _floatData = value;
            }
            get
            {
                SetValue();
                return _floatData;
            }
        }
        private FloatConfigInfo _floatData = new FloatConfigInfo();

        public void SetValue()
        {
            if (_floatData != null)
            {
                switch (DataKeeping)
                {
                    case 0:
                    _defText = _floatData.InputData.ToString("0");
                    break;
                    case 1:
                    _defText = _floatData.InputData.ToString("0.0");
                    break;
                    case 2:
                    _defText = _floatData.InputData.ToString("0.00");
                    break;
                    case 3:
                    _defText = _floatData.InputData.ToString("0.000");
                    break;
                    default:
                    _defText = _floatData.InputData.ToString("0.0000");

                    break;
                }
                Invalidate();
            }
        }

        #region
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

        [Description("单位垂直方向偏移"), Category("自定义属性")]
        [Browsable(true)]
        public float YOffsetUnit
        {
            set
            {
                _yOffsetUnit = value;
                Invalidate();
            }
            get { return _yOffsetUnit; }
        }
        private float _yOffsetUnit;

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

        [Description("单位水平方向偏移"), Category("自定义属性")]
        [Browsable(true)]
        public float XOffsetUnit
        {
            set
            {
                _xOffsetUnit = value;
                Invalidate();
            }
            get { return _xOffsetUnit; }
        }
        private float _xOffsetUnit;

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

        #endregion
        public DefValue()
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

            e.Graphics.DrawString(
            _defUnit,
            Font,
            redBrush,
            new RectangleF(
                0 + XOffsetUnit,
                0 + YOffsetUnit,
                Size.Width,
                Size.Height
                ),
            _sf
            );
            fontBrush.Color = FontBrush;
            e.Graphics.DrawString(
                _defText,
                Font,
                fontBrush,
                new RectangleF(
                    0 + XOffset,
                    0 + YOffset,
                    Size.Width,
                    Size.Height),
                _sf
                );
        }
        private SolidBrush redBrush = new SolidBrush(Color.Red);
        private SolidBrush fontBrush = new SolidBrush(Color.White);
        public DefValue CopyThis()
        {
            DefValue d = new DefValue();
            d.BorderColor = this.BorderColor;
            d.BorderStyle = this.BorderStyle;
            d.BorderWidth = this.BorderWidth;
            d.BackColor = this.BackColor;
            d.DefText = "-";
            d.FontBrush = this.FontBrush;
            d.Font = this.Font;

            return d;
        }
    }
}

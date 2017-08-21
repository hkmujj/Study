using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CommonControls
{
    public partial class DefDataTime : UserControl
    {
        [Description("文本垂直方向偏移"), Category("自定义属性")]
        [Browsable(true)]
        public float YOffset
        {
            set
            {
                _yOffset = value;
                _defLabel_Time.YOffset = value;
            }
            get { return _yOffset; }
        }
        private float _yOffset;

        [Description("字体画刷"), Category("自定义属性")]
        [Browsable(true)]
        public Color FontBrush
        {
            get { return _fontBrush; }
            set
            {
                _fontBrush = value;
                _defLabel_Time.FontBrush = value;
            }
        }
        private Color _fontBrush;

        [Description("边框颜色"), Category("自定义属性")]
        [Browsable(true)]
        public Color BorderColor
        {
            set
            {
                _borderColor = value;
                _defLabel_Time.BorderColor = value;
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
                _defLabel_Time.BorderWidth = value;
            }
            get { return _borderWidth; }
        }
        private float _borderWidth = 1;

        [Description("时间格式"), Category("自定义属性")]
        [Browsable(true)]
        public String TimeFormat
        {
            get { return _timeFormat; }
            set
            {
                _timeFormat = value;
                Invalidate();
            }
        }
        private String _timeFormat = "yy-MM-dd  HH:mm:ss";

        public DefDataTime()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint|ControlStyles.OptimizedDoubleBuffer, true);
            Timer timer = new Timer() {Interval = 1000, Enabled = true};
            timer.Tick += timer_Tick;
            timer.Start();
            components = new Container();
            components.Add(timer);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            _defLabel_Time.DefText = time.ToString(_timeFormat);
        }
    }
}

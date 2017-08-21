using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using YunDa.JC.MMI.Common;

namespace MyControls
{
    public partial class DefLine : UserControl
    {
        public DefLine()
        {
            InitializeComponent();
            SetStyle(
               ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer, true);
        }
        
        [Description("线方向"), Category("自定义属性")]
        [Browsable(true)]
        public bool Horizontal
        {
            set 
            {
                _horizontal = value;
                Invalidate();
            }
            get { return _horizontal; }
        }
        private bool _horizontal=false;
        [Description("起点"), Category("自定义属性")]
        [Browsable(false)]
        public Point StartPoint
        {
            set { _startPoint = value; }
            get { return _startPoint; }
        }
        private Point _startPoint;

        [Description("终点"), Category("自定义属性")]
        [Browsable(false)]
        public Point EndPoint
        {
            set { _endPoint = value; }
            get { return _endPoint; }
        }
        private Point _endPoint;

        [Description("线粗"),Category ("自定义属性")]
        [Browsable (true)]
        public Int32 LineWidth
        {
            set 
            {
                _lineWidth = value;
                Invalidate();
            }
            get { return _lineWidth; }
        }
        private Int32 _lineWidth =2 ;

        [Description("颜色"), Category("自定义属性")]
        [Browsable(true)]
        public Color LineColor
        {
            set 
            { 
                _lineColor = value;
            }
            get { return _lineColor; }
        }
        private Color _lineColor = Color.Red;

        [Description("线类型"), Category("自定义属性")]
        [Browsable(true)]
        public bool Dash
        {
            set
            {
                _dash = value;
                Invalidate();
            }
            get { return _dash; }
        }
        private bool _dash = false;

        [Browsable(false)]
        private Pen _myPen=new Pen(Color.White);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("输入数据"), Category("自定义属性")]
        [Browsable(true)]
        public List<BoolConfigInfo> BoolData
        {
            set { _boolData = value; }
            get { return _boolData; }
        }
        private List<BoolConfigInfo> _boolData = new List<BoolConfigInfo>();
        public void SetColor()
        {
            if (_boolData.Count >= 2)
            {
                if (!_boolData[0].InputData && !_boolData[1].InputData)
                {
                    _lineColor = Color.Gray;
                }
                else
                {
                    if (_boolData[0].InputData && _boolData[1].InputData)
                    {
                        _lineColor = Color.Lime;
                    }
                    else
                    {
                        _lineColor = Color.Red;
                    }
                }
            }
            if (_boolData.Count == 1)
            {
                if (_boolData[0].InputData)
                {
                    _lineColor = Color.Lime;
                }
                else 
                {
                    _lineColor = Color.Red;
                }
            }
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            _myPen.Color = _lineColor;
            _myPen.Width = _lineWidth;
            if (_dash)
            {
                _myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                _myPen.DashPattern = new float[] { 12, 5 };
            }
            if (_horizontal)
            {
                _startPoint = new Point(0, this.Height / 2);
                _endPoint = new Point(this.Width, this.Height / 2);
            }
            else 
            {
                _startPoint = new Point(this.Width / 2, 0);
                _endPoint = new Point(this.Width / 2, this.Height);
            }
            e.Graphics.DrawLine(_myPen, _startPoint, _endPoint);
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using YunDa.JC.MMI.Common;

namespace MyControls
{
    public partial class DefDisplay : UserControl
    {
        public DefDisplay()
        {
            InitializeComponent();
            SetStyle(
               ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer, true);
        }

        [Description("指示灯显示的颜色"), Category("自定义属性")]
        [Browsable(true)]
        public Color BtnColor
        {
            set { _btnColor = value; }
            get { return _btnColor; }
        }
        private Color _btnColor = Color.Silver;

        [Description("输入的信号"), Category("自定义属性")]
        [Browsable(true)]
        public List<BoolConfigInfo> BoolLogicSet
        {
            set { _boolLogicSet = value; }
            get
            {
                return _boolLogicSet;
            }
        }
        private List<BoolConfigInfo> _boolLogicSet = new List<BoolConfigInfo>();

        [Description("指示灯Y方向偏移"), Category("自定义属性")]
        [Browsable(true)]
        public int YOffset
        {
            set { _yOffset = value; Invalidate(); }
            get { return _yOffset; }
        }
        private int _yOffset;

        [Description("指示灯Y方向偏移"), Category("自定义属性")]
        [Browsable(true)]
        public int XOffset
        {
            set { _xOffset = value; Invalidate(); }
            get { return _xOffset; }
        }
        private int _xOffset;

        public void SetColor()
        {
            if (_boolLogicSet.Count==2)
            {
                if (!_boolLogicSet[0].InputData && !_boolLogicSet[1].InputData)
                {
                    _btnColor = Color.Silver;
                }
                else
                {
                    if (_boolLogicSet[0].InputData && _boolLogicSet[1].InputData)
                    {
                        _btnColor = Color.Lime;
                    }
                    else
                    {
                        _btnColor = Color.Red;
                    }
                }
            }
            if (_boolLogicSet.Count == 1)
            {
                if (_boolLogicSet[0].InputData)
                {
                    _btnColor = Color.Lime;
                }
                else
                {
                    _btnColor = Color.Silver;
                }
            }
            if (_boolLogicSet.Count == 3)
            {
                if (_boolLogicSet[0].InputData&&!_boolLogicSet [1].InputData&&!_boolLogicSet [2].InputData) 
                {
                    _btnColor = Color.Red;
                }
                if (_boolLogicSet[1].InputData && !_boolLogicSet[0].InputData && !_boolLogicSet[2].InputData)
                {
                    _btnColor = Color.Lime;
                }
                if (_boolLogicSet[2].InputData && !_boolLogicSet[1].InputData && !_boolLogicSet[0].InputData)
                {
                    _btnColor = Color.Silver;
                }
            }
            Invalidate();
        }
        private SolidBrush _brush = new SolidBrush(Color.White);
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _brush.Color = _btnColor;
            e.Graphics.FillEllipse(_brush, this.Width * 4 / 15 + _xOffset, this.Height * 4 / 15 + _yOffset, this.Width / 2, this.Height / 2);
        }
    }
}

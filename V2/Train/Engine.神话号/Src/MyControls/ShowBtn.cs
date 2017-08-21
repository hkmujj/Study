using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using YunDa.JC.MMI.Common;

namespace MyControls
{
    public partial class ShowBtn : UserControl
    {
        public ShowBtn()
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
        private Color _btnColor = Color.Gray;

        [Description("指示灯显示的颜色"), Category("自定义属性")]
        [Browsable(true)]
        public List<BoolConfigInfo> BoolLogicSet
        {
            set
            {
                _boolLogicSet = value;
            }
            get
            {
                return _boolLogicSet;
            }
        }
        private List<BoolConfigInfo> _boolLogicSet = new List<BoolConfigInfo>();

        public void SetColor()
        {
            if (_boolLogicSet.Count == 2)
            {
                if (_boolLogicSet[0].InputData && !_boolLogicSet[1].InputData)
                {
                    _btnColor = Color.Lime;
                }
                else
                {
                    if (!_boolLogicSet[0].InputData && _boolLogicSet[1].InputData)
                    {
                        _btnColor = Color.Red;
                    }
                    else
                    {
                        _btnColor = Color.Gray;
                    }
                }
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _brush.Color = _btnColor;
            e.Graphics.DrawEllipse(Pens.Black, Size.Width / 10, Size.Height / 10, Size.Width * 4 / 5, Size.Height * 4 / 5);
            e.Graphics.FillEllipse(_brush, Size.Width / 10, Size.Height / 10, Size.Width * 4 / 5, Size.Height * 4 / 5);
        }
        private SolidBrush _brush = new SolidBrush(Color.White);
    }
}

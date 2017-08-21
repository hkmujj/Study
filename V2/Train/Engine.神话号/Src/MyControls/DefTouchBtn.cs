using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using YunDa.JC.MMI.Common;

namespace MyControls
{
    public partial class DefTouchBtn : UserControl 
    {
        public DefTouchBtn()
        {
            InitializeComponent();
            SetStyle(
               ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer, true);
        }
        private int _clickCount = 0;

        [Description("输入数据"), Category("自定义属性")]
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

        [Description("输出数据"), Category("自定义属性")]
        [Browsable(true)]
        public List<BoolConfigInfo> OutBoolLogicSet
        {
            set
            {
                _outBoolLogicSet = value;
            }
            get
            {
                return _outBoolLogicSet;
            }
        }
        private List<BoolConfigInfo> _outBoolLogicSet = new List<BoolConfigInfo>();
    }

    
}

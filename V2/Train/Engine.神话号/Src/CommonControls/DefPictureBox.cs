using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonControls
{
    public partial class DefPictureBox : UserControl
    {
        [Description("资源图片"), Category("自定义属性")]
        [Browsable(true)]
        public Image[] ResourceImages
        {
            get { return _resourceImages; }
            set
            {
                if (_resourceImages == value) return;
                _resourceImages = value;
                Invalidate();
            }
        }
        private Image[] _resourceImages = new Image[1];

        [Description("逻辑编号"), Category("自定义属性")]
        [Browsable(true)]
        public int[] LogicIDs
        {
            get { return _logicIDs; }
            set
            {
                if (_logicIDs == value) return;
                _logicIDs = value;
                Invalidate();
            }
        }
        private int[] _logicIDs = new Int32[1];

        public DefPictureBox()
        {
            InitializeComponent();
        }
    }
}

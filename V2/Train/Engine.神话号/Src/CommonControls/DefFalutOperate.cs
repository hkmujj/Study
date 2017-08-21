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
    public enum ShowState
    {
        V0,
        V1,
        Info
    }

    public partial class DefFalutOperate : UserControl
    {
        private FalutInfo _currentFalutInfo = null;

        [Description("标题"), Category("自定义属性")]
        [Browsable(true)]
        public String TitleName
        {
            get { return _titleName; }
            set
            {
                _titleName = value;
                Invalidate();
            }
        }
        private String _titleName = "V=0";

        [Description("字体"), Category("自定义属性")]
        [Browsable(true)]
        public Font TextFont
        {
            set
            {
                _font = value;
            }
            get { return _font; }
        }
        private Font _font = new Font("宋体", 13);

        private ShowState _currentShowState = ShowState.V0;

        public DefFalutOperate()
        {
            InitializeComponent();
        }

        public void SetFalut(FalutInfo fi,ShowState ss)
        {
            _currentFalutInfo = fi;
            _currentShowState = ss;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            paint_Frame(e.Graphics);
            paint_Falut(e.Graphics);
        }

        private Brush _lineBrush = new SolidBrush(Color.FromArgb(143, 145, 149));
        private void paint_Frame(Graphics g)
        {
            g.FillRectangle(Brushes.Blue, new Rectangle(0, 0, this.Size.Width, this.Size.Height));
            g.FillRectangle(_lineBrush, new Rectangle(0, 0, this.Size.Width, 30));

            g.DrawRectangle(Pens.White, new Rectangle(0,0,167,30));
            g.DrawRectangle(Pens.White, new Rectangle(169, 0, 463, 30));
            g.DrawRectangle(Pens.White, new Rectangle(634, 0, 166, 30));

            g.DrawString(
                TitleName,
                TextFont,
                Brushes.Black,
                new Rectangle(634, 0, 166, 30),
                StaticProperty.SfCenter
                );
        }
        private void paint_Falut(Graphics g)
        {
            if (_currentFalutInfo == null) return;

            _currentFalutInfo.IsOK = true;
            g.DrawString(
                _currentFalutInfo.Grade,
                TextFont,
                Brushes.Black,
                new Rectangle(0, 0, 167, 30),
                StaticProperty.SfCenter
                );

            g.DrawString(
                _currentFalutInfo.Description,
                TextFont,
                Brushes.Black,
                new Rectangle(169, 2, 463, 30),
                StaticProperty.SfCenter
                );

            String str = "";
            switch (_currentShowState)
            {
                case ShowState.V0:
                    str = _currentFalutInfo.V0;
                    break;
                case ShowState.V1:
                    str = _currentFalutInfo.V1;
                    break;
                case ShowState.Info:
                    str = _currentFalutInfo.Info;
                    break;
            }
            if (str == null || str == "") return;
            String[] strs = str.Split('_');
            str = strs[0];
            if (strs.Length > 1)
            {
                for (int i = 1; i < strs.Length; i++)
                {
                    str += ("\n" + strs[i]);
                }
            }
            g.DrawString(
                str,
                TextFont,
                Brushes.White,
                new Rectangle(5, 40, this.Size.Width-10, this.Size.Height-40),
                StaticProperty.SfLeftTop
                );
        }

    }
}

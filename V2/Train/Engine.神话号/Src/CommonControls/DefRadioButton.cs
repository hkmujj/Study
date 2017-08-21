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
    public sealed partial class DefRadioButton : RadioButton
    {
        [Description("文本的绘制区域"), Category("自定义属性")]
        public Rectangle DefRect
        {
            get { return _defRect; }
            set { _defRect = value; }
        }
        private Rectangle _defRect;

        [Description("背景位置"), Category("自定义属性")]
        public Rectangle BackPoint
        {
            get { return _backPoint; }
            set
            {
                _backPoint = value;
            }
        }
        private Rectangle _backPoint = new Rectangle(0, 0,20,20);

        [Description("选中时候的背景"), Category("自定义属性")]
        public Image UpImage
        {
            get { return _upImage; }
            set { _upImage = value; }
        }
        private Image _upImage = null;

        [Description("没有选择时候的背景"), Category("自定义属性")]
        public Image DownImage
        {
            get { return _downImage; }
            set { _downImage = value; }
        }
        private Image _downImage = null;

        private Brush _foreBrush = Brushes.White;

        public DefRadioButton()
        {
            InitializeComponent();

            //_foreBrush = new SolidBrush(ForeColor);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (UpImage != null && DownImage!=null)
            pevent.Graphics.DrawImage(
                this.Checked ? UpImage : DownImage,
                _backPoint
                );

            //文本
            string text = "";
            if (this.Text.Contains("_"))
            {
                string[] texts = this.Text.Split(new char[] { '_' });
                for (int i = 0; i < texts.Length; i++)
                {
                    if (i > 0) text += "\r\n";
                    text += texts[i];
                }
            }
            else text = Text;
            pevent.Graphics.DrawString(
                text,
                Font,
                _foreBrush,
                _defRect,
                StaticProperty.SfLeftCenter
                );
        }
    }
}

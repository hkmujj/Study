using System.Drawing;
using System.Windows.Forms;

namespace Urban.ATC.CommonView.View
{
    public partial class PicBoxControlBase : UserControl
    {
        public PicBoxControlBase()
        {
            InitializeComponent();
        }

        protected void ChangeImage(Image img)
        {
            pictureBox1.Image = img;
        }
    }
}
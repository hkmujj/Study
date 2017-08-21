using System.Drawing;
using System.Windows.Forms;
using Urban.ATC.CommonView.Model;

namespace Urban.ATC.CommonView.View
{
    public partial class TextBase : Label
    {
        public TextBase()
        {
            InitializeComponent();
            this.ForeColor = GDICommon.LightGreyColor;
        }

        protected void ChangeText(string str)
        {
            this.Text = str;
        }

        protected string GetText()
        {
            return Text;
        }

        protected void ChangeTextColor(Color color)
        {
            this.ForeColor = color;
        }
    }
}
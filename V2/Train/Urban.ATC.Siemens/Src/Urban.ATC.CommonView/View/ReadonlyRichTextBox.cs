using System.Windows.Forms;

namespace Motor.ATP.CommonView.View
{
    public partial class ReadonlyRichTextBox : RichTextBox
    {
        public ReadonlyRichTextBox()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserMouse , true);

            SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick | ControlStyles.Selectable, false);

        }
    }
}

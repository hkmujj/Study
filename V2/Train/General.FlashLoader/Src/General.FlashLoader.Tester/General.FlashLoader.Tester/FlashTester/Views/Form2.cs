using System.Windows.Forms;

namespace Yunda.FlashTester.Views
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(DataMonitorView monitor) : this()
        {
            elementHost1.Child = monitor;
        }
    }
}

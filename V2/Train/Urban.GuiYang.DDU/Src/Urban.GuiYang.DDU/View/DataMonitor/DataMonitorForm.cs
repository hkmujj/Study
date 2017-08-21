using System;
using System.Windows.Forms;
using Microsoft.Practices.ServiceLocation;

namespace Urban.GuiYang.DDU.View.DataMonitor
{
    public partial class DataMonitorForm : Form
    {
        public DataMonitorForm()
        {
            InitializeComponent();
        }

        private void DataMonitorForm_Load(object sender, EventArgs e)
        {
            elementHost1.Child = ServiceLocator.Current.GetInstance<DataMonitorView>();
        }
    }
}

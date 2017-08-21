using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MMI.Facility.Interface.Project;
using Subway.TCMS.Infrasturcture.ViewModel;

namespace Subway.TCMS.Infrasturcture.Monitor
{
    public partial class DataMonitorForm : ProjectFormBase
    {
        public DataMonitorForm()
        {
            InitializeComponent();
        }

        public DataMonitorForm(MonitorViewModel viewModel) : this()
        {
            elementHost1.Child = new DataMonitorView() { DataContext = viewModel };
        }
    }
}

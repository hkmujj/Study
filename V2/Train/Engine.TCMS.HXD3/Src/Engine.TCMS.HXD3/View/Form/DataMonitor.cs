using Engine.TCMS.HXD3.View.DataMonitor;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.HXD3.View.Form
{
    public partial class DataMonitor : System.Windows.Forms.Form
    {
        public DataMonitor()
        {
            InitializeComponent();

            var view  = ServiceLocator.Current.GetInstance<DataMonitorView>();
            elementHost1.Child = view;
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
        }
    }
}

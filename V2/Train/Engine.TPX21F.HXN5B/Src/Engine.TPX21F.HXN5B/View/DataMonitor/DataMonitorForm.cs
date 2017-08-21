using System.Windows.Forms;
using Engine.TPX21F.HXN5B.ViewModel;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using UserControl = System.Windows.Controls.UserControl;

namespace Engine.TPX21F.HXN5B.View.DataMonitor
{
    public partial class DataMonitorForm : Form
    {
        public DataMonitorForm()
        {
            InitializeComponent();

            RegionManager.SetRegionManager(elementHost1.HostContainer,
                ServiceLocator.Current.GetInstance<IRegionManager>());


            UserControl tcms = new DataMonitorView(ServiceLocator.Current.GetInstance<HXN5BViewModel>());
            elementHost1.Child = tcms;
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
        }
    }
}

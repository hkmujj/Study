using System.Windows.Forms;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace MMI.Tester.BatchDataSender.View
{
    public partial class BatchDataSenderForm : Form
    {
        public BatchDataSenderForm()
        {
            InitializeComponent();

            RegionManager.SetRegionManager(elementHost1.HostContainer, ServiceLocator.Current.GetInstance<IRegionManager>());

            elementHost1.Child = ServiceLocator.Current.GetInstance<ShellView>();

        }
    }
}

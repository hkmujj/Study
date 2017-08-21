using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;

namespace Urban.Phillippine.View.Views
{
    /// <summary>
    /// ContentButtonShell.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentAndButtonRegion, IsDefaultView = true)]
    public partial class ContentButtonShell : UserControl
    {
        public ContentButtonShell()
        {
            InitializeComponent();
        }
    }
}
using System.Windows.Controls;

namespace Urban.ATC.Siemens.WPF.Control.View.RegionB
{
    /// <summary>
    /// RegionB.xaml 的交互逻辑
    /// </summary>
    public partial class RegionB : UserControl
    {
        public RegionB()
        {
            InitializeComponent();

            this.DegreeScaleView.ItemsSource = DegreeScaleResource.Instance.ItemCollection;
        }
    }
}
using System.Windows.Controls;
using Engine.LCDM.HXD3.Constance;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.LCDM.HXD3.Views.Flow
{
    /// <summary>
    /// FlowView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.Flow, IsDefaultView = true)]
    public partial class FlowView : UserControl
    {
        public FlowView()
        {
            InitializeComponent();
            //this.PreviewMouseDown += FlowView_PreviewMouseDown;
        }

        //private void FlowView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
            
        //}
    }
}

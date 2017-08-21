using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace TestSubsystem.View
{
    /// <summary>
    /// SubSubView1.xaml 的交互逻辑
    /// </summary>
    //[ViewExport(RegionName = RegionNames.SubViewContent)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class SubSubView1 : UserControl
    {
        public SubSubView1()
        {
            InitializeComponent();
        }
    }
}

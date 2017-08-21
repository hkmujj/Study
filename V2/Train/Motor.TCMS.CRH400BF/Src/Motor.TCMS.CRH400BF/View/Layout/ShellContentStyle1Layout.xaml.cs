using System.ComponentModel.Composition;
using System.Windows;
using Motor.TCMS.CRH400BF.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Motor.TCMS.CRH400BF.View.Layout
{
    /// <summary>
    /// ShellContentLayout.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainShellContent, IsDefaultView = true)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ShellContentStyle1Layout
    {
        public ShellContentStyle1Layout()
        {
            InitializeComponent();
        }

  
    }
}

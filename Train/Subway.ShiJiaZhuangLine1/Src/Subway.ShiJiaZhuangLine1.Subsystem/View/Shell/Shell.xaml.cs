using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Shell
{
    /// <summary>
    /// Shell.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class Shell : UserControl
    {
        public Shell()
        {
            InitializeComponent();
        }
    }
}

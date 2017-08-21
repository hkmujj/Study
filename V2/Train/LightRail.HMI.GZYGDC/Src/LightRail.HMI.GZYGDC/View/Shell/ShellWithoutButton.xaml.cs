using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Windows.Controls;

namespace LightRail.HMI.GZYGDC.View.Shell
{
    /// <summary>
    /// ShellWithoutButton.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class ShellWithoutButton : UserControl
    {
        public ShellWithoutButton()
        {
            InitializeComponent();
        }
    }
}

using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Motor.TCMS.CRH400BF.View.Shell
{
    /// <summary>
    /// ShellWithoutButton.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MainShellWithoutButton : UserControl
    {
        public MainShellWithoutButton()
        {
            InitializeComponent();
        }
    }
}

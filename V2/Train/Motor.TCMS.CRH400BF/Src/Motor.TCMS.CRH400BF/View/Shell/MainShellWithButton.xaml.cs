using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Motor.TCMS.CRH400BF.View.Shell
{
    /// <summary>
    /// ShellWithButton.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MainShellWithButton : UserControl
    {
        public MainShellWithButton()
        {
            InitializeComponent();
        }
    }
}

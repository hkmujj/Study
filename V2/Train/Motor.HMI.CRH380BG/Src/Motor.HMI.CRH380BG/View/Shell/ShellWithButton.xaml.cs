using System.ComponentModel.Composition;

namespace Motor.HMI.CRH380BG.View.Shell
{
    /// <summary>
    /// ShellWithButton.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ShellWithButton 
    {
        public ShellWithButton()
        {
            InitializeComponent();
        }
    }
}

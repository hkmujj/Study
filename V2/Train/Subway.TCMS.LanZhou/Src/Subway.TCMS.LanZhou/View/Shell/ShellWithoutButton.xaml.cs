using System.ComponentModel.Composition;
using System.Windows.Controls;
using DevExpress.Xpf.Core.Native;
using Microsoft.Practices.ServiceLocation;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.View.Shell
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

            if (!this.IsInDesignTool())
            {
                DataContext = ServiceLocator.Current.GetInstance<DomainViewModel>();
            }
        }
    }
}

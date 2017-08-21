using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.ViewModel;

namespace Engine.TCMS.HXD3.View.Shell
{
    /// <summary>
    /// TCMSShell.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class TCMSShell
    {
        [ImportingConstructor]
        public TCMSShell(HXD3TCMSViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}

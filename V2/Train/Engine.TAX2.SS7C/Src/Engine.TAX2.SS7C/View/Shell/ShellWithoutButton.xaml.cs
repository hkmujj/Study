using System.ComponentModel.Composition;
using DevExpress.Mvvm.POCO;
using Engine.TAX2.SS7C.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TAX2.SS7C.View.Shell
{
    /// <summary>
    /// ShellWithoutButton.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class ShellWithoutButton
    {
        public ShellWithoutButton()
        {

            InitializeComponent();
            if (!this.IsInDesignMode())
            {
                DataContext = ServiceLocator.Current.GetInstance<SS7CViewModel>();
            }
        }
    }
}

using Engine.Dial.Angola.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Engine.Dial.Angola.Views
{
    /// <summary>
    /// DialShell.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(DialShell))]
    public partial class DialShell : UserControl
    {
        [ImportingConstructor]
        public DialShell(AngolaViewModel viewmodel)
        {
            InitializeComponent();
            DataContext = viewmodel;
        }

        public DialShell()
        {
            InitializeComponent();
            DataContext = ServiceLocator.Current.GetInstance<AngolaViewModel>();
        }
    }
}
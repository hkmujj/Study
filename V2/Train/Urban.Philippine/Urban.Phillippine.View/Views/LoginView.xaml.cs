using System.Windows;
using System.Windows.Media;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.Views
{
    /// <summary>
    ///     LoginView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainShellRegion)]
    public partial class LoginView
    {
        private ILoginViewModel m_Login;

        public LoginView()
        {

            InitializeComponent();
            
            Grid.DataContextChanged += Grid_DataContextChanged;
        }

        private void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                m_Login = Grid.DataContext as ILoginViewModel;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            m_Login.ViewRender.Execute(null);
        }
    }
}
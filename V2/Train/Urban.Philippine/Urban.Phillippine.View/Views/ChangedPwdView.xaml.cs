using System.Windows;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.Views
{
    /// <summary>
    /// ChangedPwdView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentAndButtonRegion)]
    public partial class ChangedPwdView : UserControl
    {
        public ChangedPwdView()
        {
            InitializeComponent();
            DataContextChanged += ChangedPwdView_DataContextChanged;
            IsVisibleChanged += ChangedPwdView_IsVisibleChanged;
        }

        private void ChangedPwdView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                m_ViewModel.ChangedPassword.Visiblity.Execute(null);
            }
        }

        private IPhilippineViewModel m_ViewModel;

        private void ChangedPwdView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                m_ViewModel = e.NewValue as IPhilippineViewModel;
            }
        }
    }
}
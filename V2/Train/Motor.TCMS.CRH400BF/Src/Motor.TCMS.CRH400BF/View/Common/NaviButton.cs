using System.Windows;
using System.Windows.Controls;

namespace Motor.TCMS.CRH400BF.View.Common
{
    public class NaviButton : Button
    {
        static NaviButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NaviButton),
                new FrameworkPropertyMetadata(typeof(NaviButton)));
        }

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected", typeof (bool), typeof (NaviButton), new PropertyMetadata(default(bool)));

        public bool IsSelected
        {
            get { return (bool) GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
    }
}
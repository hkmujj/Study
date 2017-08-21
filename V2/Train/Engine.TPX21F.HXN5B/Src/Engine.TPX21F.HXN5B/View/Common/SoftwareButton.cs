using System.Windows;
using System.Windows.Controls;

namespace Engine.TPX21F.HXN5B.View.Common
{
    public class SoftwareButton : Button
    {
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected", typeof(bool), typeof(SoftwareButton), new PropertyMetadata(default(bool)));

        public bool IsSelected
        {
            get { return (bool) GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
    }
}

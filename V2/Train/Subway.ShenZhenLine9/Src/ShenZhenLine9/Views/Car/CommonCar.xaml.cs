using System.Windows;
using System.Windows.Controls;

namespace Subway.ShenZhenLine9.Views.Car
{
    /// <summary>
    /// CommonCar.xaml 的交互逻辑
    /// </summary>
    public partial class CommonCar : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public CommonCar()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty NuberMarginProperty = DependencyProperty.Register(
            "NuberMargin", typeof(Thickness), typeof(CommonCar), new PropertyMetadata(default(Thickness)));

        public Thickness NuberMargin { get { return (Thickness)GetValue(NuberMarginProperty); } set { SetValue(NuberMarginProperty, value); } }
    }
}

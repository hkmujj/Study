using System.Windows;

namespace Engine.TPX21F.HXN5B.View.Common
{
    /// <summary>
    /// SpeedDialView.xaml 的交互逻辑
    /// </summary>
    public partial class SpeedDialView 
    {
        public SpeedDialView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SpeedValueProperty = DependencyProperty.Register(
            "SpeedValue", typeof(float), typeof(SpeedDialView), new PropertyMetadata(default(float)));

        public float SpeedValue
        {
            get { return (float) GetValue(SpeedValueProperty); }
            set { SetValue(SpeedValueProperty, value); }
        }
    }
}

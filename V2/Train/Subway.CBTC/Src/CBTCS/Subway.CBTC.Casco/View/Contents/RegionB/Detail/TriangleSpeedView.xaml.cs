using System.Windows;

namespace Subway.CBTC.Casco.View.Contents.RegionB.Detail
{
    /// <summary>
    /// TriangleSpeedView.xaml 的交互逻辑
    /// </summary>
    public partial class TriangleSpeedView
    {
        public TriangleSpeedView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IndicationRoteAngleProperty = DependencyProperty.Register(
            "IndicationRoteAngle", typeof(double), typeof(TriangleSpeedView), new PropertyMetadata(default(double)));

        public double IndicationRoteAngle
        {
            get { return (double) GetValue(IndicationRoteAngleProperty); }
            set { SetValue(IndicationRoteAngleProperty, value); }
        }
    }
}

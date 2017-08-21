using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Tram.CBTC.NRIET.Constant;

namespace Tram.CBTC.NRIET.View.Contents.Main.RegionRadar
{
    /// <summary>
    /// RadarView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RadarContent, IsDefaultView = true)]
    public partial class RadarView : UserControl
    {
        public RadarView()
        {
            InitializeComponent();
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
          //Ellipse newTarget1 = new Ellipse();
          //  newTarget1.Fill = new SolidColorBrush(Colors.Red);
          //  newTarget1.Width = 12;
          //  newTarget1.Height = 12;

          //  newTarget1.SetValue(Canvas.LeftProperty, (double)20);
          //  newTarget1.SetValue(Canvas.TopProperty, (double)30);


          //  Ellipse newTarget2 = new Ellipse();
          //  newTarget2.Fill = new SolidColorBrush(Colors.Red);
          //  newTarget2.Width = 12;
          //  newTarget2.Height = 12;

          //  newTarget2.SetValue(Canvas.LeftProperty, (double)60);
          //  newTarget2.SetValue(Canvas.TopProperty, (double)50);

          //  CanvasRadar.Children.Add(newTarget1);
          //  CanvasRadar.Children.Add(newTarget2);

        }
    }
}

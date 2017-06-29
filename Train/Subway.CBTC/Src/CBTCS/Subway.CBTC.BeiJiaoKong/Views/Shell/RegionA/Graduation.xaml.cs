using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Subway.CBTC.BeiJiaoKong.Models.RegionA;

namespace Subway.CBTC.BeiJiaoKong.Views.Shell.RegionA
{
    /// <summary>
    /// Graduation.xaml 的交互逻辑
    /// </summary>
    public partial class Graduation : UserControl
    {
        public Graduation()
        {
            InitializeComponent();
            InitLine();
        }

        public void InitLine()
        {
            this.Grid.Children.Clear();

            foreach (var d in GraduationResouce.Instance.Graduation.Keys)
            {
                const int x1 = 29;
                const int z2 = 39;
                double y = 160 * GraduationResouce.Instance.Graduation[d] + 10;
                var line = new Line()
                {
                    X1 = x1,
                    X2 = z2,
                    Y1 = y,
                    Y2 = y,
                    Stroke = Brushes.White,
                    StrokeThickness = 2
                };
                var tmp = d.ToString();
                if (tmp.Equals("1"))
                {
                    tmp = "m" + tmp;
                }
                var txt = new TextBlock()
                {
                    Margin = new Thickness(0, y - 7, 25, 0),

                    Text = tmp,
                    Foreground = Brushes.LightGray,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Right
                };
                Grid.Children.Add(line);
                Grid.Children.Add(txt);
            }
        }
    }
}
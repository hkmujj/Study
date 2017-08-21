using MMI.Facility.WPFInfrastructure.Behaviors;
using Tram.CBTC.Casco.Constant;

namespace Tram.CBTC.Casco.View.Layout
{
    /// <summary>
    /// ShellContentLayout.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellContent, IsDefaultView = true)]
    public partial class ShellContentLayout
    {
        public ShellContentLayout()
        {
            InitializeComponent();
            Loaded += ShellContentLayout_Loaded;
        }

        private void ShellContentLayout_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "a.txt"), true, Encoding.UTF8);
            //foreach (var child in Grid.Children)
            //{
            //    var ch = child as ContentControl;
            //    sw.WriteLine("Name is{0}: Width:{1} Height{2}",ch.Name,ch.RenderSize.Width,ch.RenderSize.Height);
            //}
            //sw.Close();
        }
    }
}

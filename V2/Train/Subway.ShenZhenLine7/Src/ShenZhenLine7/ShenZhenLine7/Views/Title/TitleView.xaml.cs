using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine7.Constance;

namespace Subway.ShenZhenLine7.Views.Title
{
    /// <summary>
    ///     TitleView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.Title, IsDefaultView = true)]
    public partial class TitleView : UserControl
    {
        /// <summary>
        /// </summary>
        public TitleView()
        {
            InitializeComponent();
        }
    }
}

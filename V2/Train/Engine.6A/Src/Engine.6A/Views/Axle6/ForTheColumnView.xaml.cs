using Engine._6A.Constance;
using Engine._6A.Interface;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Axle6
{
    /// <summary>
    /// ForTheColumnView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.DataMonitorTabRegion, Priority = 3)]
    public partial class ForTheColumnView : ITabItemInfoProvider, IButtonResponse
    {
        private bool m_IsCurrent;

        public ForTheColumnView()
        {
            InitializeComponent();
        }
        public string HeadName { get { return "列 供"; } }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            IsCurrent = true;
        }

        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns>
        /// <see langword="true"/> if this instance accepts the navigation request; otherwise, <see langword="false"/>.
        /// </returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            IsCurrent = false;
        }

        /// <summary>
        /// 在当前视图
        /// </summary>
        public bool IsCurrent
        {
            get { return m_IsCurrent; }
             set
            {
                m_IsCurrent = value;
                ColumnButton.IsCurrent = value;
            }
        }
    }
}

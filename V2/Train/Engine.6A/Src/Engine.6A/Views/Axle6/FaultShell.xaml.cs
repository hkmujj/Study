using Engine._6A.Constance;
using Engine._6A.Event;
using Engine._6A.Interface;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Axle6
{
    /// <summary>
    /// FaultShell.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentRegion)]
    public partial class FaultShell : IButtonResponse
    {
        public FaultShell()
        {
            InitializeComponent();
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ButtonEvent>().Subscribe(args =>
            {
                if (IsCurrent && IsLoaded)
                {
                    if (args == ButttonType.Down)
                    {
                        if (TabControl.SelectedIndex < TabControl.Items.Count - 1)
                        {
                            TabControl.SelectedIndex++;
                        }
                    }
                    else if (args == ButttonType.Up)
                    {
                        if (TabControl.SelectedIndex > 0)
                        {
                            TabControl.SelectedIndex--;
                        }
                    }
                }

            },ThreadOption.UIThread);
        }

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
        public bool IsCurrent { get; set; }
    }
}

using System;
using System.Linq;
using System.Windows.Controls;
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
    /// SystemSettingShell.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentRegion)]
    public partial class SystemSettingShell : IButtonResponse
    {
        public SystemSettingShell()
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
            TabControl.SelectionChanged += TabControl_SelectionChanged;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count == 1)
            {
                var tmp = TabControl.Items.OfType<IButtonResponse>().FirstOrDefault();
                var col = e.AddedItems[0] as IButtonResponse;
                if (col != null)
                {
                    tmp.IsCurrent = true;
                }
                else
                {
                    var name = (Type.GetType(e.AddedItems[0].ToString())?.GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault() as ViewExportAttribute)?.RegionName.Equals(RegionNames.TrainInfo);
                    var s =
                        (e.AddedItems[0].GetType().GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault() as ViewExportAttribute)?.RegionName.Equals(RegionNames.TrainInfo);
                    if (name == null || !(bool)name)
                    {
                        tmp.IsCurrent = false;
                    }

                }
            }
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

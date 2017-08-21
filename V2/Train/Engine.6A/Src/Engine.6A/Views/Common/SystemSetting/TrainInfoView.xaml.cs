using System;
using System.Linq;
using System.Windows;
using Engine._6A.Constance;
using Engine._6A.Event;
using Engine._6A.EventArgs;
using Engine._6A.Interface;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.SystemSetting
{
    /// <summary>
    /// TrainInfoView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.SystemTabTabRegion, Priority = 2)]
    public partial class TrainInfoView : ITabItemInfoProvider, IButtonResponse
    {
        public TrainInfoView()
        {
            InitializeComponent();
            Button.SelectedIndexChanged += Button_SelectedIndexChanged;

            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ButtonEvent>().Subscribe(type =>
            {
                if (IsCurrent && IsLoaded)
                {
                    switch (type)
                    {
                        case ButttonType.F1:
                            break;
                        case ButttonType.F2:
                            break;
                        case ButttonType.F3:
                            break;
                        case ButttonType.F4:
                            break;
                        case ButttonType.F5:
                            break;
                        case ButttonType.Up:
                            break;
                        case ButttonType.Down:
                            break;
                        case ButttonType.Left:
                            if (Button.SelectedIndex > 0)
                            {
                                Button.SelectedIndex--;
                            }
                            break;
                        case ButttonType.Right:
                            if (Button.StateCollection != null)
                            {
                                if (Button.SelectedIndex < Button.StateCollection.Count() - 1)
                                {
                                    Button.SelectedIndex++;
                                }
                            }
                            break;
                        case ButttonType.OneConfirm:
                            break;
                        case ButttonType.Next:
                            break;
                        case ButttonType.Last:
                            break;
                        case ButttonType.Test:
                            break;
                        case ButttonType.TwoConfirm:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(type), type, null);
                    }
                }

            }, ThreadOption.UIThread);
        }

        private void Button_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            var eventArgs = e as SelectedIndexChangedRoutedEventArgs;
            if (eventArgs != null)
            {
                var evnet = ServiceLocator.Current.GetInstance<IEventAggregator>();
                evnet.GetEvent<NavigateEvent>().Publish(new NavigateEventArgs()
                {
                    Region = ContentControl.Tag.ToString(),
                    Name = eventArgs.StateCollection.ToArray()[eventArgs.NewIndex].ToString(),
                });

            }
        }

        public string HeadName
        {
            get { return "机车信息"; }
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {

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

        }

        /// <summary>
        /// 在当前视图
        /// </summary>
        public bool IsCurrent { get; set; }
    }
}

using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
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
    /// AxleButtonView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ButtonRegion)]
    [Export]
    public partial class AxleButtonView : IButtonResponse
    {
        public AxleButtonView()
        {
            InitializeComponent();
            IsVisibleChanged += AxleButtonView_IsVisibleChanged;
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ButtonEvent>().Subscribe((args) =>
            {
                if (IsCurrent && IsLoaded)
                {
                    switch (args)
                    {
                        case ButttonType.F1:
                            F1.IsChecked = true;
                            F1.Command?.Execute(F1.CommandParameter);
                            break;
                        case ButttonType.F2:
                            F2.IsChecked = true;
                            F2.Command?.Execute(F2.CommandParameter);
                            break;
                        case ButttonType.F3:
                            F3.IsChecked = true;
                            F3.Command?.Execute(F3.CommandParameter);
                            break;
                        case ButttonType.F4:
                            F4.IsChecked = true;
                            F4.Command?.Execute(F4.CommandParameter);
                            break;
                        case ButttonType.F5:
                            F5.IsChecked = true;
                            F5.Command?.Execute(F5.CommandParameter);
                            break;
                        case ButttonType.Up:
                            break;
                        case ButttonType.Down:
                            break;
                        case ButttonType.Left:
                            break;
                        case ButttonType.Right:
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
                            throw new ArgumentOutOfRangeException(nameof(args), args, null);
                    }
                }

            },ThreadOption.UIThread);
        }

        private void AxleButtonView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue || (bool)e.OldValue)
            {
                return;
            }
            foreach (var col in ButtonGrid.Children.OfType<RadioButton>())
            {
                col.IsChecked = false;
            }
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

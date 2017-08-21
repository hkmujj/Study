using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Engine._6A.Constance;
using Engine._6A.Event;
using Engine._6A.EventArgs;
using Engine._6A.Interface;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Axle8
{
    /// <summary>
    /// Axle8ButtonView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ButtonRegion)]
    [Export]
    public partial class Axle8ButtonView : IButtonResponse
    {
        public Axle8ButtonView()
        {
            InitializeComponent();
            MonitorDataButton.PreviewMouseDown += MonitorDataButton_PreviewMouseDown;
            VideoMonitorButton.PreviewMouseDown += MonitorDataButton_PreviewMouseDown;
            MonitorComboBox.SelectionChanged += ComboBoxSelectionChanged;
            VideoComboBox.SelectionChanged += ComboBoxSelectionChanged;
            IsVisibleChanged += Axle8ButtonView_IsVisibleChanged;
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ButtonEvent>().Subscribe((args) =>
            {
                if (IsCurrent && IsLoaded)
                {
                    switch (args)
                    {
                        case ButttonType.F1:
                            MainPage.IsChecked = true;
                            MainPage.Command?.Execute(MainPage.CommandParameter);
                            break;
                        case ButttonType.F2:
                            MonitorDataButton.IsChecked = true;
                            if (MonitorComboBox.Items.Count > 1)
                            {
                                if (MonitorComboBox.SelectedIndex == 0)
                                {
                                    MonitorComboBox.SelectedIndex = 1;
                                }
                                else
                                {
                                    MonitorComboBox.SelectedIndex = 0;
                                }
                            }

                            break;
                        case ButttonType.F3:
                            VideoMonitorButton.IsChecked = true;
                            if (VideoComboBox.Items.Count > 1)
                            {
                                if (VideoComboBox.SelectedIndex == 0)
                                {
                                    VideoComboBox.SelectedIndex = 1;
                                }
                                else
                                {
                                    VideoComboBox.SelectedIndex = 0;
                                }
                            }
                            break;
                        case ButttonType.F4:
                            FaultButton.IsChecked = true;
                            FaultButton.Command?.Execute(FaultButton.CommandParameter);
                            break;
                        case ButttonType.F5:
                            SystemButton.IsChecked = true;
                            SystemButton.Command.Execute(SystemButton.CommandParameter);
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

        private void Axle8ButtonView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
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

        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sd = sender as Control;
            if (e.AddedItems.Count == 1)
            {
                var enventAg = ServiceLocator.Current.GetInstance<IEventAggregator>();
                if (sd != null)
                {
                    var args = new TrainChangedEventAgrs
                    {
                        Name = e.AddedItems[0].ToString(),
                        Page = sd.Tag.ToString()
                    };
                    enventAg.GetEvent<TrainChangedEvent>().Publish(args);
                }
                enventAg.GetEvent<NavigateEvent>().Publish(new NavigateEventArgs()
                {
                    Region = RegionNames.ContentRegion,
                    Name = sd.Tag.ToString()
                });

            }
        }

        private void MonitorDataButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var sd = sender as RadioButton;
            if (sd != null)
            {
                sd.IsChecked = true;
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

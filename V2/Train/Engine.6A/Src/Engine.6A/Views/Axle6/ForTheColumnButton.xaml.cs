using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Engine._6A.CommonControl;
using Engine._6A.Constance;
using Engine._6A.Event;
using Engine._6A.EventArgs;
using Engine._6A.Interface;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Engine._6A.Views.Axle6
{
    /// <summary>
    /// ForTheColumnButton.xaml 的交互逻辑
    /// </summary>
    public partial class ForTheColumnButton : IButtonResponse
    {
        private readonly Dictionary<string, Control> m_Controls;
        private readonly IList<ImageBrush> m_Brushes;

        private readonly IEventAggregator m_EventAggregator;
        private bool m_IsCurrent;

        public ForTheColumnButton()
        {
            InitializeComponent();
            m_EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            m_Controls = new Dictionary<string, Control>();
            m_Brushes = new List<ImageBrush>
            {
                (ImageBrush) Resources["ColumnPageImg1"],
                (ImageBrush) Resources["ColumnPageImg2"]
            };
            foreach (var tmp in StackPanels.Children.OfType<RecTextButton>().Where(tmp => !string.IsNullOrEmpty(tmp.Name)))
            {
                m_Controls.Add(tmp.Name, tmp);
            }
            m_EventAggregator.GetEvent<ButtonEvent>().Subscribe((type =>
           {
               if (IsCurrent && IsLoaded)
               {
                   if (type == ButttonType.Left)
                   {
                       if (MultiStateButton.SelectedIndex > 0)
                       {
                           MultiStateButton.SelectedIndex--;
                       }
                   }
                   else if (type == ButttonType.Right)
                   {
                       if (MultiStateButton.StateCollection!=null)
                       {
                           if (MultiStateButton.SelectedIndex < MultiStateButton.StateCollection.Count() - 1)
                           {
                               MultiStateButton.SelectedIndex++;
                           }
                       }
                      
                   }
               }

           }),ThreadOption.UIThread);
            MultiStateButton.SelectedIndexChanged += MultiStateButton_SelectedIndexChanged;
            MultiStateButton.PreviewMouseDown += MultiStateButton_PreviewMouseDown;
            this.Loaded += ForTheColumnButton_Loaded;
            this.IsVisibleChanged += ForTheColumnButton_IsVisibleChanged;
        }

        private void ForTheColumnButton_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue && !(bool)e.OldValue)
            {
                m_EventAggregator.GetEvent<ForTheColumnEven>().Publish(new ForTheColumnEventArgs("TrainUp"));
                var brush1 = (Brush)Resources["ButtonUpThree"];
                var brush2 = (Brush)Resources["ButtonDownThree"];
                var forbrush = (Brush)Resources["WhiteBrush"];
                MultiStateButton.BorderBrush = Brushes.Transparent;
                var tmp = ServiceLocator.Current.GetInstance<MouseEventClass>();
                tmp.ResetBackground(m_Controls, brush1);
                tmp.SetForground(ButtonOne, forbrush);
                tmp.SetBackground(ButtonOne, brush2);
            }
        }

        private void ForTheColumnButton_Loaded(object sender, RoutedEventArgs e)
        {
            m_EventAggregator.GetEvent<ForTheColumnEven>().Publish(new ForTheColumnEventArgs("TrainUp"));
        }

        private void MultiStateButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MultiStateButton.BorderBrush = Brushes.Red;
            var brush1 = (Brush)Resources["ButtonUpThree"];
            var tmp = ServiceLocator.Current.GetInstance<MouseEventClass>();
            tmp.ResetBackground(m_Controls, brush1);


        }

        void MultiStateButton_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            var args = e as SelectedIndexChangedRoutedEventArgs;
            var btn = sender as MultiStateButton;
            if (args != null)
            {
                m_EventAggregator.GetEvent<ColumnEvent>().Publish(new ColumnEventArgs()
                {
                    NewIndex = args.NewIndex,
                    OldIndex = args.OldIndex,
                    StateCollection = args.StateCollection,
                    Region = btn.Tag.ToString(),
                });
            }
        }
        private void ButtonOne_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var col = sender as Button;
            MultiStateButton.BorderBrush = Brushes.Transparent;
            var brush1 = (Brush)Resources["ButtonDowning"];
            var forbrush = (Brush)Resources["BackgroundBrush"];

            if (col != null)
            {
                m_EventAggregator.GetEvent<ForTheColumnEven>().Publish(new ForTheColumnEventArgs(col.Tag.ToString()));

                var tmp = ServiceLocator.Current.GetInstance<MouseEventClass>();
                tmp.SetForground(col, forbrush);
                tmp.SetBackground(col, brush1);
            }
        }

        private void ButtonOne_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            var brush1 = (Brush)Resources["ButtonUpThree"];
            var brush2 = (Brush)Resources["ButtonDownThree"];
            var forbrush = (Brush)Resources["WhiteBrush"];
            var tmp = ServiceLocator.Current.GetInstance<MouseEventClass>();
            var col = sender as Button;
            if (col != null)
            {
                tmp.SetForground(col, forbrush);
                tmp.ResetBackground(m_Controls, brush1);
                tmp.SetBackground(col, brush2);
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
        public bool IsCurrent { get { return m_IsCurrent; } set { m_IsCurrent = value; } }
    }
}

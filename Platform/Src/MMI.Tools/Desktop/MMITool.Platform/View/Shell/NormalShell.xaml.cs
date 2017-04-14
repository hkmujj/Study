using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using MMITool.Infrastructure.ViewModel;

namespace MMITool.Platform.View.Shell
{
    /// <summary>
    /// NormalShell.xaml 的交互逻辑
    /// </summary>
    //[Export("Shell", typeof(Window))]
    [Export]
    internal partial class NormalShell
    {
        private readonly DispatcherTimer m_DispatcherTimer;
        int m_MouseDownCount = 0;

        [ImportingConstructor]
        public NormalShell(IMainViewModel viewModel)
        {
            DataContext = viewModel;
            m_DispatcherTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 300),
                IsEnabled = false
            };
            m_DispatcherTimer.Tick += (s, e1) =>
            {
                m_DispatcherTimer.IsEnabled = false;
                m_MouseDownCount = 0;
            };
            InitializeComponent();
        }

        private void MinButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized; //设置窗口最小化
        }

        private void CloseButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }


        /// <summary>
        /// 标题栏双击事件
        /// </summary>
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_MouseDownCount += 1;
            
            m_DispatcherTimer.IsEnabled = true;

            if (m_MouseDownCount % 2 == 0)
            {
                m_DispatcherTimer.IsEnabled = false;
                m_MouseDownCount = 0;
                WindowState = WindowState == WindowState.Maximized ?
                              WindowState.Normal : WindowState.Maximized;
            }
        }


        private void TitleContentUIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ButtonResotre_OnClick(object sender, RoutedEventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Normal:
                    WindowState = WindowState.Maximized;
                    break;
                case WindowState.Minimized:
                    WindowState = WindowState.Normal;
                    break;
                case WindowState.Maximized:
                    WindowState = WindowState.Normal;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TitleIco_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                Close();
            }
        }
    }
}

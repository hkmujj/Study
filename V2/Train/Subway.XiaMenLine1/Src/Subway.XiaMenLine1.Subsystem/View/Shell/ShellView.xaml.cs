using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Regions;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.ViewModels;

namespace Subway.XiaMenLine1.Subsystem.View.Shell
{
    /// <summary>
    /// DoMain.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class ShellView
    {
        private readonly IRegionManager m_RegionManager;
        private ShellViewModel model;
        private DispatcherTimer timer;
        private DateTime last;
        [ImportingConstructor]
        public ShellView(ShellViewModel viewModel, IRegionManager regionManager)
        {
            m_RegionManager = regionManager;
            InitializeComponent();
            DataContext = viewModel;
            model = viewModel;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if ((DateTime.Now - last).TotalSeconds >= 10)
            {
                Border.Visibility = Visibility.Visible;
            }
            else
            {
                Border.Visibility = Visibility.Hidden;
            }
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsEfeectKey")
            {
                if (model.IsEfeectKey)
                {
                    last = DateTime.Now;
                    timer.Start();
                }
                else
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        Border.Visibility = Visibility.Hidden;
                    }));

                    timer.Stop();
                }
            }
        }

        private void ShellView_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            m_RegionManager.RequestNavigate(RegionNames.MainRootShell, ViewNames.MainRooeShell);
            m_RegionManager.RequestNavigate(RegionNames.ShellContentRegion, ViewNames.ShellContentMainContentView);
            m_RegionManager.RequestNavigate(RegionNames.MainContentContentRegion, ViewNames.MainRunningView);
            m_RegionManager.RequestNavigate(RegionNames.MainRunningChildrenTrainRegion, ViewNames.MainRunningDoorPage);
        }

        private void ShellView_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            last = DateTime.Now;
        }
    }
}

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using CBTC.Infrasturcture.Monitor;
using CBTC.Infrasturcture.ViewModel.Monitor;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;
using Subway.CBTC.THALES.Model;
using Subway.CBTC.THALES.ViewModel;

namespace Subway.CBTC.THALES.View.Shell
{
    public partial class THALESForm : ProjectFormBase
    {
        public THALESForm()
        {
            InitializeComponent();

            RegionManager.SetRegionManager(elementHost1.HostContainer,
                ServiceLocator.Current.GetInstance<IRegionManager>());

            if (!DesignMode)
            {
                AppConfig = GlobalParam.Instance.InitParam.AppConfig;
                AppName = AppConfig.AppName;
                DataPackage = GlobalParam.Instance.InitParam.DataPackage;
                this.Text = GlobalParam.Instance.InitParam.AppConfig.AppName;
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source =
                        new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "THALESResource.xaml")),
                });
            }

            Load += OnLoad;


            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);

        }

        private void OnLoad(object sender, EventArgs eventArgs)
        {
            UserControl tcms;
            if (AppConfig.ActureFormConfig.IsOutterFrameVisible)
            {
                tcms = ServiceLocator.Current.GetInstance<ShellWithButton>();
            }
            else
            {
                tcms = ServiceLocator.Current.GetInstance<ShellWithoutButton>();
            }

            tcms.DataContext = ServiceLocator.Current.GetInstance<DomainViewModel>();

            if (GlobalParam.Instance.IsDebug)
            {
                tcms.Loaded += (o, args) =>
                {
                    var monitor = new DomainMonitorWindow()
                    {

                        DataContext = new MonitorViewModel<DomainViewModel>(ServiceLocator.Current.GetInstance<DomainViewModel>())
                    };
                    monitor.Show();
                };
            }



            elementHost1.Child = tcms;
        }
    }
}
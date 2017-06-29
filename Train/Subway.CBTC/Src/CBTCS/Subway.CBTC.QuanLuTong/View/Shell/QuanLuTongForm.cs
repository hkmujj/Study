using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Subway.CBTC.QuanLuTong.Model;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;
using Subway.CBTC.QuanLuTong.ViewModel;


namespace Subway.CBTC.QuanLuTong.View.Shell
{
    public partial class QuanLuTongForm : ProjectFormBase
    {
        public QuanLuTongForm()
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
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "QuanLuTongResource.xaml")),
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

            elementHost1.Child = tcms;
        }
    }
}
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;
using Motor.HMI.CRH380BG.Services;


namespace Motor.HMI.CRH380BG.View.Shell
{
    public partial class CRH380BGForm : ProjectFormBase
    {
        public CRH380BGForm()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                AppConfig = GlobalParam.Instance.InitParam.AppConfig;
                AppName = AppConfig.AppName;
                DataPackage = GlobalParam.Instance.InitParam.DataPackage;
                Text = GlobalParam.Instance.InitParam.AppConfig.AppName;
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source =
                        new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "CRH380BGResource.xaml")),
                });
            }

            var rmm = ServiceLocator.Current.GetInstance<RegionManagerProvider>();
            var vmm = ServiceLocator.Current.GetInstance<ViewModelManager>();
            if (AppConfig.AppName.ToLower().Contains("traction"))
            {
                RegionManager.SetRegionManager(elementHost1.HostContainer, rmm.MainRegionManager);

            }
            else
            {
                RegionManager.SetRegionManager(elementHost1.HostContainer, rmm.ReserveRegionManager);
            }


            UserControl tcms;
            if (AppConfig.ActureFormConfig.IsOutterFrameVisible)
            {
                tcms = ServiceLocator.Current.GetInstance<ShellWithButton>();
                
            }
            else
            {
                tcms = ServiceLocator.Current.GetInstance<ShellWithoutButton>();
                
            }
            if (AppConfig.AppName.ToLower().Contains("traction"))
            {
                tcms.DataContext = vmm.MainViewModel;

            }
            else
            {
                tcms.DataContext = vmm.ReserveViewModel;
            }
            elementHost1.Child = tcms;
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
        }
    }
}
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Motor.TCMS.CRH400BF.Model;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;
using Motor.TCMS.CRH400BF.Constant;
using Motor.TCMS.CRH400BF.Services;
using Motor.TCMS.CRH400BF.ViewModel;


namespace Motor.TCMS.CRH400BF.View.Shell
{
    public partial class CRH400BFForm : ProjectFormBase
    {
        public CRH400BFForm()
        {
            InitializeComponent();

            var regionProvider = ServiceLocator.Current.GetInstance<IRegionManagerProvider>();
            var regionManager = GlobalParam.Instance.InitParam.AppConfig.AppName == ProjectNames.Main
                ? regionProvider.MainRegionManager
                : regionProvider.ReserveRegionManager;

            RegionManager.SetRegionManager(elementHost1.HostContainer, regionManager);

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
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "CRH400BFResource.xaml")),
                });
            }
            var vmm = ServiceLocator.Current.GetInstance<ViewModelManager>();
            UserControl tcms;
            if (AppConfig.ActureFormConfig.IsOutterFrameVisible)
            {
                tcms = ServiceLocator.Current.GetInstance<MainShellWithButton>();
            }
            else
            {
                tcms = ServiceLocator.Current.GetInstance<MainShellWithoutButton>();
            }
            tcms.DataContext = GlobalParam.Instance.InitParam.AppConfig.AppName == ProjectNames.Main
                ? vmm.MainViewModel
                : vmm.ReserveViewModel;

            elementHost1.Child = tcms;
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);

        }


    }
}
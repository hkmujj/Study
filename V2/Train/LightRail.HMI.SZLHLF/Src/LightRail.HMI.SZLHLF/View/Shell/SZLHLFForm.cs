using System;
using System.Windows;
using LightRail.HMI.SZLHLF.Model;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;
using LightRail.HMI.SZLHLF.Resources;


namespace LightRail.HMI.SZLHLF.View.Shell
{
    public partial class SZLHLFForm : ProjectFormBase
    {
        public SZLHLFForm()
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
                //Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                //{
                //    Source =
                //        new Uri(string.Format("pack://application:,,,/{0};component/Resource/{1}",
                //            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "SZLHLFResource.xaml")),
                //});
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source =
                        new Uri("pack://application:,,,/LightRail.HMI.SZLHLF;component/Resources/SZLHLFResource.xaml")
                });
                System.Windows.Application.Current.Resources.MergedDictionaries.Add(SZLHLFResourceManager.Instance);
            }

            //UserControl tcms;
            //if (AppConfig.ActureFormConfig.IsOutterFrameVisible)
            //{
            //    tcms = ServiceLocator.Current.GetInstance<ShellWithButton>();
            //}
            //else
            //{
            //    tcms = ServiceLocator.Current.GetInstance<ShellWithoutButton>();
            //}
            var main = ServiceLocator.Current.GetInstance<ShellWithButton>();
            elementHost1.Child = main;
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);

            //UserControl tcms;
            //if (AppConfig.ActureFormConfig.IsOutterFrameVisible)
            //{
            //    tcms = new ShellWithButton { DataContext = ServiceLocator.Current.GetInstance<SZLHLFViewModel>() };
            //}
            //else
            //{
            //    tcms = new ShellWithoutButton { DataContext = ServiceLocator.Current.GetInstance<SZLHLFViewModel>() };
            //}
            //elementHost1.Child = tcms;
            //elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            //elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);

        }


    }
}
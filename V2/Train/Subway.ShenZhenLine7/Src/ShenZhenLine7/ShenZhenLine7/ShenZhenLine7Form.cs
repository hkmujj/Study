using System;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;
using Subway.ShenZhenLine7.Models;
using Subway.ShenZhenLine7.ViewModels;
using Subway.ShenZhenLine7.Views.Root;

namespace Subway.ShenZhenLine7
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ShenZhenLine7Form : ProjectFormBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ShenZhenLine7Form()
        {
            InitializeComponent();
            RegionManager.SetRegionManager(elementHost1.HostContainer, ServiceLocator.Current.GetInstance<IRegionManager>());


            if (!DesignMode)
            {
                AppConfig = GlobalParam.Instance.InitParam.AppConfig;
                DataPackage = GlobalParam.Instance.InitParam.DataPackage;
                AppName = AppConfig.AppName;
                Text = AppName;
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/Subway.ShenZhenLine7;component/Resource/ShenZhenLine7Resource.xaml")
                });
            }
            var main = new DoMain { DataContext = ServiceLocator.Current.GetInstance<ShenZhenLine7ViewModel>() };
            elementHost1.Child = main;

            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
        }
    }
}

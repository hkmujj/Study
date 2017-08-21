using System;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;
using Subway.ShenZhenLine9.Models;
using Subway.ShenZhenLine9.ViewModels;
using Subway.ShenZhenLine9.Views.Root;

namespace Subway.ShenZhenLine9
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ShenZhenLine9Form : ProjectFormBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ShenZhenLine9Form()
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
                    Source = new Uri("pack://application:,,,/Subway.ShenZhenLine9;component/Resource/ShenZhenLine9Resource.xaml")
                });
            }
            var main = new DoMain { DataContext = ServiceLocator.Current.GetInstance<ShenZhenLine9ViewModel>() };
            elementHost1.Child = main;

            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
        }

    }
}

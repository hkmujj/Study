using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;
using Subway.WuHanLine6.GIS.Interfaces;
using Subway.WuHanLine6.GIS.Models;
using Subway.WuHanLine6.GIS.ViewModels;
using Subway.WuHanLine6.GIS.Views.Shells;

namespace Subway.WuHanLine6.GIS.Views.App
{
    public partial class WuHanGISForm : ProjectFormBase
    {
        private WuHanGISForm()
        {
            InitializeComponent();
        }

        public WuHanGISForm(SubsystemInitParam intParam, WindowsLocation location) : this()
        {
            RegionManager.SetRegionManager(elementHost1.HostContainer, ServiceLocator.Current.GetInstance<IRegionManager>());


            if (!DesignMode)
            {
                AppConfig = intParam.AppConfig;
                DataPackage = intParam.DataPackage;
                AppName = AppConfig.AppName;
                Text = AppName;
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/Subway.WuHanLine6.GIS;component/Resource/WuHanLine6GISResource.xaml")
                });
            }
            IShell shell = null;
            if (location == WindowsLocation.Left)
            {
                shell = new LeftShell();
                GlobalParams.Instance.Left = shell;

            }
            else
            {
                shell = new RightShell();
                GlobalParams.Instance.Right = shell;
            }
            var us = (UserControl) shell;
            us.DataContext = ServiceLocator.Current.GetInstance<WuHanLine6GisViewModel>();
            elementHost1.Child = us;
            


            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
        }

    }
}

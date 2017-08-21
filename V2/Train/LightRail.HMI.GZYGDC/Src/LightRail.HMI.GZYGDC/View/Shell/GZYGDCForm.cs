using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using LightRail.HMI.GZYGDC.Model;
using LightRail.HMI.GZYGDC.ViewModel;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;


namespace LightRail.HMI.GZYGDC.View.Shell
{
    public partial class GZYGDCForm : ProjectFormBase
    {
        public GZYGDCForm()
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
                    //错误：Resources写成Resource
                    //Source =
                    //    new Uri(string.Format("pack://application:,,,/{0};component/Resource/{1}",
                    //        Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "GZYGDCResource.xaml")),

                    Source =
                        new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "GZYGDCResource.xaml")),

                });
            }

            UserControl tcms;
            if (AppConfig.ActureFormConfig.IsOutterFrameVisible)
            {
                //tcms = ServiceLocator.Current.GetInstance<ShellWithButton>();

                tcms = new ShellWithButton { DataContext = ServiceLocator.Current.GetInstance<DomainViewModel>() };
            }
            else
            {
                //tcms = ServiceLocator.Current.GetInstance<ShellWithoutButton>();

                tcms = new ShellWithoutButton { DataContext = ServiceLocator.Current.GetInstance<DomainViewModel>() };
            }
            elementHost1.Child = tcms;
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);

        }


    }
}
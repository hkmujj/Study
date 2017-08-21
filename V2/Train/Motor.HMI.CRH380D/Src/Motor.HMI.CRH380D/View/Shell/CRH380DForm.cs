using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;
using Motor.HMI.CRH380D.Models;

namespace Motor.HMI.CRH380D.View.Shell
{
    public partial class CRH380DForm : ProjectFormBase
    {
        public CRH380DForm()
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
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "CRH380DResource.xaml"))
                });
            }

            UserControl tcms;
            //if (AppConfig.ActureFormConfig.IsOutterFrameVisible)
            //{
            //    tcms = ServiceLocator.Current.GetInstance<ShellWithButton>();
            //}
            //else
            //{
            //    tcms = ServiceLocator.Current.GetInstance<ShellWithoutButton>();
            //}
            tcms = ServiceLocator.Current.GetInstance<DomainShell>();
            elementHost1.Child = tcms;
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);

        }


    }
}
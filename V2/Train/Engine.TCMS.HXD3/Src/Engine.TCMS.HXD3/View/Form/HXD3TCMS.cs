using System;
using System.Windows;
using Engine.TCMS.HXD3.Model;
using Engine.TCMS.HXD3.View.Shell;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;

namespace Engine.TCMS.HXD3.View.Form
{
    public partial class HXD3TCMS : ProjectFormBase
    {
        public HXD3TCMS()
        {
            InitializeComponent();

            RegionManager.SetRegionManager(elementHost1.HostContainer, ServiceLocator.Current.GetInstance<IRegionManager>());

            if (!DesignMode)
            {
                AppConfig = GlobalParam.Instance.InitParam.AppConfig;
                AppName = AppConfig.AppName;
                DataPackage = GlobalParam.Instance.InitParam.DataPackage;
                this.Text = GlobalParam.Instance.InitParam.AppConfig.AppName;
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/Engine.TCMS.HXD3;component/Resource/HXD3TCMSResource_CH.xaml")
                });
            }

            var tcms = ServiceLocator.Current.GetInstance<TCMSShell>();
            elementHost1.Child = tcms;
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
        }
    }
}

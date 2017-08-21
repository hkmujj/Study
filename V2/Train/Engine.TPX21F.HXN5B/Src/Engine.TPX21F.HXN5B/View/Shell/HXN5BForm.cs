using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.ViewModel;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;


namespace Engine.TPX21F.HXN5B.View.Shell
{
    public partial class HXN5BForm : ProjectFormBase
    {
        public HXN5BForm()
        {
            InitializeComponent();

            RegionManager.SetRegionManager(elementHost1.HostContainer,
                ServiceLocator.Current.GetInstance<IRegionManager>());

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
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "HXN5BResource.xaml")),
                });
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
            tcms.DataContext = ServiceLocator.Current.GetInstance<HXN5BViewModel>();
            elementHost1.Child = tcms;
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);

        }


    }
}
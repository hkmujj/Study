using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Engine.TCMS.Turkmenistan.Model;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;


namespace Engine.TCMS.Turkmenistan.View.Shell
{
    public partial class TurkmenistanResourceForm : ProjectFormBase
    {
        public TurkmenistanResourceForm()
        {
            InitializeComponent();



            if (!DesignMode)
            {
                AppConfig = GlobalParam.Instance.InitParam.AppConfig;
                AppName = AppConfig.AppName;
                DataPackage = GlobalParam.Instance.InitParam.DataPackage;
                base.Text = GlobalParam.Instance.InitParam.AppConfig.AppName;
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source =
                        new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Converters/TurkmenistanConverter.xaml")),
                });
              
            }
            UserControl tcms = new ShellWithoutButton();
            elementHost1.Child = tcms;
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
            RegionManager.SetRegionManager(elementHost1.HostContainer,
                ServiceLocator.Current.GetInstance<IRegionManager>());

        }


    }
}
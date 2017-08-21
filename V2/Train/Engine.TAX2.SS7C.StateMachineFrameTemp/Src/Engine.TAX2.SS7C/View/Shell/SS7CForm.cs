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
using Engine.TAX2.SS7C.Model;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;


namespace Engine.TAX2.SS7C.View.Shell
{
    public partial class SS7CForm : ProjectFormBase
    {
        public SS7CForm()
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
                        new Uri(string.Format("pack://application:,,,/{0};component/Resource/{1}",
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "SS7CResource.xaml")),
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
            elementHost1.Child = tcms;
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);

        }


    }
}
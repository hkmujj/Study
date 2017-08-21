using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;
using Subway.ShenZhenLine11.ViewModels;

namespace Subway.ShenZhenLine11.Views.Shell
{
    public partial class ShenZhenForm : ProjectFormBase
    {
        public ShenZhenForm()
        {
            InitializeComponent();
        }

        public ShenZhenForm(SubsystemInitParam initParam) : this()
        {
            DataPackage = initParam.DataPackage;
            AppConfig = initParam.AppConfig;
            AppName = AppConfig.AppName;
         
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/Subway.ShenZhenLine11;component/Resource/ShenZhenAppResouce.xaml")
            });

            elementHost1.Child = ServiceLocator.Current.GetInstance<DoMainShell>();
            RegionManager.SetRegionManager(elementHost1.Child, ServiceLocator.Current.GetInstance<IRegionManager>());
        }
    }
}

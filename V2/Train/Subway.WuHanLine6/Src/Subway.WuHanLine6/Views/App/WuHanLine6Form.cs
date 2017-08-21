using System;
using System.Windows;
using CommonUtil.Util;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;
using Subway.WuHanLine6.Models;
using Subway.WuHanLine6.Views.Shell;

namespace Subway.WuHanLine6.Views.App
{
    /// <summary>
    ///
    /// </summary>
    public partial class WuHanLine6Form : ProjectFormBase
    {
        /// <summary>
        ///
        /// </summary>
        public WuHanLine6Form()
        {
            InitializeComponent();

            RegionManager.SetRegionManager(elementHost1.HostContainer, ServiceLocator.Current.GetInstance<IRegionManager>());

            var main = ServiceLocator.Current.GetInstance<DoMainShell>();

            if (!DesignMode)
            {
                AppConfig = GlobalParams.Instance.InitParam.AppConfig;
                DataPackage = GlobalParams.Instance.InitParam.DataPackage;
                AppName = AppConfig.AppName;
                Text = AppName;
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/Subway.WuHanLine6;component/Resource/WuHanResource.xaml")
                });
            }
            elementHost1.Child = main;

            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);

            this.Load += WuHanLine6Form_Load;
            this.VisibleChanged += WuHanLine6Form_VisibleChanged;
            this.Closed += WuHanLine6Form_Closed;
        }

        private void WuHanLine6Form_Closed(object sender, EventArgs e)
        {
            AppLog.Info(string.Format("{0} Is Closed!", "WuHanLine6Form"));
        }

        private void WuHanLine6Form_VisibleChanged(object sender, EventArgs e)
        {
            AppLog.Info(string.Format("{0} Visibility Changed  Current Status :{1}", "WuHanLine6Form", Visible));
        }

        private void WuHanLine6Form_Load(object sender, EventArgs e)
        {
            AppLog.Info("WuHanLine6Form_Load");
        }
    }
}
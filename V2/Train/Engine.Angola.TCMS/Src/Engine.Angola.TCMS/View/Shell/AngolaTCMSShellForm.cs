using System;
using System.Windows;
using System.Windows.Controls;
using Engine.Angola.TCMS.ViewModel;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;

namespace Engine.Angola.TCMS.View.Shell
{
    public partial class AngolaTCMSShellForm : ProjectFormBase
    {
        private SubsystemInitParam m_InitParam;
        private AngolaTCMSShellViewModel m_ViewModel;

        public AngolaTCMSShellForm()
        {
            InitializeComponent();

            Text = "Angola.TCMS";

            elementHost1.Child = ServiceLocator.Current.GetInstance<AngolaTCMSShellContentLayout>();
        }

        public AngolaTCMSShellForm(SubsystemInitParam initParam, AngolaTCMSShellViewModel viewModel)
        {
            this.m_InitParam = initParam;
            this.m_ViewModel = viewModel;

            InitializeComponent();
            RegionManager.SetRegionManager(elementHost1.HostContainer, ServiceLocator.Current.GetInstance<IRegionManager>());

            if (!DesignMode)
            {
                AppConfig = initParam.AppConfig;
                AppName = AppConfig.AppName;
                DataPackage = initParam.DataPackage;
                this.Text = initParam.AppConfig.AppName;
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/Engine.Angola.TCMS;component/Resource/AngolaTCMSResource.xaml")
                });
            }

            UserControl tcms;
            if (initParam.AppConfig.ActureFormConfig.IsOutterFrameVisible)
            {
                tcms = ServiceLocator.Current.GetInstance<AngolaTCMSShellLayout>();
            }
            else
            {
                tcms = ServiceLocator.Current.GetInstance<AngolaTCMSShellContentLayout>();
            }
            
            tcms.DataContext = m_ViewModel;
            elementHost1.Child = tcms;
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
            Text = "Angola.TCMS";

        }
    }
}

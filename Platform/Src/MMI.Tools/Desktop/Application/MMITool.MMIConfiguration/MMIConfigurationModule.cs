using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.MMIConfiguration.Constant;
using MMITool.Addin.MMIConfiguration.Service;
using MMITool.Addin.MMIConfiguration.View;
using MMITool.Addin.MMIConfiguration.View.ConfigureContent;
using MMITool.Addin.MMIConfiguration.ViewModel;
using MMITool.Infrastructure;
using MMITool.Infrastructure.ViewModel;

namespace MMITool.Addin.MMIConfiguration
{
    [ModuleExport(typeof(MMIConfigurationModule), InitializationMode = InitializationMode.WhenAvailable)]
    public class MMIConfigurationModule : IModule
    {
        private readonly IRegionManager m_RegionManager;

        private readonly IApplicationService m_ApplicationService;

        [ImportingConstructor]
        public MMIConfigurationModule(IRegionManager regionManager, IApplicationService applicationService)
        {
            m_RegionManager = regionManager;
            m_ApplicationService = applicationService;
        }

        public void Initialize()
        {
            ServiceLocator.Current.GetInstance<ConfigureRepository>().Initalize();


            var config = ServiceLocator.Current.GetInstance<ConfigurationParam>();

            ValidateSelctableAndUpdate(config);

            m_ApplicationService.MainViewModel.WindWidth = 400;
            m_ApplicationService.MainViewModel.WindHeight = 300;

            var version = FileVersionInfo.GetVersionInfo(this.GetType().Assembly.Location);

            m_ApplicationService.MainViewModel.WindTitle = " MMI Configure V" + version.FileVersion;
            ((IMainViewModel) m_ApplicationService.MainViewModel).HelpCommand =
                new DelegateCommand(
                    () =>
                    {
                        //m_RegionManager.AddToRegion(RegionNames.HelpRegion,
                        //    ServiceLocator.Current.GetInstance<HelpView>());
                        m_RegionManager.RequestNavigate(RegionNames.HelpRegion, new Uri(typeof(HelpView).Name, UriKind.Relative));
                    });
            m_RegionManager.RequestNavigate(RegionNames.MainContent, new Uri(typeof(ConfigurationRootView).Name, UriKind.Relative));
            m_RegionManager.RequestNavigate(ConfigurationRegionNames.ConfigureContentRegion, new Uri(typeof(ConfigureContentView).Name, UriKind.Relative));
            m_RegionManager.RequestNavigate(ConfigurationRegionNames.ConfigureEditRegion, new Uri(typeof(NoneConfigureContentView).Name, UriKind.Relative));
        }

        private void ValidateSelctableAndUpdate(ConfigurationParam config)
        {
            var vm = ServiceLocator.Current.GetInstance<SelectExePathViewModel>();
            vm.Controller.UpdateSelectableItems(config.ConfigurationConfig.SelectableList.Where(File.Exists));

            if (vm.Model.SelctableFullNames.Count != config.ConfigurationConfig.SelectableList.Count)
            {
                config.SaveSelectableItems(vm.Model.SelctableFullNames);
            }

            vm.Model.FileFullName = vm.Model.SelctableFullNames.Any()
                ? vm.Model.SelctableFullNames.First()
                : config.ConfigurationConfig.TargetExeFileFullPath;
        }
    }
}

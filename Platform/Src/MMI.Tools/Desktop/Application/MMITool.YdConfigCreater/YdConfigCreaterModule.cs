using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.YdConfigCreater.View;
using MMITool.Infrastructure;
using MMITool.Infrastructure.ViewModel;

namespace MMITool.Addin.YdConfigCreater
{
    [ModuleExport(typeof(YdConfigCreaterModule), InitializationMode = InitializationMode.WhenAvailable)]
    public class YdConfigCreaterModule : IModule
    {
        private readonly IRegionManager m_RegionManager;

        private readonly IApplicationService m_ApplicationService;

        [ImportingConstructor]
        public YdConfigCreaterModule(IRegionManager regionManager, IApplicationService applicationService)
        {
            m_RegionManager = regionManager;
            m_ApplicationService = applicationService;
        }

        public void Initialize()
        {
            //ServiceLocator.Current.GetInstance<ConfigureRepository>().Initalize();


            //var config = ServiceLocator.Current.GetInstance<ConfigurationParam>();

            //ValidateSelctableAndUpdate(config);

            m_ApplicationService.MainViewModel.WindWidth = 400;
            m_ApplicationService.MainViewModel.WindHeight = 300;

            var version = FileVersionInfo.GetVersionInfo(GetType().Assembly.Location);

            m_ApplicationService.MainViewModel.WindTitle = " Yd Communication Config Creater V" + version.FileVersion;
            ((IMainViewModel) m_ApplicationService.MainViewModel).HelpCommand =
                new DelegateCommand(
                    () =>
                    {
                        m_RegionManager.RequestNavigate(RegionNames.HelpRegion,
                            new Uri(typeof(HelpView).Name, UriKind.Relative));
                    });

            m_RegionManager.RequestNavigate(RegionNames.MainContent, new Uri(typeof(ShellView).Name, UriKind.Relative));
            //m_RegionManager.RequestNavigate(RegionNames.MainContent, new Uri(typeof(ConfigurationRootView).Name, UriKind.Relative));
            //m_RegionManager.RequestNavigate(ConfigurationRegionNames.ConfigureContentRegion, new Uri(typeof(ConfigureContentView).Name, UriKind.Relative));
            //m_RegionManager.RequestNavigate(ConfigurationRegionNames.ConfigureEditRegion, new Uri(typeof(NoneConfigureContentView).Name, UriKind.Relative));
        }
    }
}

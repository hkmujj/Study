using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.MMIDataDebugger.View;
using MMITool.Infrastructure;

namespace MMITool.Addin.MMIDataDebugger
{
    [ModuleExport(typeof(MMIConfigurationModule), InitializationMode = InitializationMode.WhenAvailable)]
    public class MMIConfigurationModule : IModule, IDisposable
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
            m_ApplicationService.MainViewModel.WindWidth = 400;
            m_ApplicationService.MainViewModel.WindHeight = 300;

            var version = FileVersionInfo.GetVersionInfo(GetType().Assembly.Location);

            m_ApplicationService.MainViewModel.WindTitle = "MMI DataDebugger V" + version.FileVersion;
            //((IMainViewModel)m_ApplicationService.MainViewModel).HelpCommand =
            //    new DelegateCommand(
            //        () =>
            //        {
            //            //m_RegionManager.AddToRegion(RegionNames.HelpRegion,
            //            //    ServiceLocator.Current.GetInstance<HelpView>());
            //            m_RegionManager.RequestNavigate(RegionNames.HelpRegion, new Uri(typeof(HelpView).Name, UriKind.Relative));
            //        });
            m_RegionManager.RequestNavigate(RegionNames.MainContent,
                new Uri(typeof(CommunicationDataPageView).Name, UriKind.Relative));
        }

        /// <summary>执行与释放或重置非托管资源相关的应用程序定义的任务。</summary>
        public void Dispose()
        {
        }
    }
}

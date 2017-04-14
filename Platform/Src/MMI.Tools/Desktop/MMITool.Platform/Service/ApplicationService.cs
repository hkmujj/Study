using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using IMainViewModel = MMITool.Infrastructure.ViewModel.IMainViewModel;

namespace MMITool.Platform.Service
{
    [Export(typeof(IApplicationService))]
    [Export]
    public class ApplicationService : IApplicationService
    {
        public Window ShellWindow { get; private set; }

        public IMainViewModel MainViewModel { get; private set; }

        public string ConfigPath { get; private set; }

        public string AppPath { get; private set; }

        public string AddinPath { get; private set; }

        public string AddinConfigPath { get; private set; }

        MMI.Facility.WPFInfrastructure.ViewModel.IMainViewModel IApplicationService.MainViewModel
        {
            get { return MainViewModel; }
        }

        public ApplicationService()
        {
            AppPath = AppDomain.CurrentDomain.BaseDirectory;
            AddinPath = AppPath;
            AddinConfigPath = Path.Combine(AddinPath, "Config");
            ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
            MainViewModel = ServiceLocator.Current.GetInstance<IMainViewModel>();
            ShellWindow = Application.Current.MainWindow;
        }
    }
}

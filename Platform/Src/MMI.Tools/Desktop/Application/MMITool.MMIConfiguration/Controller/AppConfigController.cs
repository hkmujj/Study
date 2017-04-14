using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.DataType.Config;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.MMIConfiguration.Infrastructure;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AppConfigController : ConfigContentControllerBase<AppConfigViewModel>
    {
        protected override bool HasInitalzied { get { return ViewModel.Model.AppConfig != null; } }

        public override void ResetConfig()
        {
            var app = ConcreateRootConfig.AllAppConfigs.FirstOrDefault(f => ViewModel.TargetConfigFile.ToLower().Contains(f.AppPaths.ConfigDirectory.ToLower()));
            if (app == null)
            {
                MessageBox.Show(ServiceLocator.Current.GetInstance<IApplicationService>().ShellWindow,
                    "Can not found app config where file is {0}", ViewModel.TargetConfigFile);
                return;
            }

            ViewModel.Model.AppConfig = XmlModelDeepCopy.Copy((AppConfig)app);
        }

        public override void SaveConfig()
        {
            var app = ConcreateRootConfig.AllAppConfigs.FirstOrDefault(f => ViewModel.TargetConfigFile.ToLower().Contains(f.AppPaths.ConfigDirectory.ToLower())) as AppConfig;
            if (app == null)
            {
                MessageBox.Show(ServiceLocator.Current.GetInstance<IApplicationService>().ShellWindow,
                    "Can not found app config where file is {0}", ViewModel.TargetConfigFile);
                return;
            }
            app.ActureFormViewConfig = ViewModel.Model.AppConfig.ActureFormViewConfig;
            app.AppFileConfig = ViewModel.Model.AppConfig.AppFileConfig;

            SaveConfig(app);
        }
    }
}
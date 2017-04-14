using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.DataType.Config;
using MMI.Facility.DataType.Config.Form;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.MMIConfiguration.Infrastructure;
using MMITool.Addin.MMIConfiguration.Model;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ActureFormConfigController : ConfigContentControllerBase<ActureFormConfigViewModel>
    {
        protected override bool HasInitalzied { get { return ViewModel.Model != null && ViewModel.Model.ActureFormConfig != null; } }

        public override void ResetConfig()
        {
            ViewModel.ActureFormConfigModelCollection =
                ConcreateRootConfig.AppConfigs.Select(
                    s =>
                        new ActureFormConfigModel()
                        {
                            TargetConfigPath = s.AppPaths.ConfigDirectory.ToLower(),
                            ActureFormConfig = XmlModelDeepCopy.Copy((ActureFormConfig)s.ActureFormConfig),
                        }).ToList();

            UpdateCurrentConfigModel();
        }

        public void UpdateCurrentConfigModel()
        {
            if (ViewModel.ActureFormConfigModelCollection != null)
            {
                var app = ViewModel.ActureFormConfigModelCollection.FirstOrDefault(f => ViewModel.TargetConfigFile.ToLower().Contains(f.TargetConfigPath));
                if (app == null)
                {
                    MessageBox.Show(ServiceLocator.Current.GetInstance<IApplicationService>().ShellWindow,
                        string.Format("Can not found app config where file is {0}, maybe system config not have configure this app !", ViewModel.TargetConfigFile));
                    return;
                }

                app.TargetConfigFile = ViewModel.TargetConfigFile;
                ViewModel.Model = app;
            }
        }

        public override void SaveConfig()
        {
            foreach (var model in ViewModel.ActureFormConfigModelCollection)
            {
                SaveConfig(model.ActureFormConfig, model.TargetConfigFile);
            }
        }
    }
}
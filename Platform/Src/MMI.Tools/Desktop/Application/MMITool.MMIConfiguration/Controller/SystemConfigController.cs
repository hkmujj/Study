using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using MMI.Facility.Control.Data;
using MMI.Facility.DataType.Config;
using MMITool.Addin.MMIConfiguration.Infrastructure;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SystemConfigController : ConfigContentControllerBase<SystemConfigViewModel>
    {
        protected override bool HasInitalzied { get { return ViewModel.Model.SystemConfig != null && ViewModel.Model.SystemConfig.SubsystemConfigs.Any(); } }

        public override void SaveConfig()
        {
            SaveConfig(ViewModel.Model.SystemConfig);
            ConcreateRootConfig.SystemConfig = ViewModel.Model.SystemConfig;
        }

        public override void ResetConfig()
        {
            ViewModel.Model.SystemConfig =
                XmlModelDeepCopy.Copy((SystemConfig) ConfigManager.Instance.Config.SystemConfig);
        }
    }
}
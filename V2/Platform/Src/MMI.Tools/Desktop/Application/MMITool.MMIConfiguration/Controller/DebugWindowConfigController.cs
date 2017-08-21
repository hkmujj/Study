using System.ComponentModel.Composition;
using MMI.Facility.Control.Data;
using MMI.Facility.DataType.Config;
using MMITool.Addin.MMIConfiguration.Infrastructure;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.Controller
{
    [Export]
    public class DebugWindowConfigController : ConfigContentControllerBase<DebugWindowConfigViewModel>
    {
        protected override bool HasInitalzied { get { return ViewModel.Model.DebugWindowConfig != null; } }

        public override void ResetConfig()
        {
            ViewModel.Model.DebugWindowConfig =
                XmlModelDeepCopy.Copy((DebugWindowConfig) ConfigManager.Instance.Config.DebugWindowConfig);
        }

        public override void SaveConfig()
        {
            ConcreateRootConfig.DebugWindowConfig = ViewModel.Model.DebugWindowConfig;
            SaveConfig(ViewModel.Model.DebugWindowConfig);
        }
    }
}
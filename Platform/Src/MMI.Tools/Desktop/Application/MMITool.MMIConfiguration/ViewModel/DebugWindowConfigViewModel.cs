using System.ComponentModel.Composition;
using MMITool.Addin.MMIConfiguration.Controller;
using MMITool.Addin.MMIConfiguration.Model;

namespace MMITool.Addin.MMIConfiguration.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DebugWindowConfigViewModel : ConfigureContentEditerViewModeBase<DebugWindowConfigController>
    {
        [ImportingConstructor]
        public DebugWindowConfigViewModel(DebugWindowConfigController controller, DebugWindowConfigModel model)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;

        }
        public DebugWindowConfigModel Model { private set; get; }
    }
}
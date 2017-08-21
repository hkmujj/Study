using System.ComponentModel.Composition;
using MMITool.Addin.MMIConfiguration.Controller;
using MMITool.Addin.MMIConfiguration.Model;

namespace MMITool.Addin.MMIConfiguration.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AppConfigViewModel : ConfigureContentEditerViewModeBase<AppConfigController>
    {
        [ImportingConstructor]
        public AppConfigViewModel(AppConfigController controller, AppConfigModel model)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
        }

        public AppConfigModel Model { private set; get; }
    }
}
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Regions;
using MMITool.Addin.MMIConfiguration.Controller;
using MMITool.Addin.MMIConfiguration.Model;

namespace MMITool.Addin.MMIConfiguration.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class NetConfigViewModel : ConfigureContentEditerViewModeBase<NetConfigController>
    {

        [ImportingConstructor]
        public NetConfigViewModel(NetConfigController controller, NetConfigModel model, ConfigNavigateViewModel configNavigateViewModel)
        {
            Model = model;
            ConfigNavigateViewModel = configNavigateViewModel;
            Controller = controller;
            Controller.ViewModel = this;
        }

        public ConfigNavigateViewModel ConfigNavigateViewModel { private set; get; }

        public NetConfigModel Model { private set; get; }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var self = navigationContext.Parameters.FirstOrDefault(f => f.Key == NetConfigController.UpdateSelfChild);
            if (self.Key == null)
            {
                base.OnNavigatedTo(navigationContext);
            }
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            var self = navigationContext.Parameters.FirstOrDefault(f => f.Key == NetConfigController.UpdateSelfChild);
            if (self.Key == null)
            {
                base.OnNavigatedFrom(navigationContext);
            }
        }
    }
}

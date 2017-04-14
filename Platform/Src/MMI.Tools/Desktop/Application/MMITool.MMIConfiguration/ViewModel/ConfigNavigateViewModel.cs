using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMITool.Addin.MMIConfiguration.Controller;
using MMITool.Addin.MMIConfiguration.Model;

namespace MMITool.Addin.MMIConfiguration.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ConfigNavigateViewModel : NotificationObject
    {
        public SelectExePathViewModel SelectExePathViewModel { private set; get; }

        public ConfigNavigateController Controller { private set; get; }

        public ConfigNavigateModel Model { private set; get; }

        [ImportingConstructor]
        public ConfigNavigateViewModel(SelectExePathViewModel selectExePathViewModel,
            ConfigNavigateController controller, ConfigNavigateModel model)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            SelectExePathViewModel = selectExePathViewModel;
            selectExePathViewModel.Model.ConfigConfigFileCollection.CollectionChanged +=
                Controller.UpdateConfigFileCollection;
        }
    }
}
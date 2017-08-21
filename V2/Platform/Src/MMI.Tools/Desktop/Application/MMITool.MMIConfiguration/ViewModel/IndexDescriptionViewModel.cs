using System.ComponentModel.Composition;
using MMITool.Addin.MMIConfiguration.Controller;
using MMITool.Addin.MMIConfiguration.Model;

namespace MMITool.Addin.MMIConfiguration.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class IndexDescriptionViewModel : ConfigureContentEditerViewModeBase<IndexDescriptionController>
    {
        public IndexDescriptionModel Model { get; private set; }

        [ImportingConstructor]
        public IndexDescriptionViewModel( IndexDescriptionController controller, IndexDescriptionModel model)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
        }
    }
}
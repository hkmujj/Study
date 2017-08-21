using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMITool.Addin.YdConfigCreater.Controller.Condition;
using MMITool.Addin.YdConfigCreater.Model.Condition;

namespace MMITool.Addin.YdConfigCreater.ViewModel.Condition
{
    [Export]
    public class ConditionViewModel: NotificationObject
    {
        [ImportingConstructor]
        public ConditionViewModel(ConditionModel model, ConditionControlller controlller)
        {
            Model = model;
            Controlller = controlller;
            controlller.ViewModel = this;
            controlller.Initalize();
        }

        public ConditionControlller Controlller { get; private set; }

        public ConditionModel Model { get; private set; }
    }
}
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMITool.Addin.YdConfigCreater.Controller;
using MMITool.Addin.YdConfigCreater.Model;
using MMITool.Addin.YdConfigCreater.ViewModel.Condition;
using MMITool.Addin.YdConfigCreater.ViewModel.Result;

namespace MMITool.Addin.YdConfigCreater.ViewModel
{
    [Export]
    public class ShellViewModel : NotificationObject
    {
        [ImportingConstructor]
        public ShellViewModel(ShellModel model, ShellController controller, ResultViewModel resultViewModel, ConditionViewModel conditionViewModel)
        {
            Model = model;
            Controller = controller;
            ResultViewModel = resultViewModel;
            ConditionViewModel = conditionViewModel;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public ShellController Controller { get; private set; }

        public ShellModel Model { get; private set; }

        public ConditionViewModel ConditionViewModel { get; private set; }

        public ResultViewModel ResultViewModel { get; private set; }
    }
}
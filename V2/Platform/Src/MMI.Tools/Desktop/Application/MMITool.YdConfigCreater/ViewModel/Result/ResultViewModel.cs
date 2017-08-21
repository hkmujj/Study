using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMITool.Addin.YdConfigCreater.Controller.Result;
using MMITool.Addin.YdConfigCreater.Model.Result;

namespace MMITool.Addin.YdConfigCreater.ViewModel.Result
{
    [Export]
    public class ResultViewModel : NotificationObject
    {
        [ImportingConstructor]
        public ResultViewModel(ResultModel model, ResultController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public ResultController Controller { get; private set; }

        public ResultModel Model { get; private set; }
    }
}
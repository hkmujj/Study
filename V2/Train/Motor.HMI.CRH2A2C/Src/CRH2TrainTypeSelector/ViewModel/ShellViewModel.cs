using System.ComponentModel.Composition;
using CRH2TrainTypeSelector.Controller;
using CRH2TrainTypeSelector.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace CRH2TrainTypeSelector.ViewModel
{
    [Export]
    public class ShellViewModel : NotificationObject
    {
        [ImportingConstructor]
        public ShellViewModel(ShellModel model, ShellController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
        }

        public ShellModel Model { private set; get; }

        public ShellController Controller { private set; get; }
    }
}
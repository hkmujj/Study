using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Engine.Angola.TCMS.Model;
using Engine.Angola.TCMS.Controller;

namespace Engine.Angola.TCMS.ViewModel
{
    [Export]
    public class RightBtnViewModel : NotificationObject
    {
        [ImportingConstructor]
        public RightBtnViewModel(RightBtnModel model, RightBtnController controller, Lazy<AngolaTCMSShellViewModel> parent)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
            Parent = parent;
        }

        public Lazy<AngolaTCMSShellViewModel> Parent { private set; get; }

        public RightBtnModel Model { private set; get; }

        public RightBtnController Controller { private set; get; }
    }
}

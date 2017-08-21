using System.ComponentModel.Composition;
using Engine.Angola.TCMS.Controller;
using Engine.Angola.TCMS.Model;

namespace Engine.Angola.TCMS.ViewModel
{
    [Export]
    public class AngolaTCMSShellViewModel
    {
        [ImportingConstructor]
        public AngolaTCMSShellViewModel(AngolaTCMSShellModel model, AngolaTCMSShellController controller,RightBtnViewModel rightBtnViewModel)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            RightBtnViewModel = rightBtnViewModel;
        }

        public AngolaTCMSShellController Controller { private set; get; }

        public AngolaTCMSShellModel Model { private set; get; }

        public RightBtnViewModel RightBtnViewModel { private set; get; }
    }
}
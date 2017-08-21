using System.ComponentModel.Composition;
using Subway.TCMS.LanZhou.Controller.Domain;
using Subway.TCMS.LanZhou.Model.Domain;

namespace Subway.TCMS.LanZhou.ViewModel
{
    [Export]
    public class OtherViewModel
    {
        [ImportingConstructor]
        public OtherViewModel(OtherModel model, OtherController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public OtherModel Model { get; private set; }

        public OtherController Controller { get; private set; }
    }
}
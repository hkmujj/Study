using System.ComponentModel.Composition;
using LightRail.HMI.SZLHLF.Model;
using LightRail.HMI.SZLHLF.Controller;

namespace LightRail.HMI.SZLHLF.ViewModel
{
    [Export]
    public class InputPasswordViewModel : ModelBase
    {
        [ImportingConstructor]
        public InputPasswordViewModel(InputTrainNumModel model, InputTrainNumController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public InputTrainNumController Controller { get; private set; }

        public InputTrainNumModel Model { get; private set; }
    }
}

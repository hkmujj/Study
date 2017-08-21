using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Controller.Domain.BrakeSys;
using Engine.TPX21F.HXN5B.Model.Domain.BrakeSys;
using Engine.TPX21F.HXN5B.ViewModel.Domain.BrakeSys;

namespace Engine.TPX21F.HXN5B.ViewModel.Domain
{
    [Export]
    public class BrakeSysViewModel
    {
        [ImportingConstructor]
        public BrakeSysViewModel(BrakeSysModel model, BrakeSysController controller, BrakeSysEventViewModel eventViewModel)
        {
            Model = model;
            Controller = controller;
            EventViewModel = eventViewModel;
            controller.Initalize();
        }

        public BrakeSysController Controller { private set; get; }

        public BrakeSysModel Model { private set; get; }

        public BrakeSysEventViewModel EventViewModel { get; private set; }
    }
}
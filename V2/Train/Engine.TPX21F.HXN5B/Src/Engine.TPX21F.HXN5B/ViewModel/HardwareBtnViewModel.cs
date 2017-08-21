using System;
using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Controller;
using Engine.TPX21F.HXN5B.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.ViewModel
{
    [Export]
    public class HardwareBtnViewModel : NotificationObject
    {
        [ImportingConstructor]
        public HardwareBtnViewModel(HardwareBtnModel model, HardwareBtnController controller, Lazy<HXN5BViewModel> parent)
        {
            Model = model;
            Controller = controller;
            Parent = parent;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public Lazy<HXN5BViewModel> Parent { private set; get; }

        public HardwareBtnModel Model { private set; get; }

        public HardwareBtnController Controller { private set; get; }

    }
}
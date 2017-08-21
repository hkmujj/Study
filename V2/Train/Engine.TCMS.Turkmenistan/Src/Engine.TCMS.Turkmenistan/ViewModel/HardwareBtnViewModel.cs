using System;
using System.ComponentModel.Composition;
using Engine.TCMS.Turkmenistan.Controller;
using Engine.TCMS.Turkmenistan.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.Turkmenistan.ViewModel
{
    [Export]
    public class HardwareBtnViewModel : NotificationObject
    {
        [ImportingConstructor]
        public HardwareBtnViewModel(HardwareBtnModel model, HardwareBtnController controller, Lazy<DomainViewModel> parent)
        {
            Model = model;
            Controller = controller;
            Parent = parent;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public Lazy<DomainViewModel> Parent { private set; get; }

        public HardwareBtnModel Model { private set; get; }

        public HardwareBtnController Controller { private set; get; }

    }
}
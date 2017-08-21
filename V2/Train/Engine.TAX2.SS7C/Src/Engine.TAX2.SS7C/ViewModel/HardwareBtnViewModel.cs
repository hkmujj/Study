using System;
using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Controller;
using Engine.TAX2.SS7C.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel
{
    [Export]
    public class HardwareBtnViewModel : NotificationObject
    {
        [ImportingConstructor]
        public HardwareBtnViewModel(HardwareBtnModel model, HardwareBtnController controller, Lazy<SS7CViewModel> parent)
        {
            Model = model;
            Controller = controller;
            Parent = parent;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public Lazy<SS7CViewModel> Parent { private set; get; }

        public HardwareBtnModel Model { private set; get; }

        public HardwareBtnController Controller { private set; get; }

    }
}
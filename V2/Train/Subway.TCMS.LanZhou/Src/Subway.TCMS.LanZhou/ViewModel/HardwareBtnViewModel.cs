using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Controller;
using Subway.TCMS.LanZhou.Model.Domain;

namespace Subway.TCMS.LanZhou.ViewModel
{
    [Obsolete]
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
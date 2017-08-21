using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Controller;
using Engine.TAX2.SS7C.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel
{
    [Export]
    public class DomainViewModel : NotificationObject
    {
        [ImportingConstructor]
        public DomainViewModel(DomainController controller, DomainModel model, HardwareBtnViewModel hardwareBtnViewModel)
        {
            Controller = controller;
            Model = model;
            HardwareBtnViewModel = hardwareBtnViewModel;
        }


        public DomainController Controller { private set; get; }

        public DomainModel Model { private set; get; }

        public HardwareBtnViewModel HardwareBtnViewModel { private set; get; }
    }
}
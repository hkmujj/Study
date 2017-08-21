using System.ComponentModel.Composition;
using Engine.TCMS.Turkmenistan.Controller;
using Engine.TCMS.Turkmenistan.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.Turkmenistan.ViewModel
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
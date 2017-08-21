using System.ComponentModel.Composition;
using Motor.TCMS.CRH400BF.Controller;
using Motor.TCMS.CRH400BF.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace Motor.TCMS.CRH400BF.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HardwareBtnViewModel : NotificationObject
    {
        [ImportingConstructor]
        public HardwareBtnViewModel(HardwareBtnModel model, HardwareBtnController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            
        }

        public DomainViewModel DomainViewModel{ set; get; }

        public HardwareBtnModel Model { private set; get; }

        public HardwareBtnController Controller { private set; get; }

        public void Initalize(ViewLocation viewLocation)
        {
            Controller.ViewLocation = viewLocation;
            Controller.Initalize();
        }

    }
}
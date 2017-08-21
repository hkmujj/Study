using System.ComponentModel;
using System.ComponentModel.Composition;
using Motor.HMI.CRH380BG.Controller;
using Motor.HMI.CRH380BG.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace Motor.HMI.CRH380BG.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HardwareBtnViewModel : NotificationObject
    {
        [ImportingConstructor]
        public HardwareBtnViewModel(HardwareBtnModel model, HardwareBtnController maincontroller)
        {
            Model = model;
            Controller = maincontroller;
            maincontroller.ViewModel = this;
            maincontroller.Initalize();
            
        }

        [Browsable(false)]
        public CRH380BGViewModel DomainViewModel { set; get; }

        public HardwareBtnModel Model { private set; get; }

        public HardwareBtnController Controller { private set; get; }


    }
}
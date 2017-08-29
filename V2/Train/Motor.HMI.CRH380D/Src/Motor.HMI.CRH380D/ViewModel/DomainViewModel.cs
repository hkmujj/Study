using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Models;

namespace Motor.HMI.CRH380D.ViewModel
{
    [Export]
    public class DomainViewModel : ModelBase
    {
        [ImportingConstructor]
        public DomainViewModel(DomainController controller, DomainModel model)
        {
            Model = model;

            Controller = controller;
            Controller.ViewModel = new Lazy<DomainViewModel>(()=>this);
            Controller.Initalize();

            Instacnce = this;
        }

        [Browsable(false)]
        public static DomainViewModel Instacnce { get; private set; }
       
        public DomainController Controller { private set; get; }
       
        public DomainModel Model { private set; get; }
    }
}
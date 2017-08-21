using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380BG.Controller.Domain;
using Motor.HMI.CRH380BG.Model.Domain.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH380BG.ViewModel.Domain
{
    [Export]
    public class OtherViewModel : NotificationObject
    {
        [ImportingConstructor]
        public OtherViewModel(OtherModel model, OtherController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public OtherModel Model { private set; get; }

        public OtherController Controller { private set; get; }

    }
}

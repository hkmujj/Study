using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Domain;

namespace Motor.HMI.CRH380BG.ViewModel.Domain
{
    [Export]
    public class DomainViewModel : NotificationObject
    {
        [ImportingConstructor]
        public DomainViewModel(DomainModel model, FaultViewModel faultViewModel)
        {
            Model = model;
            FaultViewModel = faultViewModel;
            faultViewModel.Parent = this;
        }

        public CRH380BGViewModel Parent { get; set; }

        public DomainModel Model { private set; get; }

        public FaultViewModel FaultViewModel { private set; get; }
    }
}

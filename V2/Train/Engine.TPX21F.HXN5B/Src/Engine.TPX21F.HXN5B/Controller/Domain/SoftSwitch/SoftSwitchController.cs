using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.Model.Domain.SoftSwitch.Detail;
using Engine.TPX21F.HXN5B.ViewModel.Domain;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TPX21F.HXN5B.Controller.Domain.SoftSwitch
{
    [Export]
    public class SoftSwitchController : ControllerBase<SoftSwitchViewModel>
    {

        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            ViewModel.Model.SoftSwitchItemCollection = new Lazy<ReadOnlyCollection<SoftSwitchItem>>(() =>
                GlobalParam.Instance.SoftSwitchItemConfigs.Value.Select(s => new SoftSwitchItem(s))
                    .ToList()
                    .AsReadOnly()
                );
        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.Model.Domain.TrainSetting.Detail;
using Engine.TPX21F.HXN5B.ViewModel.Domain;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TPX21F.HXN5B.Controller.Domain.TrainSetting
{
    [Export]
    public class TrainSettingController : ControllerBase<TrainSettingViewModel>
    {
        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            InitalizeAutoStart();
        }

        private void InitalizeAutoStart()
        {
            ViewModel.Model.AutoStartModel.StartSystemItems =
                new Lazy<ReadOnlyCollection<AutoStartItem>>(
                    () =>
                        GlobalParam.Instance.AutoStartConfigs.Value.Select(s => new AutoStartItem(s))
                            .ToList()
                            .AsReadOnly());
        }
    }
}
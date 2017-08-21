using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Extension;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Adapter.UpdataDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    class CarFireAlarmStatusUpdataDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]

        public CarFireAlarmStatusUpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var alarm = ViewModel.OtherViewModel.Model.FireAlarm;
            for (int i = 0; i < alarm.Count; i++)
            {
                var it = ViewModel.OtherViewModel.Model.FireAlarm[i];
                it.CarFireAlarmStatus = GetDoorState(args, it.CarFireAlarmStatusConfig);
            }

        }

        private CarFireAlarmStatus GetDoorState(CommunicationDataChangedArgs args, CarFireAlarmStatusConfig it)
        {

            if (DataService.ReadService.GetInBoolOf(it.NormalStatus))
            {
                return CarFireAlarmStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(it.FualtStatus))
            {
                return CarFireAlarmStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(it.FireStatus))
            {
                return CarFireAlarmStatus.Fire;
            }
            if (DataService.ReadService.GetInBoolOf(it.PollutedStatus))
            {
                return CarFireAlarmStatus.Polluted;
            }
            return CarFireAlarmStatus.Invalid;
        }
    }
}

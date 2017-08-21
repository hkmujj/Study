using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.TCMS.LanZhou.Model;
using Subway.TCMS.LanZhou.Model.Domain;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.Model.Domain.Fault;
using Subway.TCMS.LanZhou.Model.Domain.Other;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Controller.Domain
{
    [Export]
    public class OtherController: ControllerBase<OtherViewModel>
    {
        protected OtherModel Model { get { return ViewModel.Model; } }

        public override void Initalize()
        {
            GlobalTimer.Instance.Tick500Ms += OnUpdateCurrentTime;
            Model.FireAlarm = CreateFireAlarm();
        }

        private void OnUpdateCurrentTime(object sender, EventArgs e)
        {
            Model.NowInDDU = DateTime.Now;
        }

        private List<CarFireAlarmItem> CreateFireAlarm()
        {
            return GlobalParam.Instance.CarFireAlarmStatusConfigCollection.Value.Select(config => new CarFireAlarmItem(config)).ToList();
        }
    }
}
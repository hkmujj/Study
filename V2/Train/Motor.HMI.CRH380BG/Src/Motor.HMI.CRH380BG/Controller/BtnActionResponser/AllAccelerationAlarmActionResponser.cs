using Motor.HMI.CRH380BG.Resources.Keys;
using Motor.HMI.CRH380BG.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Domain.Constant;
using Motor.HMI.CRH380BG.Model;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class AllAccelerationAlarmActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {

            NavigateTo(stateViewModel, StateKeys.Root_紧急_复位横向加速度报警);
            stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.AccelerationAlarmType = AccelerationAlarmType.AccelerationAlarm;
            stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.AccelerationAlarmVisible = true;
            switch (stateViewModel.Model.ViewLocation)
            {
                case ViewLocation.Main:
                    break;
                case ViewLocation.Reserve:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

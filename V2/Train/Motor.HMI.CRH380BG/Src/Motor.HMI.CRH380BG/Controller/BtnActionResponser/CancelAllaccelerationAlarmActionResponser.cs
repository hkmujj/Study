using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.Model.Domain.Constant;
using Motor.HMI.CRH380BG.Resources.Keys;
using Motor.HMI.CRH380BG.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class CancelAllaccelerationAlarmActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {

            if (stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.AccelerationAlarmType
                == AccelerationAlarmType.NoSpeedLimit)
            {
                NavigateTo(stateViewModel, StateKeys.Root_紧急_复位横向加速度报警没有限速);
            }
            if (stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.AccelerationAlarmType
                == AccelerationAlarmType.SpeedLimit)
            {
                NavigateTo(stateViewModel, StateKeys.Root_紧急_复位横向加速度报警限速);
            }
            stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.AccelerationAlarmVisible = false;


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

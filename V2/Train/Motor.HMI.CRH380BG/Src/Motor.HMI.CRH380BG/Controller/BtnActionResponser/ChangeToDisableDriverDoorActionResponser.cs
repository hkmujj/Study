using Motor.HMI.CRH380BG.Resources.Keys;
using System;
using System.ComponentModel.Composition;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;
using MMI.Facility.WPFInfrastructure.Event;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class ChangeToDisableDriverDoorActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            NavigateTo(stateViewModel, StateKeys.Root_禁用司机室门);
            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub给评价接口处于门界面, true));

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

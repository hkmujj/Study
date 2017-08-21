using Motor.HMI.CRH380BG.Resources.Keys;
using System;
using System.ComponentModel.Composition;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;
using Motor.HMI.CRH380BG.Model.Domain.Constant;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class ChangeToGroupHangsActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            var ss = stateViewModel.DomainViewModel.Domain.Model.SystemModel;
            if (ss.GroupHangsModel.SpeedVisible == true)
            {
                NavigateTo(stateViewModel,StateKeys.Root_系统_编组联挂4);
            }
            else
            {
                if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Open
                    || ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Open)
                {
                    NavigateTo(stateViewModel, StateKeys.Root_系统_编组联挂1);
                }
                else if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Close && ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Close)
                {
                    NavigateTo(stateViewModel, StateKeys.Root_系统_编组联挂);
                }
                else if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Close
                        && (ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Fault
                        || ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Hanged
                        || ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.ReadyHang
                        || ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Running))
                {
                    NavigateTo(stateViewModel, StateKeys.Root_系统_编组联挂2);
                }
                else if ((ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Fault
                        || ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Hanged
                        || ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.ReadyHang
                        || ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Running)
                        && ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Close)
                {
                    NavigateTo(stateViewModel, StateKeys.Root_系统_编组联挂3);
                }
                else
                {
                    NavigateTo(stateViewModel, StateKeys.Root_系统_编组联挂4);
                }
            }

            


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

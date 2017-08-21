using Motor.HMI.CRH380BG.Resources.Keys;
using System;
using System.ComponentModel.Composition;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;
using Motor.HMI.CRH380BG.Model.Domain.Constant;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class ChangeToCloseCarCoverActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            var ss = stateViewModel.DomainViewModel.Domain.Model.SystemModel;

            if (ss.GroupHangsModel.EMU2CloseCarCoverVisible == true)
            {
                if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Open
                    && ss.GroupHangsModel.EMU2AfterHangType == GroupHangsType.Open)
                {
                    NavigateTo(stateViewModel, StateKeys.Root_系统_关闭车钩罩6);
                }
                else if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Open && ss.GroupHangsModel.EMU2AfterHangType != GroupHangsType.Open)
                {
                    NavigateTo(stateViewModel, StateKeys.Root_系统_关闭车钩罩4);
                }
                else if (ss.GroupHangsModel.EMU1FrontHangType != GroupHangsType.Open && ss.GroupHangsModel.EMU2AfterHangType == GroupHangsType.Open)
                {
                    NavigateTo(stateViewModel, StateKeys.Root_系统_关闭车钩罩5);
                }
                else
                {
                    NavigateTo(stateViewModel, StateKeys.Root_系统_关闭车钩罩);
                }
            }
            else
            {
                if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Open
                    && ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Open)
                {
                    NavigateTo(stateViewModel, StateKeys.Root_系统_关闭车钩罩3);
                }
                else if (ss.GroupHangsModel.EMU1FrontHangType == GroupHangsType.Open && ss.GroupHangsModel.EMU1AfterHangType != GroupHangsType.Open)
                {
                    NavigateTo(stateViewModel, StateKeys.Root_系统_关闭车钩罩1);
                }
                else if (ss.GroupHangsModel.EMU1FrontHangType != GroupHangsType.Open && ss.GroupHangsModel.EMU1AfterHangType == GroupHangsType.Open)
                {
                    NavigateTo(stateViewModel, StateKeys.Root_系统_关闭车钩罩2);
                }
                else
                {
                    NavigateTo(stateViewModel, StateKeys.Root_系统_关闭车钩罩);
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

using Motor.HMI.CRH380BG.Resources.Keys;
using System;
using System.ComponentModel.Composition;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.Model.Domain.Constant;
using Motor.HMI.CRH380BG.ViewModel;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class ChangeToAutoSpeedControlActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            

            switch (stateViewModel.DomainViewModel.Domain.Model.MainData.AutoSpeedControlType)
            {
                case AutoSpeedControlType.AscCloes:
                    NavigateTo(stateViewModel, StateKeys.Root_自动速度控制);
                    break;
                case AutoSpeedControlType.Speed2:
                    NavigateTo(stateViewModel, StateKeys.Root_自动速度控制_2km);
                    break;
                case AutoSpeedControlType.Speed5:
                    NavigateTo(stateViewModel, StateKeys.Root_自动速度控制_5km);
                    break;
                case AutoSpeedControlType.Speed10:
                    NavigateTo(stateViewModel, StateKeys.Root_自动速度控制_10km);
                    break;
                case AutoSpeedControlType.Speed25:
                    NavigateTo(stateViewModel, StateKeys.Root_自动速度控制_25km);
                    break;
                case AutoSpeedControlType.MaxSpeed:
                    NavigateTo(stateViewModel, StateKeys.Root_自动速度控制_最大速度);
                    break;
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

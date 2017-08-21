using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;
using Motor.HMI.CRH380BG.Model.ConfigModel;
using Motor.HMI.CRH380BG.Model.Domain.Constant;
using Motor.HMI.CRH380BG.Resources.Keys;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class ChangeToFaultlVEqual0ActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            int x;
            x = stateViewModel.DomainViewModel.Domain.FaultViewModel.Model.AllItems.Count;
            if (x == 0)
            {
                NavigateTo(stateViewModel, StateKeys.Root_故障_V等于0);
            }
            else
            {
                stateViewModel.DomainViewModel.Domain.FaultViewModel.Model.CurrentSelectedItem.FaultReadState = FaultReadState.Read;
                NavigateTo(stateViewModel, StateKeys.Root_故障_V等于0);
            }
            //stateViewModel.DomainViewModel.Domain.FaultViewModel.Model.CurrentSelectedItem.FaultReadState=FaultReadState.Read;
            //NavigateTo(stateViewModel, StateKeys.Root_故障_V等于0);

            

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

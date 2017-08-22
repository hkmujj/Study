using Motor.HMI.CRH380BG.Resources.Keys;
using System;
using System.ComponentModel.Composition;
using DevExpress.Xpf.Editors.Helpers;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;
using MMI.Facility.WPFInfrastructure.Event;
using Motor.HMI.CRH380BG.Model.Domain.Constant;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class ChangeToDeleteFaultFlagActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
           
           
        
            int x;
            int y;
            y = stateViewModel.DomainViewModel.Domain.FaultViewModel.Model.AllItems.Count;
            x = stateViewModel.Model.ViewModel.DomainViewModel.Domain.Model.FaultModel.AllPagedItems.CurrentListIndex;
            if (y == 0)
            {
                return;
            }

            else
            {
                stateViewModel.Model.ViewModel.DomainViewModel.Domain.Model.FaultModel.AllItems[x].FaultReadState = FaultReadState.NotRead;
                ViewModel.Value.Domain.FaultViewModel.Controller.GotoNext();
                ViewModel.Value.Domain.FaultViewModel.Controller.GotoPre();
            }
            ViewModel.Value.Domain.FaultViewModel.Controller.AllReportFaultReadState();




        }
    }
}

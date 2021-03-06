﻿using Motor.HMI.CRH380BG.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class CloseCarCover2ActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            stateViewModel.DomainViewModel.Domain.Model.SystemModel.GroupHangsModel.ConfirmInfo1Visible = false;
            stateViewModel.DomainViewModel.Domain.Model.SystemModel.GroupHangsModel.ConfirmInfo2Visible = true;
        }
    }
}

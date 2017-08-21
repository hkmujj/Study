using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class IlluminationUpActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            switch (stateViewModel.DomainViewModel.Domain.Model.Switch.IlluminationModel.SelectNum)
            {
                case 1:
                    stateViewModel.DomainViewModel.Domain.Model.Switch.IlluminationModel.SelectNum = 0;
                    break;
                case 2:
                    stateViewModel.DomainViewModel.Domain.Model.Switch.IlluminationModel.SelectNum = 1;
                    break;
                case 3:
                    stateViewModel.DomainViewModel.Domain.Model.Switch.IlluminationModel.SelectNum = 2;
                    break;
                case 4:
                    stateViewModel.DomainViewModel.Domain.Model.Switch.IlluminationModel.SelectNum = 2;
                    break;
                case 5:
                    stateViewModel.DomainViewModel.Domain.Model.Switch.IlluminationModel.SelectNum = 2;
                    break;
                case 6:
                    stateViewModel.DomainViewModel.Domain.Model.Switch.IlluminationModel.SelectNum = 2;
                    break;
                case 7:
                    stateViewModel.DomainViewModel.Domain.Model.Switch.IlluminationModel.SelectNum = 2;
                    break;
                case 8:
                    stateViewModel.DomainViewModel.Domain.Model.Switch.IlluminationModel.SelectNum = 2;
                    break;
                case 9:
                    stateViewModel.DomainViewModel.Domain.Model.Switch.IlluminationModel.SelectNum = 2;
                    break;
                case 10:
                    stateViewModel.DomainViewModel.Domain.Model.Switch.IlluminationModel.SelectNum = 2;
                    break;
              }
         }
    }
}

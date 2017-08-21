using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using LightRail.HMI.SZLHLF.Extension;
using LightRail.HMI.SZLHLF.Model.SettingModel;
using LightRail.HMI.SZLHLF.Resources.Keys;
using MMI.Facility.WPFInfrastructure.Interfaces;
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;

namespace LightRail.HMI.SZLHLF.Controller
{
    [Export]
    public class StationSetController : ControllerBase<Lazy<StationSetInfoModel>>
    {
        [ImportingConstructor]
        public StationSetController(Lazy<StationSetInfoModel> viewModel)
            : base(viewModel)
        {
            StationEnterCommand = new DelegateCommand(OnStationSet);
        }

        public override void Initalize()
        {
            foreach (var VARIABLE in ViewModel.Value.AllStartStationItem)
            {
                VARIABLE.isCheck = false;
            }

            foreach (var VARIABLE in ViewModel.Value.AllEndStationItem)
            {
                VARIABLE.isCheck = false;
            }
        }

        public void Clear()
        {
            OutFolatKey.起点站.SendFloatData(0);
            OutFolatKey.终点站.SendFloatData(0);
        }

        public void OnStationSet()
        {
            bool bIsCheck = false;
            foreach (var VARIABLE in ViewModel.Value.AllStartStationItem)
            {
                if (VARIABLE.isCheck)
                {
                    OutFolatKey.起点站.SendFloatData(VARIABLE.Index);
                    bIsCheck = true;
                    break;
                }
            }
            if (!bIsCheck)
            {
                OutFolatKey.起点站.SendFloatData(0);
            }

            bIsCheck = false;
            foreach (var VARIABLE in ViewModel.Value.AllEndStationItem)
            {
                if (VARIABLE.isCheck)
                {
                    OutFolatKey.终点站.SendFloatData(VARIABLE.Index);
                    bIsCheck = true;
                    break;
                }
            }
            if (!bIsCheck)
            {
                OutFolatKey.终点站.SendFloatData(0);
            }
        }
        
        /// <summary>
        /// 确认
        /// </summary>
        public ICommand StationEnterCommand { get; set; }
    }
}
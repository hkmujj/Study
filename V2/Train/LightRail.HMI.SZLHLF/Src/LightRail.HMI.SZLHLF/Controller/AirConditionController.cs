using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using LightRail.HMI.SZLHLF.Extension;
using LightRail.HMI.SZLHLF.Model.AirModel;
using LightRail.HMI.SZLHLF.Resources.Keys;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace LightRail.HMI.SZLHLF.Controller
{
    [Export]
    public class AirConditionController : ControllerBase<Lazy<AirConditionModel>>
    {
        [ImportingConstructor]
        public AirConditionController(Lazy<AirConditionModel> viewModel)
            : base(viewModel)
        {
            SetTemPeratureCommand = new DelegateCommand<string>(SetSetTemPerature);
        }

        public override void Initalize()
        { 
            //默认自动模式
            ViewModel.Value.AllModelSelects[0].IsChecked = true;
            //默认强风
            ViewModel.Value.AllWindSpeedSet[0].IsChecked = true;
            //默认一档
            ViewModel.Value.AllNewAirValueSet[0].IsChecked = true;
        }

        public void Clear()
        {
            //温度默认0
            ViewModel.Value.TemperatureAdjust = 0;
            OutFolatKey.空调温度.SendFloatData(ViewModel.Value.TemperatureAdjust);
            foreach (var VARIABLE in ViewModel.Value.AllModelSelects)
            {
                VARIABLE.IsChecked = false;
            }
            foreach (var VARIABLE in ViewModel.Value.AllWindSpeedSet)
            {
                VARIABLE.IsChecked = false;
            }
            foreach (var VARIABLE in ViewModel.Value.AllNewAirValueSet)
            {
                VARIABLE.IsChecked = false;
            }
        }

        private void SetSetTemPerature(string strName)
        {
            if (strName.Equals("BtnUp"))
            {
                ViewModel.Value.TemperatureAdjust = Math.Min(ViewModel.Value.TemperatureAdjust + 1, 100);
            }
            else if (strName.Equals("BtnDown"))
            {
                ViewModel.Value.TemperatureAdjust = Math.Max(ViewModel.Value.TemperatureAdjust - 1, 0);
            }
            OutFolatKey.空调温度.SendFloatData(ViewModel.Value.TemperatureAdjust);

            foreach (var b in ViewModel.Value.AllModelSelects)
            {
                b.IsChecked = false;
            }
        }

        public ICommand SetTemPeratureCommand { get; private set; }
    }
}
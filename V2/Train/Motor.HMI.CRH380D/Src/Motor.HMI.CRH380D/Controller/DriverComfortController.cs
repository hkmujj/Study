using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Extension;
using Motor.HMI.CRH380D.Models.Model;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller
{
    [Export]
    public class DriverComfortController : ControllerBase<DriverComfortModel>
    {
        [ImportingConstructor]
        public DriverComfortController()
        {
            CheckCommand = new DelegateCommand<string>(OnCheckCommand);
            TrainCheckedCommand = new DelegateCommand<string>(OnTrainChecked);
            AddTemperatureCommand = new DelegateCommand(OnAddTemperature);
            SubTemperatureCommand = new DelegateCommand(OnSubTemperature);
            AddAirCommand = new DelegateCommand(OnAddAir);
            SubAirCommand = new DelegateCommand(OnSubAir);
        }
        public override void Initalize()
        {
            //选中状态
            ViewModel.Car0IsCheck = false;
            ViewModel.Car1IsCheck = false;
        }

        public void Clear()
        {

        }

        /// <summary>
        /// 界面跳转清除选中状态
        /// </summary>
        public void ClearState()
        {
            //选中状态
            ViewModel.Car0IsCheck = false;
            ViewModel.Car1IsCheck = false;

            foreach (var value in ViewModel.CarComfortUnits)
            {
                value.IsChecked = false;
            }
        }

        /// <summary>
        /// 车辆选中
        /// </summary>
        public ICommand TrainCheckedCommand { get; set; }

        /// <summary>
        /// 车辆点击处理函数
        /// </summary>
        public void OnTrainChecked(string strName)
        {
            switch (strName)
            {
                case "0":
                    if (ViewModel.Car1IsCheck)
                    {
                        ViewModel.Car1IsCheck = false;
                    }
                    break;
                case "1": 
                    if (ViewModel.Car0IsCheck)
                    {
                        ViewModel.Car0IsCheck = false;
                    }
                    break;
            }
            SetTemperatureButtonEnable();
        }

        /// <summary>
        /// HVAC选中
        /// </summary>
        public ICommand CheckCommand { get; set; }



        /// <summary>
        /// HVAC点击处理函数
        /// </summary>
        public void OnCheckCommand(string ButtonNameTemp)
        {
            foreach (var value in ViewModel.CarComfortUnits)
            {
                if (value.IsChecked && value.Car.ToString() != ButtonNameTemp)
                {
                    value.IsChecked = false;
                }
            }

            SetHVACButtonEnable();
        }

        /// <summary>
        /// 设置HVAC开/关可选状态
        /// </summary>
        public void SetHVACButtonEnable()
        {
            bool IsEnable = false;

            //车辆
            foreach (var value in ViewModel.CarComfortUnits)
            {
                if (value.IsChecked)
                {
                    IsEnable = true;
                    break;
                }
            }

            DomainViewModel.Instacnce.Model.StateInterface.BtnB1.IsEnable = IsEnable;
        }

        /// <summary>
        /// 点击HVAC开/关按钮发送数据
        /// </summary>
        public void HVACOpenAndCloseSendData()
        {
            foreach (var value in ViewModel.CarComfortUnits)
            {
                if (value.IsChecked)
                {
                    value.TurnOnAndOff.SendBoolData(true, true);
                }
            }
        }
        
        /// <summary>
        /// 设置调节温度可选状态
        /// </summary>
        public void SetTemperatureButtonEnable()
        {
            if (ViewModel.Car0IsCheck || ViewModel.Car1IsCheck)
            {
                ViewModel.AddTemperatureIsEnable = true;
                ViewModel.SubTemperatureIsEnable = true;
            }
        }

        /// <summary>
        /// 增加温度命令
        /// </summary>
        public ICommand AddTemperatureCommand { get; private set; }

        /// <summary>
        /// 减小温度命令
        /// </summary>
        public ICommand SubTemperatureCommand { get; private set; }

        /// <summary>
        /// 增加温度命令处理函数
        /// </summary>
        public void OnAddTemperature()
        {
            if (ViewModel.Car0IsCheck)
            {
                DomainViewModel.Instacnce.Model.CarComfortModel.SetTemperature0 += 1;
                OutFolatKey.车0设置温度.SendFloatData(DomainViewModel.Instacnce.Model.CarComfortModel.SetTemperature0);
            }
            else if (ViewModel.Car1IsCheck)
            {
                DomainViewModel.Instacnce.Model.CarComfortModel.SetTemperature1 += 1;
                OutFolatKey.车1设置温度.SendFloatData(DomainViewModel.Instacnce.Model.CarComfortModel.SetTemperature1);
            }
        }

        /// <summary>
        /// 减小温度命令处理函数
        /// </summary>
        public void OnSubTemperature()
        {
            if (ViewModel.Car0IsCheck)
            {
                DomainViewModel.Instacnce.Model.CarComfortModel.SetTemperature0 -= 1;
                OutFolatKey.车0设置温度.SendFloatData(DomainViewModel.Instacnce.Model.CarComfortModel.SetTemperature0);
            }
            else if (ViewModel.Car1IsCheck)
            {
                DomainViewModel.Instacnce.Model.CarComfortModel.SetTemperature1 -= 1;
                OutFolatKey.车1设置温度.SendFloatData(DomainViewModel.Instacnce.Model.CarComfortModel.SetTemperature1);
            }
        }

        /// <summary>
        /// 增加气流命令
        /// </summary>
        public ICommand AddAirCommand { get; private set; }

        /// <summary>
        /// 减小气流命令
        /// </summary>
        public ICommand SubAirCommand { get; private set; }

        /// <summary>
        /// 增加气流命令处理函数
        /// </summary>
        public void OnAddAir()
        {
            ViewModel.Air = Math.Min(ViewModel.Air + 1, 5);
            OutFolatKey.司机室气流.SendFloatData(ViewModel.Air);
            SetAirButtonEnable();
        }

        /// <summary>
        /// 减小气流命令处理函数
        /// </summary>
        public void OnSubAir()
        {
            ViewModel.Air = Math.Max(ViewModel.Air - 1, 0);
            OutFolatKey.司机室气流.SendFloatData(ViewModel.Air);
            SetAirButtonEnable();
        }

        /// <summary>
        /// 设置司机室气流增大/减小按钮可选状态
        /// </summary>
        public void SetAirButtonEnable()
        {
            if (0 == ViewModel.Air)
            {
                ViewModel.SubAirIsEnable = false;
                ViewModel.AddAirIsEnable = true;
            }
            if (5 == ViewModel.Air)
            {
                ViewModel.SubAirIsEnable = true;
                ViewModel.AddAirIsEnable = false;
            }
        }

        /// <summary>
        /// 点击预备条件开/关按钮发送数据
        /// </summary>
        public void PreparatoryConditionsOpenAndCloseSendData()
        {
            OutBoolKey.司机室舒适度预备条件开关.SendBoolData(true, true);
        }

        /// <summary>
        /// 点击脚踏加热按钮发送数据
        /// </summary>
        public void FootWarmingSendData()
        {
            OutBoolKey.司机室舒适度脚踏加热.SendBoolData(true, true);
        }
    }
}
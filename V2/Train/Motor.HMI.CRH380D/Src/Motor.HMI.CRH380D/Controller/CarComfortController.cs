using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Extension;
using Motor.HMI.CRH380D.Models.Model;
using Motor.HMI.CRH380D.Models.State;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller
{
    [Export]
    public class CarComfortController : ControllerBase<CarComfortModel>
    {
        [ImportingConstructor]
        public CarComfortController()
        {
            CheckCommand = new DelegateCommand<string>(OnCheckCommand);
            SetLightModeCommand = new DelegateCommand<string>(OnSetLightMode);
            AddTemperatureCommand = new DelegateCommand(OnAddTemperature);
            SubTemperatureCommand = new DelegateCommand(OnSubTemperature);
        }

        public override void Initalize()
        {
            //选中状态
            ViewModel.TrainModel.CarModel0.IsCheck = false;
            ViewModel.TrainModel.CarModel7.IsCheck = false;
            ViewModel.TrainModel.CarModel6.IsCheck = false;
            ViewModel.TrainModel.CarModel5.IsCheck = false;
            ViewModel.TrainModel.CarModel4.IsCheck = false;
            ViewModel.TrainModel.CarModel3.IsCheck = false;
            ViewModel.TrainModel.CarModel2.IsCheck = false;
            ViewModel.TrainModel.CarModel1.IsCheck = false;

            //按钮使能
            ViewModel.TrainModel.CarModel0.IsEnable = true;
            ViewModel.TrainModel.CarModel7.IsEnable = true;
            ViewModel.TrainModel.CarModel6.IsEnable = true;
            ViewModel.TrainModel.CarModel5.IsEnable = true;
            ViewModel.TrainModel.CarModel4.IsEnable = true;
            ViewModel.TrainModel.CarModel3.IsEnable = true;
            ViewModel.TrainModel.CarModel2.IsEnable = true;
            ViewModel.TrainModel.CarModel1.IsEnable = true;

            //Borderground
            ViewModel.TrainModel.CarModel0.State = CarState.None;
            ViewModel.TrainModel.CarModel7.State = CarState.None;
            ViewModel.TrainModel.CarModel6.State = CarState.None;
            ViewModel.TrainModel.CarModel5.State = CarState.None;
            ViewModel.TrainModel.CarModel4.State = CarState.None;
            ViewModel.TrainModel.CarModel3.State = CarState.None;
            ViewModel.TrainModel.CarModel2.State = CarState.None;
            ViewModel.TrainModel.CarModel1.State = CarState.None;
            
            ViewModel.TrainModel.TrainCheckedCommand = new DelegateCommand<CarModel>(OnTrainChecked);
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
            ViewModel.TrainModel.CarModel0.IsCheck = false;
            ViewModel.TrainModel.CarModel7.IsCheck = false;
            ViewModel.TrainModel.CarModel6.IsCheck = false;
            ViewModel.TrainModel.CarModel5.IsCheck = false;
            ViewModel.TrainModel.CarModel4.IsCheck = false;
            ViewModel.TrainModel.CarModel3.IsCheck = false;
            ViewModel.TrainModel.CarModel2.IsCheck = false;
            ViewModel.TrainModel.CarModel1.IsCheck = false;

            foreach (var value in ViewModel.CarComfortUnits)
            {
                value.IsChecked = false;
            }
        }

        /// <summary>
        /// 车辆点击处理函数
        /// </summary>
        public void OnTrainChecked(CarModel carModel)
        {
            //车辆
            foreach (var value in ViewModel.TrainModel.AllCarModels)
            {
                if (value.IsCheck && value != carModel)
                {
                    value.IsCheck = false;
                }
            }

            SetCutInAndCutOffButtonEnable();
            SetTemperatureButtonEnable();
        }
        
        /// <summary>
        /// 设置车辆切除/切除可选状态
        /// </summary>
        public void SetCutInAndCutOffButtonEnable()
        {
            bool IsEnable = false;

            //车辆
            foreach (var value in ViewModel.TrainModel.AllCarModels)
            {
                if (value.IsCheck)
                {
                    IsEnable = true;
                    break;
                }
            }

            DomainViewModel.Instacnce.Model.StateInterface.BtnB2.IsEnable = IsEnable;
        }

        /// <summary>
        /// 点击切入/切除按钮发送数据
        /// </summary>
        public void CutInAndCutOffSendData()
        {
            if (ViewModel.TrainModel.CarModel0.IsCheck)
            {
                OutBoolKey.客室舒适度车0车辆切入切除.SendBoolData(true, true);
                return;
            }

            if (ViewModel.TrainModel.CarModel7.IsCheck)
            {
                OutBoolKey.客室舒适度车7车辆切入切除.SendBoolData(true, true);
                return;
            }

            if (ViewModel.TrainModel.CarModel6.IsCheck)
            {
                OutBoolKey.客室舒适度车6车辆切入切除.SendBoolData(true, true);
                return;
            }

            if (ViewModel.TrainModel.CarModel5.IsCheck)
            {
                OutBoolKey.客室舒适度车5车辆切入切除.SendBoolData(true, true);
                return;
            }

            if (ViewModel.TrainModel.CarModel4.IsCheck)
            {
                OutBoolKey.客室舒适度车4车辆切入切除.SendBoolData(true, true);
                return;
            }

            if (ViewModel.TrainModel.CarModel3.IsCheck)
            {
                OutBoolKey.客室舒适度车3车辆切入切除.SendBoolData(true, true);
                return;
            }

            if (ViewModel.TrainModel.CarModel2.IsCheck)
            {
                OutBoolKey.客室舒适度车2车辆切入切除.SendBoolData(true, true);
                return;
            }

            if (ViewModel.TrainModel.CarModel1.IsCheck)
            {
                OutBoolKey.客室舒适度车1车辆切入切除.SendBoolData(true, true);
                return;
            }
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
        /// 点击预备条件开/关按钮发送数据
        /// </summary>
        public void PreparatoryConditionsOpenAndCloseSendData()
        {
            OutBoolKey.客室舒适度预备条件开关.SendBoolData(true, true);
        }

        /// <summary>
        /// 照明模式设定
        /// </summary>
        public ICommand SetLightModeCommand { get;private set; }

        private void OnSetLightMode(string tempName)
        {
            switch (tempName)
            {
                case "Normal":
                OutBoolKey.客室舒适度照明普通模式.SendBoolData(true, true);
                break;
                case "Half":
                OutBoolKey.客室舒适度照明半模式.SendBoolData(true, true);
                break;
                case "Closed":
                OutBoolKey.客室舒适度照明关闭模式.SendBoolData(true, true);
                break;
            }
        }

        /// <summary>
        /// 设置调节温度可选状态
        /// </summary>
        public void SetTemperatureButtonEnable()
        {
            bool IsEnable = false;

            //车辆
            foreach (var value in ViewModel.TrainModel.AllCarModels)
            {
                if (value.IsCheck)
                {
                    IsEnable = true;
                    break;
                }
            }

            ViewModel.AddTemperatureIsEnable = IsEnable;
            ViewModel.SubTemperatureIsEnable = IsEnable;
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
            foreach (var value in ViewModel.TrainModel.AllCarModels)
            {
                if (value.IsCheck)
                {
                    switch (value.CarNum)
                    {
                        case 0:
                            ViewModel.SetTemperature0 += 1;
                            OutFolatKey.车0设置温度.SendFloatData(ViewModel.SetTemperature0);
                            break;
                        case 1:
                            ViewModel.SetTemperature1 += 1;
                            OutFolatKey.车1设置温度.SendFloatData(ViewModel.SetTemperature1);
                            break;
                        case 2:
                            ViewModel.SetTemperature2 += 1;
                            OutFolatKey.车2设置温度.SendFloatData(ViewModel.SetTemperature2);
                            break;
                        case 3:
                            ViewModel.SetTemperature3 += 1;
                            OutFolatKey.车3设置温度.SendFloatData(ViewModel.SetTemperature3);
                            break;
                        case 4:
                            ViewModel.SetTemperature4 += 1;
                            OutFolatKey.车4设置温度.SendFloatData(ViewModel.SetTemperature4);
                            break;
                        case 5:
                            ViewModel.SetTemperature5 += 1;
                            OutFolatKey.车5设置温度.SendFloatData(ViewModel.SetTemperature5);
                            break;
                        case 6:
                            ViewModel.SetTemperature6 += 1;
                            OutFolatKey.车6设置温度.SendFloatData(ViewModel.SetTemperature6);
                            break;
                        case 7:
                            ViewModel.SetTemperature7 += 1;
                            OutFolatKey.车7设置温度.SendFloatData(ViewModel.SetTemperature7);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 减小温度命令处理函数
        /// </summary>
        public void OnSubTemperature()
        {
            foreach (var value in ViewModel.TrainModel.AllCarModels)
            {
                if (value.IsCheck)
                {
                    switch (value.CarNum)
                    {
                        case 0:
                            ViewModel.SetTemperature0 -= 1;
                            OutFolatKey.车0设置温度.SendFloatData(ViewModel.SetTemperature0);
                            break;
                        case 1:
                            ViewModel.SetTemperature1 -= 1;
                            OutFolatKey.车1设置温度.SendFloatData(ViewModel.SetTemperature1);
                            break;
                        case 2:
                            ViewModel.SetTemperature2 -= 1;
                            OutFolatKey.车2设置温度.SendFloatData(ViewModel.SetTemperature2);
                            break;
                        case 3:
                            ViewModel.SetTemperature3 -= 1;
                            OutFolatKey.车3设置温度.SendFloatData(ViewModel.SetTemperature3);
                            break;
                        case 4:
                            ViewModel.SetTemperature4 -= 1;
                            OutFolatKey.车4设置温度.SendFloatData(ViewModel.SetTemperature4);
                            break;
                        case 5:
                            ViewModel.SetTemperature5 -= 1;
                            OutFolatKey.车5设置温度.SendFloatData(ViewModel.SetTemperature5);
                            break;
                        case 6:
                            ViewModel.SetTemperature6 -= 1;
                            OutFolatKey.车6设置温度.SendFloatData(ViewModel.SetTemperature6);
                            break;
                        case 7:
                            ViewModel.SetTemperature7 -= 1;
                            OutFolatKey.车7设置温度.SendFloatData(ViewModel.SetTemperature7);
                            break;
                    }
                }
            }
        }
    }
}
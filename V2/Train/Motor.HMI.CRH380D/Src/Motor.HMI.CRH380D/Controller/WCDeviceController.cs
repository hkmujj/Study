using System.ComponentModel.Composition;
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
    public class WCDeviceController : ControllerBase<WCDeviceModel>
    {
        [ImportingConstructor]
        public WCDeviceController()
        {

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
            
            SetButtonEnable();
        }
        
        /// <summary>
        /// 设置水箱排水可选状态
        /// </summary>
        public void SetButtonEnable()
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

            DomainViewModel.Instacnce.Model.StateInterface.BtnB1.IsEnable = IsEnable;
        }

        /// <summary>
        /// 点击水箱排水按钮发送数据
        /// </summary>
        public void RemoveOneWater()
        {
            foreach (var value in ViewModel.TrainModel.AllCarModels)
            {
                if (value.IsCheck)
                {
                    switch (value.CarNum)
                    {
                        case 0:
                            OutBoolKey.车0水箱排水.SendBoolData(true, true);
                            break;
                        case 1:
                            OutBoolKey.车1水箱排水.SendBoolData(true, true);
                            break;
                        case 2:
                            OutBoolKey.车2水箱排水.SendBoolData(true, true);
                            break;
                        case 3:
                            OutBoolKey.车3水箱排水.SendBoolData(true, true);
                            break;
                        case 4:
                            OutBoolKey.车4水箱排水.SendBoolData(true, true);
                            break;
                        case 5:
                            OutBoolKey.车5水箱排水.SendBoolData(true, true);
                            break;
                        case 6:
                            OutBoolKey.车6水箱排水.SendBoolData(true, true);
                            break;
                        case 7:
                            OutBoolKey.车7水箱排水.SendBoolData(true, true);
                            break;
                    }
                }
            }
        }
        
        /// <summary>
        /// 点击全部排空按钮发送数据
        /// </summary>
        public void RemoveAllWater()
        {
            OutBoolKey.全部水箱排空.SendBoolData(true, true);
        }
    }
}
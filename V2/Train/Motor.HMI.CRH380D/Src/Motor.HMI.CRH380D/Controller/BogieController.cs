using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Models.Model;
using Motor.HMI.CRH380D.Models.State;

namespace Motor.HMI.CRH380D.Controller
{
    [Export]
    public class BogieController : ControllerBase<BogieModel>
    {
        [ImportingConstructor]
        public BogieController()
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
            //选中状态ViewModel.TrainModel.CarModel0.IsCheck = false;
            ViewModel.TrainModel.CarModel7.IsCheck = false;
            ViewModel.TrainModel.CarModel6.IsCheck = false;
            ViewModel.TrainModel.CarModel5.IsCheck = false;
            ViewModel.TrainModel.CarModel4.IsCheck = false;
            ViewModel.TrainModel.CarModel3.IsCheck = false;
            ViewModel.TrainModel.CarModel2.IsCheck = false;
            ViewModel.TrainModel.CarModel1.IsCheck = false;
        }

        public void Change()
        {
            //处理显示数据
            foreach (var value in ViewModel.TrainModel.AllCarModels)
            {
                if (value.IsCheck)
                {
                    foreach (var Bogievalue in ViewModel.AllBogieModels)
                    {
                        if (Bogievalue.CarNum == value.CarNum)
                        {
                            ViewModel.BogieDisplay.Clone(Bogievalue);
                            break;
                        }
                    }

                    break;
                }
            }
        }


        /// <summary>
        /// 车辆点击处理函数
        /// </summary>
        public void OnTrainChecked(CarModel carModel)
        {
            //设置车辆选中状态
            foreach (var value in ViewModel.TrainModel.AllCarModels)
            {
                if (value.IsCheck && value != carModel)
                {
                    value.IsCheck = false;
                }
            }

            //处理显示数据
            bool IsVisibility = false;
            foreach (var value in ViewModel.TrainModel.AllCarModels)
            {
                if (value.IsCheck)
                {
                    IsVisibility = true;

                    foreach (var Bogievalue in ViewModel.AllBogieModels)
                    {
                        if (Bogievalue.CarNum == value.CarNum)
                        {
                            ViewModel.BogieDisplay.Clone(Bogievalue);
                            break;
                        }
                    }
                    
                    break;
                }
            }
            ViewModel.BogieVisibility = IsVisibility;
        }
    }
}
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Models.Model;
using Motor.HMI.CRH380D.Models.State;

namespace Motor.HMI.CRH380D.Controller
{
    [Export]
    public class StationController : ControllerBase<StationModel>
    {
        [ImportingConstructor]
        public StationController()
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
            ViewModel.TrainModel.CarModel0.IsEnable = false;
            ViewModel.TrainModel.CarModel7.IsEnable = false;
            ViewModel.TrainModel.CarModel6.IsEnable = false;
            ViewModel.TrainModel.CarModel5.IsEnable = false;
            ViewModel.TrainModel.CarModel4.IsEnable = false;
            ViewModel.TrainModel.CarModel3.IsEnable = false;
            ViewModel.TrainModel.CarModel2.IsEnable = false;
            ViewModel.TrainModel.CarModel1.IsEnable = false;

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
        /// 车辆点击处理函数
        /// </summary>
        public void OnTrainChecked(CarModel carModel)
        {
            //无处理
        }
    }
}
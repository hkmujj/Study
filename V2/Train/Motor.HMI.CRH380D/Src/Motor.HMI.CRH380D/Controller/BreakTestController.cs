using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Extension;
using Motor.HMI.CRH380D.Models.Model;
using Motor.HMI.CRH380D.Models.State;
using Motor.HMI.CRH380D.Resources.Keys;

namespace Motor.HMI.CRH380D.Controller
{
    [Export]
    public class BreakTestController : ControllerBase<BreakTestModel>
    {
        [ImportingConstructor]
        public BreakTestController()
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
        /// 更新提示文本
        /// </summary>
        /// <param name="Num"></param>
        public void UpdateBreakTestInfoDisplay(int Num)
        {
            foreach (var value in ViewModel.AllBreakTestInfo)
            {
                if (value.Index == Num)
                {
                    ViewModel.BreakTestInfoDisplay = value.Info;
                }
            }
        }

        /// <summary>
        /// 车辆点击处理函数
        /// </summary>
        public void OnTrainChecked(CarModel carModel)
        {
            //无处理
        }
        
        /// <summary>
        /// 点击制动试验按钮发送数据
        /// </summary>
        public void BreakTest()
        {
            OutBoolKey.制动试验.SendBoolData(true, true);
        }

        /// <summary>
        /// 点击防滑保护试验按钮发送数据
        /// </summary>
        public void AvoidSlipTest()
        {
            OutBoolKey.防滑保护试验.SendBoolData(true, true);
        }

        /// <summary>
        /// 点击撒沙试验按钮发送数据
        /// </summary>
        public void CastSandTest()
        {
            OutBoolKey.撒沙试验.SendBoolData(true, true);
        }
    }
}
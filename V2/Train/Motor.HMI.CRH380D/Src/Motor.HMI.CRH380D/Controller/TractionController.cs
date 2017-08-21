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
    public class TractionController : ControllerBase<TractionModel>
    {
        [ImportingConstructor]
        public TractionController()
        {
            CheckCommand = new DelegateCommand<string>(OnCheckCommand);
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
            ViewModel.TrainModel.CarModel7.IsEnable = false;
            ViewModel.TrainModel.CarModel6.IsEnable = true;
            ViewModel.TrainModel.CarModel5.IsEnable = false;
            ViewModel.TrainModel.CarModel4.IsEnable = false;
            ViewModel.TrainModel.CarModel3.IsEnable = true;
            ViewModel.TrainModel.CarModel2.IsEnable = false;
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
        /// ACM、LCM、MCM点击处理函数
        /// </summary>
        public void OnCheckCommand(string ButtonNameTemp)
        {
            if (ButtonNameTemp == "ACM")
            {
                ViewModel.LCM1.IsChecked = false;
                ViewModel.LCM2.IsChecked = false;
                ViewModel.MCM1.IsChecked = false;
                ViewModel.MCM2.IsChecked = false;
            }
            if (ButtonNameTemp == "LCM1")
            {
                ViewModel.ACM.IsChecked = false;
                ViewModel.LCM2.IsChecked = false;
                ViewModel.MCM1.IsChecked = false;
                ViewModel.MCM2.IsChecked = false;
            }
            if (ButtonNameTemp == "LCM2")
            {
                ViewModel.ACM.IsChecked = false;
                ViewModel.LCM1.IsChecked = false;
                ViewModel.MCM1.IsChecked = false;
                ViewModel.MCM2.IsChecked = false;
            }
            if (ButtonNameTemp == "MCM1")
            {
                ViewModel.ACM.IsChecked = false;
                ViewModel.LCM1.IsChecked = false;
                ViewModel.LCM2.IsChecked = false;
                ViewModel.MCM2.IsChecked = false;
            }
            if (ButtonNameTemp == "MCM2")
            {
                ViewModel.ACM.IsChecked = false;
                ViewModel.LCM1.IsChecked = false;
                ViewModel.LCM2.IsChecked = false;
                ViewModel.MCM1.IsChecked = false;
            }

            //车辆
            foreach (var value in ViewModel.TrainModel.AllCarModels)
            {
                if (value.IsCheck)
                {
                    value.IsCheck = false;
                }
            }
            
            SetButtonEnable();
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

            //ACM
            foreach (var value in ViewModel.ACMUnits)
            {
                if (value.IsChecked)
                {
                    value.IsChecked = false;
                }
            }

            //LCM
            foreach (var value in ViewModel.LCMUnits)
            {
                if (value.IsChecked)
                {
                    value.IsChecked = false;
                }
            }

            //MCM
            foreach (var value in ViewModel.MCMUnits)
            {
                if (value.IsChecked)
                {
                    value.IsChecked = false;
                }
            }

            SetButtonEnable();
        }
        
        /// <summary>
        /// 设置切除/切除可选状态
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

            //ACM
            foreach (var value in ViewModel.ACMUnits)
            {
                if (value.IsChecked)
                {
                    IsEnable = true;
                    break;
                }
            }

            //LCM
            foreach (var value in ViewModel.LCMUnits)
            {
                if (value.IsChecked)
                {
                    IsEnable = true;
                    break;
                }
            }

            //MCM
            foreach (var value in ViewModel.MCMUnits)
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
        /// 点击切入/切除按钮发送数据
        /// </summary>
        public void CutInAndCutOffSendData()
        {
            if (ViewModel.TrainModel.CarModel0.IsCheck)
            {
                OutBoolKey.车0牵引切入切除.SendBoolData(true, true);
                return;
            }

            if (ViewModel.TrainModel.CarModel6.IsCheck)
            {
                OutBoolKey.车6牵引切入切除.SendBoolData(true, true);
                return;
            }

            if (ViewModel.TrainModel.CarModel3.IsCheck)
            {
                OutBoolKey.车3牵引切入切除.SendBoolData(true, true);
                return;
            }

            if (ViewModel.TrainModel.CarModel1.IsCheck)
            {
                OutBoolKey.车1牵引切入切除.SendBoolData(true, true);
                return;
            }

            if (ViewModel.ACM.IsChecked)
            {
                ViewModel.ACM.CutInAndCutOFF.SendBoolData(true, true);
                return;
            }

            foreach (var value in ViewModel.LCMUnits)
            {
                if (value.IsChecked)
                {
                    value.CutInAndCutOFF.SendBoolData(true, true);
                    return;
                }
            }

            foreach (var value in ViewModel.MCMUnits)
            {
                if (value.IsChecked)
                {
                    value.CutInAndCutOFF.SendBoolData(true, true);
                    return;
                }
            }
        }

        /// <summary>
        /// ACM、LCM、MCM选中
        /// </summary>
        public ICommand CheckCommand { get; set; }
        
        /// <summary>
        /// 点击全部风机开按钮发送数据
        /// </summary>
        public void OpenAllAirBlowerSendData()
        {
            OutBoolKey.全部风机开.SendBoolData(true, true);
        }
    }
}
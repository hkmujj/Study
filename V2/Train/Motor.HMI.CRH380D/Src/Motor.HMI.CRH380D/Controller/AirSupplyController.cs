using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Extension;
using Motor.HMI.CRH380D.Models.Model;
using Motor.HMI.CRH380D.Models.State;
using Motor.HMI.CRH380D.Models.Units;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller
{
    [Export]
    public class AirSupplyController : ControllerBase<AirSupplyModel>
    {
        [ImportingConstructor]
        public AirSupplyController()
        {
            PrimaryCompressorCheckCommand = new DelegateCommand<PrimaryCompressorUnit>(OnPrimaryCompressorCheckCommand);
            PressureSensorCheckCommand = new DelegateCommand<PressureSensorUnit>(OnPressureSensorCheckCommand);
        }

        public override void Initalize()
        {
           
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
            foreach (var value in ViewModel.PressureSensorUnits)
            {
                if (value.IsChecked)
                {
                    value.IsChecked = false;
                }
            }
            foreach (var value in ViewModel.PrimaryCompressorUnits)
            {
                if (value.IsChecked)
                {
                    value.IsChecked = false;
                }
            }
        }
        
        /// <summary>
        /// 设置切除/切除可选状态
        /// </summary>
        public void SetButtonEnable()
        {
            bool IsEnable = false;
            foreach (var value in ViewModel.PressureSensorUnits)
            {
                if (value.IsChecked)
                {
                    IsEnable = true;
                    break;
                }
            }
            foreach (var value in ViewModel.PrimaryCompressorUnits)
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
            foreach (var value in ViewModel.PressureSensorUnits)
            {
                if (value.IsChecked)
                {
                    value.CutInAndCutOFF.SendBoolData(true, true);
                    break;
                }
            }
            foreach (var value in ViewModel.PrimaryCompressorUnits)
            {
                if (value.IsChecked)
                {
                    value.CutInAndCutOFF.SendBoolData(true, true);
                    break;
                }
            }
        }


        /// <summary>
        /// 主压缩机点击处理函数
        /// </summary>
        public void OnPrimaryCompressorCheckCommand(PrimaryCompressorUnit tempPrimaryCompressorUnit)
        {
            foreach (var value in ViewModel.PrimaryCompressorUnits)
            {
                if (value.IsChecked && value != tempPrimaryCompressorUnit)
                {
                    value.IsChecked = false;
                }
            }

            foreach (var value in ViewModel.PressureSensorUnits)
            {
                if (value.IsChecked)
                {
                    value.IsChecked = false;
                }
            }

            SetButtonEnable();
        }

        /// <summary>
        /// 压力传感器点击处理函数
        /// </summary>
        public void OnPressureSensorCheckCommand(PressureSensorUnit tempPressureSensorUnit)
        {
            foreach (var value in ViewModel.PressureSensorUnits)
            {
                if (value.IsChecked && value != tempPressureSensorUnit)
                {
                    value.IsChecked = false;
                }
            }

            foreach (var value in ViewModel.PrimaryCompressorUnits)
            {
                if (value.IsChecked)
                {
                    value.IsChecked = false;
                }
            }

            SetButtonEnable();
        }

        /// <summary>
        /// 主压缩机选中
        /// </summary>
        public ICommand PrimaryCompressorCheckCommand { get; set; }

        /// <summary>
        /// 压力传感器选中
        /// </summary>
        public ICommand PressureSensorCheckCommand { get; set; }
    }
}
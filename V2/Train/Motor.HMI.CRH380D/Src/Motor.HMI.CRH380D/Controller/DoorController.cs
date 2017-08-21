using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Extension;
using Motor.HMI.CRH380D.Models.Model;
using Motor.HMI.CRH380D.Models.Units;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller
{
    [Export]
    public class DoorController : ControllerBase<Lazy<DoorModel>>
    {
        [ImportingConstructor]
        public DoorController(Lazy<DoorModel> viewModel)
            : base(viewModel)
        {
            DoorCheckedCommand = new DelegateCommand<DoorUnit>(OnDoorChecked);
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
            foreach (DoorUnit valueDoorUnit in ViewModel.Value.DoorUnits)
            {
                if (valueDoorUnit.IsChecked)
                {
                    valueDoorUnit.IsChecked = false;
                }
            }
        }

        /// <summary>
        /// 车门点击处理函数
        /// </summary>
        /// <param name="DoorCheckTemp"></param>
        private void OnDoorChecked(DoorUnit DoorCheckTemp)
        {
            foreach (DoorUnit valueDoorUnit in ViewModel.Value.DoorUnits)
            {
                if (valueDoorUnit.IsChecked && valueDoorUnit != DoorCheckTemp)
                {
                    valueDoorUnit.IsChecked = false;
                }
            }
            SetButtonEnable();
        }

        /// <summary>
        /// 车门点击命令
        /// </summary>
        public ICommand DoorCheckedCommand { get; private set; }

        /// <summary>
        /// 设置切除/切除可选状态
        /// </summary>
        public void SetButtonEnable()
        {
            bool IsEnable = false;

            foreach (var value in ViewModel.Value.DoorUnits)
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
            foreach (var value in ViewModel.Value.DoorUnits)
            {
                if (value.IsChecked)
                {
                    value.CutInAndCutOFF.SendBoolData(true, true);
                }
            }
        }
    }
}
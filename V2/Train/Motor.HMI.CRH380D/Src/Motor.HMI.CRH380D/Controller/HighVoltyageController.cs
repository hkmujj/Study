using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Extension;
using Motor.HMI.CRH380D.Models.Model;
using Motor.HMI.CRH380D.Models.Units;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller
{
    [Export]
    public class HighVoltyageController : ControllerBase<Lazy<HighVoltyageModel>>
    {
        [ImportingConstructor]
        public HighVoltyageController(Lazy<HighVoltyageModel> viewModel)
            : base(viewModel)
        {
            PantographCheckedCommand = new DelegateCommand<PantographUnit>(OnPantographChecked);
            LCBCheckedCommand = new DelegateCommand<LCBUnit>(OnLCBChecked);
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
            foreach (var value in ViewModel.Value.PantographUnits)
            {
                if (value.IsChecked)
                {
                    value.IsChecked = false;
                }
            }
            foreach (var value in ViewModel.Value.LCBUnits)
            {
                if (value.IsChecked)
                {
                    value.IsChecked = false;
                }
            }
        }

        /// <summary>
        /// 受电弓点击命令处理
        /// </summary>
        private void OnPantographChecked(PantographUnit PantographCheckTemp)
        {
            foreach (var value in ViewModel.Value.PantographUnits)
            {
                if (value.IsChecked && value != PantographCheckTemp)
                {
                    value.IsChecked = false;
                }
            }
            foreach (var value in ViewModel.Value.LCBUnits)
            {
                if (value.IsChecked)
                {
                    value.IsChecked = false;
                }
            }

            SetButtonEnable();
        }

        /// <summary>
        /// LCB点击命令处理
        /// </summary>
        private void OnLCBChecked(LCBUnit LCBCheckTemp)
        {
            foreach (var value in ViewModel.Value.PantographUnits)
            {
                if (value.IsChecked)
                {
                    value.IsChecked = false;
                }
            }
            foreach (var value in ViewModel.Value.LCBUnits)
            {
                if (value.IsChecked && value != LCBCheckTemp)
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

            foreach (var value in ViewModel.Value.PantographUnits)
            {
                if (value.IsChecked)
                {
                    IsEnable = true;
                    break;
                }
            }
            foreach (var value in ViewModel.Value.LCBUnits)
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
        /// 受电弓点击命令
        /// </summary>
        public ICommand PantographCheckedCommand { get; private set; }

        /// <summary>
        /// LCB点击命令
        /// </summary>
        public ICommand LCBCheckedCommand { get; private set; }

        /// <summary>
        /// 点击切入/切除按钮发送数据
        /// </summary>
        public void CutInAndCutOffSendData()
        {
            foreach (var value in ViewModel.Value.PantographUnits)
            {
                if (value.IsChecked)
                {
                    value.CutInAndCutOFF.SendBoolData(true, true);
                }
            }
            foreach (var value in ViewModel.Value.LCBUnits)
            {
                if (value.IsChecked)
                {
                    value.CutInAndCutOFF.SendBoolData(true, true);
                }
            }
        }

        /// <summary>
        /// 点击GFX-3切除按钮发送数据
        /// </summary>
        public void GFXCutOffSendData()
        {
            OutBoolKey.过分相切除.SendBoolData(true,true);
        }
    }
}
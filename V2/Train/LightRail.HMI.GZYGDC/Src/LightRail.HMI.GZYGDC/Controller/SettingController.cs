using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using LightRail.HMI.GZYGDC.Model;
using LightRail.HMI.GZYGDC.ViewModel;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace LightRail.HMI.GZYGDC.Controller
{
    [Export]
    public class SettingController : ControllerBase<Lazy<SettingViewModel>>
    {
       

       
        [ImportingConstructor]
        public SettingController(Lazy<SettingViewModel> viewModel) : base(viewModel)
        {
            BrightnessAddCommand = new DelegateCommand(BrightnessAdd);
            BrightnessReduceCommand = new DelegateCommand(BrightnessReduce);
            BrightnessAutoAdjustCommand = new DelegateCommand(BrightnessAutoAdjust);
        }


        public override void Initalize()
        {
            ViewModel.Value.Model.PropertyChanged += OnPropertyChanged;

            UpdateSendData();
        }

        /// <summary>
        /// 亮度自动调整命令
        /// </summary>
        public ICommand BrightnessAutoAdjustCommand { get; private set; }

        /// <summary>
        /// 亮度增加命令
        /// </summary>
        public ICommand BrightnessAddCommand { get; private set; }

        /// <summary>
        /// 亮度减少命令
        /// </summary>
        public ICommand BrightnessReduceCommand { get; private set; }


        /// <summary>
        /// 亮度自动调整
        /// </summary>
        private void BrightnessAutoAdjust()
        {
            ViewModel.Value.Model.SettingBrightness = 50;
        }


        /// <summary>
        /// 亮度增加
        /// </summary>
        private void BrightnessAdd()
        {
            ViewModel.Value.Model.SettingBrightness = Math.Min(ViewModel.Value.Model.SettingBrightness + 10, 100);
        }

        /// <summary>
        /// 亮度减少
        /// </summary>
        private void BrightnessReduce()
        {
            ViewModel.Value.Model.SettingBrightness = Math.Max(ViewModel.Value.Model.SettingBrightness - 10, 0);
        }

        /// <summary>
        /// 更新发送数据
        /// </summary>
        private void UpdateSendData()
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                var DataService = GlobalParam.Instance.InitParam.CommunicationDataService;

                if (DataService != null && GlobalParam.Instance.IndexDescription != null)
                {
                  

                }
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateSendData();
        }
    }
}
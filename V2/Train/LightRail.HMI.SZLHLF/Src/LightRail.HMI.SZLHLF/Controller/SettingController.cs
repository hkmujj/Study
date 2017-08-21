using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;
using DevExpress.Xpf.Bars;
using LightRail.HMI.SZLHLF.Extension;
using LightRail.HMI.SZLHLF.Model.SettingModel;
using LightRail.HMI.SZLHLF.Resources.Keys;
using MMI.Facility.WPFInfrastructure.Interfaces;
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;

namespace LightRail.HMI.SZLHLF.Controller
{
    [Export]
    public class SettingController : ControllerBase<Lazy<SettingInfoModel>>
    {
        [ImportingConstructor]
        public SettingController(Lazy<SettingInfoModel> viewModel)
            : base(viewModel)
        {
            RoadLineSetCommand = new DelegateCommand(OnRoadLineSet);

            FireCloseCommand = new DelegateCommand(() =>
            {
                OutBoolKey.火警位置_火警关闭.SendBoolData(true, true);
            });

            FireResetCommand = new DelegateCommand(() =>
            {
                OutBoolKey.火警位置_火警复位.SendBoolData(true, true);
            });

            RoadLineEnterCommand = new DelegateCommand(() =>
            {
                OutBoolKey.线路设置_线路确认.SendBoolData(true, true);
            });

            LightUpCommand = new DelegateCommand(() =>
            {
                viewModel.Value.Light = Math.Min(ViewModel.Value.Light + 10, 100);
            });

            LightDownCommand = new DelegateCommand(() =>
            {
                viewModel.Value.Light = Math.Max(viewModel.Value.Light - 10, 0);
            });

            AutoLightCommand = new DelegateCommand(() =>
            {
                viewModel.Value.Light = 50;
            });
        }


        /// <summary>
        /// 记线路选择录上一次状态
        /// </summary>
        private IDictionary<int, bool> ButtonState;

        public override void Initalize()
        {
            //默认选择ATC全自动
            ViewModel.Value.AllStationBroadcasModeltSet[0].IsChecked = true;

            //亮度默认100
            ViewModel.Value.Light = 100;
            
            ButtonState = new Dictionary<int, bool>();
            foreach (var VARIABLE in ViewModel.Value.AllRoadLineSet)
            {
                ButtonState.Add(VARIABLE.ID, VARIABLE.IsChecked);
            }
        }

        public void Clear()
        {
            foreach (var VARIABLE in ViewModel.Value.AllRoadLineSet)
            {
                VARIABLE.IsChecked = false;
            }
        }

        /// <summary>
        /// 线路选择处理函数
        /// </summary>
        private void OnRoadLineSet()
        {
            foreach (var check in ButtonState)
            {
                if (check.Value)
                {
                    foreach (var VARIABLE in ViewModel.Value.AllRoadLineSet)
                    {
                        VARIABLE.IsChecked = false;
                    }
                    break;
                }
            }
            foreach (var VARIABLE in ViewModel.Value.AllRoadLineSet)
            {
                ButtonState[VARIABLE.ID] = VARIABLE.IsChecked;
            }
        }

        /// <summary>
        /// 线路选择
        /// </summary>
        public ICommand RoadLineSetCommand { get; set; }

        /// <summary>
        /// 线路确认
        /// </summary>
        public ICommand RoadLineEnterCommand { get; set; }

        /// <summary>
        /// 火警复位
        /// </summary>
        public ICommand FireResetCommand { get; set; }

        /// <summary>
        /// 火警关闭
        /// </summary>
        public ICommand FireCloseCommand { get; set; }

        /// <summary>
        /// 增加
        /// </summary>
        public ICommand LightUpCommand { get; set; }

        /// <summary>
        /// 减小
        /// </summary>
        public ICommand LightDownCommand { get; set; }

        /// <summary>
        /// 自动
        /// </summary>
        public ICommand AutoLightCommand { get; set; }
    }
}
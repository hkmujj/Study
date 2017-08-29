using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm.POCO;
using LightRail.HMI.GZYGDC.Model;
using LightRail.HMI.GZYGDC.Model.ConfigModel;
using LightRail.HMI.GZYGDC.Model.State;
using LightRail.HMI.GZYGDC.Resources.Keys;
using LightRail.HMI.GZYGDC.ViewModel;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.Interface.Service;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace LightRail.HMI.GZYGDC.Controller
{
    [Export]
    public class AirConditionController : ControllerBase<Lazy<AirConditionViewModel>>
    {



        [ImportingConstructor]
        public AirConditionController(Lazy<AirConditionViewModel> viewModel) : base(viewModel)
        {
            TemperatureAdd2Command = new DelegateCommand(TemperatureAdd2);
            TemperatureAdd1Command = new DelegateCommand(TemperatureAdd1);
            TemperatureReduce1Command = new DelegateCommand(TemperatureReduce1);
            TemperatureReduce2Command = new DelegateCommand(TemperatureReduce2);
        }


        public override void Initalize()
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                var courseService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<ICourseService>();

                if (courseService != null)
                {
                    courseService.CourseStateChanged += OnCourseStateChanged;
                }
            }

            ResetData();

            ViewModel.Value.Model.PropertyChanged += OnPropertyChanged;

            UpdateSendData();
        }

        private void OnCourseStateChanged(object sender, CourseStateChangedArgs courseStateChangedArgs)
        {
            if (courseStateChangedArgs.CourseService != null)
            {
                if (courseStateChangedArgs.CourseService.CurrentCourseState == CourseState.Started ||
                    courseStateChangedArgs.CourseService.CurrentCourseState == CourseState.Stoped)
                {
                    ResetData();

                    UpdateSendData();
                }
            }
        }

        /// <summary>
        /// 集中控制温度+2命令
        /// </summary>
        public ICommand TemperatureAdd2Command { get; private set; }

        /// <summary>
        /// 集中控制温度+1命令
        /// </summary>
        public ICommand TemperatureAdd1Command { get; private set; }

        /// <summary>
        /// 集中控制温度-1命令
        /// </summary>
        public ICommand TemperatureReduce1Command { get; private set; }


        /// <summary>
        /// 集中控制温度-2命令
        /// </summary>
        public ICommand TemperatureReduce2Command { get; private set; }


        /// <summary>
        /// 集中控制温度+2
        /// </summary>
        private void TemperatureAdd2()
        {
            ViewModel.Value.Model.ConcentrateAirConditionInfo.SettingTemperature = Math.Min(ViewModel.Value.Model.ConcentrateAirConditionInfo.SettingTemperature + 2, 35);
        }

        /// <summary>
        /// 集中控制温度+1
        /// </summary>
        private void TemperatureAdd1()
        {
            ViewModel.Value.Model.ConcentrateAirConditionInfo.SettingTemperature = Math.Min(ViewModel.Value.Model.ConcentrateAirConditionInfo.SettingTemperature + 1, 35);
        }


        /// <summary>
        /// 集中控制温度-1
        /// </summary>
        private void TemperatureReduce1()
        {
            ViewModel.Value.Model.ConcentrateAirConditionInfo.SettingTemperature = Math.Max(ViewModel.Value.Model.ConcentrateAirConditionInfo.SettingTemperature - 1, 5);
        }

        /// <summary>
        /// 集中控制温度-2
        /// </summary>
        private void TemperatureReduce2()
        {
            ViewModel.Value.Model.ConcentrateAirConditionInfo.SettingTemperature = Math.Max(ViewModel.Value.Model.ConcentrateAirConditionInfo.SettingTemperature - 2, 5);
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
                    ViewModel.Value.Model.ConcentrateAirConditionInfo.InCarTemperature = ViewModel.Value.Model.ConcentrateAirConditionInfo.SettingTemperature;
                    DataService.WriteService.ChangeFloat(GlobalParam.Instance.IndexDescription.OutFloatDescriptionDictionary[OutFloatKeys.集中控制设定温度],
                        ViewModel.Value.Model.ConcentrateAirConditionInfo.SettingTemperature);

                    DataService.WriteService.ChangeBool(GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OutBoolKeys.集中控制_空调模式_自动模式],
                        ViewModel.Value.Model.ConcentrateAirConditionInfo.ConditionMode == AirConditionMode.Auto);
                    DataService.WriteService.ChangeBool(GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OutBoolKeys.集中控制_空调模式_通风模式],
                        ViewModel.Value.Model.ConcentrateAirConditionInfo.ConditionMode == AirConditionMode.Ventilation);
                    DataService.WriteService.ChangeBool(GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OutBoolKeys.集中控制_空调模式_关闭预冷],
                        ViewModel.Value.Model.ConcentrateAirConditionInfo.ConditionMode == AirConditionMode.ClosePrecold);
                    DataService.WriteService.ChangeBool(GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OutBoolKeys.集中控制_空调模式_测试模式],
                        ViewModel.Value.Model.ConcentrateAirConditionInfo.ConditionMode == AirConditionMode.Test);
                    DataService.WriteService.ChangeBool(GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OutBoolKeys.集中控制_空调模式_火灾模式],
                        ViewModel.Value.Model.ConcentrateAirConditionInfo.ConditionMode == AirConditionMode.Fire);

                    DataService.WriteService.ChangeBool(GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OutBoolKeys.集中控制_司机室风速_强风],
                        ViewModel.Value.Model.CabWindSpeedMode == WindSpeedMode.Strong);
                    DataService.WriteService.ChangeBool(GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OutBoolKeys.集中控制_司机室风速_中风],
                        ViewModel.Value.Model.CabWindSpeedMode == WindSpeedMode.Middle);
                    DataService.WriteService.ChangeBool(GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OutBoolKeys.集中控制_司机室风速_弱风],
                        ViewModel.Value.Model.CabWindSpeedMode == WindSpeedMode.Weak);

                }
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateSendData();
        }


        /// <summary>
        /// 重置输出数据
        /// </summary>
        public void ResetData()
        {
            //默认值
            ViewModel.Value.Model.ConcentrateAirConditionInfo.ConditionMode = AirConditionMode.Auto;
            ViewModel.Value.Model.ConcentrateAirConditionInfo.SettingTemperature = 20.0F;

            ViewModel.Value.Model.CabWindSpeedMode = WindSpeedMode.Strong;
        }
    }
}
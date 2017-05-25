using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Threading;
using Engine.TCMS.Turkmenistan.Event;
using Engine.TCMS.Turkmenistan.Extension;
using Engine.TCMS.Turkmenistan.Model.State;
using Engine.TCMS.Turkmenistan.Resources.Keys;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.Model
{
    [Export]
    public class MainModel : NotificationObject
    {
        public MainModel()
        {
            DispatcherTimer time = new DispatcherTimer();
            time.Start();
            time.Tick += (sender, args) =>
            {
                CurrentTime = DateTime.Now;
                Date = CurrentTime.ToString("yyyy-MM-dd");
                Time = CurrentTime.ToString("hh:mm:ss");
            };
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<DataServiceDataChangedEvent>().Subscribe(DataChanged);
        }

        private void DataChanged(DataServiceDataChangedEvent.Args obj)
        {
            obj.DataChangedArgs.ChangedBools.UpdateIfContains(InBoolKeys.InB牵引工况, b =>
            {
                if (b)
                {
                    WorkModel = WorkModel.Traction;
                }
            });
            obj.DataChangedArgs.ChangedBools.UpdateIfContains(InBoolKeys.InB制动工况, b =>
            {
                if (b)
                {
                    WorkModel = WorkModel.Brake;
                }
            });
            obj.DataChangedArgs.ChangedBools.UpdateIfContains(InBoolKeys.InB惰转工况, b =>
            {
                if (b)
                {
                    WorkModel = WorkModel.Coasting;
                }
            });
            obj.DataChangedArgs.ChangedBools.UpdateIfContains(InBoolKeys.InB自负荷工况, b =>
            {
                if (b)
                {
                    WorkModel = WorkModel.SlefLoading;
                }
            });
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF主电流, f => MasterCurrent = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF主电压, f => MasterVoltage = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF滑油压力, f => OilPressure = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF燃油压力, f => FuelPressure = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF冷却水温, f => CoolingWaterTemperature = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF机油温度, f => OILTEngineOilTemperature = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF机车速度, f => EngineSpeed = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF柴油机转速, f => DieselEngineSpeed = f);
        }


        private DateTime m_CurrentTime;
        private string m_Date;
        private string m_Time;

        public DateTime CurrentTime
        {
            get { return m_CurrentTime; }
            private set
            {
                if (value.Equals(m_CurrentTime))
                    return;
                m_CurrentTime = value;
                RaisePropertyChanged(() => CurrentTime);
            }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public string Date
        {
            get { return m_Date; }
            private set
            {
                if (value == m_Date)
                    return;
                m_Date = value;
                RaisePropertyChanged(() => Date);
            }
        }
        /// <summary>
        /// 时间
        /// </summary>
        public string Time
        {
            get { return m_Time; }
            private set
            {
                if (value == m_Time)
                    return;
                m_Time = value;
                RaisePropertyChanged(() => Time);
            }
        }
        /// <summary>
        /// 主电压
        /// </summary>
        public double MasterVoltage { get; set; }
        /// <summary>
        /// 主电流
        /// </summary>
        public double MasterCurrent { get; set; }
        /// <summary>
        /// 滑油压力
        /// </summary>
        public double OilPressure { get; set; }
        /// <summary>
        /// 燃油压力
        /// </summary>
        public double FuelPressure { get; set; }
        /// <summary>
        /// 冷却水温
        /// </summary>
        public double CoolingWaterTemperature { get; set; }
        /// <summary>
        /// 机油温度
        /// </summary>
        public double OILTEngineOilTemperature { get; set; }
        /// <summary>
        /// 主电压
        /// </summary>
        public double MasterVoltageFlag { get; set; }
        /// <summary>
        /// 主电流
        /// </summary>
        public double MasterCurrentFlag { get; set; }
        /// <summary>
        /// 滑油压力
        /// </summary>
        public double OilPressureFlag { get; set; }
        /// <summary>
        /// 燃油压力
        /// </summary>
        public double FuelPressureFlag { get; set; }
        /// <summary>
        /// 冷却水温
        /// </summary>
        public double CoolingWaterTemperatureFlag { get; set; }
        /// <summary>
        /// 机油温度
        /// </summary>
        public double OILTEngineOilTemperatureFlag { get; set; }
        /// <summary>
        /// 工况
        /// </summary>
        public WorkModel WorkModel { get; set; }
        /// <summary>
        /// 机车速度
        /// </summary>
        public double EngineSpeed { get; set; }
        /// <summary>
        /// 柴油机转速
        /// </summary>
        public double DieselEngineSpeed { get; set; }
        /// <summary>
        /// 本车柴油机转速
        /// </summary>
        public double CurrentDieselEngineSpeed { get; set; }
        /// <summary>
        /// 他车柴油机转速
        /// </summary>
        public double OtherDieselEngineSpeed { get; set; }
        /// <summary>
        /// 本车滑油压力
        /// </summary>
        public double CurrentOilPressure { get; set; }

        /// <summary>
        /// 他车滑油压力
        /// </summary>
        public double OtherOilPressure { get; set; }
        /// <summary>
        /// 本车冷却水温
        /// </summary>
        public double CurrentCoolingWaterTemperature { get; set; }
        /// <summary>
        /// 他车冷却水温
        /// </summary>
        public double OtherCoolingWaterTemperature { get; set; }
        /// <summary>
        /// 本车燃油压力
        /// </summary>
        public double CurrentFuelPressure { get; set; }
        /// <summary>
        /// 他车燃油压力
        /// </summary>
        public double OtherFuelPressure { get; set; }
        /// <summary>
        /// 本车机油温度
        /// </summary>
        public double CurrentOILTEngineOilTemperature { get; set; }
        /// <summary>
        /// 他车机油温度
        /// </summary>
        public double OtherOILTEngineOilTemperature { get; set; }
        /// <summary>
        /// 本车功率环温1
        /// </summary>
        public double CurrentPowerTemperature1 { get; set; }
        /// <summary>
        /// 他车功率环温1
        /// </summary>
        public double OtherPowerTemperature1 { get; set; }
        /// <summary>
        /// 本车功率环温2
        /// </summary>
        public double CurrentPowerTemperature2 { get; set; }
        /// <summary>
        /// 他车功率环温2
        /// </summary>
        public double OtherPowerTemperature2 { get; set; }
        /// <summary>
        /// 车制动风缸压力
        /// </summary>
        public double CurrentBrakePress { get; set; }
        /// <summary>
        /// 他车制动风缸压力
        /// </summary>
        public double OtherBrakePress { get; set; }
    }
}

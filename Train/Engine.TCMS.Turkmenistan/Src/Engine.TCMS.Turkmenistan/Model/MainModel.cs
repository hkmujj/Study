using System;
using System.ComponentModel.Composition;
using System.Windows.Threading;
using Engine.TCMS.Turkmenistan.Model.State;
using Microsoft.Practices.Prism.ViewModel;

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

    }
}

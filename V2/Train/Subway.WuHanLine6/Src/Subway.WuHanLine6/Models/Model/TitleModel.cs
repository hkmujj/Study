using System;
using System.ComponentModel.Composition;
using System.Windows.Threading;
using Subway.WuHanLine6.FaultInfos;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    ///
    /// </summary>
    [Export]
    public class TitleModel : ModelBase
    {
        private string m_EndStation;
        private string m_NextStation;
        private string m_CurrentStation;
        private double m_LimitSpeed;
        private double m_Speed;
        private double m_NetCurrent;
        private double m_NetVoltage;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TitleModel()
        {
            var timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = new TimeSpan(1000);
            timer.Tick += M_Timer_Tick;
            timer.Start();
        }

        private void M_Timer_Tick(object sender, EventArgs e)
        {
            Data = DateTime.Now.ToString("yyyy-MM-dd");
            Time = DateTime.Now.ToString("hh:mm:ss");
        }

        private string m_Data;
        private string m_Time;

        /// <summary>
        ///
        /// </summary>
        [Import]
        public StationManager StationManager { get; private set; }

        /// <summary>
        /// 网压
        /// </summary>
        public double NetVoltage
        {
            get { return m_NetVoltage; }
            set
            {
                if (value.Equals(m_NetVoltage))
                {
                    return;
                }
                m_NetVoltage = value;
                RaisePropertyChanged(() => NetVoltage);
            }
        }

        /// <summary>
        /// 网流
        /// </summary>
        public double NetCurrent
        {
            get { return m_NetCurrent; }
            set
            {
                if (value.Equals(m_NetCurrent))
                {
                    return;
                }
                m_NetCurrent = value;
                RaisePropertyChanged(() => NetCurrent);
            }
        }

        /// <summary>
        /// 速度
        /// </summary>
        public double Speed
        {
            get { return m_Speed; }
            set
            {
                if (value.Equals(m_Speed))
                {
                    return;
                }
                m_Speed = value;
                RaisePropertyChanged(() => Speed);
            }
        }

        /// <summary>
        /// 限速
        /// </summary>
        public double LimitSpeed
        {
            get { return m_LimitSpeed; }
            set
            {
                if (value.Equals(m_LimitSpeed))
                {
                    return;
                }
                m_LimitSpeed = value;
                RaisePropertyChanged(() => LimitSpeed);
            }
        }

        /// <summary>
        /// 当前站
        /// </summary>
        public string CurrentStation
        {
            get { return m_CurrentStation; }
            set
            {
                if (value == m_CurrentStation)
                {
                    return;
                }
                m_CurrentStation = value;
                RaisePropertyChanged(() => CurrentStation);
            }
        }

        /// <summary>
        /// 下一站
        /// </summary>
        public string NextStation
        {
            get { return m_NextStation; }
            set
            {
                if (value == m_NextStation)
                {
                    return;
                }
                m_NextStation = value;
                RaisePropertyChanged(() => NextStation);
            }
        }

        /// <summary>
        /// 终点站
        /// </summary>
        public string EndStation
        {
            get { return m_EndStation; }
            set
            {
                if (value == m_EndStation)
                {
                    return;
                }
                m_EndStation = value;
                RaisePropertyChanged(() => EndStation);
            }
        }

        /// <summary>
        /// 年月日
        /// </summary>
        public string Data
        {
            get { return m_Data; }
            set
            {
                if (value == m_Data)
                {
                    return;
                }
                m_Data = value;
                RaisePropertyChanged(() => Data);
            }
        }

        /// <summary>
        /// 时分秒
        /// </summary>
        public string Time
        {
            get { return m_Time; }
            set
            {
                if (value == m_Time)
                {
                    return;
                }
                m_Time = value;
                RaisePropertyChanged(() => Time);
            }
        }
    }
}
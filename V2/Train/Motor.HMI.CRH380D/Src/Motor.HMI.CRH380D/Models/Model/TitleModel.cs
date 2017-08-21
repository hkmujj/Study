using System;
using System.ComponentModel.Composition;
using System.Windows.Threading;
using LightRail.HMI.SZLHLF.Model;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class TitleModel : ModelBase
    {
        private float m_Speed;
        private float m_Voltage;
        private DateTime m_CurrentTime;
        private string m_Data;
        private string m_Time;

        public TitleModel()
        {
            var timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = new TimeSpan(1000);
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            //CurrentTime = DateTime.Now;
            Data = DateTime.Now.ToString("yyyy-MM-dd");
            Time = DateTime.Now.ToString("hh:mm:ss");
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

        /// <summary>
        /// 速度
        /// </summary>
        public float Speed
        {
            get { return m_Speed; }
            set
            {
                if (value == m_Speed)
                {
                    return;
                }
                m_Speed = value;
                RaisePropertyChanged(()=>Speed);
            }
        }

        /// <summary>
        /// 电压
        /// </summary>
        public float Voltage
        {
            get { return m_Voltage; }
            set
            {
                if (value == m_Voltage)
                {
                    return;
                }
                m_Voltage = value;
                RaisePropertyChanged(() => Voltage);
            }
        }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CurrentTime
        {
            get { return m_CurrentTime; }
            set
            {
                if (value == m_CurrentTime)
                {
                    return;
                }
                m_CurrentTime = value;
                RaisePropertyChanged(() => CurrentTime);
            }
        }
    }
}
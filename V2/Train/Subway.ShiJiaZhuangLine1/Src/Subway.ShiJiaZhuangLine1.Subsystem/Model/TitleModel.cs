using System;
using System.Threading;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    public class TitleModel : NotificationObject, IDisposable
    {
        private readonly Timer m_UpdateTimeTimer;

        public TitleModel()
        {
            m_UpdateTimeTimer = new Timer((state => ScreenDateTime = DateTime.Now), null, 1000, 1000);
        }

        private double m_BatteryValue;
        private double m_NetPressValue;
        private string m_EndStation;
        private string m_NextStatuin;
        private string m_CurrenStation;
        private string m_TitleName;
        private DateTime m_ScreenDateTime;
        private bool m_IsNetPressFlicker;
        private bool m_IsBatteryFlicker;

        public DateTime ScreenDateTime
        {
            set
            {
                if (value.Equals(m_ScreenDateTime))
                {
                    return;
                }
                m_ScreenDateTime = value;
                RaisePropertyChanged(() => ScreenDateTime);
            }
            get { return m_ScreenDateTime; }
        }

        public string CurrenStation
        {
            get { return m_CurrenStation; }
            set
            {
                if (value == m_CurrenStation)
                {
                    return;
                }

                m_CurrenStation = value;
                RaisePropertyChanged(() => CurrenStation);
            }
        }

        public string NextStatuin
        {
            get { return m_NextStatuin; }
            set
            {
                if (value == m_NextStatuin)
                {
                    return;
                }
                m_NextStatuin = value;
                RaisePropertyChanged(() => NextStatuin);
            }
        }

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

        public double NetPressValue
        {
            get { return m_NetPressValue; }
            set
            {
                if (value.Equals(m_NetPressValue))
                {
                    return;
                }
                m_NetPressValue = (int)value;
                RaisePropertyChanged(() => NetPressValue);
            }
        }

        public bool IsNetPressFlicker
        {
            get { return m_IsNetPressFlicker; }
            set
            {
                if (value == m_IsNetPressFlicker)
                {
                    return;
                }
                m_IsNetPressFlicker = value;
                RaisePropertyChanged(() => IsNetPressFlicker);
            }
        }

        public double BatteryValue
        {
            get { return m_BatteryValue; }
            set
            {
                if (value.Equals(m_BatteryValue))
                {
                    return;
                }
                m_BatteryValue = (int)value;
                RaisePropertyChanged(() => BatteryValue);
            }
        }

        public bool IsBatteryFlicker
        {
            get { return m_IsBatteryFlicker; }
            set
            {
                if (value == m_IsBatteryFlicker)
                {
                    return;
                }
                m_IsBatteryFlicker = value;
                RaisePropertyChanged(() => IsBatteryFlicker);
            }
        }

        public string TitleName
        {
            get { return m_TitleName; }
            set
            {
                if (value == m_TitleName)
                {
                    return;
                }
                m_TitleName = value;
                RaisePropertyChanged(() => TitleName);
            }
        }

        public void Dispose()
        {
            m_UpdateTimeTimer.Dispose();
        }
    }
}
using System;
using System.ComponentModel.Composition;
using Urban.Phillippine.View.Interface.Enum;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(ITitleViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class TitleViewModel : ViewModelBase, ITitleViewModel
    {
        private double m_Battery;
        private DateTime m_CurrentDateTime;
        private TractionBrakeLevel m_Level;
        private LevelColor m_LColor;
        private double m_NetCurrent;
        private double m_NetVoltage;
        private double m_Speed;

        public TitleViewModel()
        {
            NetCurrent = 0;
            NetCurrent = 0;
            Battery = 0;
            CurrentDateTime = DateTime.Now;
            Speed = 0;
            m_LColor = LevelColor.White;
        }

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

        public double Battery
        {
            get { return m_Battery; }
            set
            {
                if (value.Equals(m_Battery))
                {
                    return;
                }
                m_Battery = value;
                RaisePropertyChanged(() => Battery);
            }
        }

        public LevelColor LColor
        {
            get { return m_LColor; }
            set
            {
                if (value == m_LColor)
                {
                    return;
                }
                m_LColor = value;
                RaisePropertyChanged(() => LColor);
            }
        }

        public void SetColorValue(TractionBrakeLevel level)
        {
            LevelColor lclr;
            if (level == TractionBrakeLevel.EB)
            {
                lclr = LevelColor.Red;
            }
            else
            {
                lclr = LevelColor.White;
            }
            LColor = lclr;
        }

        public TractionBrakeLevel Level
        {
            get { return m_Level; }
            set
            {
                if (value == m_Level)
                {
                    return;
                }
                m_Level = value;
                SetColorValue(m_Level);
                RaisePropertyChanged(() => Level);
            }
        }

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

        public DateTime CurrentDateTime
        {
            get { return m_CurrentDateTime; }
            set
            {
                if (value.Equals(m_CurrentDateTime))
                {
                    return;
                }
                m_CurrentDateTime = value;
                RaisePropertyChanged(() => CurrentDateTime);
            }
        }
    }
}
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
namespace Motor.HMI.CRH380BG.Model.Domain.Brake
{
    [Export]
    public class BrakeEfficentModel:NotificationObject
    {
        //日期时间
        private DateTime m_Date;
        private DateTime m_Time;

        //车长，轴数
        private float m_AirBrakeTrainWidth;
        private float m_AirBrakeAxleNumber;

        //制动有效率
        private float m_AirBrakeEfficencePercent;
        private float m_AirBrakeEfficenceNumber;
        private float m_AirBrakeTotalNumber;

        //空气制动可用
        private Efficence m_IsAirBrake1Usefull;
        private Efficence m_IsAirBrake2Usefull;
        private Efficence m_IsAirBrake3Usefull;
        private Efficence m_IsAirBrake4Usefull;
        private Efficence m_IsAirBrake5Usefull;
        private Efficence m_IsAirBrake6Usefull;
        private Efficence m_IsAirBrake7Usefull;
        private Efficence m_IsAirBrake8Usefull;

        //空气制动关闭
        private EfficenceClose m_IsAirBrake1Closed;
        private EfficenceClose m_IsAirBrake2Closed;
        private EfficenceClose m_IsAirBrake3Closed;
        private EfficenceClose m_IsAirBrake4Closed;
        private EfficenceClose m_IsAirBrake5Closed;
        private EfficenceClose m_IsAirBrake6Closed;
        private EfficenceClose m_IsAirBrake7Closed;
        private EfficenceClose m_IsAirBrake8Closed;



        public Efficence IsAirBrake1Usefull
        {

            get { return m_IsAirBrake1Usefull; }

            set
            {
                if (value == m_IsAirBrake1Usefull)
                    return;
                m_IsAirBrake1Usefull = value;
                RaisePropertyChanged(() => IsAirBrake1Usefull);
            }
        }

        public Efficence IsAirBrake2Usefull
        {

            get { return m_IsAirBrake2Usefull; }

            set
            {
                if (value == m_IsAirBrake2Usefull)
                    return;
                m_IsAirBrake2Usefull = value;
                RaisePropertyChanged(() => IsAirBrake2Usefull);
            }
        }

        public Efficence IsAirBrake3Usefull
        {

            get { return m_IsAirBrake3Usefull; }

            set
            {
                if (value == m_IsAirBrake3Usefull)
                    return;
                m_IsAirBrake3Usefull = value;
                RaisePropertyChanged(() => IsAirBrake3Usefull);
            }
        }

        public Efficence IsAirBrake4Usefull
        {

            get { return m_IsAirBrake4Usefull; }

            set
            {
                if (value == m_IsAirBrake4Usefull)
                    return;
                m_IsAirBrake4Usefull = value;
                RaisePropertyChanged(() => IsAirBrake4Usefull);
            }
        }

        public Efficence IsAirBrake5Usefull
        {

            get { return m_IsAirBrake5Usefull; }

            set
            {
                if (value == m_IsAirBrake5Usefull)
                    return;
                m_IsAirBrake5Usefull = value;
                RaisePropertyChanged(() => IsAirBrake5Usefull);
            }
        }

        public Efficence IsAirBrake6Usefull
        {

            get { return m_IsAirBrake6Usefull; }

            set
            {
                if (value == m_IsAirBrake6Usefull)
                    return;
                m_IsAirBrake6Usefull = value;
                RaisePropertyChanged(() => IsAirBrake6Usefull);
            }
        }

        public Efficence IsAirBrake7Usefull
        {

            get { return m_IsAirBrake7Usefull; }

            set
            {
                if (value == m_IsAirBrake7Usefull)
                    return;
                m_IsAirBrake7Usefull = value;
                RaisePropertyChanged(() => IsAirBrake7Usefull);
            }
        }

        public Efficence IsAirBrake8Usefull
        {

            get { return m_IsAirBrake8Usefull; }

            set
            {
                if (value == m_IsAirBrake8Usefull)
                    return;
                m_IsAirBrake8Usefull = value;
                RaisePropertyChanged(() => IsAirBrake8Usefull);
            }
        }

        public EfficenceClose IsAirBrake1Closed
        {

            get { return m_IsAirBrake1Closed; }

            set
            {
                if (value == m_IsAirBrake1Closed)
                    return;
                m_IsAirBrake1Closed = value;
                RaisePropertyChanged(() => IsAirBrake1Closed);
            }
        }

        public EfficenceClose IsAirBrake2Closed
        {

            get { return m_IsAirBrake2Closed; }

            set
            {
                if (value == m_IsAirBrake2Closed)
                    return;
                m_IsAirBrake2Closed = value;
                RaisePropertyChanged(() => IsAirBrake2Closed);
            }
        }

        public EfficenceClose IsAirBrake3Closed
        {

            get { return m_IsAirBrake3Closed; }

            set
            {
                if (value == m_IsAirBrake3Closed)
                    return;
                m_IsAirBrake3Closed = value;
                RaisePropertyChanged(() => IsAirBrake3Closed);
            }
        }

        public EfficenceClose IsAirBrake4Closed
        {

            get { return m_IsAirBrake4Closed; }

            set
            {
                if (value == m_IsAirBrake4Closed)
                    return;
                m_IsAirBrake4Closed = value;
                RaisePropertyChanged(() => IsAirBrake4Closed);
            }
        }

        public EfficenceClose IsAirBrake5Closed
        {

            get { return m_IsAirBrake5Closed; }

            set
            {
                if (value == m_IsAirBrake5Closed)
                    return;
                m_IsAirBrake5Closed = value;
                RaisePropertyChanged(() => IsAirBrake5Closed);
            }
        }

        public EfficenceClose IsAirBrake6Closed
        {

            get { return m_IsAirBrake6Closed; }

            set
            {
                if (value == m_IsAirBrake6Closed)
                    return;
                m_IsAirBrake6Closed = value;
                RaisePropertyChanged(() => IsAirBrake6Closed);
            }
        }

        public EfficenceClose IsAirBrake7Closed
        {

            get { return m_IsAirBrake7Closed; }

            set
            {
                if (value == m_IsAirBrake7Closed)
                    return;
                m_IsAirBrake7Closed = value;
                RaisePropertyChanged(() => IsAirBrake7Closed);
            }
        }

        public EfficenceClose IsAirBrake8Closed
        {

            get { return m_IsAirBrake8Closed; }

            set
            {
                if (value == m_IsAirBrake8Closed)
                    return;
                m_IsAirBrake8Closed = value;
                RaisePropertyChanged(() => IsAirBrake8Closed);
            }
        }

        public float AirBrakeEfficencePercent
        {
            [DebuggerStepThrough]
            get { return m_AirBrakeEfficencePercent; }
            set
            {
                if (value.Equals(m_AirBrakeEfficencePercent))
                    return;
                m_AirBrakeEfficencePercent = value;
                RaisePropertyChanged(() => AirBrakeEfficencePercent);
            }
        }

        public float AirBrakeEfficenceNumber
        {           
            get { return m_AirBrakeEfficenceNumber; }
            set
            {
                if (value.Equals(m_AirBrakeEfficenceNumber))
                    return;
                m_AirBrakeEfficenceNumber = value;
                RaisePropertyChanged(() => AirBrakeEfficenceNumber);
            }
        }

        public float AirBrakeTotalNumber
        {        
            get { return m_AirBrakeTotalNumber; }
            set
            {
                if (value.Equals(m_AirBrakeTotalNumber))
                    return;
                m_AirBrakeTotalNumber = value;
                RaisePropertyChanged(() => AirBrakeTotalNumber);
            }
        }

        public float AirBrakeTrainWidth
        {      
            get { return m_AirBrakeTrainWidth; }
            set
            {
                if (value.Equals(m_AirBrakeTrainWidth))
                    return;
                m_AirBrakeTrainWidth = value;
                RaisePropertyChanged(() => AirBrakeTrainWidth);
            }
        }

        public float AirBrakeAxleNumber
        {
            get { return m_AirBrakeAxleNumber; }
            set
            {
                if (value.Equals(m_AirBrakeAxleNumber))
                    return;
                m_AirBrakeAxleNumber = value;
                RaisePropertyChanged(() => AirBrakeAxleNumber);
            }
        }


        public DateTime Date
        {
            get { return m_Date; }
            set
            {
                if (value.Equals(m_Date))
                    return;
                m_Date = value;
                RaisePropertyChanged(() => Date);
            }
        }

        public DateTime Time
        {
            get { return m_Time; }
            set
            {
                if (value.Equals(m_Time))
                    return;
                m_Time = value;
                RaisePropertyChanged(() => Time);
            }
        }


    }
}
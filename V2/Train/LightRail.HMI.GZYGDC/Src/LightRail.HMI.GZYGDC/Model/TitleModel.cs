using System.ComponentModel.Composition;
using CommonUtil.Util;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.Model
{
    [Export]
    public class TitleModel : NotificationObject
    {
        private string m_strNextStation;

        private string m_strCurViewTitle;

        private string m_strEndStation;

        private string m_strTrainNum;

        private float m_ElectricalCurrent;

        private float m_Voltage;

        private float m_Speed;





        /// <summary>
        /// 下一站
        /// </summary>
        public string NextStation
        {
            get
            {
                if (m_strNextStation.IsNullOrWhiteSpace())
                {
                    m_strNextStation = "?";
                }
                
                return m_strNextStation;
            }
            set
            {
                if (value == m_strNextStation)
                {
                    return;
                }

                m_strNextStation = value;

                RaisePropertyChanged(() => NextStation);
            }
        }


        /// <summary>
        /// 当前视图标题
        /// </summary>
        public string CurViewTitle
        {
            get { return m_strCurViewTitle; }
            set
            {
                if (value == m_strCurViewTitle)
                {
                    return;
                }

                m_strCurViewTitle = value;

                RaisePropertyChanged(() => CurViewTitle);
            }
        }


        /// <summary>
        /// 终点站
        /// </summary>
        public string EndStation
        {
            get
            {
                if (m_strEndStation.IsNullOrWhiteSpace())
                {
                    m_strEndStation = "?";
                }

                return m_strEndStation;
            }
            set
            {
                if (value == m_strEndStation)
                {
                    return;
                }

                m_strEndStation = value;

                RaisePropertyChanged(() => EndStation);
            }
        }


        /// <summary>
        /// 车次号、列车号
        /// </summary>
        public string TrainNum
        {
            get { return m_strTrainNum; }
            set
            {
                if (value == m_strTrainNum)
                {
                    return;
                }

                m_strTrainNum = value;

                RaisePropertyChanged(() => TrainNum);
            }
        }


        /// <summary>
        /// 电流
        /// </summary>
        public float ElectricalCurrent
        {
            get { return m_ElectricalCurrent; }
            set
            {
                if (value.Equals(m_ElectricalCurrent))
                {
                    return;
                }

                m_ElectricalCurrent = value;

                RaisePropertyChanged(() => ElectricalCurrent);
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
                if (value.Equals(m_Voltage))
                {
                    return;
                }

                m_Voltage = value;

                RaisePropertyChanged(() => Voltage);
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
                if (value.Equals(m_Speed))
                {
                    return;
                }

                m_Speed = value;

                RaisePropertyChanged(() => Speed);
            }
        }




    }
}
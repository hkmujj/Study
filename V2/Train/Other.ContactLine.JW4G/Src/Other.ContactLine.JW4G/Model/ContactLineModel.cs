using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.Prism.ViewModel;

namespace Other.ContactLine.JW4G.Model
{
    [Export]
    public class ContactLineModel : NotificationObject
    {
        private float m_Speed;
        private WorkStates m_WorkStates;
        private float m_WaterTemputer;
        private float m_FuelPressure;
        private float m_IncreasePressure;
        private float m_FuelUse;
        private float m_Course;
        private float m_TrainSpeed;
        private float m_Voltage;
        private float m_Electric;
        private float m_Tempreture;
        private float m_SpeedOfTransmission;
        private float m_FlowTempreture;
        private float m_FuelTempreture;
        private float m_BatteryPressure;
        private float m_WorkHours;
        private float m_MakeElectric;
        private float m_FuelPosition;
        private string m_InfoUnit;
        private List<FalutInUnit> m_HistoryFault = new List<FalutInUnit>();
        private int m_Index;
        private Visibility m_MMIBlack;

        public ContactLineModel()
        {
            MMIBlack = Visibility.Hidden;
        }
        /// <summary>
        /// 黑屏
        /// </summary>
        public Visibility MMIBlack
        {
            get { return m_MMIBlack; }
            set
            {
                if (value == m_MMIBlack)
                    return;
                m_MMIBlack = value;
                RaisePropertyChanged(() => MMIBlack);
            }
        }

        /// <summary>
        /// 工况
        /// </summary>
        public WorkStates WorkStates
        {
            get { return m_WorkStates; }
            set
            {
                if (value == m_WorkStates)
                {
                    return;
                }
                m_WorkStates = value;
                RaisePropertyChanged(() => WorkStates);
            }
        }

        /// <summary>
        /// 转速
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

        /// <summary>
        /// 冷却水温
        /// </summary>
        public float WaterTemputer
        {
            get { return m_WaterTemputer; }
            set
            {
                if (value.Equals(m_WaterTemputer))
                {
                    return;
                }
                m_WaterTemputer = value;
                RaisePropertyChanged(() => WaterTemputer);
            }
        }

        /// <summary>
        /// 机油压力
        /// </summary>
        public float FuelPressure
        {
            get { return m_FuelPressure; }
            set
            {
                if (value.Equals(m_FuelPressure))
                {
                    return;
                }
                m_FuelPressure = value;
                RaisePropertyChanged(() => FuelPressure);
            }
        }

        /// <summary>
        /// 增加压力
        /// </summary>
        public float IncreasePressure
        {
            get { return m_IncreasePressure; }
            set
            {
                if (value.Equals(m_IncreasePressure))
                {
                    return;
                }
                m_IncreasePressure = value;
                RaisePropertyChanged(() => IncreasePressure);
            }
        }

        /// <summary>
        /// 燃油消耗
        /// </summary>
        public float FuelUse
        {
            get { return m_FuelUse; }
            set
            {
                if (value.Equals(m_FuelUse))
                {
                    return;
                }
                m_FuelUse = value;
                RaisePropertyChanged(() => FuelUse);
            }
        }
        /// <summary>
        /// 里程
        /// </summary>
        public float Course
        {
            get { return m_Course; }
            set
            {
                if (value.Equals(m_Course))
                {
                    return;
                }
                m_Course = value;
                RaisePropertyChanged(() => Course);
            }
        }
        /// <summary>
        /// 车速
        /// </summary>
        public float TrainSpeed
        {
            get { return m_TrainSpeed; }
            set
            {
                if (value.Equals(m_TrainSpeed))
                {
                    return;
                }
                m_TrainSpeed = value;
                RaisePropertyChanged(() => TrainSpeed);
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
        /// 电流
        /// </summary>
        public float Electric
        {
            get { return m_Electric; }
            set
            {
                if (value.Equals(m_Electric))
                {
                    return;
                }
                m_Electric = value;
                RaisePropertyChanged(() => Electric);
            }
        }
        /// <summary>
        /// 燃油油位
        /// </summary>
        public float FuelPosition
        {
            get { return m_FuelPosition; }
            set
            {
                if (value.Equals(m_FuelPosition))
                {
                    return;
                }
                m_FuelPosition = value;
                RaisePropertyChanged(() => FuelPosition);
            }
        }

        /// <summary>
        /// 变速箱温度
        /// </summary>
        public float TempretureOfTransmission
        {
            get { return m_Tempreture; }
            set
            {
                if (value.Equals(m_Tempreture))
                {
                    return;
                }
                m_Tempreture = value;
                RaisePropertyChanged(() => TempretureOfTransmission);
            }
        }
        /// <summary>
        /// 变速箱转速
        /// </summary>
        public float SpeedOfTransmission
        {
            get { return m_SpeedOfTransmission; }
            set
            {
                if (value.Equals(m_SpeedOfTransmission))
                {
                    return;
                }
                m_SpeedOfTransmission = value;
                RaisePropertyChanged(() => SpeedOfTransmission);
            }
        }
        /// <summary>
        /// 进气温度
        /// </summary>
        public float FlowTempreture
        {
            get { return m_FlowTempreture; }
            set
            {
                if (value.Equals(m_FlowTempreture))
                {
                    return;
                }
                m_FlowTempreture = value;
                RaisePropertyChanged(() => FlowTempreture);
            }
        }
        /// <summary>
        /// 机油温度
        /// </summary>
        public float FuelTempreture
        {
            get { return m_FuelTempreture; }
            set
            {
                if (value.Equals(m_FuelTempreture))
                {
                    return;
                }
                m_FuelTempreture = value;
                RaisePropertyChanged(() => FuelTempreture);
            }
        }
        /// <summary>
        /// 电池电压
        /// </summary>
        public float BatteryPressure
        {
            get { return m_BatteryPressure; }
            set
            {
                if (value.Equals(m_BatteryPressure))
                {
                    return;
                }
                m_BatteryPressure = value;
                RaisePropertyChanged(() => BatteryPressure);
            }
        }
        /// <summary>
        /// 工作小时
        /// </summary>
        public float WorkHours
        {
            get { return m_WorkHours; }
            set
            {
                if (value.Equals(m_WorkHours))
                {
                    return;
                }
                m_WorkHours = value;
                RaisePropertyChanged(() => WorkHours);
            }
        }
        /// <summary>
        /// 发电电流
        /// </summary>
        public float MakeElectric
        {
            get { return m_MakeElectric; }
            set
            {
                if (value.Equals(m_MakeElectric))
                {
                    return;
                }
                m_MakeElectric = value;
                RaisePropertyChanged(() => MakeElectric);
            }
        }

        public string InfoUnit
        {
            get { return m_InfoUnit; }
            set
            {
                if (Equals(value, m_InfoUnit))
                {
                    return;
                }
                m_InfoUnit = value;
                RaisePropertyChanged(() => InfoUnit);
            }
        }

        public List<FalutInUnit> HistoryFault
        {
            get { return m_HistoryFault; }
            set
            {
                if (Equals(value, m_HistoryFault))
                {
                    return;
                }
                m_HistoryFault = value;
                Index = 0;
                InfoUnit = m_HistoryFault.Count > 0 ? m_HistoryFault[0].ContentAlgorithmBase : "";
            }
        }

        public int Index
        {
            get { return m_Index; }
            set
            {
                if (value == m_Index)
                {
                    return;
                }
                if (value >= HistoryFault.Count || value < 0)
                    return;
                m_Index = value;
                InfoUnit = m_HistoryFault[m_Index].ContentAlgorithmBase;
            }
        }
    }

    public enum WorkStates
    {
        [Description("空 挡")]
        Empty,
        [Description("前 进")]
        Up,
        [Description("后 退")]
        Down
    }
}
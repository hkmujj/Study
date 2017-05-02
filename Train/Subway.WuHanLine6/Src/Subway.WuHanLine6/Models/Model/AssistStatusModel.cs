using System.ComponentModel.Composition;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 辅助系统状态Model
    /// </summary>
    [Export]
    public class AssistStatusModel : ModelBase
    {
        private double m_AccumulatorF006;
        private double m_AccumulatorF001;
        private double m_OutpoutChargeCurrentF006;
        private double m_OutpoutChargeCurrentF001;
        private double m_OutPutLoadCurrentF006;
        private double m_OutPutLoadCurrentF001;
        private double m_DynamoOutputVoltageF006;
        private double m_DynamoOutputVoltageF001;
        private double m_DynamoGeneratrixVoltageF006;
        private double m_DynamoGeneratrixVoltageF001;
        private double m_DynamoInputVoltageF006;
        private double m_DynamoInputVoltageF001;
        private bool m_DynamoStatusF006;
        private bool m_DynamoStatusF001;
        private double m_AssistInputCurrentF006;
        private double m_AssistInputCurrentF001;
        private double m_AssistOutputCurrentF006;
        private double m_AssistOutputCurrentF001;
        private double m_AssistOutputRateF006;
        private double m_AssistOutputRateF001;
        private double m_AssistOutputVoltageF006;
        private double m_AssistOutputVoltageF001;
        private double m_AssistGeneratrixVoltageF006;
        private double m_AssistGeneratrixVoltageF001;
        private double m_AssistInputVoltageF006;
        private double m_AssistInputVoltageF001;
        private double m_AssistEnergyF006;
        private double m_AssistEnergyF001;
        private bool m_AssistStatusF006;
        private bool m_AssistStatusF001;

        /// <summary>
        /// 辅助系统状态
        /// </summary>
        public bool AssistStatusF001
        {
            get { return m_AssistStatusF001; }
            set
            {
                if (value == m_AssistStatusF001)
                {
                    return;
                }
                m_AssistStatusF001 = value;
                RaisePropertyChanged(() => AssistStatusF001);
            }
        }

        /// <summary>
        /// 辅助系统状态
        /// </summary>
        public bool AssistStatusF006
        {
            get { return m_AssistStatusF006; }
            set
            {
                if (value == m_AssistStatusF006)
                {
                    return;
                }
                m_AssistStatusF006 = value;
                RaisePropertyChanged(() => AssistStatusF006);
            }
        }

        /// <summary>
        /// 辅助能耗
        /// </summary>
        public double AssistEnergyF001
        {
            get { return m_AssistEnergyF001; }
            set
            {
                if (value.Equals(m_AssistEnergyF001))
                {
                    return;
                }
                m_AssistEnergyF001 = value;
                RaisePropertyChanged(() => AssistEnergyF001);
            }
        }

        /// <summary>
        /// 辅助能耗
        /// </summary>
        public double AssistEnergyF006
        {
            get { return m_AssistEnergyF006; }
            set
            {
                if (value.Equals(m_AssistEnergyF006))
                {
                    return;
                }
                m_AssistEnergyF006 = value;
                RaisePropertyChanged(() => AssistEnergyF006);
            }
        }

        /// <summary>
        /// 输入电压
        /// </summary>
        public double AssistInputVoltageF001
        {
            get { return m_AssistInputVoltageF001; }
            set
            {
                if (value.Equals(m_AssistInputVoltageF001))
                {
                    return;
                }
                m_AssistInputVoltageF001 = value;
                RaisePropertyChanged(() => AssistInputVoltageF001);
            }
        }

        /// <summary>
        /// 输入电压
        /// </summary>
        public double AssistInputVoltageF006
        {
            get { return m_AssistInputVoltageF006; }
            set
            {
                if (value.Equals(m_AssistInputVoltageF006))
                {
                    return;
                }
                m_AssistInputVoltageF006 = value;
                RaisePropertyChanged(() => AssistInputVoltageF006);
            }
        }

        /// <summary>
        /// 母线电压
        /// </summary>
        public double AssistGeneratrixVoltageF001
        {
            get { return m_AssistGeneratrixVoltageF001; }
            set
            {
                if (value.Equals(m_AssistGeneratrixVoltageF001))
                {
                    return;
                }
                m_AssistGeneratrixVoltageF001 = value;
                RaisePropertyChanged(() => AssistGeneratrixVoltageF001);
            }
        }

        /// <summary>
        /// 母线电压
        /// </summary>
        public double AssistGeneratrixVoltageF006
        {
            get { return m_AssistGeneratrixVoltageF006; }
            set
            {
                if (value.Equals(m_AssistGeneratrixVoltageF006))
                {
                    return;
                }
                m_AssistGeneratrixVoltageF006 = value;
                RaisePropertyChanged(() => AssistGeneratrixVoltageF006);
            }
        }

        /// <summary>
        /// 输出电压
        /// </summary>
        public double AssistOutputVoltageF001
        {
            get { return m_AssistOutputVoltageF001; }
            set
            {
                if (value.Equals(m_AssistOutputVoltageF001))
                {
                    return;
                }
                m_AssistOutputVoltageF001 = value;
                RaisePropertyChanged(() => AssistOutputVoltageF001);
            }
        }

        /// <summary>
        /// 输出电压
        /// </summary>
        public double AssistOutputVoltageF006
        {
            get { return m_AssistOutputVoltageF006; }
            set
            {
                if (value.Equals(m_AssistOutputVoltageF006))
                {
                    return;
                }
                m_AssistOutputVoltageF006 = value;
                RaisePropertyChanged(() => AssistOutputVoltageF006);
            }
        }

        /// <summary>
        /// 输出频率
        /// </summary>
        public double AssistOutputRateF001
        {
            get { return m_AssistOutputRateF001; }
            set
            {
                if (value.Equals(m_AssistOutputRateF001))
                {
                    return;
                }
                m_AssistOutputRateF001 = value;
                RaisePropertyChanged(() => AssistOutputRateF001);
            }
        }

        /// <summary>
        /// 输出频率
        /// </summary>
        public double AssistOutputRateF006
        {
            get { return m_AssistOutputRateF006; }
            set
            {
                if (value.Equals(m_AssistOutputRateF006))
                {
                    return;
                }
                m_AssistOutputRateF006 = value;
                RaisePropertyChanged(() => AssistOutputRateF006);
            }
        }

        /// <summary>
        /// 输出电流
        /// </summary>
        public double AssistOutputCurrentF001
        {
            get { return m_AssistOutputCurrentF001; }
            set
            {
                if (value.Equals(m_AssistOutputCurrentF001))
                {
                    return;
                }
                m_AssistOutputCurrentF001 = value;
                RaisePropertyChanged(() => AssistOutputCurrentF001);
            }
        }

        /// <summary>
        /// 输出电流
        /// </summary>
        public double AssistOutputCurrentF006
        {
            get { return m_AssistOutputCurrentF006; }
            set
            {
                if (value.Equals(m_AssistOutputCurrentF006))
                {
                    return;
                }
                m_AssistOutputCurrentF006 = value;
                RaisePropertyChanged(() => AssistOutputCurrentF006);
            }
        }

        /// <summary>
        /// 输入电流
        /// </summary>
        public double AssistInputCurrentF001
        {
            get { return m_AssistInputCurrentF001; }
            set
            {
                if (value.Equals(m_AssistInputCurrentF001))
                {
                    return;
                }
                m_AssistInputCurrentF001 = value;
                RaisePropertyChanged(() => AssistInputCurrentF001);
            }
        }

        /// <summary>
        /// 输入电流
        /// </summary>
        public double AssistInputCurrentF006
        {
            get { return m_AssistInputCurrentF006; }
            set
            {
                if (value.Equals(m_AssistInputCurrentF006))
                {
                    return;
                }
                m_AssistInputCurrentF006 = value;
                RaisePropertyChanged(() => AssistInputCurrentF006);
            }
        }

        /// <summary>
        /// 充电机状态
        /// </summary>
        public bool DynamoStatusF001
        {
            get { return m_DynamoStatusF001; }
            set
            {
                if (value == m_DynamoStatusF001)
                {
                    return;
                }
                m_DynamoStatusF001 = value;
                RaisePropertyChanged(() => DynamoStatusF001);
            }
        }

        /// <summary>
        /// 充电机状态
        /// </summary>
        public bool DynamoStatusF006
        {
            get { return m_DynamoStatusF006; }
            set
            {
                if (value == m_DynamoStatusF006)
                {
                    return;
                }
                m_DynamoStatusF006 = value;
                RaisePropertyChanged(() => DynamoStatusF006);
            }
        }

        /// <summary>
        /// 输入电压
        /// </summary>
        public double DynamoInputVoltageF001
        {
            get { return m_DynamoInputVoltageF001; }
            set
            {
                if (value.Equals(m_DynamoInputVoltageF001))
                {
                    return;
                }
                m_DynamoInputVoltageF001 = value;
                RaisePropertyChanged(() => DynamoInputVoltageF001);
            }
        }

        /// <summary>
        /// 输入电压
        /// </summary>
        public double DynamoInputVoltageF006
        {
            get { return m_DynamoInputVoltageF006; }
            set
            {
                if (value.Equals(m_DynamoInputVoltageF006))
                {
                    return;
                }
                m_DynamoInputVoltageF006 = value;
                RaisePropertyChanged(() => DynamoInputVoltageF006);
            }
        }

        /// <summary>
        /// 母线电压
        /// </summary>
        public double DynamoGeneratrixVoltageF001
        {
            get { return m_DynamoGeneratrixVoltageF001; }
            set
            {
                if (value.Equals(m_DynamoGeneratrixVoltageF001))
                {
                    return;
                }
                m_DynamoGeneratrixVoltageF001 = value;
                RaisePropertyChanged(() => DynamoGeneratrixVoltageF001);
            }
        }

        /// <summary>
        /// 母线电压
        /// </summary>
        public double DynamoGeneratrixVoltageF006
        {
            get { return m_DynamoGeneratrixVoltageF006; }
            set
            {
                if (value.Equals(m_DynamoGeneratrixVoltageF006))
                {
                    return;
                }
                m_DynamoGeneratrixVoltageF006 = value;
                RaisePropertyChanged(() => DynamoGeneratrixVoltageF006);
            }
        }

        /// <summary>
        /// 输出电压
        /// </summary>
        public double DynamoOutputVoltageF001
        {
            get { return m_DynamoOutputVoltageF001; }
            set
            {
                if (value.Equals(m_DynamoOutputVoltageF001))
                {
                    return;
                }
                m_DynamoOutputVoltageF001 = value;
                RaisePropertyChanged(() => DynamoOutputVoltageF001);
            }
        }

        /// <summary>
        /// 输出电压
        /// </summary>
        public double DynamoOutputVoltageF006
        {
            get { return m_DynamoOutputVoltageF006; }
            set
            {
                if (value.Equals(m_DynamoOutputVoltageF006))
                {
                    return;
                }
                m_DynamoOutputVoltageF006 = value;
                RaisePropertyChanged(() => DynamoOutputVoltageF006);
            }
        }

        /// <summary>
        /// 输出负载电流
        /// </summary>
        public double OutPutLoadCurrentF001
        {
            get { return m_OutPutLoadCurrentF001; }
            set
            {
                if (value.Equals(m_OutPutLoadCurrentF001))
                {
                    return;
                }
                m_OutPutLoadCurrentF001 = value;
                RaisePropertyChanged(() => OutPutLoadCurrentF001);
            }
        }

        /// <summary>
        /// 输出负载电流
        /// </summary>
        public double OutPutLoadCurrentF006
        {
            get { return m_OutPutLoadCurrentF006; }
            set
            {
                if (value.Equals(m_OutPutLoadCurrentF006))
                {
                    return;
                }
                m_OutPutLoadCurrentF006 = value;
                RaisePropertyChanged(() => OutPutLoadCurrentF006);
            }
        }

        /// <summary>
        /// 输出充电电流
        /// </summary>
        public double OutpoutChargeCurrentF001
        {
            get { return m_OutpoutChargeCurrentF001; }
            set
            {
                if (value.Equals(m_OutpoutChargeCurrentF001))
                {
                    return;
                }
                m_OutpoutChargeCurrentF001 = value;
                RaisePropertyChanged(() => OutpoutChargeCurrentF001);
            }
        }

        /// <summary>
        /// 输出充电电流
        /// </summary>
        public double OutpoutChargeCurrentF006
        {
            get { return m_OutpoutChargeCurrentF006; }
            set
            {
                if (value.Equals(m_OutpoutChargeCurrentF006))
                {
                    return;
                }
                m_OutpoutChargeCurrentF006 = value;
                RaisePropertyChanged(() => OutpoutChargeCurrentF006);
            }
        }

        /// <summary>
        /// 蓄电池温度
        /// </summary>
        public double AccumulatorF001
        {
            get { return m_AccumulatorF001; }
            set
            {
                if (value.Equals(m_AccumulatorF001))
                {
                    return;
                }
                m_AccumulatorF001 = value;
                RaisePropertyChanged(() => AccumulatorF001);
            }
        }

        /// <summary>
        /// 蓄电池温度
        /// </summary>
        public double AccumulatorF006
        {
            get { return m_AccumulatorF006; }
            set
            {
                if (value.Equals(m_AccumulatorF006))
                {
                    return;
                }
                m_AccumulatorF006 = value;
                RaisePropertyChanged(() => AccumulatorF006);
            }
        }
    }
}
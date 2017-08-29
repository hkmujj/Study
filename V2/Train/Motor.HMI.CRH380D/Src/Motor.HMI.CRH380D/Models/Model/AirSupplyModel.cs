using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class AirSupplyModel : ModelBase
    {
        private float m_PressureSensorValue0;
        private float m_PressureSensorValue5;
        private float m_PressureSensorValue4;
        private float m_PressureSensorValue1;

        private ObservableCollection<PrimaryCompressorUnit> m_PrimaryCompressorUnits;
        private ObservableCollection<SubCompressorUnit> m_SubCompressorUnits;
        private ObservableCollection<PressureSensorUnit> m_PressureSensorUnits;

        [ImportingConstructor]
        public AirSupplyModel(AirSupplyController airSupplyController)
        {
            PrimaryCompressorUnits = new ObservableCollection<PrimaryCompressorUnit>(GlobalParam.Instance.PrimaryCompressorUnits.OrderBy(o => o.Num));
            SubCompressorUnits = new ObservableCollection<SubCompressorUnit>(GlobalParam.Instance.SubCompressorUnits.OrderBy(o => o.Num));
            PressureSensorUnits = new ObservableCollection<PressureSensorUnit>(GlobalParam.Instance.PressureSensorUnits.OrderBy(o => o.Num));

            AirSupplyController = airSupplyController;
            AirSupplyController.ViewModel = this;
            AirSupplyController.Initalize();
        }

        /// <summary>
        /// 控制
        /// </summary>
        public AirSupplyController AirSupplyController { get; set; }
        
        /// <summary>
        /// 车0压力传感器压力
        /// </summary>
        public float PressureSensorValue0
        {
            get { return  m_PressureSensorValue0;}
            set {
                if (value == m_PressureSensorValue0)
                {
                    return;
                }
                m_PressureSensorValue0 = value;
                RaisePropertyChanged(() => PressureSensorValue0);
            }
        }

        /// <summary>
        /// 车0压力传感器压力
        /// </summary>
        public float PressureSensorValue5
        {
            get { return m_PressureSensorValue5; }
            set
            {
                if (value == m_PressureSensorValue5)
                {
                    return;
                }
                m_PressureSensorValue5 = value;
                RaisePropertyChanged(() => PressureSensorValue5);
            }
        }

        /// <summary>
        /// 车4压力传感器压力
        /// </summary>
        public float PressureSensorValue4
        {
            get { return m_PressureSensorValue4; }
            set
            {
                if (value == m_PressureSensorValue4)
                {
                    return;
                }
                m_PressureSensorValue4 = value;
                RaisePropertyChanged(() => PressureSensorValue4);
            }
        }

        /// <summary>
        /// 车1压力传感器压力
        /// </summary>
        public float PressureSensorValue1
        {
            get { return m_PressureSensorValue1; }
            set
            {
                if (value == m_PressureSensorValue1)
                {
                    return;
                }
                m_PressureSensorValue1 = value;
                RaisePropertyChanged(() => PressureSensorValue1);
            }
        }

        /// <summary>
        /// 主压缩机单元
        /// </summary>
        public ObservableCollection<PrimaryCompressorUnit> PrimaryCompressorUnits
        {
            get { return m_PrimaryCompressorUnits; }
            private set
            {
                if (Equals(value, m_PrimaryCompressorUnits))
                {
                    return;
                }
                m_PrimaryCompressorUnits = value;
                RaisePropertyChanged(() => PrimaryCompressorUnits);
                RaisePropertyChanged(() => PrimaryCompressor5);
                RaisePropertyChanged(() => PrimaryCompressor4);
            }
        }
        /// <summary>
        /// 车5主压缩机
        /// </summary>
        public PrimaryCompressorUnit PrimaryCompressor5 { get { return PrimaryCompressorUnits.Where(w => w.Car == 5 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车4主压缩机
        /// </summary>
        public PrimaryCompressorUnit PrimaryCompressor4 { get { return PrimaryCompressorUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 辅助压缩机单元
        /// </summary>
        public ObservableCollection<SubCompressorUnit> SubCompressorUnits
        {
            get { return m_SubCompressorUnits; }
            private set
            {
                if (Equals(value, m_SubCompressorUnits))
                {
                    return;
                }
                m_SubCompressorUnits = value;
                RaisePropertyChanged(() => SubCompressorUnits);
                RaisePropertyChanged(() => SubCompressor7);
                RaisePropertyChanged(() => SubCompressor2);
            }
        }

        /// <summary>
        /// 车7辅助压缩机
        /// </summary>
        public SubCompressorUnit SubCompressor7 { get { return SubCompressorUnits.Where(w => w.Car == 7 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车2辅助压缩机
        /// </summary>
        public SubCompressorUnit SubCompressor2 { get { return SubCompressorUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        

        /// <summary>
        /// 压力传感器单元 
        /// </summary>
        public ObservableCollection<PressureSensorUnit> PressureSensorUnits
        {
            get { return m_PressureSensorUnits; }
            private set
            {
                if (Equals(value, m_PressureSensorUnits))
                {
                    return;
                }
                m_PressureSensorUnits = value;
                RaisePropertyChanged(() => PressureSensorUnits);
                RaisePropertyChanged(() => PressureSensor0);
                RaisePropertyChanged(() => PressureSensor5);
                RaisePropertyChanged(() => PressureSensor4);
                RaisePropertyChanged(() => PressureSensor1);
            }
        }

        /// <summary>
        /// 车0压力传感器
        /// </summary>
        public PressureSensorUnit PressureSensor0 { get { return PressureSensorUnits.Where(w => w.Car == 0 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        
        /// <summary>
        /// 车5压力传感器
        /// </summary>
        public PressureSensorUnit PressureSensor5 { get { return PressureSensorUnits.Where(w => w.Car == 5 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车4压力传感器
        /// </summary>
        public PressureSensorUnit PressureSensor4 { get { return PressureSensorUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车1压力传感器
        /// </summary>
        public PressureSensorUnit PressureSensor1 { get { return PressureSensorUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
    }
}
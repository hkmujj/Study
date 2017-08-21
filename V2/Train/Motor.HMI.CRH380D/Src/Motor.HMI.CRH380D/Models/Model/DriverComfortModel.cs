using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.SZLHLF.Model;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class DriverComfortModel : ModelBase
    {
        private int m_Air;
        private ObservableCollection<CarComfortUnit> m_CarComfortUnits;
        private float m_OutTemperature;
        private bool m_AddTemperatureIsEnable;
        private bool m_SubTemperatureIsEnable;
        private bool m_AddAirIsEnable;
        private bool m_SubAirIsEnable;
        private bool m_Car0IsCheck;
        private bool m_Car1IsCheck;

        [ImportingConstructor]
        public DriverComfortModel(DriverComfortController driverComfortController)
        {
            CarComfortUnits = new ObservableCollection<CarComfortUnit>(GlobalParam.Instance.CarComfortUnits.OrderBy(o => o.Num));

            DriverComfortController = driverComfortController;
            DriverComfortController.ViewModel = this;
            DriverComfortController.Initalize();
        }

        /// <summary>
        /// 控制
        /// </summary>
        public DriverComfortController DriverComfortController { get; set; }
        

        /// <summary>
        /// 司机室气流
        /// </summary>
        public int Air
        {
            get { return m_Air; }
            set {
                if (value == m_Air)
                {
                    return;
                }
                m_Air = value;
                RaisePropertyChanged(() => Air);
            }
        }

        /// <summary>
        /// 空调状态单元
        /// </summary>
        public ObservableCollection<CarComfortUnit> CarComfortUnits
        {
            get { return m_CarComfortUnits; }
            private set
            {
                if (Equals(value, m_CarComfortUnits))
                {
                    return;
                }
                m_CarComfortUnits = value;
                RaisePropertyChanged(() => CarComfortUnits);
                RaisePropertyChanged(() => CarComfort0);
                RaisePropertyChanged(() => CarComfort7);
                RaisePropertyChanged(() => CarComfort6);
                RaisePropertyChanged(() => CarComfort5);
                RaisePropertyChanged(() => CarComfort4);
                RaisePropertyChanged(() => CarComfort3);
                RaisePropertyChanged(() => CarComfort2);
                RaisePropertyChanged(() => CarComfort1);
            }
        }
        /// <summary>
        /// 车0空调状态
        /// </summary>
        public CarComfortUnit CarComfort0 { get { return CarComfortUnits.Where(w => w.Car == 0 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车7空调状态
        /// </summary>
        public CarComfortUnit CarComfort7 { get { return CarComfortUnits.Where(w => w.Car == 7 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车6空调状态
        /// </summary>
        public CarComfortUnit CarComfort6 { get { return CarComfortUnits.Where(w => w.Car == 6 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车5空调状态
        /// </summary>
        public CarComfortUnit CarComfort5 { get { return CarComfortUnits.Where(w => w.Car == 5 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车4空调状态
        /// </summary>
        public CarComfortUnit CarComfort4 { get { return CarComfortUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车3空调状态
        /// </summary>
        public CarComfortUnit CarComfort3 { get { return CarComfortUnits.Where(w => w.Car == 3 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车2空调状态
        /// </summary>
        public CarComfortUnit CarComfort2 { get { return CarComfortUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车1空调状态
        /// </summary>
        public CarComfortUnit CarComfort1 { get { return CarComfortUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 外部温度
        /// </summary>
        public float OutTemperature
        {
            get { return m_OutTemperature; }
            set
            {
                if (value == m_OutTemperature)
                {
                    return;
                }
                m_OutTemperature = value;
                RaisePropertyChanged(() => OutTemperature);
            }
        }

        /// <summary>
        /// 温度增加
        /// </summary>
        public bool AddTemperatureIsEnable
        {
            get { return m_AddTemperatureIsEnable; }
            set
            {
                if (value == m_AddTemperatureIsEnable)
                {
                    return;
                }
                m_AddTemperatureIsEnable = value;
                RaisePropertyChanged(() => AddTemperatureIsEnable);
            }
        }

        /// <summary>
        /// 温度减少
        /// </summary>
        public bool SubTemperatureIsEnable
        {
            get { return m_SubTemperatureIsEnable; }
            set
            {
                if (value == m_SubTemperatureIsEnable)
                {
                    return;
                }
                m_SubTemperatureIsEnable = value;
                RaisePropertyChanged(() => SubTemperatureIsEnable);
            }
        }

        /// <summary>
        /// 气流增加
        /// </summary>
        public bool AddAirIsEnable
        {
            get { return m_AddAirIsEnable; }
            set
            {
                if (value == m_AddAirIsEnable)
                {
                    return;
                }
                m_AddAirIsEnable = value;
                RaisePropertyChanged(() => AddAirIsEnable);
            }
        }

        /// <summary>
        /// 气流减少
        /// </summary>
        public bool SubAirIsEnable
        {
            get { return m_SubAirIsEnable; }
            set
            {
                if (value == m_SubAirIsEnable)
                {
                    return;
                }
                m_SubAirIsEnable = value;
                RaisePropertyChanged(() => SubAirIsEnable);
            }
        }

        /// <summary>
        /// 司机室0选中
        /// </summary>
        public bool Car0IsCheck
        {
            get { return m_Car0IsCheck; }
            set
            {
                if (value == m_Car0IsCheck)
                {
                    return;
                }
                m_Car0IsCheck = value;
                RaisePropertyChanged(() => Car0IsCheck);
            }
        }

        /// <summary>
        /// 司机室1选中
        /// </summary>
        public bool Car1IsCheck
        {
            get { return m_Car1IsCheck; }
            set
            {
                if (value == m_Car1IsCheck)
                {
                    return;
                }
                m_Car1IsCheck = value;
                RaisePropertyChanged(() => Car1IsCheck);
            }
        }
    }
}
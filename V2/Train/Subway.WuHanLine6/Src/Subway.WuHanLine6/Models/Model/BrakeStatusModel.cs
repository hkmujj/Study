using System.ComponentModel.Composition;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 制动状态Model
    /// </summary>
    [Export]
    public class BrakeStatusModel : ModelBase
    {
        private bool m_BogieF0011;
        private bool m_BogieF0021;
        private bool m_BogieF0031;
        private double m_BrakeF0061;
        private double m_BrakeF0051;
        private double m_BrakeF0041;
        private double m_BrakeF0031;
        private double m_BrakeF0021;
        private double m_BrakeF0011;
        private double m_LoadF0061;
        private double m_LoadF0051;
        private double m_LoadF0041;
        private double m_LoadF0031;
        private double m_LoadF0021;
        private double m_LoadF0011;
        private double m_BrakeF0062;
        private double m_BrakeF0052;
        private double m_BrakeF0042;
        private double m_BrakeF0032;
        private double m_BrakeF0022;
        private double m_BrakeF0012;
        private double m_LoadF0062;
        private double m_LoadF0052;
        private double m_LoadF0042;
        private double m_LoadF0032;
        private double m_LoadF0022;
        private double m_LoadF0012;
        private bool m_AutoCheck26Two;
        private bool m_AutoCheck26One;
        private bool m_AutoCheck24Two;
        private bool m_AutoCheck24One;
        private bool m_KeepBrakeTwo;
        private bool m_KeepBrakeOne;
        private bool m_ParkingStatusF006;
        private bool m_ParkingStatusF005;
        private bool m_ParkingStatusF004;
        private bool m_ParkingStatusF003;
        private bool m_ParkingStatusF002;
        private bool m_ParkingStatusF001;
        private bool m_BrakeInflictionF0062;
        private bool m_BrakeInflictionF0052;
        private bool m_BrakeInflictionF0042;
        private bool m_BrakeInflictionF0032;
        private bool m_BrakeInflictionF0022;
        private bool m_BrakeInflictionF0012;
        private bool m_BrakeInflictionF0061;
        private bool m_BrakeInflictionF0051;
        private bool m_BrakeInflictionF0041;
        private bool m_BrakeInflictionF0031;
        private bool m_BrakeInflictionF0021;
        private bool m_BrakeInflictionF0011;
        private bool m_BogieF0062;
        private bool m_BogieF0052;
        private bool m_BogieF0042;
        private bool m_BogieF0032;
        private bool m_BogieF0022;
        private bool m_BogieF0012;
        private bool m_BogieF0061;
        private bool m_BogieF0051;
        private bool m_BogieF0041;
        private double m_Axle4SpeedF006;
        private double m_Axle4SpeedF005;
        private double m_Axle4SpeedF004;
        private double m_Axle4SpeedF003;
        private double m_Axle4SpeedF002;
        private double m_Axle4SpeedF001;
        private double m_Axle3SpeedF006;
        private double m_Axle3SpeedF005;
        private double m_Axle3SpeedF004;
        private double m_Axle3SpeedF003;
        private double m_Axle3SpeedF002;
        private double m_Axle3SpeedF001;
        private double m_Axle2SpeedF006;
        private double m_Axle2SpeedF005;
        private double m_Axle2SpeedF004;
        private double m_Axle2SpeedF003;
        private double m_Axle2SpeedF002;
        private double m_Axle2SpeedF001;
        private double m_Axle1SpeedF006;
        private double m_Axle1SpeedF005;
        private double m_Axle1SpeedF004;
        private double m_Axle1SpeedF003;
        private double m_Axle1SpeedF002;
        private double m_Axle1SpeedF001;
        private double m_MasterPressureTwo;
        private double m_MasterPressureOne;
        private double m_TickingBrakeTwo;
        private double m_TickingBrakeOne;
        private double m_CommonBrakeF006;
        private double m_CommonBrakeF005;
        private double m_CommonBrakeF004;
        private double m_CommonBrakeF003;
        private double m_CommonBrakeF002;
        private double m_CommonBrakeF001;
        private double m_ElectricityBrakeF005;
        private double m_ElectricityBrakeF004;
        private double m_ElectricityBrakeF003;
        private double m_ElectricityBrakeF002;
        private double m_APSPressureF0062;
        private double m_APSPressureF0052;
        private double m_APSPressureF0042;
        private double m_APSPressureF0032;
        private double m_APSPressureF0022;
        private double m_APSPressureF0012;
        private double m_APSPressureF0061;
        private double m_APSPressureF0051;
        private double m_APSPressureF0041;
        private double m_APSPressureF0031;
        private double m_APSPressureF0021;
        private double m_APSPressureF0011;

        /// <summary>
        /// 转向架隔离
        /// </summary>
        public bool BogieF0011
        {
            get { return m_BogieF0011; }
            set
            {
                if (value == m_BogieF0011)
                {
                    return;
                }
                m_BogieF0011 = value;
                RaisePropertyChanged(() => BogieF0011);
            }
        }

        /// <summary>
        /// 转向架隔离
        /// </summary>
        public bool BogieF0021
        {
            get { return m_BogieF0021; }
            set
            {
                if (value == m_BogieF0021)
                {
                    return;
                }
                m_BogieF0021 = value;
                RaisePropertyChanged(() => BogieF0021);
            }
        }

        /// <summary>
        /// 转向架隔离
        /// </summary>
        public bool BogieF0031
        {
            get { return m_BogieF0031; }
            set
            {
                if (value == m_BogieF0031)
                {
                    return;
                }
                m_BogieF0031 = value;
                RaisePropertyChanged(() => BogieF0031);
            }
        }

        /// <summary>
        /// 转向架隔离
        /// </summary>
        public bool BogieF0041
        {
            get { return m_BogieF0041; }
            set
            {
                if (value == m_BogieF0041)
                {
                    return;
                }
                m_BogieF0041 = value;
                RaisePropertyChanged(() => BogieF0041);
            }
        }

        /// <summary>
        /// 转向架隔离
        /// </summary>
        public bool BogieF0051
        {
            get { return m_BogieF0051; }
            set
            {
                if (value == m_BogieF0051)
                {
                    return;
                }
                m_BogieF0051 = value;
                RaisePropertyChanged(() => BogieF0051);
            }
        }

        /// <summary>
        /// 转向架隔离
        /// </summary>
        public bool BogieF0061
        {
            get { return m_BogieF0061; }
            set
            {
                if (value == m_BogieF0061)
                {
                    return;
                }
                m_BogieF0061 = value;
                RaisePropertyChanged(() => BogieF0061);
            }
        }

        /// <summary>
        /// 转向架隔离
        /// </summary>
        public bool BogieF0012
        {
            get { return m_BogieF0012; }
            set
            {
                if (value == m_BogieF0012)
                {
                    return;
                }
                m_BogieF0012 = value;
                RaisePropertyChanged(() => BogieF0012);
            }
        }

        /// <summary>
        /// 转向架隔离
        /// </summary>
        public bool BogieF0022
        {
            get { return m_BogieF0022; }
            set
            {
                if (value == m_BogieF0022)
                {
                    return;
                }
                m_BogieF0022 = value;
                RaisePropertyChanged(() => BogieF0022);
            }
        }

        /// <summary>
        /// 转向架隔离
        /// </summary>
        public bool BogieF0032
        {
            get { return m_BogieF0032; }
            set
            {
                if (value == m_BogieF0032)
                {
                    return;
                }
                m_BogieF0032 = value;
                RaisePropertyChanged(() => BogieF0032);
            }
        }

        /// <summary>
        /// 转向架隔离
        /// </summary>
        public bool BogieF0042
        {
            get { return m_BogieF0042; }
            set
            {
                if (value == m_BogieF0042)
                {
                    return;
                }
                m_BogieF0042 = value;
                RaisePropertyChanged(() => BogieF0042);
            }
        }

        /// <summary>
        /// 转向架隔离
        /// </summary>
        public bool BogieF0052
        {
            get { return m_BogieF0052; }
            set
            {
                if (value == m_BogieF0052)
                {
                    return;
                }
                m_BogieF0052 = value;
                RaisePropertyChanged(() => BogieF0052);
            }
        }

        /// <summary>
        /// 转向架隔离
        /// </summary>
        public bool BogieF0062
        {
            get { return m_BogieF0062; }
            set
            {
                if (value == m_BogieF0062)
                {
                    return;
                }
                m_BogieF0062 = value;
                RaisePropertyChanged(() => BogieF0062);
            }
        }

        /// <summary>
        /// 制动施加
        /// </summary>
        public bool BrakeInflictionF0011
        {
            get { return m_BrakeInflictionF0011; }
            set
            {
                if (value == m_BrakeInflictionF0011)
                {
                    return;
                }
                m_BrakeInflictionF0011 = value;
                RaisePropertyChanged(() => BrakeInflictionF0011);
            }
        }

        /// <summary>
        /// 制动施加
        /// </summary>
        public bool BrakeInflictionF0021
        {
            get { return m_BrakeInflictionF0021; }
            set
            {
                if (value == m_BrakeInflictionF0021)
                {
                    return;
                }
                m_BrakeInflictionF0021 = value;
                RaisePropertyChanged(() => BrakeInflictionF0021);
            }
        }

        /// <summary>
        /// 制动施加
        /// </summary>
        public bool BrakeInflictionF0031
        {
            get { return m_BrakeInflictionF0031; }
            set
            {
                if (value == m_BrakeInflictionF0031)
                {
                    return;
                }
                m_BrakeInflictionF0031 = value;
                RaisePropertyChanged(() => BrakeInflictionF0031);
            }
        }

        /// <summary>
        /// 制动施加
        /// </summary>
        public bool BrakeInflictionF0041
        {
            get { return m_BrakeInflictionF0041; }
            set
            {
                if (value == m_BrakeInflictionF0041)
                {
                    return;
                }
                m_BrakeInflictionF0041 = value;
                RaisePropertyChanged(() => BrakeInflictionF0041);
            }
        }

        /// <summary>
        /// 制动施加
        /// </summary>
        public bool BrakeInflictionF0051
        {
            get { return m_BrakeInflictionF0051; }
            set
            {
                if (value == m_BrakeInflictionF0051)
                {
                    return;
                }
                m_BrakeInflictionF0051 = value;
                RaisePropertyChanged(() => BrakeInflictionF0051);
            }
        }

        /// <summary>
        /// 制动施加
        /// </summary>
        public bool BrakeInflictionF0061
        {
            get { return m_BrakeInflictionF0061; }
            set
            {
                if (value == m_BrakeInflictionF0061)
                {
                    return;
                }
                m_BrakeInflictionF0061 = value;
                RaisePropertyChanged(() => BrakeInflictionF0061);
            }
        }

        /// <summary>
        /// 制动施加
        /// </summary>
        public bool BrakeInflictionF0012
        {
            get { return m_BrakeInflictionF0012; }
            set
            {
                if (value == m_BrakeInflictionF0012)
                {
                    return;
                }
                m_BrakeInflictionF0012 = value;
                RaisePropertyChanged(() => BrakeInflictionF0012);
            }
        }

        /// <summary>
        /// 制动施加
        /// </summary>
        public bool BrakeInflictionF0022
        {
            get { return m_BrakeInflictionF0022; }
            set
            {
                if (value == m_BrakeInflictionF0022)
                {
                    return;
                }
                m_BrakeInflictionF0022 = value;
                RaisePropertyChanged(() => BrakeInflictionF0022);
            }
        }

        /// <summary>
        /// 制动施加
        /// </summary>
        public bool BrakeInflictionF0032
        {
            get { return m_BrakeInflictionF0032; }
            set
            {
                if (value == m_BrakeInflictionF0032)
                {
                    return;
                }
                m_BrakeInflictionF0032 = value;
                RaisePropertyChanged(() => BrakeInflictionF0032);
            }
        }

        /// <summary>
        /// 制动施加
        /// </summary>
        public bool BrakeInflictionF0042
        {
            get { return m_BrakeInflictionF0042; }
            set
            {
                if (value == m_BrakeInflictionF0042)
                {
                    return;
                }
                m_BrakeInflictionF0042 = value;
                RaisePropertyChanged(() => BrakeInflictionF0042);
            }
        }

        /// <summary>
        /// 制动施加
        /// </summary>
        public bool BrakeInflictionF0052
        {
            get { return m_BrakeInflictionF0052; }
            set
            {
                if (value == m_BrakeInflictionF0052)
                {
                    return;
                }
                m_BrakeInflictionF0052 = value;
                RaisePropertyChanged(() => BrakeInflictionF0052);
            }
        }

        /// <summary>
        /// 制动施加
        /// </summary>
        public bool BrakeInflictionF0062
        {
            get { return m_BrakeInflictionF0062; }
            set
            {
                if (value == m_BrakeInflictionF0062)
                {
                    return;
                }
                m_BrakeInflictionF0062 = value;
                RaisePropertyChanged(() => BrakeInflictionF0062);
            }
        }

        /// <summary>
        /// 停放制动状态
        /// </summary>
        public bool ParkingStatusF001
        {
            get { return m_ParkingStatusF001; }
            set
            {
                if (value == m_ParkingStatusF001)
                {
                    return;
                }
                m_ParkingStatusF001 = value;
                RaisePropertyChanged(() => ParkingStatusF001);
            }
        }

        /// <summary>
        /// 停放制动状态
        /// </summary>
        public bool ParkingStatusF002
        {
            get { return m_ParkingStatusF002; }
            set
            {
                if (value == m_ParkingStatusF002)
                {
                    return;
                }
                m_ParkingStatusF002 = value;
                RaisePropertyChanged(() => ParkingStatusF002);
            }
        }

        /// <summary>
        /// 停放制动状态
        /// </summary>
        public bool ParkingStatusF003
        {
            get { return m_ParkingStatusF003; }
            set
            {
                if (value == m_ParkingStatusF003)
                {
                    return;
                }
                m_ParkingStatusF003 = value;
                RaisePropertyChanged(() => ParkingStatusF003);
            }
        }

        /// <summary>
        /// 停放制动状态
        /// </summary>
        public bool ParkingStatusF004
        {
            get { return m_ParkingStatusF004; }
            set
            {
                if (value == m_ParkingStatusF004)
                {
                    return;
                }
                m_ParkingStatusF004 = value;
                RaisePropertyChanged(() => ParkingStatusF004);
            }
        }

        /// <summary>
        /// 停放制动状态
        /// </summary>
        public bool ParkingStatusF005
        {
            get { return m_ParkingStatusF005; }
            set
            {
                if (value == m_ParkingStatusF005)
                {
                    return;
                }
                m_ParkingStatusF005 = value;
                RaisePropertyChanged(() => ParkingStatusF005);
            }
        }

        /// <summary>
        /// 停放制动状态
        /// </summary>
        public bool ParkingStatusF006
        {
            get { return m_ParkingStatusF006; }
            set
            {
                if (value == m_ParkingStatusF006)
                {
                    return;
                }
                m_ParkingStatusF006 = value;
                RaisePropertyChanged(() => ParkingStatusF006);
            }
        }

        /// <summary>
        /// 保持制动
        /// </summary>
        public bool KeepBrakeOne
        {
            get { return m_KeepBrakeOne; }
            set
            {
                if (value == m_KeepBrakeOne)
                {
                    return;
                }
                m_KeepBrakeOne = value;
                RaisePropertyChanged(() => KeepBrakeOne);
            }
        }

        /// <summary>
        /// 保持制动
        /// </summary>
        public bool KeepBrakeTwo
        {
            get { return m_KeepBrakeTwo; }
            set
            {
                if (value == m_KeepBrakeTwo)
                {
                    return;
                }
                m_KeepBrakeTwo = value;
                RaisePropertyChanged(() => KeepBrakeTwo);
            }
        }

        /// <summary>
        /// 自检时间间隔超过24小时
        /// </summary>
        public bool AutoCheck24One
        {
            get { return m_AutoCheck24One; }
            set
            {
                if (value == m_AutoCheck24One)
                {
                    return;
                }
                m_AutoCheck24One = value;
                RaisePropertyChanged(() => AutoCheck24One);
            }
        }

        /// <summary>
        /// 自检时间间隔超过24小时
        /// </summary>
        public bool AutoCheck24Two
        {
            get { return m_AutoCheck24Two; }
            set
            {
                if (value == m_AutoCheck24Two)
                {
                    return;
                }
                m_AutoCheck24Two = value;
                RaisePropertyChanged(() => AutoCheck24Two);
            }
        }

        /// <summary>
        /// 自检时间间隔超过26小时
        /// </summary>
        public bool AutoCheck26One
        {
            get { return m_AutoCheck26One; }
            set
            {
                if (value == m_AutoCheck26One)
                {
                    return;
                }
                m_AutoCheck26One = value;
                RaisePropertyChanged(() => AutoCheck26One);
            }
        }

        /// <summary>
        /// 自检时间间隔超过26小时
        /// </summary>
        public bool AutoCheck26Two
        {
            get { return m_AutoCheck26Two; }
            set
            {
                if (value == m_AutoCheck26Two)
                {
                    return;
                }
                m_AutoCheck26Two = value;
                RaisePropertyChanged(() => AutoCheck26Two);
            }
        }

        /// <summary>
        /// 载荷
        /// </summary>
        public double LoadF0011
        {
            get { return m_LoadF0011; }
            set
            {
                if (value.Equals(m_LoadF0011))
                {
                    return;
                }
                m_LoadF0011 = value;
                RaisePropertyChanged(() => LoadF0011);
            }
        }

        /// <summary>
        /// 载荷
        /// </summary>
        public double LoadF0021
        {
            get { return m_LoadF0021; }
            set
            {
                if (value.Equals(m_LoadF0021))
                {
                    return;
                }
                m_LoadF0021 = value;
                RaisePropertyChanged(() => LoadF0021);
            }
        }

        /// <summary>
        /// 载荷
        /// </summary>
        public double LoadF0031
        {
            get { return m_LoadF0031; }
            set
            {
                if (value.Equals(m_LoadF0031))
                {
                    return;
                }
                m_LoadF0031 = value;
                RaisePropertyChanged(() => LoadF0031);
            }
        }

        /// <summary>
        /// 载荷
        /// </summary>
        public double LoadF0041
        {
            get { return m_LoadF0041; }
            set
            {
                if (value.Equals(m_LoadF0041))
                {
                    return;
                }
                m_LoadF0041 = value;
                RaisePropertyChanged(() => LoadF0041);
            }
        }

        /// <summary>
        /// 载荷
        /// </summary>
        public double LoadF0051
        {
            get { return m_LoadF0051; }
            set
            {
                if (value.Equals(m_LoadF0051))
                {
                    return;
                }
                m_LoadF0051 = value;
                RaisePropertyChanged(() => LoadF0051);
            }
        }

        /// <summary>
        /// 载荷
        /// </summary>
        public double LoadF0061
        {
            get { return m_LoadF0061; }
            set
            {
                if (value.Equals(m_LoadF0061))
                {
                    return;
                }
                m_LoadF0061 = value;
                RaisePropertyChanged(() => LoadF0061);
            }
        }

        /// <summary>
        /// 载荷
        /// </summary>
        public double LoadF0012
        {
            get { return m_LoadF0012; }
            set
            {
                if (value.Equals(m_LoadF0012))
                {
                    return;
                }
                m_LoadF0012 = value;
                RaisePropertyChanged(() => LoadF0012);
            }
        }

        /// <summary>
        /// 载荷
        /// </summary>
        public double LoadF0022
        {
            get { return m_LoadF0022; }
            set
            {
                if (value.Equals(m_LoadF0022))
                {
                    return;
                }
                m_LoadF0022 = value;
                RaisePropertyChanged(() => LoadF0022);
            }
        }

        /// <summary>
        /// 载荷
        /// </summary>
        public double LoadF0032
        {
            get { return m_LoadF0032; }
            set
            {
                if (value.Equals(m_LoadF0032))
                {
                    return;
                }
                m_LoadF0032 = value;
                RaisePropertyChanged(() => LoadF0032);
            }
        }

        /// <summary>
        /// 载荷
        /// </summary>
        public double LoadF0042
        {
            get { return m_LoadF0042; }
            set
            {
                if (value.Equals(m_LoadF0042))
                {
                    return;
                }
                m_LoadF0042 = value;
                RaisePropertyChanged(() => LoadF0042);
            }
        }

        /// <summary>
        /// 载荷
        /// </summary>
        public double LoadF0052
        {
            get { return m_LoadF0052; }
            set
            {
                if (value.Equals(m_LoadF0052))
                {
                    return;
                }
                m_LoadF0052 = value;
                RaisePropertyChanged(() => LoadF0052);
            }
        }

        /// <summary>
        /// 载荷
        /// </summary>
        public double LoadF0062
        {
            get { return m_LoadF0062; }
            set
            {
                if (value.Equals(m_LoadF0062))
                {
                    return;
                }
                m_LoadF0062 = value;
                RaisePropertyChanged(() => LoadF0062);
            }
        }

        /// <summary>
        /// 制动压力
        /// </summary>
        public double BrakeF0011
        {
            get { return m_BrakeF0011; }
            set
            {
                if (value.Equals(m_BrakeF0011))
                {
                    return;
                }
                m_BrakeF0011 = value;
                RaisePropertyChanged(() => BrakeF0011);
            }
        }

        /// <summary>
        /// 制动压力
        /// </summary>
        public double BrakeF0021
        {
            get { return m_BrakeF0021; }
            set
            {
                if (value.Equals(m_BrakeF0021))
                {
                    return;
                }
                m_BrakeF0021 = value;
                RaisePropertyChanged(() => BrakeF0021);
            }
        }

        /// <summary>
        /// 制动压力
        /// </summary>
        public double BrakeF0031
        {
            get { return m_BrakeF0031; }
            set
            {
                if (value.Equals(m_BrakeF0031))
                {
                    return;
                }
                m_BrakeF0031 = value;
                RaisePropertyChanged(() => BrakeF0031);
            }
        }

        /// <summary>
        /// 制动压力
        /// </summary>
        public double BrakeF0041
        {
            get { return m_BrakeF0041; }
            set
            {
                if (value.Equals(m_BrakeF0041))
                {
                    return;
                }
                m_BrakeF0041 = value;
                RaisePropertyChanged(() => BrakeF0041);
            }
        }

        /// <summary>
        /// 制动压力
        /// </summary>
        public double BrakeF0051
        {
            get { return m_BrakeF0051; }
            set
            {
                if (value.Equals(m_BrakeF0051))
                {
                    return;
                }
                m_BrakeF0051 = value;
                RaisePropertyChanged(() => BrakeF0051);
            }
        }

        /// <summary>
        /// 制动压力
        /// </summary>
        public double BrakeF0061
        {
            get { return m_BrakeF0061; }
            set
            {
                if (value.Equals(m_BrakeF0061))
                {
                    return;
                }
                m_BrakeF0061 = value;
                RaisePropertyChanged(() => BrakeF0061);
            }
        }

        /// <summary>
        /// 制动压力
        /// </summary>
        public double BrakeF0012
        {
            get { return m_BrakeF0012; }
            set
            {
                if (value.Equals(m_BrakeF0012))
                {
                    return;
                }
                m_BrakeF0012 = value;
                RaisePropertyChanged(() => BrakeF0012);
            }
        }

        /// <summary>
        /// 制动压力
        /// </summary>
        public double BrakeF0022
        {
            get { return m_BrakeF0022; }
            set
            {
                if (value.Equals(m_BrakeF0022))
                {
                    return;
                }
                m_BrakeF0022 = value;
                RaisePropertyChanged(() => BrakeF0022);
            }
        }

        /// <summary>
        /// 制动压力
        /// </summary>
        public double BrakeF0032
        {
            get { return m_BrakeF0032; }
            set
            {
                if (value.Equals(m_BrakeF0032))
                {
                    return;
                }
                m_BrakeF0032 = value;
                RaisePropertyChanged(() => BrakeF0032);
            }
        }

        /// <summary>
        /// 制动压力
        /// </summary>
        public double BrakeF0042
        {
            get { return m_BrakeF0042; }
            set
            {
                if (value.Equals(m_BrakeF0042))
                {
                    return;
                }
                m_BrakeF0042 = value;
                RaisePropertyChanged(() => BrakeF0042);
            }
        }

        /// <summary>
        /// 制动压力
        /// </summary>
        public double BrakeF0052
        {
            get { return m_BrakeF0052; }
            set
            {
                if (value.Equals(m_BrakeF0052))
                {
                    return;
                }
                m_BrakeF0052 = value;
                RaisePropertyChanged(() => BrakeF0052);
            }
        }

        /// <summary>
        /// 制动压力
        /// </summary>
        public double BrakeF0062
        {
            get { return m_BrakeF0062; }
            set
            {
                if (value.Equals(m_BrakeF0062))
                {
                    return;
                }
                m_BrakeF0062 = value;
                RaisePropertyChanged(() => BrakeF0062);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double APSPressureF0011
        {
            get { return m_APSPressureF0011; }
            set
            {
                if (value.Equals(m_APSPressureF0011))
                {
                    return;
                }
                m_APSPressureF0011 = value;
                RaisePropertyChanged(() => APSPressureF0011);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double APSPressureF0021
        {
            get { return m_APSPressureF0021; }
            set
            {
                if (value.Equals(m_APSPressureF0021))
                {
                    return;
                }
                m_APSPressureF0021 = value;
                RaisePropertyChanged(() => APSPressureF0021);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double APSPressureF0031
        {
            get { return m_APSPressureF0031; }
            set
            {
                if (value.Equals(m_APSPressureF0031))
                {
                    return;
                }
                m_APSPressureF0031 = value;
                RaisePropertyChanged(() => APSPressureF0031);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double APSPressureF0041
        {
            get { return m_APSPressureF0041; }
            set
            {
                if (value.Equals(m_APSPressureF0041))
                {
                    return;
                }
                m_APSPressureF0041 = value;
                RaisePropertyChanged(() => APSPressureF0041);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double APSPressureF0051
        {
            get { return m_APSPressureF0051; }
            set
            {
                if (value.Equals(m_APSPressureF0051))
                {
                    return;
                }
                m_APSPressureF0051 = value;
                RaisePropertyChanged(() => APSPressureF0051);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double APSPressureF0061
        {
            get { return m_APSPressureF0061; }
            set
            {
                if (value.Equals(m_APSPressureF0061))
                {
                    return;
                }
                m_APSPressureF0061 = value;
                RaisePropertyChanged(() => APSPressureF0061);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double APSPressureF0012
        {
            get { return m_APSPressureF0012; }
            set
            {
                if (value.Equals(m_APSPressureF0012))
                {
                    return;
                }
                m_APSPressureF0012 = value;
                RaisePropertyChanged(() => APSPressureF0012);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double APSPressureF0022
        {
            get { return m_APSPressureF0022; }
            set
            {
                if (value.Equals(m_APSPressureF0022))
                {
                    return;
                }
                m_APSPressureF0022 = value;
                RaisePropertyChanged(() => APSPressureF0022);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double APSPressureF0032
        {
            get { return m_APSPressureF0032; }
            set
            {
                if (value.Equals(m_APSPressureF0032))
                {
                    return;
                }
                m_APSPressureF0032 = value;
                RaisePropertyChanged(() => APSPressureF0032);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double APSPressureF0042
        {
            get { return m_APSPressureF0042; }
            set
            {
                if (value.Equals(m_APSPressureF0042))
                {
                    return;
                }
                m_APSPressureF0042 = value;
                RaisePropertyChanged(() => APSPressureF0042);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double APSPressureF0052
        {
            get { return m_APSPressureF0052; }
            set
            {
                if (value.Equals(m_APSPressureF0052))
                {
                    return;
                }
                m_APSPressureF0052 = value;
                RaisePropertyChanged(() => APSPressureF0052);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double APSPressureF0062
        {
            get { return m_APSPressureF0062; }
            set
            {
                if (value.Equals(m_APSPressureF0062))
                {
                    return;
                }
                m_APSPressureF0062 = value;
                RaisePropertyChanged(() => APSPressureF0062);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double ElectricityBrakeF002
        {
            get { return m_ElectricityBrakeF002; }
            set
            {
                if (value.Equals(m_ElectricityBrakeF002))
                {
                    return;
                }
                m_ElectricityBrakeF002 = value;
                RaisePropertyChanged(() => ElectricityBrakeF002);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double ElectricityBrakeF003
        {
            get { return m_ElectricityBrakeF003; }
            set
            {
                if (value.Equals(m_ElectricityBrakeF003))
                {
                    return;
                }
                m_ElectricityBrakeF003 = value;
                RaisePropertyChanged(() => ElectricityBrakeF003);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double ElectricityBrakeF004
        {
            get { return m_ElectricityBrakeF004; }
            set
            {
                if (value.Equals(m_ElectricityBrakeF004))
                {
                    return;
                }
                m_ElectricityBrakeF004 = value;
                RaisePropertyChanged(() => ElectricityBrakeF004);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double ElectricityBrakeF005
        {
            get { return m_ElectricityBrakeF005; }
            set
            {
                if (value.Equals(m_ElectricityBrakeF005))
                {
                    return;
                }
                m_ElectricityBrakeF005 = value;
                RaisePropertyChanged(() => ElectricityBrakeF005);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double CommonBrakeF001
        {
            get { return m_CommonBrakeF001; }
            set
            {
                if (value.Equals(m_CommonBrakeF001))
                {
                    return;
                }
                m_CommonBrakeF001 = value;
                RaisePropertyChanged(() => CommonBrakeF001);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double CommonBrakeF002
        {
            get { return m_CommonBrakeF002; }
            set
            {
                if (value.Equals(m_CommonBrakeF002))
                {
                    return;
                }
                m_CommonBrakeF002 = value;
                RaisePropertyChanged(() => CommonBrakeF002);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double CommonBrakeF003
        {
            get { return m_CommonBrakeF003; }
            set
            {
                if (value.Equals(m_CommonBrakeF003))
                {
                    return;
                }
                m_CommonBrakeF003 = value;
                RaisePropertyChanged(() => CommonBrakeF003);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double CommonBrakeF004
        {
            get { return m_CommonBrakeF004; }
            set
            {
                if (value.Equals(m_CommonBrakeF004))
                {
                    return;
                }
                m_CommonBrakeF004 = value;
                RaisePropertyChanged(() => CommonBrakeF004);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double CommonBrakeF005
        {
            get { return m_CommonBrakeF005; }
            set
            {
                if (value.Equals(m_CommonBrakeF005))
                {
                    return;
                }
                m_CommonBrakeF005 = value;
                RaisePropertyChanged(() => CommonBrakeF005);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double CommonBrakeF006
        {
            get { return m_CommonBrakeF006; }
            set
            {
                if (value.Equals(m_CommonBrakeF006))
                {
                    return;
                }
                m_CommonBrakeF006 = value;
                RaisePropertyChanged(() => CommonBrakeF006);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double TickingBrakeOne
        {
            get { return m_TickingBrakeOne; }
            set
            {
                if (value.Equals(m_TickingBrakeOne))
                {
                    return;
                }
                m_TickingBrakeOne = value;
                RaisePropertyChanged(() => TickingBrakeOne);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double TickingBrakeTwo
        {
            get { return m_TickingBrakeTwo; }
            set
            {
                if (value.Equals(m_TickingBrakeTwo))
                {
                    return;
                }
                m_TickingBrakeTwo = value;
                RaisePropertyChanged(() => TickingBrakeTwo);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double MasterPressureOne
        {
            get { return m_MasterPressureOne; }
            set
            {
                if (value.Equals(m_MasterPressureOne))
                {
                    return;
                }
                m_MasterPressureOne = value;
                RaisePropertyChanged(() => MasterPressureOne);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double MasterPressureTwo
        {
            get { return m_MasterPressureTwo; }
            set
            {
                if (value.Equals(m_MasterPressureTwo))
                {
                    return;
                }
                m_MasterPressureTwo = value;
                RaisePropertyChanged(() => MasterPressureTwo);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle1SpeedF001
        {
            get { return m_Axle1SpeedF001; }
            set
            {
                if (value.Equals(m_Axle1SpeedF001))
                {
                    return;
                }
                m_Axle1SpeedF001 = value;
                RaisePropertyChanged(() => Axle1SpeedF001);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle1SpeedF002
        {
            get { return m_Axle1SpeedF002; }
            set
            {
                if (value.Equals(m_Axle1SpeedF002))
                {
                    return;
                }
                m_Axle1SpeedF002 = value;
                RaisePropertyChanged(() => Axle1SpeedF002);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle1SpeedF003
        {
            get { return m_Axle1SpeedF003; }
            set
            {
                if (value.Equals(m_Axle1SpeedF003))
                {
                    return;
                }
                m_Axle1SpeedF003 = value;
                RaisePropertyChanged(() => Axle1SpeedF003);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle1SpeedF004
        {
            get { return m_Axle1SpeedF004; }
            set
            {
                if (value.Equals(m_Axle1SpeedF004))
                {
                    return;
                }
                m_Axle1SpeedF004 = value;
                RaisePropertyChanged(() => Axle1SpeedF004);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle1SpeedF005
        {
            get { return m_Axle1SpeedF005; }
            set
            {
                if (value.Equals(m_Axle1SpeedF005))
                {
                    return;
                }
                m_Axle1SpeedF005 = value;
                RaisePropertyChanged(() => Axle1SpeedF005);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle1SpeedF006
        {
            get { return m_Axle1SpeedF006; }
            set
            {
                if (value.Equals(m_Axle1SpeedF006))
                {
                    return;
                }
                m_Axle1SpeedF006 = value;
                RaisePropertyChanged(() => Axle1SpeedF006);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle2SpeedF001
        {
            get { return m_Axle2SpeedF001; }
            set
            {
                if (value.Equals(m_Axle2SpeedF001))
                {
                    return;
                }
                m_Axle2SpeedF001 = value;
                RaisePropertyChanged(() => Axle2SpeedF001);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle2SpeedF002
        {
            get { return m_Axle2SpeedF002; }
            set
            {
                if (value.Equals(m_Axle2SpeedF002))
                {
                    return;
                }
                m_Axle2SpeedF002 = value;
                RaisePropertyChanged(() => Axle2SpeedF002);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle2SpeedF003
        {
            get { return m_Axle2SpeedF003; }
            set
            {
                if (value.Equals(m_Axle2SpeedF003))
                {
                    return;
                }
                m_Axle2SpeedF003 = value;
                RaisePropertyChanged(() => Axle2SpeedF003);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle2SpeedF004
        {
            get { return m_Axle2SpeedF004; }
            set
            {
                if (value.Equals(m_Axle2SpeedF004))
                {
                    return;
                }
                m_Axle2SpeedF004 = value;
                RaisePropertyChanged(() => Axle2SpeedF004);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle2SpeedF005
        {
            get { return m_Axle2SpeedF005; }
            set
            {
                if (value.Equals(m_Axle2SpeedF005))
                {
                    return;
                }
                m_Axle2SpeedF005 = value;
                RaisePropertyChanged(() => Axle2SpeedF005);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle2SpeedF006
        {
            get { return m_Axle2SpeedF006; }
            set
            {
                if (value.Equals(m_Axle2SpeedF006))
                {
                    return;
                }
                m_Axle2SpeedF006 = value;
                RaisePropertyChanged(() => Axle2SpeedF006);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle3SpeedF001
        {
            get { return m_Axle3SpeedF001; }
            set
            {
                if (value.Equals(m_Axle3SpeedF001))
                {
                    return;
                }
                m_Axle3SpeedF001 = value;
                RaisePropertyChanged(() => Axle3SpeedF001);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle3SpeedF002
        {
            get { return m_Axle3SpeedF002; }
            set
            {
                if (value.Equals(m_Axle3SpeedF002))
                {
                    return;
                }
                m_Axle3SpeedF002 = value;
                RaisePropertyChanged(() => Axle3SpeedF002);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle3SpeedF003
        {
            get { return m_Axle3SpeedF003; }
            set
            {
                if (value.Equals(m_Axle3SpeedF003))
                {
                    return;
                }
                m_Axle3SpeedF003 = value;
                RaisePropertyChanged(() => Axle3SpeedF003);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle3SpeedF004
        {
            get { return m_Axle3SpeedF004; }
            set
            {
                if (value.Equals(m_Axle3SpeedF004))
                {
                    return;
                }
                m_Axle3SpeedF004 = value;
                RaisePropertyChanged(() => Axle3SpeedF004);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle3SpeedF005
        {
            get { return m_Axle3SpeedF005; }
            set
            {
                if (value.Equals(m_Axle3SpeedF005))
                {
                    return;
                }
                m_Axle3SpeedF005 = value;
                RaisePropertyChanged(() => Axle3SpeedF005);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle3SpeedF006
        {
            get { return m_Axle3SpeedF006; }
            set
            {
                if (value.Equals(m_Axle3SpeedF006))
                {
                    return;
                }
                m_Axle3SpeedF006 = value;
                RaisePropertyChanged(() => Axle3SpeedF006);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle4SpeedF001
        {
            get { return m_Axle4SpeedF001; }
            set
            {
                if (value.Equals(m_Axle4SpeedF001))
                {
                    return;
                }
                m_Axle4SpeedF001 = value;
                RaisePropertyChanged(() => Axle4SpeedF001);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle4SpeedF002
        {
            get { return m_Axle4SpeedF002; }
            set
            {
                if (value.Equals(m_Axle4SpeedF002))
                {
                    return;
                }
                m_Axle4SpeedF002 = value;
                RaisePropertyChanged(() => Axle4SpeedF002);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle4SpeedF003
        {
            get { return m_Axle4SpeedF003; }
            set
            {
                if (value.Equals(m_Axle4SpeedF003))
                {
                    return;
                }
                m_Axle4SpeedF003 = value;
                RaisePropertyChanged(() => Axle4SpeedF003);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle4SpeedF004
        {
            get { return m_Axle4SpeedF004; }
            set
            {
                if (value.Equals(m_Axle4SpeedF004))
                {
                    return;
                }
                m_Axle4SpeedF004 = value;
                RaisePropertyChanged(() => Axle4SpeedF004);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle4SpeedF005
        {
            get { return m_Axle4SpeedF005; }
            set
            {
                if (value.Equals(m_Axle4SpeedF005))
                {
                    return;
                }
                m_Axle4SpeedF005 = value;
                RaisePropertyChanged(() => Axle4SpeedF005);
            }
        }

        /// <summary>
        /// 轴4旋转速度（KM/H）
        /// </summary>
        public double Axle4SpeedF006
        {
            get { return m_Axle4SpeedF006; }
            set
            {
                if (value.Equals(m_Axle4SpeedF006))
                {
                    return;
                }
                m_Axle4SpeedF006 = value;
                RaisePropertyChanged(() => Axle4SpeedF006);
            }
        }
    }
}
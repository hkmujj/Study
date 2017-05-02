using System.ComponentModel.Composition;
using Subway.WuHanLine6.Controller;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 制动自检Model
    /// </summary>
    [Export]
    public class BrakeAutoCheckModel : ModelBase
    {
        private bool m_BrakeIsolationF0062;
        private bool m_BrakeIsolationF0061;
        private bool m_BrakeIsolationF0052;
        private bool m_BrakeIsolationF0051;
        private bool m_BrakeIsolationF0042;
        private bool m_BrakeIsolationF0041;
        private bool m_BrakeIsolationF0032;
        private bool m_BrakeIsolationF0031;
        private bool m_BrakeIsolationF0022;
        private bool m_BrakeIsolationF0021;
        private bool m_BrakeIsolationF0012;
        private bool m_BrakeIsolationF0011;
        private double m_BrakeF0062;
        private double m_BrakeF0061;
        private double m_BrakeF0052;
        private double m_BrakeF0051;
        private double m_BrakeF0042;
        private double m_BrakeF0041;
        private double m_BrakeF0032;
        private double m_BrakeF0031;
        private double m_BrakeF0022;
        private double m_BrakeF0021;
        private double m_BrakeF0012;
        private double m_BrakeF0011;
        private double m_AirSpringF0062;
        private double m_AirSpringF0061;
        private double m_AirSpringF0052;
        private double m_AirSpringF0051;
        private double m_AirSpringF0042;
        private double m_AirSpringF0041;
        private double m_AirSpringF0032;
        private double m_AirSpringF0031;
        private double m_AirSpringF0022;
        private double m_AirSpringF0021;
        private double m_AirSpringF0012;
        private double m_AirSpringF0011;
        private bool m_IsAutoChecking;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controller"></param>
        [ImportingConstructor]
        public BrakeAutoCheckModel(BrakeAutoCheckController controller)
        {
            Controller = controller;
            controller.ViewModel = this;
        }

        /// <summary>
        /// 控制类
        /// </summary>
        public BrakeAutoCheckController Controller { get; private set; }

        /// <summary>
        /// 空气簧压力
        /// </summary>
        public double AirSpringF0011
        {
            get { return m_AirSpringF0011; }
            set
            {
                if (value.Equals(m_AirSpringF0011))
                {
                    return;
                }
                m_AirSpringF0011 = value;
                RaisePropertyChanged(() => AirSpringF0011);
            }
        }

        /// <summary>
        /// 空气簧压力
        /// </summary>
        public double AirSpringF0012
        {
            get { return m_AirSpringF0012; }
            set
            {
                if (value.Equals(m_AirSpringF0012))
                {
                    return;
                }
                m_AirSpringF0012 = value;
                RaisePropertyChanged(() => AirSpringF0012);
            }
        }

        /// <summary>
        /// 空气簧压力
        /// </summary>
        public double AirSpringF0021
        {
            get { return m_AirSpringF0021; }
            set
            {
                if (value.Equals(m_AirSpringF0021))
                {
                    return;
                }
                m_AirSpringF0021 = value;
                RaisePropertyChanged(() => AirSpringF0021);
            }
        }

        /// <summary>
        /// 空气簧压力
        /// </summary>
        public double AirSpringF0022
        {
            get { return m_AirSpringF0022; }
            set
            {
                if (value.Equals(m_AirSpringF0022))
                {
                    return;
                }
                m_AirSpringF0022 = value;
                RaisePropertyChanged(() => AirSpringF0022);
            }
        }

        /// <summary>
        /// 空气簧压力
        /// </summary>
        public double AirSpringF0031
        {
            get { return m_AirSpringF0031; }
            set
            {
                if (value.Equals(m_AirSpringF0031))
                {
                    return;
                }
                m_AirSpringF0031 = value;
                RaisePropertyChanged(() => AirSpringF0031);
            }
        }

        /// <summary>
        /// 空气簧压力
        /// </summary>
        public double AirSpringF0032
        {
            get { return m_AirSpringF0032; }
            set
            {
                if (value.Equals(m_AirSpringF0032))
                {
                    return;
                }
                m_AirSpringF0032 = value;
                RaisePropertyChanged(() => AirSpringF0032);
            }
        }

        /// <summary>
        /// 空气簧压力
        /// </summary>
        public double AirSpringF0041
        {
            get { return m_AirSpringF0041; }
            set
            {
                if (value.Equals(m_AirSpringF0041))
                {
                    return;
                }
                m_AirSpringF0041 = value;
                RaisePropertyChanged(() => AirSpringF0041);
            }
        }

        /// <summary>
        /// 空气簧压力
        /// </summary>
        public double AirSpringF0042
        {
            get { return m_AirSpringF0042; }
            set
            {
                if (value.Equals(m_AirSpringF0042))
                {
                    return;
                }
                m_AirSpringF0042 = value;
                RaisePropertyChanged(() => AirSpringF0042);
            }
        }

        /// <summary>
        /// 空气簧压力
        /// </summary>
        public double AirSpringF0051
        {
            get { return m_AirSpringF0051; }
            set
            {
                if (value.Equals(m_AirSpringF0051))
                {
                    return;
                }
                m_AirSpringF0051 = value;
                RaisePropertyChanged(() => AirSpringF0051);
            }
        }

        /// <summary>
        /// 空气簧压力
        /// </summary>
        public double AirSpringF0052
        {
            get { return m_AirSpringF0052; }
            set
            {
                if (value.Equals(m_AirSpringF0052))
                {
                    return;
                }
                m_AirSpringF0052 = value;
                RaisePropertyChanged(() => AirSpringF0052);
            }
        }

        /// <summary>
        /// 空气簧压力
        /// </summary>
        public double AirSpringF0061
        {
            get { return m_AirSpringF0061; }
            set
            {
                if (value.Equals(m_AirSpringF0061))
                {
                    return;
                }
                m_AirSpringF0061 = value;
                RaisePropertyChanged(() => AirSpringF0061);
            }
        }

        /// <summary>
        /// 空气簧压力
        /// </summary>
        public double AirSpringF0062
        {
            get { return m_AirSpringF0062; }
            set
            {
                if (value.Equals(m_AirSpringF0062))
                {
                    return;
                }
                m_AirSpringF0062 = value;
                RaisePropertyChanged(() => AirSpringF0062);
            }
        }

        /// <summary>
        /// 制动缸压力
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
        /// 制动缸压力
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
        /// 制动缸压力
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
        /// 制动缸压力
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
        /// 制动缸压力
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
        /// 制动缸压力
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
        /// 制动缸压力
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
        /// 制动缸压力
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
        /// 制动缸压力
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
        /// 制动缸压力
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
        /// 制动缸压力
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
        /// 制动缸压力
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
        /// 制动隔离
        /// </summary>
        public bool BrakeIsolationF0011
        {
            get { return m_BrakeIsolationF0011; }
            set
            {
                if (value == m_BrakeIsolationF0011)
                {
                    return;
                }
                m_BrakeIsolationF0011 = value;
                RaisePropertyChanged(() => BrakeIsolationF0011);
            }
        }

        /// <summary>
        /// 制动隔离
        /// </summary>
        public bool BrakeIsolationF0012
        {
            get { return m_BrakeIsolationF0012; }
            set
            {
                if (value == m_BrakeIsolationF0012)
                {
                    return;
                }
                m_BrakeIsolationF0012 = value;
                RaisePropertyChanged(() => BrakeIsolationF0012);
            }
        }

        /// <summary>
        /// 制动隔离
        /// </summary>
        public bool BrakeIsolationF0021
        {
            get { return m_BrakeIsolationF0021; }
            set
            {
                if (value == m_BrakeIsolationF0021)
                {
                    return;
                }
                m_BrakeIsolationF0021 = value;
                RaisePropertyChanged(() => BrakeIsolationF0021);
            }
        }

        /// <summary>
        /// 制动隔离
        /// </summary>
        public bool BrakeIsolationF0022
        {
            get { return m_BrakeIsolationF0022; }
            set
            {
                if (value == m_BrakeIsolationF0022)
                {
                    return;
                }
                m_BrakeIsolationF0022 = value;
                RaisePropertyChanged(() => BrakeIsolationF0022);
            }
        }

        /// <summary>
        /// 制动隔离
        /// </summary>
        public bool BrakeIsolationF0031
        {
            get { return m_BrakeIsolationF0031; }
            set
            {
                if (value == m_BrakeIsolationF0031)
                {
                    return;
                }
                m_BrakeIsolationF0031 = value;
                RaisePropertyChanged(() => BrakeIsolationF0031);
            }
        }

        /// <summary>
        /// 制动隔离
        /// </summary>
        public bool BrakeIsolationF0032
        {
            get { return m_BrakeIsolationF0032; }
            set
            {
                if (value == m_BrakeIsolationF0032)
                {
                    return;
                }
                m_BrakeIsolationF0032 = value;
                RaisePropertyChanged(() => BrakeIsolationF0032);
            }
        }

        /// <summary>
        /// 制动隔离
        /// </summary>
        public bool BrakeIsolationF0041
        {
            get { return m_BrakeIsolationF0041; }
            set
            {
                if (value == m_BrakeIsolationF0041)
                {
                    return;
                }
                m_BrakeIsolationF0041 = value;
                RaisePropertyChanged(() => BrakeIsolationF0041);
            }
        }

        /// <summary>
        /// 制动隔离
        /// </summary>
        public bool BrakeIsolationF0042
        {
            get { return m_BrakeIsolationF0042; }
            set
            {
                if (value == m_BrakeIsolationF0042)
                {
                    return;
                }
                m_BrakeIsolationF0042 = value;
                RaisePropertyChanged(() => BrakeIsolationF0042);
            }
        }

        /// <summary>
        /// 制动隔离
        /// </summary>
        public bool BrakeIsolationF0051
        {
            get { return m_BrakeIsolationF0051; }
            set
            {
                if (value == m_BrakeIsolationF0051)
                {
                    return;
                }
                m_BrakeIsolationF0051 = value;
                RaisePropertyChanged(() => BrakeIsolationF0051);
            }
        }

        /// <summary>
        /// 制动隔离
        /// </summary>
        public bool BrakeIsolationF0052
        {
            get { return m_BrakeIsolationF0052; }
            set
            {
                if (value == m_BrakeIsolationF0052)
                {
                    return;
                }
                m_BrakeIsolationF0052 = value;
                RaisePropertyChanged(() => BrakeIsolationF0052);
            }
        }

        /// <summary>
        /// 制动隔离
        /// </summary>
        public bool BrakeIsolationF0061
        {
            get { return m_BrakeIsolationF0061; }
            set
            {
                if (value == m_BrakeIsolationF0061)
                {
                    return;
                }
                m_BrakeIsolationF0061 = value;
                RaisePropertyChanged(() => BrakeIsolationF0061);
            }
        }

        /// <summary>
        /// 制动隔离
        /// </summary>
        public bool BrakeIsolationF0062
        {
            get { return m_BrakeIsolationF0062; }
            set
            {
                if (value == m_BrakeIsolationF0062)
                {
                    return;
                }
                m_BrakeIsolationF0062 = value;
                RaisePropertyChanged(() => BrakeIsolationF0062);
            }
        }

        /// <summary>
        /// 是否自动自检中
        /// </summary>
        public bool IsAutoChecking
        {
            get { return m_IsAutoChecking; }
            set
            {
                if (value == m_IsAutoChecking)
                {
                    return;
                }
                m_IsAutoChecking = value;
                Controller.AutoCheckCommand.RaiseCanExecuteChanged();
                Controller.AutoCheckEnd.RaiseCanExecuteChanged();
                RaisePropertyChanged(() => IsAutoChecking);
            }
        }
    }
}
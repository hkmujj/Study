using System.ComponentModel.Composition;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    ///     旁路Model
    /// </summary>
    [Export]
    public class BypassModel : ModelBase
    {
        private bool m_ATCCutF001;
        private bool m_ATCCutF006;
        private bool m_BrakeF001;
        private bool m_BrakeF006;
        private bool m_DoorF001;
        private bool m_DoorF006;
        private bool m_DriveDoorF001;
        private bool m_DriveDoorF006;
        private bool m_PantographF001;
        private bool m_PantographF006;
        private bool m_ParkingF001;
        private bool m_ParkingF006;
        private bool m_TotalFanF001;
        private bool m_TotalFanF006;
        private bool m_VigilantF001;
        private bool m_VigilantF006;
        private bool m_ZeroF001;
        private bool m_ZeroF006;

        /// <summary>
        ///     门零速旁路
        /// </summary>
        public bool ZeroF001
        {
            get { return m_ZeroF001; }
            set
            {
                if (value == m_ZeroF001)
                {
                    return;
                }
                m_ZeroF001 = value;
                RaisePropertyChanged(() => ZeroF001);
            }
        }

        /// <summary>
        ///     门零速旁路
        /// </summary>
        public bool ZeroF006
        {
            get { return m_ZeroF006; }
            set
            {
                if (value == m_ZeroF006)
                {
                    return;
                }
                m_ZeroF006 = value;
                RaisePropertyChanged(() => ZeroF006);
            }
        }

        /// <summary>
        ///     司机室门旁路
        /// </summary>
        public bool DriveDoorF001
        {
            get { return m_DriveDoorF001; }
            set
            {
                if (value == m_DriveDoorF001)
                {
                    return;
                }
                m_DriveDoorF001 = value;
                RaisePropertyChanged(() => DriveDoorF001);
            }
        }

        /// <summary>
        ///     司机室门旁路
        /// </summary>
        public bool DriveDoorF006
        {
            get { return m_DriveDoorF006; }
            set
            {
                if (value == m_DriveDoorF006)
                {
                    return;
                }
                m_DriveDoorF006 = value;
                RaisePropertyChanged(() => DriveDoorF006);
            }
        }

        /// <summary>
        ///     总风压力可用旁路
        /// </summary>
        public bool TotalFanF001
        {
            get { return m_TotalFanF001; }
            set
            {
                if (value == m_TotalFanF001)
                {
                    return;
                }
                m_TotalFanF001 = value;
                RaisePropertyChanged(() => TotalFanF001);
            }
        }

        /// <summary>
        ///     总风压力可用旁路
        /// </summary>
        public bool TotalFanF006
        {
            get { return m_TotalFanF006; }
            set
            {
                if (value == m_TotalFanF006)
                {
                    return;
                }
                m_TotalFanF006 = value;
                RaisePropertyChanged(() => TotalFanF006);
            }
        }

        /// <summary>
        ///     允许升弓旁路
        /// </summary>
        public bool PantographF001
        {
            get { return m_PantographF001; }
            set
            {
                if (value == m_PantographF001)
                {
                    return;
                }
                m_PantographF001 = value;
                RaisePropertyChanged(() => PantographF001);
            }
        }

        /// <summary>
        ///     允许升弓旁路
        /// </summary>
        public bool PantographF006
        {
            get { return m_PantographF006; }
            set
            {
                if (value == m_PantographF006)
                {
                    return;
                }
                m_PantographF006 = value;
                RaisePropertyChanged(() => PantographF006);
            }
        }

        /// <summary>
        ///     警惕按钮旁路
        /// </summary>
        public bool VigilantF001
        {
            get { return m_VigilantF001; }
            set
            {
                if (value == m_VigilantF001)
                {
                    return;
                }
                m_VigilantF001 = value;
                RaisePropertyChanged(() => VigilantF001);
            }
        }

        /// <summary>
        ///     警惕按钮旁路
        /// </summary>
        public bool VigilantF006
        {
            get { return m_VigilantF006; }
            set
            {
                if (value == m_VigilantF006)
                {
                    return;
                }
                m_VigilantF006 = value;
                RaisePropertyChanged(() => VigilantF006);
            }
        }

        /// <summary>
        ///     门关好旁路
        /// </summary>
        public bool DoorF001
        {
            get { return m_DoorF001; }
            set
            {
                if (value == m_DoorF001)
                {
                    return;
                }
                m_DoorF001 = value;
                RaisePropertyChanged(() => DoorF001);
            }
        }

        /// <summary>
        ///     门关好旁路
        /// </summary>
        public bool DoorF006
        {
            get { return m_DoorF006; }
            set
            {
                if (value == m_DoorF006)
                {
                    return;
                }
                m_DoorF006 = value;
                RaisePropertyChanged(() => DoorF006);
            }
        }

        /// <summary>
        ///     停放制动旁路
        /// </summary>
        public bool ParkingF001
        {
            get { return m_ParkingF001; }
            set
            {
                if (value == m_ParkingF001)
                {
                    return;
                }
                m_ParkingF001 = value;
                RaisePropertyChanged(() => ParkingF001);
            }
        }

        /// <summary>
        ///     停放制动旁路
        /// </summary>
        public bool ParkingF006
        {
            get { return m_ParkingF006; }
            set
            {
                if (value == m_ParkingF006)
                {
                    return;
                }
                m_ParkingF006 = value;
                RaisePropertyChanged(() => ParkingF006);
            }
        }

        /// <summary>
        ///     ATC切除
        /// </summary>
        public bool ATCCutF001
        {
            get { return m_ATCCutF001; }
            set
            {
                if (value == m_ATCCutF001)
                {
                    return;
                }
                m_ATCCutF001 = value;
                RaisePropertyChanged(() => ATCCutF001);
            }
        }

        /// <summary>
        ///     ATC切除
        /// </summary>
        public bool ATCCutF006
        {
            get { return m_ATCCutF006; }
            set
            {
                if (value == m_ATCCutF006)
                {
                    return;
                }
                m_ATCCutF006 = value;
                RaisePropertyChanged(() => ATCCutF006);
            }
        }

        /// <summary>
        ///     所有制动缓解旁路
        /// </summary>
        public bool BrakeF001
        {
            get { return m_BrakeF001; }
            set
            {
                if (value == m_BrakeF001)
                {
                    return;
                }
                m_BrakeF001 = value;
                RaisePropertyChanged(() => BrakeF001);
            }
        }

        /// <summary>
        ///     所有制动缓解旁路
        /// </summary>
        public bool BrakeF006
        {
            get { return m_BrakeF006; }
            set
            {
                if (value == m_BrakeF006)
                {
                    return;
                }
                m_BrakeF006 = value;
                RaisePropertyChanged(() => BrakeF006);
            }
        }
    }
}
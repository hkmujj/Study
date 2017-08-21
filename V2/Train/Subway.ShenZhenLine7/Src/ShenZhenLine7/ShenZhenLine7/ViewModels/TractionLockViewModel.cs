using System.ComponentModel.Composition;
using Subway.ShenZhenLine7.Controller.ViewModelController;
using Subway.ShenZhenLine7.Extention;
using Subway.ShenZhenLine7.Interfaces;

namespace Subway.ShenZhenLine7.ViewModels
{
    /// <summary>
    /// 牵引封锁
    /// </summary>
    [Export]
    [Export(typeof(ICourceStatusChanged))]
    public class TractionLockViewModel : ViewModelBase
    {
        private bool m_TrainRemove;
        private bool m_Car6VigilantButtonUp;
        private bool m_Car1VigilantButtonUp;
        private bool m_Car6EmergencyBrakeInfiction;
        private bool m_Car1EmergencyBrakeInfiction;
        private bool m_Car6EmergencyParking2;
        private bool m_Car6EmergencyParking1;
        private bool m_Car1EmergencyParking2;
        private bool m_Car1EmergencyParking1;
        private bool m_Car6MasterFan;
        private bool m_Car1MasterFan;
        private bool m_Car6ParkingBrakeUnRemit;
        private bool m_Car5ParkingBrakeUnRemit;
        private bool m_Car4ParkingBrakeUnRemit;
        private bool m_Car3ParkingBrakeUnRemit;
        private bool m_Car2ParkingBrakeUnRemit;
        private bool m_Car1ParkingBrakeUnRemit;
        private bool m_Car6RightUnClose;
        private bool m_Car5RightUnClose;
        private bool m_Car4RightUnClose;
        private bool m_Car3RightUnClose;
        private bool m_Car2RightUnClose;
        private bool m_Car1RightUnClose;
        private bool m_Car6LeftUnClose;
        private bool m_Car5LeftUnClose;
        private bool m_Car4LeftUnClose;
        private bool m_Car3LeftUnClose;
        private bool m_Car2LeftUnClose;
        private bool m_Car1LeftUnClose;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controller"></param>
        [ImportingConstructor]
        public TractionLockViewModel(TractionLockController controller)
        {
            controller.ViewModel = this;
            Controller = controller;
        }

        /// <summary>
        /// 清除运行数据
        /// </summary>
        public override void Clear()
        {
            this.Cast(false);
        }
        /// <summary>
        /// 控制器
        /// </summary>
        public TractionLockController Controller { get; private set; }
        /// <summary>
        /// 左侧门未关好
        /// </summary>
        public bool Car1LeftUnClose
        {
            get { return m_Car1LeftUnClose; }
            set
            {
                if (value == m_Car1LeftUnClose)
                {
                    return;
                }
                m_Car1LeftUnClose = value;
                RaisePropertyChanged(() => Car1LeftUnClose);
            }
        }

        /// <summary>
        /// 左侧门未关好
        /// </summary>
        public bool Car2LeftUnClose
        {
            get { return m_Car2LeftUnClose; }
            set
            {
                if (value == m_Car2LeftUnClose)
                {
                    return;
                }
                m_Car2LeftUnClose = value;
                RaisePropertyChanged(() => Car2LeftUnClose);
            }
        }

        /// <summary>
        /// 左侧门未关好
        /// </summary>
        public bool Car3LeftUnClose
        {
            get { return m_Car3LeftUnClose; }
            set
            {
                if (value == m_Car3LeftUnClose)
                {
                    return;
                }
                m_Car3LeftUnClose = value;
                RaisePropertyChanged(() => Car3LeftUnClose);
            }
        }

        /// <summary>
        /// 左侧门未关好
        /// </summary>
        public bool Car4LeftUnClose
        {
            get { return m_Car4LeftUnClose; }
            set
            {
                if (value == m_Car4LeftUnClose)
                {
                    return;
                }
                m_Car4LeftUnClose = value;
                RaisePropertyChanged(() => Car4LeftUnClose);
            }
        }

        /// <summary>
        /// 左侧门未关好
        /// </summary>
        public bool Car5LeftUnClose
        {
            get { return m_Car5LeftUnClose; }
            set
            {
                if (value == m_Car5LeftUnClose)
                {
                    return;
                }
                m_Car5LeftUnClose = value;
                RaisePropertyChanged(() => Car5LeftUnClose);
            }
        }

        /// <summary>
        /// 左侧门未关好
        /// </summary>
        public bool Car6LeftUnClose
        {
            get { return m_Car6LeftUnClose; }
            set
            {
                if (value == m_Car6LeftUnClose)
                {
                    return;
                }
                m_Car6LeftUnClose = value;
                RaisePropertyChanged(() => Car6LeftUnClose);
            }
        }

        /// <summary>   
        /// 右侧门未关好
        /// </summary>
        public bool Car1RightUnClose
        {
            get { return m_Car1RightUnClose; }
            set
            {
                if (value == m_Car1RightUnClose)
                {
                    return;
                }
                m_Car1RightUnClose = value;
                RaisePropertyChanged(() => Car1RightUnClose);
            }
        }

        /// <summary>
        /// 右侧门未关好
        /// </summary>
        public bool Car2RightUnClose
        {
            get { return m_Car2RightUnClose; }
            set
            {
                if (value == m_Car2RightUnClose)
                {
                    return;
                }
                m_Car2RightUnClose = value;
                RaisePropertyChanged(() => Car2RightUnClose);
            }
        }

        /// <summary>
        /// 右侧门未关好
        /// </summary>
        public bool Car3RightUnClose
        {
            get { return m_Car3RightUnClose; }
            set
            {
                if (value == m_Car3RightUnClose)
                {
                    return;
                }
                m_Car3RightUnClose = value;
                RaisePropertyChanged(() => Car3RightUnClose);
            }
        }

        /// <summary>
        /// 右侧门未关好
        /// </summary>
        public bool Car4RightUnClose
        {
            get { return m_Car4RightUnClose; }
            set
            {
                if (value == m_Car4RightUnClose)
                {
                    return;
                }
                m_Car4RightUnClose = value;
                RaisePropertyChanged(() => Car4RightUnClose);
            }
        }

        /// <summary>
        /// 右侧门未关好
        /// </summary>
        public bool Car5RightUnClose
        {
            get { return m_Car5RightUnClose; }
            set
            {
                if (value == m_Car5RightUnClose)
                {
                    return;
                }
                m_Car5RightUnClose = value;
                RaisePropertyChanged(() => Car5RightUnClose);
            }
        }

        /// <summary>
        /// 右侧门未关好
        /// </summary>
        public bool Car6RightUnClose
        {
            get { return m_Car6RightUnClose; }
            set
            {
                if (value == m_Car6RightUnClose)
                {
                    return;
                }
                m_Car6RightUnClose = value;
                RaisePropertyChanged(() => Car6RightUnClose);
            }
        }

        /// <summary>
        /// 停放制动未缓解
        /// </summary>
        public bool Car1ParkingBrakeUnRemit
        {
            get { return m_Car1ParkingBrakeUnRemit; }
            set
            {
                if (value == m_Car1ParkingBrakeUnRemit)
                {
                    return;
                }
                m_Car1ParkingBrakeUnRemit = value;
                RaisePropertyChanged(() => Car1ParkingBrakeUnRemit);
            }
        }

        /// <summary>
        /// 停放制动未缓解
        /// </summary>
        public bool Car2ParkingBrakeUnRemit
        {
            get { return m_Car2ParkingBrakeUnRemit; }
            set
            {
                if (value == m_Car2ParkingBrakeUnRemit)
                {
                    return;
                }
                m_Car2ParkingBrakeUnRemit = value;
                RaisePropertyChanged(() => Car2ParkingBrakeUnRemit);
            }
        }

        /// <summary>
        /// 停放制动未缓解
        /// </summary>
        public bool Car3ParkingBrakeUnRemit
        {
            get { return m_Car3ParkingBrakeUnRemit; }
            set
            {
                if (value == m_Car3ParkingBrakeUnRemit)
                {
                    return;
                }
                m_Car3ParkingBrakeUnRemit = value;
                RaisePropertyChanged(() => Car3ParkingBrakeUnRemit);
            }
        }

        /// <summary>
        /// 停放制动未缓解
        /// </summary>
        public bool Car4ParkingBrakeUnRemit
        {
            get { return m_Car4ParkingBrakeUnRemit; }
            set
            {
                if (value == m_Car4ParkingBrakeUnRemit)
                {
                    return;
                }
                m_Car4ParkingBrakeUnRemit = value;
                RaisePropertyChanged(() => Car4ParkingBrakeUnRemit);
            }
        }

        /// <summary>
        /// 停放制动未缓解
        /// </summary>
        public bool Car5ParkingBrakeUnRemit
        {
            get { return m_Car5ParkingBrakeUnRemit; }
            set
            {
                if (value == m_Car5ParkingBrakeUnRemit)
                {
                    return;
                }
                m_Car5ParkingBrakeUnRemit = value;
                RaisePropertyChanged(() => Car5ParkingBrakeUnRemit);
            }
        }

        /// <summary>
        /// 停放制动未缓解
        /// </summary>
        public bool Car6ParkingBrakeUnRemit
        {
            get { return m_Car6ParkingBrakeUnRemit; }
            set
            {
                if (value == m_Car6ParkingBrakeUnRemit)
                {
                    return;
                }
                m_Car6ParkingBrakeUnRemit = value;
                RaisePropertyChanged(() => Car6ParkingBrakeUnRemit);
            }
        }

        /// <summary>
        /// 主风管压力不足
        /// </summary>
        public bool Car1MasterFan
        {
            get { return m_Car1MasterFan; }
            set
            {
                if (value == m_Car1MasterFan)
                {
                    return;
                }
                m_Car1MasterFan = value;
                RaisePropertyChanged(() => Car1MasterFan);
            }
        }

        /// <summary>
        /// 主风管压力不足
        /// </summary>
        public bool Car6MasterFan
        {
            get { return m_Car6MasterFan; }
            set
            {
                if (value == m_Car6MasterFan)
                {
                    return;
                }
                m_Car6MasterFan = value;
                RaisePropertyChanged(() => Car6MasterFan);
            }
        }

        /// <summary>
        /// 紧急停车蘑菇拍
        /// </summary>
        public bool Car1EmergencyParking1
        {
            get { return m_Car1EmergencyParking1; }
            set
            {
                if (value == m_Car1EmergencyParking1)
                {
                    return;
                }
                m_Car1EmergencyParking1 = value;
                RaisePropertyChanged(() => Car1EmergencyParking1);
            }
        }

        /// <summary>
        /// 紧急停车蘑菇拍
        /// </summary>
        public bool Car1EmergencyParking2
        {
            get { return m_Car1EmergencyParking2; }
            set
            {
                if (value == m_Car1EmergencyParking2)
                {
                    return;
                }
                m_Car1EmergencyParking2 = value;
                RaisePropertyChanged(() => Car1EmergencyParking2);
            }
        }

        /// <summary>
        /// 紧急停车蘑菇拍
        /// </summary>
        public bool Car6EmergencyParking1
        {
            get { return m_Car6EmergencyParking1; }
            set
            {
                if (value == m_Car6EmergencyParking1)
                {
                    return;
                }
                m_Car6EmergencyParking1 = value;
                RaisePropertyChanged(() => Car6EmergencyParking1);
            }
        }

        /// <summary>
        /// 紧急停车蘑菇拍
        /// </summary>
        public bool Car6EmergencyParking2
        {
            get { return m_Car6EmergencyParking2; }
            set
            {
                if (value == m_Car6EmergencyParking2)
                {
                    return;
                }
                m_Car6EmergencyParking2 = value;
                RaisePropertyChanged(() => Car6EmergencyParking2);
            }
        }

        /// <summary>
        /// 紧急制动施加
        /// </summary>
        public bool Car1EmergencyBrakeInfiction
        {
            get { return m_Car1EmergencyBrakeInfiction; }
            set
            {
                if (value == m_Car1EmergencyBrakeInfiction)
                {
                    return;
                }
                m_Car1EmergencyBrakeInfiction = value;
                RaisePropertyChanged(() => Car1EmergencyBrakeInfiction);
            }
        }

        /// <summary>
        /// 紧急制动施加
        /// </summary>
        public bool Car6EmergencyBrakeInfiction
        {
            get { return m_Car6EmergencyBrakeInfiction; }
            set
            {
                if (value == m_Car6EmergencyBrakeInfiction)
                {
                    return;
                }
                m_Car6EmergencyBrakeInfiction = value;
                RaisePropertyChanged(() => Car6EmergencyBrakeInfiction);
            }
        }

        /// <summary>
        /// 警惕按钮松开
        /// </summary>
        public bool Car1VigilantButtonUp
        {
            get { return m_Car1VigilantButtonUp; }
            set
            {
                if (value == m_Car1VigilantButtonUp)
                {
                    return;
                }
                m_Car1VigilantButtonUp = value;
                RaisePropertyChanged(() => Car1VigilantButtonUp);
            }
        }

        /// <summary>
        /// 警惕按钮松开
        /// </summary>
        public bool Car6VigilantButtonUp
        {
            get { return m_Car6VigilantButtonUp; }
            set
            {
                if (value == m_Car6VigilantButtonUp)
                {
                    return;
                }
                m_Car6VigilantButtonUp = value;
                RaisePropertyChanged(() => Car6VigilantButtonUp);
            }
        }

        /// <summary>
        /// 列车移动时制动不缓解
        /// </summary>
        public bool TrainRemove
        {
            get { return m_TrainRemove; }
            set
            {
                if (value == m_TrainRemove)
                {
                    return;
                }
                m_TrainRemove = value;
                RaisePropertyChanged(() => TrainRemove);
            }
        }
    }
}
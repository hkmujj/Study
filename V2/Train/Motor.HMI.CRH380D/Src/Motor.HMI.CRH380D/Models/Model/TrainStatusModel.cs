using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using LightRail.HMI.SZLHLF.Model;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class TrainStatusModel : ModelBase
    {
        private float m_TractionValue;
        private float m_BreakValue;
        private float m_ElectronFlow;
        private float m_LimitSpeed;
        private bool m_EmergencyLoop;
        private bool m_SafeBreakLoop;
        private bool m_ParkBreak;
        private bool m_StopTraction;
        private bool m_LCB;
        private bool m_PrimaryAirCylinder;
        private bool m_AvoidSlip;
        private bool m_RescueLoopBack;
        private bool m_OpenAllDoor;

        [ImportingConstructor]
        public TrainStatusModel()
        {

        }

        /// <summary>
        /// 牵引力
        /// </summary>
        public float TractionValue
        {
            get { return m_TractionValue; }
            set {
                if (value == m_TractionValue)
                {
                    return;
                }
                m_TractionValue = value;
                RaisePropertyChanged(() => TractionValue);
            }
        }

        /// <summary>
        /// 制动力
        /// </summary>
        public float BreakValue
        {
            get { return m_BreakValue; }
            set
            {
                if (value == m_BreakValue)
                {
                    return;
                }
                m_BreakValue = value;
                RaisePropertyChanged(() => BreakValue);
            }
        }

        /// <summary>
        /// 网侧电流
        /// </summary>
        public float ElectronFlow
        {
            get { return m_ElectronFlow; }
            set
            {
                if (value == m_ElectronFlow)
                {
                    return;
                }
                m_ElectronFlow = value;
                RaisePropertyChanged(() => ElectronFlow);
            }
        }

        /// <summary>
        /// 限速
        /// </summary>
        public float LimitSpeed
        {
            get { return m_LimitSpeed; }
            set
            {
                if (value == m_LimitSpeed)
                {
                    return;
                }
                m_LimitSpeed = value;
                RaisePropertyChanged(() => LimitSpeed);
            }
        }

        /// <summary>
        /// 紧急制动回路
        /// </summary>
        public bool EmergencyLoop
        {
            get { return m_EmergencyLoop; }
            set
            {
                if (value == m_EmergencyLoop)
                {
                    return;
                }
                m_EmergencyLoop = value;
                RaisePropertyChanged(() => EmergencyLoop);
            }
        }

        /// <summary>
        /// 安全制动回路
        /// </summary>
        public bool SafeBreakLoop
        {
            get { return m_SafeBreakLoop; }
            set
            {
                if (value == m_SafeBreakLoop)
                {
                    return;
                }
                m_SafeBreakLoop = value;
                RaisePropertyChanged(() => SafeBreakLoop);
            }
        }

        /// <summary>
        /// 施加停放制动
        /// </summary>
        public bool ParkBreak
        {
            get { return m_ParkBreak; }
            set
            {
                if (value == m_ParkBreak)
                {
                    return;
                }
                m_ParkBreak = value;
                RaisePropertyChanged(() => ParkBreak);
            }
        }

        /// <summary>
        /// 牵引阻断
        /// </summary>
        public bool StopTraction
        {
            get { return m_StopTraction; }
            set
            {
                if (value == m_StopTraction)
                {
                    return;
                }
                m_StopTraction = value;
                RaisePropertyChanged(() => StopTraction);
            }
        }

        /// <summary>
        /// 主断路器
        /// </summary>
        public bool LCB
        {
            get { return m_LCB; }
            set
            {
                if (value == m_LCB)
                {
                    return;
                }
                m_LCB = value;
                RaisePropertyChanged(() => LCB);
            }
        }

        /// <summary>
        /// 主风缸压力
        /// </summary>
        public bool PrimaryAirCylinder
        {
            get { return m_PrimaryAirCylinder; }
            set
            {
                if (value == m_PrimaryAirCylinder)
                {
                    return;
                }
                m_PrimaryAirCylinder = value;
                RaisePropertyChanged(() => PrimaryAirCylinder);
            }
        }

        /// <summary>
        /// 防滑保护
        /// </summary>
        public bool AvoidSlip
        {
            get { return m_AvoidSlip; }
            set
            {
                if (value == m_AvoidSlip)
                {
                    return;
                }
                m_AvoidSlip = value;
                RaisePropertyChanged(() => AvoidSlip);
            }
        }

        /// <summary>
        /// 救援/回送
        /// </summary>
        public bool RescueLoopBack
        {
            get { return m_RescueLoopBack; }
            set
            {
                if (value == m_RescueLoopBack)
                {
                    return;
                }
                m_RescueLoopBack = value;
                RaisePropertyChanged(() => RescueLoopBack);
            }
        }
        
        /// <summary>
        /// 车门全部关闭
        /// </summary>
        public bool OpenAllDoor
        {
            get { return m_OpenAllDoor; }
            set
            {
                if (value == m_OpenAllDoor)
                {
                    return;
                }
                m_OpenAllDoor = value;
                RaisePropertyChanged(() => OpenAllDoor);
            }
        }
        
    }
}
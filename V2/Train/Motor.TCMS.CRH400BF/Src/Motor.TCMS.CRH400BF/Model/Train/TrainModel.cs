using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Motor.TCMS.CRH400BF.Model.Constant;
using Motor.TCMS.CRH400BF.Model.Train.CartPart;

namespace Motor.TCMS.CRH400BF.Model.Train
{
    [Export]
    public class TrainModel : NotificationObject
    {

        private List<Car> m_CarCollection;
        private float m_Speed;
        private float m_BrakeLevelValue;
        private float m_TouchNetA;
        private double m_TouchNetV;
        private double m_TouchNetV1;
        private float m_WindPipeValue;
        private AtpLevelState m_AtpLevel;
        private DirectionState m_DirectionState;
        private bool m_FirstDriverRoom;
        private bool m_EighthDriverRoom;
        private TrainFaultState m_TrainFaultState;

        private TrainWorkConditionState m_TrainWorkConditionState;
        private bool m_LeftDoorUnRelease;
        private bool m_RightDoorUnRelease;
        private float m_SettingSpeed;
        private float m_BrakeLevel;
        private float m_TractionLevel;
        private bool m_Level;
        private bool m_ConstantSpeed;
        private string m_DisplayUnit;
        private string m_DisplayValue;
        private string m_DisplayModel;

        public TrainStateIcon TrainStateIcon { get; set; }

        [ImportingConstructor]
        public TrainModel(OtherModel otherModel)
        {
            OtherModel = otherModel;
        }
        public List<Car> CarCollection
        {
            get { return m_CarCollection; }
            set
            {
                if (Equals(value, m_CarCollection))
                {
                    return;
                }

                m_CarCollection = value;
                RaisePropertyChanged(() => CarCollection);
            }
        }
        public TrainWorkConditionState TrainWorkConditionState
        {
            get { return m_TrainWorkConditionState; }
            set
            {
                if (value == m_TrainWorkConditionState)
                {
                    return;
                }

                m_TrainWorkConditionState = value;
                ChangingDisplay();
                RaisePropertyChanged(() => TrainWorkConditionState);
            }
        }
        //速度值
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
        //制动级别
        public float BrakeLevelValue
        {
            get { return m_BrakeLevelValue; }
            set
            {
                if (value.Equals(m_BrakeLevelValue))
                {
                    return;
                }

                m_BrakeLevelValue = value;
                RaisePropertyChanged(() => BrakeLevelValue);
            }
        }
        //public TowPage TowPage { get; set; }

        //public BrakePage BrakePage { get; set; }
        public OtherModel OtherModel { get; private set; }
        /// 网压
        /// </summary>
        public double TouchNetV
        {
            get { return m_TouchNetV; }
            set
            {
                if (value.Equals(m_TouchNetV))
                {
                    return;
                }
                m_TouchNetV = value;
                RaisePropertyChanged(() => TouchNetV);
            }
        }

        public double TouchNetV1
        {
            get { return m_TouchNetV1; }
            set
            {
                if (value.Equals(m_TouchNetV1))
                {
                    return;
                }

                m_TouchNetV1 = value;
                RaisePropertyChanged(() => TouchNetV1);
            }
        }

        /// <summary>
        /// 网流
        /// </summary>
        public float TouchNetA
        {
            get { return m_TouchNetA; }
            set
            {
                if (value.Equals(m_TouchNetA))
                {
                    return;
                }

                m_TouchNetA = value;
                RaisePropertyChanged(() => TouchNetA);
            }
        }
        public float WindPipeValue
        {
            get { return m_WindPipeValue; }
            set
            {
                if (value.Equals(m_WindPipeValue))
                {
                    return;
                }

                m_WindPipeValue = value;
                RaisePropertyChanged(() => WindPipeValue);
            }
        }
        public AtpLevelState AtpLevel
        {
            get { return m_AtpLevel; }
            set
            {
                if (value == m_AtpLevel)
                {
                    return;
                }

                m_AtpLevel = value;
                RaisePropertyChanged(() => AtpLevel);
            }
        }
        public TrainFaultState TrainFaultState
        {
            get { return m_TrainFaultState; }
            set
            {
                if (value == m_TrainFaultState)
                {
                    return;
                }

                m_TrainFaultState = value;
                RaisePropertyChanged(() => TrainFaultState);
            }
        }

        public DirectionState DirectionState
        {
            get { return m_DirectionState; }
            set
            {
                if (value == m_DirectionState)
                {
                    return;
                }

                m_DirectionState = value;
                RaisePropertyChanged(() => DirectionState);
            }
        }

        public bool FirstDriverRoom
        {
            get { return m_FirstDriverRoom; }
            set
            {
                if (value == m_FirstDriverRoom)
                {
                    return;
                }

                m_FirstDriverRoom = value;
                RaisePropertyChanged(() => FirstDriverRoom);
            }
        }

        public bool EighthDriverRoom
        {
            get { return m_EighthDriverRoom; }
            set
            {
                if (value == m_EighthDriverRoom)
                {
                    return;
                }

                m_EighthDriverRoom = value;
                RaisePropertyChanged(() => EighthDriverRoom);
            }
        }
        /// <summary>
        /// 左侧车门未释放
        /// </summary>
        public bool LeftDoorUnRelease
        {
            get { return m_LeftDoorUnRelease; }
            set
            {
                if (value == m_LeftDoorUnRelease)
                    return;

                m_LeftDoorUnRelease = value;
                RaisePropertyChanged(() => LeftDoorUnRelease);
            }
        }

        /// <summary>
        /// 右侧车门未释放
        /// </summary>
        public bool RightDoorUnRelease
        {
            get { return m_RightDoorUnRelease; }
            set
            {
                if (value == m_RightDoorUnRelease)
                    return;

                m_RightDoorUnRelease = value;
                RaisePropertyChanged(() => RightDoorUnRelease);
            }
        }

        /// <summary>
        /// 恒速模式
        /// </summary>
        public bool ConstantSpeed
        {
            get { return m_ConstantSpeed; }
            set
            {
                if (value == m_ConstantSpeed)
                    return;

                m_ConstantSpeed = value;
                ChangingDisplay();
                RaisePropertyChanged(() => ConstantSpeed);
            }
        }

        /// <summary>
        /// 级位模式
        /// </summary>
        public bool Level
        {
            get { return m_Level; }
            set
            {
                if (value == m_Level)
                    return;

                m_Level = value;
                ChangingDisplay();
                RaisePropertyChanged(() => Level);
            }
        }

        /// <summary>
        /// 牵引级位
        /// </summary>
        public float TractionLevel
        {
            get { return m_TractionLevel; }
            set
            {
                if (value.Equals(m_TractionLevel))
                    return;

                m_TractionLevel = value;
                ChangingDisplay();
                RaisePropertyChanged(() => TractionLevel);
            }
        }

        /// <summary>
        /// 制动级位
        /// </summary>
        public float BrakeLevel
        {
            get { return m_BrakeLevel; }
            set
            {
                if (value.Equals(m_BrakeLevel))
                    return;

                m_BrakeLevel = value;
                ChangingDisplay();
                RaisePropertyChanged(() => BrakeLevel);
            }
        }

        /// <summary>
        /// 设定速度
        /// </summary>
        public float SettingSpeed
        {
            get { return m_SettingSpeed; }
            set
            {
                if (value.Equals(m_SettingSpeed))
                    return;

                m_SettingSpeed = value;
                ChangingDisplay();
                RaisePropertyChanged(() => SettingSpeed);
            }
        }

        private void ChangingDisplay()
        {
            if (TrainWorkConditionState == TrainWorkConditionState.Brake)
            {
                DisplayValue = BrakeLevel.ToString("F0");
                DisplayModel = "制动级位";
                DisplayUnit = "级";
                return;
            }

            if (ConstantSpeed)
            {
                DisplayValue = SettingSpeed.ToString("F0");
                DisplayUnit = "km/h";
                if (SettingSpeed > float.Epsilon)
                {
                    DisplayModel = "速度控制";
                }
                else
                {
                    DisplayModel = "速度模式";
                }
            }
            if (Level)
            {
                DisplayValue = TrainWorkConditionState == TrainWorkConditionState.Brake ? BrakeLevel.ToString("F0") : TractionLevel.ToString("F0");
                DisplayModel = TrainWorkConditionState == TrainWorkConditionState.Brake ? "制动级位" : "牵引级位";
                DisplayUnit = "级";
            }
        }
        /// <summary>
        /// 显示模式
        /// </summary>
        public string DisplayModel
        {
            get { return m_DisplayModel; }
            set
            {
                if (value == m_DisplayModel)
                    return;

                m_DisplayModel = value;
                RaisePropertyChanged(() => DisplayModel);
            }
        }

        /// <summary>
        /// 显示值
        /// </summary>
        public string DisplayValue
        {
            get { return m_DisplayValue; }
            set
            {
                if (value == m_DisplayValue)
                    return;

                m_DisplayValue = value;
                RaisePropertyChanged(() => DisplayValue);
            }
        }

        /// <summary>
        /// 显示单位
        /// </summary>
        public string DisplayUnit
        {
            get { return m_DisplayUnit; }
            set
            {
                if (value == m_DisplayUnit)
                    return;

                m_DisplayUnit = value;
                RaisePropertyChanged(() => DisplayUnit);
            }
        }
    }
}

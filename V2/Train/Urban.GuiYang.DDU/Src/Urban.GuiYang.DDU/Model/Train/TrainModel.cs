using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.Train
{
    [Export]
    public class TrainModel : NotificationObject
    {
        private List<Car> m_CarCollection;
        private float m_TouchNetA;
        private float m_TouchNetV;
        private DriveModel m_DriveModel;
        private float m_LimitedSpeed;
        private float m_Speed;
        private WorkState m_WorkState;
        private RunningDirection m_RunningDirection;
        private float m_BrakePercent;
        private int m_BrakePageIndex;
        private float m_TowPercent;
        private int m_AirConditionPageIndex;
       
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

        public int BrakePageIndex
        {
            get { return m_BrakePageIndex; }
            set
            {
                if (value == m_BrakePageIndex)
                {
                    return;
                }
                m_BrakePageIndex = value;
                RaisePropertyChanged(() => BrakePageIndex);
            }
        }

        public int AirConditionPageIndex
        {
            get { return m_AirConditionPageIndex; }
            set
            {
                if (value == m_AirConditionPageIndex)
                {
                    return;
                }
                m_AirConditionPageIndex = value;
                RaisePropertyChanged(() => AirConditionPageIndex);
            }
        }

        /// <summary>
        /// 牵引制动 %  - 为制动 + 为牵引 
        /// </summary>
        public float TowPercent
        {
            get { return m_TowPercent; }
            set
            {
                if (value.Equals(m_TowPercent))
                {
                    return;
                }

                m_TowPercent = value;
                RaisePropertyChanged(() => TowPercent);
            }
        }

        public float BrakePercent
        {
            get { return m_BrakePercent; }
            set
            {
                if (value.Equals(m_BrakePercent))
                {
                    return;
                }

                m_BrakePercent = value;
                RaisePropertyChanged(() => BrakePercent);
            }
        }

        public RunningDirection RunningDirection
        {
            get { return m_RunningDirection; }
            set
            {
                if (value == m_RunningDirection)
                {
                    return;
                }

                m_RunningDirection = value;
                RaisePropertyChanged(() => RunningDirection);
            }
        }

        /// <summary>
        /// 列车运行状态
        /// </summary>
        public WorkState WorkState
        {
            get { return m_WorkState; }
            set
            {
                if (value == m_WorkState)
                {
                    return;
                }

                m_WorkState = value;
                RaisePropertyChanged(() => WorkState);
            }
        }

        /// <summary>
        /// 速度
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
        /// 限速
        /// </summary>
        public float LimitedSpeed
        {
            get { return m_LimitedSpeed; }
            set
            {
                if (value.Equals(m_LimitedSpeed))
                {
                    return;
                }

                m_LimitedSpeed = value;
                RaisePropertyChanged(() => LimitedSpeed);
            }
        }

        /// <summary>
        /// 驾驶模式
        /// </summary>
        public DriveModel DriveModel
        {
            get { return m_DriveModel; }
            set
            {
                if (value == m_DriveModel)
                {
                    return;
                }

                m_DriveModel = value;
                RaisePropertyChanged(() => DriveModel);
            }
        }

        /// <summary>
        /// 网压
        /// </summary>
        public float TouchNetV
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

        public OtherModel OtherModel { get; private set; }
    }
}
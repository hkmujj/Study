using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TAX2.SS7C.Events;
using Engine.TAX2.SS7C.Extension;
using Engine.TAX2.SS7C.Model.Domain.Constant;
using Engine.TAX2.SS7C.Model.Domain.Details;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.Model.Domain
{
    [Export]
    public class TrainInfoModel : NotificationObject
    {
        private float m_MagneticCuttingI2;
        private float m_MagneticCuttingI1;
        private float m_EleI6;
        private float m_EleI5;
        private float m_EleI4;
        private float m_EleI3;
        private float m_EleI2;
        private float m_EleI1;
        private float m_EleV2;
        private float m_EleV1;
        private float m_CurrentSpeed;
        private float m_RunningLevel;
        private float m_AxisTemperature;
        private int m_AxisLocation;
        private float m_EmergEle;
        private float m_EmergMagneticCuttingI;
        private float m_EmergEleV;
        private WorkState m_WorkState;
        private FeedbackFlag m_FeedbackFlag;
        private float m_MagneticCuttingRatio;
        private AssistSystemInfoState m_AssistSystemInfoState;
        private CutoffState m_CutoffStateLCU;
        private CutoffState m_CutoffState2;
        private CutoffState m_CutoffState1;
        private bool m_IsMagneticCuttingRatioVisible;


        public Lazy<ObservableCollection<TrainState2Item>> ItemCollection { private set; get; }

        public PowerSupplyUnit PowerSupplyUnit1 { get; private set; }
        public PowerSupplyUnit PowerSupplyUnit2 { get; private set; }

        /// <summary>
        /// 辅助信息
        /// </summary>
        public AssistSystemInfoState AssistSystemInfoState
        {
            get { return m_AssistSystemInfoState; }
            set
            {
                if (value == m_AssistSystemInfoState)
                {
                    return;
                }

                m_AssistSystemInfoState = value;
                RaisePropertyChanged(() => AssistSystemInfoState);
            }
        }

        /// <summary>
        /// 削磁系数 是否可见
        /// </summary>
        public bool IsMagneticCuttingRatioVisible
        {
            get { return m_IsMagneticCuttingRatioVisible; }
            set
            {
                if (value == m_IsMagneticCuttingRatioVisible)
                {
                    return;
                }

                m_IsMagneticCuttingRatioVisible = value;
                RaisePropertyChanged(() => IsMagneticCuttingRatioVisible);
            }
        }

        /// <summary>
        /// 削磁系数
        /// </summary>
        public float MagneticCuttingRatio
        {
            get { return m_MagneticCuttingRatio; }
            set
            {
                if (value.Equals(m_MagneticCuttingRatio))
                {
                    return;
                }

                m_MagneticCuttingRatio = value;
                RaisePropertyChanged(() => MagneticCuttingRatio);
            }
        }

        public CutoffState CutoffState1
        {
            get { return m_CutoffState1; }
            set
            {
                if (value == m_CutoffState1)
                {
                    return;
                }

                m_CutoffState1 = value;
                RaisePropertyChanged(() => CutoffState1);
            }
        }

        public CutoffState CutoffState2
        {
            get { return m_CutoffState2; }
            set
            {
                if (value == m_CutoffState2)
                {
                    return;
                }

                m_CutoffState2 = value;
                RaisePropertyChanged(() => CutoffState2);
            }
        }

        public CutoffState CutoffStateLCU
        {
            get { return m_CutoffStateLCU; }
            set
            {
                if (value == m_CutoffStateLCU)
                {
                    return;
                }

                m_CutoffStateLCU = value;
                RaisePropertyChanged(() => CutoffStateLCU);
            }
        }

        /// <summary>
        /// 工况
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

        public int AxisLocation
        {
            get { return m_AxisLocation; }
            set
            {
                if (value == m_AxisLocation)
                {
                    return;
                }

                m_AxisLocation = value;
                RaisePropertyChanged(() => AxisLocation);
            }
        }

        public float AxisTemperature
        {
            get { return m_AxisTemperature; }
            set
            {
                if (value.Equals(m_AxisTemperature))
                {
                    return;
                }

                m_AxisTemperature = value;
                RaisePropertyChanged(() => AxisTemperature);
            }
        }

        public float RunningLevel
        {
            get { return m_RunningLevel; }
            set
            {
                if (value.Equals(m_RunningLevel))
                {
                    return;
                }

                m_RunningLevel = value;
                RaisePropertyChanged(() => RunningLevel);
            }
        }

        /// <summary>
        /// 加馈标志
        /// </summary>
        public FeedbackFlag FeedbackFlag
        {
            get { return m_FeedbackFlag; }
            set
            {
                if (value == m_FeedbackFlag)
                {
                    return;
                }

                m_FeedbackFlag = value;
                RaisePropertyChanged(() => FeedbackFlag);
            }
        }

        public float CurrentSpeed
        {
            get { return m_CurrentSpeed; }
            set
            {
                if (value.Equals(m_CurrentSpeed))
                {
                    return;
                }

                m_CurrentSpeed = value;
                RaisePropertyChanged(() => CurrentSpeed);
            }
        }

        public float EleV1
        {
            get { return m_EleV1; }
            set
            {
                if (value.Equals(m_EleV1))
                {
                    return;
                }

                m_EleV1 = value;
                RaisePropertyChanged(() => EleV1);
            }
        }

        public float EleV2
        {
            get { return m_EleV2; }
            set
            {
                if (value.Equals(m_EleV2))
                {
                    return;
                }

                m_EleV2 = value;
                RaisePropertyChanged(() => EleV2);
            }
        }

        public float EleI1
        {
            get { return m_EleI1; }
            set
            {
                if (value.Equals(m_EleI1))
                {
                    return;
                }

                m_EleI1 = value;
                RaisePropertyChanged(() => EleI1);
            }
        }

        public float EleI2
        {
            get { return m_EleI2; }
            set
            {
                if (value.Equals(m_EleI2))
                {
                    return;
                }

                m_EleI2 = value;
                RaisePropertyChanged(() => EleI2);
            }
        }

        public float EleI3
        {
            get { return m_EleI3; }
            set
            {
                if (value.Equals(m_EleI3))
                {
                    return;
                }

                m_EleI3 = value;
                RaisePropertyChanged(() => EleI3);
            }
        }

        public float EleI4
        {
            get { return m_EleI4; }
            set
            {
                if (value.Equals(m_EleI4))
                {
                    return;
                }

                m_EleI4 = value;
                RaisePropertyChanged(() => EleI4);
            }
        }

        public float EleI5
        {
            get { return m_EleI5; }
            set
            {
                if (value.Equals(m_EleI5))
                {
                    return;
                }

                m_EleI5 = value;
                RaisePropertyChanged(() => EleI5);
            }
        }

        public float EleI6
        {
            get { return m_EleI6; }
            set
            {
                if (value.Equals(m_EleI6))
                {
                    return;
                }

                m_EleI6 = value;
                RaisePropertyChanged(() => EleI6);
            }
        }

        public float EmergEleV
        {
            get { return m_EmergEleV; }
            set
            {
                if (value.Equals(m_EmergEleV))
                {
                    return;
                }

                m_EmergEleV = value;
                RaisePropertyChanged(() => EmergEleV);
            }
        }

        public float EmergEleI
        {
            get { return m_EmergEle; }
            set
            {
                if (value.Equals(m_EmergEle))
                {
                    return;
                }

                m_EmergEle = value;
                RaisePropertyChanged(() => EmergEleI);
            }
        }

        public float EmergMagneticCuttingI
        {
            get { return m_EmergMagneticCuttingI; }
            set
            {
                if (value.Equals(m_EmergMagneticCuttingI))
                {
                    return;
                }

                m_EmergMagneticCuttingI = value;
                RaisePropertyChanged(() => EmergMagneticCuttingI);
            }
        }

        public float MagneticCuttingI1
        {
            get { return m_MagneticCuttingI1; }
            set
            {
                if (value.Equals(m_MagneticCuttingI1))
                {
                    return;
                }

                m_MagneticCuttingI1 = value;
                RaisePropertyChanged(() => MagneticCuttingI1);
            }
        }

        public float MagneticCuttingI2
        {
            get { return m_MagneticCuttingI2; }
            set
            {
                if (value.Equals(m_MagneticCuttingI2))
                {
                    return;
                }

                m_MagneticCuttingI2 = value;
                RaisePropertyChanged(() => MagneticCuttingI2);
            }
        }

        [ImportingConstructor]
        public TrainInfoModel(IEventAggregator eventAggregator)
        {
            ItemCollection = new Lazy<ObservableCollection<TrainState2Item>>(() =>
            {
                eventAggregator.GetEvent<DomainmodelLazyValueCreatedEvent>().PublishLater();

                return new ObservableCollection<TrainState2Item>(
                    GlobalParam.Instance.TrainInfoPage2ConfigCollection.Value.Select(s => new TrainState2Item(s)));
            });

            PowerSupplyUnit1 = new PowerSupplyUnit();
            PowerSupplyUnit2 = new PowerSupplyUnit();
        }
    }
}
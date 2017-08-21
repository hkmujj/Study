using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.SZLHLF.Controller;
using LightRail.HMI.SZLHLF.Interface;
using LightRail.HMI.SZLHLF.Model.Units;

namespace LightRail.HMI.SZLHLF.Model.AirModel
{
    [Export]
    [Export(typeof(IModels))]
    public class AirConditionModel : ModelBase
    {
        private ObservableCollection<AirConditionUnit> m_AirConditionUnits;
        private AirModelState m_AirModelState1;
        private AirModelState m_AirModelState2;
        private AirModelState m_AirModelState3;
        private AirModelState m_AirModelState4;
        private float m_TemperatureOut;
        private float m_Temperature1;
        private float m_Temperature2;
        private float m_Temperature3;
        private float m_Temperature4;
        private float m_TemperatureAdjust;

        [ImportingConstructor]
        public AirConditionModel(AirConditionController airConditionController)
        {
            m_AirConditionUnits = new ObservableCollection<AirConditionUnit>(GlobalParam.Instance.AirConditionUnits);
            AllModelSelects = new ObservableCollection<AirModelSetUnit>(GlobalParam.Instance.AirModelSetUnit.OrderBy(o => o.ID));
            AllWindSpeedSet = new ObservableCollection<WindSpeedSetUnit>(GlobalParam.Instance.WindSpeedSetUnit.OrderBy(o=>o.ID));
            AllNewAirValueSet = new ObservableCollection<NewAirValueSetUnit>(GlobalParam.Instance.NewAirValueSetUnit.OrderBy(o=>o.ID));

            AirConditionController = airConditionController;
            AirConditionController.ViewModel = new Lazy<AirConditionModel>(() => this);
            AirConditionController.Initalize();
        }

        public override void Initialize()
        {
            AirConditionController.Initalize();
        }

        public override void Clear()
        {
            AirConditionController.Clear();
        }

        public AirConditionController AirConditionController { get; set; }

        /// <summary>
        /// 空调模式设定
        /// </summary>
        public ObservableCollection<AirModelSetUnit> AllModelSelects { get; set; }

        /// <summary>
        /// 风速设定
        /// </summary>
        public ObservableCollection<WindSpeedSetUnit> AllWindSpeedSet { get; set; }

        /// <summary>
        /// 新风量设定
        /// </summary>
        public ObservableCollection<NewAirValueSetUnit> AllNewAirValueSet { get; set; }

        /// <summary>
        /// 所有空调单元
        /// </summary>
        public ObservableCollection<AirConditionUnit> AirConditionUnits
        {
            get { return m_AirConditionUnits; }
            private set
            {
                if (Equals(value, m_AirConditionUnits))
                {
                    return;
                }
                m_AirConditionUnits = value;
                RaisePropertyChanged(() => AirConditionUnits);
                RaisePropertyChanged(() => Car1Location1AirConditionUnit);
                RaisePropertyChanged(() => Car2Location1AirConditionUnit);
                RaisePropertyChanged(() => Car3Location1AirConditionUnit);
                RaisePropertyChanged(() => Car4Location1AirConditionUnit);
            }
        }

        /// <summary>
        /// 1车1位置
        /// </summary>
        public AirConditionUnit Car1Location1AirConditionUnit { get { return AirConditionUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 2车1位置
        /// </summary>
        public AirConditionUnit Car2Location1AirConditionUnit { get { return AirConditionUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 3车1位置
        /// </summary>
        public AirConditionUnit Car3Location1AirConditionUnit { get { return AirConditionUnits.Where(w => w.Car == 3 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 4车1位置
        /// </summary>
        public AirConditionUnit Car4Location1AirConditionUnit { get { return AirConditionUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车1空调模式
        /// </summary>
        public AirModelState AirModelState1
        {
            get { return m_AirModelState1; }
            set
            {
                if (value == m_AirModelState1)
                {
                    return;
                }

                m_AirModelState1 = value;
                RaisePropertyChanged(() => AirModelState1);
            }
        }

        /// <summary>
        /// 车2空调模式
        /// </summary>
        public AirModelState AirModelState2
        {
            get { return m_AirModelState2; }
            set
            {
                if (value == m_AirModelState2)
                {
                    return;
                }

                m_AirModelState2 = value;
                RaisePropertyChanged(() => AirModelState2);
            }
        }

        /// <summary>
        /// 车3空调模式
        /// </summary>
        public AirModelState AirModelState3
        {
            get { return m_AirModelState3; }
            set
            {
                if (value == m_AirModelState3)
                {
                    return;
                }

                m_AirModelState3 = value;
                RaisePropertyChanged(() => AirModelState3);
            }
        }

        /// <summary>
        /// 车4空调模式
        /// </summary>
        public AirModelState AirModelState4
        {
            get { return m_AirModelState4; }
            set
            {
                if (value == m_AirModelState4)
                {
                    return;
                }

                m_AirModelState4 = value;
                RaisePropertyChanged(() => AirModelState4);
            }
        }

        /// <summary>
        /// 室外温度
        /// </summary>
        public float TemperatureOut
        {
            get { return m_TemperatureOut; }
            set
            {
                if (value == m_TemperatureOut)
                {
                    return;
                }

                m_TemperatureOut = value;
                RaisePropertyChanged(() => TemperatureOut);
            }
        }

        /// <summary>
        /// 车1温度
        /// </summary>
        public float Temperature1
        {
            get { return m_Temperature1; }
            set
            {
                if (value == m_Temperature1)
                {
                    return;
                }

                m_Temperature1 = value;
                RaisePropertyChanged(() => Temperature1);
            }
        }

        /// <summary>
        /// 车2温度
        /// </summary>
        public float Temperature2
        {
            get { return m_Temperature2; }
            set
            {
                if (value == m_Temperature2)
                {
                    return;
                }

                m_Temperature2 = value;
                RaisePropertyChanged(() => Temperature2);
            }
        }

        /// <summary>
        /// 车3温度
        /// </summary>
        public float Temperature3
        {
            get { return m_Temperature3; }
            set
            {
                if (value == m_Temperature3)
                {
                    return;
                }

                m_Temperature3 = value;
                RaisePropertyChanged(() => Temperature3);
            }
        }

        /// <summary>
        /// 车4温度
        /// </summary>
        public float Temperature4
        {
            get { return m_Temperature4; }
            set
            {
                if (value == m_Temperature4)
                {
                    return;
                }

                m_Temperature4 = value;
                RaisePropertyChanged(() => Temperature4);
            }
        }

        /// <summary>
        /// 设置温度
        /// </summary>
        public float TemperatureAdjust
        {
            get { return m_TemperatureAdjust; }
            set
            {
                if (value == m_TemperatureAdjust)
                {
                    return;
                }

                m_TemperatureAdjust = value;
                RaisePropertyChanged(() => TemperatureAdjust);
            }
        }
    }
}

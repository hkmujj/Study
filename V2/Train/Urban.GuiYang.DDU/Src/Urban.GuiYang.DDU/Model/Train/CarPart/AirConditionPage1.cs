using System.Diagnostics;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class AirConditionPage1 : CarPartBase
    {
        private CarItem<AirConditionState, CarAirConditionConfig> m_AirCondition1;
        private CarItem<AirConditionState, CarAirConditionConfig> m_AirCondition2;
        private CarItem<ValidateValueStateItem, CarTemperatureConfig> m_CarTemperature;
        private CarItem<ControlState, CarControlModelConfig> m_ControlModel;

        [DebuggerStepThrough]
        public AirConditionPage1(AirConditionItemType itemType)
        {
            ItemType = itemType;
        }

        public AirConditionItemType ItemType { get; private set; }

        public CarItem<ControlState, CarControlModelConfig> ControlModel
        {
            get { return m_ControlModel; }
            set
            {
                if (Equals(value, m_ControlModel))
                {
                    return;
                }
                m_ControlModel = value;
                RaisePropertyChanged(() => ControlModel);
            }
        }

        public CarItem<ValidateValueStateItem, CarTemperatureConfig> CarTemperature
        {
            get { return m_CarTemperature; }
            set
            {
                if (Equals(value, m_CarTemperature))
                {
                    return;
                }
                m_CarTemperature = value;
                RaisePropertyChanged(() => CarTemperature);
            }
        }

        public CarItem<AirConditionState, CarAirConditionConfig> AirCondition1
        {
            get { return m_AirCondition1; }
            set
            {
                if (Equals(value, m_AirCondition1))
                {
                    return;
                }
                m_AirCondition1 = value;
                RaisePropertyChanged(() => AirCondition1);
            }
        }

        public CarItem<AirConditionState, CarAirConditionConfig> AirCondition2
        {
            get { return m_AirCondition2; }
            set
            {
                if (Equals(value, m_AirCondition2))
                {
                    return;
                }
                m_AirCondition2 = value;
                RaisePropertyChanged(() => AirCondition2);
            }
        }
    }
}
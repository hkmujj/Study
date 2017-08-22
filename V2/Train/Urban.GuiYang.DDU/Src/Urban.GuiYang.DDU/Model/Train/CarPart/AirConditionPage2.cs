using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class AirConditionPage2 : CarPartBase
    {
        private CarItem<NewAirValveState, CarGroup1NewAirValveConfig> m_Group1NewAirValve1;
        private CarItem<NewAirValveState, CarGroup1NewAirValveConfig> m_Group1NewAirValve2;
        private CarItem<BackAirValveState, CarGroup1BackAirValveConfig> m_Group1BackAirValve2;
        private CarItem<BackAirValveState, CarGroup1BackAirValveConfig> m_Group1BackAirValve1;

        private CarItem<NewAirValveState, CarGroup2NewAirValveConfig> m_Group2NewAirValve1;
        private CarItem<NewAirValveState, CarGroup2NewAirValveConfig> m_Group2NewAirValve2;
        private CarItem<BackAirValveState, CarGroup2BackAirValveConfig> m_Group2BackAirValve2;
        private CarItem<BackAirValveState, CarGroup2BackAirValveConfig> m_Group2BackAirValve1;

        public CarItem<BackAirValveState, CarGroup2BackAirValveConfig> Group2BackAirValve2
        {
            get { return m_Group2BackAirValve2; }
            set
            {
                if (Equals(value, m_Group2BackAirValve2))
                {
                    return;
                }
                m_Group2BackAirValve2 = value;
                RaisePropertyChanged(() => Group2BackAirValve2);
            }
        }
        public CarItem<BackAirValveState, CarGroup2BackAirValveConfig> Group2BackAirValve1
        {
            get { return m_Group2BackAirValve1; }
            set
            {
                if (Equals(value, m_Group2BackAirValve1))
                {
                    return;
                }
                m_Group2BackAirValve1 = value;
                RaisePropertyChanged(() => Group2BackAirValve1);
            }
        }

        public CarItem<NewAirValveState, CarGroup2NewAirValveConfig> Group2NewAirValve1
        {
            get { return m_Group2NewAirValve1; }
            set
            {
                if (Equals(value, m_Group2NewAirValve1))
                {
                    return;
                }
                m_Group2NewAirValve1 = value;
                RaisePropertyChanged(() => Group2NewAirValve1);
            }
        }

        public CarItem<NewAirValveState, CarGroup2NewAirValveConfig> Group2NewAirValve2
        {
            get { return m_Group2NewAirValve2; }
            set
            {
                if (Equals(value, m_Group2NewAirValve2))
                {
                    return;
                }
                m_Group2NewAirValve2 = value;
                RaisePropertyChanged(() => Group2NewAirValve2);
            }
        }

        public CarItem<BackAirValveState, CarGroup1BackAirValveConfig> Group1BackAirValve1
        {
            get { return m_Group1BackAirValve1; }
            set
            {
                if (Equals(value, m_Group1BackAirValve1))
                {
                    return;
                }
                m_Group1BackAirValve1 = value;
                RaisePropertyChanged(() => Group1BackAirValve1);
            }
        }
        public CarItem<BackAirValveState, CarGroup1BackAirValveConfig> Group1BackAirValve2
        {
            get { return m_Group1BackAirValve2; }
            set
            {
                if (Equals(value, m_Group1BackAirValve2))
                {
                    return;
                }
                m_Group1BackAirValve2 = value;
                RaisePropertyChanged(() => Group1BackAirValve2);
            }
        }

        public CarItem<NewAirValveState, CarGroup1NewAirValveConfig> Group1NewAirValve2
        {
            get { return m_Group1NewAirValve2; }
            set
            {
                if (Equals(value, m_Group1NewAirValve2))
                {
                    return;
                }
                m_Group1NewAirValve2 = value;
                RaisePropertyChanged(() => Group1NewAirValve2);
            }
        }


        public CarItem<NewAirValveState, CarGroup1NewAirValveConfig> Group1NewAirValve1
        {
            get { return m_Group1NewAirValve1; }
            set
            {
                if (Equals(value, m_Group1NewAirValve1))
                {
                    return;
                }
                m_Group1NewAirValve1 = value;
                RaisePropertyChanged(() => Group1NewAirValve1);
            }
        }
    }
}

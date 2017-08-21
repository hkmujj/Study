using Urban.GuiYang.DDU.Config;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class BrakePage3 : CarPartBase
    {
        private CarItem<ValidateFloatItem, CarWeightConfig> m_Weight;

        private CarItem<ValidateFloatItem, CarLadenConfig> m_Laden;

        public CarItem<ValidateFloatItem, CarWeightConfig> Weight
        {
            get { return m_Weight; }
            set
            {
                if (value == m_Weight)
                {
                    return;
                }

                m_Weight= value;
                RaisePropertyChanged(() => Weight);
            }
        }

        public CarItem<ValidateFloatItem, CarLadenConfig> Laden
        {
            get { return m_Laden; }
            set
            {
                if (value == m_Laden)
                {
                    return;
                }

                m_Laden = value;
                RaisePropertyChanged(() => Laden);
            }
        }


        /*
                private float? m_Laden;
                private float? m_Weight;
                public float? Weight
                {
                    get { return m_Weight; }
                    set
                    {
                        if (value.Equals(m_Weight))
                        {
                            return;
                        }

                        m_Weight = value;
                        RaisePropertyChanged(() => Weight);
                    }
                }

                public float? Laden
                {
                    get { return m_Laden; }
                    set
                    {
                        if (value.Equals(m_Laden))
                        {
                            return;
                        }

                        m_Laden = value;
                        RaisePropertyChanged(() => Laden);
                    }
                }
        */
    }
}
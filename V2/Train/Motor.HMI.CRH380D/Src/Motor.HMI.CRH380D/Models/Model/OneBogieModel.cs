using LightRail.HMI.SZLHLF.Model;
using Motor.HMI.CRH380D.Models.State;

namespace Motor.HMI.CRH380D.Models.Model
{
    public class OneBogieModel : ModelBase
    {
        private IMSState m_IMSState1;
        private IMSState m_IMSState2;
        private float m_AxleTemperature1;
        private float m_AxleTemperature2;
        private float m_GearTemperature1;
        private float m_GearTemperature2;
        private float m_GearTemperature3;
        private float m_GearTemperature4;
        private float m_GearTemperature5;
        private float m_GearTemperature6;
        private float m_GearTemperature7;
        private float m_GearTemperature8;


        public OneBogieModel(int num)
        {
            CarNum = num;
        }

        public void Clone(OneBogieModel temp)
        {
            this.CarNum = temp.CarNum;
            this.AxleTemperature1 = temp.AxleTemperature1;
            this.AxleTemperature2 = temp.AxleTemperature2;
            this.GearTemperature1 = temp.GearTemperature1;
            this.GearTemperature2 = temp.GearTemperature2;
            this.GearTemperature3 = temp.GearTemperature3;
            this.GearTemperature4 = temp.GearTemperature4;
            this.GearTemperature5 = temp.GearTemperature5;
            this.GearTemperature6 = temp.GearTemperature6;
            this.GearTemperature7 = temp.GearTemperature7;
            this.GearTemperature8 = temp.GearTemperature8;
            this.IMSState1 = temp.IMSState1;
            this.IMSState2 = temp.IMSState2;
        }

        public int CarNum { get; set; }
        
        /// <summary>
        /// IMS状态1
        /// </summary>
        public IMSState IMSState1
        {
            get { return m_IMSState1; }
            set
            {
                if (value == m_IMSState1)
                {
                    return;
                }
                m_IMSState1 = value;
                RaisePropertyChanged(() => IMSState1);
            }
        }

        /// <summary>
        /// IMS状态2
        /// </summary>
        public IMSState IMSState2
        {
            get { return m_IMSState2; }
            set
            {
                if (value == m_IMSState2)
                {
                    return;
                }
                m_IMSState2 = value;
                RaisePropertyChanged(() => IMSState2);
            }
        }

        /// <summary>
        /// 轴温1
        /// </summary>
        public float AxleTemperature1
        {
            get { return m_AxleTemperature1; }
            set
            {
                if (value == m_AxleTemperature1)
                {
                    return;
                }
                m_AxleTemperature1 = value;
                RaisePropertyChanged(() => AxleTemperature1);
            }
        }

        /// <summary>
        /// 轴温2
        /// </summary>
        public float AxleTemperature2
        {
            get { return m_AxleTemperature2; }
            set
            {
                if (value == m_AxleTemperature2)
                {
                    return;
                }
                m_AxleTemperature2 = value;
                RaisePropertyChanged(() => AxleTemperature2);
            }
        }

        /// <summary>
        /// 齿轮箱温度1
        /// </summary>
        public float GearTemperature1
        {
            get { return m_GearTemperature1; }
            set
            {
                if (value == m_GearTemperature1)
                {
                    return;
                }
                m_GearTemperature1 = value;
                RaisePropertyChanged(() => GearTemperature1);
            }
        }

        /// <summary>
        /// 齿轮箱温度2
        /// </summary>
        public float GearTemperature2
        {
            get { return m_GearTemperature2; }
            set
            {
                if (value == m_GearTemperature2)
                {
                    return;
                }
                m_GearTemperature2 = value;
                RaisePropertyChanged(() => GearTemperature2);
            }
        }

        /// <summary>
        /// 齿轮箱温度3
        /// </summary>
        public float GearTemperature3
        {
            get { return m_GearTemperature3; }
            set
            {
                if (value == m_GearTemperature3)
                {
                    return;
                }
                m_GearTemperature3 = value;
                RaisePropertyChanged(() => GearTemperature3);
            }
        }

        /// <summary>
        /// 齿轮箱温度4
        /// </summary>
        public float GearTemperature4
        {
            get { return m_GearTemperature4; }
            set
            {
                if (value == m_GearTemperature4)
                {
                    return;
                }
                m_GearTemperature4 = value;
                RaisePropertyChanged(() => GearTemperature4);
            }
        }

        /// <summary>
        /// 齿轮箱温度5
        /// </summary>
        public float GearTemperature5
        {
            get { return m_GearTemperature5; }
            set
            {
                if (value == m_GearTemperature5)
                {
                    return;
                }
                m_GearTemperature5 = value;
                RaisePropertyChanged(() => GearTemperature5);
            }
        }

        /// <summary>
        /// 齿轮箱温度6
        /// </summary>
        public float GearTemperature6
        {
            get { return m_GearTemperature6; }
            set
            {
                if (value == m_GearTemperature6)
                {
                    return;
                }
                m_GearTemperature6 = value;
                RaisePropertyChanged(() => GearTemperature6);
            }
        }

        /// <summary>
        /// 齿轮箱温度7
        /// </summary>
        public float GearTemperature7
        {
            get { return m_GearTemperature7; }
            set
            {
                if (value == m_GearTemperature7)
                {
                    return;
                }
                m_GearTemperature7 = value;
                RaisePropertyChanged(() => GearTemperature7);
            }
        }

        /// <summary>
        /// 齿轮箱温度8
        /// </summary>
        public float GearTemperature8
        {
            get { return m_GearTemperature8; }
            set
            {
                if (value == m_GearTemperature8)
                {
                    return;
                }
                m_GearTemperature8 = value;
                RaisePropertyChanged(() => GearTemperature8);
            }
        }
    }
}
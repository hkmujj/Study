using LightRail.HMI.SZLHLF.Model;
using Motor.HMI.CRH380D.Models.State;

namespace Motor.HMI.CRH380D.Models.Model
{
    public class CarModel : ModelBase
    {
        private CarState m_CarState;
        private bool m_IsCheck;
        private bool m_IsEnable;

        public CarModel(int num)
        {
            CarNum = num;
        }

        public int CarNum { get; set; }
        
        /// <summary>
        /// 车辆显示状态
        /// </summary>
        public CarState State
        {
            get { return m_CarState; }
            set
            {
                if (value == m_CarState)
                {
                    return;
                }
                m_CarState = value;
                RaisePropertyChanged(() => State);
            }
        }

        /// <summary>
        /// 是否选择
        /// </summary>
        public bool IsCheck
        {
            get { return m_IsCheck; }
            set
            {
                if (value == m_IsCheck)
                {
                    return;
                }
                m_IsCheck = value;
                RaisePropertyChanged(() => IsCheck);
            }
        }

        /// <summary>
        /// 是否使能
        /// </summary>
        public bool IsEnable
        {
            get { return m_IsEnable; }
            set
            {
                if (value == m_IsEnable)
                {
                    return;
                }
                m_IsEnable = value;
                RaisePropertyChanged(() => IsEnable);
            }
        }
    }
}
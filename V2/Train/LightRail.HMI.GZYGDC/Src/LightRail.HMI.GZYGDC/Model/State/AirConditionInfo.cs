using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.Model.State
{
    /// <summary>
    /// 空调信息
    /// </summary>
    public class AirConditionInfo : NotificationObject
    {

        private AirConditionMode m_ConditionMode;

        private float m_SettingTemperature;

        private float m_InCarTemperature;


        /// <summary>
        /// 空调模式
        /// </summary>
        public AirConditionMode ConditionMode
        {
            get { return m_ConditionMode; }
            set
            {
                if (Equals(value, m_ConditionMode))
                {
                    return;
                }

                m_ConditionMode = value;

                RaisePropertyChanged(() => ConditionMode);
            }
        }



        /// <summary>
        /// 设定温度
        /// </summary>
        public float SettingTemperature
        {
            get { return m_SettingTemperature; }
            set
            {
                if (Equals(value, m_SettingTemperature))
                {
                    return;
                }

                m_SettingTemperature = value;

                RaisePropertyChanged(() => SettingTemperature);
            }
        }


        /// <summary>
        /// 车内温度
        /// </summary>
        public float InCarTemperature
        {
            get { return m_InCarTemperature; }
            set
            {
                if (Equals(value, m_InCarTemperature))
                {
                    return;
                }

                m_InCarTemperature = value;

                RaisePropertyChanged(() => InCarTemperature);
            }
        }

    }
}

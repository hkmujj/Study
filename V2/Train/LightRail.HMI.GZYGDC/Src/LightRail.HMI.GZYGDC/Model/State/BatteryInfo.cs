using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.Model.State
{
    /// <summary>
    /// 电池信息
    /// </summary>
    public class BatteryInfo : NotificationObject
    {

        private float m_PowerQuantityPercent;

        private float m_Temperature;




        /// <summary>
        /// 电池电量百分比
        /// </summary>
        public float PowerQuantityPercent
        {
            get { return m_PowerQuantityPercent; }
            set
            {
                if (value.Equals(m_PowerQuantityPercent))
                {
                    return;
                }

                m_PowerQuantityPercent = value;

                RaisePropertyChanged(() => PowerQuantityPercent);
            }
        }


        /// <summary>
        /// 电池温度
        /// </summary>
        public float Temperature
        {
            get { return m_Temperature; }
            set
            {
                if (value.Equals(m_Temperature))
                {
                    return;
                }

                m_Temperature = value;

                RaisePropertyChanged(() => Temperature);
            }
        }
    }
}

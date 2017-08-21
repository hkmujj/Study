using System.ComponentModel.Composition;
using LightRail.HMI.SZLHLF.Model;

namespace LightRail.HMI.SZLHLF.ViewModel
{
    [Export]
    public class SecondViewModel : ModelBase
    {
        private float m_EnergyStoragePowerAnalogVoltage;

        /// <summary>
        /// 储能电源模拟电压
        /// </summary>
        public float EnergyStoragePowerAnalogVoltage
        {
            get { return m_EnergyStoragePowerAnalogVoltage; }
            set
            {
                if (value.Equals(m_EnergyStoragePowerAnalogVoltage))
                {
                    return;
                }

                m_EnergyStoragePowerAnalogVoltage = value;
                RaisePropertyChanged(() => EnergyStoragePowerAnalogVoltage);
            }
        }
    }
}

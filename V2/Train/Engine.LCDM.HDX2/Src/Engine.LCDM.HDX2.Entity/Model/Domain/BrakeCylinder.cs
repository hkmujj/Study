using Microsoft.Practices.Prism.ViewModel;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    /// <summary>
    /// 制动扛
    /// </summary>
    public class BrakeCylinder : NotificationObject
    {
        private double m_BrakeCylinder1Pressure;
        private double m_BrakeCylinder2Pressure;

        public double BrakeCylinder1Pressure
        {
            set
            {
                if (value.Equals(m_BrakeCylinder1Pressure))
                {
                    return;
                }
                m_BrakeCylinder1Pressure = value;
                RaisePropertyChanged(() => BrakeCylinder1Pressure);
            }
            get { return m_BrakeCylinder1Pressure; }
        }

        public double BrakeCylinder2Pressure
        {
            set
            {
                if (value.Equals(m_BrakeCylinder2Pressure))
                {
                    return;
                }
                m_BrakeCylinder2Pressure = value;
                RaisePropertyChanged(() => BrakeCylinder2Pressure);
            }
            get { return m_BrakeCylinder2Pressure; }
        }
    }
}
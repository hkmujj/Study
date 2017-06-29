using CBTC.Infrasturcture.Model.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace CBTC.Infrasturcture.Model.Signal.Speed
{
    public class SpeedModel : NotificationObject
    {
        private CBTCColor m_SpeedColor;
        private float m_Value;
        private bool m_Visible;

        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (value == m_Visible)
                    return;

                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
        }

        public float Value
        {
            get { return m_Value; }
            set
            {
                if (value.Equals(m_Value))
                    return;

                m_Value = value;
                RaisePropertyChanged(() => Value);
            }
        }

        public CBTCColor SpeedColor
        {
            get { return m_SpeedColor; }
            set
            {
                if (value == m_SpeedColor)
                    return;

                m_SpeedColor = value;
                RaisePropertyChanged(() => SpeedColor);
            }
        }
    }
}
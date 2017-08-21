using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
{
    [DebuggerDisplay("SpeedColor={SpeedColor}, Value={Value}")]
    public class SpeedModel : NotificationObject, ISpeedModel
    {
        private float m_Value;
        private ATPColor m_SpeedColor;

        public float Value
        {
            get { return m_Value; }
            set
            {
               // var sp = Math.Abs(value);
                if (value.Equals(m_Value))
                {
                    return;
                }
                m_Value = value;
                RaisePropertyChanged(() => Value);
            }
        }

        public ATPColor SpeedColor
        {
            get { return m_SpeedColor; }
            set
            {
                if (value == m_SpeedColor)
                {
                    return;
                }
                m_SpeedColor = value;
                RaisePropertyChanged(() => SpeedColor);
            }
        }
    }
}
using System;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model
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
                if (Math.Abs(value - (m_Value)) < float.Epsilon)
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
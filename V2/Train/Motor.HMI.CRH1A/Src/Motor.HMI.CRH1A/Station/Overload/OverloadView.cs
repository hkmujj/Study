using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CommonUtil.Controls;
using CommonUtil.Controls.Roundness;

namespace Motor.HMI.CRH1A.Station.Overload
{
    class OverloadView : GDIRoundness
    {
        public OverloadView()
        {
            R = 10;
            State = OverloadState.Default;
            Visible = true;
            NeedDrawContent = true;
        }

        private OverloadState m_State;
        // TODO ContentColor

        public OverloadState State
        {
            set
            {
                m_State = value;
                switch (value)
                {
                    case OverloadState.SeriousOverloading :
                        ContentColor = Color.Red;
                        break;
                    case OverloadState.Overloading :
                        ContentColor = Color.Yellow;
                        break;
                    case OverloadState.Normal :
                        ContentColor = Color.Green;
                        break;
                    default :
                        throw new ArgumentOutOfRangeException("value");
                }
            }
            get { return m_State; }
        }
    }
}

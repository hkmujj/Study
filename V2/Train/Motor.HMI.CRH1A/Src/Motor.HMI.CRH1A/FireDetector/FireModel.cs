using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CommonUtil.Controls.Button;
using CommonUtil.Controls.Button;
using Motor.HMI.CRH1A.Common;

namespace Motor.HMI.CRH1A.FireDetector
{
    class FireModelState
    {
        private readonly CRH1AButton m_Button;

        private Color ButtonColor { set { m_Button.SetButtonColor(value.R, value.G, value.B); } }

        private bool m_IsFireModel;

        private readonly Action<int> m_ChangeStateAction;

        public FireModelState(CRH1AButton button, Action<int> changeStateAction)
        {
            m_Button = button;
            m_ChangeStateAction = changeStateAction;
            //m_ChangeStateAction(0);
            ButtonColor = Color.FromArgb(192, 192, 192);
            m_IsFireModel = false;
        }

        public void ChangeState()
        {
            m_IsFireModel = !m_IsFireModel;
            if (m_IsFireModel)
            {
                ButtonColor = Color.Yellow;
                m_ChangeStateAction(1);
            }
            else
            {
                ButtonColor = Color.FromArgb(192, 192, 192);
                m_ChangeStateAction(0);
            }
        }
    }
}

using System;
using Urban.ATC.CommonView.Model;
using Urban.ATC.CommonView.View;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Common.Control
{
    public partial class FilkerText : TextBase
    {
        private string m_CurrentText;
        private MaximumMode m_Model;
        public bool TextFiler { get; set; }

        public MaximumMode Model
        {
            get { return m_Model; }
            set
            {
                if (m_Model == value)
                {
                    return;
                }
                m_Model = value;
                ChangeText();
            }
        }

        private void ChangeText()
        {
            switch (Model)
            {
                case MaximumMode.Initial:
                    m_CurrentText = string.Empty;
                    break;

                case MaximumMode.RestrictedMode:
                    m_CurrentText = "RM";
                    break;

                case MaximumMode.SMIntermittent:
                    m_CurrentText = "SM-I";
                    break;

                case MaximumMode.SMContinuous:
                    m_CurrentText = "SM-C";
                    break;

                case MaximumMode.AMIntermittent:
                    m_CurrentText = "AM-I";
                    break;

                case MaximumMode.AMContinuous:
                    m_CurrentText = "AM-C";
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public FilkerText()
        {
            InitializeComponent();
            ChangeTextColor(GDICommon.LightGreyColor);

            m_CurrentText = string.Empty;
            m_Timer.Start();
        }

        private void m_Timer_Tick(object sender, EventArgs e)
        {
            if (TextFiler)
            {
                ChangeText(DateTime.Now.Second % 2 == 0 ? m_CurrentText : string.Empty);
            }
            else
            {
                ChangeText(m_CurrentText);
            }
        }
    }
}
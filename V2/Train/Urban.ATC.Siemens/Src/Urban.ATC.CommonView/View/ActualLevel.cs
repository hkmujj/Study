using System;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.CommonView.View
{
    public partial class ActualLevel : TextBase
    {
        private ActualLevels m_Level;

        public ActualLevels Level
        {
            get { return m_Level; }
            set
            {
                if (m_Level == value)
                {
                    return;
                }
                m_Level = value;
                ChangeText();
            }
        }

        private void ChangeText()
        {
            switch (Level)
            {
                case ActualLevels.Initial:
                    ChangeText("");
                    break;

                case ActualLevels.Interlocking:
                    ChangeText("IXL");
                    break;

                case ActualLevels.Intermittent:
                    ChangeText("ITC");
                    break;

                case ActualLevels.Continuous:
                    ChangeText("CTC");
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ActualLevel()
        {
            InitializeComponent();
        }
    }
}
using System;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.CommonView.View
{
    public partial class TextTRec : TextBase
    {
        private float m_Number;
        private RecType m_Type;

        public float Number
        {
            get { return m_Number; }
            set
            {
                if (m_Number == value)
                {
                    return;
                }
                m_Number = value;
                ChangeText();
            }
        }

        private void ChangeText()
        {
            if (Number <= 0)
            {
                SetText(string.Empty);
                return;
            }
            switch (Type)
            {
                case RecType.TripNumber:

                    SetText(string.Format("T{0}", Number.ToString("000000")));
                    break;

                case RecType.DestinationNumber:
                    SetText(string.Format("D{0}", Number.ToString("00")));
                    break;

                case RecType.CrewNumber:
                    SetText(string.Format("C{0}", Number.ToString("000")));
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetText(string str)
        {
            ChangeText(str);
        }

        public RecType Type
        {
            get { return m_Type; }
            set
            {
                if (m_Type == value)
                {
                    return;
                }
                m_Type = value;
                ChangeText();
            }
        }

        public TextTRec()
        {
            InitializeComponent();
        }
    }
}
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.Angola.Diesel.Model
{
    [Export]
    public class DialModel : NotificationObject
    {
        private NotificationRectange m_DialRectange;
        private double m_Dial1;
        private double m_Dial4;
        private double m_Dial3;
        private double m_Dial2;

        public NotificationRectange DialRectange
        {
            set
            {
                if (Equals(value, m_DialRectange))
                {
                    return;
                }

                m_DialRectange = value;
                RaisePropertyChanged(() => DialRectange);
            }
            get { return m_DialRectange; }
        }

        public double Dial1
        {
            set
            {
                if (value.Equals(m_Dial1))
                    return;

                m_Dial1 = value;
                RaisePropertyChanged(() => Dial1);
            }
            get { return m_Dial1; }
        }

        public double Dial2
        {
            set
            {
                if (value.Equals(m_Dial2))
                    return;

                m_Dial2 = value;
                RaisePropertyChanged(() => Dial2);
            }
            get { return m_Dial2; }
        }

        public double Dial3
        {
            set
            {
                if (value.Equals(m_Dial3))
                    return;

                m_Dial3 = value;
                RaisePropertyChanged(() => Dial3);
            }
            get { return m_Dial3; }
        }

        public double Dial4
        {
            set
            {
                if (value.Equals(m_Dial4))
                    return;

                m_Dial4 = value;
                RaisePropertyChanged(() => Dial4);
            }
            get { return m_Dial4; }
        }
    }
}
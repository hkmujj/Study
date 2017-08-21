
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Model.Domain.Constant;

namespace Subway.TCMS.LanZhou.Model.Domain.Other
{
    public class CarCommunicationStatusData : NotificationObject
    {
        private CarCommunicationStatus m_CommunicationHmi1;
        private CarCommunicationStatus m_CommunicationCcu1;
        private CarCommunicationStatus m_CommunicationErm1;
        private CarCommunicationStatus m_CommunicationPIS1;
        private CarCommunicationStatus m_CommunicationFas1;
        private CarCommunicationStatus m_CommunicationDcua1;

        private CarCommunicationStatus m_CommunicationRiom1;
        private CarCommunicationStatus m_CommunicationRiom7;
        private CarCommunicationStatus m_CommunicationHcac1;
        private CarCommunicationStatus m_CommunicationEdcu1;
        private CarCommunicationStatus m_CommunicationEdcu7;
        private CarCommunicationStatus m_CommunicationBCU1;
        private CarCommunicationStatus m_CommunicationTds;

        private CarCommunicationStatus m_CommunicationEcr1;
        private CarCommunicationStatus m_CommunicationRiom2;
        private CarCommunicationStatus m_CommunicationHcac2;
        private CarCommunicationStatus m_CommunicationEdcu2;
        private CarCommunicationStatus m_CommunicationEdcu8;
        private CarCommunicationStatus m_CommunicationDcum1;

        private CarCommunicationStatus m_CommunicationPds1;
        private CarCommunicationStatus m_CommunicationRiom3;
        private CarCommunicationStatus m_CommunicationHcac3;
        private CarCommunicationStatus m_CommunicationEdcu3;
        private CarCommunicationStatus m_CommunicationEdcu9;
        private CarCommunicationStatus m_CommunicationBCU2;
        private CarCommunicationStatus m_CommunicationDcua2;
        private CarCommunicationStatus m_CommunicationDcum2;
        private CarCommunicationStatus m_CommunicationCcud1;

        private CarCommunicationStatus m_CommunicationPds2;
        private CarCommunicationStatus m_CommunicationRiom4;
        private CarCommunicationStatus m_CommunicationHcac4;
        private CarCommunicationStatus m_CommunicationEdcu4;
        private CarCommunicationStatus m_CommunicationEdcu10;
        private CarCommunicationStatus m_CommunicationBCU3;
        private CarCommunicationStatus m_CommunicationDcua3;
        private CarCommunicationStatus m_CommunicationDcum3;
        private CarCommunicationStatus m_CommunicationCcud2;

        private CarCommunicationStatus m_CommunicationEcr2;
        private CarCommunicationStatus m_CommunicationRiom5;
        private CarCommunicationStatus m_CommunicationHcac5;
        private CarCommunicationStatus m_CommunicationEdcu5;
        private CarCommunicationStatus m_CommunicationEdcu11;
        private CarCommunicationStatus m_CommunicationDcum4;

        private CarCommunicationStatus m_CommunicationRiom6;
        private CarCommunicationStatus m_CommunicationRiom8;
        private CarCommunicationStatus m_CommunicationHcac6;
        private CarCommunicationStatus m_CommunicationEdcu6;
        private CarCommunicationStatus m_CommunicationEdcu12;
        private CarCommunicationStatus m_CommunicationBCU4;

        private CarCommunicationStatus m_CommunicationHmi2;
        private CarCommunicationStatus m_CommunicationCcu2;
        private CarCommunicationStatus m_CommunicationErm2;
        private CarCommunicationStatus m_CommunicationPIS2;
        private CarCommunicationStatus m_CommunicationFas2;
        private CarCommunicationStatus m_CommunicationAtc;
        private CarCommunicationStatus m_CommunicationDcua4;


      

        public CarCommunicationStatus CommunicationHmi1
        {
            get { return m_CommunicationHmi1; }
            set
            {
                if (value.Equals(m_CommunicationHmi1))
                {
                    return;
                }
                m_CommunicationHmi1 = value;
                RaisePropertyChanged(() => CommunicationHmi1);
            }
        }
        public CarCommunicationStatus CommunicationCcu1
        {
            get { return m_CommunicationCcu1; }
            set
            {
                if (value.Equals(m_CommunicationCcu1))
                {
                    return;
                }
                m_CommunicationCcu1 = value;
                RaisePropertyChanged(() => CommunicationCcu1);
            }
        }
        public CarCommunicationStatus CommunicationErm1
        {
            get { return m_CommunicationErm1; }
            set
            {
                if (value.Equals(m_CommunicationErm1))
                {
                    return;
                }
                m_CommunicationErm1 = value;
                RaisePropertyChanged(() => CommunicationErm1);
            }
        }
        public CarCommunicationStatus CommunicationPIS1
        {
            get { return m_CommunicationPIS1; }
            set
            {
                if (value.Equals(m_CommunicationPIS1))
                {
                    return;
                }
                m_CommunicationPIS1 = value;
                RaisePropertyChanged(() => CommunicationPIS1);
            }
        }
        public CarCommunicationStatus CommunicationFas1
        {
            get { return m_CommunicationFas1; }
            set
            {
                if (value.Equals(m_CommunicationFas1))
                {
                    return;
                }
                m_CommunicationFas1 = value;
                RaisePropertyChanged(() => CommunicationFas1);
            }
        }
        public CarCommunicationStatus CommunicationDcua1
        {
            get { return m_CommunicationDcua1; }
            set
            {
                if (value.Equals(m_CommunicationDcua1))
                {
                    return;
                }
                m_CommunicationDcua1 = value;
                RaisePropertyChanged(() => CommunicationDcua1);
            }
        }




        public CarCommunicationStatus CommunicationRiom1
        {
            get { return m_CommunicationRiom1; }
            set
            {
                if (value.Equals(m_CommunicationRiom1))
                {
                    return;
                }
                m_CommunicationRiom1 = value;
                RaisePropertyChanged(() => CommunicationRiom1);
            }
        }
        public CarCommunicationStatus CommunicationRiom7
        {
            get { return m_CommunicationRiom7; }
            set
            {
                if (value.Equals(m_CommunicationRiom7))
                {
                    return;
                }
                m_CommunicationRiom7 = value;
                RaisePropertyChanged(() => CommunicationRiom7);
            }
        }
        public CarCommunicationStatus CommunicationHcac1
        {
            get { return m_CommunicationHcac1; }
            set
            {
                if (value.Equals(m_CommunicationHcac1))
                {
                    return;
                }
                m_CommunicationHcac1 = value;
                RaisePropertyChanged(() => CommunicationHcac1);
            }
        }
        public CarCommunicationStatus CommunicationEdcu1
        {
            get { return m_CommunicationEdcu1; }
            set
            {
                if (value.Equals(m_CommunicationEdcu1))
                {
                    return;
                }
                m_CommunicationEdcu1 = value;
                RaisePropertyChanged(() => CommunicationEdcu1);
            }
        }
        public CarCommunicationStatus CommunicationEdcu7
        {
            get { return m_CommunicationEdcu7; }
            set
            {
                if (value.Equals(m_CommunicationEdcu7))
                {
                    return;
                }
                m_CommunicationEdcu7 = value;
                RaisePropertyChanged(() => CommunicationEdcu7);
            }
        }
        public CarCommunicationStatus CommunicationBCU1
        {
            get { return m_CommunicationBCU1; }
            set
            {
                if (value.Equals(m_CommunicationBCU1))
                {
                    return;
                }
                m_CommunicationBCU1 = value;
                RaisePropertyChanged(() => CommunicationBCU1);
            }
        }
        public CarCommunicationStatus CommunicationTds
        {
            get { return m_CommunicationTds; }
            set
            {
                if (value.Equals(m_CommunicationTds))
                {
                    return;
                }
                m_CommunicationTds = value;
                RaisePropertyChanged(() => CommunicationTds);
            }
        }




        public CarCommunicationStatus CommunicationEcr1
        {
            get { return m_CommunicationEcr1; }
            set
            {
                if (value.Equals(m_CommunicationEcr1))
                {
                    return;
                }
                m_CommunicationEcr1 = value;
                RaisePropertyChanged(() => CommunicationEcr1);
            }
        }
        public CarCommunicationStatus CommunicationRiom2
        {
            get { return m_CommunicationRiom2; }
            set
            {
                if (value.Equals(m_CommunicationRiom2))
                {
                    return;
                }
                m_CommunicationRiom2 = value;
                RaisePropertyChanged(() => CommunicationRiom2);
            }
        }
        public CarCommunicationStatus CommunicationHcac2
        {
            get { return m_CommunicationHcac2; }
            set
            {
                if (value.Equals(m_CommunicationHcac2))
                {
                    return;
                }
                m_CommunicationHcac2 = value;
                RaisePropertyChanged(() => CommunicationHcac2);
            }
        }
        public CarCommunicationStatus CommunicationEdcu2
        {
            get { return m_CommunicationEdcu2; }
            set
            {
                if (value.Equals(m_CommunicationEdcu2))
                {
                    return;
                }
                m_CommunicationEdcu2 = value;
                RaisePropertyChanged(() => CommunicationEdcu2);
            }
        }
        public CarCommunicationStatus CommunicationEdcu8
        {
            get { return m_CommunicationEdcu8; }
            set
            {
                if (value.Equals(m_CommunicationEdcu8))
                {
                    return;
                }
                m_CommunicationEdcu8 = value;
                RaisePropertyChanged(() => CommunicationEdcu8);
            }
        }
        public CarCommunicationStatus CommunicationDcum1
        {
            get { return m_CommunicationDcum1; }
            set
            {
                if (value.Equals(m_CommunicationDcum1))
                {
                    return;
                }
                m_CommunicationDcum1 = value;
                RaisePropertyChanged(() => CommunicationDcum1);
            }
        }




        public CarCommunicationStatus CommunicationPds1
        {
            get { return m_CommunicationPds1; }
            set
            {
                if (value.Equals(m_CommunicationPds1))
                {
                    return;
                }
                m_CommunicationPds1 = value;
                RaisePropertyChanged(() => CommunicationPds1);
            }
        }
        public CarCommunicationStatus CommunicationRiom3
        {
            get { return m_CommunicationRiom3; }
            set
            {
                if (value.Equals(m_CommunicationRiom3))
                {
                    return;
                }
                m_CommunicationRiom3 = value;
                RaisePropertyChanged(() => CommunicationRiom3);
            }
        }
        public CarCommunicationStatus CommunicationHcac3
        {
            get { return m_CommunicationHcac3; }
            set
            {
                if (value.Equals(m_CommunicationHcac3))
                {
                    return;
                }
                m_CommunicationHcac3 = value;
                RaisePropertyChanged(() => CommunicationHcac3);
            }
        }
        public CarCommunicationStatus CommunicationEdcu3
        {
            get { return m_CommunicationEdcu3; }
            set
            {
                if (value.Equals(m_CommunicationEdcu3))
                {
                    return;
                }
                m_CommunicationEdcu3 = value;
                RaisePropertyChanged(() => CommunicationEdcu3);
            }
        }
        public CarCommunicationStatus CommunicationEdcu9
        {
            get { return m_CommunicationEdcu9; }
            set
            {
                if (value.Equals(m_CommunicationEdcu9))
                {
                    return;
                }
                m_CommunicationEdcu9 = value;
                RaisePropertyChanged(() => CommunicationEdcu9);
            }
        }
        public CarCommunicationStatus CommunicationBCU2
        {
            get { return m_CommunicationBCU2; }
            set
            {
                if (value.Equals(m_CommunicationBCU2))
                {
                    return;
                }
                m_CommunicationBCU2 = value;
                RaisePropertyChanged(() => CommunicationBCU2);
            }
        }
        public CarCommunicationStatus CommunicationDcua2
        {
            get { return m_CommunicationDcua2; }
            set
            {
                if (value.Equals(m_CommunicationDcua2))
                {
                    return;
                }
                m_CommunicationDcua2 = value;
                RaisePropertyChanged(() => CommunicationDcua2);
            }
        }
        public CarCommunicationStatus CommunicationDcum2
        {
            get { return m_CommunicationDcum2; }
            set
            {
                if (value.Equals(m_CommunicationDcum2))
                {
                    return;
                }
                m_CommunicationDcum2 = value;
                RaisePropertyChanged(() => CommunicationDcum2);
            }
        }
        public CarCommunicationStatus CommunicationCcud1
        {
            get { return m_CommunicationCcud1; }
            set
            {
                if (value.Equals(m_CommunicationCcud1))
                {
                    return;
                }
                m_CommunicationCcud1 = value;
                RaisePropertyChanged(() => CommunicationCcud1);
            }
        }




        public CarCommunicationStatus CommunicationPds2
        {
            get { return m_CommunicationPds2; }
            set
            {
                if (value.Equals(m_CommunicationPds2))
                {
                    return;
                }
                m_CommunicationPds2 = value;
                RaisePropertyChanged(() => CommunicationPds2);
            }
        }
        public CarCommunicationStatus CommunicationRiom4
        {
            get { return m_CommunicationRiom4; }
            set
            {
                if (value.Equals(m_CommunicationRiom4))
                {
                    return;
                }
                m_CommunicationRiom4 = value;
                RaisePropertyChanged(() => CommunicationRiom4);
            }
        }
        public CarCommunicationStatus CommunicationHcac4
        {
            get { return m_CommunicationHcac4; }
            set
            {
                if (value.Equals(m_CommunicationHcac4))
                {
                    return;
                }
                m_CommunicationHcac4 = value;
                RaisePropertyChanged(() => CommunicationHcac4);
            }
        }
        public CarCommunicationStatus CommunicationEdcu4
        {
            get { return m_CommunicationEdcu4; }
            set
            {
                if (value.Equals(m_CommunicationEdcu4))
                {
                    return;
                }
                m_CommunicationEdcu4 = value;
                RaisePropertyChanged(() => CommunicationEdcu4);
            }
        }
        public CarCommunicationStatus CommunicationEdcu10
        {
            get { return m_CommunicationEdcu10; }
            set
            {
                if (value.Equals(m_CommunicationEdcu10))
                {
                    return;
                }
                m_CommunicationEdcu10 = value;
                RaisePropertyChanged(() => CommunicationEdcu10);
            }
        }
        public CarCommunicationStatus CommunicationBCU3
        {
            get { return m_CommunicationBCU3; }
            set
            {
                if (value.Equals(m_CommunicationBCU3))
                {
                    return;
                }
                m_CommunicationBCU3 = value;
                RaisePropertyChanged(() => CommunicationBCU3);
            }
        }
        public CarCommunicationStatus CommunicationDcua3
        {
            get { return m_CommunicationDcua3; }
            set
            {
                if (value.Equals(m_CommunicationDcua3))
                {
                    return;
                }
                m_CommunicationDcua3 = value;
                RaisePropertyChanged(() => CommunicationDcua3);
            }
        }
        public CarCommunicationStatus CommunicationDcum3
        {
            get { return m_CommunicationDcum3; }
            set
            {
                if (value.Equals(m_CommunicationDcum3))
                {
                    return;
                }
                m_CommunicationDcum3 = value;
                RaisePropertyChanged(() => CommunicationDcum3);
            }
        }
        public CarCommunicationStatus CommunicationCcud2
        {
            get { return m_CommunicationCcud2; }
            set
            {
                if (value.Equals(m_CommunicationCcud2))
                {
                    return;
                }
                m_CommunicationCcud2 = value;
                RaisePropertyChanged(() => CommunicationCcud2);
            }
        }




        public CarCommunicationStatus CommunicationEcr2
        {
            get { return m_CommunicationEcr2; }
            set
            {
                if (value.Equals(m_CommunicationEcr2))
                {
                    return;
                }
                m_CommunicationEcr2 = value;
                RaisePropertyChanged(() => CommunicationEcr2);
            }
        }
        public CarCommunicationStatus CommunicationRiom5
        {
            get { return m_CommunicationRiom5; }
            set
            {
                if (value.Equals(m_CommunicationRiom5))
                {
                    return;
                }
                m_CommunicationRiom5 = value;
                RaisePropertyChanged(() => CommunicationRiom5);
            }
        }
        public CarCommunicationStatus CommunicationHcac5
        {
            get { return m_CommunicationHcac5; }
            set
            {
                if (value.Equals(m_CommunicationHcac5))
                {
                    return;
                }
                m_CommunicationHcac5 = value;
                RaisePropertyChanged(() => CommunicationHcac5);
            }
        }
        public CarCommunicationStatus CommunicationEdcu5
        {
            get { return m_CommunicationEdcu5; }
            set
            {
                if (value.Equals(m_CommunicationEdcu5))
                {
                    return;
                }
                m_CommunicationEdcu5 = value;
                RaisePropertyChanged(() => CommunicationEdcu5);
            }
        }
        public CarCommunicationStatus CommunicationEdcu11
        {
            get { return m_CommunicationEdcu11; }
            set
            {
                if (value.Equals(m_CommunicationEdcu11))
                {
                    return;
                }
                m_CommunicationEdcu11 = value;
                RaisePropertyChanged(() => CommunicationEdcu11);
            }
        }
        public CarCommunicationStatus CommunicationDcum4
        {
            get { return m_CommunicationDcum4; }
            set
            {
                if (value.Equals(m_CommunicationDcum4))
                {
                    return;
                }
                m_CommunicationDcum4 = value;
                RaisePropertyChanged(() => CommunicationDcum4);
            }
        }



        public CarCommunicationStatus CommunicationRiom6
        {
            get { return m_CommunicationRiom6; }
            set
            {
                if (value.Equals(m_CommunicationRiom6))
                {
                    return;
                }
                m_CommunicationRiom6 = value;
                RaisePropertyChanged(() => CommunicationRiom6);
            }
        }
        public CarCommunicationStatus CommunicationRiom8
        {
            get { return m_CommunicationRiom8; }
            set
            {
                if (value.Equals(m_CommunicationRiom8))
                {
                    return;
                }
                m_CommunicationRiom8 = value;
                RaisePropertyChanged(() => CommunicationRiom8);
            }
        }
        public CarCommunicationStatus CommunicationHcac6
        {
            get { return m_CommunicationHcac6; }
            set
            {
                if (value.Equals(m_CommunicationHcac6))
                {
                    return;
                }
                m_CommunicationHcac6 = value;
                RaisePropertyChanged(() => CommunicationHcac6);
            }
        }
        public CarCommunicationStatus CommunicationEdcu6
        {
            get { return m_CommunicationEdcu6; }
            set
            {
                if (value.Equals(m_CommunicationEdcu6))
                {
                    return;
                }
                m_CommunicationEdcu6 = value;
                RaisePropertyChanged(() => CommunicationEdcu6);
            }
        }
        public CarCommunicationStatus CommunicationEdcu12
        {
            get { return m_CommunicationEdcu12; }
            set
            {
                if (value.Equals(m_CommunicationEdcu12))
                {
                    return;
                }
                m_CommunicationEdcu12 = value;
                RaisePropertyChanged(() => CommunicationEdcu12);
            }
        }
        public CarCommunicationStatus CommunicationBCU4
        {
            get { return m_CommunicationBCU4; }
            set
            {
                if (value.Equals(m_CommunicationBCU4))
                {
                    return;
                }
                m_CommunicationBCU4 = value;
                RaisePropertyChanged(() => CommunicationBCU4);
            }
        }




        public CarCommunicationStatus CommunicationHmi2
        {
            get { return m_CommunicationHmi2; }
            set
            {
                if (value.Equals(m_CommunicationHmi2))
                {
                    return;
                }
                m_CommunicationHmi2 = value;
                RaisePropertyChanged(() => CommunicationHmi2);
            }
        }
        public CarCommunicationStatus CommunicationCcu2
        {
            get { return m_CommunicationCcu2; }
            set
            {
                if (value.Equals(m_CommunicationCcu2))
                {
                    return;
                }
                m_CommunicationCcu2 = value;
                RaisePropertyChanged(() => CommunicationCcu2);
            }
        }
        public CarCommunicationStatus CommunicationErm2
        {
            get { return m_CommunicationErm2; }
            set
            {
                if (value.Equals(m_CommunicationErm2))
                {
                    return;
                }
                m_CommunicationErm2 = value;
                RaisePropertyChanged(() => CommunicationErm2);
            }
        }
        public CarCommunicationStatus CommunicationPIS2
        {
            get { return m_CommunicationPIS2; }
            set
            {
                if (value.Equals(m_CommunicationPIS2))
                {
                    return;
                }
                m_CommunicationPIS2 = value;
                RaisePropertyChanged(() => CommunicationPIS2);
            }
        }
        public CarCommunicationStatus CommunicationFas2
        {
            get { return m_CommunicationFas2; }
            set
            {
                if (value.Equals(m_CommunicationFas2))
                {
                    return;
                }
                m_CommunicationFas2 = value;
                RaisePropertyChanged(() => CommunicationFas2);
            }
        }
        public CarCommunicationStatus CommunicationAtc
        {
            get { return m_CommunicationAtc; }
            set
            {
                if (value.Equals(m_CommunicationAtc))
                {
                    return;
                }
                m_CommunicationAtc = value;
                RaisePropertyChanged(() => CommunicationAtc);
            }
        }

        public CarCommunicationStatus CommunicationDcua4
        {
            get { return m_CommunicationDcua4; }
            set
            {
                if (value.Equals(m_CommunicationDcua4))
                {
                    return;
                }
                m_CommunicationDcua4 = value;
                RaisePropertyChanged(() => CommunicationDcua4);
            }
        }
    }
}

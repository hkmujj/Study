using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Maintenance.Train.Detail
{
    public class PortDataItem : NotificationObject
    {
        private int m_D15;
        private int m_D14;
        private int m_D13;
        private int m_D12;
        private int m_D11;
        private int m_D10;
        private int m_D9;
        private int m_D8;
        private int m_D7;
        private int m_D6;
        private int m_D5;
        private int m_D4;
        private int m_D3;
        private int m_D2;
        private int m_D1;
        private int m_D0;

        [DebuggerStepThrough]
        public PortDataItem(int port)
        {
            Port = port;
        }

        public int Port { get; private set; }

        public int D0
        {
            get { return m_D0; }
            set
            {
                if (value == m_D0)
                {
                    return;
                }

                m_D0 = value;
                RaisePropertyChanged(() => D0);
            }
        }

        public int D1
        {
            get { return m_D1; }
            set
            {
                if (value == m_D1)
                {
                    return;
                }

                m_D1 = value;
                RaisePropertyChanged(() => D1);
            }
        }

        public int D2
        {
            get { return m_D2; }
            set
            {
                if (value == m_D2)
                {
                    return;
                }

                m_D2 = value;
                RaisePropertyChanged(() => D2);
            }
        }

        public int D3
        {
            get { return m_D3; }
            set
            {
                if (value == m_D3)
                {
                    return;
                }

                m_D3 = value;
                RaisePropertyChanged(() => D3);
            }
        }

        public int D4
        {
            get { return m_D4; }
            set
            {
                if (value == m_D4)
                {
                    return;
                }

                m_D4 = value;
                RaisePropertyChanged(() => D4);
            }
        }

        public int D5
        {
            get { return m_D5; }
            set
            {
                if (value == m_D5)
                {
                    return;
                }

                m_D5 = value;
                RaisePropertyChanged(() => D5);
            }
        }

        public int D6
        {
            get { return m_D6; }
            set
            {
                if (value == m_D6)
                {
                    return;
                }

                m_D6 = value;
                RaisePropertyChanged(() => D6);
            }
        }

        public int D7
        {
            get { return m_D7; }
            set
            {
                if (value == m_D7)
                {
                    return;
                }

                m_D7 = value;
                RaisePropertyChanged(() => D7);
            }
        }

        public int D8
        {
            get { return m_D8; }
            set
            {
                if (value == m_D8)
                {
                    return;
                }

                m_D8 = value;
                RaisePropertyChanged(() => D8);
            }
        }

        public int D9
        {
            get { return m_D9; }
            set
            {
                if (value == m_D9)
                {
                    return;
                }

                m_D9 = value;
                RaisePropertyChanged(() => D9);
            }
        }

        public int D10
        {
            get { return m_D10; }
            set
            {
                if (value == m_D10)
                {
                    return;
                }

                m_D10 = value;
                RaisePropertyChanged(() => D10);
            }
        }

        public int D11
        {
            get { return m_D11; }
            set
            {
                if (value == m_D11)
                {
                    return;
                }

                m_D11 = value;
                RaisePropertyChanged(() => D11);
            }
        }

        public int D12
        {
            get { return m_D12; }
            set
            {
                if (value == m_D12)
                {
                    return;
                }

                m_D12 = value;
                RaisePropertyChanged(() => D12);
            }
        }

        public int D13
        {
            get { return m_D13; }
            set
            {
                if (value == m_D13)
                {
                    return;
                }

                m_D13 = value;
                RaisePropertyChanged(() => D13);
            }
        }

        public int D14
        {
            get { return m_D14; }
            set
            {
                if (value == m_D14)
                {
                    return;
                }

                m_D14 = value;
                RaisePropertyChanged(() => D14);
            }
        }

        public int D15
        {
            get { return m_D15; }
            set
            {
                if (value == m_D15)
                {
                    return;
                }

                m_D15 = value;
                RaisePropertyChanged(() => D15);
            }
        }
    }
}
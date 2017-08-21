using System.ComponentModel.Composition;
using Engine._6A.Constance;
using Engine._6A.Interface.ViewModel.DataMonitor;
using Engine._6A.ViewModel.Common;

namespace Engine._6A.ViewModel.Axle6
{
    [Export(ContractName.Axle6, typeof(IRunLineOneViewModelBase))]
    [Export(ContractName.Axle8, typeof(IRunLineOneViewModelBase))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RunLineOneViewModel : ViewModelBase, IRunLineOneViewModelBase
    {
        private string m_Axle6Bit6;
        private string m_Axle5Bit6;
        private string m_Axle4Bit6;
        private string m_Axle3Bit6;
        private string m_Axle2Bit6;
        private string m_Axle1Bit6;
        private string m_Axle6Bit5;
        private string m_Axle5Bit5;
        private string m_Axle4Bit5;
        private string m_Axle3Bit5;
        private string m_Axle2Bit5;
        private string m_Axle1Bit5;
        private string m_Axle6Bit4;
        private string m_Axle5Bit4;
        private string m_Axle4Bit4;
        private string m_Axle3Bit4;
        private string m_Axle2Bit4;
        private string m_Axle1Bit4;
        private string m_Axle6Bit3;
        private string m_Axle5Bit3;
        private string m_Axle4Bit3;
        private string m_Axle3Bit3;
        private string m_Axle2Bit3;
        private string m_Axle1Bit3;
        private string m_Axle6Bit2;
        private string m_Axle5Bit2;
        private string m_Axle4Bit2;
        private string m_Axle3Bit2;
        private string m_Axle2Bit2;
        private string m_Axle1Bit2;
        private string m_Axle6Bit1;
        private string m_Axle5Bit1;
        private string m_Axle4Bit1;
        private string m_Axle3Bit1;
        private string m_Axle2Bit1;
        private string m_Axle1Bit1;
        private string m_Axle8Bit6;
        private string m_Axle8Bit5;
        private string m_Axle8Bit4;
        private string m_Axle8Bit3;
        private string m_Axle8Bit2;
        private string m_Axle8Bit1;
        private string m_Axle7Bit6;
        private string m_Axle7Bit5;
        private string m_Axle7Bit4;
        private string m_Axle7Bit3;
        private string m_Axle7Bit2;
        private string m_Axle7Bit1;

        public RunLineOneViewModel()
        {
            
        }
        public string Axle1Bit1
        {
            get { return m_Axle1Bit1; }
            set
            {
                if (value == m_Axle1Bit1)
                {
                    return;
                }
                m_Axle1Bit1 = value;
                RaisePropertyChanged(() => Axle1Bit1);
            }
        }

        public string Axle2Bit1
        {
            get { return m_Axle2Bit1; }
            set
            {
                if (value == m_Axle2Bit1)
                {
                    return;
                }
                m_Axle2Bit1 = value;
                RaisePropertyChanged(() => Axle2Bit1);
            }
        }

        public string Axle3Bit1
        {
            get { return m_Axle3Bit1; }
            set
            {
                if (value == m_Axle3Bit1)
                {
                    return;
                }
                m_Axle3Bit1 = value;
                RaisePropertyChanged(() => Axle3Bit1);
            }
        }

        public string Axle4Bit1
        {
            get { return m_Axle4Bit1; }
            set
            {
                if (value == m_Axle4Bit1)
                {
                    return;
                }
                m_Axle4Bit1 = value;
                RaisePropertyChanged(() => Axle4Bit1);
            }
        }

        public string Axle5Bit1
        {
            get { return m_Axle5Bit1; }
            set
            {
                if (value == m_Axle5Bit1)
                {
                    return;
                }
                m_Axle5Bit1 = value;
                RaisePropertyChanged(() => Axle5Bit1);
            }
        }

        public string Axle6Bit1
        {
            get { return m_Axle6Bit1; }
            set
            {
                if (value == m_Axle6Bit1)
                {
                    return;
                }
                m_Axle6Bit1 = value;
                RaisePropertyChanged(() => Axle6Bit1);
            }
        }

        public string Axle1Bit2
        {
            get { return m_Axle1Bit2; }
            set
            {
                if (value == m_Axle1Bit2)
                {
                    return;
                }
                m_Axle1Bit2 = value;
                RaisePropertyChanged(() => Axle1Bit2);
            }
        }

        public string Axle2Bit2
        {
            get { return m_Axle2Bit2; }
            set
            {
                if (value == m_Axle2Bit2)
                {
                    return;
                }
                m_Axle2Bit2 = value;
                RaisePropertyChanged(() => Axle2Bit2);
            }
        }

        public string Axle3Bit2
        {
            get { return m_Axle3Bit2; }
            set
            {
                if (value == m_Axle3Bit2)
                {
                    return;
                }
                m_Axle3Bit2 = value;
                RaisePropertyChanged(() => Axle3Bit2);
            }
        }

        public string Axle4Bit2
        {
            get { return m_Axle4Bit2; }
            set
            {
                if (value == m_Axle4Bit2)
                {
                    return;
                }
                m_Axle4Bit2 = value;
                RaisePropertyChanged(() => Axle4Bit2);
            }
        }

        public string Axle5Bit2
        {
            get { return m_Axle5Bit2; }
            set
            {
                if (value == m_Axle5Bit2)
                {
                    return;
                }
                m_Axle5Bit2 = value;
                RaisePropertyChanged(() => Axle5Bit2);
            }
        }

        public string Axle6Bit2
        {
            get { return m_Axle6Bit2; }
            set
            {
                if (value == m_Axle6Bit2)
                {
                    return;
                }
                m_Axle6Bit2 = value;
                RaisePropertyChanged(() => Axle6Bit2);
            }
        }

        public string Axle1Bit3
        {
            get { return m_Axle1Bit3; }
            set
            {
                if (value == m_Axle1Bit3)
                {
                    return;
                }
                m_Axle1Bit3 = value;
                RaisePropertyChanged(() => Axle1Bit3);
            }
        }

        public string Axle2Bit3
        {
            get { return m_Axle2Bit3; }
            set
            {
                if (value == m_Axle2Bit3)
                {
                    return;
                }
                m_Axle2Bit3 = value;
                RaisePropertyChanged(() => Axle2Bit3);
            }
        }

        public string Axle3Bit3
        {
            get { return m_Axle3Bit3; }
            set
            {
                if (value == m_Axle3Bit3)
                {
                    return;
                }
                m_Axle3Bit3 = value;
                RaisePropertyChanged(() => Axle3Bit3);
            }
        }

        public string Axle4Bit3
        {
            get { return m_Axle4Bit3; }
            set
            {
                if (value == m_Axle4Bit3)
                {
                    return;
                }
                m_Axle4Bit3 = value;
                RaisePropertyChanged(() => Axle4Bit3);
            }
        }

        public string Axle5Bit3
        {
            get { return m_Axle5Bit3; }
            set
            {
                if (value == m_Axle5Bit3)
                {
                    return;
                }
                m_Axle5Bit3 = value;
                RaisePropertyChanged(() => Axle5Bit3);
            }
        }

        public string Axle6Bit3
        {
            get { return m_Axle6Bit3; }
            set
            {
                if (value == m_Axle6Bit3)
                {
                    return;
                }
                m_Axle6Bit3 = value;
                RaisePropertyChanged(() => Axle6Bit3);
            }
        }

        public string Axle1Bit4
        {
            get { return m_Axle1Bit4; }
            set
            {
                if (value == m_Axle1Bit4)
                {
                    return;
                }
                m_Axle1Bit4 = value;
                RaisePropertyChanged(() => Axle1Bit4);
            }
        }

        public string Axle2Bit4
        {
            get { return m_Axle2Bit4; }
            set
            {
                if (value == m_Axle2Bit4)
                {
                    return;
                }
                m_Axle2Bit4 = value;
                RaisePropertyChanged(() => Axle2Bit4);
            }
        }

        public string Axle3Bit4
        {
            get { return m_Axle3Bit4; }
            set
            {
                if (value == m_Axle3Bit4)
                {
                    return;
                }
                m_Axle3Bit4 = value;
                RaisePropertyChanged(() => Axle3Bit4);
            }
        }

        public string Axle4Bit4
        {
            get { return m_Axle4Bit4; }
            set
            {
                if (value == m_Axle4Bit4)
                {
                    return;
                }
                m_Axle4Bit4 = value;
                RaisePropertyChanged(() => Axle4Bit4);
            }
        }

        public string Axle5Bit4
        {
            get { return m_Axle5Bit4; }
            set
            {
                if (value == m_Axle5Bit4)
                {
                    return;
                }
                m_Axle5Bit4 = value;
                RaisePropertyChanged(() => Axle5Bit4);
            }
        }

        public string Axle6Bit4
        {
            get { return m_Axle6Bit4; }
            set
            {
                if (value == m_Axle6Bit4)
                {
                    return;
                }
                m_Axle6Bit4 = value;
                RaisePropertyChanged(() => Axle6Bit4);
            }
        }

        public string Axle1Bit5
        {
            get { return m_Axle1Bit5; }
            set
            {
                if (value == m_Axle1Bit5)
                {
                    return;
                }
                m_Axle1Bit5 = value;
                RaisePropertyChanged(() => Axle1Bit5);
            }
        }

        public string Axle2Bit5
        {
            get { return m_Axle2Bit5; }
            set
            {
                if (value == m_Axle2Bit5)
                {
                    return;
                }
                m_Axle2Bit5 = value;
                RaisePropertyChanged(() => Axle2Bit5);
            }
        }

        public string Axle3Bit5
        {
            get { return m_Axle3Bit5; }
            set
            {
                if (value == m_Axle3Bit5)
                {
                    return;
                }
                m_Axle3Bit5 = value;
                RaisePropertyChanged(() => Axle3Bit5);
            }
        }

        public string Axle4Bit5
        {
            get { return m_Axle4Bit5; }
            set
            {
                if (value == m_Axle4Bit5)
                {
                    return;
                }
                m_Axle4Bit5 = value;
                RaisePropertyChanged(() => Axle4Bit5);
            }
        }

        public string Axle5Bit5
        {
            get { return m_Axle5Bit5; }
            set
            {
                if (value == m_Axle5Bit5)
                {
                    return;
                }
                m_Axle5Bit5 = value;
                RaisePropertyChanged(() => Axle5Bit5);
            }
        }

        public string Axle6Bit5
        {
            get { return m_Axle6Bit5; }
            set
            {
                if (value == m_Axle6Bit5)
                {
                    return;
                }
                m_Axle6Bit5 = value;
                RaisePropertyChanged(() => Axle6Bit5);
            }
        }

        public string Axle7Bit1
        {
            get { return m_Axle7Bit1; }
            set
            {
                if (value == m_Axle7Bit1)
                {
                    return;
                }
                m_Axle7Bit1 = value;
                RaisePropertyChanged(() => Axle7Bit1);
            }
        }

        public string Axle7Bit2
        {
            get { return m_Axle7Bit2; }
            set
            {
                if (value == m_Axle7Bit2)
                {
                    return;
                }
                m_Axle7Bit2 = value;
                RaisePropertyChanged(() => Axle7Bit2);
            }
        }

        public string Axle7Bit3
        {
            get { return m_Axle7Bit3; }
            set
            {
                if (value == m_Axle7Bit3)
                {
                    return;
                }
                m_Axle7Bit3 = value;
                RaisePropertyChanged(() => Axle7Bit3);
            }
        }

        public string Axle7Bit4
        {
            get { return m_Axle7Bit4; }
            set
            {
                if (value == m_Axle7Bit4)
                {
                    return;
                }
                m_Axle7Bit4 = value;
                RaisePropertyChanged(() => Axle7Bit4);
            }
        }

        public string Axle7Bit5
        {
            get { return m_Axle7Bit5; }
            set
            {
                if (value == m_Axle7Bit5)
                {
                    return;
                }
                m_Axle7Bit5 = value;
                RaisePropertyChanged(() => Axle7Bit5);
            }
        }

        public string Axle7Bit6
        {
            get { return m_Axle7Bit6; }
            set
            {
                if (value == m_Axle7Bit6)
                {
                    return;
                }
                m_Axle7Bit6 = value;
                RaisePropertyChanged(() => Axle7Bit6);
            }
        }

        public string Axle8Bit1
        {
            get { return m_Axle8Bit1; }
            set
            {
                if (value == m_Axle8Bit1)
                {
                    return;
                }
                m_Axle8Bit1 = value;
                RaisePropertyChanged(() => Axle8Bit1);
            }
        }

        public string Axle8Bit2
        {
            get { return m_Axle8Bit2; }
            set
            {
                if (value == m_Axle8Bit2)
                {
                    return;
                }
                m_Axle8Bit2 = value;
                RaisePropertyChanged(() => Axle8Bit2);
            }
        }

        public string Axle8Bit3
        {
            get { return m_Axle8Bit3; }
            set
            {
                if (value == m_Axle8Bit3)
                {
                    return;
                }
                m_Axle8Bit3 = value;
                RaisePropertyChanged(() => Axle8Bit3);
            }
        }

        public string Axle8Bit4
        {
            get { return m_Axle8Bit4; }
            set
            {
                if (value == m_Axle8Bit4)
                {
                    return;
                }
                m_Axle8Bit4 = value;
                RaisePropertyChanged(() => Axle8Bit4);
            }
        }

        public string Axle8Bit5
        {
            get { return m_Axle8Bit5; }
            set
            {
                if (value == m_Axle8Bit5)
                {
                    return;
                }
                m_Axle8Bit5 = value;
                RaisePropertyChanged(() => Axle8Bit5);
            }
        }

        public string Axle8Bit6
        {
            get { return m_Axle8Bit6; }
            set
            {
                if (value == m_Axle8Bit6)
                {
                    return;
                }
                m_Axle8Bit6 = value;
                RaisePropertyChanged(() => Axle8Bit6);
            }
        }

        public string Axle1Bit6
        {
            get { return m_Axle1Bit6; }
            set
            {
                if (value == m_Axle1Bit6)
                {
                    return;
                }
                m_Axle1Bit6 = value;
                RaisePropertyChanged(() => Axle1Bit6);
            }
        }

        public string Axle2Bit6
        {
            get { return m_Axle2Bit6; }
            set
            {
                if (value == m_Axle2Bit6)
                {
                    return;
                }
                m_Axle2Bit6 = value;
                RaisePropertyChanged(() => Axle2Bit6);
            }
        }

        public string Axle3Bit6
        {
            get { return m_Axle3Bit6; }
            set
            {
                if (value == m_Axle3Bit6)
                {
                    return;
                }
                m_Axle3Bit6 = value;
                RaisePropertyChanged(() => Axle3Bit6);
            }
        }

        public string Axle4Bit6
        {
            get { return m_Axle4Bit6; }
            set
            {
                if (value == m_Axle4Bit6)
                {
                    return;
                }
                m_Axle4Bit6 = value;
                RaisePropertyChanged(() => Axle4Bit6);
            }
        }

        public string Axle5Bit6
        {
            get { return m_Axle5Bit6; }
            set
            {
                if (value == m_Axle5Bit6)
                {
                    return;
                }
                m_Axle5Bit6 = value;
                RaisePropertyChanged(() => Axle5Bit6);
            }
        }

        public string Axle6Bit6
        {
            get { return m_Axle6Bit6; }
            set
            {
                if (value == m_Axle6Bit6)
                {
                    return;
                }
                m_Axle6Bit6 = value;
                RaisePropertyChanged(() => Axle6Bit6);
            }
        }

    }
}
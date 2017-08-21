using System.ComponentModel.Composition;
using Engine.HMI.SS3B.Interface.ViewState;

namespace Engine.HMI.SS3B.View.ViewModel.LiuZhou
{

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class NetPageViewModel : ViewModelBase
    {
        private NetPageColor m_LCU1ColorOther;
        private NetPageColor m_DCU2ColorOther;
        private NetPageColor m_DCU1ColorOther;
        private NetPageColor m_IDU1ColorOther;
        private NetPageColor m_SCUColorOther;
        private NetPageColor m_LCU2ColorOther;
        private NetPageColor m_TAX2ColorOther;
        private NetPageColor m_IDU2ColorOther;
        private NetPageColor m_CCUColorOther;
        private NetPageColor m_LCU1ColorOringin;
        private NetPageColor m_DCU2ColorOringin;
        private NetPageColor m_DCU1ColorOringin;
        private NetPageColor m_IDU1ColorOringin;
        private NetPageColor m_SCUColorOringin;
        private NetPageColor m_LCU2ColorOringin;
        private NetPageColor m_TAX2ColorOringin;
        private NetPageColor m_IDU2ColorOringin;
        private NetPageColor m_CCUColorOringin;



        public NetPageColor CCUColorOringin
        {
            get { return m_CCUColorOringin; }
            set
            {
                if (value == m_CCUColorOringin)
                {
                    return;
                }
                m_CCUColorOringin = value;
                RaisePropertyChanged(() => CCUColorOringin);
            }
        }

        public NetPageColor IDU2ColorOringin
        {
            get { return m_IDU2ColorOringin; }
            set
            {
                if (value == m_IDU2ColorOringin)
                {
                    return;
                }
                m_IDU2ColorOringin = value;
                RaisePropertyChanged(() => IDU2ColorOringin);
            }
        }

        public NetPageColor TAX2ColorOringin
        {
            get { return m_TAX2ColorOringin; }
            set
            {
                if (value == m_TAX2ColorOringin)
                {
                    return;
                }
                m_TAX2ColorOringin = value;
                RaisePropertyChanged(() => TAX2ColorOringin);
            }
        }

        public NetPageColor LCU2ColorOringin
        {
            get { return m_LCU2ColorOringin; }
            set
            {
                if (value == m_LCU2ColorOringin)
                {
                    return;
                }
                m_LCU2ColorOringin = value;
                RaisePropertyChanged(() => LCU2ColorOringin);
            }
        }

        public NetPageColor SCUColorOringin
        {
            get { return m_SCUColorOringin; }
            set
            {
                if (value == m_SCUColorOringin)
                {
                    return;
                }
                m_SCUColorOringin = value;
                RaisePropertyChanged(() => SCUColorOringin);
            }
        }

        public NetPageColor IDU1ColorOringin
        {
            get { return m_IDU1ColorOringin; }
            set
            {
                if (value == m_IDU1ColorOringin)
                {
                    return;
                }
                m_IDU1ColorOringin = value;
                RaisePropertyChanged(() => IDU1ColorOringin);
            }
        }

        public NetPageColor DCU1ColorOringin
        {
            get { return m_DCU1ColorOringin; }
            set
            {
                if (value == m_DCU1ColorOringin)
                {
                    return;
                }
                m_DCU1ColorOringin = value;
                RaisePropertyChanged(() => DCU1ColorOringin);
            }
        }

        public NetPageColor DCU2ColorOringin
        {
            get { return m_DCU2ColorOringin; }
            set
            {
                if (value == m_DCU2ColorOringin)
                {
                    return;
                }
                m_DCU2ColorOringin = value;
                RaisePropertyChanged(() => DCU2ColorOringin);
            }
        }

        public NetPageColor LCU1ColorOringin
        {
            get { return m_LCU1ColorOringin; }
            set
            {
                if (value == m_LCU1ColorOringin)
                {
                    return;
                }
                m_LCU1ColorOringin = value;
                RaisePropertyChanged(() => LCU1ColorOringin);
            }
        }

        public NetPageColor CCUColorOther
        {
            get { return m_CCUColorOther; }
            set
            {
                if (value == m_CCUColorOther)
                {
                    return;
                }
                m_CCUColorOther = value;
                RaisePropertyChanged(() => CCUColorOther);
            }
        }

        public NetPageColor IDU2ColorOther
        {
            get { return m_IDU2ColorOther; }
            set
            {
                if (value == m_IDU2ColorOther)
                {
                    return;
                }
                m_IDU2ColorOther = value;
                RaisePropertyChanged(() => IDU2ColorOther);
            }
        }

        public NetPageColor TAX2ColorOther
        {
            get { return m_TAX2ColorOther; }
            set
            {
                if (value == m_TAX2ColorOther)
                {
                    return;
                }
                m_TAX2ColorOther = value;
                RaisePropertyChanged(() => TAX2ColorOther);
            }
        }

        public NetPageColor LCU2ColorOther
        {
            get { return m_LCU2ColorOther; }
            set
            {
                if (value == m_LCU2ColorOther)
                {
                    return;
                }
                m_LCU2ColorOther = value;
                RaisePropertyChanged(() => LCU2ColorOther);
            }
        }

        public NetPageColor SCUColorOther
        {
            get { return m_SCUColorOther; }
            set
            {
                if (value == m_SCUColorOther)
                {
                    return;
                }
                m_SCUColorOther = value;
                RaisePropertyChanged(() => SCUColorOther);
            }
        }

        public NetPageColor IDU1ColorOther
        {
            get { return m_IDU1ColorOther; }
            set
            {
                if (value == m_IDU1ColorOther)
                {
                    return;
                }
                m_IDU1ColorOther = value;
                RaisePropertyChanged(() => IDU1ColorOther);
            }
        }

        public NetPageColor DCU1ColorOther
        {
            get { return m_DCU1ColorOther; }
            set
            {
                if (value == m_DCU1ColorOther)
                {
                    return;
                }
                m_DCU1ColorOther = value;
                RaisePropertyChanged(() => DCU1ColorOther);
            }
        }

        public NetPageColor DCU2ColorOther
        {
            get { return m_DCU2ColorOther; }
            set
            {
                if (value == m_DCU2ColorOther)
                {
                    return;
                }
                m_DCU2ColorOther = value;
                RaisePropertyChanged(() => DCU2ColorOther);
            }
        }

        public NetPageColor LCU1ColorOther
        {
            get { return m_LCU1ColorOther; }
            set
            {
                if (value == m_LCU1ColorOther)
                {
                    return;
                }
                m_LCU1ColorOther = value;
                RaisePropertyChanged(() => LCU1ColorOther);
            }
        }
    }
}

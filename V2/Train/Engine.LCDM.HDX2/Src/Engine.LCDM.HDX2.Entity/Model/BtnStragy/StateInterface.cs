using System.Collections.ObjectModel;
using System.Diagnostics;
using Engine.LCDM.HDX2.Entity.Model.Domain;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.LCDM.HDX2.Entity.Model.BtnStragy
{
    [DebuggerDisplay("Id={Id} Title={Title}")]
    public class StateInterface : NotificationObject, IStateInterface, IRaiseResourceChangedProvider
    {
        private BtnItem m_BtnF8;
        private BtnItem m_BtnF7;
        private BtnItem m_BtnF6;
        private BtnItem m_BtnF5;
        private BtnItem m_BtnF4;
        private BtnItem m_BtnF3;
        private BtnItem m_BtnF2;
        private BtnItem m_BtnF1;
        private string m_Title;

        public string Title
        {
            get { return m_Title; }
            set
            {
                if (value == m_Title)
                {
                    return;
                }
                m_Title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public StateInterfaceKey Id { get; set; }

        public BtnItem BtnF1
        {
            get { return m_BtnF1; }
            set
            {
                if (Equals(value, m_BtnF1))
                {
                    return;
                }
                m_BtnF1 = value;
                RaisePropertyChanged(() => BtnF1);
            }
        }

        public BtnItem BtnF2
        {
            get { return m_BtnF2; }
            set
            {
                if (Equals(value, m_BtnF2))
                {
                    return;
                }
                m_BtnF2 = value;
                RaisePropertyChanged(() => BtnF2);
            }
        }

        public BtnItem BtnF3
        {
            get { return m_BtnF3; }
            set
            {
                if (Equals(value, m_BtnF3))
                {
                    return;
                }
                m_BtnF3 = value;
                RaisePropertyChanged(() => BtnF3);
            }
        }

        public BtnItem BtnF4
        {
            get { return m_BtnF4; }
            set
            {
                if (Equals(value, m_BtnF4))
                {
                    return;
                }
                m_BtnF4 = value;
                RaisePropertyChanged(() => BtnF4);
            }
        }

        public BtnItem BtnF5
        {
            get { return m_BtnF5; }
            set
            {
                if (Equals(value, m_BtnF5))
                {
                    return;
                }
                m_BtnF5 = value;
                RaisePropertyChanged(() => BtnF5);
            }
        }

        public BtnItem BtnF6
        {
            get { return m_BtnF6; }
            set
            {
                if (Equals(value, m_BtnF6))
                {
                    return;
                }
                m_BtnF6 = value;
                RaisePropertyChanged(() => BtnF6);
            }
        }

        public BtnItem BtnF7
        {
            get { return m_BtnF7; }
            set
            {
                if (Equals(value, m_BtnF7))
                {
                    return;
                }
                m_BtnF7 = value;
                RaisePropertyChanged(() => BtnF7);
            }
        }

        public BtnItem BtnF8
        {
            get { return m_BtnF8; }
            set
            {
                if (Equals(value, m_BtnF8))
                {
                    return;
                }
                m_BtnF8 = value;
                RaisePropertyChanged(() => BtnF8);
            }
        }

        public void RaiseResourceChanged()
        {
            RaisePropertyChanged(() => Title);
            RaisePropertyChanged(() => BtnF1);
            RaisePropertyChanged(() => BtnF2);
            RaisePropertyChanged(() => BtnF3);
            RaisePropertyChanged(() => BtnF4);
            RaisePropertyChanged(() => BtnF5);
            RaisePropertyChanged(() => BtnF6);
            RaisePropertyChanged(() => BtnF7);
            RaisePropertyChanged(() => BtnF8);
        }
    }
}
using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.Turkmenistan.Model
{
    [Export]
    public class HardwareBtnModel : NotificationObject
    {
        private ICommand m_B5Command;
        private ICommand m_B4Command;
        private ICommand m_B3Command;
        private ICommand m_B2Command;
        private ICommand m_B1Command;
        public ICommand B1Command
        {
            set
            {
                if (Equals(value, m_B1Command))
                {
                    return;
                }

                m_B1Command = value;
                RaisePropertyChanged(() => B1Command);
            }
            get { return m_B1Command; }
        }

        public ICommand B2Command
        {
            set
            {
                if (Equals(value, m_B2Command))
                {
                    return;
                }

                m_B2Command = value;
                RaisePropertyChanged(() => B2Command);
            }
            get { return m_B2Command; }
        }

        public ICommand B3Command
        {
            set
            {
                if (Equals(value, m_B3Command))
                {
                    return;
                }

                m_B3Command = value;
                RaisePropertyChanged(() => B3Command);
            }
            get { return m_B3Command; }
        }

        public ICommand B4Command
        {
            set
            {
                if (Equals(value, m_B4Command))
                {
                    return;
                }

                m_B4Command = value;
                RaisePropertyChanged(() => B4Command);
            }
            get { return m_B4Command; }
        }

        public ICommand B5Command
        {
            set
            {
                if (Equals(value, m_B5Command))
                {
                    return;
                }

                m_B5Command = value;
                RaisePropertyChanged(() => B5Command);
            }
            get { return m_B5Command; }
        }
    }
}
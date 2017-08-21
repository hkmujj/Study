using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.Angola.TCMS.Model
{
    [Export]
    public class RightBtnModel : NotificationObject
    {
        private ICommand m_F1Command;
        private ICommand m_F2Command;
        private ICommand m_F3Command;
        private ICommand m_F4Command;
        private ICommand m_F5Command;

        public ICommand F1Command
        {
            set
            {
                if (Equals(value, m_F1Command))
                {
                    return;
                }

                m_F1Command = value;
                RaisePropertyChanged(() => F1Command);
            }
            get { return m_F1Command; }
        }

        public ICommand F2Command
        {
            set
            {
                if (Equals(value, m_F2Command))
                {
                    return;
                }

                m_F2Command = value;
                RaisePropertyChanged(() => F2Command);
            }
            get { return m_F2Command; }
        }

        public ICommand F3Command
        {
            set
            {
                if (Equals(value, m_F3Command))
                {
                    return;
                }

                m_F3Command = value;
                RaisePropertyChanged(() => F3Command);
            }
            get { return m_F3Command; }
        }

        public ICommand F4Command
        {
            set
            {
                if (Equals(value, m_F4Command))
                {
                    return;
                }

                m_F4Command = value;
                RaisePropertyChanged(() => F4Command);
            }
            get { return m_F4Command; }
        }

        public ICommand F5Command
        {
            set
            {
                if (Equals(value, m_F5Command))
                {
                    return;
                }

                m_F5Command = value;
                RaisePropertyChanged(() => F5Command);
            }
            get { return m_F5Command; }
        }
    }
}

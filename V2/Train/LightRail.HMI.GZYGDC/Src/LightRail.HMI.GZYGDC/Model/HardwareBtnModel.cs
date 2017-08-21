using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.Model
{
    [Export]
    public class HardwareBtnModel : NotificationObject
    {
        private ICommand m_OKCommand;
        private ICommand m_SettingCommand;
        private ICommand m_SaveAsCommand;
        private ICommand m_DownCommand;
        private ICommand m_UpCommand;
        private ICommand m_RightCommand;
        private ICommand m_LeftCommand;
        private ICommand m_QueryCommand;
        private ICommand m_B10Command;
        private ICommand m_B9Command;
        private ICommand m_B8Command;
        private ICommand m_B7Command;
        private ICommand m_B6Command;
        private ICommand m_B5Command;
        private ICommand m_B4Command;
        private ICommand m_B3Command;
        private ICommand m_B2Command;
        private ICommand m_B1Command;
        private ICommand m_RelaseCommand;
        private ICommand m_UnlockCommand;
        private ICommand m_AlertCommand;

        public ICommand AlertCommand
        {
            set
            {
                if (Equals(value, m_AlertCommand))
                {
                    return;
                }

                m_AlertCommand = value;
                RaisePropertyChanged(() => AlertCommand);
            }
            get { return m_AlertCommand; }
        }

        public ICommand UnlockCommand
        {
            set
            {
                if (Equals(value, m_UnlockCommand))
                {
                    return;
                }

                m_UnlockCommand = value;
                RaisePropertyChanged(() => UnlockCommand);
            }
            get { return m_UnlockCommand; }
        }

        public ICommand RelaseCommand
        {
            set
            {
                if (Equals(value, m_RelaseCommand))
                {
                    return;
                }

                m_RelaseCommand = value;
                RaisePropertyChanged(() => RelaseCommand);
            }
            get { return m_RelaseCommand; }
        }

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

        public ICommand B6Command
        {
            set
            {
                if (Equals(value, m_B6Command))
                {
                    return;
                }

                m_B6Command = value;
                RaisePropertyChanged(() => B6Command);
            }
            get { return m_B6Command; }
        }

        public ICommand B7Command
        {
            set
            {
                if (Equals(value, m_B7Command))
                {
                    return;
                }

                m_B7Command = value;
                RaisePropertyChanged(() => B7Command);
            }
            get { return m_B7Command; }
        }

        public ICommand B8Command
        {
            set
            {
                if (Equals(value, m_B8Command))
                {
                    return;
                }

                m_B8Command = value;
                RaisePropertyChanged(() => B8Command);
            }
            get { return m_B8Command; }
        }

        public ICommand B9Command
        {
            set
            {
                if (Equals(value, m_B9Command))
                {
                    return;
                }

                m_B9Command = value;
                RaisePropertyChanged(() => B9Command);
            }
            get { return m_B9Command; }
        }

        public ICommand B10Command
        {
            set
            {
                if (Equals(value, m_B10Command))
                {
                    return;
                }

                m_B10Command = value;
                RaisePropertyChanged(() => B10Command);
            }
            get { return m_B10Command; }
        }

        public ICommand QueryCommand
        {
            set
            {
                if (Equals(value, m_QueryCommand))
                {
                    return;
                }

                m_QueryCommand = value;
                RaisePropertyChanged(() => QueryCommand);
            }
            get { return m_QueryCommand; }
        }

        public ICommand LeftCommand
        {
            set
            {
                if (Equals(value, m_LeftCommand))
                {
                    return;
                }

                m_LeftCommand = value;
                RaisePropertyChanged(() => LeftCommand);
            }
            get { return m_LeftCommand; }
        }

        public ICommand RightCommand
        {
            set
            {
                if (Equals(value, m_RightCommand))
                {
                    return;
                }

                m_RightCommand = value;
                RaisePropertyChanged(() => RightCommand);
            }
            get { return m_RightCommand; }
        }

        public ICommand UpCommand
        {
            set
            {
                if (Equals(value, m_UpCommand))
                {
                    return;
                }

                m_UpCommand = value;
                RaisePropertyChanged(() => UpCommand);
            }
            get { return m_UpCommand; }
        }

        public ICommand DownCommand
        {
            set
            {
                if (Equals(value, m_DownCommand))
                {
                    return;
                }

                m_DownCommand = value;
                RaisePropertyChanged(() => DownCommand);
            }
            get { return m_DownCommand; }
        }

        public ICommand SaveAsCommand
        {
            set
            {
                if (Equals(value, m_SaveAsCommand))
                {
                    return;
                }

                m_SaveAsCommand = value;
                RaisePropertyChanged(() => SaveAsCommand);
            }
            get { return m_SaveAsCommand; }
        }

        public ICommand SettingCommand
        {
            set
            {
                if (Equals(value, m_SettingCommand))
                {
                    return;
                }

                m_SettingCommand = value;
                RaisePropertyChanged(() => SettingCommand);
            }
            get { return m_SettingCommand; }
        }

        public ICommand OKCommand
        {
            set
            {
                if (Equals(value, m_OKCommand))
                {
                    return;
                }

                m_OKCommand = value;
                RaisePropertyChanged(() => OKCommand);
            }
            get { return m_OKCommand; }
        }
    }
}
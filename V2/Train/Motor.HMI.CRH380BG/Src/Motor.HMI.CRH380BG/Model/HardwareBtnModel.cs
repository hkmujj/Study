using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.WPFInfrastructure.Interactivity;

namespace Motor.HMI.CRH380BG.Model
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
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
        private ICommand m_BOKCommand;
        private ICommand m_BReturnCommand;
        private ICommand m_BSwitchDisplayCommand;
        private ICommand m_BTrainStopCheckCommand;
        private ICommand m_BTrainRunningCheckCommand;
        private ICommand m_BFaultInfoCommand;
        private ICommand m_BIofoBoxCommand;
        private ICommand m_BLanguageSelectCommand;
        private ICommand m_BDayandnightSwitchCommand;
        private ICommand m_BLightCommand;
        private ICommand m_BSwitchCommand;

        private DelegateCommand<CommandParameter> m_B1MouseUpCommand;
        private DelegateCommand<CommandParameter> m_B2MouseUpCommand;
        private DelegateCommand<CommandParameter> m_B3MouseUpCommand;
        private DelegateCommand<CommandParameter> m_B4MouseUpCommand;
        private DelegateCommand<CommandParameter> m_B5MouseUpCommand;
        private DelegateCommand<CommandParameter> m_B6MouseUpCommand;
        private DelegateCommand<CommandParameter> m_B7MouseUpCommand;
        private DelegateCommand<CommandParameter> m_B8MouseUpCommand;
        private DelegateCommand<CommandParameter> m_B9MouseUpCommand;
        private DelegateCommand<CommandParameter> m_B10MouseUpCommand;
        private DelegateCommand<CommandParameter> m_B1MouseDownCommand;
        private DelegateCommand<CommandParameter> m_B2MouseDownCommand;
        private DelegateCommand<CommandParameter> m_B3MouseDownCommand;
        private DelegateCommand<CommandParameter> m_B4MouseDownCommand;
        private DelegateCommand<CommandParameter> m_B5MouseDownCommand;
        private DelegateCommand<CommandParameter> m_B6MouseDownCommand;
        private DelegateCommand<CommandParameter> m_B7MouseDownCommand;
        private DelegateCommand<CommandParameter> m_B8MouseDownCommand;
        private DelegateCommand<CommandParameter> m_B9MouseDownCommand;
        private DelegateCommand<CommandParameter> m_B10MouseDownCommand;
        private DelegateCommand<CommandParameter> m_BOkMouseUpCommand;
        private DelegateCommand<CommandParameter> m_BOkMouseDownCommand;

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

        public ICommand BSwitchCommand
        {
            set
            {
                if (Equals(value, m_BSwitchCommand))
                {
                    return;
                }

                m_BSwitchCommand = value;
                RaisePropertyChanged(() => BSwitchCommand);
            }
            get { return m_BSwitchCommand; }
        }

        public ICommand BLightCommand
        {
            set
            {
                if (Equals(value, m_BLightCommand))
                {
                    return;
                }

                m_BLightCommand = value;
                RaisePropertyChanged(() => BLightCommand);
            }
            get { return m_BLightCommand; }
        }

        public ICommand BDayandnightSwitchCommand
        {
            set
            {
                if (Equals(value, m_BDayandnightSwitchCommand))
                {
                    return;
                }

                m_BDayandnightSwitchCommand = value;
                RaisePropertyChanged(() => BDayandnightSwitchCommand);
            }
            get { return m_BDayandnightSwitchCommand; }
        }

        public ICommand BLanguageSelectCommand
        {
            set
            {
                if (Equals(value, m_BLanguageSelectCommand))
                {
                    return;
                }

                m_BLanguageSelectCommand = value;
                RaisePropertyChanged(() => BLanguageSelectCommand);
            }
            get { return m_BLanguageSelectCommand; }
        }

        public ICommand BIofoBoxCommand
        {
            set
            {
                if (Equals(value, m_BIofoBoxCommand))
                {
                    return;
                }

                m_BIofoBoxCommand = value;
                RaisePropertyChanged(() => BIofoBoxCommand);
            }
            get { return m_BIofoBoxCommand; }
        }

        public ICommand BFaultInfoCommand
        {
            set
            {
                if (Equals(value, m_BFaultInfoCommand))
                {
                    return;
                }

                m_BFaultInfoCommand = value;
                RaisePropertyChanged(() => BFaultInfoCommand);
            }
            get { return m_BFaultInfoCommand; }
        }

        public ICommand BTrainRunningCheckCommand
        {
            set
            {
                if (Equals(value, m_BTrainRunningCheckCommand))
                {
                    return;
                }

                m_BTrainRunningCheckCommand = value;
                RaisePropertyChanged(() => BTrainRunningCheckCommand);
            }
            get { return m_BTrainRunningCheckCommand; }
        }

        public ICommand BTrainStopCheckCommand
        {
            set
            {
                if (Equals(value, m_BTrainStopCheckCommand))
                {
                    return;
                }

                m_BTrainStopCheckCommand = value;
                RaisePropertyChanged(() => BTrainStopCheckCommand);
            }
            get { return m_BTrainStopCheckCommand; }
        }

        public ICommand BSwitchDisplayCommand
        {
            set
            {
                if (Equals(value, m_BSwitchDisplayCommand))
                {
                    return;
                }

                m_BSwitchDisplayCommand = value;
                RaisePropertyChanged(() => BSwitchDisplayCommand);
            }
            get { return m_BSwitchDisplayCommand; }
        }

        public ICommand BReturnCommand
        {
            set
            {
                if (Equals(value, m_BReturnCommand))
                {
                    return;
                }

                m_BReturnCommand = value;
                RaisePropertyChanged(() => BReturnCommand);
            }
            get { return m_BReturnCommand; }
        }

        public ICommand BOKCommand
        {
            set
            {
                if (Equals(value, m_BOKCommand))
                {
                    return;
                }

                m_BOKCommand = value;
                RaisePropertyChanged(() => BOKCommand);
            }
            get { return m_BOKCommand; }
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

        public DelegateCommand<CommandParameter> B10MouseDownCommand
        {
            set
            {
                if (Equals(value, m_B10MouseDownCommand))
                {
                    return;
                }

                m_B10MouseDownCommand = value;
                RaisePropertyChanged(() => B10MouseDownCommand);
            }
            get { return m_B10MouseDownCommand; }
        }

        public DelegateCommand<CommandParameter> B9MouseDownCommand
        {
            set
            {
                if (Equals(value, m_B9MouseDownCommand))
                {
                    return;
                }

                m_B9MouseDownCommand = value;
                RaisePropertyChanged(() => B9MouseDownCommand);
            }
            get { return m_B9MouseDownCommand; }
        }

        public DelegateCommand<CommandParameter> B8MouseDownCommand
        {
            set
            {
                if (Equals(value, m_B8MouseDownCommand))
                {
                    return;
                }

                m_B8MouseDownCommand = value;
                RaisePropertyChanged(() => B8MouseDownCommand);
            }
            get { return m_B8MouseDownCommand; }
        }

        public DelegateCommand<CommandParameter> B7MouseDownCommand
        {
            set
            {
                if (Equals(value, m_B7MouseDownCommand))
                {
                    return;
                }

                m_B7MouseDownCommand = value;
                RaisePropertyChanged(() => B7MouseDownCommand);
            }
            get { return m_B7MouseDownCommand; }
        }

        public DelegateCommand<CommandParameter> B6MouseDownCommand
        {
            set
            {
                if (Equals(value, m_B6MouseDownCommand))
                {
                    return;
                }

                m_B6MouseDownCommand = value;
                RaisePropertyChanged(() => B6MouseDownCommand);
            }
            get { return m_B6MouseDownCommand; }
        }

        public DelegateCommand<CommandParameter> B5MouseDownCommand
        {
            set
            {
                if (Equals(value, m_B5MouseDownCommand))
                {
                    return;
                }

                m_B5MouseDownCommand = value;
                RaisePropertyChanged(() => B5MouseDownCommand);
            }
            get { return m_B5MouseDownCommand; }
        }

        public DelegateCommand<CommandParameter> B4MouseDownCommand
        {
            set
            {
                if (Equals(value, m_B4MouseDownCommand))
                {
                    return;
                }

                m_B4MouseDownCommand = value;
                RaisePropertyChanged(() => B4MouseDownCommand);
            }
            get { return m_B4MouseDownCommand; }
        }

        public DelegateCommand<CommandParameter> B3MouseDownCommand
        {
            set
            {
                if (Equals(value, m_B3MouseDownCommand))
                {
                    return;
                }

                m_B3MouseDownCommand = value;
                RaisePropertyChanged(() => B3MouseDownCommand);
            }
            get { return m_B3MouseDownCommand; }
        }

        public DelegateCommand<CommandParameter> B2MouseDownCommand
        {
            set
            {
                if (Equals(value, m_B2MouseDownCommand))
                {
                    return;
                }

                m_B2MouseDownCommand = value;
                RaisePropertyChanged(() => B2MouseDownCommand);
            }
            get { return m_B2MouseDownCommand; }
        }

        public DelegateCommand<CommandParameter> B1MouseDownCommand
        {
            set
            {
                if (Equals(value, m_B1MouseDownCommand))
                {
                    return;
                }

                m_B1MouseDownCommand = value;
                RaisePropertyChanged(() => B1MouseDownCommand);
            }
            get { return m_B1MouseDownCommand; }
        }

        public DelegateCommand<CommandParameter> B10MouseUpCommand
        {
            set
            {
                if (Equals(value, m_B10MouseUpCommand))
                {
                    return;
                }

                m_B10MouseUpCommand = value;
                RaisePropertyChanged(() => B10MouseUpCommand);
            }
            get { return m_B10MouseUpCommand; }
        }

        public DelegateCommand<CommandParameter> B9MouseUpCommand
        {
            set
            {
                if (Equals(value, m_B9MouseUpCommand))
                {
                    return;
                }

                m_B9MouseUpCommand = value;
                RaisePropertyChanged(() => B9MouseUpCommand);
            }
            get { return m_B9MouseUpCommand; }
        }

        public DelegateCommand<CommandParameter> B8MouseUpCommand
        {
            set
            {
                if (Equals(value, m_B8MouseUpCommand))
                {
                    return;
                }

                m_B8MouseUpCommand = value;
                RaisePropertyChanged(() => B8MouseUpCommand);
            }
            get { return m_B8MouseUpCommand; }
        }

        public DelegateCommand<CommandParameter> B7MouseUpCommand
        {
            set
            {
                if (Equals(value, m_B7MouseUpCommand))
                {
                    return;
                }

                m_B7MouseUpCommand = value;
                RaisePropertyChanged(() => B7MouseUpCommand);
            }
            get { return m_B7MouseUpCommand; }
        }

        public DelegateCommand<CommandParameter> B6MouseUpCommand
        {
            set
            {
                if (Equals(value, m_B6MouseUpCommand))
                {
                    return;
                }

                m_B6MouseUpCommand = value;
                RaisePropertyChanged(() => B6MouseUpCommand);
            }
            get { return m_B6MouseUpCommand; }
        }

        public DelegateCommand<CommandParameter> B5MouseUpCommand
        {
            set
            {
                if (Equals(value, m_B5MouseUpCommand))
                {
                    return;
                }

                m_B5MouseUpCommand = value;
                RaisePropertyChanged(() => B5MouseUpCommand);
            }
            get { return m_B5MouseUpCommand; }
        }

        public DelegateCommand<CommandParameter> B4MouseUpCommand
        {
            set
            {
                if (Equals(value, m_B4MouseUpCommand))
                {
                    return;
                }

                m_B4MouseUpCommand = value;
                RaisePropertyChanged(() => B4MouseUpCommand);
            }
            get { return m_B4MouseUpCommand; }
        }

        public DelegateCommand<CommandParameter> B3MouseUpCommand
        {
            set
            {
                if (Equals(value, m_B3MouseUpCommand))
                {
                    return;
                }

                m_B3MouseUpCommand = value;
                RaisePropertyChanged(() => B3MouseUpCommand);
            }
            get { return m_B3MouseUpCommand; }
        }

        public DelegateCommand<CommandParameter> B2MouseUpCommand
        {
            set
            {
                if (Equals(value, m_B2MouseUpCommand))
                {
                    return;
                }

                m_B2MouseUpCommand = value;
                RaisePropertyChanged(() => B2MouseUpCommand);
            }
            get { return m_B2MouseUpCommand; }
        }

        public DelegateCommand<CommandParameter> B1MouseUpCommand
        {
            set
            {
                if (Equals(value, m_B1MouseUpCommand))
                {
                    return;
                }

                m_B1MouseUpCommand = value;
                RaisePropertyChanged(() => B1MouseUpCommand);
            }
            get { return m_B1MouseUpCommand; }
        }


        public DelegateCommand<CommandParameter> BOkMouseUpCommand
        {
            set
            {
                if (Equals(value, m_BOkMouseUpCommand))
                {
                    return;
                }

                m_BOkMouseUpCommand = value;
                RaisePropertyChanged(() => BOkMouseUpCommand);
            }
            get { return m_BOkMouseUpCommand; }
        }

        public DelegateCommand<CommandParameter> BOkMouseDownCommand
        {
            set
            {
                if (Equals(value, m_BOkMouseDownCommand))
                {
                    return;
                }

                m_BOkMouseDownCommand = value;
                RaisePropertyChanged(() => BOkMouseDownCommand);
            }
            get { return m_BOkMouseDownCommand; }
        }

        //public ICommand BOkMouseUpCommand
        //{
        //    set
        //    {
        //        if (Equals(value, m_BOkMouseUpCommand))
        //        {
        //            return;
        //        }

        //        m_BOkMouseUpCommand = value;
        //        RaisePropertyChanged(() => BOkMouseUpCommand);
        //    }
        //    get { return m_BOkMouseUpCommand; }
        //}

        //public ICommand BOkMouseDownCommand
        //{
        //    set
        //    {
        //        if (Equals(value, m_BOkMouseDownCommand))
        //        {
        //            return;
        //        }

        //        m_BOkMouseDownCommand = value;
        //        RaisePropertyChanged(() => BOkMouseDownCommand);
        //    }
        //    get { return m_BOkMouseDownCommand; }
        //}
    }
}
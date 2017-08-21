using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Send;

namespace Tram.CBTC.NRIET.ViewModel
{
    //[Export]
    public class StartViewModel : NotificationObject
    {
        private bool m_bDriverNumGotFocus = true;
        private string m_DriverNum = string.Empty;
        private string m_InputContent = string.Empty;
        private string m_Password = string.Empty;

        //[ImportingConstructor]
        public StartViewModel(DomainViewModel viewModel)
        {
            ViewModel = viewModel;

            InputCommand = new DelegateCommand<string>(Input);
            ConfirmCommand = new DelegateCommand(Confirm);
            ClearAllCommand = new DelegateCommand(ClearAll);
            DriverNumGotFocusCommand = new DelegateCommand<string>(DriverNumTextBox_OnGotFocus);
            PasswordGotFocusCommand = new DelegateCommand<string>(PasswordTextBox_OnGotFocus);
        }


        /// <summary>
        ///     司机号
        /// </summary>
        public string DriverNum
        {
            get { return m_DriverNum; }
            set
            {
                if (value == m_DriverNum)
                    return;

                m_DriverNum = value;

                RaisePropertyChanged(() => DriverNum);
            }
        }


        /// <summary>
        ///     密码
        /// </summary>
        public string Password
        {
            get { return m_Password; }
            set
            {
                if (value == m_Password)
                    return;

                m_Password = value;

                RaisePropertyChanged(() => Password);
            }
        }


        /// <summary>
        ///     输入内容
        /// </summary>
        public string InputContent
        {
            get
            {
                if (m_bDriverNumGotFocus)
                    m_InputContent = DriverNum;
                else
                    m_InputContent = Password;

                return m_InputContent;
            }
            set
            {
                if (value == m_InputContent)
                    return;

                m_InputContent = value;

                if (m_bDriverNumGotFocus)
                    DriverNum = m_InputContent;
                else
                    Password = m_InputContent;

                RaisePropertyChanged(() => InputContent);
            }
        }


        public DomainViewModel ViewModel { get; private set; }

        public ICommand InputCommand { get; private set; }

        public ICommand ConfirmCommand { get; private set; }

        public ICommand ClearAllCommand { get; private set; }


        public ICommand DriverNumGotFocusCommand { get; private set; }


        public ICommand PasswordGotFocusCommand { get; private set; }


        private void Input(string content)
        {
            if (m_bDriverNumGotFocus)
                DriverNum += content;
            else
                Password += content;
        }


        private void Confirm()
        {
            ViewModel.Controller.IsOpenKeyboard = false;
            
            if (ViewModel.CBTC.SendInterface.InputDriverId(new SendModel<string>(DriverNum)) &&
                ViewModel.CBTC.SendInterface.ConfirmPassword(new SendModel<string>(Password)))
            {
                var CurCabStatus = ViewModel.CBTC.TrainInfo.CarriageInfo.CurCabStatus;
                var CurrentCab = ViewModel.CBTC.TrainInfo.CarriageInfo.CurrentCab;
                var RemoteCab = ViewModel.CBTC.TrainInfo.CarriageInfo.RemoteCab;

                if (CurCabStatus == 0)
                {
                    if (CurrentCab.State == OBCUStatus.Standby)
                    {
                        ViewModel.Controller.Navigator.ToTrainNoActiveViewCommand.Execute(null);
                    }
                    else
                    {
                        ViewModel.Controller.Navigator.ToMainViewCommand.Execute(null);
                    }
                }
                else if (CurCabStatus == 1)
                {
                    if (RemoteCab.State == OBCUStatus.Standby)
                    {
                        ViewModel.Controller.Navigator.ToTrainNoActiveViewCommand.Execute(null);
                    }
                    else
                    {
                        ViewModel.Controller.Navigator.ToMainViewCommand.Execute(null);
                    }
                }
                else if (CurCabStatus == 2)
                {

                }
            }
        }


        private void ClearAll()
        {
            if (ViewModel.Controller.IsOpenKeyboard)
            {
                if (m_bDriverNumGotFocus)
                    DriverNum = "";
                else
                    Password = "";
            }
            else
            {
                DriverNum = "";
                Password = "";
            }
        }

        private void DriverNumTextBox_OnGotFocus(string parameter)
        {
            ViewModel.Controller.IsOpenKeyboard = false;

            m_bDriverNumGotFocus = true;

            ViewModel.Controller.IsOpenKeyboard = true;
        }

        private void PasswordTextBox_OnGotFocus(string parameter)
        {
            ViewModel.Controller.IsOpenKeyboard = false;

            m_bDriverNumGotFocus = false;

            ViewModel.Controller.IsOpenKeyboard = true;
        }
    }
}

using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interactivity;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Send;

namespace Tram.CBTC.NRIET.ViewModel
{
    //[Export]
    public class SettingViewModel : NotificationObject
    {
        private bool m_bTrainNumGotFocus = false;
        private bool m_bTrainNumEditStatus = false;

        private bool m_bLineRunDirectionGotFocus = false;
        private bool m_bLineRunDirectionEditStatus = false;

        private string m_TrainNum = string.Empty;
        private LineRunDirection m_LineRunDirectionStatus;
        private string m_InputContent = string.Empty;
        

        //[ImportingConstructor]
        public SettingViewModel(DomainViewModel viewModel)
        {
            ViewModel = viewModel;

            InputCommand = new DelegateCommand<string>(Input);
            ConfirmCommand = new DelegateCommand(Confirm);
            ClearAllCommand = new DelegateCommand(ClearAll);
            TrainNumGotFocusCommand = new DelegateCommand<string>(TrainNumTextBox_OnGotFocus);
            LineRunDirectionStatusGotFocusCommand = new DelegateCommand(LineRunDirection_OnGotFocus);

            ViewModel.CBTC.RoadInfo.TrainIDChangedEvent += RoadInfo_OnTrainIDChangedEvent;
            ViewModel.CBTC.RoadInfo.LineRunDirectionStatusChangedEvent += RoadInfo_OnLineRunDirectionStatusChangedEvent;

            LoadedCommand = new DelegateCommand<CommandParameter>(OnLoaded);
        }

        private void OnLoaded(CommandParameter obj)
        {
            //重新加载新数据
            RoadInfo_OnTrainIDChangedEvent();
            RoadInfo_OnLineRunDirectionStatusChangedEvent();
        }

        private void RoadInfo_OnTrainIDChangedEvent()
        {
            if (m_bTrainNumEditStatus)
            {

            }
            else
            {
                if (ViewModel.CBTC.RoadInfo.TrainID != null)
                {
                    TrainNum = ViewModel.CBTC.RoadInfo.TrainID;
                }
            }
        }

        private void RoadInfo_OnLineRunDirectionStatusChangedEvent()
        {
            if (m_bLineRunDirectionEditStatus)
            {

            }
            else
            {
                LineRunDirectionStatus = ViewModel.CBTC.RoadInfo.LineRunDirection;
            }
        }


        /// <summary>
        ///     车次号
        /// </summary>
        public string TrainNum
        {
            get { return m_TrainNum; }
            set
            {
                if (m_TrainNum.Equals(value))
                    return;

                m_TrainNum = value;

                RaisePropertyChanged(() => TrainNum);
            }
        }


        /// <summary>
        /// 线路运行方向
        /// </summary>
        public LineRunDirection LineRunDirectionStatus
        {
            get { return m_LineRunDirectionStatus; }
            set
            {
                if (value == m_LineRunDirectionStatus)
                    return;

                m_LineRunDirectionStatus = value;
                RaisePropertyChanged(() => LineRunDirectionStatus);
            }
        }


        /// <summary>
        ///     输入内容
        /// </summary>
        public string InputContent
        {
            get
            {
                if (m_bTrainNumGotFocus)
                    m_InputContent = TrainNum;

                return m_InputContent;
            }
            set
            {
                if (value.Equals(m_InputContent))
                    return;

                m_InputContent = value;

                if (m_bTrainNumGotFocus)
                    TrainNum = m_InputContent;

                RaisePropertyChanged(() => InputContent);
            }
        }


        public DomainViewModel ViewModel { get; private set; }

        public ICommand InputCommand { get; private set; }

        public ICommand ConfirmCommand { get; private set; }

        public ICommand ClearAllCommand { get; private set; }


        public ICommand TrainNumGotFocusCommand { get; private set; }

        public ICommand LineRunDirectionStatusGotFocusCommand { get; private set; }

        public DelegateCommand<CommandParameter> LoadedCommand { get; private set; }

        private void Input(string content)
        {
            if (m_bTrainNumGotFocus && !m_bTrainNumEditStatus)
            {
                m_bTrainNumEditStatus = true;
                TrainNum = String.Empty;
            }

            if (m_bTrainNumGotFocus && m_bTrainNumEditStatus)
            {
                TrainNum += content;
            }
        }


        private void Confirm()
        {
            ViewModel.Controller.IsOpenKeyboard = false;
            
            if (ViewModel.CBTC.SendInterface.InputTrainId(new SendModel<string>(TrainNum)) &&
                ViewModel.CBTC.SendInterface.InputLineRunDirection(new SendModel<LineRunDirection>(LineRunDirectionStatus)))
            {
                ViewModel.Controller.Navigator.IsOpenSettingViewMenuItem = false;


            }

            m_bTrainNumGotFocus = false;
            m_bTrainNumEditStatus = false;

            m_bLineRunDirectionGotFocus = false;
            m_bLineRunDirectionEditStatus = false;
        }


        private void ClearAll()
        {
            if (ViewModel.Controller.IsOpenKeyboard)
            {
                if (m_bTrainNumGotFocus)
                    TrainNum = "";
            }
            else
            {
                TrainNum = "";
            }
        }

        private void TrainNumTextBox_OnGotFocus(string parameter)
        {
            ViewModel.Controller.IsOpenKeyboard = false;

            m_bTrainNumGotFocus = true;

            ViewModel.Controller.IsOpenKeyboard = true;
        }

        private void LineRunDirection_OnGotFocus()
        {
            ViewModel.Controller.IsOpenKeyboard = false;

            m_bLineRunDirectionGotFocus = true;

            if (m_bLineRunDirectionGotFocus && !m_bLineRunDirectionEditStatus)
            {
                m_bLineRunDirectionEditStatus = true;
            }
        }
    }
}

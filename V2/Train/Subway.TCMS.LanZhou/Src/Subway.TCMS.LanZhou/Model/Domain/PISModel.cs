using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Model.Domain.PIS;
using Subway.TCMS.LanZhou.Utils;

namespace Subway.TCMS.LanZhou.Model.Domain
{
    [Export]
    public class PISModel : NotificationObject
    {
        private Station m_EndStation;
        private Station m_CurrentStation;

        public Station CurrentStation
        {
            get { return m_CurrentStation; }
            set
            {
                if (Equals(value, m_CurrentStation))
                {
                    return;
                }
                m_CurrentStation = value;
                RaisePropertyChanged(() => CurrentStation);
            }
        }

        public Station EndStation
        {
            get { return m_EndStation; }
            set
            {
                if (Equals(value, m_EndStation))
                {
                    return;
                }
                m_EndStation = value;
                RaisePropertyChanged(() => EndStation);
            }
        }

        public Lazy<PageWrapper<Station>> ShowingStationList { get; set; }

        public Lazy<ReadOnlyCollection<Station>> StationCollection { get; set; }

        public DelegateCommand NavigateToLocationInfoCommand { get; set; }
        public DelegateCommand ReturnRunningViewCommand { get; set; }
        public DelegateCommand AirConditionSettingCommand { get; set; }
        public DelegateCommand AirConditionStatusCommand { get; set; }
        public DelegateCommand TrainStatusBrakeCommand { get; set; }
        public DelegateCommand TrainStatusTowCommand { get; set; }
        public DelegateCommand TrainStatusAssistCommand { get; set; }

        public DelegateCommand FaultDetailPageViewCommand { get; set; }


        public DelegateCommand RunningHelpPageReturnCommand { get; set; }
        public DelegateCommand RunningHelpNextPageCommand { get; set; }
        public DelegateCommand RunningHelpPrePageCommand { get; set; }

        public DelegateCommand TrainStatusHelpNextPage1Command { get; set; }
        public DelegateCommand TrainStatusHelpNextPage2Command { get; set; }
        public DelegateCommand TrainStatusHelpPrePage1Command { get; set; }
        public DelegateCommand TrainStatusHelpPrePage2Command { get; set; }

        public DelegateCommand AirConditionSettingHelCommand { get; set; }
        public DelegateCommand AirConditionStatusHelpCommand { get; set; }

    }
}
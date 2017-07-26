using System;
using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200C.Subsys.Constant;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class CheckDataContentPopViewModel : DriverPopupViewModelBase
    {
        private DateTime m_CurrentDateTime;
        private string m_TrainId;
        private string m_DriverId;

        public string DriverId
        {
            set
            {
                if (value == m_DriverId)
                {
                    return;
                }
                m_DriverId = value;
                RaisePropertyChanged(() => DriverId);
            }
            get { return m_DriverId; }
        }

        public string TrainId
        {
            set
            {
                if (value == m_TrainId)
                {
                    return;
                }
                m_TrainId = value;
                RaisePropertyChanged(() => TrainId);
            }
            get { return m_TrainId; }
        }

        public DateTime CurrentDateTime
        {
            set
            {
                if (value.Equals(m_CurrentDateTime))
                {
                    return;
                }
                m_CurrentDateTime = value;
                RaisePropertyChanged(() => CurrentDateTime);
            }
            get { return m_CurrentDateTime; }
        }

        public CheckDataContentPopViewModel()
        {
            PopupViewName = PopupContentViewNames.CheckDataContentPopView;
            TitleContent = PopupViewStringKeys.StringTitleCheckTranData;
            PopViewTitleContent = PopupViewStringKeys.StringData;
        }

        public override void ResponseAction(IDriverInterface driverInterface)
        {
            TrainId = driverInterface.ATP.TrainInfo.Driver.TrainId;
            DriverId = driverInterface.ATP.TrainInfo.Driver.DriverId;
            CurrentDateTime = driverInterface.ATP.Other.ShowingDateTime;

            base.ResponseAction(driverInterface);
        }
    }
}
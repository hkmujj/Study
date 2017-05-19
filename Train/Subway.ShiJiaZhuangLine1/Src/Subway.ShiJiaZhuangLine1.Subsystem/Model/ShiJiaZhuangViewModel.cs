using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    public class ShiJiaZhuangViewModel : NotificationObject
    {
        private TitleModel m_TitleModel;
        private MainModel m_MainModel;
        private MainRunningBtnViewModel m_MainRunningBtnViewModel;
        private DoorModel m_DoorModel;
        private AirConditionModel m_AirModel;
        private AssistModel m_AssistModel;
        private BrakeModel m_BrakeModel;
        private TractionModel m_TractionModel;
        private AirPumpModel m_AirPumpModel;
        private FrsmHighSpeedModel m_FrsmHighSpeedModel;

        public ShiJiaZhuangViewModel()
        {
            TitleModel = new TitleModel();
            MainModel = new MainModel();
            // ButtonModel = new ButtonModel(this);
            DoorModel = new DoorModel();
            AirModel = new AirConditionModel();
            AssistModel = new AssistModel();
            BrakeModel = new BrakeModel();
            TractionModel = new TractionModel();
            AirPumpModel = new AirPumpModel();
            FrsmHighSpeedModel = new FrsmHighSpeedModel();
        }

        public FrsmHighSpeedModel FrsmHighSpeedModel
        {
            get { return m_FrsmHighSpeedModel; }
            set
            {
                if (Equals(value, m_FrsmHighSpeedModel))
                {
                    return;
                }
                m_FrsmHighSpeedModel = value;
                RaisePropertyChanged(() => FrsmHighSpeedModel);
            }
        }

        public AirPumpModel AirPumpModel
        {
            get { return m_AirPumpModel; }
            set
            {
                if (Equals(value, m_AirPumpModel))
                {
                    return;
                }
                m_AirPumpModel = value;
                RaisePropertyChanged(() => AirPumpModel);
            }
        }

        public TractionModel TractionModel
        {
            get { return m_TractionModel; }
            set
            {
                if (Equals(value, m_TractionModel))
                {
                    return;
                }
                m_TractionModel = value;
                RaisePropertyChanged(() => TractionModel);
            }
        }

        public BrakeModel BrakeModel
        {
            get { return m_BrakeModel; }
            set
            {
                if (Equals(value, m_BrakeModel))
                {
                    return;
                }
                m_BrakeModel = value;
                RaisePropertyChanged(() => BrakeModel);
            }
        }

        public AssistModel AssistModel
        {
            get { return m_AssistModel; }
            set
            {
                if (Equals(value, m_AssistModel))
                {
                    return;
                }
                m_AssistModel = value;
                RaisePropertyChanged(() => AssistModel);
            }
        }

        public AirConditionModel AirModel
        {
            get { return m_AirModel; }
            set
            {
                if (Equals(value, m_AirModel))
                {
                    return;
                }
                m_AirModel = value;
                RaisePropertyChanged(() => AirModel);
            }
        }

        public DoorModel DoorModel
        {
            get { return m_DoorModel; }
            set
            {
                if (Equals(value, m_DoorModel))
                {
                    return;
                }
                m_DoorModel = value;
                RaisePropertyChanged(() => DoorModel);
            }
        }

        public MainRunningBtnViewModel MainRunningBtnViewModel
        {
            get { return m_MainRunningBtnViewModel; }
            set
            {
                if (Equals(value, m_MainRunningBtnViewModel))
                {
                    return;
                }
                m_MainRunningBtnViewModel = value;
                RaisePropertyChanged(() => MainRunningBtnViewModel);
            }
        }

        public MainModel MainModel
        {
            get { return m_MainModel; }
            set
            {
                if (Equals(value, m_MainModel))
                {
                    return;
                }
                m_MainModel = value;
                RaisePropertyChanged(() => MainModel);
            }
        }

        public TitleModel TitleModel
        {
            get { return m_TitleModel; }
            set
            {
                if (Equals(value, m_TitleModel))
                {
                    return;
                }
                m_TitleModel = value;
                RaisePropertyChanged(() => TitleModel);
            }
        }

        public void DataChanged(object sender, CommunicationDataChangedArgs e)
        {
            MainModel.ChangeStatus(sender, e);
            AirModel.ChangeStatus(sender, e);
            AssistModel.ChangeStatus(sender, e);
            TractionModel.ChangeStatus(sender, e);
            AirPumpModel.ChangeStatus(sender, e);
            FrsmHighSpeedModel.ChangeStatus(sender, e);
            MainRunningBtnViewModel.ChangeStatus(sender, e);
        }
    }
}

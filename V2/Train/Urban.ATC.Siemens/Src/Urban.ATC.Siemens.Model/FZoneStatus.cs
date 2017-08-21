using Microsoft.Practices.Prism.ViewModel;
using Urban.ATC.Domain.Interface;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Siemens.Model
{
    public class FZoneStatus : NotificationObject, IFZoneStatus
    {
        private DoorDetailModel m_DoorDetailModel;
        private SpecialModel m_SpecialModel;
        private InfoLevl m_InfoLevl;
        private IMessgeInfo m_MessgeInfo;

        public DoorDetailModel DoorDetailModel
        {
            get { return m_DoorDetailModel; }
            set
            {
                if (m_DoorDetailModel == value)
                {
                    return;
                }
                m_DoorDetailModel = value;
                RaisePropertyChanged(() => DoorDetailModel);
            }
        }

        public SpecialModel SpecialModel
        {
            get { return m_SpecialModel; }
            set
            {
                if (m_SpecialModel == value)
                {
                    return;
                }
                m_SpecialModel = value;
                RaisePropertyChanged(() => SpecialModel);
            }
        }

        public IMessgeInfo MessgeInfo
        {
            get { return m_MessgeInfo; }
            set
            {
                if (Equals(value, m_MessgeInfo))
                {
                    return;
                }
                m_MessgeInfo = value;
                RaisePropertyChanged(() => MessgeInfo);
            }
        }

        public InfoLevl InfoLevl
        {
            get { return m_InfoLevl; }
            set
            {
                if (value == m_InfoLevl)
                {
                    return;
                }
                m_InfoLevl = value;
                RaisePropertyChanged(() => InfoLevl);
            }
        }
    }
}
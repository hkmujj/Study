using Microsoft.Practices.Prism.ViewModel;
using Urban.GuiYang.DDU.Model.PIS.Details;

namespace Urban.GuiYang.DDU.Model.PIS
{
    public class SettingStationModel : NotificationObject
    {
        private string m_LineId;
        private Station m_SkipStation2;
        private Station m_SkipStation1;
        private Station m_EndStation;
        private Station m_NextStation;
        private Station m_StartStation;

        public string LineId
        {
            get { return m_LineId; }
            set
            {
                if (value == m_LineId)
                {
                    return;
                }
                m_LineId = value;
                RaisePropertyChanged(() => LineId);
            }
        }

        public Station StartStation
        {
            get { return m_StartStation; }
            set
            {
                if (Equals(value, m_StartStation))
                {
                    return;
                }
                m_StartStation = value;
                RaisePropertyChanged(() => StartStation);
            }
        }

        public Station NextStation
        {
            get { return m_NextStation; }
            set
            {
                if (value == m_NextStation)
                {
                    return;
                }
                m_NextStation = value;
                RaisePropertyChanged(() => NextStation);
            }
        }

        public Station EndStation
        {
            get { return m_EndStation; }
            set
            {
                if (value == m_EndStation)
                {
                    return;
                }
                m_EndStation = value;
                RaisePropertyChanged(() => EndStation);
            }
        }

        public Station SkipStation1
        {
            get { return m_SkipStation1; }
            set
            {
                if (value == m_SkipStation1)
                {
                    return;
                }
                m_SkipStation1 = value;
                RaisePropertyChanged(() => SkipStation1);
            }
        }

        public Station SkipStation2
        {
            get { return m_SkipStation2; }
            set
            {
                if (value == m_SkipStation2)
                {
                    return;
                }
                m_SkipStation2 = value;
                RaisePropertyChanged(() => SkipStation2);
            }
        }
    }
}
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.Model.PIS.Details;

namespace Urban.GuiYang.DDU.Model.PIS
{
    [Export]
    public class StationModel: NotificationObject
    {
        private Station m_NextStation;
        private Station m_EndStatiion;
        private float m_CurrentStationDistance;
        private float m_NextStationDistance;
        
        private OpenDoorType m_OpenDoorType;
        private Station m_SkipStation1;
        private Station m_SkipStation2;
        private string m_LineId;
        private Station m_CurrentStation;

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

        public Station EndStatiion
        {
            get { return m_EndStatiion; }
            set
            {
                if (value == m_EndStatiion)
                {
                    return;
                }

                m_EndStatiion = value;
                RaisePropertyChanged(() => EndStatiion);
            }
        }

        public float CurrentStationDistance
        {
            get { return m_CurrentStationDistance; }
            set
            {
                if (value.Equals(m_CurrentStationDistance))
                {
                    return;
                }

                m_CurrentStationDistance = value;
                RaisePropertyChanged(() => CurrentStationDistance);
            }
        }

        public float NextStationDistance
        {
            get { return m_NextStationDistance; }
            set
            {
                if (value.Equals(m_NextStationDistance))
                {
                    return;
                }

                m_NextStationDistance = value;
                RaisePropertyChanged(() => NextStationDistance);
            }
        }

    

        public OpenDoorType OpenDoorType
        {
            get { return m_OpenDoorType; }
            set
            {
                if (value == m_OpenDoorType)
                {
                    return;
                }
                m_OpenDoorType = value;
                RaisePropertyChanged(() => OpenDoorType);
            }
        }

        public Station SkipStation1
        {
            get { return m_SkipStation1; }
            set
            {
                if (Equals(value, m_SkipStation1))
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
                if (Equals(value, m_SkipStation2))
                {
                    return;
                }
                m_SkipStation2 = value;
                RaisePropertyChanged(() => SkipStation2);
            }
        }
    }
}
using System.Diagnostics.Contracts;
using Motor.ATP.Infrasturcture.Interface;

// ReSharper disable once CheckNamespace
namespace Motor.ATP.Infrasturcture.Model
{
    public class Station : TrainInfoPartialBase, IStation
    {
        private readonly IStationInterpreter m_StationInterpreter;

        private string m_CurrentStation;
        private bool m_Visible;
        private OpenDoorLocation m_CurrentOpenDoorLocation;

        public Station(ITrainInfo parent, IStationInterpreter stationInterpreter)
            : base(parent)
        {
            Contract.Requires(stationInterpreter != null);
            m_StationInterpreter = stationInterpreter;
            Visible = true;
        }

        public string CurrentStation
        {
            get { return m_CurrentStation; }
            set
            {
                if (value == m_CurrentStation)
                {
                    return;
                }
                m_CurrentStation = value;
                RaisePropertyChanged(() => CurrentStation);
            }
        }

        /// <summary>
        /// 当前开门侧
        /// </summary>
        public OpenDoorLocation CurrentOpenDoorLocation
        {
            get { return m_CurrentOpenDoorLocation; }
            set
            {
                if (value == m_CurrentOpenDoorLocation)
                {
                    return;
                }
                m_CurrentOpenDoorLocation = value;
                RaisePropertyChanged(() => CurrentOpenDoorLocation);
            }
        }

        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (value == m_Visible)
                {
                    return;
                }
                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
        }
    }
}
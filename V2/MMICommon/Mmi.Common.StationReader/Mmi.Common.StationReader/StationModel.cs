using System.Diagnostics;

namespace Mmi.Common.StationReader
{
    [DebuggerDisplay("CommunicationId={CommunicationId},ShowId={ShowId},StationName={StationName}")]
    public class StationModel
    {
        [DebuggerStepThrough]
        public StationModel(float communicationId, string stationName, string showId = null)
        {
            CommunicationId = communicationId;
            StationName = stationName;
            ShowId = showId;
        }

        public string ShowId { private set; get; }

        public float CommunicationId { private set; get; }

        public string StationName { private set; get; }
    }
}
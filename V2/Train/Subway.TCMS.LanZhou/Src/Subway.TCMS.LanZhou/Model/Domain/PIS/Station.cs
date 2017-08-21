using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Config;

namespace Subway.TCMS.LanZhou.Model.Domain.PIS
{
    [DebuggerDisplay("Name={Name}")]
    public class Station : NotificationObject
    {
        [DebuggerStepThrough]
        public Station(StationConfig stationConfig)
        {
            StationConfig = stationConfig;
        }

        public StationConfig StationConfig { get; private set; }

        public string Name
        {
            get { return StationConfig.StationCH; }
        }

    }
}
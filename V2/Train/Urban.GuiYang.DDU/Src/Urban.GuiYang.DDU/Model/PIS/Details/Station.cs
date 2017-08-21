using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Urban.GuiYang.DDU.Config;

namespace Urban.GuiYang.DDU.Model.PIS.Details
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

        public string Name { get { return StationConfig.StationCH; } }

    }
}
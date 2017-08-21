using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Fault;
using Tram.CBTC.Infrasturcture.Model.Road;

namespace Tram.CBTC.NRIET.ViewModel
{
    public class Test
    {
        private Test()
        {
            Init();
        }
        public static Test Instance = new Test();
        public List<StationPass> CBTCRoadInfoStationInfoStationPasses { get; private set; }

        private void Init()
        {
            CBTCRoadInfoStationInfoStationPasses = new List<StationPass>();
            for (int i = 0; i < 11; ++i)
            {
                CBTCRoadInfoStationInfoStationPasses.Add(new StationPass() { IsStation = true, StationName = "站点" + i, });
                if (i < 10)
                {
                    for (int j = 0; j < 4; ++j)
                    {
                        CBTCRoadInfoStationInfoStationPasses.Add(new StationPass() { IsStation = false });
                    }
                }

            }
            RadarTargets = new ObservableCollection<RadarTarget>();
            RadarTargets.Add(new RadarTarget() { BackColor = CBTCColor.Red, HorizontalDistance = 20, VerticalDistance = 100 });
            RadarTargets.Add(new RadarTarget() { BackColor = CBTCColor.Green, HorizontalDistance = 50, VerticalDistance = 150 });
            RadarTargets.Add(new RadarTarget() { BackColor = CBTCColor.White, HorizontalDistance = 80, VerticalDistance = 190 });
        }
     
        public ObservableCollection<RadarTarget> RadarTargets { get; set; }
    }
}

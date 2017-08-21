using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.HMI.CRH1A.Common.Config;

namespace Motor.HMI.CRH1A.Appraise
{
    class AppraiseResource
    {
        private readonly Dictionary<ViewConfig, int> m_ViewConfigIndexDictionary;

        public AppraiseResource()
        {
            m_ViewConfigIndexDictionary = new Dictionary<ViewConfig, int>()
                                          {
                                              { ViewConfig.SystemComfort, 0 },
                                              { ViewConfig.SystemSanitary, 1 },
                                              { ViewConfig.SystemDoor, 2 },
                                              { ViewConfig.SystemRoller, 3 },
                                              { ViewConfig.SystemFire, 4 },
                                              { ViewConfig.SystemFont, 5 },
                                              { ViewConfig.SystemBrake, 6 },
                                              { ViewConfig.SystemHightVol, 7 },
                                              { ViewConfig.SystemPowerSupply, 8 },
                                              { ViewConfig.SystemAirSupply, 9 },
                                              { ViewConfig.SystemTow, 10 },
                                              { ViewConfig.TestBrakeTest, 11 },
                                              { ViewConfig.TestDrivControl, 12 },
                                              { ViewConfig.TestLightTest, 13 },
                                              { ViewConfig.AlarmRecord, 14 },
                                              { ViewConfig.AlarmAll, 15 },
                                              { ViewConfig.AlarmReport, 16 },
                                              { ViewConfig.Station, 17 },

                                          };
        }

        public int GetIndexOf(ViewConfig viewConfig)
        {
            if (!m_ViewConfigIndexDictionary.ContainsKey(viewConfig))
            {
                return -1;
            }
            return m_ViewConfigIndexDictionary[viewConfig];
        }
    }
}

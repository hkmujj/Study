using System.Collections.Generic;
using Motor.HMI.CRH5G.Config;
using Motor.HMI.CRH5G.底层共用;

namespace Motor.HMI.CRH5G.Appraise
{
    class AppraiseResource
    {
        private readonly Dictionary<ViewConfig, int> m_ViewConfigIndexDictionary;

        public AppraiseResource()
        {
            m_ViewConfigIndexDictionary = new Dictionary<ViewConfig, int>
                                            {
                                              { ViewConfig.MenuOneScreen, 1 },
                                              { ViewConfig.MenuTwoScreen, 2 },
                                              { ViewConfig.MenuThreeScreen, 3 },
                                              { ViewConfig.MenuFourScreen, 4 },
                                              { ViewConfig.MenuFiveScreen, 5 },
                                              { ViewConfig.MenuSixScreen, 6 },
                                              { ViewConfig.MenuSevenScreen, 7 },
                                              { ViewConfig.MenuEightScreen, 8 },
                                              { ViewConfig.MenuNineScreen, 9 },
                                              { ViewConfig.Command, 40 },
                                              { ViewConfig.InstrumentScreen1, 21 },
                                              { ViewConfig.TestOneScreen, 30 },
                                              { ViewConfig.TestTwoScreen, 31 },
                                              { ViewConfig.TestThreeScreen, 32 },
                                              { ViewConfig.FaultHistory,102},
                                              { ViewConfig.LanguageTimeSettings, 25}
                                              
                                              
                                          };
        }

        public int GetIndexOf(ViewConfig viewConfig, ScreenIdentification id)
        {
            if (!m_ViewConfigIndexDictionary.ContainsKey(viewConfig))
            {
                return -1;
            }
            return m_ViewConfigIndexDictionary[viewConfig];
        }
    }
}

using System.Collections.Generic;
using Motor.HMI.CRH3C380B.Config;

namespace Motor.HMI.CRH3C380B.Appraise
{
    internal class AppraiseResource
    {
        private readonly Dictionary<ViewConfig, int> m_ViewConfigIndexDictionary1D;

        public AppraiseResource()
        {
            m_ViewConfigIndexDictionary1D = new Dictionary<ViewConfig, int>
            {
                {ViewConfig.DMIDoor, 130},
            };
        }

        public int GetIndexOf(ViewConfig viewConfig)
        {
            if (!m_ViewConfigIndexDictionary1D.ContainsKey(viewConfig))
            {
                return -1;
            }
            return m_ViewConfigIndexDictionary1D[viewConfig];
        }
    }
}

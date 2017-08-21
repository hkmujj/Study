using System.Linq;
using CRH2MMI.Common.Global;

namespace CRH2MMI.Vigilant
{
    class VigilantResource
    {
        private VigilantView m_VigilantView;

        public VigilantConfig VigilantConfig { private set; get; }

        public VigilantResource(VigilantView vigilantView)
        {
            m_VigilantView = vigilantView;
        }

        public void Init()
        {
            if (VigilantConfig == null || VigilantConfig.ViewItems == null || !VigilantConfig.ViewItems.Any())
            {
                VigilantConfig = GlobalInfo.CurrentCRH2Config.VigilantConfig;
            }
        }
    }
}

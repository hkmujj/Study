using System.Linq;
using CRH2MMI.Common.Global;

namespace CRH2MMI.BPRescue
{
    internal class BPResource
    {
        private BPRescueView m_BPRescue;

        public BPConfig BPConfig { private set; get; }

        public BPResource(BPRescueView bpRescue)
        {
            m_BPRescue = bpRescue;
        }

        public void Init()
        {
            if (BPConfig == null || BPConfig.BPGridLine == null || BPConfig.BPGridLine.ViewItems == null || !BPConfig.BPGridLine.ViewItems.Any())
            {
                BPConfig = GlobalInfo.CurrentCRH2Config.BPRescueConfig;
            }
        }

    }
}

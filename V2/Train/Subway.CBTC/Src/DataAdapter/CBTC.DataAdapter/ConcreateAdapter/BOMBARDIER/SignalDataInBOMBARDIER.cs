using CBTC.DataAdapter.Model;
using System;

namespace CBTC.DataAdapter.ConcreateAdapter.BOMBARDIER
{
    [Serializable]
    public class SignalDataInBOMBARDIER : SignalDataIn
    {

        public SignalDataInBOMBARDIER()
        {
            
        }

        public override void ClearInfo()
        {
            base.ClearInfo();
        }
    }
}
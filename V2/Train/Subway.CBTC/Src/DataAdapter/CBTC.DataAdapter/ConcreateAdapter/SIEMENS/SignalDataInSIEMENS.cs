using CBTC.DataAdapter.Model;
using System;

namespace CBTC.DataAdapter.ConcreateAdapter.SIEMENS
{
    [Serializable]
    public class SignalDataInSIEMENS : SignalDataIn
    {
        public SignalDataInSIEMENS()
        {
            
        }

        public override void ClearInfo()
        {
            base.ClearInfo();
        }
    }
}
using System;

namespace CBTC.Infrasturcture.Model.Constant
{
    [Flags]
    public enum FaultLocation
    {   
        None,
        ATP,
        ATP1,
        ATO,
        BTM,
        TWC,
        RAD,
        Radar,
        AM,
    }
}
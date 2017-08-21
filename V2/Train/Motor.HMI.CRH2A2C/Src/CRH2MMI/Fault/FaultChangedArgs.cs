using System;
using System.Collections.ObjectModel;

namespace CRH2MMI.Fault
{
    class FaultChangedArgs : EventArgs
    {
        public FaultChangedArgs(FaultChangedType type, ReadOnlyCollection<FaultItemInfo> changedFaultItemInfos)
        {
            ChangedFaultItemInfos = changedFaultItemInfos;
            Type = type;
        }

        public FaultChangedType Type { private set; get; }

        public ReadOnlyCollection<FaultItemInfo> ChangedFaultItemInfos { private set; get; }
    }

    enum FaultChangedType
    {
        Add, 
        Remove,
        Clear,
    }
}

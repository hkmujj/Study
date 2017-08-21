using System;

namespace CRH2MMI.CutState
{
    class RemovalStateChangedArgs : EventArgs
    {
        public RemovalStateChangedArgs(RemovalStateChangedType type, string occuceRemovalState)
        {
            OccuceRemovalState = occuceRemovalState;
            Type = type;
        }

        public RemovalStateChangedType Type { private set; get; }

        public string OccuceRemovalState { private set; get; }

    }

    enum RemovalStateChangedType
    {
        Add,
        Removel,
    }
}

using System.Diagnostics;
using Motor.HMI.CRH1A.Common.Train;
using System;

namespace Motor.HMI.CRH1A.Common.EventArg
{
    class TrainStateChangedArgs : EventArgs
    {
        [DebuggerStepThrough]
        public TrainStateChangedArgs(TrainState oldState, TrainState newState)
        {
            NewState = newState;
            OldState = oldState;
        }

        public TrainState OldState { private set; get; }

        public TrainState NewState { private set; get; }
    }
}

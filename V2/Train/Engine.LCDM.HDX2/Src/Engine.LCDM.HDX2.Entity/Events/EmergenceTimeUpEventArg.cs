using System.Diagnostics;

namespace Engine.LCDM.HDX2.Entity.Events
{
    public class EmergenceTimeUpEventArg
    {
        [DebuggerStepThrough]
        public EmergenceTimeUpEventArg(EmergenceTimerState emergenceTimerState)
        {
            EmergenceTimerState = emergenceTimerState;
        }

        public EmergenceTimerState EmergenceTimerState { private set; get; }
    }

    public enum EmergenceTimerState
    {
        Counting,

        End,

        Stopped
    }
}
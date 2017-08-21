using System.Diagnostics;
using Engine.LCDM.HDX2.Entity.Model.Domain;

namespace Engine.LCDM.HDX2.Entity.Events
{
    public class PowerStateChangedEventArg
    {
        [DebuggerStepThrough]
        public PowerStateChangedEventArg(PowerState powerState)
        {
            PowerState = powerState;
        }

        public PowerState PowerState { private set; get; }
    }
}
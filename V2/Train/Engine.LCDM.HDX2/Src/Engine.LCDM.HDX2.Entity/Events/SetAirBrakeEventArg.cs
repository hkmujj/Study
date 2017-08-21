using System.Diagnostics;

namespace Engine.LCDM.HDX2.Entity.Events
{
    public class SetAirBrakeEventArg
    {
        [DebuggerStepThrough]
        public SetAirBrakeEventArg(SetAirBrakeType setAirBrakeType)
        {
            SetAirBrakeType = setAirBrakeType;
        }

        public SetAirBrakeType SetAirBrakeType { private set; get; }
    }

    public enum SetAirBrakeType
    {
        Begin,

        KPa,

        CutInOff,

        TrainType,

        MakeupAir,

        Ok,

        Cancel
    }
}
using System.Diagnostics;

namespace Engine.LCDM.HDX2.Entity.Events
{
    public class SetEventArg
    {
        [DebuggerStepThrough]
        public SetEventArg(SetType setType)
        {
            SetType = setType;
        }

        public SetType SetType { private set; get; }
    }

    public enum SetType
    {
        Begin,

        ReserveCommon,

        SigleMutil,

        Ok,

        Cancel,
    }
}
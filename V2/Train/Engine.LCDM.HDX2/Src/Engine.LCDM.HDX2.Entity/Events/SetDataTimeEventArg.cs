using System.Diagnostics;

namespace Engine.LCDM.HDX2.Entity.Events
{
    public class SetDataTimeEventArg
    {
        [DebuggerStepThrough]
        public SetDataTimeEventArg(SetDataTimeType setDataTimeType)
        {
            SetDataTimeType = setDataTimeType;
        }

        public SetDataTimeType SetDataTimeType { private set; get; }
    }

    public enum SetDataTimeType
    {
        Begin,

        ChangeTime,

        ChangeYear,

        ChangeMounth,

        ChangeDay,

        GetFromNet,

        Ok,

        Cancel,
    }
}
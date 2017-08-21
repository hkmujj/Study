using System.Diagnostics;

namespace Engine.LCDM.HDX2.Entity.Events
{
    [DebuggerDisplay("SetTrainIdType={SetTrainIdType}")]
    public class SetTrainIdEventArg
    {
        [DebuggerStepThrough]
        public SetTrainIdEventArg(SetTrainIdType setTrainIdType)
        {
            SetTrainIdType = setTrainIdType;
        }

        public SetTrainIdType SetTrainIdType { private set; get; }
    }

    public enum SetTrainIdType
    {
        Begin,

        SetData1,
        SetData2,
        SetData3,
        SetData4,
        SetData5,

        Ok,

        Cancel,
    }
}
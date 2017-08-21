using System.Diagnostics;
using Microsoft.Practices.Prism.Events;
using MMITool.Addin.YdConfigCreater.Model.Condition;

namespace MMITool.Addin.YdConfigCreater.Events
{
    public class CreateResultEvent : CompositePresentationEvent<CreateResultEvent.Args>
    {
        public enum CreateState
        {
            ToStart,

            Completed,

            Fail,
        }

        public class Args
        {
            [DebuggerStepThrough]
            public Args(ConditionModel conditionModel, CreateState state = CreateState.ToStart)
            {
                ConditionModel = conditionModel;
                State = state;
            }

            public ConditionModel ConditionModel { get; private set; }

            public CreateState State { get; private set; }
        }
    }
}
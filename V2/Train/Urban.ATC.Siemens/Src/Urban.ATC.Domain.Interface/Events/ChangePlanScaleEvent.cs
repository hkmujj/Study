using System.Diagnostics;
using Microsoft.Practices.Prism.Events;

namespace Motor.ATP.Domain.Interface.Events
{
    public class ChangePlanScaleEvent : CompositePresentationEvent<ChangePlanScaleEventEventArgs>
    {

    }

    public class ChangePlanScaleEventEventArgs
    {
        public ChangePlanScaleType ScaleType { private set; get; }

        [DebuggerStepThrough]
        public ChangePlanScaleEventEventArgs(ChangePlanScaleType scaleType)
        {
            ScaleType = scaleType;
        }
    }

    public enum ChangePlanScaleType
    {
        Increase,
        Reduce,
    }
}
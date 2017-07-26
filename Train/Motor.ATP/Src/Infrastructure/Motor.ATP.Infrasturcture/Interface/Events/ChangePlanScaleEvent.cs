using System.Diagnostics;
using Microsoft.Practices.Prism.Events;

namespace Motor.ATP.Infrasturcture.Interface.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class ChangePlanScaleEvent : CompositePresentationEvent<ChangePlanScaleEvent.Args>
    {
        /// <summary>
        /// 
        /// </summary>
        public class Args
        {
            /// <summary>
            /// 
            /// </summary>
            public ATPType ATPType { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            public ChangePlanScaleType ScaleType { private set; get; }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="scaleType"></param>
            /// <param name="atpType"></param>
            [DebuggerStepThrough]
            public Args(ChangePlanScaleType scaleType, ATPType atpType)
            {
                ScaleType = scaleType;
                ATPType = atpType;
            }
        }
    }

    /// <summary>
    /// 更改计划区类型
    /// </summary>
    public enum ChangePlanScaleType
    {
        /// <summary>
        /// 增大
        /// </summary>
        Increase,

        /// <summary>
        /// 缩小
        /// </summary>
        Reduce,
    }
}
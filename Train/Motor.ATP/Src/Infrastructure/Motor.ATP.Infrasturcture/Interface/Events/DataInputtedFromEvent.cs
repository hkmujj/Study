using System;
using System.Diagnostics;
using Microsoft.Practices.Prism.Events;

namespace Motor.ATP.Infrasturcture.Interface.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class DataInputtedFromEvent : CompositePresentationEvent<DataInputtedFromEvent.Args>
    {
        /// <summary>
        /// 
        /// </summary>
        public class Args
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="atpType"></param>
            /// <param name="dataType"></param>
            /// <param name="inputtedFrom"></param>
            [DebuggerStepThrough]
            public Args(ATPType atpType, Type dataType, DataInputtedFrom inputtedFrom)
            {
                DataType = dataType;
                InputtedFrom = inputtedFrom;
                ATPType = atpType;
            }

            /// <summary>
            /// 
            /// </summary>
            public ATPType ATPType { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            public Type DataType { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            public DataInputtedFrom InputtedFrom { get; private set; }
        }
    }
}
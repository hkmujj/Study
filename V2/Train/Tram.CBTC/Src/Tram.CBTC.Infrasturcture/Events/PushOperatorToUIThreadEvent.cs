using System;
using Microsoft.Practices.Prism.Events;
using Tram.CBTC.Infrasturcture.Model;

namespace Tram.CBTC.Infrasturcture.Events
{
    /// <summary>
    /// 操作到ui 线程
    /// </summary>
    public class PushOperatorToUIThreadEvent : CompositePresentationEvent<PushOperatorToUIThreadEvent.Args>
    {
        /// <summary>
        /// 
        /// </summary>
        public class Args
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="opreat"></param>
            /// <param name="screenIdentity"></param>
            /// <param name="params"></param>
            public Args(Delegate opreat, ScreenIdentity screenIdentity, params object[] @params)
            {
                Operate = opreat;
                ScreenIdentity = screenIdentity;
                Params = @params;
            }

            /// <summary>
            /// ID
            /// </summary>
            public ScreenIdentity ScreenIdentity { get; private set; }

            /// <summary>
            /// 操作
            /// </summary>
            public Delegate Operate { get; private set; }

            /// <summary>
            /// 参数
            /// </summary>
            public object[] Params { get; private set; }
        }
    }
}
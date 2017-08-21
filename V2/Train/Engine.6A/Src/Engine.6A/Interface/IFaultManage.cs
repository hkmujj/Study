using System;
using System.Collections.Generic;
using Engine._6A.Enums;
using MMI.Common.Msg.Interface;

namespace Engine._6A.Interface
{
    public interface IFaultManage : IManage<int, IFaultInfo>, IPaging<IFaultInfo>
    {
        IList<IFaultInfo> DisplayList { get; }
        void Reset(FaultType type);
        event Action DispalyInfoChnged;
    }
}
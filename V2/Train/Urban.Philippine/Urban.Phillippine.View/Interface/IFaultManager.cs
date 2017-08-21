using System;
using System.Collections.Generic;
using MMI.Common.Msg.Interface;
using Urban.Phillippine.View.EventArgs;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Interface
{
    public interface IFaultManager : IManage<int, IFaultInfo>, IPaging<IFaultInfo>
    {
        bool Init(int pageNum);
        event Action<FaultChangedArgs<int>> MaxPageChanged;
        event Action<FaultChangedArgs<int>> CurrentPageChanged;
        event Action<FaultChangedArgs<IList<IFaultInfo>>> CurrentChanged;
        FaultType CurrentType { get; }
        void SetCurrentType(FaultType type);
    }
}
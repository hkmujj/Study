using System;
using Engine._6A.Enums;

namespace Engine._6A.Interface
{
    public interface IFaultInfo
    {
        FaultType Type { get; }
        int LogicId { get; }
        DateTime DataTime { get; set; }
        SubSystem SubSystem { get; set; }
        string Context { get; set; }
        Distinction Distinction { get; set; }
        string Position { get; set; }
        string Speed { get; set; }
    }
}
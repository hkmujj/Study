using System;

namespace Urban.Phillippine.View.Interface
{
    public interface IFaultInfo : ICloneable
    {
        int Logic { get; set; }
        string Name { get; set; }
        int Code { get; set; }
        string Description { get; set; }
        DateTime Time { get; set; }
    }
}
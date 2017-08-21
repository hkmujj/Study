using System;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Interface.ViewModel
{
    public interface ITitleViewModel : IViewModelBase
    {
        double NetVoltage { get; set; }
        double NetCurrent { get; set; }
        double Battery { get; set; }
        TractionBrakeLevel Level { get; set; }
        LevelColor LColor { get; set; }
        double Speed { get; set; }
        DateTime CurrentDateTime { get; set; }
    }
}
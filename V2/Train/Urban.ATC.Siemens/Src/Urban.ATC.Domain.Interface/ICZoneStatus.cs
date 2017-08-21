using System.ComponentModel;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Domain.Interface
{
    public interface ICZoneStatus : INotifyPropertyChanged
    {
        OBCUModel ObcuModel { get; }
        TrainInteGrity TrainInteGrityC3 { get; }
        TrainInteGrity TrainInteGrityC4 { get; }
        DriveingBrakeType DriveingBrakeType { get; }
        MaximumMode MaximumMode { get; }
        bool ModelFlicker { get; }
    }
}
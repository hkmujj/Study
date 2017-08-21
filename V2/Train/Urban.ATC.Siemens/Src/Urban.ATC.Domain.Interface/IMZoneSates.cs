using System.ComponentModel;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Domain.Interface
{
    public interface IMZoneSates : INotifyPropertyChanged
    {
        float NumberT1 { get; }
        float NumberT2 { get; }
        float NumberT3 { get; }
        ActualLevels ActualLevels { get; }
        DriveModel DriveModel { get; }
        ReverseModel ReverseModel { get; }
        StopModel StopModel { get; }
        DoorModel DoorModel { get; }
        DoorRelease DoorRelease { get; }
        DepartureType DepartureType { get; }
        DoorDetailModel DoorDetailModel { get; }
        SpecialModel SpecialModel { get; }
        EmergencyModel EmergencyModel { get; }
    }
}
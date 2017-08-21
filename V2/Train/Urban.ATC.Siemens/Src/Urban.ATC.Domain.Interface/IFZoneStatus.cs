using System.ComponentModel;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Domain.Interface
{
    public interface IFZoneStatus : INotifyPropertyChanged
    {
        DoorDetailModel DoorDetailModel { get; }
        SpecialModel SpecialModel { get; }
        IMessgeInfo MessgeInfo { get; }
        InfoLevl InfoLevl { get; }
    }
}
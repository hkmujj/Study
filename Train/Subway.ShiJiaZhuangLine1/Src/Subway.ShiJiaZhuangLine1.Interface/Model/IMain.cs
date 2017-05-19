using System.Windows;
using System.Windows.Media;
using Subway.ShiJiaZhuangLine1.Interface.Enum;

namespace Subway.ShiJiaZhuangLine1.Interface.Model
{
    public interface IMain : IViewModel
    {
        Visibility DoorNumVisibility { get; set; }
        Visibility DriverOneVisibility { get; }
        Visibility DriverSixVisibility { get; }
        double BrakeEffective { get; }
        double TractionEffective { get; }
        Visibility BrakeVisibility { get; }
        Visibility TractionVisibility { get; }
        Visibility HighSpeedVisibility { get; }
        bool EnmergencyBrake { get; }
        SolidColorBrush EmergencyBrush { get; }
        ControlModel ControlModel { get; }
        bool TrainLeftDoorOpen { get; }
        bool TrainRunLeft { get; }
        bool TrainRightDoorOpen { get; }
        bool TrainRunRight { get; }
        double LimitSpeed { get; }
        StationModel StationModel { get; }
        double Speed { get; }

    }
}
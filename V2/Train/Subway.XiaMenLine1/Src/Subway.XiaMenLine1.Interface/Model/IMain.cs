namespace Subway.XiaMenLine1.Interface.Model
{
    public interface IMain : IViewModel
    {
        System.Windows.Visibility DoorNumVisibility { get; set; }
        System.Windows.Visibility DriverOneVisibility { get; }
        System.Windows.Visibility DriverSixVisibility { get; }
        double BrakeEffective { get; }
        double TractionEffective { get; }
        System.Windows.Visibility BrakeVisibility { get; }
        System.Windows.Visibility TractionVisibility { get; }
        System.Windows.Visibility HighSpeedVisibility { get; }
        bool EnmergencyBrake { get; }
        System.Windows.Media.SolidColorBrush EmergencyBrush { get; }
        Enum.ControlModel ControlModel { get; }
        bool TrainLeftDoorOpen { get; }
        bool TrainRunLeft { get; }
        bool TrainRightDoorOpen { get; }
        bool TrainRunRight { get; }
        double LimitSpeed { get; }
        Enum.StationModel StationModel { get; }
        double Speed { get; }

    }
}
namespace Subway.ShiJiaZhuangLine1.Interface.Model
{
    public interface ITractionLockModel : IViewModel
    {

        bool Car1LeftDoor { get; }
        bool Car2LeftDoor { get; }
        bool Car3LeftDoor { get; }
        bool Car4LeftDoor { get; }
        bool Car5LeftDoor { get; }
        bool Car6LeftDoor { get; }
        bool Car1RightDoor { get; }
        bool Car2RightDoor { get; }
        bool Car3RightDoor { get; }
        bool Car4RightDoor { get; }
        bool Car5RightDoor { get; }
        bool Car6RightDoor { get; }
        bool Car1Parking { get; }
        bool Car2Parking { get; }
        bool Car3Parking { get; }
        bool Car4Parking { get; }
        bool Car5Parking { get; }
        bool Car6Parking { get; }
        bool MasterFan { get; }
        bool Car1EnmergencyButton1 { get; }
        bool Car1EnmergencyButton2 { get; }
        bool Car2EnmergencyButton1 { get; }
        bool Car2EnmergencyButton2 { get; }

    }
}
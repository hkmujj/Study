namespace Subway.XiaMenLine1.Interface.Model
{
    public interface IAirCondition : IViewModel
    {
        Enum.AirSystemStatus Car1Air1 { get; }
        Enum.AirSystemStatus Car2Air1 { get; }
        Enum.AirSystemStatus Car3Air1 { get; }
        Enum.AirSystemStatus Car4Air1 { get; }
        Enum.AirSystemStatus Car5Air1 { get; }
        Enum.AirSystemStatus Car6Air1 { get; }
        Enum.AirSystemStatus Car1Air2 { get; }
        Enum.AirSystemStatus Car2Air2 { get; }
        Enum.AirSystemStatus Car3Air2 { get; }
        Enum.AirSystemStatus Car4Air2 { get; }
        Enum.AirSystemStatus Car5Air2 { get; }
        Enum.AirSystemStatus Car6Air2 { get; }
        double BackFanCar1Num1 { get; }
        double BackFanCar1Num2 { get; }
        double BackFanCar2Num1 { get; }
        double BackFanCar2Num2 { get; }
        double BackFanCar3Num1 { get; }
        double BackFanCar3Num2 { get; }
        double BackFanCar4Num1 { get; }
        double BackFanCar4Num2 { get; }
        double BackFanCar5Num1 { get; }
        double BackFanCar5Num2 { get; }
        double BackFanCar6Num1 { get; }
        double BackFanCar6Num2 { get; }

    }
}
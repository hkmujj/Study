namespace Subway.XiaMenLine1.Interface.Model
{
    public interface IFrsmHighSpeed : IViewModel
    {
        Enum.FrsmHighSpeed Car2HighSpeed { get; }
        Enum.FrsmHighSpeed Car3HighSpeed { get; }
        Enum.FrsmHighSpeed Car4HighSpeed { get; }
        Enum.FrsmHighSpeed Car5HighSpeed { get; }
        Enum.FrsmHighSpeed Car2Fram { get; }
        Enum.FrsmHighSpeed Car5Fram { get; }
        Enum.FrsmHighSpeed Car2Pantograph { get; }
        Enum.FrsmHighSpeed Car5Pantograph { get; }

    }
}
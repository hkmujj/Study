using Subway.ShiJiaZhuangLine1.Interface.Enum;

namespace Subway.ShiJiaZhuangLine1.Interface.Model
{
    public interface IFrsmHighSpeed : IViewModel
    {
        FrsmHighSpeed Car2HighSpeed { get; }
        FrsmHighSpeed Car3HighSpeed { get; }
        FrsmHighSpeed Car4HighSpeed { get; }
        FrsmHighSpeed Car5HighSpeed { get; }
        FrsmHighSpeed Car2Fram { get; }
        FrsmHighSpeed Car5Fram { get; }
        FrsmHighSpeed Car2Pantograph { get; }
        FrsmHighSpeed Car5Pantograph { get; }

    }
}
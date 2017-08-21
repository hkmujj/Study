using Subway.XiaMenLine1.Interface.Enum;

namespace Subway.XiaMenLine1.Interface.Model
{
    public interface ISmoke : IViewModel
    {
        SmokeStatus Car1SmokeStatus { get; }
        SmokeStatus Car2SmokeStatus { get; }
        SmokeStatus Car3SmokeStatus { get; }
        SmokeStatus Car4SmokeStatus { get; }
        SmokeStatus Car5SmokeStatus { get; }
        SmokeStatus Car6SmokeStatus { get; }
        
    }
}
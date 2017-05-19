using Subway.ShiJiaZhuangLine1.Interface.Enum;

namespace Subway.ShiJiaZhuangLine1.Interface.Model
{
    public interface IAssistModel : IViewModel
    {
        AssistPowerStatus Car1Organ { get; }
        AssistPowerStatus Car6Organ { get; }
        AssistPowerStatus Car3Organ { get; }
        AssistPowerStatus Car4Organ { get; }
        AssistPowerStatus Car1Charge { get; }
        AssistPowerStatus Car6Charge { get; }

    }
}
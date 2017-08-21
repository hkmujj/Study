namespace Subway.XiaMenLine1.Interface.Model
{
    public interface IAssistModel : IViewModel
    {
        Enum.AssistPowerStatus Car1Organ { get; }
        Enum.AssistPowerStatus Car6Organ { get; }
        Enum.AssistPowerStatus Car3Organ { get; }
        Enum.AssistPowerStatus Car4Organ { get; }
        Enum.AssistPowerStatus Car1Charge { get; }
        Enum.AssistPowerStatus Car6Charge { get; }

    }
}
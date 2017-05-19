using Subway.ShiJiaZhuangLine1.Interface.Enum;

namespace Subway.ShiJiaZhuangLine1.Interface.Model
{
    public interface IButton : IViewModel
    {
        AirConditionState AirConditionState { get; set; }
        AirPumpStatus AirPumpStatus { get; set; }
        AssistPowerState AirAssistPowerState { get; set; }
        DoorStatus DoorStatus { get; set; }
        BrakeStatus BrakeStatus { get; set; }
        EmergencyTalkState EmergencyTalkState { get; set; }
        TractionStatus TractionStatus { get; set; }
        SmokeStatus SmokeStatus { get; set; }
        FrsmHighSpeed FrsmHighSpeed { get; set; }
        string CurrentView { get; set; }

        void ResetButtonState();

    }
}
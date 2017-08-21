namespace Subway.XiaMenLine1.Interface.Model
{
    public interface IButton : IViewModel
    {
        Enum.AirConditionState AirConditionState { get; set; }
        Enum.AirPumpStatus AirPumpStatus { get; set; }
        Enum.AssistPowerState AirAssistPowerState { get; set; }
        Enum.DoorStatus DoorStatus { get; set; }
        Enum.BrakeStatus BrakeStatus { get; set; }
        Enum.EmergencyTalkState EmergencyTalkState { get; set; }
        Enum.TractionStatus TractionStatus { get; set; }
        Enum.SmokeStatus SmokeStatus { get; set; }
        Enum.FrsmHighSpeed FrsmHighSpeed { get; set; }

        void ResetButtonState();


        string Button8Str { get; set; }
        string Button9Str { get; set; }

    }
}
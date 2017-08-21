namespace MMI.Common.Msg.Interface.ButtonReactivation
{
    public interface IButtonInfo
    {
        int Logic { get; }
        int OutLogic { get; }
        string ChinaInfo { get; }
        string EnglishInfo { get; }
        ButtonStatus Status { get; set; }
    }
}
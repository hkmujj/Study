namespace MMI.Common.Msg.Interface.Stations
{
    public interface IStation
    {
        int LogicNum { get; }
        int Number { get; }
        string StationName { get; }
    }
}
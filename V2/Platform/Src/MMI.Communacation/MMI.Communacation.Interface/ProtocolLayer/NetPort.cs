namespace MMI.Communacation.Interface.ProtocolLayer
{
    public enum CommandNetPort
    {
        // ReSharper disable once InconsistentNaming
        Port_1000 = 59395,
        // ReSharper disable once InconsistentNaming
        Port_2000 = 53255,
        // ReSharper disable once InconsistentNaming
        Port_3000 = 47115,

        Default = Port_1000,
    }

    public enum DataNetPort
    {
        // ReSharper disable once InconsistentNaming
        Port_1000 = 59651,
        // ReSharper disable once InconsistentNaming
        Port_2000 = 53511,
        // ReSharper disable once InconsistentNaming
        Port_3000 = 47371,

        Default = Port_1000,
    }
}

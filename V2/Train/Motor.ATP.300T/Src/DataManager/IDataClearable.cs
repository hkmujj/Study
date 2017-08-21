namespace Motor.ATP._300T.DataManager
{
    public interface IDataClearable
    {
        void ClearData(DataClearFlag clearFlag = DataClearFlag.All);
    }

    public enum DataClearFlag
    {
        All,

        WaiteForDriver,
    }
}
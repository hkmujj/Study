namespace CBTC.DataAdapter
{
    public interface IDataAdapterFactory
    {
        DataAdapterBase CreateDataAdapter(CBTC.Infrasturcture.Model.CBTC cbtc);
    }
}
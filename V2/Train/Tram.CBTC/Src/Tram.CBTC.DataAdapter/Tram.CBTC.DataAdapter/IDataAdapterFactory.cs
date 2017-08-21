namespace Tram.CBTC.DataAdapter
{
    public interface IDataAdapterFactory
    {
        DataAdapterBase CreateDataAdapter(global::Tram.CBTC.Infrasturcture.Model.CBTC cbtc);
    }
}
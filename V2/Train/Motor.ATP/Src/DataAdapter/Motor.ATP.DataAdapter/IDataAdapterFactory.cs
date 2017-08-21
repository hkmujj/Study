using Motor.ATP.Infrasturcture.Model;

namespace Motor.ATP.DataAdapter
{
    public interface IDataAdapterFactory
    {
        DataAdapterBase CreateDataAdapter(ATPDomain atp);
    }
}
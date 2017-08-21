namespace Urban.Philippine.Adapter.Interface
{
    public interface IModelAdapterBase : IDataChanged
    {
        IModelAdapter Adapter { get; }
    }
}
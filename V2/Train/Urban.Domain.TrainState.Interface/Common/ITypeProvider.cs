namespace Urban.Domain.TrainState.Interface.Common
{
    public interface ITypeProvider
    {
        object Type { get; }
    }

    public interface ITypeProvider<out T> : ITypeProvider
    {
        new T Type { get; }
    }
}
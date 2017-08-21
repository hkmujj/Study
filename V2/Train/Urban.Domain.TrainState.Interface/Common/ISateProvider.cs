namespace Urban.Domain.TrainState.Interface.Common
{
    public interface ISateProvider
    {
        object State { get; }
    }

    public interface ISateProvider<out T> : ISateProvider
    {
        new T State { get; }
    }
}
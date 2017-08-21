namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 可区分车类型的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDifferentiateCarType<T>
    {
        T CarType { get; }
    }
}

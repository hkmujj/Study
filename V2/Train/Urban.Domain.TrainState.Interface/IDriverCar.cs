namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 司机室
    /// </summary>
    public interface IDriverCar : ICar
    {
        /// <summary>
        /// 司机室在用
        /// </summary>
        bool Driving { get; }
    }
}

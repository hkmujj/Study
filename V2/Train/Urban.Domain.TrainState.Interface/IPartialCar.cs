namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 车厢的一部分 
    /// </summary>
    public interface IPartialCar
    {
        /// <summary>
        /// 车号
        /// </summary>
        int CarNumber { get; }

        /// <summary>
        /// 
        /// </summary>
        ICar Parent { get; }
    }
}

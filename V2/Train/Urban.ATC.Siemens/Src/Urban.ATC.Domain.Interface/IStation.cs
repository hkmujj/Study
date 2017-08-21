namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 车站
    /// </summary>
    public interface IStation : ITrainInfoPartial, IVisibility
    {
        /// <summary>
        /// 当前车站
        /// </summary>
        string CurrentStation { set; get; }
    }
}
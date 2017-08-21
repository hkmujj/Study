namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 车辆信息的一部分
    /// </summary>
    public interface ITrainInfoPartial
    {
        ITrainInfo Parent { get; }
    }
}
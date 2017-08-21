namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 可用性
    /// </summary>
    public interface IEnabled
    {
        bool IsEnabled { set; get; }
    }
}
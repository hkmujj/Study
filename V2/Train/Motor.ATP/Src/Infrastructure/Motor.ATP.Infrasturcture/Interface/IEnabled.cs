namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 可用性
    /// </summary>
    public interface IEnabled
    {
        /// <summary>
        /// 可用性
        /// </summary>
        bool IsEnabled { set; get; }
    }
}
namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IATPPower : IATPPartial
    {
        /// <summary>
        /// 电源状态
        /// </summary>
        ATPPowerState ATPPowerState { get; }
    }
}
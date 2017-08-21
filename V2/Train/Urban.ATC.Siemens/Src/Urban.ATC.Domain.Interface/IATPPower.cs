namespace Motor.ATP.Domain.Interface
{
    public interface IATPPower : IATPPartial
    {
        /// <summary>
        /// 电源状态
        /// </summary>
        ATPPowerState ATPPowerState { get; }
    }
}
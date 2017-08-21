namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 可能故障的
    /// </summary>
    public interface IFaultable
    {
        /// <summary>
        /// 是否故障
        /// </summary>
        bool IsFault { set; get; }

        /// <summary>
        /// 故障信息
        /// </summary>
        string FaultInfo { set; get; }
    }

    public interface IFaultable<T> : IFaultable 
    {
        /// <summary>
        /// 故障类型
        /// </summary>
        T FaultType { set; get; }
    }

}

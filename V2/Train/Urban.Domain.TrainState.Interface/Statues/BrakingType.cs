namespace Urban.Domain.TrainState.Interface.Statues
{
    /// <summary>
    /// 制动类型
    /// </summary>
    public enum BrakingType
    {
        Unkown,

        /// <summary>
        /// 空气
        /// </summary>
        Air,

        /// <summary>
        /// 电力
        /// </summary>
        Electric,

        /// <summary>
        /// 常用 
        /// </summary>
        Ordinary,

        /// <summary>
        /// 紧急
        /// </summary>
        Emergent,
    }
}
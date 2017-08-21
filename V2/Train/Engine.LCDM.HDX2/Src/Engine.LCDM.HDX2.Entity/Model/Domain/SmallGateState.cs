namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    /// <summary>
    /// 小闸状态
    /// </summary>
    public enum SmallGateState
    {
        /// <summary>
        /// 缓解
        /// </summary>
        Relase,

        /// <summary>
        /// 紧急
        /// </summary>
        Emergance,

        /// <summary>
        /// 运转
        /// </summary>
        Work,

        /// <summary>
        /// 常用制动
        /// </summary>
        NormalBrake,

        /// <summary>
        /// 全制
        /// </summary>
        FullBrake

    }
}
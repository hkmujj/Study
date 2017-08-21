namespace LightRail.HMI.SZLHLF.Model.TrainModel
{
    /// <summary>
    /// 辅助电源
    /// </summary>
    public enum AssistPowerSource
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal,
        /// <summary>
        /// 严重故障
        /// </summary>
        SeriousTrouble,
        /// <summary>
        /// 中等，轻微故障
        /// </summary>
        SlightTrouble,
        /// <summary>
        /// 未运行
        /// </summary>
        NotOperation, 
    }
}

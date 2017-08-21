namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 发车状态
    /// </summary>
    public enum EmergencyBrakeStatus
    {
        None,

        /// <summary>
        /// 已实施紧急
        /// </summary>
        HasEB,

        /// <summary>
        /// 未实施紧急
        /// </summary>
        NotEB
    }
}
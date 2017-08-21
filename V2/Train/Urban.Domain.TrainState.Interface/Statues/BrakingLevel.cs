namespace Urban.Domain.TrainState.Interface.Statues
{
    /// <summary>
    /// 制动级位
    /// </summary>
    public enum BrakingLevel
    {
        None,
        B0,
        B1,
        B2,
        B3,
        B4,
        B5,
        B6,
        B7,

        /// <summary>
        /// 紧急制动
        /// </summary>
        Emergency,

        /// <summary>
        /// 停放制动
        /// </summary>
        Parking,
    }
}
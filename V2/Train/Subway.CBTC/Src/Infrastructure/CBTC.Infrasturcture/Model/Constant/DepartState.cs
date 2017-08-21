namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 发车状态
    /// </summary>
    public enum DepartState
    {
        None,

        ///// <summary>
        ///// 请求 发车
        ///// </summary>
        //RequireDepart,

        /// <summary>
        /// 发车条件具备 
        /// </summary>
        CanDepart,

        /// <summary>
        ///  列车对准但不能发车
        /// </summary>
        CannotDepart,

        ///// <summary>
        ///// 请求关闭车门
        ///// </summary>
        //RequireCloseDoor,

        /// <summary>
        /// 列车正在移动
        /// </summary>
        Moving,
    }
}
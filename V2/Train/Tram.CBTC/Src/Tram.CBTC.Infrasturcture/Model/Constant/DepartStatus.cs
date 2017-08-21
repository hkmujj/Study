namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 发车状态
    /// 0、无 
    /// 1、发车条件具备  
    /// 2、列车对准但发车条件不具备 
    /// 3、列车正在移动 
    /// 4、无效
    /// </summary>
    public enum DepartStatus
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 发车条件具备
        /// </summary>
        CanDepart,
        /// <summary>
        /// 列车对准但发车条件不具备
        /// </summary>
        ConnotDepart,
        /// <summary>
        /// 列车正在移动
        /// </summary>
        Moving,
        /// <summary>
        /// 无效
        /// </summary>
        Invalid
    }
}

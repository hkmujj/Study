namespace MMI.Facility.WPFInfrastructure.View
{
    /// <summary>
    /// 可以闪烁的
    /// </summary>
    public interface IFlickeringable
    {
        /// <summary>
        /// 闪烁的
        /// </summary>
        bool Flickering { set; get; }
    }
}

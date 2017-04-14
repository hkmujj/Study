namespace MMI.Facility.WPFInfrastructure.Behaviors
{
    /// <summary>
    /// View region 关系
    /// </summary>
    public interface IViewRegionRelevance
    {
        /// <summary>
        /// 
        /// </summary>
        string RegionName { get; }

        /// <summary>
        /// IsDefaultView  在 RegionName 有值时有效
        /// </summary>
        bool IsDefaultView { get; }

        /// <summary>
        /// Priority ， 越小排在越前,  在 RegionName 有值时有效
        /// </summary>
        int Priority { get; }
    }
}
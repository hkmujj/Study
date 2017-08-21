namespace MMI.Facility.WPFInfrastructure.Behaviors
{
    /// <summary>
    /// 
    /// </summary>
    public class ViewRegionRelevance : IViewRegionRelevance
    {
        /// <summary>
        /// 
        /// </summary>
        public string RegionName { get; internal set; }

        public bool IsDefaultView { get; internal set; }

        /// <summary>
        /// Priority ， 越小排在越前,  在 RegionName 有值时有效
        /// </summary>
        public int Priority { get; internal set; }
    }

}
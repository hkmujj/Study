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
        /// Priority �� ԽС����Խǰ,  �� RegionName ��ֵʱ��Ч
        /// </summary>
        public int Priority { get; internal set; }
    }

}
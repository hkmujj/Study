namespace MMI.Facility.WPFInfrastructure.Behaviors
{
    /// <summary>
    /// View region ��ϵ
    /// </summary>
    public interface IViewRegionRelevance
    {
        /// <summary>
        /// 
        /// </summary>
        string RegionName { get; }

        /// <summary>
        /// IsDefaultView  �� RegionName ��ֵʱ��Ч
        /// </summary>
        bool IsDefaultView { get; }

        /// <summary>
        /// Priority �� ԽС����Խǰ,  �� RegionName ��ֵʱ��Ч
        /// </summary>
        int Priority { get; }
    }
}
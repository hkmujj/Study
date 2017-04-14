namespace MMI.Facility.Interface.Project
{
    /// <summary>
    /// 子系统入口 
    /// </summary>
    public interface ISubsystem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="initParam"></param>
        void Initalize(SubsystemInitParam initParam);
    }
}
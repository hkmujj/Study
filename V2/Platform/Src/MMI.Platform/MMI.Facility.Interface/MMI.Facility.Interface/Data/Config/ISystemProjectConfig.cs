namespace MMI.Facility.Interface.Data.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISystemProjectConfig
    {
        /// <summary>
        /// 
        /// </summary>
        string ProjectName {  get; }

        /// <summary>
        /// 
        /// </summary>
        string ProjectConfigFile {  get; }

        /// <summary>
        /// 
        /// </summary>
        int StartProjectCount {  get; }

        /// <summary>
        /// 
        /// </summary>
        string Project1 {  get; }

        /// <summary>
        /// 
        /// </summary>
        string Project2 {  get; }

        /// <summary>
        /// 
        /// </summary>
        string Project3 {  get; }

        /// <summary>
        /// 
        /// </summary>
        string Project4 {  get; }

        /// <summary>
        /// 
        /// </summary>
        string ProjectIdentify {  get; }

        /// <summary>
        /// 
        /// </summary>
        string[] Projects { get; }

        /// <summary>
        /// 
        /// </summary>
        void Ready();
    }
}

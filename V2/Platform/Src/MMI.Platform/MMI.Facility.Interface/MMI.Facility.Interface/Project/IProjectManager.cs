using System.Collections.ObjectModel;

namespace MMI.Facility.Interface.Project
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProjectManager
    {
        /// <summary>
        /// 所有的工程名集合
        /// </summary>
        ReadOnlyCollection<string> ProjectNameCollection { get; }



    }
}
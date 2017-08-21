using System.Collections.ObjectModel;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Interface.Data.Config
{
    

    /// <summary>
    /// 
    /// </summary>
    public interface IIndexDescriptionConfig
    {
        /// <summary>
        /// 
        /// </summary>
        ReadOnlyCollection<IProjectIndexDescriptionConfig> IndexDescriptionConfigCollection { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IProjectIndexDescriptionConfig GetProjectIndexDescriptionConfig(ICommunicationDataKey key);

    }

    /// <summary>
    /// 
    /// </summary>
    public interface IProjectIndexDescriptionConfig
    {
        /// <summary>
        /// 
        /// </summary>
        ProjectType ProjectType { get; }

        /// <summary>
        /// 
        /// </summary>
        ReadOnlyCollection<string> AppNameCollection { get; }


        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<string, int> InBoolDescriptionDictionary { get; }

        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<string, int> InFloatDescriptionDictionary { get; }

        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<string, int> OutBoolDescriptionDictionary { get; }

        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<string, int> OutFloatDescriptionDictionary { get; }
    }
}
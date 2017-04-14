using MMI.Facility.Interface.IndexDescription;

namespace MMI.Facility.Interface.Data.Config
{
    /// <summary>
    /// 
    /// </summary>

    public interface IAppCommunicationInterfaceModel
    {
        /// <summary>
        /// 
        /// </summary>
        AppIndexType IndexType { get; }

        /// <summary>
        /// 相对路径
        /// </summary>
        string RelativeFilePath { get; }

        /// <summary>
        /// 名字索引 对
        /// </summary>
        IReadOnlyDictionary<string, int> NameIndexDictionary { get; }
    }
}
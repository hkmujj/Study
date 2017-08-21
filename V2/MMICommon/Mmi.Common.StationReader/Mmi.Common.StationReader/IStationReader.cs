using System.Collections.ObjectModel;
using System.IO;

namespace Mmi.Common.StationReader
{
    public interface IStationReader
    {
        /// <summary>
        /// 所有的站点集合
        /// </summary>
        ReadOnlyCollection<StationModel> StationModelCollection { get; }

        /// <summary>
        /// 加载车站文件
        /// </summary>
        /// <param name="file"></param>
        bool Load(FileInfo file);
    }
}
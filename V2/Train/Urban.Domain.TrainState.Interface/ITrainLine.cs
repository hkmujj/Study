using System.Collections.ObjectModel;

namespace Urban.Domain.TrainState.Interface
{
    public interface ITrainLine
    {
        /// <summary>
        /// 车次号
        /// </summary>
        string TrainLineNumber { get; }

        /// <summary>
        /// 所有车站
        /// </summary>
        ReadOnlyCollection<IStation> StationCollection { get; }

        /// <summary>
        /// 当前站
        /// </summary>
        IStation CurrentStation { get; }

        /// <summary>
        /// 下一站
        /// </summary>
        IStation NextStation { get; }

        /// <summary>
        /// 上一站
        /// </summary>
        IStation PreviousStation { get; }

    }
}
using System.Collections.ObjectModel;

namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 列车单元
    /// </summary>
    public interface ITrainUnit
    {
        /// <summary>
        /// 单元号
        /// </summary>
        int UnitNumber { get; }

        /// <summary>
        /// 单元所有的车厢
        /// </summary>
        ReadOnlyCollection<ICar> Cars { get; }

    }
}
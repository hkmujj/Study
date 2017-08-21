using System.Collections.ObjectModel;
using Urban.Domain.TrainState.Interface.Common;
using Urban.Domain.TrainState.Interface.Infomation;
using Urban.Domain.TrainState.Interface.Statues;

namespace Urban.Domain.TrainState.Interface
{
    public interface ITrain : IListeningModelProvider, IStateUpdatable, IVisibility
    {
        TrainType TrainType { get; }

        /// <summary>
        /// ATP
        /// </summary>
        IATP ATP { get; }

        /// <summary>
        /// 电源
        /// </summary>
        IPower Power { get; }

        /// <summary>
        /// 牵引
        /// </summary>
        ITraction Traction { get; }

        /// <summary>
        /// 制动
        /// </summary>
        ReadOnlyCollection<IBraking> BrakingCollection { get; }

        /// <summary>
        /// 速度
        /// </summary>
        ISpeed Speed { get; }

        /// <summary>
        /// 列车单元
        /// </summary>
        ReadOnlyCollection<ITrainUnit> TrainUnits { get; }
        
        /// <summary>
        /// 所有车厢
        /// </summary>
        ReadOnlyCollection<ICar> Cars { get; }

        /// <summary>
        /// 列车线路
        /// </summary>
        ITrainLine TrainLine { get; }

        /// <summary>
        /// 消息集合
        /// </summary>
        IInfomationCollection InfomationCollection { get; }

    }

    
}

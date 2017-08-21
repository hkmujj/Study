using System.Collections.ObjectModel;
using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Interface
{
    public interface ICar : ITrainPartial, ITemperatureMensurability, IListeningModelProvider, IStateUpdatable, ITypeProvider
    {

        int CarNumber { get; }

        /// <summary>
        /// 车厢名
        /// </summary>
        string CarName { get; }

        /// <summary>
        /// 单元号
        /// </summary>
        ITrainUnit TrainUnit { get; }

        ICarPower Power { get; }

        /// <summary>
        /// 洗手间
        /// </summary>
        IToilet Toilet { get; }

        /// <summary>
        /// 受电弓
        /// </summary>
        ReadOnlyCollection<IPantograph> Pantographs { get; }

        /// <summary>
        /// 车门
        /// </summary>
        ReadOnlyCollection<IDoor> Doors { get; }

        /// <summary>
        /// 转向架
        /// </summary>
        IBogies Bogies { get; }

        /// <summary>
        /// 制动状态
        /// </summary>
        ICarBraking CarBraking { get; }

        /// <summary>
        /// 风缸
        /// </summary>
        ReadOnlyCollection<IAirCylinder> AirCylinder { get; }

        /// <summary>
        /// 空调系统
        /// </summary>
        ReadOnlyCollection<IHVAC> HvacCollection { get; }

        /// <summary>
        /// 乘客信息
        /// </summary>
        ReadOnlyCollection<IPassenger> PassengerCollection { get; }
    }
}
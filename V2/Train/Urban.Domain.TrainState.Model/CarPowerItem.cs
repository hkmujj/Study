using System.Diagnostics;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Common;
using Urban.Domain.TrainState.Interface.Statues;

namespace Urban.Domain.TrainState.Model
{
    [DebuggerDisplay("Type={Type} State={State}")]
    public class CarPowerItem : UpdatingProvider<CarPowerItem>, ICarPowerItem
    {
        [DebuggerStepThrough]
        public CarPowerItem(CarPowerType type)
        {
            Type = type;
        }

        object ISateProvider.State
        {
            get { return State; }
        }

        public CarPowerState State { get; set; }

        object ITypeProvider.Type
        {
            get { return Type; }
        }

        public CarPowerType Type { get; private set; }
    }
}
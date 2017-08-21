using Urban.Domain.TrainState.Interface.Statues;

namespace Urban.Domain.TrainState.Interface
{
    public interface IPassenger
    {
        PassengerState PassengerState { get; }
    }

}
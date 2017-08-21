using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Statues;

namespace Urban.Domain.TrainState.Model
{
    public class Passenger : UpdatingProvider<Passenger>, IPassenger
    {
        public PassengerState PassengerState { get; set; }
    }
}
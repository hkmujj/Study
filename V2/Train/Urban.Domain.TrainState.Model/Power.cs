using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Model
{
    public class Power : UpdatingProvider<Power>, IPower
    {
        public ContactLinePower ContactLinePower { get; protected set; }

        IContactLinePower IPower.ContactLinePower
        {
            get { return ContactLinePower; }
        }

        public AccumulatorPower AccumulatorPower { get; protected set; }

        IAccumulatorPower IPower.AccumulatorPower
        {
            get { return AccumulatorPower; }
        }

        public Power()
        {
            ContactLinePower = new ContactLinePower();
            AccumulatorPower = new AccumulatorPower();
        }

        protected override void OnUpdating(Power target, IUpdateParam updateParam)
        {
            ContactLinePower.Update(updateParam);

            AccumulatorPower.Update(updateParam);
        }
    }
}
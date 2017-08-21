using Urban.Domain.TrainState.Interface;

namespace Urban.Domain.TrainState.Model
{
    public class TrainStateBase : ITrainState
    {
        object IIdentityProvide.Identity
        {
            get { return Identity; }
        }

        public ScreenIdentity Identity { get; set; }

        public TrainBase Train { set; get; }

        ITrain ITrainState.Train { get { return Train; } }
    }
}
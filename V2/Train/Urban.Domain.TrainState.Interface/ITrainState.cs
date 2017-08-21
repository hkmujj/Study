namespace Urban.Domain.TrainState.Interface
{
    public interface ITrainState : IIdentityProvide<ScreenIdentity>
    {
        ITrain Train { get; }
    }
}
namespace Urban.Domain.TrainState.Interface
{
    public interface IExternalEnvironment : ITemperatureMensurability
    {
        IContactLine ContactLine { get; }
    }
}

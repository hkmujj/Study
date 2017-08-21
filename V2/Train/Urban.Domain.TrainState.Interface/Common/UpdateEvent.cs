namespace Urban.Domain.TrainState.Interface.Common
{
    public delegate void UpdateEvent<in T>(T target, IUpdateParam updateParam);
}
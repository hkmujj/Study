namespace Motor.ATP.Domain.Interface
{
    public interface IConnectState : ITrainInfoPartial, IVisibility
    {
        GSMRState GSMRState { get; }

        RBCConnectState RBCConnectState { get; }

        string RBCID { get; }

        string TelNumber { get; }
    }
}
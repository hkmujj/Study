using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 接触网电源
    /// </summary>
    public interface IContactLinePower : IListeningModelProvider, IStateUpdatable
    {
        float ContactLineVoltage { get; }   
    }
}
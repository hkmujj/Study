using Urban.Domain.TrainState.Interface.Statues;

namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 牵引
    /// </summary>
    public interface ITraction
    {
        TractionLevel TractionLevel { get; }

        float TractionValue { get; }
    }
}
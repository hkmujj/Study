using Urban.Domain.TrainState.Interface.Statues;

namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 可以被切除的
    /// </summary>
    public interface ICanCutPart
    {
        string CanCutPartName { get; }

        UseState UseState { get; }
    }
}
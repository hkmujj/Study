using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Domain.Interface
{
    public interface IMessgeInfo
    {
        string Content { get; }
        int LogicID { get; }
        InfoLevl Level { get; }
        bool IsConfirm { get; }
    }
}
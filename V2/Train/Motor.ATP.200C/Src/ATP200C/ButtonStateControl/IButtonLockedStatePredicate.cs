using ATP200C.FunctionButton;

namespace ATP200C.ButtonStateControl
{
    public interface IButtonLockedStatePredicate
    {
        bool IsLocked(ButtonInfo btnInfo);
    }
}
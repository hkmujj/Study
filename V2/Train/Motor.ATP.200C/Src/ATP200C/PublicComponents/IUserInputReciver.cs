using ATP200C.MainView;

namespace ATP200C.PublicComponents
{
    public interface IUserInputReciver
    {
        void ActionCommand(ATPBaseClass baseClass, UserActionCommand cmd);

        void ActionData(UserActionData data);
    }
}
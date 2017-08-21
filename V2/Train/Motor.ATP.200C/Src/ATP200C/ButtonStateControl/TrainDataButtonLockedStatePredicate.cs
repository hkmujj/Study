using ATP200C.FunctionButton;
using ATP200C.MainView;
using ATP200C.PublicComponents;

namespace ATP200C.ButtonStateControl
{
    public class TrainDataButtonLockedStatePredicate : IButtonLockedStatePredicate
    {
        public bool IsLocked(ButtonInfo btnInfo)
        {
            return ATPMain.Instance.ControlModel != ControModelEnum.待机;
        }
    }
}
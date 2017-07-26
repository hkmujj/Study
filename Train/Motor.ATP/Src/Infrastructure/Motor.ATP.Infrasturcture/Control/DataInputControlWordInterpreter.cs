using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.Infrasturcture.Control
{
    /// <summary>
    /// 输入状态控制
    /// </summary>
    public class DataInputControlWordInterpreter : IDriverInputInterpreter
    {
        private readonly UserActionType m_OkActionType;

        private readonly UserActionType m_CancelActionType;

        private readonly UserActionType m_DeleteActionType;

        public DataInputControlWordInterpreter(UserActionType okActionType = UserActionType.F6,
            UserActionType deleteActionType = UserActionType.F7, UserActionType cancelActionType = UserActionType.F8)
        {
            m_OkActionType = okActionType;
            m_CancelActionType = cancelActionType;
            m_DeleteActionType = deleteActionType;
        }

        public DriverInputState InputState
        {
            get { return DriverInputState.Other; }
        }

        public void Reset()
        {

        }

        public DriverInputInterpreterResult Interpreter(UserActionType actionType)
        {
            if (m_OkActionType != UserActionType.None && actionType == m_OkActionType)
            {
                return new DriverInputInterpreterResult(DriverInputInterpreterResult.InputType.Control,
                    DriverInputControlWord.Ok);
            }
            if (m_DeleteActionType != UserActionType.None && actionType == m_DeleteActionType)
            {
                return new DriverInputInterpreterResult(DriverInputInterpreterResult.InputType.Control,
                    DriverInputControlWord.Delete);
            }
            if (m_DeleteActionType != UserActionType.None && actionType == m_CancelActionType)
            {
                return new DriverInputInterpreterResult(DriverInputInterpreterResult.InputType.Control,
                    DriverInputControlWord.Cancel);
            }

            return DriverInputInterpreterResult.InvalidateResult;
        }
    }
}
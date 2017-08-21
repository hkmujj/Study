namespace Motor.ATP.Domain.Interface.UserAction
{
    public interface IDriverInputInterpreter
    {
        DriverInputState InputState { get; }

        void Reset();

        DriverInputInterpreterResult Interpreter(UserActionType actionType);
    }
}
using System.Diagnostics.Contracts;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.InputDataInterpreter
{
    public class TrainDataInputDataInterpreter : IDriverInputInterpreter
    {
        public TrainDataInputDataInterpreter()
        {
            InputState = DriverInputState.Other;
        }

        public string[] Lengths { set; get; }

        public DriverInputState InputState { get; private set; }
        public void Reset()
        {
            
        }

        public DriverInputInterpreterResult Interpreter(UserActionType actionType)
        {
            Contract.Requires(Lengths != null && Lengths.Length >= 2);

            if (actionType == UserActionType.F1)
            {
                return new DriverInputInterpreterResult(Lengths[0]);
            }
            else if (actionType == UserActionType.F2)
            {
                return new DriverInputInterpreterResult(Lengths[1]);
            }

            return null;
        }
    }
}
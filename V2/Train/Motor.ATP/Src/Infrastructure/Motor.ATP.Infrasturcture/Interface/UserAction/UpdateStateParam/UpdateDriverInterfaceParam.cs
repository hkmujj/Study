using System.Diagnostics;

namespace Motor.ATP.Infrasturcture.Interface.UserAction.UpdateStateParam
{
    public class UpdateDriverInterfaceParam : IUpdateDriverInterfaceParam
    {
        [DebuggerStepThrough]
        public UpdateDriverInterfaceParam(object sender)
        {
            Sender = sender;
        }

        public object Sender { get; private set; }
    }
}
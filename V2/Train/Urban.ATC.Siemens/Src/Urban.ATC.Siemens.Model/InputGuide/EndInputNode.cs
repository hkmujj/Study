using System.Diagnostics;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Model.InputGuide
{
    [DebuggerDisplay("EndInputNode, CurrentInputKey = {CurrentInputKey}")]
    public sealed class EndInputNode : InputNodeBase
    {
        public static readonly EndInputNode Default = new EndInputNode();

        public override bool IsNeedInput()
        {
            return false;
        }

        private EndInputNode() :
            base(DriverInterfaceKey.Root, null)
        {
        }
    }
}
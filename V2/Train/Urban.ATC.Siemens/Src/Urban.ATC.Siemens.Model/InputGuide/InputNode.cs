using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Motor.ATP.Domain.Interface.InputGuide;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Model.InputGuide
{
    public class InputNode : InputNodeBase
    {
        private readonly Expression<Func<bool>> m_NeedInputProvider;

        public override bool IsNeedInput()
        {
            return m_NeedInputProvider == null || m_NeedInputProvider.Compile()();
        }

        [DebuggerStepThrough]
        public InputNode(DriverInterfaceKey currentInputKey, IInputNode nextNode, Expression<Func<bool>> needInputProvider = null)
            : base(currentInputKey, nextNode)
        {
            m_NeedInputProvider = needInputProvider;
        }


        [DebuggerStepThrough]
        public InputNode(DriverInterfaceKey currentInputKey, Expression<Func<bool>> needInputProvider = null)
            : base(currentInputKey, EndInputNode.Default)
        {
            m_NeedInputProvider = needInputProvider;
        }
    }
}
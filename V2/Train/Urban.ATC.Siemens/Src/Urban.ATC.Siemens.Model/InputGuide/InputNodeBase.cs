using System.Diagnostics;
using Motor.ATP.Domain.Interface.InputGuide;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Model.InputGuide
{
    [DebuggerDisplay("CurrentInputKey = {CurrentInputKey}")]
    public abstract class InputNodeBase : IInputNode
    {
        private IInputNode m_NextNode;

        public DriverInterfaceKey CurrentInputKey { get; private set; }

        public IInputNode PreviousNode { get; private set; }

        public IInputNode NextNode
        {
            get { return m_NextNode; }
            internal set
            {
                m_NextNode = value;
                var next = m_NextNode as InputNodeBase;
                if (next != null && next != EndInputNode.Default) 
                {
                    next.PreviousNode = this;
                }
            }
        }

        /// <summary>
        /// 是否需要输入数据
        /// </summary>
        /// <returns></returns>
        public virtual bool IsNeedInput()
        {
            return true;
        }

        protected InputNodeBase(DriverInterfaceKey currentInputKey, IInputNode nextNode)
        {
            CurrentInputKey = currentInputKey;
            NextNode = nextNode;
        }
    }
}
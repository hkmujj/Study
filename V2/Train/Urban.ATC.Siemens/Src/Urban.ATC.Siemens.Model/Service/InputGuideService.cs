using System.Diagnostics.Contracts;
using CommonUtil.Model;
using Motor.ATP.Domain.Interface.InputGuide;
using Motor.ATP.Domain.Interface.Service;
using Motor.ATP.Domain.Interface.UserAction;
using Motor.ATP.Domain.Model.InputGuide;

namespace Motor.ATP.Domain.Model.Service
{
    public class InputGuideService : ATPPartialBase, IInputGuideService
    {
        /// <summary>
        /// 当前输入节点
        /// </summary>
        public IInputNode CurrentInputNode { get; private set; }

        /// <summary>
        /// 步骤对应的root输入节点
        /// </summary>
        protected IReadOnlyDictionary<InputStepType, IInputNode> m_StepInputRootNodeDictionary;


        public InputGuideService(ATPDomain parent, IReadOnlyDictionary<InputStepType, IInputNode> stepInputRootNodeDictionary)
            : base(parent)
        {
            Contract.Requires(stepInputRootNodeDictionary != null);
            m_StepInputRootNodeDictionary = stepInputRootNodeDictionary;
            CurrentInputNode.IsEndNode();
        }

        public virtual void SelectInputStep(InputStepType stepType)
        {
            CurrentInputNode = m_StepInputRootNodeDictionary.ContainsKey(stepType)
                ? m_StepInputRootNodeDictionary[stepType]
                : EndInputNode.Default;
        }

        public virtual bool Goforward()
        {
            if (CurrentInputNode.IsEndNode())
            {
                if (CurrentInputNode == null)
                {
                    CurrentInputNode = EndInputNode.Default;
                }
                return false;
            }

            var next = CurrentInputNode.NextNode;
            while (!next.IsNeedInput() && !next.IsEndNode())
            {
                next = next.NextNode;
            }
            CurrentInputNode = next;
            return true;
        }

        public bool Contains(DriverInterfaceKey key)
        {
            if (CurrentInputNode.IsEndNode() || CurrentInputNode == null)
            {
                return false;
            }
            var next = CurrentInputNode;
            while (next.CurrentInputKey != key && !next.IsEndNode())
            {
                next = next.NextNode;
            }
            return next.CurrentInputKey == key;
        }
    }
}
using System;
using System.Linq.Expressions;
using Motor.ATP.Domain.Interface.InputGuide;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Model.InputGuide
{
    public class InputNodeLinkBuilder
    {
        public IInputNode Result { private set; get; }

        public InputNodeLinkBuilder()
        {
            Result = EndInputNode.Default;
        }

        public void Clear()
        {
            Result = EndInputNode.Default;
        }

        public void AddNextNode(DriverInterfaceKey currentInputKey, Expression<Func<bool>> needInputProvider = null)
        {
            if (Result is EndInputNode)
            {
                Result = new InputNode(currentInputKey, needInputProvider);
            }
            else
            {
                var endParent = (InputNodeBase)FindEndNodeParent();

                endParent.NextNode = new InputNode(currentInputKey, needInputProvider);
            }
        }

        private IInputNode FindEndNodeParent()
        {
            var node = Result;
            while (node.NextNode != EndInputNode.Default)
            {
                node = node.NextNode;
            }
            return node;
        }
    }
}
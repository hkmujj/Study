using Motor.ATP.Domain.Interface.InputGuide;

namespace Motor.ATP.Domain.Model.InputGuide
{
    // ReSharper disable once InconsistentNaming
    public static class IInputNodeExtension
    {
        public static bool IsEndOrEmpty(this IInputNode node)
        {
            return node == EndInputNode.Default || node == null;
        }

        public static bool IsEndNode(this IInputNode node)
        {
            return node is EndInputNode;
        }
    }
}
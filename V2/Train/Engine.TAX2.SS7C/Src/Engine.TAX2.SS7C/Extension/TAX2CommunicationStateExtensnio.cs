using Engine.TAX2.SS7C.Model.Domain.Constant;

namespace Engine.TAX2.SS7C.Extension
{
    public static class TAX2CommunicationStateExtension
    {
        public static bool IsCommunicationFail(this TAX2CommunicationState state)
        {
            return state != TAX2CommunicationState.Normal;
        }

        public static bool IsCommunicationUnkown(this TAX2CommunicationState state)
        {
            return state == TAX2CommunicationState.Unkown;
        }
    }
}
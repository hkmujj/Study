using System.Runtime.InteropServices;

namespace MMI.Communacation.Control.ProtocolLayer.RecvPackage
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct RecvNetDataTypeC
    {
        [FieldOffset(0)]
        public RecvDataType DataType;
    }
}
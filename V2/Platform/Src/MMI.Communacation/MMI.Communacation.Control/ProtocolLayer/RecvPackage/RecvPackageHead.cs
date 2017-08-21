using System.Runtime.InteropServices;

namespace MMI.Communacation.Control.ProtocolLayer.RecvPackage
{
    /// <summary>
    ///  �������� ����ͷ�� �����ṹ
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct RecvPackageHead
    {
        [FieldOffset(0)]
        public int Value;

        [FieldOffset(0)]
        public RecvNetDataTypeC TypeC;

        [FieldOffset(0)]
        public NetDataTypeD TypeD;
    }
}
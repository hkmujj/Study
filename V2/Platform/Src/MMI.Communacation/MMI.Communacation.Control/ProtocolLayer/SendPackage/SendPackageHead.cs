using System.Runtime.InteropServices;
using MMI.Communacation.Control.ProtocolLayer.RecvPackage;

namespace MMI.Communacation.Control.ProtocolLayer.SendPackage
{
    /// <summary>
    /// 发送数据包结构
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct SendPackageHead
    {
        [FieldOffset(0)]
        public int Value;

        [FieldOffset(0)]
        public SendDataTypeC TypeC;

        [FieldOffset(0)]
        public NetDataTypeD TypeD;
    }
}
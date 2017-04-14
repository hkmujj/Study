using System;
using MMI.Communacation.Interface.ProtocolLayer;

namespace MMI.Communacation.Control.ProtocolLayer.SendPackage
{
    /// <summary>
    /// SendPackageHeadWrapper， 使用Wrapper是避免Struct实现接口
    /// </summary>
    public class SendPackageHeadWrapper : ISend
    {
        public SendPackageHead PackageHead;

        public SendPackageHeadWrapper()
        {
            PackageHead = new SendPackageHead();
        }

        public byte[] ToSendBytes()
        {
            return BitConverter.GetBytes(PackageHead.Value);
        }
    }
}

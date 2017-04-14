using System;
using System.Net;

namespace MMI.Communacation.Control.ProtocolLayer
{
    class NoneNetProtocolService : NetProtocolServiceBase
    {
        public override void Send(string ip, int port, byte[] data)
        {
            throw new NotImplementedException();
        }

        public override void Send(IPEndPoint ipEndPoint, byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}

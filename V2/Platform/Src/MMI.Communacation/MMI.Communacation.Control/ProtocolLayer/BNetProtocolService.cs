using System;
using System.Net;
using System.Net.Sockets;
using CommonUtil.Util;
using MMI.Communacation.Control.ConcreateProtocol;
using MMI.Communacation.Control.ProtocolLayer.RecvPackage;
using MMI.Facility.Interface.Data.Config;
using Timer = System.Timers.Timer;

namespace MMI.Communacation.Control.ProtocolLayer
{
    class BNetProtocolService : NetProtocolServiceBase
    {
        private BNetProtocol m_BNetProtocol;
        private Timer m_NotifyOnlineTimer;

        public override void Initialize(IConfig config)
        {
            InitalizeNetProtocol(config);
        }

        private void InitalizeNetProtocol(IConfig config)
        {
            m_BNetProtocol = new BNetProtocol();
            m_BNetProtocol.OnFmsSysBegin += (sender, args) => OnBegin(args);
            m_BNetProtocol.OnFmsSysEnd += (sender, args) => OnEnd(args);
            m_BNetProtocol.OnFmsMMIRealDataReceive += (bt, index) => OnDataReceived(bt, new RecvPackageHead() {Value = index});

            m_BNetProtocol.Intalize(config);
            m_BNetProtocol.Start();
            m_NotifyOnlineTimer = new Timer(2000);
            m_NotifyOnlineTimer.Elapsed += (sender, args) => m_BNetProtocol.NotifyOnline();
            m_NotifyOnlineTimer.Start();
        }

        public override void Close()
        {
            if (m_BNetProtocol != null)
            {
                m_BNetProtocol.Stop();
                m_BNetProtocol.Dispose();
            }
            if (m_NotifyOnlineTimer != null)
            {
                m_NotifyOnlineTimer.Close();
            }
            base.Close();
        }

        public override void Send(string ip, int port, byte[] data)
        {
            Send(new IPEndPoint(IPAddress.Parse(ip), port), data);
        }

        public override void Send(IPEndPoint ipEndPoint, byte[] data)
        {

            if (data == null)
            {
                return;
            }

            try
            {
                var sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                sock.SendTo(data, ipEndPoint);
                sock.Close();
            }
            catch (Exception e)
            {
                LogMgr.Error(string.Format("Send udp data error, {0}", e.Message));
            }
        }
    }
}

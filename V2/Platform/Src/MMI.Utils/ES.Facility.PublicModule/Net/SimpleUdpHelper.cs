using System;
using System.Net.Sockets;

namespace ES.Facility.PublicModule.Net
{
    /// <summary>
    /// ��UDPʹ����
    /// </summary>
    public class SimpleUdpHelper
    {

        #region ::::::::::::::::::::::::::::::::  variable   :::::::::::::::::::::::::::::::

        /// <summary>����ʹ��</summary>
        [CLSCompliant(false)]
        protected UdpClient _netUdpListener;

        /// <summary>����״̬</summary>
        [CLSCompliant(false)]
        protected bool _udpIsListener = false;


        #endregion
    }
}

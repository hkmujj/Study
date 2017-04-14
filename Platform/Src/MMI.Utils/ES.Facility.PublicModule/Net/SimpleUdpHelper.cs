using System;
using System.Net.Sockets;

namespace ES.Facility.PublicModule.Net
{
    /// <summary>
    /// 简单UDP使用类
    /// </summary>
    public class SimpleUdpHelper
    {

        #region ::::::::::::::::::::::::::::::::  variable   :::::::::::::::::::::::::::::::

        /// <summary>监听使用</summary>
        [CLSCompliant(false)]
        protected UdpClient _netUdpListener;

        /// <summary>监听状态</summary>
        [CLSCompliant(false)]
        protected bool _udpIsListener = false;


        #endregion
    }
}

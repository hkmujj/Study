using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace ES.Facility.PublicModule.Test
{
    /// <summary>
    /// FMS需要的IP地址类型
    /// </summary>
    public class IPAddressInfo : IIPAddressInfo
    {
        public string IpName = string.Empty;

        public int PortNum = 0;

        public IPEndPoint IpPoint = null;
        public IPEndPoint IpPointOnlyIp = null;

        public void SetIpPoint()
        {
            SetIpPoint(IpName, PortNum);
        }

        /// <summary>
        /// 设置IpPoint
        /// </summary>
        /// <param name="cip">IP</param>
        /// <param name="nport">Port</param>
        public void SetIpPoint(string cip, int nport)
        {
            if (IpPoint != null)
            {
                IpPoint.Address = IPAddress.Parse(cip);
                IpPoint.Port = nport;
            }
            else
            {
                IpPoint = new IPEndPoint(IPAddress.Parse(cip), nport);
            }

            if (IpPointOnlyIp != null)
            {
                IpPointOnlyIp.Address = IPAddress.Parse(cip);
                IpPointOnlyIp.Port = 0;
            }
            else
            {
                IpPointOnlyIp = new IPEndPoint(IPAddress.Parse(cip), 0);
            }

            IpName = IpPoint.Address.ToString();
            PortNum = IpPoint.Port;
        }

        public IPAddressInfo()
        {
        }

        public IPAddressInfo(string cip, int nport)
        {
            IpName = cip;
            PortNum = nport;
            SetIpPoint();
        }

        public IPAddressInfo(string cIp, NetPort nPortIndex, bool isCmd)
        {
            IpName = cIp;
            SetPortFromIndex(nPortIndex, isCmd);

            SetIpPoint();
        }

        /// <summary>
        /// FMS定义的端口类型
        /// </summary>
        public enum NetPort
        {
            Port_1000,
            Port_2000,
            Port_3000,
            Port_4000
        }

        /// <summary>
        /// 按照FMS定义设置实际端口
        /// </summary>
        /// <param name="nPortIndex"></param>
        /// <param name="isCmd"></param>
        /// <returns></returns>
        public bool SetPortFromIndex(NetPort nPortIndex, bool isCmd)
        {
            return getPortByIndex(nPortIndex, isCmd, out PortNum);
        }

        /// <summary>
        /// 按照FMS的设置进行端口分配
        /// </summary>
        /// <param name="nPortIndex">FMS 端口定义</param>
        /// <param name="isCmd">是否是命令，非命令用实时数据传送</param>
        /// <param name="portNum">输出的端口号</param>
        /// <returns>True 存在该端口定义, False 不存在该端口定义 默认使用Port_1000</returns>
        public static bool getPortByIndex(NetPort nPortIndex, bool isCmd, out int portNum)
        {
            switch (nPortIndex)
            {
                case NetPort.Port_1000 :
                    portNum = isCmd ? 59395 : 59651;
                    break;
                case NetPort.Port_2000 :
                    portNum = isCmd ? 53255 : 53511;
                    break;
                case NetPort.Port_3000 :
                    portNum = isCmd ? 47115 : 47371;
                    break;
                case NetPort.Port_4000 :
                    portNum = isCmd ? 41231 : 41487;
                    break;
                    //默认使用Port_1000端口
                default :
                    portNum = isCmd ? 59395 : 59651;
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 按照FMS的设置进行端口分配
        /// </summary>
        /// <param name="nPortIndex">FMS 端口定义</param>
        /// <param name="isCmd">是否是命令，非命令用实时数据传送</param>
        /// <param name="portNum">输出的端口号</param>
        /// <returns>True 存在该端口定义, False 不存在该端口定义 默认使用Port_1000</returns>
        public static int GetPortByIndex(NetPort nPortIndex, bool isCmd)
        {
            int port;
            getPortByIndex(nPortIndex, isCmd, out port);
            return port;
        }
    }

    /// <summary>
    /// FMS使用的IP地址类型，用于替代 IPAddressInfo
    /// </summary>
    public class FmsSysAddreBaseInfo : IFmsSysAddreBaseInfo
    {
        public bool IsOk = false;
        public string ip = "127.0.0.1";
        public IPAddressInfo.NetPort port = IPAddressInfo.NetPort.Port_1000;
        public string SysName = string.Empty;
        public int SysState = -1;          //系统状态

        //.......................................
        //为统计而增加
       
        //.......................................
        protected int FmsCmdPort = 0;
        protected int FmsRealPort = 0;

        public IPAddress ipAddress;

        public FmsSysAddreBaseInfo() { }
        public FmsSysAddreBaseInfo(string nip, IPAddressInfo.NetPort nFmsPort)
            : base()
        {
            ip = nip; port = nFmsPort;
            IsOk = IPAddress.TryParse(ip, out ipAddress);

            SysName = "unKonw";
            

            IPAddressInfo.getPortByIndex(port, true, out FmsCmdPort);
            IPAddressInfo.getPortByIndex(port, false, out FmsRealPort);
        }

        public FmsSysAddreBaseInfo(string nip, IPAddressInfo.NetPort nFmsPort , string nSysName)
            : base()
        {
            ip = nip; port = nFmsPort;
            IsOk = IPAddress.TryParse(ip, out ipAddress);

            SysName = nSysName;

            IPAddressInfo.getPortByIndex(port, true, out FmsCmdPort);
            IPAddressInfo.getPortByIndex(port, false, out FmsRealPort);
        }

        public bool CopyToArr(ref byte[] outBytes, int nBeginIndex)
        {
            if (!IsOk) return false;
            if (outBytes.Length < 4) return false;

            //将端口转为字节数组
            ipAddress.GetAddressBytes().CopyTo(outBytes, nBeginIndex);



            return true;
        }

        public int getFmsPort(bool isCmd)
        {
            return isCmd ? FmsCmdPort : FmsRealPort;
        }

        public int getDefinePortNum()
        {
            if (port == IPAddressInfo.NetPort.Port_4000) return 4000;
            else if (port == IPAddressInfo.NetPort.Port_3000) return 3000;
            else if (port == IPAddressInfo.NetPort.Port_2000) return 2000;
            else return 1000;
        }
    }

    /// <summary>
    /// 记录使用的IP地址类型
    /// </summary>
    public class FmsAddressInfo : FmsSysAddreBaseInfo
    {
        public int SysId = 0;       //系统ID

        public int RecCmdCount = 0;     //接受到的命令数据
        public int RecRealCount = 0;    //接受到的实时数据

        public int SendCmdCount = 0;    //发送的命令数据
        public int SendRealCount = 0;   //发送的实时数据

        public FmsAddressInfo() { }
        public FmsAddressInfo(int nid, string nip, IPAddressInfo.NetPort nFmsPort)
            : base()
        {
            ip = nip; port = nFmsPort;
            IsOk = IPAddress.TryParse(ip, out ipAddress);

            IPAddressInfo.getPortByIndex(port, true, out FmsCmdPort);
            IPAddressInfo.getPortByIndex(port, false, out FmsRealPort);

            SysId = nid;
            RecCmdCount = 0;     //接受到的命令数据
            RecRealCount = 0;    //接受到的实时数据

            SendCmdCount = 0;    //发送的命令数据
            SendRealCount = 0;   //发送的实时数据
        }

        public void ZeroRecCount()
        {
            RecCmdCount = 0;     //接受到的命令数据
            RecRealCount = 0;    //接受到的实时数据

            SendCmdCount = 0;    //发送的命令数据
            SendRealCount = 0;   //发送的实时数据
        }
    }

    public class ConfigValue
    {
        /// <summary>
        /// 执行 返回给教员的登录信息的 时间间隔
        /// </summary>
        public int ReTeachTimeSpan = 500;

        /// <summary>
        /// 取第几个网卡的地址
        /// </summary>
        public int LocIpNum = 0;

        /// <summary>
        /// 监听端口
        /// </summary>
        public int ListerPort = 47115;
        public byte[] ListerPortBytes = new byte[4];
        public IPAddressInfo.NetPort ListerPortName = IPAddressInfo.NetPort.Port_1000;

        /// <summary>
        /// 本机IP信息
        /// </summary>
        public IPAddress LocIpAddress = null;
        public byte[] LocIpAddressBytes = new byte[4];

        /// <summary>
        /// 教员系统IP地址
        /// </summary>
        public IPAddressInfo TeachIpInfo = new IPAddressInfo("127.0.0.1", IPAddressInfo.NetPort.Port_1000, false);

        /// <summary>
        /// CPU系统IP地址
        /// </summary>
        public IPAddressInfo CpuIpInfo = new IPAddressInfo("127.0.0.1", IPAddressInfo.NetPort.Port_2000, false);

        /// <summary>
        /// Unitiy使用的系统编号
        /// </summary>
        public int UnitiyNum = 4;
    }


    /// <summary>
    /// 测试在unitiy中的线程及事件使用
    /// </summary>
    public class ThreadAndEvent
    {
        public bool IsDebug = false;

        [field: CLSCompliant(false)]
        protected Timer _mainTimer;

        [field: CLSCompliant(false)]
        protected UdpClient _netUdpListener;               //udp 监听使用

        [field: CLSCompliant(false)]
        protected UdpClient _netUdpListenerB;              //udp 监听使用

        [field: CLSCompliant(false)]
        protected UdpClient _netGroupListener;             //多播使用的
                

        [field: CLSCompliant(false)]
        protected System.Threading.Thread _netUdpLstThread;
        [field: CLSCompliant(false)]
        protected System.Threading.Thread _netUdpLstThreadB;

        [field: CLSCompliant(false)]
        protected System.Threading.Thread _netGroupLstThread;

        [field: CLSCompliant(false)]
        protected bool _netUdpIsListener = false;

        //private ManualResetEvent stopEvent = new ManualResetEvent(false);

        public delegate void udpMessageReceive(object sender, byte[] bt);
        public event udpMessageReceive OnUdpMessageReceive;
        public void append_udpMessageReceive(object sender, byte[] bt)
        {
            if (OnUdpMessageReceive != null) OnUdpMessageReceive(sender, bt);
        }

        public delegate void udpStatIsStop(object sender);
        public event udpStatIsStop OnUdpStatIsStop;
        public void append_udpStatIsStop(object sender)
        {
            if (OnUdpStatIsStop != null) OnUdpStatIsStop(sender);
        }

        public delegate void udpError(object sender, string nErrMsg);
        public event udpError OnUdpError;
        public void append_udpError(object sender, string nErrMsg)
        {
            if (OnUdpError != null) OnUdpError(sender, nErrMsg);
        }

        [field: CLSCompliant(false)]
        protected int _Port = 12202;

        [field: CLSCompliant(false)]
        protected int _PortB = 12202;

        private void timeProc(object state)
        {
            try
            {
                var t = (Timer)state;
                t.Dispose();

                OnMainTimerAss();

                if (_netUdpIsListener)
                {
                    _mainTimer = new Timer(timeProc);
                    _mainTimer.Change(2100, 0);
                }
            }
            catch (Exception ex)
            {
                if (IsDebug) MessageBox.Show(ex.Message);
            }
        }


        protected virtual void OnMainTimerAss()
        {

        }

        public void start()
        {
            try
            {
                if (null == theLocIp)
                {
                    return;
                }

                //必须绑定到指定地址中
                var ipLocalEndPointA = new IPEndPoint(theLocIp, _Port);
                //_netUdpListener = new System.Net.Sockets.UdpClient(_Port);
                _netUdpListener = new UdpClient(ipLocalEndPointA);
                

                _netUdpLstThread = new System.Threading.Thread(netUdpGetting);

                var ipLocalEndPointB = new IPEndPoint(theLocIp, _PortB);
                //_netUdpListenerB = new UdpClient(_PortB);
                _netUdpListenerB = new UdpClient(ipLocalEndPointB);
                _netUdpLstThreadB = new System.Threading.Thread(netUdpGettingB);
                //_netUdpLstThreadB = new System.Threading.Thread(new System.Threading.ThreadStart(netUdpGetting));
                
                _netUdpIsListener = true;

                _mainTimer = new Timer(timeProc);
                _mainTimer.Change(2000, 0);

                _netUdpLstThread.Start();
                _netUdpLstThreadB.Start();


                //需要让ListenerA 加入组播地址
                //
                var ipLocalEndPointC = new IPEndPoint(theLocIp, 5150);
                _netGroupListener = new UdpClient(ipLocalEndPointC);
                _netGroupLstThread = new System.Threading.Thread(netUdpGettingC);

                var tmpGroupIpStr = "233.7.8.";
                var tmpGroupIsEnable = false;
                for (var tmpGroupIndex = 1; tmpGroupIndex < theGroupEnables.Length; tmpGroupIndex++)
                {
                    if (theGroupEnables[tmpGroupIndex])
                    {
                        _netGroupListener.JoinMulticastGroup(IPAddress.Parse(tmpGroupIpStr + tmpGroupIndex.ToString()));
                        tmpGroupIsEnable = true;
                    }
                }
                //_netGroupListener.JoinMulticastGroup(IPAddress.Parse("233.7.8.1"));
                //_netGroupListener.JoinMulticastGroup(IPAddress.Parse("233.7.8.5"));

                if (tmpGroupIsEnable)
                {
                    _netGroupLstThread.Start();
                }
                
            }
            catch (Exception exMsg)
            {
                //Todo: 为unity屏蔽
                //System.Diagnostics.Debug.WriteLine(exMsg.ToString());
                append_udpError(this, exMsg.Message);

                if (IsDebug) MessageBox.Show(exMsg.Message);
            }
        }

        public void stop()
        {
            if (_mainTimer != null)
            {
                try
                {
                    _mainTimer.Change(30000, 0);
                    _mainTimer.Dispose();
                }
                catch { }
            }

            if (IsDebug) MessageBox.Show("Timer 退出");

            try
            {
                _netUdpLstThread.Abort();
                _netUdpLstThreadB.Abort();
            }
            catch { }

            if (theSendSock != null)
            {
                theSendSock.Close();
                //if (IsDebug) System.Windows.Forms.MessageBox.Show("UDP ReaLData 关闭");
            }

            if (_netUdpListener != null)
            {
                _netUdpListener.Close();
                if (IsDebug) MessageBox.Show("UDP ReaLData 关闭");
            }

            if (_netUdpListenerB != null)
            {
                _netUdpListenerB.Close();
                if (IsDebug) MessageBox.Show("UDP Cmd 关闭");
            }

            if (_netGroupListener != null)
            {
                _netGroupListener.Close();
                if (IsDebug) MessageBox.Show("Group 关闭");
            }

            _netUdpIsListener = false;

        }

        public void netUdpGetting()
        {

            //System.Net.IPAddress ipad = System.Net.IPAddress.Parse(_Port);
            //System.Net.IPAddress ipad2 = System.Net.IPAddress.Parse(_configVar.NetInfo.writeIP2);
            while (_netUdpIsListener)
            {
                System.Threading.Thread.Sleep(5);
                byte[] bt;
                IPEndPoint ipep;
                ipep = new IPEndPoint(IPAddress.Parse("192.168.2.45"), 0);

                try
                {
                    bt = _netUdpListener.Receive(ref ipep);

                    //string tmpS = Encoding.GetEncoding("GBK").GetString(bt);

                    //DeCodeNetData(bt);
                    //在该处进行信息分解
                    _tmpIpInfo = ipep.Address.ToString();
                    if (checkFmsSysFrom(ref _tmpIpInfo))
                    {
                        //如果开启分配表的话 则进行地址过滤及统计
                        FmsSysAddreInfoUpdate(ref _tmpIpInfo);
                    }

                    if (checkUdpIpFrom(ref ipep))
                    {
                        //append_udpMessageReceive(this, bt);
                        DeCodeNetData(ref bt, ref _tmpIpInfo);
                    }
                }
                catch (Exception exMsg)
                {
                    if (IsDebug) MessageBox.Show(exMsg.Message);

                    //Todo: 为unity屏蔽
                    //System.Diagnostics.Debug.WriteLine(exMsg);
                    append_udpError(this, exMsg.Message);
                    _netUdpIsListener = false;
                }
            }

            append_udpStatIsStop(this);
        }

        public void netUdpGettingB()
        {

            //System.Net.IPAddress ipad = System.Net.IPAddress.Parse(_Port);
            //System.Net.IPAddress ipad2 = System.Net.IPAddress.Parse(_configVar.NetInfo.writeIP2);
            while (_netUdpIsListener)
            {
                System.Threading.Thread.Sleep(5);
                byte[] bt;
                IPEndPoint ipep;
                ipep = new IPEndPoint(IPAddress.Parse("192.168.2.45"), 0);

                try
                {
                    bt = _netUdpListenerB.Receive(ref ipep);

                    //DeCodeNetData(bt);
                    //在该处进行信息分解
                    //if (checkUdpIpFrom(ref ipep))
                    //{
                    //    append_udpMessageReceive(this, bt);
                    //    DeCodeNetData(bt);
                    //}

                    //DeCodeNetData(bt);
                    //在该处进行信息分解
                    _tmpIpInfo = ipep.Address.ToString();
                    if (checkFmsSysFrom(ref _tmpIpInfo))
                    {
                        //如果开启分配表的话 则进行地址过滤及统计
                        FmsSysAddreInfoUpdate(ref _tmpIpInfo);
                    }

                    if (checkUdpIpFrom(ref ipep))
                    {
                        //append_udpMessageReceive(this, bt);
                        DeCodeNetData(ref bt, ref _tmpIpInfo);
                    }

                }
                catch (Exception exMsg)
                {
                    if (IsDebug) MessageBox.Show(exMsg.Message);

                    //Todo: 为unity屏蔽
                    //System.Diagnostics.Debug.WriteLine(exMsg);
                    append_udpError(this, exMsg.Message);
                    _netUdpIsListener = false;
                }
            }

            append_udpStatIsStop(this);
        }


        public void netUdpGettingC()
        {

            //System.Net.IPAddress ipad = System.Net.IPAddress.Parse(_Port);
            //System.Net.IPAddress ipad2 = System.Net.IPAddress.Parse(_configVar.NetInfo.writeIP2);
            while (_netUdpIsListener)
            {
                System.Threading.Thread.Sleep(5);
                byte[] bt;
                IPEndPoint ipep;
                ipep = new IPEndPoint(IPAddress.Parse("192.168.2.45"), 0);

                try
                {
                    bt = _netGroupListener.Receive(ref ipep);

                    //DeCodeNetData(bt);
                    //在该处进行信息分解
                    //if (checkUdpIpFrom(ref ipep))
                    //{
                    //    append_udpMessageReceive(this, bt);
                    //    DeCodeNetData(bt);
                    //}

                    //DeCodeNetData(bt);
                    //在该处进行信息分解
                    //_tmpIpInfo = ipep.Address.ToString();
                    //if (checkFmsSysFrom(ref _tmpIpInfo))
                    //{
                    //    //如果开启分配表的话 则进行地址过滤及统计
                    //    FmsSysAddreInfoUpdate(ref _tmpIpInfo);
                    //}

                    if (checkUdpIpFrom(ref ipep))
                    {
                        //append_udpMessageReceive(this, bt);
                        DeCodeNetData(ref bt, ref _tmpIpInfo);
                    }

                }
                catch (Exception exMsg)
                {
                    if (IsDebug) MessageBox.Show(exMsg.Message);

                    //Todo: 为unity屏蔽
                    //System.Diagnostics.Debug.WriteLine(exMsg);
                    append_udpError(this, exMsg.Message);
                    _netUdpIsListener = false;
                }
            }

            append_udpStatIsStop(this);
        }

        protected virtual bool checkUdpIpFrom(ref IPEndPoint ipep)
        {
            return true;
        }

        /// <summary>
        /// 系统地址分解
        /// </summary>
        /// <param name="nip"></param>
        /// <returns></returns>
        private bool checkFmsSysFrom(ref string nip)
        {
            if (!isUseFmsAddreDict) return true;
            return FmsIpToUnitNum.ContainsKey(nip);
        }

        protected virtual void FmsSysAddreInfoUpdate(ref string nip)
        {

        }

        /// <summary>
        /// 解码数据
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        public virtual bool DeCodeNetData(ref byte[] bt, ref string nIpString)
        {
            return true;
        }

        

        public bool sendUdpMessage(string nIP, int nPort, byte[] bt)
        {
            var outBool = false;
            if (bt == null) return outBool;
            if (theSendSock == null) return outBool;
            try
            {
                //Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                var iep2 = new IPEndPoint(IPAddress.Parse(nIP), nPort);

                var data = (byte[])bt.Clone();
                theSendSock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                theSendSock.SendTo(data, iep2);
                //sock.Close();
                outBool = true;
            }
            catch (Exception exMsg)
            {
                append_udpError(this, exMsg.Message);
            }
            return outBool;
        }

        public void sendUdpMessageC(string nIP, int nPort, string nStr)
        {
            if (null == _netUdpListener) return;

            var tmpIEP = new IPEndPoint(IPAddress.Parse(nIP), nPort);
            var tmpByte = Encoding.Unicode.GetBytes(nStr);
            _netUdpListener.Send(tmpByte, tmpByte.Length, tmpIEP);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //地址列表操作函数
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        #region :::::::::   地址列表操作函数    :::::::::::::::::::::::::::::::::::::::::::::::::::::
        public void EnableFmsAddreDict(bool nEnable) { isUseFmsAddreDict = nEnable; }

        /// <summary>
        /// 初始化地址列表
        /// </summary>
        public void InitFmsAddreDict()
        {
            FmsIpToUnitNum.Clear();
            FmsAddreDict.Clear();
        }

        /// <summary>
        /// 重置FMS地址统计数据
        /// </summary>
        public bool ZeroFmsAddreRecDataCount()
        {
            if (isUseFmsAddreDict) return false;

            foreach (var tmpFAI in FmsAddreDict.Values)
            {
                tmpFAI.ZeroRecCount();
            }

            return true;
        }
        /// <summary>
        /// 增加地址对象
        /// </summary>
        /// <param name="nUnitNum"></param>
        /// <param name="nFDI"></param>
        public bool AddFmsAddreDict(int nUnitNum, string nip, IPAddressInfo.NetPort nFmsPort)
        {
            if (isUseFmsAddreDict) return false;
            //增加地址对象
            //如果该系统编号已经存在 则线删除
            if (FmsAddreDict.ContainsKey(nUnitNum))
            {
                if (FmsIpToUnitNum.ContainsKey(FmsAddreDict[nUnitNum].ip))
                {
                    var tmpListA = FmsIpToUnitNum[FmsAddreDict[nUnitNum].ip];
                    tmpListA.Remove(FmsAddreDict[nUnitNum].SysId);
                }
                FmsAddreDict.Remove(nUnitNum);
            }

            //添加该地址信息
            FmsAddreDict.Add(nUnitNum, new FmsAddressInfo(nUnitNum, nip, nFmsPort));
            if (!FmsIpToUnitNum.ContainsKey(nip))
            {
                //若不存在 系统IP则增加
                FmsIpToUnitNum.Add(nip, new List<int>(20));
            }
            var tmpList = FmsIpToUnitNum[nip];
            if (!tmpList.Contains(nUnitNum))
            {
                //若不存在 系统编号则增加
                tmpList.Add(nUnitNum);         
            }

            if (nUnitNum < theSysUnitNumIP.Length)
            {
                theSysUnitNumIP[nUnitNum] = nip;
            }

            return true;
        }

        

        #endregion

        #region ::::::::::::::::::::::::::::::::  variable   :::::::::::::::::::::::::::::::
        protected Socket theSendSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        protected IPAddress theLocIp = null;    //仅用于记录本地IP使用

        string _tmpIpInfo = string.Empty;       //在地址转换时为避免反复使用而创建
        //系统IP表
        //protected System.Collections.Generic.SortedDictionary<string, NetAddressInfo> theSysUnitList = new SortedDictionary<string, NetAddressInfo>();
        protected bool isUseFmsAddreDict = false;                   //是否启用该地址列表
        protected string[] theSysUnitNumIP = new string[300];       //初始支持200个系统编号
        protected Dictionary<int, FmsAddressInfo> FmsAddreDict = new Dictionary<int, FmsAddressInfo>();
        protected Dictionary<string, List<int>> FmsIpToUnitNum = new Dictionary<string, List<int>>();

        protected int theGatherIndex = 342;
        public int GatherIndex { get { return theGatherIndex; } set { theGatherIndex = value; } }

        protected bool[] theGroupEnables = new bool[10];
        public bool[] GroupEnables { get { return theGroupEnables; } set { theGroupEnables = value; } }
        
        #endregion
    }
}

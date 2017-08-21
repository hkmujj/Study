using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Net;
using System.Windows.Forms;
using ES.Facility.PublicModule.IO;
using ES.Facility.PublicModule.TypeTransition;
using ES.Facility.PublicModule.Win32;

namespace ES.Facility.PublicModule.Test
{
    unsafe public class FMSNetHelper : ThreadAndEvent
    {
        #region ::::::::::::::::::::::::::::::::  function   :::::::::::::::::::::::::::::::

        #region ::::::::::::::  配置文件信息设置

        /// <summary>
        /// 设置配置文件所在的目录
        /// </summary>
        /// <param name="cDir"></param>
        public void setConfigDir(string cDir)
        {
            ConfigFullPath = Path.GetFullPath(cDir + "//" + ConfigFileName);
        }
        
        protected virtual void initConfigFile(string cFullPath)
        {
            var ih = new IniHelper(cFullPath);

            ih.Inset(getIniProjectName, getIniNameListerPort, IPAddressInfo.NetPort.Port_1000.ToString());
            ih.Inset(getIniProjectName, getIniNameTeachIP, "127.0.0.1");
            ih.Inset(getIniProjectName, getIniNameTeachPort, IPAddressInfo.NetPort.Port_1000.ToString());
            ih.Inset(getIniProjectName, getIniNameCpuIP, "127.0.0.1");
            ih.Inset(getIniProjectName, getIniNameCpuPort, IPAddressInfo.NetPort.Port_1000.ToString());
            ih.Inset(getIniProjectName, getIniNameLocIpNum, "0");
        }

        protected bool getConfigValueByName(string cName, ref string cOutValue) { return getConfigValueByName(ConfigFullPath, cName, ref cOutValue); }
        protected bool getConfigValueByName(string cFullPath, string cName, ref string cOutValue)
        {
            var ih = new IniHelper(cFullPath);
            
            cOutValue = ih.Select<string>(getIniProjectName, cName);
            return cOutValue == "NULL" ? false : true;
        }

        /// <summary>
        /// 从运行目录中读取配置文件
        /// </summary>
        /// <param name="cExMsg"></param>
        /// <returns></returns>
        public bool loadConfigFromFile(ref string cExMsg)
        {
            setConfigDir(Path.GetDirectoryName(Application.ExecutablePath));
            return loadConfigFromFile(ConfigFullPath, ref cExMsg);
        }

        /// <summary>
        /// 从文件读取配置信息
        /// </summary>
        /// <param name="cPath">完整路径</param>
        /// <returns></returns>
        public bool loadConfigFromFile(string cFullPath, ref string cExMsg)
        {
            ConfigFullPath = cFullPath;

            if (!FileHelper.GetCurFileA(ConfigFullPath))
            {
                initConfigFile(ConfigFullPath);
                cExMsg = "配置文件" + cFullPath + " 不存在，系统为其初始化一个默认配置，请修改";
                return false;
            }

            var tmpIniValue = string.Empty;
            var tmpInt = 0;

            //读取监听端口
            if (!getConfigValueByName(getIniNameListerPort, ref tmpIniValue))
            {
                cExMsg = "未读取到监听端口";
                return false;
            }
            else
            {
                var et = new EnumTransition();
                var tmpNP = et.strToEnum<IPAddressInfo.NetPort>(tmpIniValue);
                if (IPAddressInfo.getPortByIndex(tmpNP, false, out tmpInt) && et.IsRight)
                {
                    NetConfig.ListerPort = tmpInt;
                    NetConfig.ListerPortName = tmpNP;
                }
                else
                {
                    cExMsg = "监听端口定义不符合(Port_x000)定义";
                    return false;
                }
            }

            //读取教员IP
            if (!getConfigValueByName(getIniNameTeachIP, ref  NetConfig.TeachIpInfo.IpName))
            {
                cExMsg = "未读取到教员IP";
                return false;
            }
            else
            {
                IPAddress tmpIp;
                if (!IPAddress.TryParse(NetConfig.TeachIpInfo.IpName, out tmpIp))
                {
                    cExMsg = "教员IP格式错误";
                    return false;
                }
            }

            //读取主控IP
            if (!getConfigValueByName(getIniNameCpuIP, ref  NetConfig.CpuIpInfo.IpName))
            {
                cExMsg = "未读取到主控IP";
                return false;
            }
            else
            {
                IPAddress tmpIp;
                if (!IPAddress.TryParse(NetConfig.CpuIpInfo.IpName, out tmpIp))
                {
                    cExMsg = "主控IP格式错误";
                    return false;
                }
            }

            //读取教员端口 该端口为命令数据端口
            if (!getConfigValueByName(getIniNameTeachPort, ref tmpIniValue))
            {
                cExMsg = "未读取到教员端口";
                return false;
            }
            else
            {
                var et = new EnumTransition();
                var tmpNP = et.strToEnum<IPAddressInfo.NetPort>(tmpIniValue);
                if (IPAddressInfo.getPortByIndex(tmpNP, true, out tmpInt) && et.IsRight)
                {
                    NetConfig.TeachIpInfo.PortNum = tmpInt;
                }
                else
                {
                    cExMsg = "教员端口定义不符合(Port_x000)定义";
                    return false;
                }
            }

            //读取主控端口 该端口为实时数据端口
            if (!getConfigValueByName(getIniNameCpuPort, ref tmpIniValue))
            {
                cExMsg = "未读取到主控端口";
                return false;
            }
            else
            {
                var et = new EnumTransition();
                var tmpNP = et.strToEnum<IPAddressInfo.NetPort>(tmpIniValue);
                if (IPAddressInfo.getPortByIndex(tmpNP, false, out tmpInt) && et.IsRight)
                {
                    NetConfig.CpuIpInfo.PortNum = tmpInt;
                }
                else
                {
                    cExMsg = "主控端口定义不符合(Port_x000)定义";
                    return false;
                }
            }

            //读取网卡序号
            if (!getConfigValueByName(getIniNameLocIpNum, ref tmpIniValue))
            {
                cExMsg = "未读取到网卡序号";
                return false;
            }
            else
            {
                if (int.TryParse(tmpIniValue, out tmpInt))
                {
                    NetConfig.LocIpNum = tmpInt;
                }
                else
                {
                    cExMsg = "网卡序号是整形数字";
                    return false;
                }
            }

            //读取Unitiy系统编号
            if (!getConfigValueByName(getIniNameUinitiyNum, ref tmpIniValue))
            {
                cExMsg = "未读取到Unitiy系统编号";
                return false;
            }
            else
            {
                if (int.TryParse(tmpIniValue, out tmpInt))
                {
                    if (tmpInt < 0 || tmpInt >= 255)
                    {
                        cExMsg = "Unitiy系统编号取值范围为0~255";
                        return false;
                    }

                    NetConfig.UnitiyNum = tmpInt;
                }
                else
                {
                    cExMsg = "Unitiy系统编号应为整数";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 从文件读取配置信息
        /// </summary>
        /// <param name="pathMustRight">必须检查配置文件路径</param>
        /// <param name="cExMsg"></param>
        /// <returns></returns>
        public bool loadConfigFromFile(bool pathMustRight, ref string cExMsg)
        {
            if (pathMustRight)
            {
                var tmpDir = Path.GetDirectoryName(ConfigFullPath);
                if (!Directory.Exists(tmpDir))
                {
                    cExMsg = "配置文件的目录错误"; return false;
                }

                if (!File.Exists(ConfigFullPath))
                {
                    cExMsg = "配置文件不存在"; return false;
                }
            }

            return loadConfigFromFile(ConfigFullPath, ref cExMsg);
        }

        #endregion

        #region ::::::::::::::  配置数据设置
        public virtual bool initNetConfigDataByFileInfo(ref string cExMsg)
        {
            //设置网卡IP信息
            //IPHostEntry hostInfo = Dns.GetHostByName(Dns.GetHostName());
            var tempIpAddress = Dns.GetHostAddresses(Dns.GetHostName());

            var tmpIpAddress = new List<IPAddress>();
            foreach (var ip in tempIpAddress)
            {
                if (ip.AddressFamily.Equals(AddressFamily.InterNetwork))
                {
                    tmpIpAddress.Add(ip);
                }
            }

            if (tmpIpAddress.Count <= 0)
            {
                cExMsg = "未检查到网卡"; return false;
            }

            if (NetConfig.LocIpNum > (tmpIpAddress.Count - 1))
            {
                cExMsg = "配置文件中设置的网卡序号不存在，单网卡请设置为0"; return false;
            }
            NetConfig.LocIpAddress = tmpIpAddress[NetConfig.LocIpNum];
            NetConfig.LocIpAddress.GetAddressBytes().CopyTo(NetConfig.LocIpAddressBytes, 0);
            
            //将本地IP设置到基对象上
            theLocIp = NetConfig.LocIpAddress;

            //设置本机端口信息
            var tmpPort = 0;
            switch (NetConfig.ListerPortName)
            {
                case IPAddressInfo.NetPort.Port_1000: tmpPort = 1000; break;
                case IPAddressInfo.NetPort.Port_2000: tmpPort = 2000; break;
                case IPAddressInfo.NetPort.Port_3000: tmpPort = 3000; break;
                case IPAddressInfo.NetPort.Port_4000: tmpPort = 4000; break;
                default: tmpPort = 1000; break;
            }
                        
            BitConverter.GetBytes(tmpPort).CopyTo(NetConfig.ListerPortBytes, 0);

            //设置教员地址信息
            NetConfig.TeachIpInfo.SetIpPoint();

            //设置主控地址信息
            NetConfig.CpuIpInfo.SetIpPoint();

            //设置基类的信息
            _Port = NetConfig.ListerPort;
            IPAddressInfo.getPortByIndex(NetConfig.ListerPortName, true, out _PortB);

            //设置基类中的 系统列表
            //theSysUnitList.Clear();
            //:: 特殊系统需要归到指定位置

            return true;
        }

        #endregion

        #region ::::::::::::::  与FMS交互的网络命令

        protected override void OnMainTimerAss()
        {
            sendNetCmdViewLogin();
            
            //sendNetFmsGetStatus();
        }
        

        public virtual void sendNetFmsGetStatus()
        {
            //vNetFmsGetStatus[0] = 5;
            vNetFmsGetStatus[4] = 0;     //BitConverter.GetBytes(0);       //from
            vNetFmsGetStatus[8] = 1;     // BitConverter.GetBytes(3);       //to

            IPAddressInfo.getPortByIndex(IPAddressInfo.NetPort.Port_2000, true, out theTmpSendProt);
            sendUdpMessage("192.168.1.54", theTmpSendProt, vNetFmsGetStatus);

        }

        /// <summary>
        /// 发送实景系统登录命令
        /// </summary>
        public virtual void sendNetCmdViewLogin()
        {
            //设置接收的系统
            vNetCmdLoginData[4] = Convert.ToByte(NetConfig.UnitiyNum);

            //设置本机IP和Port
            vNetCmdLoginData[24] = NetConfig.LocIpAddressBytes[0];
            vNetCmdLoginData[25] = NetConfig.LocIpAddressBytes[1];
            vNetCmdLoginData[26] = NetConfig.LocIpAddressBytes[2];
            vNetCmdLoginData[27] = NetConfig.LocIpAddressBytes[3];

            vNetCmdLoginData[28] = NetConfig.ListerPortBytes[0];
            vNetCmdLoginData[29] = NetConfig.ListerPortBytes[1];

            //发送命令
            sendUdpMessage(NetConfig.TeachIpInfo.IpName, NetConfig.TeachIpInfo.PortNum, vNetCmdLoginData);
        }

        public virtual void sendNetCmdOnLine(int nCheckID)
        {
            //设置
            vNetCmdOnLineData[0] = Convert.ToByte(5);
            vNetCmdOnLineData[4] = Convert.ToByte(NetConfig.UnitiyNum);
                        
            BitConverter.GetBytes(268435459).CopyTo(vNetCmdOnLineData, 24);
            BitConverter.GetBytes(nCheckID + 1).CopyTo(vNetCmdOnLineData, 12);
            //发送命令
            sendUdpMessage(NetConfig.TeachIpInfo.IpName, NetConfig.TeachIpInfo.PortNum, vNetCmdOnLineData);
            //sendUdpMessage(theNetConfig.TeachIpInfo.IpName, theNetConfig.TeachIpInfo.PortNum, vNetEventDataB);

            
            //主控也需要一份
            vNetCmdOnLineData[2] = Convert.ToByte(SceneState);
            vNetCmdOnLineData[4] = Convert.ToByte(NetConfig.UnitiyNum);
            sendUdpMessage(NetConfig.CpuIpInfo.IpName, NetConfig.CpuIpInfo.PortNum, vNetCmdOnLineData);
        }

        public virtual void sendNetEventData(byte[] nBytes)
        {
            if (nBytes == null || nBytes.Length > 1000) return;

            //设置
            BitConverter.GetBytes(1301).CopyTo(vNetEventData, 0);
            nBytes.CopyTo(vNetEventData, 4);

            sendUdpMessage(NetConfig.CpuIpInfo.IpName, NetConfig.CpuIpInfo.PortNum, vNetEventData);
        }

        public virtual void sendNetEventData(ref int[] nInts, ref float[] nFloats, ref byte[] nBytes)
        {
            //测试数据使用
            //for (int i = 0; i < 10; i++)
            //{
            //    nInts[i] = i;
            //    nFloats[i] = Convert.ToSingle(i);
            //    nBytes[i] = Convert.ToByte(i);
            //}

            //设置
            BitConverter.GetBytes(1301).CopyTo(vNetEventData, 0);

            for (theIndex = 0; theIndex < 10; theIndex++)
                BitConverter.GetBytes(nInts[theIndex]).CopyTo(vNetEventData, 4 + 4 * theIndex);

            for (; theIndex < 20; theIndex++)
                BitConverter.GetBytes(nFloats[theIndex - 10]).CopyTo(vNetEventData, 4 + 4 * theIndex);

            nBytes.CopyTo(vNetEventData, 4 + 4 * theIndex);

            sendUdpMessage(NetConfig.CpuIpInfo.IpName, NetConfig.CpuIpInfo.PortNum, vNetEventData);
        }

        //byte[] tmpEventByte = new byte[164];
        public bool ConvertFmsEventData(ref byte[] nDesByte, ref FmsEvent nEvent)
        {

            nDesByte[0] = 160;
            nDesByte[1] = 0;
            nDesByte[2] = 0;
            nDesByte[3] = 0;


            var tmpByteA = BitConverter.GetBytes((int)nEvent.nID);
            var tmpIndex = 0;
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[4 + tmpIndex] = tmpByteA[tmpIndex];
            }

            tmpByteA = BitConverter.GetBytes((byte)nEvent.nType);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[8 + tmpIndex] = tmpByteA[tmpIndex];
            }

            tmpByteA = BitConverter.GetBytes((byte)nEvent.nSubType);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[9 + tmpIndex] = tmpByteA[tmpIndex];
            }

            tmpByteA = BitConverter.GetBytes((byte)nEvent.ucDestination);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[10 + tmpIndex] = tmpByteA[tmpIndex];
            }

            tmpByteA = BitConverter.GetBytes((int)nEvent.lPara1);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[16 + tmpIndex] = tmpByteA[tmpIndex];
            }

            tmpByteA = BitConverter.GetBytes((int)nEvent.lPara2);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[20 + tmpIndex] = tmpByteA[tmpIndex];
            }

            tmpByteA = BitConverter.GetBytes((int)nEvent.lPara3);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[24 + tmpIndex] = tmpByteA[tmpIndex];
            }

            tmpByteA = BitConverter.GetBytes((int)nEvent.lPara4);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[28 + tmpIndex] = tmpByteA[tmpIndex];
            }

            tmpByteA = BitConverter.GetBytes((double)nEvent.dPara1);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[44 + 8 * 0 + tmpIndex] = tmpByteA[tmpIndex];
            }

            tmpByteA = BitConverter.GetBytes((double)nEvent.dPara2);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[44 + 8 * 1 + tmpIndex] = tmpByteA[tmpIndex];
            }

            tmpByteA = BitConverter.GetBytes((double)nEvent.dPara3);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[44 + 8 * 2 + tmpIndex] = tmpByteA[tmpIndex];
            }
            tmpByteA = BitConverter.GetBytes((double)nEvent.dPara4);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[44 + 8 * 3 + tmpIndex] = tmpByteA[tmpIndex];
            }

            tmpByteA = BitConverter.GetBytes((double)nEvent.dPara5);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[44 + 8 * 4 + tmpIndex] = tmpByteA[tmpIndex];
            }

            tmpByteA = BitConverter.GetBytes((double)nEvent.dPara6);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[44 + 8 * 5 + tmpIndex] = tmpByteA[tmpIndex];
            }

            tmpByteA = BitConverter.GetBytes((double)nEvent.dPara7);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[44 + 8 * 6 + tmpIndex] = tmpByteA[tmpIndex];
            }

            tmpByteA = BitConverter.GetBytes((double)nEvent.dPara8);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[44 + 8 * 7 + tmpIndex] = tmpByteA[tmpIndex];
            }

            tmpByteA = BitConverter.GetBytes((double)nEvent.dPara9);
            for (tmpIndex = 0; tmpIndex < tmpByteA.Length; tmpIndex++)
            {
                nDesByte[44 + 8 * 8 + tmpIndex] = tmpByteA[tmpIndex];
            }

            return true;

            //fixed (byte* pbyte = tmpEventByte)
            //{
            //    System.IntPtr tmpPtr = (System.IntPtr)pbyte;
            //    System.Runtime.InteropServices.Marshal.StructureToPtr(nEvent, tmpPtr, true);
            //}

            //if (nDesByte.Length > 164)
            //{
            //    Array.Clear(nDesByte, 0, nDesByte.Length);
            //    nDesByte[0] = 0xdd;
            //    nDesByte[1] = 0x5;

            //    Array.Copy(tmpEventByte, 0, nDesByte, 4, tmpEventByte.Length);
            //    return true;
            //}

            //return false;
        }
        #endregion


        #region ::::::::::::::: FMS相关对象控制使用
        /// <summary>
        /// 初始化 实时数据区
        /// </summary>
        public void ZeroFmsRealData()
        {
            Array.Clear(vNetFmsRealFloats, 0, vNetFmsRealFloats.Length);
            Array.Clear(vNetFmsRealByteBools, 0, vNetFmsRealByteBools.Length);
            Array.Clear(vNetFmsRealOldByteBools, 0, vNetFmsRealOldByteBools.Length);
        }
        #endregion

        #region ::::::::::::::  基类的网络命令
        protected override bool checkUdpIpFrom(ref IPEndPoint ipep)
        {
            var tempStr = ipep.ToString();
            tempStr = ipep.Address.ToString();
            tempStr = ipep.Port.ToString();

            return true;
            //如果信息未来至指定的系统 则不处理
            return (ipep.Address.Equals(NetConfig.TeachIpInfo.IpPointOnlyIp.Address) || ipep.Address.Equals(NetConfig.CpuIpInfo.IpPointOnlyIp.Address));
        }


        /// <summary>
        /// 按FMS的定义进行数据分解
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        public override bool DeCodeNetData(ref byte[] bt, ref string nIpString)
        {
            //vNetCmdId = BitConverter.ToInt32(bt, 0);
            vNetCmdId = BitConverter.ToInt16(bt, 0);
            switch (vNetCmdId)
            {
                case -800:
                    {
                        //多播数据
                        vEsNetInfo.assistInt1 = BitConverter.ToInt32(bt, 4 * 7);
                        if (vEsNetInfo.assistInt1 != theGatherIndex) break;     //当数据域不相同则退

                        vEsNetInfo.verId = BitConverter.ToInt32(bt, 4*0);
                        vEsNetInfo.cmdType = BitConverter.ToInt32(bt, 4*1);
                        vEsNetInfo.dataType = BitConverter.ToInt32(bt, 4*2);
                        vEsNetInfo.paraInt1 = BitConverter.ToInt32(bt, 4*3);
                        vEsNetInfo.paraInt2 = BitConverter.ToInt32(bt, 4*4);
                        vEsNetInfo.paraInt3 = BitConverter.ToInt32(bt, 4*5);
                        vEsNetInfo.paraInt4 = BitConverter.ToInt32(bt, 4*6);

                        //vEsNetInfo.assistInt1 = BitConverter.ToInt32(bt, 4*7);


                        fixed (char* pByte = vEsNetInfo.paraChar1)
                        {
                            var tmpPtr = (IntPtr)pByte;
                            Marshal.Copy(bt, 32, tmpPtr, 16);
                        }

                        fixed (char* pByte = vEsNetInfo.paraChar2)
                        {
                            var tmpPtr = (IntPtr)pByte;
                            Marshal.Copy(bt, 32 + 16, tmpPtr, 16);
                        }

                        vEsNetInfo.index = BitConverter.ToInt32(bt, 32+ 16 + 16);
                        vEsNetInfo.endID = BitConverter.ToInt32(bt, 32 + 16 + 16 + 4);

                        fixed (char* pByte = vEsNetInfo.paraLChar1)
                        {
                            var tmpPtr = (IntPtr)pByte;
                            Marshal.Copy(bt, 72, tmpPtr, 800);
                        }

                        appendNetEsDataRecc(ref vEsNetInfo, ref nIpString);
                        appendNetEsDataRecB(ref vEsNetInfo, ref nIpString, ref bt, 72);
                        break;
                    }
                case 800:
                    {
                        //android系统的相关数据包
                        deCodeAndroid(ref bt);

                        break;
                    }
                case 0:
                    {
                        //日志信息
                        break;
                    }
                case 1:
                    {
                        //子系统登陆
                        break;
                    }
                case 2:
                    {
                        //#define CMD_SYS_LOGOFF		2		//子系注销(整个系统退出)
                        break;
                    }
                case 3:
                    {
                        //地址更新 16/232
                        break;
                    }
                case 5:
                    {
                        //客户端应答
                        //int tempInt = BitConverter.ToInt32(bt, 24);
                        break;
                    }
                case 4:
                    {
                        //查询系统状态
                        //教员发来的 12/增加的序号 总24
                        //sendNetCmdOnLine(BitConverter.ToInt32(bt, 12));

                        //返回一个查询事件供网络使用者处理
                        appendFmsNetSysRespones(BitConverter.ToInt32(bt, 4),    //from
                                                BitConverter.ToInt32(bt, 8),    //to
                                                BitConverter.ToInt32(bt, 12));  //para
                        break;
                    }
                case 8:
                    {
                        //#define CMD_SYS_STOP		8		//停止系统(系统本身存在)
                        break;
                    }
                case 11:
                    {
                        //#define CMD_SYS_REPLY		11		//回复命令，有些命令在发出后，需要验证对方收到，通过这个命令达到目的
                        //string tempStr20 = BitConverter.ToString(bt);
                        break;
                    }
                case 1510:
                    {
                        //const long RDATA_TO_PUB = 1510;     //向所有系统广播的实时数据,
                        //数据定义参见cRealDataToPub
                        //FMS实时数据
                        if (!(bt.Length >= 1004)) break;

                        for (theReadBtIndex = 0; theReadBtIndex < 51; theReadBtIndex++)
                        {
                            vNetFmsRealFloats[theReadBtIndex] = BitConverter.ToSingle(bt, 4 + 4 * theReadBtIndex);
                        }

                        Array.Copy(bt, 200 * 4 + 4, vNetFmsRealByteBools, 0, 200);

                        appendFmsRealFloatReceiveB(ref vNetFmsRealFloats, ref vNetFmsRealByteBools);

                        break;
                    }
                case 20:
                    {
                        //#define CMD_STU_INFO		20
                        //string tempStr20 = BitConverter.ToString(bt);
                        break;
                    }
                case 106:
                    {
                        //const long COURSE_TO_IO = 106;	//课程数据：从主控到IO
                        //主要为各类车辆状态
                        //string tempStr106 = BitConverter.ToString(bt);
                        break;
                    }
                case 200:
                    {
                        //教员发送训练标识信息到相关系统
                        //#define CMD_TRAIN_INFO		200		
                        //string tempStr200 = BitConverter.ToString(bt);
                        break;
                    }
                case 201:
                    {
                        ////准备课程	,带有课程头信息,作为课程开始准备命令,每接收到此命令,将生成一个课程,并读取相应的基础信息
                        //#define CMD_PRE_CS			201		
                        //string tempStr201 = BitConverter.ToString(bt);
                        break;
                    }
                case 202:
                    {
                        //准备进路信息,带有进路信息,此信息将被添加到课程中
                        //#define CMD_PRE_ROUTE		202		
                        //string tempStr202 = BitConverter.ToString(bt);
                        break;
                    }
                case 203:
                    {
                        //准备编组,带有编组信息,此信息将被添加到课程中
                        //#define CMD_PRE_GROUP		203		
                        //string tempStr203 = BitConverter.ToString(bt);
                        break;
                    }

                case 210:
                    {
                        //#define CMD_PRE_READY		210		
                        //教员系统使用(由主控系统发出) 或则主控使用(有其他子系统发出)，表示相应的系统数据准备完成
                        //string tempStr210 = BitConverter.ToString(bt);
                        break;
                    }
                case 221:
                    {
                        //#define CMD_DATA_EXIST		221		//(主控)子系统在接收到课程标识信息后，检测数据库，确定基础数据是否存在，只有在所有需要的数据都存在的情况下，才向教员系统发送此命令。
						//教员系统接收到此命令后在发送课程的后续数据(进路，编组)
                        //string tempStr221 = BitConverter.ToString(bt);

                        break;
                    }
                case 222:
                    {
                        //string tempStr222 = BitConverter.ToString(bt);
                        break;
                    }
                case 1103:
                    {
                        //开始训练
                        appendFmsSysBegin(this);

                        SceneDataCount = 0;
                        EventCount = 0;
                        break;
                    }
                case 1105:
                    {
                        //暂停训练
                        break;
                    }
                case 1109:
                    {
                        //结束训练
                        appendFmsSysEnd(this);
                        break;
                    }
                case 196508:
                    {
                        //要求关机
                        ServerRoot.PowerOff();
                        break;
                    }
                case 130972:
                    {
                        //要求重启系统
                        ServerRoot.Reboot();
                        break;
                    }
                case 1505:
                case 1506:
                    { 
                        //MMI需要处理的实时数据
                        appendFmsMMIRealDataReceive(ref bt, vNetCmdId);
                        break;
                    }

                case 1508:
                    {
                        //CIR需要的调度内容命令
                        appendFmsCirCmdReceive(ref bt);
                        break;
                    }
                case 101:
                    {
                        //课程其他数据
                        theCourseHour = BitConverter.ToInt32(bt, 824);
                        theCourseIsDVD = BitConverter.ToInt32(bt, 828); ;
                        theCourseDVDSegNo = BitConverter.ToInt32(bt, 816); ;
                        theCourseFirstOffset = BitConverter.ToSingle(bt, 820); ;

                        appendFmsCourseDataReceive(theCourseHour, theCourseIsDVD, theCourseDVDSegNo, theCourseFirstOffset);

                        //进路数据
                        theReadBtIndex = 12;
                        theReadBtIndex = BitConverter.ToInt32(bt, theReadBtIndex);

                        //fixed (int* pByte = vSceneRount.nRouteSegList)
                        //{
                        //    System.IntPtr tmpPtr = (System.IntPtr)pByte;
                        //    System.Runtime.InteropServices.Marshal.Copy(bt, 12, tmpPtr, 4 * 200);
                        //}
                        //theReadBtIndex = 12 + 4 * 200;
                        vSceneRounts.Clear();

                        for (var i = 0; i < theReadBtIndex; i++)
                        {
                            vSceneRounts.Add(BitConverter.ToInt32(bt, 16 + 4 * i));
                        }

                        appendSceneRountReceive(this, vSceneRounts, theReadBtIndex, bt);
                        break;
                    }
                case 1500:
                    {
                        //const long RDATA_TO_IO = 1500;     //向IO系统发送的实时数据，
                        //数据定义参见cRealDataToIO
                        //string tempStr1500 = BitConverter.ToString(bt);
                        break;
                    }
                case 1501:
                    {
                        if (SceneDataCount >= (int.MaxValue - 10)) SceneDataCount = 0;
                        SceneDataCount++;

                        //实景需要处理的实时数据
                        //从804开始
                        
                        vSceneSpeedDouble  = (BitConverter.ToDouble(bt, 4 + 8 * 1));
                        vSceneSegmentIDDouble  = (BitConverter.ToDouble(bt, 4 + 8 * 2));
                        vSceneDistanceInSegmentDouble  = (BitConverter.ToDouble(bt, 4 + 8 * 3));
                        vSceneTotalDistanceDouble = (BitConverter.ToDouble(bt, 4 + 8 * 4));
                        vSceneDistanceToStopSignDouble = (BitConverter.ToDouble(bt, 4 + 8 * 5));
                        vSceneNowTimeDouble = (BitConverter.ToDouble(bt, 4 + 8 * 6));

                        vSceneSpeed = Convert.ToSingle(vSceneSpeedDouble);
                        vSceneSegmentID = Convert.ToSingle(vSceneSegmentIDDouble);
                        vSceneDistanceInSegment = Convert.ToSingle(vSceneDistanceInSegmentDouble);
                        vSceneTotalDistance = Convert.ToSingle(vSceneTotalDistanceDouble);
                        vSceneDistanceToStopSign = Convert.ToSingle(vSceneDistanceToStopSignDouble);
                        vSceneNowTime = Convert.ToSingle(vSceneNowTimeDouble);

                        if (IsUseEventsQueue)
                        {
                            //使用事件容器时候 为处理方空余出时间
                            if (theFmsEventQueue.Count <= 0)
                                appendSceneDataReceive(this, vSceneSpeed, vSceneSegmentID, vSceneDistanceInSegment, vSceneTotalDistance, vSceneDistanceToStopSign);
                            else
                            {
                                if (theEventsSendedOnce)
                                {
                                    //上次发送事件数据
                                    //忽略此实时数据 发送事件更新事件
                                    appendSceneEventChanged(ref theFmsEventQueue);
                                    theEventsSendedOnce = true;
                                }
                                else
                                {
                                    appendSceneDataReceive(this, vSceneSpeed, vSceneSegmentID, vSceneDistanceInSegment, vSceneTotalDistance, vSceneDistanceToStopSign);
                                    theEventsSendedOnce = false;
                                }
                            }
                        }
                        else
                        {
                            //不使用事件容器模式时 直接调用                                           
                            appendSceneDataReceive(this, vSceneNowTime, vSceneSpeed, vSceneSegmentID, vSceneDistanceInSegment, vSceneTotalDistance);
                            appendSceneDataReceiveD(vSceneNowTime, vSceneSpeedDouble, vSceneSegmentIDDouble, vSceneDistanceInSegmentDouble, vSceneTotalDistanceDouble);
                        }
                        
                        break;
                    }
                case 257:
                    {
                        //主控向教员系统返回自己的状态 szieof 24
                        //const UCHAR STA_SYS_INITING = 0;     //系统初始化中
                        //const UCHAR STA_SYS_NET_OK = 1;     //主控系统网络初始化完毕 
                        //const UCHAR STA_SYS_NET_FAIL = 2;     //主控系统网络失败

                        //const UCHAR STA_SYS_MAIN_READY = 3;     //主控系统就绪,子系统尚未就绪  (无法进入课程初始化)
                        //const UCHAR STA_SYS_ALL_READY = 4;     //主控系统就绪，子系统全部就绪 (可进行课程初始化)
                        //const UCHAR STA_SYS_KEY_SUB_FAIL = 5;     //关键子系统准备失败           (无法进入课程初始化) 
                        //const UCHAR STA_SYS_UKEY_SUB_FAIL = 6;     //非关键子系统故障             (可进入课程初始化) 

                        //const UCHAR STA_SYS_COURSE_INITING = 7;     //主控课程初始化中        
                        //const UCHAR STA_SYS_COURSE_MAIN_FAIL = 8;     //主控系统课程初始化失败， 
                        //const UCHAR STA_SYS_COURSE_MAIN_OK = 9;     //主控系统课程初始化完成，等待子系统课程就绪

                        //const UCHAR STA_SYS_COURSE_KEY_SUB_FAIL = 10;     //有关键子系统课程初始化失败      (无法运行) 
                        //const UCHAR STA_SYS_COURSE_UKEY_SUB_FAIL = 11;     //有非关键子系统课程初始化失败    (允许运行) 
                        //const UCHAR STA_SYS_COURSE_READY = 12;     //主控及全部子系统课程初始化成功  (运行就绪)

                        //const UCHAR STA_SYS_RUNNING = 13;     //系统课程运行中
                        //const UCHAR STA_SYS_RUN_PAUSE = 14;     //系统运行暂停中

                        //const UCHAR STA_SYS_REPLAYING = 15;     //系统回放
                        //const UCHAR STA_SYS_REPLAY_PAUSE = 16;     //系统回放暂停

                        //const UCHAR STA_SYS_PREPARING = 17;     //系统准备中

                        //const UCHAR STA_SYS_RESETTING = 18;     //系统重启中
                        //const UCHAR STA_SYS_SHUTTING_DOWN = 19;     //系统关闭中
                        theTempIntInDeCodeNetData = BitConverter.ToInt32(bt, 12);
                        appendFmsNetSysMainStat(theTempIntInDeCodeNetData);
                        break;
                    }
                case 1100:
                    {
                        //主控呼叫各个子系统
                        if (bt.Length == 32)
                        {
                            //string tempStr1100 = BitConverter.ToString(bt);
                            //byte[] tempBytes = new byte[] { 0x4C, 0x04, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x12, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                            appendFmsNetSysMainCall(BitConverter.ToInt32(bt, 4),    //from
                                                    BitConverter.ToInt32(bt, 8),    //to
                                                    BitConverter.ToInt32(bt, 12));  //para
                            
                        }
                        //IO的输入数据
                        else
                        {
                            var theTmpBitArray = new BitArray(bt);

                            var theTempBoolChangedIndex = 0;
                            var tempBoolIndex = 0;
                            var tempBool = new bool[theTmpBitArray.Count * 8];

                            for (theTempBoolChangedIndex = 0; theTempBoolChangedIndex < theTmpBitArray.Count; theTempBoolChangedIndex++)
                            {
                                tempBool[tempBoolIndex] = theTmpBitArray[theTempBoolChangedIndex];
                                tempBoolIndex++;
                            }
                        }
                        break;
                    }
                case 1101:
                    {
                        if (bt.Length <= 24)
                        {
                            //主控呼叫各个子系统
                            //string tempStr1101 = BitConverter.ToString(bt);
                            appendFmsNetSysMainCall(BitConverter.ToInt32(bt, 4),    //from
                                                    BitConverter.ToInt32(bt, 8),    //to
                                                    BitConverter.ToInt32(bt, 12));  //para
                        }
                        else
                        {
                            //IO的MMI输入数据
                        }
                        break;
                    }
                case 2020:
                    {
                        //操作动作数据
                        break;
                    }
                case 1300:  
                    {
                        //const long  RDATA_FROM_SENCE    =   1300;   
                        //来自主视景的实时数据

                        break;
                    }
                case 160:
                    {
                        EventCount++;

                        //const long EVENT_TRIGGER = 160;	    //事件数据：从主控到所有子系统
                        vSceneEvet.nID = BitConverter.ToInt32(bt, 4);
                        vSceneEvet.nType = bt[8];
                        vSceneEvet.nSubType = bt[9];
                        vSceneEvet.ucDestination = bt[10];

                        vSceneEvet.lPara1 = BitConverter.ToInt32(bt, 16);
                        vSceneEvet.lPara2 = BitConverter.ToInt32(bt, 20);
                        vSceneEvet.lPara3 = BitConverter.ToInt32(bt, 24);
                        vSceneEvet.lPara4 = BitConverter.ToInt32(bt, 28);

                        vSceneEvet.dPara1 = BitConverter.ToDouble(bt, 44);
                        vSceneEvet.dPara2 = BitConverter.ToDouble(bt, 52);

                        if (bt.Length >= 110)
                        {
                            vSceneEvet.dPara3 = BitConverter.ToDouble(bt, 52 + 8 * 1);
                            vSceneEvet.dPara4 = BitConverter.ToDouble(bt, 52 + 8 * 2);
                            vSceneEvet.dPara5 = BitConverter.ToDouble(bt, 52 + 8 * 3);
                            vSceneEvet.dPara6 = BitConverter.ToDouble(bt, 52 + 8 * 4);
                            vSceneEvet.dPara7 = BitConverter.ToDouble(bt, 52 + 8 * 5);
                            vSceneEvet.dPara8 = BitConverter.ToDouble(bt, 52 + 8 * 6);
                            vSceneEvet.dPara9 = BitConverter.ToDouble(bt, 52 + 8 * 7);
                            vSceneEvet.dPara10 = BitConverter.ToDouble(bt, 52 + 8 * 8);
                        }

                        fixed( byte* pByte = vSceneEvet.ucPara1)
                        {
                            
                            var tmpPtr = (IntPtr)pByte;
                            Marshal.Copy(bt, 32, tmpPtr, 8);
                        }

                        //使用事件模式时 不直接通知
                        IsUseEventsQueue = false;
                        if (IsUseEventsQueue)
                            theFmsEventQueue.Enqueue(vSceneEvet);
                        else
                            appendSceneEventReceive(this, vSceneEvet);

                        break;
                    }
                default:
                    {
                        appendExNetDataRec(ref bt, ref nIpString);
                    }
                    return false;
            }

            return true;
        }
        #endregion

        #endregion


        #region ::::::::::::::::::::::::::::::::  android func  ::::::::::::::::::::::::::::
        public virtual void deCodeAndroid(ref byte[] bt)
        {
            theTempIntInDeCodeNetData = BitConverter.ToInt32(bt, 4);

            if (theTempIntInDeCodeNetData == 1)
            {
                //float数组
                theTempIntInDeCodeNetData = BitConverter.ToInt32(bt, 8);

                for (theTempIndexB = 0; theTempIndexB < (theTempIntInDeCodeNetData / 4); theTempIndexB++)
                {
                    vNetFmsRealFloats[theTempIndexB] = BitConverter.ToSingle(bt, 12 + 4 * theTempIndexB);
                }

                if (theTempIntInDeCodeNetData / 4 > 0)
                {
                    appendNetAnroidFloats(ref vNetFmsRealFloats, theTempIntInDeCodeNetData / 4);
                }
            }
        }
        #endregion


        #region ::::::::::::::::::::::::::::::::  test func  :::::::::::::::::::::::::::::::

        public void getRawDataReceive(ref byte[] bt)
        {

            var tmpBmp = new Bitmap(800, 600);
            //System.Drawing.Bitmap tmpBmp = new System.Drawing.Bitmap(GetDeskImage());
            var tmpG = Graphics.FromImage(tmpBmp);
            tmpG.DrawLine(new Pen(new SolidBrush(Color.Blue)), 0, 0, 800, 600);

            var drawString = DateTime.Now.ToString();
            var drawFont = new Font("Arial", 16);
            var drawBrush = new SolidBrush(Color.White);

            var drawPoint = new PointF(150.0F, 150.0F);
            tmpG.DrawString(drawString, drawFont, drawBrush, drawPoint);

            //tmpG.DrawString
            var memStream = new MemoryStream(1024 * 10);
            tmpBmp.Save(memStream, ImageFormat.Jpeg);
            bt = memStream.ToArray();

            tmpG.Dispose();
            tmpBmp.Dispose();
        }

        #endregion

        #region ::::::::::::::::::::::::::::::::  evnet      :::::::::::::::::::::::::::::::

        /// <summary>
        /// 所有未知扩展数据 都是用该接口处理
        /// </summary>
        /// <param name="bt"></param>
        public delegate void ExNetDataRec(ref byte[] bt, ref string nIpString);
        public event ExNetDataRec OnExNetDataRec;
        public void appendExNetDataRec(ref byte[] bt, ref string nIpString)
        {
            if (OnExNetDataRec != null)
                OnExNetDataRec(ref bt, ref nIpString);
        }

        /// <summary>
        /// 根据反编译的代码添加 
        /// 2012-07-11 00:02
        /// 接收教员相应
        /// </summary>
        /// <param name="bt"></param>
        /// <param name="nIpString"></param>
        /// <param name="nCmd"></param>
        public delegate void ExNetFmsRec(ref byte[] bt, ref string nIpString, int nCmd);
        public event ExNetFmsRec OnExNetFmsRec;
        public void appendExNetFmsRec(ref byte[] bt, ref string nIpString, int nCmd)
        {
            if (OnExNetFmsRec != null)
            {
                OnExNetFmsRec(ref bt, ref nIpString, nCmd);
            }
        }

        /// <summary>
        /// 800 版本 的 float
        /// </summary>
        /// <param name="nFloats"></param>
        /// <param name="nLength"></param>
        public delegate void NetAnroidFloats(ref float[] nFloats, int nLength);
        public event NetAnroidFloats OnNetAnroidFloats;
        public void appendNetAnroidFloats(ref float[] nFloats, int nLength)
        {
            if (OnNetAnroidFloats != null)
                OnNetAnroidFloats(ref nFloats, nLength);
        }

        /// <summary>
        /// -800 版本 处理接口
        /// </summary>
        /// <param name="bt"></param>
        public delegate void NetEsDataRec(ref EsNetInfo nEsNetInfo, ref string nIpString);
        public event NetEsDataRec OnNetEsDataRec;
        public void appendNetEsDataRecc(ref EsNetInfo nEsNetInfo, ref string nIpString)
        {
            if (OnNetEsDataRec != null)
                OnNetEsDataRec(ref nEsNetInfo, ref nIpString);
        }

        public delegate void NetEsDataRecB(ref EsNetInfo nEsNetInfo, ref string nIpString, ref byte[] bt, int beginIndex);
        public event NetEsDataRecB OnNetEsDataRecB;
        public void appendNetEsDataRecB(ref EsNetInfo nEsNetInfo, ref string nIpString, ref byte[] bt, int beginIndex)
        {
            if (OnNetEsDataRecB != null)
                OnNetEsDataRecB(ref nEsNetInfo, ref nIpString, ref bt, beginIndex);
        }

        /// <summary>
        /// 全功能发送的实时数据
        /// </summary>
        /// <param name="nFloats"></param>
        public delegate void FmsRealFloatReceive(ref float[] nFloats);
        public event FmsRealFloatReceive OnFmsRealFloatReceive;
        public void appendFmsRealFloatReceive(ref float[] nFloats)
        {
            if (OnFmsRealFloatReceive != null) OnFmsRealFloatReceive(ref nFloats);
        }

        public delegate void FmsRealFloatReceiveB(ref float[] nFloats, ref byte[] nBytes);
        public event FmsRealFloatReceiveB OnFmsRealFloatReceiveB;
        public void appendFmsRealFloatReceiveB(ref float[] nFloats, ref byte[] nBytes)
        {
            if (OnFmsRealFloatReceiveB != null) OnFmsRealFloatReceiveB(ref nFloats, ref nBytes);
        }

        public delegate void FmsSceneDataReceive(object sender, float fSpeed, float fSegmentID, float fDistanceInSegment, float fTotalDistance, float fDistanceToStopSign);
        public event FmsSceneDataReceive OnSceneDataReceive;
        public void appendSceneDataReceive(object sender, float fSpeed, float fSegmentID, float fDistanceInSegment, float fTotalDistance, float fDistanceToStopSign)
        {
            if (OnSceneDataReceive != null) OnSceneDataReceive(sender, fSpeed, fSegmentID, fDistanceInSegment, fTotalDistance, fDistanceToStopSign);
        }

        //                   距离训练时间
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fTime">时间</param>
        /// <param name="fSpeed">速度</param>
        /// <param name="fSegmentID">区段编号</param>
        /// <param name="fDistanceInSegment">区段偏移</param>
        /// <param name="fTotalDistance">累计运行距离</param>
        /// <param name="fDistanceToStopSign">距离当前停车标距离</param>
        public delegate void FmsSceneDataReceiveD(double fNowTime, double fSpeed, double fSegmentID, double fDistanceInSegment, double fTotalDistance);//, double fDistanceToStopSign);
        public event FmsSceneDataReceiveD OnSceneDataReceiveD;
        public void appendSceneDataReceiveD(double fNowTime, double fSpeed, double fSegmentID, double fDistanceInSegment, double fTotalDistance)//, double fDistanceToStopSign)
        {
            if (OnSceneDataReceiveD != null) OnSceneDataReceiveD(fNowTime, fSpeed, fSegmentID, fDistanceInSegment, fTotalDistance);//, fDistanceToStopSign);
        }

        public delegate void FmsSceneEventReceive(object sender, FmsEvent nEvent);
        public event FmsSceneEventReceive OnSceneEventReceive;
        public void appendSceneEventReceive(object sender, FmsEvent nEvent)
        {
            if (OnSceneEventReceive != null) OnSceneEventReceive(sender, nEvent);
        }

        public delegate void FmsSceneEventChanged(ref Queue<FmsEvent> nEvents);
        public event FmsSceneEventChanged OnSceneEventChanged;
        public void appendSceneEventChanged(ref Queue<FmsEvent> nEvents)
        {
            if (OnSceneEventChanged != null) OnSceneEventChanged(ref nEvents);
        }

        public delegate void FmsSceneRountReceive(object sender, List<int> nRounts, int nCount, byte[] nbytes);
        public event FmsSceneRountReceive OnSceneRountReceive;
        public void appendSceneRountReceive(object sender, List<int> nRounts, int nCount, byte[] nbytes)
        {
            if (OnSceneRountReceive != null) OnSceneRountReceive(sender, nRounts, nCount, nbytes);
        }

        public delegate void FmsSysBegin(object sender);
        public event FmsSysBegin OnFmsSysBegin;
        public void appendFmsSysBegin(object sender) { if (OnFmsSysBegin != null) OnFmsSysBegin(sender); }

        public delegate void FmsSysEnd(object sender);
        public event FmsSysEnd OnFmsSysEnd;
        public void appendFmsSysEnd(object sender) { if (OnFmsSysEnd != null) OnFmsSysEnd(sender); }

        public delegate void FmsCourseDataReceive(int nHour, int nIsDvd, int nDVDSegNo, float nFirstOffset);
        public event FmsCourseDataReceive OnFmsCourseDataReceive;
        public void appendFmsCourseDataReceive(int nHour, int nIsDVD, int nDVDSegNo, float nFirstOffset)
        {
            if (OnFmsCourseDataReceive != null) OnFmsCourseDataReceive(nHour, nIsDVD, nDVDSegNo, nFirstOffset);
        }

        public delegate void FmsMMIRealDataReceive(ref byte[] bt, int nDataIndex);
        public event FmsMMIRealDataReceive OnFmsMMIRealDataReceive;
        public void appendFmsMMIRealDataReceive(ref byte[] bt, int nDataIndex)
        {
            if (OnFmsMMIRealDataReceive != null)
                OnFmsMMIRealDataReceive(ref bt, nDataIndex);
        }

        public delegate void FmsCirCmdReceive(ref byte[] bt);
        public event FmsCirCmdReceive OnFmsCirCmdReceive;
        public void appendFmsCirCmdReceive(ref byte[] bt)
        {
            if (OnFmsCirCmdReceive != null)
                OnFmsCirCmdReceive(ref bt);
        }

        //子系统应答教员,
        public delegate void FmsNetSysRespones(int nFrom, int nTo, int nPara);
        public event FmsNetSysRespones OnFmsNetSysRespones;
        public void appendFmsNetSysRespones(int nFrom, int nTo, int nPara)
        {
            if (OnFmsNetSysRespones != null)
                OnFmsNetSysRespones(nFrom, nTo, nPara);
        }

        //主控呼叫子系统,
        public delegate void FmsNetSysMainCall(int nFrom, int nTo, int nPara);
        public event FmsNetSysMainCall OnFmsNetSysMainCall;
        public void appendFmsNetSysMainCall(int nFrom, int nTo, int nPara)
        {
            if (OnFmsNetSysMainCall != null)
                OnFmsNetSysMainCall(nFrom, nTo, nPara);
        }

        //主控返回自身状态
        public delegate void FmsNetSysMainStat(int nStat);
        public event FmsNetSysMainStat OnFmsNetSysMainStat;
        public void appendFmsNetSysMainStat(int nStat)
        {
            if (OnFmsNetSysMainStat != null)
                OnFmsNetSysMainStat(nStat);
        }  

        #endregion

        #region ::::::::::::::::::::::::::::::::  attribute  :::::::::::::::::::::::::::::::

        public ConfigValue NetConfig { get; set; }

        /// <summary> 配置文件的文件名称</summary>
        public string ConfigFileName { get; protected set; }

        /// <summary> 配置文件的完整路径</summary>
        public string ConfigFullPath { get; set; }

        /// <summary> 配置文件的目录</summary>
        public String ConfigDir { get { return Path.GetDirectoryName(ConfigFullPath); } }

        public bool NetIsRuning { get { return _netUdpIsListener; } }

        public int SceneState { get; set; }

        public int EventCount { get; set; }

        public int SceneDataCount { get; set; }


        private int theCourseHour;
        private int theCourseIsDVD;
        private int theCourseDVDSegNo;
        private float theCourseFirstOffset;

        private int theTempIntInDeCodeNetData = 0;      //在解码网络包函数中使用的临时变量
        #region ::::::::::::::  配置文件指定信息
        public string getIniProjectName { get { return "FmsConfig"; } }

        public string getIniNameTeachIP { get { return "TeachIP"; } }
        public string getIniNameTeachPort { get { return "TeachPort"; } }

        public string getIniNameCpuIP { get { return "CpuIP"; } }
        public string getIniNameCpuPort { get { return "CpuPort"; } }

        /// <summary> 本机使用监听端口</summary>
        public string getIniNameListerPort { get { return "ListerPort"; } }

        /// <summary> 本机使用的网卡编号</summary>
        public string getIniNameLocIpNum { get { return "LocIpNum"; } }

        /// <summary> 配置文件的UinitiyNum系统在FMS中的编号</summary>
        public string getIniNameUinitiyNum { get { return "UinitiyNum"; } }        
        #endregion

        #endregion

        #region ::::::::::::::::::::::::::::::::  variable   :::::::::::::::::::::::::::::::
        protected byte[] vNetCmdLoginData = new byte[] { 1, 0, 0, 0, 18, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 168, 0, 0, 208, 07, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        protected byte[] vNetCmdOnLineData = new byte[] { 4, 0, 0, 0, 0, 0, 0, 0, 22, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0 };
        protected byte[] vNetEventData = new byte[1024];
        protected byte[] vNetEventDataB = new byte[] { 5, 0, 0, 0, 18, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 168, 0, 0, 208, 07, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        //模拟地址列表
        //protected byte[] vNetFmsSysList = new byte[] { 3, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 232, 3, 0, 0, 0, 0, 0, 0, 192, 168, 3, 146, 232, 3, 0, 0, 192, 168, 3, 146, 208, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 168, 3, 146, 232, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        protected byte[] vNetFmsGetStatus = new byte[]
                    //{ 4, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 26, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    { 4, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 26, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        
        protected byte[] vNetFmsBeginCouse = new byte[] { };

        //避免反复创建而使用的对象
        int theIndex = 0;
        protected byte[] theBytes = new byte[40];
        protected int[] theInts = new int[10];
        protected float[] theFloats = new float[20];

        protected float[] vNetFmsRealFloats = new float[100];
        protected byte[] vNetFmsRealByteBools = new byte[200];

        protected bool theFmsRealByteBoolsIsChanged = false;
        
        protected byte[] vNetFmsRealOldByteBools = new byte[200];       //用于历史比较时使用

        int theTempIndexB = 0;

        private int theTmpSendProt = 0; //往FMS发送数据时使用，主要用于GetPortByIndex函数的返回用;

        protected int vNetCmdId = 0;    //分解网络数据包时使用

        private float vSceneSpeed = 0.0f;
        private float vSceneSegmentID = 0.0f;
        private float vSceneDistanceInSegment = 0.0f;
        private float vSceneTotalDistance = 0.0f;
        private float vSceneDistanceToStopSign = 0.0f;
        private float vSceneNowTime = 0.0f;

        private double vSceneSpeedDouble = 0.0;
        private double vSceneSegmentIDDouble = 0.0;
        private double vSceneDistanceInSegmentDouble = 0.0;
        private double vSceneTotalDistanceDouble = 0.0;
        private double vSceneDistanceToStopSignDouble = 0.0;
        private double vSceneNowTimeDouble = 0.0;

        private int theReadBtIndex = 0;     //临时记录约定bt index 用 避免反复创建

        private FmsEvent vSceneEvet;        //需要反复使用的主控发送的事件信息
        private EsNetInfo vEsNetInfo;       //需要反复使用的主控发送的事件信息
        //protected FmsRountInfo vSceneRount;   //需要反复使用的主控发送的进路数据
        protected List<int> vSceneRounts = new List<int>();

        private bool theEventsSendedOnce = false;       //记录上次发送的是否是事件数据
        public Queue<FmsEvent> theFmsEventQueue = new Queue<FmsEvent>(40);               //事件缓存

        /// <summary>
        ///是否使用队列来进行事件通知
        /// </summary>
        public bool IsUseEventsQueue { get; set; }


        public int NetRealDataCount = 0;        //网络实时数据计数

        public FMSNetHelper()
        {
            IsUseEventsQueue = true;
            SceneDataCount = 0;
            EventCount = 0;
            SceneState = 0;
            ConfigFullPath = string.Empty;
            ConfigFileName = "config.ini";
            NetConfig = new ConfigValue();
        }

        #endregion
    }    
}

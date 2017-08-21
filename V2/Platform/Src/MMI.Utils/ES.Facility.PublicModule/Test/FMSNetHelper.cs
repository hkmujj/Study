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

        #region ::::::::::::::  �����ļ���Ϣ����

        /// <summary>
        /// ���������ļ����ڵ�Ŀ¼
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
        /// ������Ŀ¼�ж�ȡ�����ļ�
        /// </summary>
        /// <param name="cExMsg"></param>
        /// <returns></returns>
        public bool loadConfigFromFile(ref string cExMsg)
        {
            setConfigDir(Path.GetDirectoryName(Application.ExecutablePath));
            return loadConfigFromFile(ConfigFullPath, ref cExMsg);
        }

        /// <summary>
        /// ���ļ���ȡ������Ϣ
        /// </summary>
        /// <param name="cPath">����·��</param>
        /// <returns></returns>
        public bool loadConfigFromFile(string cFullPath, ref string cExMsg)
        {
            ConfigFullPath = cFullPath;

            if (!FileHelper.GetCurFileA(ConfigFullPath))
            {
                initConfigFile(ConfigFullPath);
                cExMsg = "�����ļ�" + cFullPath + " �����ڣ�ϵͳΪ���ʼ��һ��Ĭ�����ã����޸�";
                return false;
            }

            var tmpIniValue = string.Empty;
            var tmpInt = 0;

            //��ȡ�����˿�
            if (!getConfigValueByName(getIniNameListerPort, ref tmpIniValue))
            {
                cExMsg = "δ��ȡ�������˿�";
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
                    cExMsg = "�����˿ڶ��岻����(Port_x000)����";
                    return false;
                }
            }

            //��ȡ��ԱIP
            if (!getConfigValueByName(getIniNameTeachIP, ref  NetConfig.TeachIpInfo.IpName))
            {
                cExMsg = "δ��ȡ����ԱIP";
                return false;
            }
            else
            {
                IPAddress tmpIp;
                if (!IPAddress.TryParse(NetConfig.TeachIpInfo.IpName, out tmpIp))
                {
                    cExMsg = "��ԱIP��ʽ����";
                    return false;
                }
            }

            //��ȡ����IP
            if (!getConfigValueByName(getIniNameCpuIP, ref  NetConfig.CpuIpInfo.IpName))
            {
                cExMsg = "δ��ȡ������IP";
                return false;
            }
            else
            {
                IPAddress tmpIp;
                if (!IPAddress.TryParse(NetConfig.CpuIpInfo.IpName, out tmpIp))
                {
                    cExMsg = "����IP��ʽ����";
                    return false;
                }
            }

            //��ȡ��Ա�˿� �ö˿�Ϊ�������ݶ˿�
            if (!getConfigValueByName(getIniNameTeachPort, ref tmpIniValue))
            {
                cExMsg = "δ��ȡ����Ա�˿�";
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
                    cExMsg = "��Ա�˿ڶ��岻����(Port_x000)����";
                    return false;
                }
            }

            //��ȡ���ض˿� �ö˿�Ϊʵʱ���ݶ˿�
            if (!getConfigValueByName(getIniNameCpuPort, ref tmpIniValue))
            {
                cExMsg = "δ��ȡ�����ض˿�";
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
                    cExMsg = "���ض˿ڶ��岻����(Port_x000)����";
                    return false;
                }
            }

            //��ȡ�������
            if (!getConfigValueByName(getIniNameLocIpNum, ref tmpIniValue))
            {
                cExMsg = "δ��ȡ���������";
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
                    cExMsg = "�����������������";
                    return false;
                }
            }

            //��ȡUnitiyϵͳ���
            if (!getConfigValueByName(getIniNameUinitiyNum, ref tmpIniValue))
            {
                cExMsg = "δ��ȡ��Unitiyϵͳ���";
                return false;
            }
            else
            {
                if (int.TryParse(tmpIniValue, out tmpInt))
                {
                    if (tmpInt < 0 || tmpInt >= 255)
                    {
                        cExMsg = "Unitiyϵͳ���ȡֵ��ΧΪ0~255";
                        return false;
                    }

                    NetConfig.UnitiyNum = tmpInt;
                }
                else
                {
                    cExMsg = "Unitiyϵͳ���ӦΪ����";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ���ļ���ȡ������Ϣ
        /// </summary>
        /// <param name="pathMustRight">�����������ļ�·��</param>
        /// <param name="cExMsg"></param>
        /// <returns></returns>
        public bool loadConfigFromFile(bool pathMustRight, ref string cExMsg)
        {
            if (pathMustRight)
            {
                var tmpDir = Path.GetDirectoryName(ConfigFullPath);
                if (!Directory.Exists(tmpDir))
                {
                    cExMsg = "�����ļ���Ŀ¼����"; return false;
                }

                if (!File.Exists(ConfigFullPath))
                {
                    cExMsg = "�����ļ�������"; return false;
                }
            }

            return loadConfigFromFile(ConfigFullPath, ref cExMsg);
        }

        #endregion

        #region ::::::::::::::  ������������
        public virtual bool initNetConfigDataByFileInfo(ref string cExMsg)
        {
            //��������IP��Ϣ
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
                cExMsg = "δ��鵽����"; return false;
            }

            if (NetConfig.LocIpNum > (tmpIpAddress.Count - 1))
            {
                cExMsg = "�����ļ������õ�������Ų����ڣ�������������Ϊ0"; return false;
            }
            NetConfig.LocIpAddress = tmpIpAddress[NetConfig.LocIpNum];
            NetConfig.LocIpAddress.GetAddressBytes().CopyTo(NetConfig.LocIpAddressBytes, 0);
            
            //������IP���õ���������
            theLocIp = NetConfig.LocIpAddress;

            //���ñ����˿���Ϣ
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

            //���ý�Ա��ַ��Ϣ
            NetConfig.TeachIpInfo.SetIpPoint();

            //�������ص�ַ��Ϣ
            NetConfig.CpuIpInfo.SetIpPoint();

            //���û������Ϣ
            _Port = NetConfig.ListerPort;
            IPAddressInfo.getPortByIndex(NetConfig.ListerPortName, true, out _PortB);

            //���û����е� ϵͳ�б�
            //theSysUnitList.Clear();
            //:: ����ϵͳ��Ҫ�鵽ָ��λ��

            return true;
        }

        #endregion

        #region ::::::::::::::  ��FMS��������������

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
        /// ����ʵ��ϵͳ��¼����
        /// </summary>
        public virtual void sendNetCmdViewLogin()
        {
            //���ý��յ�ϵͳ
            vNetCmdLoginData[4] = Convert.ToByte(NetConfig.UnitiyNum);

            //���ñ���IP��Port
            vNetCmdLoginData[24] = NetConfig.LocIpAddressBytes[0];
            vNetCmdLoginData[25] = NetConfig.LocIpAddressBytes[1];
            vNetCmdLoginData[26] = NetConfig.LocIpAddressBytes[2];
            vNetCmdLoginData[27] = NetConfig.LocIpAddressBytes[3];

            vNetCmdLoginData[28] = NetConfig.ListerPortBytes[0];
            vNetCmdLoginData[29] = NetConfig.ListerPortBytes[1];

            //��������
            sendUdpMessage(NetConfig.TeachIpInfo.IpName, NetConfig.TeachIpInfo.PortNum, vNetCmdLoginData);
        }

        public virtual void sendNetCmdOnLine(int nCheckID)
        {
            //����
            vNetCmdOnLineData[0] = Convert.ToByte(5);
            vNetCmdOnLineData[4] = Convert.ToByte(NetConfig.UnitiyNum);
                        
            BitConverter.GetBytes(268435459).CopyTo(vNetCmdOnLineData, 24);
            BitConverter.GetBytes(nCheckID + 1).CopyTo(vNetCmdOnLineData, 12);
            //��������
            sendUdpMessage(NetConfig.TeachIpInfo.IpName, NetConfig.TeachIpInfo.PortNum, vNetCmdOnLineData);
            //sendUdpMessage(theNetConfig.TeachIpInfo.IpName, theNetConfig.TeachIpInfo.PortNum, vNetEventDataB);

            
            //����Ҳ��Ҫһ��
            vNetCmdOnLineData[2] = Convert.ToByte(SceneState);
            vNetCmdOnLineData[4] = Convert.ToByte(NetConfig.UnitiyNum);
            sendUdpMessage(NetConfig.CpuIpInfo.IpName, NetConfig.CpuIpInfo.PortNum, vNetCmdOnLineData);
        }

        public virtual void sendNetEventData(byte[] nBytes)
        {
            if (nBytes == null || nBytes.Length > 1000) return;

            //����
            BitConverter.GetBytes(1301).CopyTo(vNetEventData, 0);
            nBytes.CopyTo(vNetEventData, 4);

            sendUdpMessage(NetConfig.CpuIpInfo.IpName, NetConfig.CpuIpInfo.PortNum, vNetEventData);
        }

        public virtual void sendNetEventData(ref int[] nInts, ref float[] nFloats, ref byte[] nBytes)
        {
            //��������ʹ��
            //for (int i = 0; i < 10; i++)
            //{
            //    nInts[i] = i;
            //    nFloats[i] = Convert.ToSingle(i);
            //    nBytes[i] = Convert.ToByte(i);
            //}

            //����
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


        #region ::::::::::::::: FMS��ض������ʹ��
        /// <summary>
        /// ��ʼ�� ʵʱ������
        /// </summary>
        public void ZeroFmsRealData()
        {
            Array.Clear(vNetFmsRealFloats, 0, vNetFmsRealFloats.Length);
            Array.Clear(vNetFmsRealByteBools, 0, vNetFmsRealByteBools.Length);
            Array.Clear(vNetFmsRealOldByteBools, 0, vNetFmsRealOldByteBools.Length);
        }
        #endregion

        #region ::::::::::::::  �������������
        protected override bool checkUdpIpFrom(ref IPEndPoint ipep)
        {
            var tempStr = ipep.ToString();
            tempStr = ipep.Address.ToString();
            tempStr = ipep.Port.ToString();

            return true;
            //�����Ϣδ����ָ����ϵͳ �򲻴���
            return (ipep.Address.Equals(NetConfig.TeachIpInfo.IpPointOnlyIp.Address) || ipep.Address.Equals(NetConfig.CpuIpInfo.IpPointOnlyIp.Address));
        }


        /// <summary>
        /// ��FMS�Ķ���������ݷֽ�
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
                        //�ಥ����
                        vEsNetInfo.assistInt1 = BitConverter.ToInt32(bt, 4 * 7);
                        if (vEsNetInfo.assistInt1 != theGatherIndex) break;     //����������ͬ����

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
                        //androidϵͳ��������ݰ�
                        deCodeAndroid(ref bt);

                        break;
                    }
                case 0:
                    {
                        //��־��Ϣ
                        break;
                    }
                case 1:
                    {
                        //��ϵͳ��½
                        break;
                    }
                case 2:
                    {
                        //#define CMD_SYS_LOGOFF		2		//��ϵע��(����ϵͳ�˳�)
                        break;
                    }
                case 3:
                    {
                        //��ַ���� 16/232
                        break;
                    }
                case 5:
                    {
                        //�ͻ���Ӧ��
                        //int tempInt = BitConverter.ToInt32(bt, 24);
                        break;
                    }
                case 4:
                    {
                        //��ѯϵͳ״̬
                        //��Ա������ 12/���ӵ���� ��24
                        //sendNetCmdOnLine(BitConverter.ToInt32(bt, 12));

                        //����һ����ѯ�¼�������ʹ���ߴ���
                        appendFmsNetSysRespones(BitConverter.ToInt32(bt, 4),    //from
                                                BitConverter.ToInt32(bt, 8),    //to
                                                BitConverter.ToInt32(bt, 12));  //para
                        break;
                    }
                case 8:
                    {
                        //#define CMD_SYS_STOP		8		//ֹͣϵͳ(ϵͳ�������)
                        break;
                    }
                case 11:
                    {
                        //#define CMD_SYS_REPLY		11		//�ظ������Щ�����ڷ�������Ҫ��֤�Է��յ���ͨ���������ﵽĿ��
                        //string tempStr20 = BitConverter.ToString(bt);
                        break;
                    }
                case 1510:
                    {
                        //const long RDATA_TO_PUB = 1510;     //������ϵͳ�㲥��ʵʱ����,
                        //���ݶ���μ�cRealDataToPub
                        //FMSʵʱ����
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
                        //const long COURSE_TO_IO = 106;	//�γ����ݣ������ص�IO
                        //��ҪΪ���೵��״̬
                        //string tempStr106 = BitConverter.ToString(bt);
                        break;
                    }
                case 200:
                    {
                        //��Ա����ѵ����ʶ��Ϣ�����ϵͳ
                        //#define CMD_TRAIN_INFO		200		
                        //string tempStr200 = BitConverter.ToString(bt);
                        break;
                    }
                case 201:
                    {
                        ////׼���γ�	,���пγ�ͷ��Ϣ,��Ϊ�γ̿�ʼ׼������,ÿ���յ�������,������һ���γ�,����ȡ��Ӧ�Ļ�����Ϣ
                        //#define CMD_PRE_CS			201		
                        //string tempStr201 = BitConverter.ToString(bt);
                        break;
                    }
                case 202:
                    {
                        //׼����·��Ϣ,���н�·��Ϣ,����Ϣ������ӵ��γ���
                        //#define CMD_PRE_ROUTE		202		
                        //string tempStr202 = BitConverter.ToString(bt);
                        break;
                    }
                case 203:
                    {
                        //׼������,���б�����Ϣ,����Ϣ������ӵ��γ���
                        //#define CMD_PRE_GROUP		203		
                        //string tempStr203 = BitConverter.ToString(bt);
                        break;
                    }

                case 210:
                    {
                        //#define CMD_PRE_READY		210		
                        //��Աϵͳʹ��(������ϵͳ����) ��������ʹ��(��������ϵͳ����)����ʾ��Ӧ��ϵͳ����׼�����
                        //string tempStr210 = BitConverter.ToString(bt);
                        break;
                    }
                case 221:
                    {
                        //#define CMD_DATA_EXIST		221		//(����)��ϵͳ�ڽ��յ��γ̱�ʶ��Ϣ�󣬼�����ݿ⣬ȷ�����������Ƿ���ڣ�ֻ����������Ҫ�����ݶ����ڵ�����£������Աϵͳ���ʹ����
						//��Աϵͳ���յ���������ڷ��Ϳγ̵ĺ�������(��·������)
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
                        //��ʼѵ��
                        appendFmsSysBegin(this);

                        SceneDataCount = 0;
                        EventCount = 0;
                        break;
                    }
                case 1105:
                    {
                        //��ͣѵ��
                        break;
                    }
                case 1109:
                    {
                        //����ѵ��
                        appendFmsSysEnd(this);
                        break;
                    }
                case 196508:
                    {
                        //Ҫ��ػ�
                        ServerRoot.PowerOff();
                        break;
                    }
                case 130972:
                    {
                        //Ҫ������ϵͳ
                        ServerRoot.Reboot();
                        break;
                    }
                case 1505:
                case 1506:
                    { 
                        //MMI��Ҫ�����ʵʱ����
                        appendFmsMMIRealDataReceive(ref bt, vNetCmdId);
                        break;
                    }

                case 1508:
                    {
                        //CIR��Ҫ�ĵ�����������
                        appendFmsCirCmdReceive(ref bt);
                        break;
                    }
                case 101:
                    {
                        //�γ���������
                        theCourseHour = BitConverter.ToInt32(bt, 824);
                        theCourseIsDVD = BitConverter.ToInt32(bt, 828); ;
                        theCourseDVDSegNo = BitConverter.ToInt32(bt, 816); ;
                        theCourseFirstOffset = BitConverter.ToSingle(bt, 820); ;

                        appendFmsCourseDataReceive(theCourseHour, theCourseIsDVD, theCourseDVDSegNo, theCourseFirstOffset);

                        //��·����
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
                        //const long RDATA_TO_IO = 1500;     //��IOϵͳ���͵�ʵʱ���ݣ�
                        //���ݶ���μ�cRealDataToIO
                        //string tempStr1500 = BitConverter.ToString(bt);
                        break;
                    }
                case 1501:
                    {
                        if (SceneDataCount >= (int.MaxValue - 10)) SceneDataCount = 0;
                        SceneDataCount++;

                        //ʵ����Ҫ�����ʵʱ����
                        //��804��ʼ
                        
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
                            //ʹ���¼�����ʱ�� Ϊ���������ʱ��
                            if (theFmsEventQueue.Count <= 0)
                                appendSceneDataReceive(this, vSceneSpeed, vSceneSegmentID, vSceneDistanceInSegment, vSceneTotalDistance, vSceneDistanceToStopSign);
                            else
                            {
                                if (theEventsSendedOnce)
                                {
                                    //�ϴη����¼�����
                                    //���Դ�ʵʱ���� �����¼������¼�
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
                            //��ʹ���¼�����ģʽʱ ֱ�ӵ���                                           
                            appendSceneDataReceive(this, vSceneNowTime, vSceneSpeed, vSceneSegmentID, vSceneDistanceInSegment, vSceneTotalDistance);
                            appendSceneDataReceiveD(vSceneNowTime, vSceneSpeedDouble, vSceneSegmentIDDouble, vSceneDistanceInSegmentDouble, vSceneTotalDistanceDouble);
                        }
                        
                        break;
                    }
                case 257:
                    {
                        //�������Աϵͳ�����Լ���״̬ szieof 24
                        //const UCHAR STA_SYS_INITING = 0;     //ϵͳ��ʼ����
                        //const UCHAR STA_SYS_NET_OK = 1;     //����ϵͳ�����ʼ����� 
                        //const UCHAR STA_SYS_NET_FAIL = 2;     //����ϵͳ����ʧ��

                        //const UCHAR STA_SYS_MAIN_READY = 3;     //����ϵͳ����,��ϵͳ��δ����  (�޷�����γ̳�ʼ��)
                        //const UCHAR STA_SYS_ALL_READY = 4;     //����ϵͳ��������ϵͳȫ������ (�ɽ��пγ̳�ʼ��)
                        //const UCHAR STA_SYS_KEY_SUB_FAIL = 5;     //�ؼ���ϵͳ׼��ʧ��           (�޷�����γ̳�ʼ��) 
                        //const UCHAR STA_SYS_UKEY_SUB_FAIL = 6;     //�ǹؼ���ϵͳ����             (�ɽ���γ̳�ʼ��) 

                        //const UCHAR STA_SYS_COURSE_INITING = 7;     //���ؿγ̳�ʼ����        
                        //const UCHAR STA_SYS_COURSE_MAIN_FAIL = 8;     //����ϵͳ�γ̳�ʼ��ʧ�ܣ� 
                        //const UCHAR STA_SYS_COURSE_MAIN_OK = 9;     //����ϵͳ�γ̳�ʼ����ɣ��ȴ���ϵͳ�γ̾���

                        //const UCHAR STA_SYS_COURSE_KEY_SUB_FAIL = 10;     //�йؼ���ϵͳ�γ̳�ʼ��ʧ��      (�޷�����) 
                        //const UCHAR STA_SYS_COURSE_UKEY_SUB_FAIL = 11;     //�зǹؼ���ϵͳ�γ̳�ʼ��ʧ��    (��������) 
                        //const UCHAR STA_SYS_COURSE_READY = 12;     //���ؼ�ȫ����ϵͳ�γ̳�ʼ���ɹ�  (���о���)

                        //const UCHAR STA_SYS_RUNNING = 13;     //ϵͳ�γ�������
                        //const UCHAR STA_SYS_RUN_PAUSE = 14;     //ϵͳ������ͣ��

                        //const UCHAR STA_SYS_REPLAYING = 15;     //ϵͳ�ط�
                        //const UCHAR STA_SYS_REPLAY_PAUSE = 16;     //ϵͳ�ط���ͣ

                        //const UCHAR STA_SYS_PREPARING = 17;     //ϵͳ׼����

                        //const UCHAR STA_SYS_RESETTING = 18;     //ϵͳ������
                        //const UCHAR STA_SYS_SHUTTING_DOWN = 19;     //ϵͳ�ر���
                        theTempIntInDeCodeNetData = BitConverter.ToInt32(bt, 12);
                        appendFmsNetSysMainStat(theTempIntInDeCodeNetData);
                        break;
                    }
                case 1100:
                    {
                        //���غ��и�����ϵͳ
                        if (bt.Length == 32)
                        {
                            //string tempStr1100 = BitConverter.ToString(bt);
                            //byte[] tempBytes = new byte[] { 0x4C, 0x04, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x12, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                            appendFmsNetSysMainCall(BitConverter.ToInt32(bt, 4),    //from
                                                    BitConverter.ToInt32(bt, 8),    //to
                                                    BitConverter.ToInt32(bt, 12));  //para
                            
                        }
                        //IO����������
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
                            //���غ��и�����ϵͳ
                            //string tempStr1101 = BitConverter.ToString(bt);
                            appendFmsNetSysMainCall(BitConverter.ToInt32(bt, 4),    //from
                                                    BitConverter.ToInt32(bt, 8),    //to
                                                    BitConverter.ToInt32(bt, 12));  //para
                        }
                        else
                        {
                            //IO��MMI��������
                        }
                        break;
                    }
                case 2020:
                    {
                        //������������
                        break;
                    }
                case 1300:  
                    {
                        //const long  RDATA_FROM_SENCE    =   1300;   
                        //�������Ӿ���ʵʱ����

                        break;
                    }
                case 160:
                    {
                        EventCount++;

                        //const long EVENT_TRIGGER = 160;	    //�¼����ݣ������ص�������ϵͳ
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

                        //ʹ���¼�ģʽʱ ��ֱ��֪ͨ
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
                //float����
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
        /// ����δ֪��չ���� �����øýӿڴ���
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
        /// ���ݷ�����Ĵ������ 
        /// 2012-07-11 00:02
        /// ���ս�Ա��Ӧ
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
        /// 800 �汾 �� float
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
        /// -800 �汾 ����ӿ�
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
        /// ȫ���ܷ��͵�ʵʱ����
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

        //                   ����ѵ��ʱ��
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fTime">ʱ��</param>
        /// <param name="fSpeed">�ٶ�</param>
        /// <param name="fSegmentID">���α��</param>
        /// <param name="fDistanceInSegment">����ƫ��</param>
        /// <param name="fTotalDistance">�ۼ����о���</param>
        /// <param name="fDistanceToStopSign">���뵱ǰͣ�������</param>
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

        //��ϵͳӦ���Ա,
        public delegate void FmsNetSysRespones(int nFrom, int nTo, int nPara);
        public event FmsNetSysRespones OnFmsNetSysRespones;
        public void appendFmsNetSysRespones(int nFrom, int nTo, int nPara)
        {
            if (OnFmsNetSysRespones != null)
                OnFmsNetSysRespones(nFrom, nTo, nPara);
        }

        //���غ�����ϵͳ,
        public delegate void FmsNetSysMainCall(int nFrom, int nTo, int nPara);
        public event FmsNetSysMainCall OnFmsNetSysMainCall;
        public void appendFmsNetSysMainCall(int nFrom, int nTo, int nPara)
        {
            if (OnFmsNetSysMainCall != null)
                OnFmsNetSysMainCall(nFrom, nTo, nPara);
        }

        //���ط�������״̬
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

        /// <summary> �����ļ����ļ�����</summary>
        public string ConfigFileName { get; protected set; }

        /// <summary> �����ļ�������·��</summary>
        public string ConfigFullPath { get; set; }

        /// <summary> �����ļ���Ŀ¼</summary>
        public String ConfigDir { get { return Path.GetDirectoryName(ConfigFullPath); } }

        public bool NetIsRuning { get { return _netUdpIsListener; } }

        public int SceneState { get; set; }

        public int EventCount { get; set; }

        public int SceneDataCount { get; set; }


        private int theCourseHour;
        private int theCourseIsDVD;
        private int theCourseDVDSegNo;
        private float theCourseFirstOffset;

        private int theTempIntInDeCodeNetData = 0;      //�ڽ��������������ʹ�õ���ʱ����
        #region ::::::::::::::  �����ļ�ָ����Ϣ
        public string getIniProjectName { get { return "FmsConfig"; } }

        public string getIniNameTeachIP { get { return "TeachIP"; } }
        public string getIniNameTeachPort { get { return "TeachPort"; } }

        public string getIniNameCpuIP { get { return "CpuIP"; } }
        public string getIniNameCpuPort { get { return "CpuPort"; } }

        /// <summary> ����ʹ�ü����˿�</summary>
        public string getIniNameListerPort { get { return "ListerPort"; } }

        /// <summary> ����ʹ�õ��������</summary>
        public string getIniNameLocIpNum { get { return "LocIpNum"; } }

        /// <summary> �����ļ���UinitiyNumϵͳ��FMS�еı��</summary>
        public string getIniNameUinitiyNum { get { return "UinitiyNum"; } }        
        #endregion

        #endregion

        #region ::::::::::::::::::::::::::::::::  variable   :::::::::::::::::::::::::::::::
        protected byte[] vNetCmdLoginData = new byte[] { 1, 0, 0, 0, 18, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 168, 0, 0, 208, 07, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        protected byte[] vNetCmdOnLineData = new byte[] { 4, 0, 0, 0, 0, 0, 0, 0, 22, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0 };
        protected byte[] vNetEventData = new byte[1024];
        protected byte[] vNetEventDataB = new byte[] { 5, 0, 0, 0, 18, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 168, 0, 0, 208, 07, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        //ģ���ַ�б�
        //protected byte[] vNetFmsSysList = new byte[] { 3, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 232, 3, 0, 0, 0, 0, 0, 0, 192, 168, 3, 146, 232, 3, 0, 0, 192, 168, 3, 146, 208, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 168, 3, 146, 232, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        protected byte[] vNetFmsGetStatus = new byte[]
                    //{ 4, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 26, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    { 4, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 26, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        
        protected byte[] vNetFmsBeginCouse = new byte[] { };

        //���ⷴ��������ʹ�õĶ���
        int theIndex = 0;
        protected byte[] theBytes = new byte[40];
        protected int[] theInts = new int[10];
        protected float[] theFloats = new float[20];

        protected float[] vNetFmsRealFloats = new float[100];
        protected byte[] vNetFmsRealByteBools = new byte[200];

        protected bool theFmsRealByteBoolsIsChanged = false;
        
        protected byte[] vNetFmsRealOldByteBools = new byte[200];       //������ʷ�Ƚ�ʱʹ��

        int theTempIndexB = 0;

        private int theTmpSendProt = 0; //��FMS��������ʱʹ�ã���Ҫ����GetPortByIndex�����ķ�����;

        protected int vNetCmdId = 0;    //�ֽ��������ݰ�ʱʹ��

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

        private int theReadBtIndex = 0;     //��ʱ��¼Լ��bt index �� ���ⷴ������

        private FmsEvent vSceneEvet;        //��Ҫ����ʹ�õ����ط��͵��¼���Ϣ
        private EsNetInfo vEsNetInfo;       //��Ҫ����ʹ�õ����ط��͵��¼���Ϣ
        //protected FmsRountInfo vSceneRount;   //��Ҫ����ʹ�õ����ط��͵Ľ�·����
        protected List<int> vSceneRounts = new List<int>();

        private bool theEventsSendedOnce = false;       //��¼�ϴη��͵��Ƿ����¼�����
        public Queue<FmsEvent> theFmsEventQueue = new Queue<FmsEvent>(40);               //�¼�����

        /// <summary>
        ///�Ƿ�ʹ�ö����������¼�֪ͨ
        /// </summary>
        public bool IsUseEventsQueue { get; set; }


        public int NetRealDataCount = 0;        //����ʵʱ���ݼ���

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

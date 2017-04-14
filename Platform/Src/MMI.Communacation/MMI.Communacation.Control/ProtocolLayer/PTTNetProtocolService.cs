using System;
using System.Net;
using ES.Facility.PublicModule.Win32;
using MMI.Communacation.Control.ProtocolLayer.RecvPackage;
using MMI.Communacation.Interface.AppLayer;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Data.Config;
using Yunda.PTT2D.ViewLayer.Infrastructure.Model;
using Yunda.PTT2D.ViewLayer.Infrastructure.Proxy;

namespace MMI.Communacation.Control.ProtocolLayer
{
    public class PTTNetProtocolService : NetProtocolServiceBase
    {
        private PluginProxy m_PluginProxy;

        private PluginData m_SendDataBuffer;

        private bool m_Startted;

        public override void Initialize(IConfig config)
        {
            m_SendDataBuffer = PluginData.Create(PluginType.PluginType_MMI, PluginType.PluginType_MainNode);
            m_PluginProxy = new PluginProxy(PluginType.PluginType_MMI);
            m_PluginProxy.DataRecvedEvent += PluginProxyOnDataRecvedEvent;
            m_PluginProxy.CommandRecvedEvent += PluginProxyOnCommandRecvedEvent;
            m_PluginProxy.UninitalizeEvent += PluginProxyOnUninitalizeEvent;
            m_PluginProxy.Start();
            m_Startted = true;
        }

        private void PluginProxyOnUninitalizeEvent()
        {
            SysLog.Info("Recv plugin proxy uninitalize evnet, shut down this application.");
            Environment.Exit(0);
        }

        private void PluginProxyOnCommandRecvedEvent(byte[] bytes)
        {

        }

        private void PluginProxyOnDataRecvedEvent(byte[] bytes)
        {
            DecodeNetData(bytes);
        }

        private bool DecodeNetData(byte[] datas)
        {
            int cmdId = BitConverter.ToInt16(datas, 0);
            switch (cmdId)
            {
                case 1103:
                    //开始训练
                    OnBegin(new NetCommandEventArgs());
                    break;
                case 1105:
                    //暂停训练
                    break;
                case 1109:
                    //结束训练
                    OnEnd(new NetCommandEventArgs());
                    break;
                case 196508:
                    //要求关机
                    ServerRoot.PowerOff();
                    break;
                case 130972:
                    //要求重启系统
                    ServerRoot.Reboot();
                    break;

                case (int) RecvDataType.FirstPackage:
                case (int) RecvDataType.SecondPackage:
                case (int) RecvDataType.ThirdPackage:
                    //MMI需要处理的实时数据
                    OnDataReceived(datas, new RecvPackageHead() { Value = cmdId});
                    break;

                default:
                    return false;
            }

            return true;
        }

        public override void Send(string ip, int port, byte[] data)
        {
            if (m_Startted)
            {
                data.CopyTo(m_SendDataBuffer.Data, 0);
                m_SendDataBuffer.DataLenth = (uint)data.Length;
                m_PluginProxy.SendData(m_SendDataBuffer);
            }
        }

        public override void Send(IPEndPoint ipEndPoint, byte[] data)
        {
            if (m_Startted)
            {
                data.CopyTo(m_SendDataBuffer.Data, 0);
                m_SendDataBuffer.DataLenth = (uint) data.Length;
                m_PluginProxy.SendData(m_SendDataBuffer);
            }
        }
    }
}
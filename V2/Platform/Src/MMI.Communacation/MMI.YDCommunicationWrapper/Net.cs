using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MMI.YDCommunicationWrapper
{
    public class Net
    {
        public delegate void CmdProc(int sysid, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1024)] byte[] data);

        public delegate void RealProc(int sysid, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1024)] byte[] data, int len);

        public delegate void McastProc([MarshalAs(UnmanagedType.LPArray, SizeConst = 1024)] byte[] data, int len);

        private CmdProc m_Cp;

        private RealProc m_Rp;

        private McastProc m_Mp;

        [method:MethodImpl(MethodImplOptions.Synchronized)]
        public event CmdProc OnRecvCmdData;

        [method:MethodImpl(MethodImplOptions.Synchronized)]
        public event RealProc OnRecvRealData;

        [method:MethodImpl(MethodImplOptions.Synchronized)]
        public event McastProc OnMcastData;

        public void Ref_RecvCmdData(int sysid, byte[] data)
        {
            if (OnRecvCmdData != null)
            {
                OnRecvCmdData(sysid, data);
            }
        }

        public void Ref_RecvRealData(int sysid, byte[] data, int len)
        {
            if (OnRecvRealData != null)
            {
                OnRecvRealData(sysid, data, len);
            }
        }

        public void Ref_McastData(byte[] data, int len)
        {
            if (OnMcastData != null)
            {
                OnMcastData(data, len);
            }
        }

        

        public void Init()
        {
            m_Cp = OnCmdProc;
            m_Rp = OnRealProc;
            m_Mp = OnMcastProc;
            try
            {
                YDCommunicationNative.InitNetwork(m_Cp, m_Rp, m_Mp);
            }
            catch (Exception)
            {
            }
            Console.WriteLine("Init finished !");
        }

        private void OnCmdProc(int sysid, byte[] data)
        {
            Ref_RecvCmdData(sysid, data);
        }

        private void OnRealProc(int sysid, byte[] data, int len)
        {
            Ref_RecvRealData(sysid, data, len);
        }

        private void OnMcastProc(byte[] data, int len)
        {
            Ref_McastData(data, len);
        }

        public void CloseNetwork()
        {
            YDCommunicationNative.CloseNetwork_();
        }

        public int GetSystemID()
        {
            return YDCommunicationNative.GetSystemID_();
        }

        public int GetSystemStatus(int sysid)
        {
            return YDCommunicationNative.GetSystemStatus_(sysid);
        }

        public void SetSystemStatus(int status)
        {
            YDCommunicationNative.SetSystemStatus_(status);
        }

        public int CreateMcastGrp(string name, string remark)
        {
            return YDCommunicationNative.CreateMcastGrp_(name, remark);
        }

        public bool JoinMcastGroup(int grpID)
        {
            return YDCommunicationNative.JoinMcastGroup_(grpID);
        }

        public bool LeaveMcastGroup(int grpID)
        {
            return YDCommunicationNative.LeaveMcastGroup_(grpID);
        }

        public int SendCmdData(int sysID, byte[] data)
        {
            return YDCommunicationNative.SendCmdData_(sysID, data);
        }

        public int SendRealData(int sysID, byte[] data, int len)
        {
            return YDCommunicationNative.SendRealData_(sysID, data, len);
        }

        public int SendMcastData(int grpID, byte[] data, int len)
        {
            return YDCommunicationNative.MulticastData_(grpID, data, len);
        }
    }
}
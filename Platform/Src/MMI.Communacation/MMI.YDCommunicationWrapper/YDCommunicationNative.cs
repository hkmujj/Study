using System.Runtime.InteropServices;

namespace MMI.YDCommunicationWrapper
{
    internal class YDCommunicationNative
    {
        const string YDCommunicationDllName = "YDCommunication";

        [DllImport(YDCommunicationDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void InitNetwork(Net.CmdProc cmd, Net.RealProc real, Net.McastProc mcast);

        [DllImport(YDCommunicationDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CloseNetwork")]
        public static extern void CloseNetwork_();

        [DllImport(YDCommunicationDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetSystemID")]
        public static extern int GetSystemID_();

        [DllImport(YDCommunicationDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetSystemStatus")]
        public static extern int GetSystemStatus_(int sysid);

        [DllImport(YDCommunicationDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetSystemStatus")]
        public static extern void SetSystemStatus_(int status);

        [DllImport(YDCommunicationDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateMcastGrp")]
        public static extern int CreateMcastGrp_(string name, string remark);

        [DllImport(YDCommunicationDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "JoinMcastGroup")]
        public static extern bool JoinMcastGroup_(int grpID);

        [DllImport(YDCommunicationDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LeaveMcastGroup")]
        public static extern bool LeaveMcastGroup_(int grpID);

        [DllImport(YDCommunicationDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SendCmdData")]
        public static extern int SendCmdData_(int sysID, byte[] data);

        [DllImport(YDCommunicationDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SendRealData")]
        public static extern int SendRealData_(int sysID, byte[] data, int len);

        [DllImport(YDCommunicationDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MulticastData")]
        public static extern int MulticastData_(int sysID, byte[] data, int len);
    }
}
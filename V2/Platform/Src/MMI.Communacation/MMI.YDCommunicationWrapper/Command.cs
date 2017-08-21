using System.Runtime.InteropServices;

namespace MMI.YDCommunicationWrapper
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Command
    {
        public short cmd;

        public short subType;

        public int sFrom;

        public int sTo;

        public int flags;

        public int nParamLen;

        public int reserve1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CommandConstant.MaxParamLength)]
        public byte[] cParam;

        public int GetInfoLen()
        {
            return CommandConstant.CommandHeadLength;
        }

        public int GetParamLen()
        {
            return nParamLen;
        }

        public int GetBufLen()
        {
            return GetInfoLen() + GetParamLen();
        }
    }
}

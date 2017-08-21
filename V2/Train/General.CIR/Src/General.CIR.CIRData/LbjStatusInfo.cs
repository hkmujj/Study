using System;
using System.Runtime.InteropServices;

namespace General.CIR.CIRData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LbjStatusInfo
    {
        public byte alarmStatus;

        public byte workStatus;

        public byte tailStatus;

        public TailInfo tailInfo;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
        public byte[] tailAirPressure;

        public short TailAirPressure
        {
            get
            {
                short num = BitConverter.ToInt16(tailAirPressure, 0);
                short num2 = num;
                var num3 = num2 & 15;
                num2 = num;
                var num4 = (short)(((int)num2 & 61440) >> 12);
                num2 = num;
                var num5 = (short)((num2 & 3840) >> 8);
                return (short)(num3 * 100 + num4 * 10 + num5);
            }
        }
    }
}

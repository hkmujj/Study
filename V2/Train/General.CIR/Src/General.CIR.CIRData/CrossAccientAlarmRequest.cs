using System;
using System.Runtime.InteropServices;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct CrossAccientAlarmRequest
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
		public byte[] kmMark;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
		public byte[] infoSendDateTime;

		public double KMMark
		{
			get
			{
				return (double)(BitConverter.ToInt16(kmMark, 0) / 1000);
			}
		}

		public DateTime InfoSendDateTime
		{
			get
			{
				int num = BitConverter.ToInt32(infoSendDateTime, 0);
				int num2 = num;
				long num3 = ((long)num2 & (long)(-67108864)) >> 26;
				num2 = num;
				long num4 = (long)((num2 & 62914560) >> 22);
				num2 = num;
				long num5 = (long)((num2 & 4063232) >> 16);
				num2 = num;
				long num6 = (long)((num2 & 126976) >> 12);
				num2 = num;
				long num7 = (long)((num2 & 4032) >> 6);
				num2 = num;
				long num8 = (long)(num2 & 63);
				DateTime result = new DateTime((int)num3, (int)num4, (int)num5, (int)num6, (int)num7, (int)num8);
				return result;
			}
		}
	}
}

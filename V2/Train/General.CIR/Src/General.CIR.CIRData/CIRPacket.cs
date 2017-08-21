using System;
using System.Runtime.InteropServices;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct CIRPacket
	{
		public byte Start;

		public byte Version;

		public ushort pack_len;

		public byte Cbsport;

		public byte Cbsaddrlen;

		public byte Cbdport;

		public byte Cbdaddrlen;

		public byte Cbop;

		public byte Cbcmd;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1014, ArraySubType = UnmanagedType.I1)]
		public byte[] Buff;

		public void Init()
		{
			Start = 126;
			Version = 1;
			Cbsaddrlen = 0;
			Cbdaddrlen = 0;
			pack_len = 6;
			byte[] bytes = BitConverter.GetBytes(pack_len);
			byte[] array = new byte[bytes.Length];
			bool flag = bytes.Length >= 2;
			if (flag)
			{
				array[0] = bytes[1];
				array[1] = bytes[0];
			}
			pack_len = BitConverter.ToUInt16(array, 0);
			bool flag2 = Buff == null;
			if (flag2)
			{
				Buff = new byte[1014];
			}
		}

		public void SetHeadInfo(byte srcPort, byte tarPort, byte operation, byte cmdType)
		{
			Cbsport = srcPort;
			Cbdport = tarPort;
			Cbop = operation;
			Cbcmd = cmdType;
		}

		public void SetData(byte[] data)
		{
			bool flag = data == null;
			if (!flag)
			{
				pack_len = (ushort)(6 + data.Length);
				byte[] bytes = BitConverter.GetBytes(pack_len);
				byte[] array = new byte[bytes.Length];
				bool flag2 = bytes.Length >= 2;
				if (flag2)
				{
					array[0] = bytes[1];
					array[1] = bytes[0];
				}
				pack_len = BitConverter.ToUInt16(array, 0);
				data.CopyTo(Buff, 0);
			}
		}

		public int GetPackLen()
		{
			byte[] bytes = BitConverter.GetBytes(pack_len);
			byte[] array = new byte[bytes.Length];
			bool flag = bytes.Length >= 2;
			if (flag)
			{
				array[0] = bytes[1];
				array[1] = bytes[0];
			}
			return (int)BitConverter.ToUInt16(array, 0);
		}

		public int GetDataLen()
		{
			return GetPackLen() + 4;
		}

		public int GetTotalLen()
		{
			return 1024;
		}
	}
}

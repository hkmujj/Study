using System;
using System.Runtime.InteropServices;
using System.Text;

namespace General.CIR.CIRData
{
	public static class CIRCommAgent
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void OnReceive(IntPtr bufferPtr, int nLen);

		[DllImport("CIRComm.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool InitCIR(string ipAddr = "127.0.0.1", ushort uSendPort = 20001, ushort uRecvPort = 20000);

		[DllImport("CIRComm.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetRecvHandler(OnReceive handler);

		[DllImport("CIRComm.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SendCIRData(byte[] data, int uLen, bool bDelayOnSendBefore = false, ushort uDelay = 0);

		[DllImport("CIRComm.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void UnInitCIR();

		[DllImport("CIRComm.dll", CharSet = CharSet.Ansi, EntryPoint = "STR2BCD")]
		public static extern bool Str2BCD(StringBuilder strAscii, StringBuilder strBcd);

		[DllImport("CIRComm.dll", CharSet = CharSet.Ansi, EntryPoint = "INT2BCD")]
		public static extern bool Int2BCD(int data, int dataLen, StringBuilder strBcd);

		[DllImport("CIRComm.dll", CharSet = CharSet.Ansi, EntryPoint = "BCD2STR")]
		public static extern bool BCD2Str(StringBuilder strBcd, int nLen, StringBuilder strAscii);

		[DllImport("CIRComm.dll", CharSet = CharSet.Ansi, EntryPoint = "BCD2INT")]
		public static extern int BCD2Int(byte[] bytes, int nLen);

		public static byte[] StructToBytes(object obj)
		{
			bool flag = obj == null;
			byte[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = Marshal.SizeOf(obj);
				bool flag2 = num <= 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					IntPtr intPtr = Marshal.AllocHGlobal(num);
					bool flag3 = intPtr == IntPtr.Zero;
					if (flag3)
					{
						result = null;
					}
					else
					{
						byte[] array = new byte[num];
						Marshal.StructureToPtr(obj, intPtr, true);
						Marshal.Copy(intPtr, array, 0, array.Length);
						Marshal.FreeHGlobal(intPtr);
						result = array;
					}
				}
			}
			return result;
		}

		public static object PtrToStruct(IntPtr ptr, int nLen, Type type)
		{
			bool flag = ptr == IntPtr.Zero;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = nLen <= 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					object obj = Marshal.PtrToStructure(ptr, type);
					result = obj;
				}
			}
			return result;
		}

		public static object BytesToStruct(byte[] bytes, Type type, int nDataIndex = 0)
		{
			bool flag = bytes == null;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = Marshal.SizeOf(type);
				IntPtr intPtr = Marshal.AllocHGlobal(num);
				bool flag2 = intPtr == IntPtr.Zero;
				if (flag2)
				{
					result = null;
				}
				else
				{
					Marshal.Copy(bytes, nDataIndex, intPtr, num);
					object obj = Marshal.PtrToStructure(intPtr, type);
					Marshal.FreeHGlobal(intPtr);
					result = obj;
				}
			}
			return result;
		}
	}
}

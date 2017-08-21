using System.Runtime.InteropServices;
using System.Text;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct StationInfo
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
		public byte[] name;

		public ushort code;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
		public byte[] phoneNumber;

		public string Name
		{
			get
			{
				string @string = Encoding.Default.GetString(name);
				return @string.TrimEnd(new char[1]);
			}
		}

		public string PhoneNumber
		{
			get
			{
				string @string = Encoding.Default.GetString(phoneNumber);
				return @string.TrimEnd(new char[1]);
			}
		}
	}
}

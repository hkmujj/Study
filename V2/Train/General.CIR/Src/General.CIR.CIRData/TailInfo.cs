using System.Runtime.InteropServices;
using System.Text;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct TailInfo
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
		public byte[] trainID;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
		public byte[] tailID;

		public string TrainID
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(8);
				StringBuilder stringBuilder2 = new StringBuilder(Encoding.Default.GetString(trainID));
				CIRCommAgent.BCD2Str(stringBuilder2, stringBuilder2.Length, stringBuilder);
				return stringBuilder.ToString();
			}
			set
			{
				string text = value;
				bool flag = text.Length > 8;
				if (flag)
				{
					text = text.Remove(8);
				}
				else
				{
					bool flag2 = text.Length < 8;
					if (flag2)
					{
						text = text.PadRight(8);
					}
				}
				StringBuilder strAscii = new StringBuilder(text);
				StringBuilder stringBuilder = new StringBuilder();
				CIRCommAgent.Str2BCD(strAscii, stringBuilder);
				trainID = Encoding.Default.GetBytes(stringBuilder.ToString());
			}
		}

		public string TailID
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(6);
				StringBuilder stringBuilder2 = new StringBuilder(Encoding.Default.GetString(tailID));
				CIRCommAgent.BCD2Str(stringBuilder2, stringBuilder2.Length, stringBuilder);
				return stringBuilder.ToString();
			}
			set
			{
				string text = value;
				bool flag = text.Length > 6;
				if (flag)
				{
					text = text.Remove(6);
				}
				else
				{
					bool flag2 = text.Length < 6;
					if (flag2)
					{
						text = text.PadRight(6);
					}
				}
				StringBuilder strAscii = new StringBuilder(text);
				StringBuilder stringBuilder = new StringBuilder();
				CIRCommAgent.Str2BCD(strAscii, stringBuilder);
				tailID = Encoding.Default.GetBytes(stringBuilder.ToString());
			}
		}
	}
}

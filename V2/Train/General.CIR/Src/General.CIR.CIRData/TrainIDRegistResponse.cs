using System.Runtime.InteropServices;
using System.Text;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct TrainIDRegistResponse
	{
		public byte registStatus;

		public byte FailedReason;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
		public byte[] trainID;

		public byte finishChar;

		public bool IsRegist
		{
			get
			{
				return (registStatus & 64) == 64;
			}
		}

		public bool IsSucceed
		{
			get
			{
				return (registStatus & 128) == 128;
			}
		}

		public string TrainID
		{
			get
			{
				return Encoding.Default.GetString(trainID);
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
				trainID = Encoding.Default.GetBytes(text);
				finishChar = 59;
			}
		}
	}
}

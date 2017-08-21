using System.Runtime.InteropServices;
using System.Text;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct CallRequestInitiativeGSMR
	{
		public byte callType;

		public byte priorityLevel;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
		public byte[] callNumber;

		public byte finishChar;

		public string CallNumber
		{
			get
			{
				return Encoding.Default.GetString(callNumber);
			}
			set
			{
				string text = value;
				bool flag = text.Length > 16;
				if (flag)
				{
					text = text.Remove(16);
				}
				else
				{
					bool flag2 = text.Length < 16;
					if (flag2)
					{
						text = text.PadRight(16);
					}
				}
				callNumber = Encoding.Default.GetBytes(text);
				finishChar = 59;
			}
		}
	}
}

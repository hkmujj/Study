using System.Runtime.InteropServices;
using System.Text;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct LineSingalModeSwitch
	{
		public byte singalMode;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
		public byte[] lineName;

		public byte finishChar;

		public string LineName
		{
			get
			{
				return Encoding.Default.GetString(lineName);
			}
			set
			{
				string text = value;
				bool flag = text.Length > 32;
				if (flag)
				{
					text = text.Remove(32);
				}
				else
				{
					bool flag2 = text.Length < 32;
					if (flag2)
					{
						text = text.PadRight(32, ' ');
					}
				}
				lineName = Encoding.Default.GetBytes(text);
				finishChar = 59;
			}
		}
	}
}

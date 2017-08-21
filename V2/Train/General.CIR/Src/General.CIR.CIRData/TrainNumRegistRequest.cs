using System.Runtime.InteropServices;
using System.Text;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct TrainNumRegistRequest
	{
		public byte Regist;

		public byte Order;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
		public byte[] trainNum;

		public byte finishChar;

		public string TrainNum
		{
			get
			{
				return Encoding.Default.GetString(trainNum);
			}
			set
			{
				string text = value;
				bool flag = text.Length > 7;
				if (flag)
				{
					text = text.Remove(7);
				}
				else
				{
					bool flag2 = text.Length < 7;
					if (flag2)
					{
						text = text.PadRight(7);
					}
				}
				trainNum = Encoding.Default.GetBytes(text);
				finishChar = 59;
			}
		}
	}
}

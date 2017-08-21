using System.Runtime.InteropServices;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct PressDownUpPTT
	{
		public byte actionPtt;

		public byte tailKeepTime;

		public byte tailContinueTime;
	}
}

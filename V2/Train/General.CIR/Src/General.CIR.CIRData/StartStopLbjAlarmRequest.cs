using System.Runtime.InteropServices;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct StartStopLbjAlarmRequest
	{
		public byte StartStop;
	}
}

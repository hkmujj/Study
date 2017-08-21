using System.Runtime.InteropServices;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct PowerStatusCtrl
	{
		public byte TrunOn;
	}
}

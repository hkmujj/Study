using System.Runtime.InteropServices;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct CIRTime
	{
		public byte hour;

		public byte minute;

		public byte second;
	}
}

using System.Runtime.InteropServices;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct CIRDate
	{
		public byte year;

		public byte month;

		public byte day;
	}
}

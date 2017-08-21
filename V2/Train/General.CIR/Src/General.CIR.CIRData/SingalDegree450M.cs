using System.Runtime.InteropServices;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SingalDegree450M
	{
		public byte lBSingal;

		public byte hBSingal;
	}
}

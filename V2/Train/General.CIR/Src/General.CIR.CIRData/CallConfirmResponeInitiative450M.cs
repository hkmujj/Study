using System.Runtime.InteropServices;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct CallConfirmResponeInitiative450M
	{
		public byte callStatus;

		public byte callNumber;

		public bool IsSucceed
		{
			get
			{
				return (callStatus & 128) == 128;
			}
		}

		public bool IsOnThePhone
		{
			get
			{
				return (callStatus & 64) == 64;
			}
		}
	}
}

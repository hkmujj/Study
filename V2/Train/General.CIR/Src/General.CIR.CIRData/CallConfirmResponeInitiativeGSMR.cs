using System.Runtime.InteropServices;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct CallConfirmResponeInitiativeGSMR
	{
		public byte callStatus;

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

		public bool IsGroupPhone
		{
			get
			{
				return (callStatus & 32) == 32;
			}
		}
	}
}

using General.CIR.CIRData;

namespace General.CIR.Events
{
	public class NetWorkEventArgs
	{
		public BusinessType BusinessType
		{
			get;
			set;
		}

		public CIRPacket Data
		{
			get;
			set;
		}
	}
}

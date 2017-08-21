namespace General.CIR.Events
{
	public class CallEventArgs
	{
		public string Name
		{
			get;
			private set;
		}

		public bool IsCall
		{
			get;
			private set;
		}

		public CallEventArgs(string name = "", bool isCall = false)
		{
			Name = name;
			IsCall = isCall;
		}
	}
}

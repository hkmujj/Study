using System;

namespace General.CIR.Models.Units
{
	public class ButtonUnits
	{
		public string Names
		{
			get;
			set;
		}

		public Action Action
		{
			get;
			set;
		}

		public void Response()
		{
			Action expr_07 = Action;
			if (expr_07 != null)
			{
				expr_07();
			}
		}
	}
}

using System.Linq;
using General.CIR.Models.Units;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class SerchBottomResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			int index = ViewModel.DispatchCommandViewModel.SelectSerchItem.Index;
			bool flag = index == 6;
			if (flag)
			{
				index = 1;
			}
			else
			{
				int index2 = index;
				index = index2 + 1;
			}
			ViewModel.DispatchCommandViewModel.SelectSerchItem = ViewModel.DispatchCommandViewModel.AllSerchItems.FirstOrDefault((SerchItem f) => f.Index == index);
		}

		public override void ClickDown()
		{
		}
	}
}

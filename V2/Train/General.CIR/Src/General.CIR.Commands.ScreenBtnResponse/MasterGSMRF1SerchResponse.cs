using System.Linq;
using General.CIR.Models.Units;
using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class MasterGSMRF1SerchResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			Response(BtnItemKeys.调度命令查询界面);
			ViewModel.DispatchCommandViewModel.Trips = InFoResource.命令查询界面提示信息;
			ViewModel.DispatchCommandViewModel.SelectSerchItem = ViewModel.DispatchCommandViewModel.AllSerchItems.FirstOrDefault<SerchItem>();
		}

		public override void ClickDown()
		{
		}
	}
}

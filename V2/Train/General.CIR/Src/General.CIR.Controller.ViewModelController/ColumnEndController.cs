using System.ComponentModel.Composition;
using System.Windows.Media;
using General.CIR.ViewModels;

namespace General.CIR.Controller.ViewModelController
{
	[Export]
	public class ColumnEndController : ControllerBase<ColumnEndViewModel>
	{
		private static readonly SolidColorBrush RedBrush = new SolidColorBrush(Colors.Red);

		private static readonly SolidColorBrush YellowBrush = new SolidColorBrush(Colors.Yellow);

		private static readonly SolidColorBrush BlackBrush = new SolidColorBrush(Colors.Black);

		public void UpdateState()
		{
			bool isRequst = ViewModel.IsRequst;
			if (isRequst)
			{
				ViewModel.ColumnEndStatusBrush = YellowBrush;
				ViewModel.ColumnEndStatusStr = "请求建立连接";
				ViewModel.ColumnEndStr = "列尾:";
				ViewModel.FanPressDisplay = "风压:";
			}
			else
			{
				bool isConnect = ViewModel.IsConnect;
				if (isConnect)
				{
					ViewModel.ColumnEndStatusBrush = BlackBrush;
					ViewModel.ColumnEndStatusStr = "已连接";
					ViewModel.ColumnEndStr = "列尾状态:";
					ViewModel.FanPressDisplay = string.Format("尾部风压:{0}千帕 ID:{1}", ViewModel.FanPress, ViewModel.ID);
				}
				else
				{
					ViewModel.ColumnEndStatusBrush = RedBrush;
					ViewModel.ColumnEndStatusStr = "未连接";
					ViewModel.ColumnEndStr = "列尾:";
					ViewModel.FanPressDisplay = "风压:";
				}
			}
		}

		public override void Initialize()
		{
			ViewModel.ColumnEndStatusBrush = RedBrush;
			ViewModel.ColumnEndStatusStr = "未连接";
			ViewModel.ColumnEndStr = "列尾:";
			ViewModel.FanPressDisplay = "风压:";
			ViewModel.IsConnect = false;
			ViewModel.IsRequst = false;
		}
	}
}

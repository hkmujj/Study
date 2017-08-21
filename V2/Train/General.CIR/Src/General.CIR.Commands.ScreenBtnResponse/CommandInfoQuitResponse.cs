using System.Collections.ObjectModel;
using System.Linq;
using General.CIR.Models.Units;
using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class CommandInfoQuitResponse : BtnResponseBase
    {

        public override void ClickUp()
        {

            var collection = ViewModel.DispatchCommandViewModel.AllUnit.Where(w => w.CommandType == ViewModel.DispatchCommandViewModel.DisplayUnit.CommandType);
            ViewModel.DispatchCommandViewModel.DisplayUnits = new ObservableCollection<DispatchCommandUnit>(collection);
            ViewModel.DispatchCommandViewModel.SelectUnit = ViewModel.DispatchCommandViewModel.DisplayUnits.FirstOrDefault<DispatchCommandUnit>();
            ViewModel.Controller.NavigatorToKey(BtnItemKeys.调度命令查询界面列表界面);
            ViewModel.DispatchCommandViewModel.Trips = InFoResource.设置主界面提示信息;
        }

        public override void ClickDown()
        {
        }
    }
}

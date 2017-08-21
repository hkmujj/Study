using System.Linq;
using General.CIR.Models.Units;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class SerchNumber6Response : BtnResponseBase
    {


        public override void ClickUp()
        {
            SerchItem serchItem = ViewModel.DispatchCommandViewModel.AllSerchItems.FirstOrDefault(f => f.Index == 6);
            bool flag = serchItem != null;
            if (flag)
            {
                ViewModel.DispatchCommandViewModel.SelectSerchItem = serchItem;
            }
        }

        public override void ClickDown()
        {
        }
    }
}

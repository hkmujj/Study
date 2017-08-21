using System.Linq;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class SerchNumber4Response : BtnResponseBase
    {

        public override void ClickUp()
        {
            var serchItem = ViewModel.DispatchCommandViewModel.AllSerchItems.FirstOrDefault(f => f.Index == 4);
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

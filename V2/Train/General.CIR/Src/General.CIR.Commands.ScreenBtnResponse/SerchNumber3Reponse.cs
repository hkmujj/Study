using System.Linq;
using General.CIR.Models.Units;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class SerchNumber3Reponse : BtnResponseBase
    {


        public override void ClickUp()
        {

            SerchItem serchItem = ViewModel.DispatchCommandViewModel.AllSerchItems.FirstOrDefault(f => f.Index == 3);
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

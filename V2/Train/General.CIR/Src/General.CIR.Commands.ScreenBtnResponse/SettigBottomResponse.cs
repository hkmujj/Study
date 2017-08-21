using System.Linq;
using General.CIR.Models.Units;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class SettigBottomResponse : BtnResponseBase
    {
        public override void ClickUp()
        {

            SettingItem expr17 = ViewModel.SettingViewModel.SelectItem;
            var index = ((expr17 != null) ? new int?(expr17.Index) : null);
            bool hasValue = index.HasValue;
            if (hasValue)
            {
                bool flag = index == 10;
                if (flag)
                {
                    index = 1;
                }
                else
                {
                    int? index1 = index;
                    index = index1 + 1;
                }
                ViewModel.SettingViewModel.SelectItem = ViewModel.SettingViewModel.AllItems.FirstOrDefault(f => f.Index == index);
            }
        }

        public override void ClickDown()
        {
        }
    }
}

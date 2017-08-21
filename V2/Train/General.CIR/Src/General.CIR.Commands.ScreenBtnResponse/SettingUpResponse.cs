using System.Linq;
using General.CIR.Models.Units;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class SettingUpResponse : BtnResponseBase
    {
        public override void ClickUp()
        {

            SettingItem expr17 = ViewModel.SettingViewModel.SelectItem;
            var index1 = ((expr17 != null) ? new int?(expr17.Index) : null);
            bool hasValue = index1.HasValue;
            if (hasValue)
            {
                bool flag = index1 == 1;
                if (flag)
                {
                    index1 = new int?(10);
                }
                else
                {
                    int? index = index1;
                    index1 = index - 1;
                }
                ViewModel.SettingViewModel.SelectItem = ViewModel.SettingViewModel.AllItems.FirstOrDefault((SettingItem f) => f.Index == index1);
            }
        }

        public override void ClickDown()
        {
        }
    }
}

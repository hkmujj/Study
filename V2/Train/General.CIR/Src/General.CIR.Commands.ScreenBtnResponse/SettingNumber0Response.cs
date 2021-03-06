using System.Linq;
using General.CIR.Models.Units;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class SettingNumber0Response : BtnResponseBase
    {
       

        public override void ClickUp()
        {
            SettingItem settingItem = ViewModel.SettingViewModel.AllItems.FirstOrDefault(f => f.Index == 10);
            bool flag = settingItem != null;
            if (flag)
            {
                ViewModel.SettingViewModel.SelectItem = settingItem;
            }
        }

        public override void ClickDown()
        {
        }
    }
}

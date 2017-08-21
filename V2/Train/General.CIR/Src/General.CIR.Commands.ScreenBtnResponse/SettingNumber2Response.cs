using System.Linq;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class SettingNumber2Response : BtnResponseBase
    {
        //[CompilerGenerated]
        //[Serializable]
        //private sealed class <>c
        //{
        //	public static readonly SettingNumber2Response.<>c <>9 = new SettingNumber2Response.<>c();

        //	public static Func<SettingItem, bool> <>9__0_0;

        //	internal bool <ClickUp>b__0_0(SettingItem f)
        //	{
        //		return f.Index == 2;
        //	}
        //}

        public override void ClickUp()
        {
            var settingItem = ViewModel.SettingViewModel.AllItems.FirstOrDefault(f => f.Index == 2);
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

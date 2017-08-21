using System.Collections.ObjectModel;
using System.Linq;
using General.CIR.Models;
using General.CIR.Models.Units;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class MaininsatnceRightReponse : BtnResponseBase
    {
        //[CompilerGenerated]
        //[Serializable]
        //private sealed class <>c
        //{
        //	public static readonly MaininsatnceRightReponse.<>c <>9 = new MaininsatnceRightReponse.<>c();

        //	public static Func<MaininsatnceItem, bool> <>9__0_0;

        //	internal bool <ClickUp>b__0_0(MaininsatnceItem w)
        //	{
        //		return w.Page == 2;
        //	}
        //}

        public override void ClickUp()
        {
            bool flag = ViewModel.MaininstanceViewModel.SelectItem.Page != 2;
            if (flag)
            {
                ViewModel.MaininstanceViewModel.DisplayItems = new ObservableCollection<MaininsatnceItem>(GlobalParams.Instance.MainInsatnce.AllItems.Where(w => w.Page == 2));
            }
        }

        public override void ClickDown()
        {
        }
    }
}

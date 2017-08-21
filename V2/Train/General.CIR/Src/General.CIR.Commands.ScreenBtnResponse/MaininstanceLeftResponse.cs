using System.Collections.ObjectModel;
using System.Linq;
using General.CIR.Models;
using General.CIR.Models.Units;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class MaininstanceLeftResponse : BtnResponseBase
    {

        public override void ClickUp()
        {
            bool flag = ViewModel.MaininstanceViewModel.SelectItem.Page != 1;
            if (flag)
            {
                ViewModel.MaininstanceViewModel.DisplayItems = new ObservableCollection<MaininsatnceItem>(GlobalParams.Instance.MainInsatnce.AllItems.Where(w => w.Page == 1));
            }
        }

        public override void ClickDown()
        {
        }
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH5G.Staus.Menu
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MenuNineScreen : MenuScreenBase
    {
        public MenuNineScreen()
        {
            TextFileIndex = "MenuNineScreen";
            NameArr = new[]{ "×ó²àµÆ", "×ó½ü¹âµÆ", "×óÇ°ÕÕµÆ",
                                      "×óºìµÆ", "Í·µÆ", "ÓÒ²àµÆ",
                                      "ÓÒ½ü¹âµÆ", "ÓÒÇ°ÕÕµÆ", "ÓÒºìµÆ" };
        }
        //2
        public override string GetInfo()
        {
            return "²Ëµ¥9ÊÓÍ¼";
        }

        protected override void InitalizeButtomBtnContents(SateBottomButtonVisitor visitor)
        {
            visitor.ButtonContentCollection = new List<string>() { "7", "8", "9", "ÆäËü", "", "", "", "", "", Button10 };
            visitor.SelectableIndexCollection = new ReadOnlyCollection<int>(Enumerable.Range(0, 10).ToList());
        }

        protected override void ResponseUser()
        {

            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress) return;
            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 15://1
                    append_postCmd(CmdType.ChangePage, 7, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 16://2
                    append_postCmd(CmdType.ChangePage, 8, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 17://3
                    append_postCmd(CmdType.ChangePage, 9, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 18://ÆäËû
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;

            }
        }
    }
}